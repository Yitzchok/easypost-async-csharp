/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

namespace EasyPost
{
    public class EasyPostMessage : Resource
    {
        /// <summary>
        /// The name of the carrier generating the error, e.g. "UPS"
        /// </summary>
        public string Carrier { get; set; }

        /// <summary>
        /// The category of error that occurred. Most frequently "rate_error"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The category of error that occurred. Most frequently "rate_error"
        /// </summary>
        public string Message { get; set; }
    }
}