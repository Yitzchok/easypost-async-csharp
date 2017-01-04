/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Collections.Generic;
using System.Net;

namespace EasyPost
{
    /// <summary>
    /// Class to represent a request error. 
    /// </summary>
    public class RequestError
    {
        /// <summary>
        /// Http status code for the request
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Human readable description of the problem
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Optional list of detailed error information
        /// </summary>
        public List<Error> Errors { get; set; }

        /// <summary>
        /// Raw response content
        /// </summary>
        public string Content { get; set; }
    }
}