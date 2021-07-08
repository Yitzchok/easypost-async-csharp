/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Threading.Tasks;
using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest
{
    [TestClass]
    public class WebhooksTest
    {
        private EasyPostClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new EasyPostClient("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        [Ignore]
        public async Task TestLifecycle()
        {
            const string url = "https://easypostwebhooktest.free.beeceptor.com/webhooks";

            var webhook = await _client.CreateWebhook(url);
            Assert.AreEqual(url, webhook.Url);

            var webhookId = webhook.Id;

            var t = await _client.GetWebhook(webhookId);
            Assert.AreEqual(webhookId, t.Id);
            Assert.AreEqual(url, t.Url);

            await _client.DeleteWebhook(webhookId);

            var notFound = await _client.GetWebhook(webhookId);

            Assert.IsNull(notFound.Id);
            Assert.AreEqual("NOT_FOUND", notFound.RequestError.Code);
        }

        [TestMethod]
        public async Task TestList()
        {
            var webhooks = await _client.ListWebhooks();
            Assert.AreNotEqual(0, webhooks.Webhooks.Count);
        }
    }
}