import axios from 'axios'
import { ICreateFormFields, IUpdateFormFields, IDeleteFormFields } from '../../components'
import { ISubscriptionsResponse, ISubscriptionsPayload } from '../types'

const getSubscriptions = (): Promise<ISubscriptionsResponse[]> => {
  return axios.get('https://localhost:5001/subscriptions').then((response) => {
    const subscriptions = response.data as ISubscriptionsResponse[]
    // TODO: map to the model for the UI
    return subscriptions
  })
}

const getSubscriptionById = (id: string): Promise<ISubscriptionsResponse> => {
  return axios.get(`https://localhost:5001/subscriptions/${id}`).then((response) => {
    const subscription = response.data as ISubscriptionsResponse
    // TODO: map to the model for the UI
    return subscription
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
