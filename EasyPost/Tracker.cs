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
    public class Tracker : EasyPostObject
    {
        /// <summary>
        /// The tracking code provided by the carrier
        /// </summary>
        public string TrackingCode { get; set; }

        /// <summary>
        /// The current status of the package, possible values are "unknown", "pre_transit", "in_transit", "out_for_delivery", "delivered", "available_for_pickup", "return_to_sender", "failure", "cancelled" or "error"
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The name of the person who signed for the package (if available)
        /// </summary>
        public string SignedBy { get; set; }

        /// <summary>
        /// The weight of the package as measured by the carrier in ounces (if available)
        /// </summary>
        public double? Weight { get; set; }

        /// <summary>
        /// The estimated delivery date provided by the carrier (if available)
        /// </summary>
        public DateTime? EstDeliveryDate { get; set; }

        /// <summary>
        /// The id of the EasyPost Shipment object associated with the Tracker (if any)
        /// </summary>
        public string ShipmentId { get; set; }

        /// <summary>
        /// The name of the carrier handling the shipment
        /// </summary>
        public string Carrier { get; set; }

        /// <summary>
        /// Array of the associated TrackingDetail objects
        /// </summary>
        public List<TrackingDetail> TrackingDetails { get; set; }

        /// <summary>
        /// The associated CarrierDetail object (if available)
        /// </summary>
        public CarrierDetail CarrierDetail { get; set; }

        /// <summary>
        /// URL to a publicly-accessible html page that shows tracking details for this tracker
        /// </summary>
        public string PublicUrl { get; set; }

        /// <summary>
        /// Array of the associated Fee objects
        /// </summary>
        public List<Fee> Fees { get; set; }

        /// <summary>
        /// Time when tracker was updated
        /// </summary>
        public DateTime TrackingUpdatedAt { get; set; }
    }

    /// <summary>
    /// Tracker API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Creates a tracker for a carrier and tracking code
        /// </summary>
        /// <param name="carrier">Carrier</param>
        /// <param name="trackingCode">Tracking code</param>
        /// <returns>Tracker instance.</returns>
        public async Task<Tracker> CreateTracker(
            string carrier,
            string trackingCode)
        {
            var request = new EasyPostRequest("trackers", RestSharp.Method.POST);
            var parameters = new Dictionary<string, object>() {
                { "tracking_code", trackingCode },
                { "carrier", carrier }
            };
            request.AddBody(parameters, "tracker");

            return await Execute<Tracker>(request);
        }

        /// <summary>
        /// Retrieve a Tracker from its id.
        /// </summary>
        /// <param name="id">String representing a Tracker. Starts with "trk_".</param>
        /// <returns>Tracker instance.</returns>
        public async Task<Tracker> GetTracker(
            string id)
        {
            var request = new EasyPostRequest("trackers/{id}");
            request.AddUrlSegment("id", id);

            return await Execute<Tracker>(request);
        }

        /// <summary>
        /// Get a paginated list of trackers.
        /// </summary>
        /// <param name="options">Options for the pagination function</param>
        /// <returns>Instance of EasyPost.ShipmentList</returns>
        public async Task<TrackerList> ListTrackers(
            TrackerListOptions options = null)
        {
            var request = new EasyPostRequest("trackers");
            if (options != null) {
                request.AddQueryString(options.AsDictionary());
            }

            var trackerList = await Execute<TrackerList>(request);
            trackerList.Options = options;
            return trackerList;
        }
    }
}