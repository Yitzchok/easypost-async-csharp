/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Collections.Generic;
using RestSharp;

namespace EasyPost
{
    public class CustomsInfo : EasyPostObject
    {
        /// <summary>
        /// "EEL" or "PFC" value less than $2500: "NOEEI 30.37(a)"; value greater than $2500: see Customs Guide
        /// </summary>
        public string EelPfc { get; set; }

        /// <summary>
        /// "documents", "gift", "merchandise", "returned_goods", "sample", or "other"
        /// </summary>
        public string ContentsType { get; set; }

        /// <summary>
        /// Human readable description of content. Required for certain carriers and always required if contents_type is "other"
        /// </summary>
        public string ContentsExplanation { get; set; }

        /// <summary>
        /// Electronically certify the information provided
        /// </summary>
        public bool? CustomsCertify { get; set; }

        /// <summary>
        /// Required if customs_certify is true
        /// </summary>
        public string CustomsSigner { get; set; }

        /// <summary>
        /// "abandon" or "return", defaults to "return"
        /// </summary>
        public string NonDeliveryOption { get; set; }

        /// <summary>
        /// "none", "other", "quarantine", or "sanitary_phytosanitary_inspection"
        /// </summary>
        public string RestrictionType { get; set; }

        /// <summary>
        /// Required if restriction_type is not "none"
        /// </summary>
        public string RestrictionComments { get; set; }

        /// <summary>
        /// Describes to products being shipped
        /// </summary>
        public List<CustomsItem> CustomsItems { get; set; }
    }

    /// <summary>
    /// CustomersInfo API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Retrieve a CustomsInfo from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsInfo. Starts with "cstinfo_".</param>
        /// <returns>CustomsInfo instance.</returns>
        public CustomsInfo GetCustomsInfo(
            string id)
        {
            var request = new EasyPostRequest("customs_infos/{id}");
            request.AddUrlSegment("id", id);

            return Execute<CustomsInfo>(request);
        }

        /// <summary>
        /// Create a CustomsInfo.
        /// </summary>
        /// <param name="customsInfo">Customs info to create</param>
        /// <returns>EasyPost.CustomsInfo instance.</returns>
        public CustomsInfo CreateCustomsInfo(
            CustomsInfo customsInfo)
        {
            if (customsInfo.Id != null) {
                throw new ResourceAlreadyCreated();
            }

            var request = new EasyPostRequest("customs_infos", Method.POST);
            request.AddBody(customsInfo.AsDictionary(), "customs_info");

            return Execute<CustomsInfo>(request);
        }
    }
}