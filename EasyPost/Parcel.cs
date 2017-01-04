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
    public class Parcel : EasyPostObject
    {
        /// <summary>
        /// Required if predefined_package is empty
        /// </summary>
        public double? Length { get; set; }

        /// <summary>
        /// Required if predefined_package is empty
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// Required if predefined_package is empty
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Always required
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Optional, one of the pre defined packages defined here:
        /// https://www.easypost.com/docs/api#predefined-packages
        /// </summary>
        public string PredefinedPackage { get; set; }
    }

    /// <summary>
    /// Parcel API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Retrieve a Parcel from its id.
        /// </summary>
        /// <param name="id">String representing a Parcel. Starts with "prcl_".</param>
        /// <returns>Parcel instance.</returns>
        public async Task<Parcel> GetParcel(
            string id)
        {
            var request = new EasyPostRequest("parcels/{id}");
            request.AddUrlSegment("id", id);

            return await Execute<Parcel>(request);
        }

        /// <summary>
        /// Create a Parcel.
        /// </summary>
        /// <param name="parcel">Parcel to create</param>
        /// <returns>Parcel instance.</returns>
        public async Task<Parcel> CreateParcel(
            Parcel parcel)
        {
            var request = new EasyPostRequest("parcels", Method.POST);
            request.AddBody(parcel.AsDictionary(), "parcel");

            return await Execute<Parcel>(request);
        }
    }
}