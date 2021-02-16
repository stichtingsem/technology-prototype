import axios from 'axios'
import { IEventsResponse } from '../types'

const getEvents = (): Promise<IEventsResponse[]> => {
  return axios.get(`https://localhost:5001/events`).then((response) => {
    const events = response.data as IEventsResponse[]
    // TODO: map to the model for the UI
    return events
  })
}

export { getEvents }
