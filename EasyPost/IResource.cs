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
    public interface IResource
    {
        /// <summary>
        /// Returns the resource properties as a dictionary
        /// </summary>
        /// <returns>Dictionary for the resource properties</returns>
        Dictionary<string, object> AsDictionary();
    }
}