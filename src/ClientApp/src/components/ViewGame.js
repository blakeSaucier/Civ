import React, { useState, useEffect } from 'react';
import { Map } from "./Map";
import { get } from "../repo/apiRequest";
import {Container, Navbar, NavbarBrand} from "reactstrap";

function GameHeader(props) {
    return(
        <header>
            <Navbar className="border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand>{props.game.name}</NavbarBrand>    
                </Container>
            </Navbar>
        </header>
    )
}

function ViewGame(props) {
    const [ game, setGame] = useState({ name: "", regions: []});
    const [ civs, setCivs ] = useState([]);
    const { id } = props.match.params;
    
    useEffect(() => {
        const loadData = async () => {
            const game = await get(`/api/game/${id}/regions`)
                .then(res => res.json());
            setGame(game);
            const civs = await get(`/api/game/${id}/civs`)
              .then(res => res.json());
            setCivs(civs);
        }
        loadData();
    }, []);
    
    return (
        <div>
            <GameHeader game={game}/>
            <Container fluid={true} >
                <Map civs={civs} game={game}/>
            </Container>    
        </div>
    )
}

export { ViewGame };