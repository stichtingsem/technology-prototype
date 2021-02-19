import { IEventsResponse } from './IEventsResponse'

export interface ISubscriptionsResponse {
  Id: string
  PostbackUrl: string
  Events: IEventsResponse[]
  Secret: string
}