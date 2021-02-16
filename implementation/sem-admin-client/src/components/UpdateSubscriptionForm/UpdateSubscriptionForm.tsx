import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form'

import { IEventDropdownOption, IUpdateFormFields } from '../'
import { getEvents, updateSubscription } from '../../api'
import './UpdateSubscriptionForm.css';

const UpdateSubscriptionForm = () => {
  const { register, handleSubmit, formState, errors } = useForm<
    IUpdateFormFields
  >({
    mode: 'all',
  })

  const [enabledEventsList, setEnabledEventsList] = useState<string[]>([])
  const [eventDropdownOptions, setEventDropdownOptions] = useState<IEventDropdownOption[]>([])

  useEffect(() => {
    getEvents().then(result => setEventDropdownOptions(result)).catch(() => {
      setEventDropdownOptions([
        { id: '1', name: 'mp.entitlement.active' },
        { id: '2', name: 'mp.entitlement.refunded' },
        { id: '3', name: 'mp.entitlement.updated' },
        { id: '4', name: 'sis.enrollment' },
        { id: '5', name: 'sis.student' },
        { id: '6', name: 'sis.student-delivery' },
        { id: '7', name: 'sis.teacher' },
        { id: '8', name: 'sis.group' }
      ])
    })
  }, [])

  const addEvent = (selectedEvent: string) => {
    if (selectedEvent && !enabledEventsList.includes(selectedEvent))
      setEnabledEventsList([ ...enabledEventsList, selectedEvent ])
  }

  const removeEvent = (removedEvent: string) => {
    setEnabledEventsList(enabledEventsList.filter(enabledEvent => enabledEvent !== removedEvent))
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
        <label htmlFor="enabledEvents">Enabled Events:</label>
        <select
          id="enabledEvents"
          name="enabledEvents"
          onChange={(event) => addEvent(event.target.value)}
          ref={register({ required: false })}
          required
        >
          <option value="">Select Event</option>
          {eventDropdownOptions.map(option => 
            <option value={option.id}>{option.name}</option>
          )}
        </select>
        {(formState.dirtyFields.enabledEvents && !enabledEventsList.length) &&
          <p className="error-text">Enabled Events is required</p>
        }
        <ul>
          {enabledEventsList.map(enabledEvent => 
            <li key={enabledEvent}>{enabledEvent}<span className="remove-button" onClick={() => removeEvent(enabledEvent)}>x</span></li>
          )}
        </ul>
        <button className="submit-button" type="submit" disabled={!formState.isValid || !enabledEventsList.length}>Submit</button>
      </form>
    </>
  );
}

export { UpdateSubscriptionForm }
