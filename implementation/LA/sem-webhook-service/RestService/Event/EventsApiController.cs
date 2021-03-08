﻿/*
 * Generic Event Stream API
 *
 * The proposed architecture for the new ecosystem is based on an Event model, where each provider of data to the ecosystem provides a mechanism for other participants to securely subscribe to events that are of interest to them.  The Event API is the mechanism, in conjunction with the Webhook API, that enables this event based notification mechanism to function reliably.  It enables subscribers to request events from the provider, and paginate through to the last place that it received a message.  Notes:   - Pagination is essential - using start and limit semantics.   - Limits should be imposed on page size.   - Order should always be reverese chronological.   - Filtering is available by matching specific event type.   - Providers should ensure that this API is cacheable or able to be retrieved at minimal cost, as it may be accessed a lot by consumers who are 'catching up'      > Note:  We may need to provide an API that creates a 'dummy' event list of all entities to enable an initial synchronisation of all data (e.g. a brand new LA that wants to do an initial synchronisation with data from the SIS) - this may be simpler than providing a GET requests for entities in the SIS itseelf (e.g. get students).
 *
 * OpenAPI spec version: 1.0.0
 * Contact: clifton@infinitaslearning.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using Abp.Dependency;
using Abp.Webhooks;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestService.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class EventsApiController : ControllerBase
    {
        private IWebhookSubscriptionsStore WebhookSubscriptionsStore;
        private IWebhookSendAttemptStore WebhookSendAttemptStore;
        private IWebhookEventStore WebhookEventStore;
        public EventsApiController()
        {
            this.WebhookSubscriptionsStore = IocManager.Instance.Resolve<IWebhookSubscriptionsStore>();
            this.WebhookSendAttemptStore = IocManager.Instance.Resolve<IWebhookSendAttemptStore>();
            this.WebhookEventStore = IocManager.Instance.Resolve<IWebhookEventStore>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Allows a subscriber to retrieve a list of past events.</remarks>
        /// <param name="type">Filter by a specific type of event, e.g. la.usage - specific to the service implementing the API.</param>
        /// <param name="start">Start point for pagination of results, defaults to 0,</param>
        /// <param name="limit">Limit of number of results returned by page, defaults to 20 with max 100.</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/events")]
        [Route("/events")]
        [ValidateModelState]
        [SwaggerOperation("ListEvents")]
        [SwaggerResponse(statusCode: 200, type: typeof(Events), description: "OK")]
        public virtual IActionResult ListEvents([FromQuery] string type, [FromQuery] int? start, [FromQuery] int? limit)
        {
            if (start == null)
                start = 0;
            if (limit == null)
                limit = 20;
            if (limit > 100)
                limit = 100;

            // TODO : use info from JWT token to get the tenantID
            int? tenantId = null;

            var subscriptions = this.WebhookSubscriptionsStore.GetAllSubscriptions(tenantId, type);

            if ((subscriptions == null) || (subscriptions.Count == 0))
                return new ObjectResult(null);

            var sendAttempts = new List<WebhookSendAttempt>();

            // TODO : For now use only the first subscription!
            // Is it even possible to have multiple subscriptions per type?
            // If so, wont they generate the same events?
            //if (subscriptions.Count==1)
            var sendAttempsForSubscription = this.WebhookSendAttemptStore.GetAllSendAttemptsBySubscriptionAsPagedList(tenantId, subscriptions[0].Id, limit.Value, start.Value);
            sendAttempts.AddRange(sendAttempsForSubscription.Items);

            var events = new Events();

            sendAttempts.ForEach(a =>
            {
                a.WebhookEvent = this.WebhookEventStore.Get(tenantId, a.WebhookEventId);

                events.Add(new ModelEvent()
                {
                    Created = ToDecimal(a.CreationTime),
                    Data = GetDataFromWebhookData(a.WebhookEvent.Data),
                    Id = a.Id,
                    Type = a.WebhookEvent.WebhookName,
                    Url = GetUrlFromWebhookData(a.WebhookEvent.Data)
                });
            });
            return new ObjectResult(events);

            ////TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            //// return StatusCode(200, default(Events));
            //string exampleJson = null;
            //exampleJson = "[ {\n  \"data\" : { },\n  \"created\" : 1530291411,\n  \"id\" : \"d290f1ee-6c54-4b01-90e6-d701748f0851\",\n  \"type\" : \"la.usage\",\n  \"url\" : \"url\"\n}, {\n  \"data\" : { },\n  \"created\" : 1530291411,\n  \"id\" : \"d290f1ee-6c54-4b01-90e6-d701748f0851\",\n  \"type\" : \"la.usage\",\n  \"url\" : \"url\"\n} ]";

            //var example = exampleJson != null
            //? JsonConvert.DeserializeObject<Events>(exampleJson)
            //: default(Events);            //TODO: Change the data returned
            //return new ObjectResult(example);
        }

        private string GetUrlFromWebhookData(string data)
        {
            // TODO: Put data and URL together in data field of event --> unpack
            return "http://nu.nl";
        }

        private object GetDataFromWebhookData(string data)
        {
            // TODO: Put data and URL together in data field of event --> unpack
            return data;
        }

        private decimal? ToDecimal(DateTime creationTime)
        {
            // TODO : what is used for decimal datetime? Ticks since ...?
            return 123;
        }
    }
}