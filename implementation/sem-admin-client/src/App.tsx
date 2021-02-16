import React, { useState } from 'react';
import { IDP, IClientConfig } from '@ccs/il.authentication.avatar'

import {
  SubscriptionsList,
  CreateSubscriptionForm, 
  UpdateSubscriptionForm, 
  DeleteSubscriptionForm
} from './components'
import './App.css';

function App() {
  const avatarConfig: IClientConfig = {
    clientId: 'IDP.Playground',
    scope: 'openid fullname profile email role offline_access',
    idpUrl: 'https://il-authentication-web-noordhoff.azurewebsites.net/'
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
      {/* <div className="header">
        <ul>
          <li>
            <span className={visibleSection === 'view' ? "active" : ""} onClick={() => setVisibleSection('view')}>View</span>
          </li>
          <li>
            <span className={visibleSection === 'create' ? "active" : ""} onClick={() => setVisibleSection('create')}>Create</span>
          </li>
          <li>
            <span className={visibleSection === 'update' ? "active" : ""} onClick={() => setVisibleSection('update')}>Update</span>
          </li>
          <li>
            <span className={visibleSection === 'delete' ? "active" : ""} onClick={() => setVisibleSection('delete')}>Delete</span>
          </li>
        </ul>
        <IDP {...avatarConfig} />
      </div> */}
      <div className="content">
        {visibleSection === 'view' && <SubscriptionsList />}
        {visibleSection === 'create' && <CreateSubscriptionForm />}
        {visibleSection === 'update' && <UpdateSubscriptionForm />}
        {visibleSection === 'delete' && <DeleteSubscriptionForm />}
      </div>
    </div>
  );
}

export default App;
