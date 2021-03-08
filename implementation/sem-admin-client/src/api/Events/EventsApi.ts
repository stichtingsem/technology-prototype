import axios from 'axios'
import { IEventsResponse } from '../types'
import { IEventDropdownOption } from '../../components'
import { getJwtToken } from '@ccs/il.authentication.avatar'

const getHeaders = async () => {
  const token = await getJwtToken()
  
  return {
    headers: { Authorization: `Bearer ${token}` }
  }
}

const getEvents = async (): Promise<IEventDropdownOption[]> => {
  const headers = await getHeaders()

  return axios.get('api/eventtypes', headers).then((response) => {
    const events = response.data as IEventsResponse[]
    const eventDropdownOptions: IEventDropdownOption[] = events.map(event => ({
      id: event.id,
      name: event.name
    }))

    return eventDropdownOptions
  })
}

export { getEvents }
