import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { ViewGame } from './components/ViewGame';
import { New } from './components/New';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Switch>
            <Route path="/game/:id" component={ViewGame} />
            <Route path={["new", "/"]}>
                <Layout>
                    <Route exact path='/' component={Home} />
                    <Route path='/new' component={New} />
                </Layout>
            </Route>
        </Switch>
    );
  }
}
