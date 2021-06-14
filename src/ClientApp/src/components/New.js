import React, { useState } from 'react';
import {Button, Col, Form, FormGroup, Input, Label, Row, Spinner} from 'reactstrap';
import { range } from 'ramda';
import { post } from '../repo/apiRequest';

function CivDetails(props) {
    return (
        <Row form>
            <Col md={6}>
                <FormGroup>
                    <Label for={`civ_name`}>Civ Name</Label>
                    <Input 
                        required 
                        type="text" 
                        name={`name`}
                        id={`name${props.i}`}
                        value={props.name}
                        onChange={props.updateCiv}/>
                </FormGroup>
            </Col>
            <Col md={3}>
                <FormGroup>
                    <Label for={`civ_color`}>Color</Label>
                    <Input
                        required
                        type="color"
                        name={`color`}
                        id={`color${props.i}`}
                        onChange={props.updateCiv}
                        value={props.color}/>
                </FormGroup>
            </Col>
        </Row>
    )
}

const initialFormState = {
    gameName: '',
    password: '',
    civCount: 2,
    civs: [
        { name: '', color: '#000000' },
        { name: '', color: '#000000' }
    ]
}

function New(props) {
    const [submitting, setSubmitting] = useState(false);
    const [formState, setFormState] = useState(initialFormState);
    
    const handleSubmit = async e => {
        e.preventDefault();
        setSubmitting(true);
        const { gameId } = await post('/api/game', formState)
            .then(res => res.json());
        setSubmitting(false);
        props.history.push(`/game/${gameId}`);
    }
        
    const formDataChanged = e => {
        const res = {
            ...formState,
            [e.target.name]: e.target.value
        };
        setFormState(res);
    }

    const civCountOnChange = e => {
        const numberOfCivs = parseInt(e.target.value);
        const civs = range(0, numberOfCivs).map(i => formState.civs[i] || { name: '', color: '#000000' });
        setFormState({
            ...formState,
            civCount: numberOfCivs,
            civs,
        });
    }
    
    const updateCiv = (civ, index) => {
        const { civs } = formState;
        civs[index][civ.target.name] = civ.target.value;
        setFormState({
            ...formState,
            civs,
        });
    }
    
    return (
        <Form onSubmit={handleSubmit}>
            <Row form>
                <Col md={6}>
                    <h2>New Game</h2>
                    <FormGroup>
                        <Label for="gameName">Give this game a name</Label>
                        <Input 
                            required 
                            type="text" 
                            name="gameName" 
                            id="gameName"
                            value={formState.gameName}
                            onChange={formDataChanged}/>
                    </FormGroup>
                    <FormGroup>
                        <Label for="password">Admin Password</Label>
                        <Input 
                            required 
                            type="password" 
                            name="password" 
                            id="password"
                            value={formState.password}
                            onChange={formDataChanged}/>
                    </FormGroup>
                    <FormGroup>
                        <Label for="civ_count">Number of Civilizations</Label>
                        <Input 
                            required
                            type="number" 
                            name="civ_count" 
                            onChange={civCountOnChange}
                            value={formState.civCount} />
                    </FormGroup>
                    {formState.civs.map((civ, i) => {
                        return (
                            <CivDetails 
                                name={civ.name}
                                color={civ.color}
                                i={i}
                                key={i}
                                updateCiv={e => {
                                    updateCiv(e, i);
                                }}/>
                        );
                    })}
                    {submitting
                        ? <Spinner color="primary"/>
                        : <Button type="submit">Create</Button>}
                </Col>
            </Row>
        </Form>
    )
}

export { New }