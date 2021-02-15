import axios from 'axios'
import { IGetEventsResponse } from './IGetEventsResponse'

const getEvents = (): Promise<IGetEventsResponse[]> => {
  return axios.get(`https://localhost:5001/events`).then((response) => {
    const subscriptions = response.data as IGetEventsResponse[]
    // TODO: map to the model for the UI
    return subscriptions
  })
}

export { getEvents }
