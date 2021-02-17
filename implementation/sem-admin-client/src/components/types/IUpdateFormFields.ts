import { IEventDropdownOption } from './IEventDropdownOption'

export interface IUpdateFormFields {
  id: string
  url: string
  enabledEvents: IEventDropdownOption[]
}
