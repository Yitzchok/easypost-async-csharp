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
using RestSharp;

namespace EasyPost
{
    public class Pickup : EasyPostObject
    {
        /// <summary>
        /// One of: "unknown", "scheduled", or "canceled"
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Name of the pickup
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The earliest time at which the package is available to pick up
        /// </summary>
        public DateTime MinDatetime { get; set; }

        /// <summary>
        /// The latest time at which the package is available to pick up. Must be later than the min_datetime
        /// </summary>
        public DateTime MaxDatetime { get; set; }

        /// <summary>
        /// Is the pickup address the account's address?
        /// </summary>
        public bool IsAccountAddress { get; set; }

        /// <summary>
        /// Additional text to help the driver successfully obtain the package
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        /// A list of messages containing carrier errors encountered during pickup rate generation
        /// </summary>
        public List<string> Messages { get; set; }

        /// <summary>
        /// The confirmation number for a booked pickup from the carrier
        /// </summary>
        public string Confirmation { get; set; }

        /// <summary>
        /// The associated Shipment
        /// </summary>
        public Shipment Shipment { get; set; }

        /// <summary>
        /// The associated Address
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// The list of carriers (if empty, all carriers were used) used to generate pickup rates
        /// </summary>
        public List<CarrierAccount> CarrierAccounts { get; set; }

        /// <summary>
        /// The list of different pickup rates across valid carrier accounts for the shipment
        /// </summary>
        public List<CarrierRate> PickupRates { get; set; }
    }

    /// <summary>
    /// Pickup API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>Pickup instance.</returns>
        public async Task<Pickup> GetPickup(
            string id)
        {
            var request = new EasyPostRequest("pickups/{id}");
            request.AddUrlSegment("id", id);

            return await Execute<Pickup>(request);
        }

        /// <summary>
        /// Create a Pickup.
        /// </summary>
        /// <param name="pickup">Pickup to create</param>
        /// <returns>Pickup instance.</returns>
        public async Task<Pickup> CreatePickup(
            Pickup pickup = null)
        {
            var request = new EasyPostRequest("pickups", Method.POST);
            if (pickup != null) {
                request.AddBody(pickup.AsDictionary(), "pickup");
            }

            return await Execute<Pickup>(request);
        }

        /// <summary>
        /// Purchase this pickup.
        /// </summary>
        /// <param name="id">Pickup id to purchase</param>
        /// <param name="carrier">The name of the carrier to purchase with.</param>
        /// <param name="service">The name of the service to purchase.</param>
        /// <returns>Pickup instance.</returns>
        public async Task<Pickup> BuyPickup(
            string id,
            string carrier,
            string service)
        {
            var request = new EasyPostRequest("pickups/{id}/buy", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddBody(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("carrier", carrier),
                new KeyValuePair<string, string>("service", service)
            });

            return await Execute<Pickup>(request);
        }

        /// <summary>
        /// Cancel this pickup.
        /// </summary>
        /// <param name="id">Pickup id to cancel</param>
        /// <returns>Pickup instance.</returns>
        public async Task<Pickup> CancelPickp(
            string id)
        {
            var request = new EasyPostRequest("pickups/{id}/cancel", Method.POST);
            request.AddUrlSegment("id", id);

            return await Execute<Pickup>(request);
        }
    }
}