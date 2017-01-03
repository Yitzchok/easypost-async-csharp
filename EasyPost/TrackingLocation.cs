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
    public class TrackingLocation
    {
        /// <summary>
        /// Created at
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Last updated at
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// The city where the scan event occurred (if available)
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The state where the scan event occurred (if available)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The country where the scan event occurred (if available)
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The postal code where the scan event occurred (if available)
        /// </summary>
        public string Zip { get; set; }
    }
}