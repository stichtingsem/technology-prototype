export interface ISubscriptionsPayload {
  WebhookId?: string
  PostbackUrl: string
  EventIds: string[]
  Secret: string
}