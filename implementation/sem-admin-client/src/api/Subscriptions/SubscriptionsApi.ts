import axios from 'axios'
import { v4 as uuidv4 } from 'uuid'

import { ISubscriptionRow, ICreateFormFields, IUpdateFormFields, IDeleteFormFields } from '../../components'
import { ISubscriptionsResponse, ISubscriptionsPayload } from '../types'

const getSubscriptions = (): Promise<ISubscriptionRow[]> => {
  return axios.get('https://localhost:5001/subscriptions').then((response) => {
    const subscriptions = response.data as ISubscriptionsResponse[]
    const subscriptionsRows: ISubscriptionRow[] = subscriptions.map(subscription => ({
      id: subscription.Id,
      url: subscription.PostbackUrl,
      enabledEvents: subscription.Events.map(event => event.Name).join(', ')
    }))

    return subscriptionsRows
  })
}

const getSubscriptionById = (id: string): Promise<ISubscriptionRow> => {
  return axios.get(`https://localhost:5001/subscriptions/${id}`).then((response) => {
    const subscription = response.data as ISubscriptionsResponse
    const subscriptionRow: ISubscriptionRow = {
      id: subscription.Id,
      url: subscription.PostbackUrl,
      enabledEvents: subscription.Events.map(event => event.Name).join(', ')
    }

    return subscriptionRow
  })
}

const createSubscription = (formData: ICreateFormFields) => {
  const payload: ISubscriptionsPayload = {
    PostbackUrl: formData.url,
    EventIds: formData.enabledEvents.map(event => event.id),
    Secret: uuidv4()
  }

  axios.post('https://localhost:5001/subscriptions', payload)
}

const updateSubscription = (formData: IUpdateFormFields) => {
  const payload: ISubscriptionsPayload = {
    PostbackUrl: formData.url,
    EventIds: formData.enabledEvents.map(event => event.id),
    Secret: uuidv4()
  }

  axios.put(`https://localhost:5001/subscriptions/${formData.id}`, payload)
}

const deleteSubscription = (formData: IDeleteFormFields) => {
  axios.delete(`https://localhost:5001/subscriptions/${formData.id}`)
}

export { 
  getSubscriptions,
  getSubscriptionById,
  createSubscription,
  updateSubscription,
  deleteSubscription
}
