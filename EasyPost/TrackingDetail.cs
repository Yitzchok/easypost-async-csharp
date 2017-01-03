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
    public class TrackingDetail : Resource
    {
        /// <summary>
        /// Description of the scan event
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Status of the package at the time of the scan event, possible values are:
        /// "unknown", "pre_transit", "in_transit", "out_for_delivery", "delivered", 
        /// "available_for_pickup", "return_to_sender", "failure", "cancelled" or "error"
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The timestamp when the tracking scan occurred
        /// </summary>
        public DateTime? Datetime { get; set; }

        /// <summary>
        /// The original source of the information for this scan event, usually the carrier
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The location associated with the scan event
        /// </summary>
        public TrackingLocation TrackingLocation { get; set; }
    }
}