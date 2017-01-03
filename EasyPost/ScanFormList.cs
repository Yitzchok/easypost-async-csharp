/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Collections.Generic;
using System.Linq;

namespace EasyPost
{
    public class ScanFormList : Resource
    {
        /// <summary>
        /// List of scan forms
        /// </summary>
        public List<ScanForm> ScanForms { get; set; }

        /// <summary>
        /// True if there is more in the list
        /// </summary>
        public bool HasMore { get; set; }

        /// <summary>
        /// Options used to generate the list
        /// </summary>
        public ScanFormListOptions Options { get; set; }

        /// <summary>
        /// Get the next page of scan forms based on the original parameters used to generate the list
        /// </summary>
        /// <param name="client">Easy post client to use</param>
        /// <returns>A new ScanFormList instance.</returns>
        public ScanFormList Next(
            IEasyPostClient client)
        {
            var options = Options ?? new ScanFormListOptions();
            options.BeforeId = ScanForms.Last().Id;
            return client.ListScanForms(options);
        }
    }
}