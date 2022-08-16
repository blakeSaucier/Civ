import React, { Component } from 'react';
import {NavLink, Row} from "reactstrap";
import GoToGame from "./GoToGame";
import {Link} from "react-router-dom";

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <div className="col-md-8-fluid text-center">
          <h1 className="mb-3">Civilization</h1>
          <p className="lead mb-4">
            Turn based pre-historical stragety game
            
          </p>
          <br/>
          <br/>
          <br/>

          <Row className="justify-content-center">
              <NavLink tag={Link} className="btn btn-primary btn-lg" to="/new" style={{display: 'inline-block'}}>New Game</NavLink>
          </Row>
          <br/>
          <Row className="justify-content-center">
            <GoToGame/>
          </Row>
        </div>
      </div>
    );
  }
}
