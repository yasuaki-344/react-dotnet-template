import React, { useEffect, useState } from 'react'
import { Route, Redirect } from 'react-router-dom'
import { ApplicationPaths, QueryParameterNames } from './ApiAuthorizationConstants'
import authService from './AuthorizeService'

export const AuthorizeRoute = (props) => {
  const [ready, setReady] = useState(false);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  let _subscription = authService.subscribe(() => authenticationChanged());

  useEffect(() => {
    populateAuthenticationState();
    return () => {
      authService.unsubscribe(_subscription);
    }
  }, []);

  const populateAuthenticationState = async () => {
    const authenticated = await authService.isAuthenticated();
    setReady(true);
    setIsAuthenticated(authenticated);
  }

  const authenticationChanged = async () => {
    setReady(false);
    setIsAuthenticated(false);
    await populateAuthenticationState();
  }

  var link = document.createElement("a");
  link.href = props.path;
  const returnUrl = `${link.protocol}//${link.host}${link.pathname}${link.search}${link.hash}`;
  const redirectUrl = `${ApplicationPaths.Login}?${QueryParameterNames.ReturnUrl}=${encodeURIComponent(returnUrl)}`
  if (!ready) {
    return <div></div>;
  } else {
    const { component: Component, ...rest } = props;
    return <Route {...rest}
      render={(props) => {
        if (isAuthenticated) {
          return <Component {...props} />
        } else {
          return <Redirect to={redirectUrl} />
        }
      }} />
  }
}
