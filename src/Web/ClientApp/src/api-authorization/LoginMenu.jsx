import React, { useState, useEffect, useRef, Fragment } from 'react';
import { ListItem } from '@material-ui/core';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';

export const LoginMenu = (props) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [userName, setUserName] = useState(null);
  let _subscription = authService.subscribe(() => populateState());

  useEffect(() => {
    populateState();
    return () => {
      authService.unsubscribe(_subscription);
    }
  }, [])

  const populateState = async () => {
    const [authenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
    setIsAuthenticated(authenticated);
    setUserName(user && user.name);
  }

  const authenticatedView = (userName, profilePath, logoutPath) => (
    <Fragment>
      <ListItem button>
        <Link to={profilePath}>ようこそ {userName}</Link>
      </ListItem>
      <ListItem button>
        <Link to={logoutPath}>ログアウト</Link>
      </ListItem>
    </Fragment>
  );

  const anonymousView = (registerPath, loginPath) => (
    <Fragment>
      <ListItem button>
        <Link to={registerPath}>登録</Link>
      </ListItem>
      <ListItem button>
        <Link to={loginPath}>ログイン</Link>
      </ListItem>
    </Fragment>
  );

  if (!isAuthenticated) {
    const registerPath = `${ApplicationPaths.Register}`;
    const loginPath = `${ApplicationPaths.Login}`;
    return anonymousView(registerPath, loginPath);
  } else {
    const profilePath = `${ApplicationPaths.Profile}`;
    const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
    return authenticatedView(userName, profilePath, logoutPath);
  }
}
