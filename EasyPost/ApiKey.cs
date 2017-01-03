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
    public class ApiKey
    {
        /// <summary>
        /// The actual key value to use for authentication
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Set based on which api-key you used, either "test" or "production".
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// Date the key was created
        /// </summary>
        public DateTime? CreatedAt { get; set; }
    }
}
