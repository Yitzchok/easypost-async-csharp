/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

namespace EasyPost
{
    public class Error : Resource
    {
        /// <summary>
        /// Error code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Field of the request that the error describes
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Human readable description of the problem
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Optional suggestion
        /// </summary>
        public string Suggestion { get; set; }
    }
}