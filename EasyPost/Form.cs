/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

namespace EasyPost
{
    public class Form : EasyPostObject
    {
        /// <summary>
        /// Url of the form
        /// </summary>
        public string FormUrl { get; set; }

        /// <summary>
        /// Type of the form
        /// </summary>
        public string FormType { get; set; }

        /// <summary>
        /// True if submitted electronically
        /// </summary>
        public bool SubmittedElectronically { get; set; }
    }
}