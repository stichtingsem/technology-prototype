import axios from 'axios'
import { IEventsResponse } from '../types'
import { IEventDropdownOption } from '../../components'

const getEvents = (): Promise<IEventDropdownOption[]> => {
  return axios.get(`https://localhost:5001/events`).then((response) => {
    const events = response.data as IEventsResponse[]
    const eventDropdownOptions: IEventDropdownOption[] = events.map(event => ({
      id: event.Id,
      name: event.Name
    }))

    return eventDropdownOptions
  })
}

export { getEvents }
