import React, { useState } from 'react';
import {
    MapContainer,
    Popup,
    ImageOverlay,
    Polygon,
    Tooltip
} from 'react-leaflet';
import {CRS} from 'leaflet';

const bounds = [[0,0], [349, 465]];

function Region ({ region }) {
    const [color, setColor] = useState(region.color);
    console.log(color);
    const handleColorChange = e => setColor(e.target.value);
    const displayOptions =
        {
            color: color === '' ? 'grey' : color,
            opacity: color === '' ? 0.2 : 1.0
        }
    
    return (
        <Polygon
            positions={region.coords}
            key={region.regionName}
            pathOptions={displayOptions}>
            <Tooltip>{region.regionName}</Tooltip>
            
            <Popup>
                {region.civName}
                <br/>
                Select a color
                <br/>
                <select
                    name="color"
                    id="grey"
                    onChange={handleColorChange}>
                    <option value={""}>none</option>
                    <option value={"blue"}>blue</option>
                    <option value={"red"}>red</option>
                    <option value={"purple"}>purple</option>
                    <option value={"yellow"}>yellow</option>
                </select>
            </Popup>
        </Polygon>
    );
}

function ImageMap() {
    return (
        <ImageOverlay
            url="https://papercivstorage.blob.core.windows.net/map/paper-civ.png"
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
                { game.regions.map(region => <Region region={region} key={region.regionName} />) }
            </MapContainer>
        </div>
    )
}

export { Map };