/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPost;

namespace EasyPostTest
{
    [TestClass]
    public class ClientTest
    {
        [TestMethod]
        public void TestApiBase()
        {
            var client = new EasyPostClient(new ClientConfiguration("apiKey", "https://foobar.com"));
            Assert.AreEqual(new Uri("https://foobar.com"), client.RestClient.BaseUrl);
        }

        [TestMethod]
        public void TestRestClient()
        {
            var client = new EasyPostClient(new ClientConfiguration("apiKey"));
            Assert.AreEqual(client.RestClient.BaseUrl, "https://api.easypost.com/v2");
        }

        [TestMethod]
        public void TestRestClientWithOptions()
        {
            var client = new EasyPostClient(new ClientConfiguration("someapikey", "http://apiBase.com"));
            Assert.AreEqual(new Uri("http://apiBase.com"), client.RestClient.BaseUrl);
        }

        [TestMethod]
        public void TestPrepareRequest()
        {
            var client = new EasyPostClient("apiKey");
            var request = new EasyPostRequest("resource");

            var parameters = client.PrepareRequest(request).Parameters.Select(parameter => parameter.ToString()).ToList();
            CollectionAssert.Contains(parameters, "user_agent=EasyPost/CSharpASync/" + client.Version);
            CollectionAssert.Contains(parameters, "authorization=Bearer apiKey");
            CollectionAssert.Contains(parameters, "content_type=application/x-www-form-urlencoded");
        }

        [TestMethod]
        public void TestPrepareRequestWithOptions()
        {
            var client = new EasyPostClient(new ClientConfiguration("someapikey", "http://foobar.com"));
            var request = new EasyPostRequest("resource");

            var parameters = client.PrepareRequest(request).Parameters.Select(parameter => parameter.ToString()).ToList();
            CollectionAssert.Contains(parameters, "user_agent=EasyPost/CSharpASync/" + client.Version);
            CollectionAssert.Contains(parameters, "authorization=Bearer someapikey");
            CollectionAssert.Contains(parameters, "content_type=application/x-www-form-urlencoded");
        }
    }
}