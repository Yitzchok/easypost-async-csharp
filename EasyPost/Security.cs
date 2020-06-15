/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Net;

namespace EasyPost
{
    internal class Security
    {
        public static SecurityProtocolType GetProtocol()
        {
#if NET45
            return SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
#else
            return (SecurityProtocolType)0x00000C00;
#endif
        }
    }
}