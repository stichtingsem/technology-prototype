export interface ISubscriptionsPayload {
  webhookId?: string
  postbackUrl: string
  eventIds: string[]
  secret: string
}