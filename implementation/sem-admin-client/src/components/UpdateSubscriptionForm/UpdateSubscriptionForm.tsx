import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form'

import { IEventDropdownOption, IUpdateFormFields } from '../'
import { getEvents, updateSubscription } from '../../api'
import './UpdateSubscriptionForm.css';

const UpdateSubscriptionForm = () => {
  const { register, handleSubmit, formState, errors, setValue } = useForm<
    IUpdateFormFields
  >({
    mode: 'all',
  })

  const [enabledEventsList, setEnabledEventsList] = useState<IEventDropdownOption[]>([])
  const [eventDropdownOptions, setEventDropdownOptions] = useState<IEventDropdownOption[]>([])

  useEffect(() => {
    register({ name: 'enabledEvents', type: 'custom' }, { required: true })

    getEvents().then(result => setEventDropdownOptions(result))
  }, [register])

  const addEvent = (selectedEventId: string) => {
    const selectedEvent = eventDropdownOptions.find(event => event.id === selectedEventId)

    if (selectedEvent) {
      const updatedEnabledEvents = [ ...enabledEventsList, selectedEvent]

      setEnabledEventsList(updatedEnabledEvents)
      setValue('enabledEvents', updatedEnabledEvents, { shouldValidate: true })
    }
  }

  const removeEvent = (removedEventId: string) => {
    const removedEvent = eventDropdownOptions.find(event => event.id === removedEventId)

    if (removedEvent) {
      const updatedEnabledEvents = enabledEventsList.filter(enabledEvent => enabledEvent.id !== removedEvent.id)

      setEnabledEventsList(updatedEnabledEvents)
      setValue('enabledEvents', updatedEnabledEvents, { shouldValidate: true })
    }
  }

  return (
    <>
      <h2>Update Webhook</h2>
      <form className="form-container" onSubmit={handleSubmit(updateSubscription)}>
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
        <label htmlFor="url">URL:</label>
        <input type="text"
          id="url"
          name="url" 
          required
          placeholder="URL"
          ref={register({ required: true })}
        />
        {errors.url &&
          <p className="error-text">URL is required</p>
        }
        <label htmlFor="enabledEventsDropdown">Enabled Events:</label>
        <select
          id="enabledEventsDropdown"
          onChange={(event) => addEvent(event.target.value)}
        >
          <option value="">Select Event</option>
          {eventDropdownOptions.map(option => 
            <option key={option.id} value={option.id}>{option.name}</option>
          )}
        </select>
        {errors.enabledEvents &&
          <p className="error-text">Enabled Events is required</p>
        }
        <ul>
          {enabledEventsList.map(enabledEvent => 
            <li key={enabledEvent.id}>{enabledEvent.name}<span className="remove-button" onClick={() => removeEvent(enabledEvent.id)}>x</span></li>
          )}
        </ul>
        <button className="submit-button" type="submit" disabled={!formState.isValid || !enabledEventsList.length}>Submit</button>
      </form>
    </>
  );
}

export { UpdateSubscriptionForm }
