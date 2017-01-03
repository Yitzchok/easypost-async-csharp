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
    /// <summary>
    /// Base class for all EasyPost objects
    /// </summary>
    public class EasyPostObject : Resource
    {
        /// <summary>
        /// Unique identifier for the resource. Every EasyPost Object that can be 
        /// created through the API has an id field that is used to refer to the object 
        /// in other API calls. An id consists of a prefix based on the object type 
        /// (e.g. Shipments have the prefix "shp_") followed by 32 hex characters. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Object type name. Every first-class EasyPost object includes an object value. 
       /// </summary>
        public string Object { get; set; }

        /// <summary>
        /// Date the object was created
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Date the object was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Set based on which api-key you used, either "test" or "production".
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// Many API objects also have an optional reference field that may be assigned during creation. The reference values 
        /// you assign may be used in place of id values in many API endpoints, but we recommend using the EasyPost-assigned id 
        /// instead because ids are guaranteed to be unique within the system, while reference uniqueness is not enforced. 
        /// </summary>
        public string Reference { get; set; }
    }
}
