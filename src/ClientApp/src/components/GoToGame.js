import React, { useState} from 'react';
import { useHistory } from 'react-router-dom'
import { Spinner } from "reactstrap";
import { get } from "../repo/apiRequest";

function GoToGame() {
    const history = useHistory();
    const [gameCode, setGameCode] = useState(null);
    const [submitting, setSubmitting] = useState(false);
    
    const handleSubmit = async e => {
        e.preventDefault();
        setSubmitting(true);
        const res = await get(`/api/game/search?query=${gameCode}`);
        setSubmitting(false);
        if (res.ok) {
            const { id } = await res.json();
            history.push(`/game/${id}`);
        }
    }
    
    const handleGameCodeUpdate = e => {
        const { value } = e.target;
        setGameCode(value);
    }
    
    return(
      <div className="input-group input-group-lg col-md-4">
          <input onChange={handleGameCodeUpdate} type="text" className="form-control" placeholder="Game Id or code" />
          <div className="input-group-append">
              { submitting
                ? <Spinner color="primary"/>
                : <button onClick={handleSubmit} className="btn btn-primary btn-lg">Go to game</button> }
          </div>
          
      </div>
    )
}

export default GoToGame;