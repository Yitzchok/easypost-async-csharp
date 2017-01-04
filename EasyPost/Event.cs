/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost
{
    public class Event : EasyPostObject
    {
        /// <summary>
        /// Result type and event name, see the "Possible Event Types" section for more information
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Previous values of relevant result attributes
        /// </summary>
        public Dictionary<string, object> PreviousAttributes { get; set; }

        /// <summary>
        /// The object associated with the Event. See the "object" attribute on the result to determine its specific type
        /// </summary>
        public Dictionary<string, object> Result { get; set; }

        /// <summary>
        /// The current status of the event. Possible values are "completed", "failed", "in_queue", "retrying", or "pending" (deprecated)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Webhook URLs that have not yet been successfully notified as of the time this webhook event was sent. The URL 
        /// receiving the Event will still be listed in pending_urls, as will any other URLs that receive the Event at the same time
        /// </summary>
        public List<string> PendingUrls { get; set; }

        /// <summary>
        /// Webhook URLs that have already been successfully notified as of the time this webhook was sent
        /// </summary>
        public List<string> CompletedUrls { get; set; }
    }

    /// <summary>
    /// Event API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Retrieve a Event from its id.
        /// </summary>
        /// <param name="id">String representing a Event. Starts with "evt_".</param>
        /// <returns>EasyPost.Event instance.</returns>
        public async Task<Event> GetEvent(
            string id)
        {
            var request = new EasyPostRequest("events/{id}");
            request.AddUrlSegment("id", id);

            return await Execute<Event>(request);
        }
    }
}