/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest
{
    [TestClass]
    public class ScanFormTest
    {
        private EasyPostClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new EasyPostClient("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestScanFormList()
        {
            var scanFormList = _client.ListScanForms(new ScanFormListOptions {
                PageSize = 1,
            });
            Assert.AreNotEqual(null, scanFormList.ScanForms[0].BatchId);
            Assert.AreNotEqual(0, scanFormList.ScanForms.Count);
            var nextScanFormList = scanFormList.Next(_client);
            Assert.AreNotEqual(scanFormList.ScanForms[0].Id, nextScanFormList.ScanForms[0].Id);
        }
    }
}