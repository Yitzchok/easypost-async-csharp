/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public partial class EasyPostClient : IEasyPostClient
    {
        internal RestClient RestClient;
        internal ClientConfiguration Configuration;

        /// <summary>
        /// Version of the EasyPost client API
        /// </summary>
        public string Version;

        /// <summary>
        /// Create a new EasyPost client
        /// </summary>
        /// <param name="apiKey">API key to use</param>
        public EasyPostClient(
            string apiKey)
            : this(new ClientConfiguration(apiKey))
        {
        }

        /// <summary>
        /// Create a new EasyPost client
        /// </summary>
        /// <param name="clientConfiguration">Client configuration to use</param>
        public EasyPostClient(
            ClientConfiguration clientConfiguration)
        {
            if (clientConfiguration == null)
                throw new ArgumentNullException(nameof(clientConfiguration));
            Configuration = clientConfiguration;

            RestClient = new RestClient(clientConfiguration.ApiBase);

            var assembly = Assembly.GetExecutingAssembly();
            var info = FileVersionInfo.GetVersionInfo(assembly.Location);
            Version = info.FileVersion;
        }

        /// <summary>
        /// Internal function to execute a request
        /// </summary>
        /// <param name="request">EasyPost request to execute</param>
        private void Execute(
            EasyPostRequest request)
        {
            RestClient.Execute(PrepareRequest(request));
        }

        /// <summary>
        /// Internal function to execute a typed request
        /// </summary>
        /// <typeparam name="T">Type of the JSON response we are expecting</typeparam>
        /// <param name="request">EasyPost request to execute</param>
        /// <returns>Response for the request</returns>
        private T Execute<T>(
            EasyPostRequest request) where T : new()
        {
            var response = RestClient.Execute<T>(PrepareRequest(request));
            var statusCode = response.StatusCode;

            if (statusCode >= HttpStatusCode.BadRequest) {
                string message, details;
                try {
                    var body = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(response.Content);
                    message = (string)body["error"]["message"];
                    details = (string)body["error"]["code"];
                } catch {
                    throw new HttpException(statusCode, "RESPONSE.PARSE_ERROR", response.Content);
                }
                throw new HttpException(statusCode, message, details);
            }

            return response.Data;
        }

        /// <summary>
        /// Internal function to prepate the request to be executed
        /// </summary>
        /// <param name="request">EasyPost request to be executed</param>
        /// <returns>RestSharp request to execute</returns>
        internal RestRequest PrepareRequest(
            EasyPostRequest request)
        {
            var restRequest = request.RestRequest;

            restRequest.AddHeader("user_agent", string.Concat("EasyPost/CSharpASync/", Version));
            restRequest.AddHeader("authorization", "Bearer " + Configuration.ApiKey);
            restRequest.AddHeader("content_type", "application/x-www-form-urlencoded");

            return restRequest;
        }
    }
}