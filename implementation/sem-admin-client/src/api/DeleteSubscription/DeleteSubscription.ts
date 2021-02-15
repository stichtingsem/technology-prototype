import axios from 'axios'
import { IDeleteFormFields } from '../../components'

const deleteSubscription = (formData: IDeleteFormFields) => {
  axios.delete(`https://localhost:5001/subscriptions/${formData.id}`)
}

export { deleteSubscription }
