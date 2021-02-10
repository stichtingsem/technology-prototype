import React from 'react';
import logo from './logo.svg';
import { IDP, IClientConfig } from '@ccs/il.authentication.avatar'
import './App.css';

function App() {
  const avatarConfig: IClientConfig = {
    clientId: 'IDP.Playground',
    scope: 'openid fullname profile email role offline_access',
    idpUrl: 'https://il-authentication-web-noordhoff.azurewebsites.net/'
  }

  return (
    <div className="App">
      <header className="App-header">
        <IDP {...avatarConfig} />
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Checking changes applied to docker.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
