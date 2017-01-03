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
    public class User : EasyPostObject
    {
        /// <summary>
        /// The ID of the parent user object. Top-level users are defined as users with no parent
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// First and last name required
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password for the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Password confirmation for the user
        /// </summary>
        public string PasswordConfirmation { get; set; }

        /// <summary>
        /// Optional phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Formatted as string "XX.XXXXX"
        /// </summary>
        public string Balance { get; set; }

        /// <summary>
        /// USD formatted dollars and cents, delimited by a decimal point
        /// </summary>
        public string RechargeAmount { get; set; }

        /// <summary>
        /// USD formatted dollars and cents, delimited by a decimal point
        /// </summary>
        public string SecondaryRechargeAmount { get; set; }

        /// <summary>
        /// Number of cents USD that when your balance drops below, we automatically recharge your account with your primary payment method.
        /// </summary>
        public string RechargeThreshold { get; set; }

        /// <summary>
        /// All associated children
        /// </summary>
        public List<User> Children { get; set; }

        /// <summary>
        /// Price pre shipment
        /// </summary>
        public string PricePerShipment { get; set; }

        /// <summary>
        /// All user API keys
        /// </summary>
        public List<ApiKey> ApiKeys { get; set; }
    }

    /// <summary>
    /// User API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Retrieve a User from its id. If no id is specified, it returns the user for the api_key specified.
        /// </summary>
        /// <param name="id">String representing a user. Starts with "user_".</param>
        /// <returns>User instance.</returns>
        public User GetUser(
            string id = null)
        {
            EasyPostRequest request;
            if (id == null) {
                request = new EasyPostRequest("users");
            } else {
                request = new EasyPostRequest("users/{id}");
                request.AddUrlSegment("id", id);
            }

            return Execute<User>(request);
        }

        /// <summary>
        /// Create a child user for the account associated with the api_key specified.
        /// </summary>
        /// <param name="userName">Name of the user</param>
        /// <returns>EasyPost.User instance.</returns>
        public User CreateUser(
            string userName)
        {
            var request = new EasyPostRequest("users", Method.POST);
            request.AddBody(new Dictionary<string, object> { { "name", userName } }, "user");

            return Execute<User>(request);
        }

        /// <summary>
        /// Update the User associated with the api_key specified.
        /// </summary>
        /// <param name="user">User parameters to update</param>
        public User UpdateUser(
            User user)
        {
            var request = new EasyPostRequest("users/{id}", Method.PUT);
            request.AddUrlSegment("id", user.Id);
            request.AddBody(user.AsDictionary(), "user");

            return Execute<User>(request);
        }

        /// <summary>
        /// Destroys a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        public void DestroyUser(
            string id)
        {
            var request = new EasyPostRequest("users/{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            Execute(request);
        }
    }
}