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
    public class ShipmentList : Resource
    {
        /// <summary>
        /// List of shipments in this list
        /// </summary>
        public List<Shipment> Shipments { get; set; }

        /// <summary>
        /// True if there are more shipments to retrieve
        /// </summary>
        public bool HasMore { get; set; }

        /// <summary>
        /// Options used to generate the list
        /// </summary>
        public ShipmentListOptions Options { get; set; }

        /// <summary>
        /// Get the next page of shipments based on the original parameters used to generate the list
        /// </summary>
        /// <param name="client">Easy post client to use</param>
        /// <returns>A new ShipmentList instance.</returns>
        public async Task<ShipmentList> Next(
            IEasyPostClient client)
        {
            var options = Options ?? new ShipmentListOptions();
            options.BeforeId = Shipments.Last().Id;
            return await client.ListShipments(options);
        }
    }
}