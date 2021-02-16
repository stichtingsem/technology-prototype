import React, { useState, useEffect } from 'react';

import { ISubscriptionRow } from './ISubscriptionRow'
import { getSubscriptions } from '../../api'
import './SubscriptionsList.css';

const SubscriptionsList = () => {
  const [subscriptions, setSubscriptions] = useState<ISubscriptionRow[]>([])
  useEffect(() => {
    getSubscriptions().then(result => setSubscriptions(result))
  }, [])

  return (
    <>
      <h2>Subscriptions</h2>
      <table>
        <tr>
          <th>Id</th>
          <th>URL</th>
          <th>Enabled Events</th>
        </tr>
        {subscriptions.map(subscription => 
          <tr>
            <td>{subscription.id}</td>
            <td>{subscription.url}</td>
            <td>{subscription.enabledEvents}</td>
          </tr>
        )}
      </table>
    </>
  );
}

export { SubscriptionsList }
