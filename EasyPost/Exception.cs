/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System;
using System.Net;

namespace EasyPost
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode;
        public string Code;

        public HttpException(
            HttpStatusCode statusCode,
            string code,
            string message)
            : base(message)
        {
            StatusCode = statusCode;
            Code = code;
        }
    }

    public class ResourceAlreadyCreated : Exception
    {
    }
}