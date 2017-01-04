/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Threading.Tasks;
using RestSharp;

namespace EasyPost
{
    public class Item : EasyPostObject
    {
        /// <summary>
        /// Name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Harmonized code of the item
        /// </summary>
        public string HarmonizedCode { get; set; }

        /// <summary>
        /// Country of origin of the item
        /// </summary>
        public string CountryOfOrigin { get; set; }

        /// <summary>
        /// Warehouse location of the item
        /// </summary>
        public string WarehouseLocation { get; set; }

        /// <summary>
        /// Value of the item
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Length of the item
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Width of the item
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height of the item
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Weight of the item
        /// </summary>
        public double Weight { get; set; }
        // ADD CUSTOM REFERENCES (e.g. sku, upc)
    }

    /// <summary>
    /// Item API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Retrieve an Item from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Item. Starts with "item_" if passing an id.</param>
        /// <returns>Item instance.</returns>
        public async Task<Item> GetItem(
            string id)
        {
            var request = new EasyPostRequest("items/{id}");
            request.AddUrlSegment("id", id);

            return await Execute<Item>(request);
        }

        /// <summary>
        /// Create an Item.
        /// </summary>
        /// <param name="item">Item to create</param>
        /// <returns>EasyPost.Item instance.</returns>
        public async Task<Item> CreateItem(
            Item item)
        {
            var request = new EasyPostRequest("items", Method.POST);
            request.AddBody(item.AsDictionary(), "item");

            return await Execute<Item>(request);
        }

        /// <summary>
        /// Retrieve a Item from a custom reference.
        /// </summary>
        /// <param name="name">String containing the name of the custom reference to search for.</param>
        /// <param name="value">String containing the value of the custom reference to search for.</param>
        /// <returns>Item instance.</returns>
        public async Task<Item> GetItemByReference(
            string name,
            string value)
        {
            var request = new EasyPostRequest("items/retrieve_reference/?{name}={value}");
            request.AddUrlSegment("name", name);
            request.AddUrlSegment("value", value);

            return await Execute<Item>(request);
        }
    }
}