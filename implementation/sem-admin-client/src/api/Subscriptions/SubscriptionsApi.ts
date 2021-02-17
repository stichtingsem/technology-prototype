import axios from 'axios'
import { ISubscriptionRow, ICreateFormFields, IUpdateFormFields, IDeleteFormFields } from '../../components'
import { ISubscriptionsResponse, ISubscriptionsPayload } from '../types'

const getSubscriptions = (): Promise<ISubscriptionRow[]> => {
  return axios.get('https://localhost:5001/subscriptions').then((response) => {
    const subscriptions = response.data as ISubscriptionsResponse[]
    const subscriptionsRows: ISubscriptionRow[] = subscriptions.map(subscription => ({
      id: subscription.Id,
      url: subscription.PostbackUrl,
      enabledEvents: subscription.Events.join()
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
      enabledEvents: subscription.Events.join()
    }

    return subscriptionRow
  })
}

const createSubscription = (formData: ICreateFormFields) => {
  console.log({formData})
  const payload: ISubscriptionsPayload = {
    PostbackUrl: formData.url,
    EventIds: formData.enabledEvents.map(event => event.id),
    Secret: '' // TODO: generate random GUID or token cryptographically
  }

  axios.post('https://localhost:5001/subscriptions', payload)
}

const updateSubscription = (formData: IUpdateFormFields) => {
  console.log({formData})
  const payload: ISubscriptionsPayload = {
    PostbackUrl: formData.url,
    EventIds: formData.enabledEvents.map(event => event.id),
    Secret: '' // TODO: do we have to generate this again?
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
