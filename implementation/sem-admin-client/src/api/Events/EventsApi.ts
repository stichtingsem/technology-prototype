import axios from 'axios'
import { IEventsResponse } from '../types'
import { IEventDropdownOption } from '../../components'
// import { getJwtToken } from '@ccs/il.authentication.avatar'

const getHeaders = async () => {
  // const token = await getJwtToken()
  const token = 'eyJhbGciOiJSUzI1NiIsImtpZCI6IjkxOUE1RDM5M0JEMjBDNjkzMUE0MjgxQTREREMwM0FFNTI4NjA0RjUiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJrWnBkT1R2U0RHa3hwQ2dhVGR3RHJsS0dCUFUifQ.eyJuYmYiOjE2MTM2NjI2NzEsImV4cCI6MTYxMzY2Mzg3MSwiaXNzIjoiaHR0cHM6Ly9pZGVudGl0eS5ub29yZGhvZmYubmwiLCJhdWQiOiJ0ZWNobmlla29wbWFhdC9UUCIsIm5vbmNlIjoiZ3VpZCIsImlhdCI6MTYxMzY2MjY3MSwiYXRfaGFzaCI6IndJOC1rTURidDdsRUstOHVoclNfNUEiLCJzX2hhc2giOiJONVA3Q2hYaFZMU2lUQ2VkMVdrMDNBIiwic2lkIjoiLTdXUEZsMmRhU1J6THFoY0xtTTg3ZyIsInN1YiI6Ijk4NzE0NmI3LWM5MDUtNDc1MS1iNzBhLWU0YTI0MzA0NTkyNCIsImF1dGhfdGltZSI6MTYxMzY2MjE2MCwiaWRwIjoibG9jYWwiLCJwcmltYXJ5c2lkIjoiOTg3MTQ2YjctYzkwNS00NzUxLWI3MGEtZTRhMjQzMDQ1OTI0IiwiZW1haWwiOiJudXJpYS5leHRyZW1hZG91cm9AaW5maW5pdGFzbGVhcm5pbmcuY29tIiwiZW1haWxfdmVyaWZpZWQiOiJ0cnVlIiwibmFtZSI6Ik51cmlhIEV4dHJlbWFkb3VybyIsInVuaXF1ZV9uYW1lIjoiTnVyaWEgRXh0cmVtYWRvdXJvIiwiZ2l2ZW5fbmFtZSI6Ik51cmlhIiwiZmFtaWx5X25hbWUiOiJFeHRyZW1hZG91cm8iLCJtaWRkbGVfbmFtZSI6IiIsInJvbGUiOlsiTm9vcmRob2ZmVXNlciIsIkljdENvb3JkaW5hdG9yIl0sInNlbGVjdGVkc2Nob29sIjoiODllN2ZlOWEtNWY1My00MGNlLWI1MGMtMDYzMTI1OThhYjc5IiwiYWN0T3JnSWQiOiI4OWU3ZmU5YS01ZjUzLTQwY2UtYjUwYy0wNjMxMjU5OGFiNzkiLCJsaWNlbnNlIjoidGVjaG5pZWtvcG1hYXQvVFAiLCJpc190ZWFjaGVyIjoiZmFsc2UiLCJhbXIiOlsicHdkIl19.tDiKoO2Dac6KO7dVJMU1pYeLk-9H9wJA5EKiNhWuTCshZu8dkr4GvHngnBAWug3fdrDsHGKrlU0n9LBPpvGAX4G8UOiol9IdptZu62Bemo0aOe9ASawPd76VGF2zIa2ei614sgfpvKUKhxm_rNa6tnnXm-VjkFOl66Yy7DK4KH_jl12SAstMfH20spSq2bLxSs0g6G0sIMs2QUIioEuRwa1dCgPssfocuJQ5yRyaes-N0vehYKZXuavM8Wp5lRozP9fxsNeWI8lDkaFv32PeukTF2S8MBA0esBqiUc4ECgGzVFhfsBMpS9cs-dK0BmTCRMAvo4bZRjDgC1MufBH3f3ltdNz8bW93t5eZKIWSVC_xJ3-k881MOOIDJpvhKKc0q14c0puS4rtpn20BqxRlHDjB3jxfQ4Vtc8PBqC6bkkNc8KOOjNqigDxCm72imYBitsnVlg_kiHeQsY1OLUASS6vlTzuSPpcx0Cmd9teF3fOE3bqzyu4_pI9nXizqKufDhtaWOp4PQ_JU9bBzbBqjZpfzn-OuvgEeklC3Sa0R2MmJAqLR-aTq9Yh38w1cH48UZ4aDFLMAhF9typqOdLHeVyhsMI1JeaZesf80J9KlR1fLmptnfM4Chb8OW5x_zHeNg9NHNt_9JesucO947EEcAhMElettlD92F0gV1r-FQhM'
  
  return {
    headers: { Authorization: `Bearer ${token}` }
  }
}

const getEvents = async (): Promise<IEventDropdownOption[]> => {
  const headers = await getHeaders()

  return axios.get('api/events', headers).then((response) => {
    const events = response.data as IEventsResponse[]
    const eventDropdownOptions: IEventDropdownOption[] = events.map(event => ({
      id: event.Id,
      name: event.Name
    }))

    return eventDropdownOptions
  })
}

export { getEvents }
