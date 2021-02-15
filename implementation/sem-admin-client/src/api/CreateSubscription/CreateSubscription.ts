import axios from 'axios'
import { ICreateFormFields } from '../../components'
import { ICreatePayload } from './ICreatePayload'

const createSubscription = (formData: ICreateFormFields) => {
  const payload: ICreatePayload = {
    url: formData.url,
    enabled_events: formData.enabledEvents
  }

  axios.post('https://localhost:5001/subscriptions', payload)
}

export { createSubscription }
