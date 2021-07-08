/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost
{
    public class Webhook : EasyPostObject
    {
        /// <summary>
        /// The url for the webhook callbacks
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The disabled date if disabled
        /// </summary>
        public DateTime? DisabledAt { get; set; }
    }

    /// <summary>
    /// Tracker API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Retrieve a Webhook from its id.
        /// </summary>
        /// <param name="id">String representing a Webhook. Starts with "hook_".</param>
        /// <returns>Webhook instance.</returns>
        public async Task<Webhook> GetWebhook(
            string id)
        {
            var request = new EasyPostRequest("webhooks/{id}");
            request.AddUrlSegment("id", id);

            return await Execute<Webhook>(request);
        }

        /// <summary>
        /// Get an unpaginated list of Webhooks.
        /// </summary>
        /// <returns>Instance of WebhookList</returns>
        public async Task<WebhookList> ListWebhooks()
        {
            var request = new EasyPostRequest("webhooks");

            var webhookList = await Execute<WebhookList>(request);
            return webhookList;
        }

        /// <summary>
        /// Creates a new Webhook
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Webhook instance.</returns>
        public async Task<Webhook> CreateWebhook(string url)
        {
            var request = new EasyPostRequest("webhooks", RestSharp.Method.POST);
            var parameters = new Dictionary<string, object>() {
                { "url", url },
            };
            request.AddBody(parameters, "webhook");

            return await Execute<Webhook>(request);
        }

        /// <summary>
        /// Update a Webhook.
        /// Enables a Webhook that has been disabled.
        /// </summary>
        /// <param name="id">String representing a Webhook. Starts with "hook_".</param>
        /// <returns>Webhook instance.</returns>
        public async Task<Webhook> UpdateWebhook(
            string id)
        {
            var request = new EasyPostRequest("webhooks/{id}", RestSharp.Method.PUT);
            request.AddUrlSegment("id", id);

            return await Execute<Webhook>(request);
        }

        /// <summary>
        /// Delete a Webhook.
        /// </summary>
        /// <param name="id">String representing a Webhook. Starts with "hook_".</param>
        /// <returns>Webhook instance.</returns>
        public async Task<Webhook> DeleteWebhook(
            string id)
        {
            var request = new EasyPostRequest("webhooks/{id}", RestSharp.Method.DELETE);
            request.AddUrlSegment("id", id);

            return await Execute<Webhook>(request);
        }
    }
}