/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using RestSharp;

namespace EasyPost
{
    public class CustomsItem : EasyPostObject
    {
        /// <summary>
        /// Required, description of item being shipped
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Required, greater than zero
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Required, greater than zero, total value (unit value * quantity)
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Required, greater than zero, total weight (unit weight * quantity)
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Harmonized Tariff Schedule, e.g. "6109.10.0012" for Men's T-shirts
        /// </summary>
        public string HsTariffNumber { get; set; }

        /// <summary>
        /// SKU/UPC or other product identifier
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Required, 2 char country code
        /// </summary>
        public string OriginCountry { get; set; }

        /// <summary>
        /// 3 char currency code, default USD
        /// </summary>
        public string Currency { get; set; }
    }

    /// <summary>
    /// CustomsItem API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Retrieve a CustomsItem from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsItem. Starts with "cstitem_".</param>
        /// <returns>CustomsItem instance.</returns>
        public CustomsItem GetCustomsItem(
            string id)
        {
            var request = new EasyPostRequest("customs_items/{id}");
            request.AddUrlSegment("id", id);

            return Execute<CustomsItem>(request);
        }

        /// <summary>
        /// Create a CustomsItem.
        /// </summary>
        /// <param name="customsItem">Customs item parameters</param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        public CustomsItem CreateCustomsItem(
            CustomsItem customsItem)
        {
            if (customsItem.Id != null) {
                throw new ResourceAlreadyCreated();
            }

            var request = new EasyPostRequest("customs_items", Method.POST);
            request.AddBody(customsItem.AsDictionary(), "customs_item");

            return Execute<CustomsItem>(request);
        }
    }
}