import React, { useState} from 'react';
import { useHistory } from 'react-router-dom'
import { Button, Card, CardBody, Form, FormGroup, Input, Label, Spinner } from "reactstrap";
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
        <Card>
            <CardBody>
                <Form onSubmit={handleSubmit}>
                    <FormGroup>
                        <Label for="gameCode">Game Code or Id</Label>
                        <Input onChange={handleGameCodeUpdate} type="text" name="gameCode"/>
                    </FormGroup>
                    { submitting 
                        ? <Spinner color="primary"/>
                        : <Button>Go to game</Button> }
                </Form>
            </CardBody>
        </Card>
    )
}

export default GoToGame;