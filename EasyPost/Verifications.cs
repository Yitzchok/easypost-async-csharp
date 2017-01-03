/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

namespace EasyPost
{
    public class Verifications : Resource
    {
        /// <summary>
        /// Only applicable to US addresses - checks and sets the ZIP+4
        /// </summary>
        public Verification Zip4 { get; set; }

        /// <summary>
        /// Checks that the address is deliverable and makes minor corrections to spelling/format. US addresses will also have their "residential" status checked and set.
        /// </summary>
        public Verification Delivery { get; set; }
    }
}