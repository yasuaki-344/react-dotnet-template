import React, { FC } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { ApplicationPaths } from './api-authorization/ApiAuthorizationConstants';
import { ApiAuthorizationRoutes } from './api-authorization/ApiAuthorizationRoutes';
import './custom.css'
import AuthorizeRoute from './api-authorization/AuthorizaRoute';

export const App: FC = (): JSX.Element => {
  return (
    <Layout>
      <Route exact path='/' component={Home} />
      <Route path='/counter' component={Counter} />
      <AuthorizeRoute path='/fetch-data' component={FetchData} />
      <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
    </Layout>
  );
}

App.displayName = App.name;
