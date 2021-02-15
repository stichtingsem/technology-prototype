import axios from 'axios'
import { IUpdateFormFields } from '../../components'
import { IUpdatePayload } from './IUpdatePayload'

const updateSubscription = (formData: IUpdateFormFields) => {
  const payload: IUpdatePayload = {
    url: formData.url,
    enabled_events: formData.enabledEvents
  }

  axios.put(`https://localhost:5001/subscriptions/${formData.id}`, payload)
}

export { updateSubscription }
