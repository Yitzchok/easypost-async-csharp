/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyPost
{
    public class TrackerList
    {
        /// <summary>
        /// List of trackers
        /// </summary>
        public List<Tracker> Trackers { get; set; }

        /// <summary>
        /// True if there is more in the list
        /// </summary>
        public bool HasMore { get; set; }

        /// <summary>
        /// Options used to generate the list
        /// </summary>
        public TrackerListOptions Options { get; set; }

        /// <summary>
        /// Get the next page of shipments based on the original parameters used to generate the list
        /// </summary>
        /// <param name="client">Easy post client to use</param>
        /// <returns>A new ShipmentList instance.</returns>
        public async Task<TrackerList> Next(
            IEasyPostClient client)
        {
            var options = Options ?? new TrackerListOptions();
            options.BeforeId = Trackers.Last().Id;
            return await client.ListTrackers(options);
        }
    }
}