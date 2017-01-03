/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

namespace EasyPost
{
    public class Credentials : Resource
    {
        /// <summary>
        /// There are five possible values, which encode the security need and storage type for each attribute: "visible", "checkbox", "fake", "password", and "masked"
        /// </summary>
        public string Visibility { get; set; }

        /// <summary>
        /// Most attributes have generic names, so for clarity a "label" value is provided for clearer representation when rendering forms
        /// </summary>
        public string Label { get; set; }
    }
}