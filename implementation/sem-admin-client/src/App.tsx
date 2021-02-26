import React, { useState } from 'react';
import { IDP, IClientConfig } from '@ccs/il.authentication.avatar'

import {
  SubscriptionsList,
  CreateSubscriptionForm, 
  UpdateSubscriptionForm, 
  DeleteSubscriptionForm
} from './components'
import './App.css';

function getClientConfig() {
  return fetch('/client_config.json')
    .then((response) => { return response.json() })
    .catch((error) => {
      console.error(error);
    });
}

function App() {
  const clientConfig = getClientConfig();
  clientConfig.then(config => {
    const idpUrl = config.IdpUrl;
    const avatarConfig: IClientConfig = {
      clientId: 'IDP.Playground',
      scope: 'openid fullname profile email role offline_access',
      idpUrl: idpUrl
    }

    const [visibleSection, setVisibleSection] = useState<string>('view')

    return (
      <div>
        <header className="header">
          <nav>
            <span className="navbar_link" onClick={() => setVisibleSection('view')}>View</span>
            <span className="navbar_link" onClick={() => setVisibleSection('create')}>Create</span>
            <span className="navbar_link" onClick={() => setVisibleSection('update')}>Update</span>
            <span className="navbar_link" onClick={() => setVisibleSection('delete')}>Delete</span>
          </nav>
          <IDP {...avatarConfig} />
        </header>
        <div className="content">
          {visibleSection === 'view' && <SubscriptionsList />}
          {visibleSection === 'create' && <CreateSubscriptionForm />}
          {visibleSection === 'update' && <UpdateSubscriptionForm />}
          {visibleSection === 'delete' && <DeleteSubscriptionForm />}
        </div>
      </div>
    );
  })
}

export default App;
