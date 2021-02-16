import React, { useState } from 'react';
import { useForm } from 'react-hook-form'

import { IUpdateFormFields } from './IUpdateFormFields'
import { updateSubscription } from '../../api'
import './UpdateSubscriptionForm.css';

const UpdateSubscriptionForm = () => {
  const { register, handleSubmit, formState, errors } = useForm<
  IUpdateFormFields
  >({
    mode: 'all',
  })

  const [enabledEventsList, setEnabledEventsList] = useState<string[]>([])

  const addEvent = (selectedEvent: string) => {
    if (selectedEvent && !enabledEventsList.includes(selectedEvent))
      setEnabledEventsList([ ...enabledEventsList, selectedEvent ])
  }

  const removeEvent = (removedEvent: string) => {
    setEnabledEventsList(enabledEventsList.filter(enabledEvent => enabledEvent !== removedEvent))
  }

  return (
    <>
      <h2>Update</h2>
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
          <option value="mp.entitlement.active" title="First created and active">mp.entitlement.active</option>
          <option value="mp.entitlement.refunded" title="Refunded">mp.entitlement.refunded</option>
          <option value="mp.entitlement.updated" title="Modified in any other way">mp.entitlement.updated</option>
          <option value="sis.enrollment" title="A student has been enrolled, or their enrolment changed">sis.enrollment</option>
          <option value="sis.student" title="A student has been created or modified">sis.student</option>
          <option value="sis.student-delivery" title="A students delivery information has been created or modified">sis.student-delivery</option>
          <option value="sis.teacher" title="A teacher has been created or modified">sis.teacher</option>
          <option value="sis.group" title="A group has been modified (this is an aggregate event) - this means that basic metadata changed, a student added/removed, a teacher added or removed or a student subject choice modified within a group.">sis.group</option>
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
