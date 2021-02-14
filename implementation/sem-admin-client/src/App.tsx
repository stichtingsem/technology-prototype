import React, { useState } from 'react';
import { IDP, IClientConfig } from '@ccs/il.authentication.avatar'

import { 
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

  const [visibleSection, setVisibleSection] = useState<string>('create')

  return (
    <div className="App">
      <IDP {...avatarConfig} />
      <ul>
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
      {visibleSection === 'create' && <CreateSubscriptionForm />}
      {visibleSection === 'update' && <UpdateSubscriptionForm />}
      {visibleSection === 'delete' && <DeleteSubscriptionForm />}
    </div>
  );
}

export default App;
