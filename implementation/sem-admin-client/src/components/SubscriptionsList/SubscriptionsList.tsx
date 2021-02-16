import React, { useState, useEffect } from 'react';

import { ISubscriptionRow } from './ISubscriptionRow'
import { getSubscriptions } from '../../api'
import './SubscriptionsList.css';

const SubscriptionsList = () => {
  const [subscriptions, setSubscriptions] = useState<ISubscriptionRow[]>([])
  useEffect(() => {
    getSubscriptions().then(result => setSubscriptions(result)).catch(() => {
      setSubscriptions([
        { id: 'aaaa-bbbb-cccc-dddd', url: 'https://test.com', enabledEvents: 'mp.entitlement.active,mp.entitlement.refunded'},
        { id: 'eeee-ffff-gggg-hhhh', url: 'https://test.com', enabledEvents: 'mp.entitlement.updated'},
        { id: 'iiii-jjjj-kkkk-llll', url: 'https://test.com', enabledEvents: 'sis.enrollment,sis.student'},
      ])
    })
  }, [])

  return (
    <>
      <h2>Existing Webhooks</h2>
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
