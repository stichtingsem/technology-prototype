import { IEventsResponse } from './IEventsResponse'

export interface ISubscriptionsResponse {
  id: string
  postbackUrl: string
  events: IEventsResponse[]
  secret: string
}