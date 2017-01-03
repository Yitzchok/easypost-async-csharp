/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

namespace EasyPost
{
    public class VerificationDetails : Resource
    {
        /// <summary>
        /// The latitude of the address
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude of the address
        /// </summary>
        public double Longitude { get; set; }
    }
}