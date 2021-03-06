﻿/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPost;

namespace EasyPostTest
{
    [TestClass]
    public class CarrierAccountTest
    {
        private EasyPostClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new EasyPostClient("VJ63zukvLyxz92NKP1k0EQ");
        }

        [TestMethod]
        public void TestRetrieve()
        {
            var account = _client.GetCarrierAccount("ca_7c7X1XzO").Result;
            Assert.AreEqual("ca_7c7X1XzO", account.Id);
        }

        [TestMethod]
        public void TestCrud()
        {
            var account = _client.CreateCarrierAccount(new CarrierAccount {
                Type = "EndiciaAccount",
                Description = "description",
            }).Result;

            Assert.IsNotNull(account.Id);
            Assert.AreEqual(account.Type, "EndiciaAccount");

            account.Reference = "new-reference";
            account = _client.UpdateCarrierAccount(account).Result;
            Assert.AreEqual("new-reference", account.Reference);

            _client.DestroyCarrierAccount(account.Id).Wait();

            account = _client.GetCarrierAccount(account.Id).Result;
            Assert.IsNotNull(account.RequestError);
            Assert.AreEqual(account.RequestError.Code, "NOT_FOUND");
        }

        [TestMethod]
        public void TestList()
        {
            var accounts = _client.ListCarrierAccounts().Result;
            Assert.AreEqual(accounts[0].Id, "ca_7c7X1XzO");
        }
    }
}