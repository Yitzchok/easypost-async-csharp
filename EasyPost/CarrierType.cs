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
    public class CarrierType : Resource
    {
        /// <summary>
        /// Specifies the CarrierAccount type. Note that "EndiciaAccount" is the current USPS integration account type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Readable name for the carrier type
        /// </summary>
        public string Readable { get; set; }

        /// <summary>
        /// Logo for the carrier type
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Contains at least one of the following keys: "auto_link", "credentials", "test_credentials", and "custom_workflow"
        /// </summary>
        public CarrierField Fields { get; set; }
    }

    /// <summary>
    /// CarrierAccount API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Get a list of all carrier types
        /// </summary>
        /// <returns>List of carrier types</returns>
        public async Task<List<CarrierType>> ListCarrierTypes()
        {
            var request = new EasyPostRequest("carrier_types");
            return await Execute<List<CarrierType>>(request);
        }
    }
}