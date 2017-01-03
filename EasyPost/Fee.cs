/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

namespace EasyPost
{
    public class Fee : Resource
    {
        /// <summary>
        /// The name of the category of fee. Possible types are "LabelFee", "PostageFee", "InsuranceFee", and "TrackerFee"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// USD value with sub-cent precision
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Whether EasyPost has successfully charged your account for the fee
        /// </summary>
        public bool Charged { get; set; }

        /// <summary>
        /// Whether the Fee has been refunded successfully
        /// </summary>
        public bool Refunded { get; set; }
    }
}