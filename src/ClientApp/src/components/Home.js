import React, { Component } from 'react';
import {Button, Card, CardBody, Col, Container, Form, FormGroup, Input, Jumbotron, Label, Row} from "reactstrap";
import GoToGame from "./GoToGame";

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <Jumbotron fluid>
          <Container fluid>
            <h1 className="display-3">Civ</h1>
            <p className="lead">Turn based pre-historical stragety game</p>
          </Container>
        </Jumbotron>
        
        <Row>
          <Col md={3} sm={12}>
            <GoToGame />
          </Col>
        </Row>
      </div>
    );
  }
}
