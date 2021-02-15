import axios from 'axios'
import { IGetSubscriptionsResponse } from './IGetSubscriptionsResponse'

const getSubscriptions = (): Promise<IGetSubscriptionsResponse[]> => {
  return axios.get(`https://localhost:5001/subscriptions`).then((response) => {
    const subscriptions = response.data as IGetSubscriptionsResponse[]
    // TODO: map to the model for the UI
    return subscriptions
  })
}

export { getSubscriptions }
