/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Collections.Generic;

namespace EasyPost
{
    public class CarrierField : Resource
    {
        /// <summary>
        /// If this key is present with the value of true, no other attributes are needed for CarrierAccount creation
        /// </summary>
        public bool? AutoLink { get; set; }

        /// <summary>
        /// If this key is present with the value of true, CarrierAccount creation of this type requires extra work not handled by 
        /// the CarrierAccount standard API
        /// </summary>
        public bool? CustomWorkflow { get; set; }

        /// <summary>
        /// If this object is present, required attribute names and their metadata are presented inside
        /// </summary>
        public Dictionary<string, Credentials> Credentials { get; set; }

        /// <summary>
        /// If this object is present, it contains attribute names and metadata just as the credentials object. It is not required for 
        /// CarrierAccount creation if you do not plan on using the carrier account for test mode
        /// </summary>
        public Dictionary<string, Credentials> TestCredentials { get; set; }
    }
}