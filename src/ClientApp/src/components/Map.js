import React, {useState} from 'react';
import {
    MapContainer,
    Popup,
    ImageOverlay,
    Polygon,
    Tooltip
} from 'react-leaflet';
import {CRS} from 'leaflet';
import {del, post} from "../repo/apiRequest";

const bounds = [[0,0], [349, 465]];

function AssignRegionToCiv ({region, game, onCivPicked}) {
  const [selectedCivId, setSelectedCivId] = useState(region.civId)
  
  const handleColorPicked = async (e) => {
    const civ = game.civs.find(civ => civ.id === parseInt(e.target.value));
    onCivPicked(civ);
    if (!civ) {
      await del(`/api/game/${game.gameId}/region`, {
        civId: selectedCivId,
        regionCode: region.code
      }).then(() => setSelectedCivId(0));
    } else {
      await post(`/api/game/${game.gameId}/region`, {
        civId: civ.id,
        regionCode: region.code
      }).then(() => setSelectedCivId(civ.id));
    }
  }
  
  return (
    <form>
      <div className="form-group">
        <label htmlFor="regionSelector">Assign {region.regionName} to</label>
        <select 
          className="form-control"
          name="color"
          id="regionSelector"
          onChange={handleColorPicked}
          defaultValue={selectedCivId}>
          <option value={0} id={0}>None</option>
          {game.civs.map(civ =>
            <option key={civ.id} id={civ.id} value={civ.id}>{civ.name} </option>)}
        </select>
      </div>
    </form>
  );
}

function Region ({ region, game }) {
    
    const initial = () => 
        game.civs.find(civ => civ.id === region.civId);
      
    const [ownedByCiv, setOwnedByCiv] = useState(initial());
    const color = ownedByCiv ? ownedByCiv.color : 'grey';
    const opacity = ownedByCiv ? 1.0 : 0.2;
    const displayOptions = { color, opacity }
    
    return (
        <Polygon
            positions={region.coords}
            pathOptions={displayOptions}>
            <Tooltip>{region.regionName}</Tooltip>
            
            <Popup>
              <div className="card-body">
                <h5 className="card-title">{region.regionName}</h5>
                <h6 className="card-subtitle">Owned by 
                  <strong> {ownedByCiv ? ownedByCiv.name : 'nobody'}</strong>
                </h6>
                <p className="card-text">Some interesting facts about {region.regionName}</p>
                <AssignRegionToCiv region={region} game={game} onCivPicked={setOwnedByCiv} />
              </div>
            </Popup>
        </Polygon>
    );
}

function ImageMap() {
    return (
        <ImageOverlay
            url="/paper-civ.png"
            bounds={bounds} />
    )
}

function Map({ game }) {
    return (
        <div id="map">
            <MapContainer
                crs={CRS.Simple}
                bounds={bounds}
                center={[175, 232]}
                zoom={2}>
                <ImageMap/>
                { game.regions.map(region => <Region key={region.regionName} game={game} region={region} />) }
            </MapContainer>
        </div>
    )
}

export { Map };