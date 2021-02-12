import React from 'react';
import { useForm } from 'react-hook-form'

import { IDeleteFormFields } from './IDeleteFormFields'
import './DeleteSubscriptionForm.css';

const DeleteSubscriptionForm = () => {
  const { register, handleSubmit, formState, errors } = useForm<
  IDeleteFormFields
  >({
    mode: 'all',
  })

  const submitForm = () => {
    console.log('Deleting subscription...')
  }

  return (
    <>
      <h2>Delete</h2>
      <form className="form-container" onSubmit={handleSubmit(submitForm)}>
        <label htmlFor="id">Id:</label>
        <input type="text"
            id="id"
            name="id" 
            required
            placeholder="Id"
            ref={register({ required: true })}
        />
        {errors.id &&
          <p className="error-text">Id is required</p>
        }
        <button className="submit-button" type="submit" disabled={!formState.isValid}>Submit</button>
      </form>
    </>
  );
}

export { DeleteSubscriptionForm }
