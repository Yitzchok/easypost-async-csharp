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
    public class Verification : Resource
    {
        /// <summary>
        /// The success of the verification
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// All errors that caused the verification to fail
        /// </summary>
        public List<Error> Errors { get; set; }

        /// <summary>
        /// Extra data related to the verification
        /// </summary>
        public VerificationDetails Details { get; set; }
    }
}