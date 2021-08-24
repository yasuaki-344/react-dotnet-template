import React, { Component, Fragment } from 'react';
import { ListItem } from '@material-ui/core';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';

export class LoginMenu extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      userName: null
    };
  }

  componentDidMount() {
    this._subscription = authService.subscribe(() => this.populateState());
    this.populateState();
  }

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  async populateState() {
    const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
    this.setState({
      isAuthenticated,
      userName: user && user.name
    });
  }

  render() {
    const { isAuthenticated, userName } = this.state;
    if (!isAuthenticated) {
      const registerPath = `${ApplicationPaths.Register}`;
      const loginPath = `${ApplicationPaths.Login}`;
      return this.anonymousView(registerPath, loginPath);
    } else {
      const profilePath = `${ApplicationPaths.Profile}`;
      const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
      return this.authenticatedView(userName, profilePath, logoutPath);
    }
  }

  authenticatedView(userName, profilePath, logoutPath) {
    return (<Fragment>
      <ListItem button>
        <Link to={profilePath}>ようこそ {userName}</Link>
      </ListItem>
      <ListItem button>
        <Link to={logoutPath}>ログアウト</Link>
      </ListItem>
    </Fragment>);
  }

  anonymousView(registerPath, loginPath) {
    return (<Fragment>
      <ListItem button>
        <Link to={registerPath}>登録</Link>
      </ListItem>
      <ListItem button>
        <Link to={loginPath}>ログイン</Link>
      </ListItem>
    </Fragment>);
  }
}
