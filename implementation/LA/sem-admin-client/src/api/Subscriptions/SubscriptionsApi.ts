import axios from 'axios'
import { v4 as uuidv4 } from 'uuid'
import { getJwtToken } from '@ccs/il.authentication.avatar'

import { ISubscriptionRow, ICreateFormFields, IUpdateFormFields, IDeleteFormFields } from '../../components'
import { ISubscriptionsResponse, ISubscriptionsPayload } from '../types'

const getHeaders = async () => {
  const token = await getJwtToken()
  
  return {
    headers: { Authorization: `Bearer ${token}` }
  }
}

const getSubscriptions = async (): Promise<ISubscriptionRow[]> => {
  const headers = await getHeaders()

  return axios.get('api/webhooks', headers).then((response) => {
    const subscriptions = response.data as ISubscriptionsResponse[]
    const subscriptionsRows: ISubscriptionRow[] = subscriptions.map(subscription => ({
      id: subscription.id,
      url: subscription.postbackUrl,
      enabledEvents: subscription.events.map(event => event.name).join(', ')
    }))

    return subscriptionsRows
  })
}

const getSubscriptionById = async (id: string): Promise<ISubscriptionRow> => {
  const headers = await getHeaders()

  return axios.get(`api/webhooks/${id}`, headers).then((response) => {
    const subscription = response.data as ISubscriptionsResponse
    const subscriptionRow: ISubscriptionRow = {
      id: subscription.id,
      url: subscription.postbackUrl,
      enabledEvents: subscription.events.map(event => event.name).join(', ')
    }

    return subscriptionRow
  })
}

const createSubscription = async (formData: ICreateFormFields) => {
  const payload: ISubscriptionsPayload = {
    postbackUrl: formData.url,
    eventIds: formData.enabledEvents.map(event => event.id),
    secret: uuidv4()
  }
  const headers = await getHeaders()

  axios.post('api/webhooks', payload, headers)
}

const updateSubscription = async (formData: IUpdateFormFields) => {
  const payload: ISubscriptionsPayload = {
    postbackUrl: formData.url,
    eventIds: formData.enabledEvents.map(event => event.id),
    secret: uuidv4()
  }
  const headers = await getHeaders()

  axios.put(`api/webhooks/${formData.id}`, payload, headers)
}

const deleteSubscription = async (formData: IDeleteFormFields) => {
  const headers = await getHeaders()

  axios.delete(`api/webhooks/${formData.id}`, headers)
}

export { 
  getSubscriptions,
  getSubscriptionById,
  createSubscription,
  updateSubscription,
  deleteSubscription
}
