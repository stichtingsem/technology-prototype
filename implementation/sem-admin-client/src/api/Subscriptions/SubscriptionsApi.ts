import axios from 'axios'
import { ISubscriptionRow, ICreateFormFields, IUpdateFormFields, IDeleteFormFields } from '../../components'
import { ISubscriptionsResponse, ISubscriptionsPayload } from '../types'

const getSubscriptions = (): Promise<ISubscriptionRow[]> => {
  return axios.get('https://localhost:5001/subscriptions').then((response) => {
    const subscriptions = response.data as ISubscriptionsResponse[]
    const subscriptionsRows: ISubscriptionRow[] = subscriptions.map(subscription => ({
      id: subscription.id,
      url: subscription.postbackUrl,
      enabledEvents: subscription.enabledEvents.join()
    }))

    return subscriptionsRows
  })
}

const getSubscriptionById = (id: string): Promise<ISubscriptionRow> => {
  return axios.get(`https://localhost:5001/subscriptions/${id}`).then((response) => {
    const subscription = response.data as ISubscriptionsResponse
    const subscriptionRow: ISubscriptionRow = {
      id: subscription.id,
      url: subscription.postbackUrl,
      enabledEvents: subscription.enabledEvents.join()
    }

    return subscriptionRow
  })
}

const createSubscription = (formData: ICreateFormFields) => {
  const payload: ISubscriptionsPayload = {
    postbackUrl: formData.url,
    enabledEvents: formData.enabledEvents,
    secret: '' // TODO: generate random GUID or token cryptographically
  }

  axios.post('https://localhost:5001/subscriptions', payload)
}

const updateSubscription = (formData: IUpdateFormFields) => {
  const payload: ISubscriptionsPayload = {
    postbackUrl: formData.url,
    enabledEvents: formData.enabledEvents,
    secret: '' // TODO: do we have to generate this again?
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
