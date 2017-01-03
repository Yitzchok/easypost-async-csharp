/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System;

namespace EasyPost
{
    public class TrackerListOptions : Resource
    {
        /// <summary>
        /// Optional tracking number string. Only retrieve trackers with the given tracking code.
        /// </summary>
        public string TrackingCode { get; set; }

        /// <summary>
        /// Optional tracker's carrier. Only retrieve trackers with the given carrier.
        /// </summary>
        public string Carrier { get; set; }

        /// <summary>
        /// Optional pagination parameter. Only trackers created before the given ID will be included. May not be used with AfterId.
        /// </summary>
        public string BeforeId { get; set; }

        /// <summary>
        /// Optional pagination parameter. Only trackers created after the given ID will be included. May not be used with BeforeId.
        /// </summary>
        public string AfterId { get; set; }

        /// <summary>
        /// Only return trackers created after this timestamp. Defaults to 1 month ago, or 1 month before a passed EndDatetime.
        /// </summary>
        public DateTime? StartDatetime { get; set; }

        /// <summary>
        /// Only return trackers created before this timestamp. Defaults to end of the current day, or 1 month after a passed StartDatetime.
        /// </summary>
        public DateTime? EndDatetime { get; set; }

        /// <summary>
        /// The number of trackers to return on each page. The maximum value is 100
        /// </summary>
        public int? PageSize { get; set; }
    }
}