﻿import React, { useState, useEffect } from 'react';
import { Map } from "./Map";
import { get } from "../repo/apiRequest";
import {Container, Navbar, NavbarBrand, NavbarText } from "reactstrap";
import {Link} from "react-router-dom";

function GameHeader(props) {
    return(
        <header>
            <Navbar className="border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand tag={Link} to="/">Civ</NavbarBrand>
                    
                    <NavbarBrand>{props.game.name}</NavbarBrand>
                    <NavbarText>{props.game.code}</NavbarText>
                </Container>
            </Navbar>
        </header>
    )
}

function ViewGame(props) {
    const [ game, setGame] = useState({ name: "", regions: [], civs: []});
    const { id } = props.match.params;
    
    useEffect(() => {
        const loadData = async () => {
            const game = await get(`/api/game/${id}/regions`)
                .then(res => res.json());
            const civs = await get(`/api/game/${id}/civs`)
              .then(res => res.json());
            setGame({...game, civs})
        }
        loadData();
    }, []);
    
    return (
        <div>
            <GameHeader game={game}/>
            <Container fluid={true} >
                <Map game={game}/>
            </Container>    
        </div>
    )
}

export { ViewGame };