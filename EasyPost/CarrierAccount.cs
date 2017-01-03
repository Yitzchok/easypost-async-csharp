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
    public class CarrierAccount : EasyPostObject
    {
        /// <summary>
        /// The name of the carrier type. Note that "EndiciaAccount" is the current USPS integration account type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// An optional, user-readable field to help distinguish accounts
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The name used when displaying a readable value for the type of the account
        /// </summary>
        public string Readable { get; set; }

        /// <summary>
        /// Unlike the "credentials" object contained in "fields", this nullable object contains just raw credential pairs for client library consumption
        /// </summary>
        public Dictionary<string, object> Credentials { get; set; }

        /// <summary>
        /// Unlike the "test_credentials" object contained in "fields", this nullable object contains just raw test_credential pairs for client library consumption
        /// </summary>
        public Dictionary<string, object> TestCredentials { get; set; }
    }

    /// <summary>
    /// CarrierAccount API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Get a list of carrier accounts
        /// </summary>
        /// <returns>List of carrier accounts</returns>
        public List<CarrierAccount> ListCarrierAccounts()
        {
            var request = new EasyPostRequest("carrier_accounts");
            return Execute<List<CarrierAccount>>(request);
        }

        /// <summary>
        /// Retrieve a CarrierAccount from its id.
        /// </summary>
        /// <param name="id">String representing a carrier account. Starts with "ca_".</param>
        /// <returns>CarrierAccount instance.</returns>
        public CarrierAccount GetCarrierAccount(
            string id)
        {
            var request = new EasyPostRequest("carrier_accounts/{id}");
            request.AddUrlSegment("id", id);

            return Execute<CarrierAccount>(request);
        }

        /// <summary>
        /// Create a CarrierAccount.
        /// </summary>
        /// <param name="carrierAccount">Carriern account details to create</param>
        /// <returns>CarrierAccount instance.</returns>
        public CarrierAccount CreateCarrierAccount(
            CarrierAccount carrierAccount)
        {
            if (carrierAccount.Id != null) {
                throw new ResourceAlreadyCreated();
            }

            var request = new EasyPostRequest("carrier_accounts", Method.POST);
            request.AddBody(carrierAccount.AsDictionary(), "carrier_account");

            return Execute<CarrierAccount>(request);
        }

        /// <summary>
        /// Update this CarrierAccount.
        /// </summary>
        /// <param name="carrierAccount">Carrier account details</param>
        /// <returns>CarrierAccount instance.</returns>
        public CarrierAccount UpdateCarrierAccount(
            CarrierAccount carrierAccount)
        {
            var request = new EasyPostRequest("carrier_accounts/{id}", Method.PUT);
            request.AddUrlSegment("id", carrierAccount.Id);
            request.AddBody(carrierAccount.AsDictionary(), "carrier_account");

            return Execute<CarrierAccount>(request);
        }

        /// <summary>
        /// Remove this CarrierAccount from your account.
        /// </summary>
        /// <param name="id">Carrier account id</param>
        public void DestroyCarrierAccount(
            string id)
        {
            var request = new EasyPostRequest("carrier_accounts/{id}", Method.DELETE);
            request.AddUrlSegment("id", id);

            Execute(request);
        }
    }
}