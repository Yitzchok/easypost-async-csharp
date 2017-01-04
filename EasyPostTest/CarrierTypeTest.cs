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
    public class CarrierTypeTest
    {
        private EasyPostClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new EasyPostClient("VJ63zukvLyxz92NKP1k0EQ");
        }

        [TestMethod]
        public void TestListCarrierTypes()
        {
            var types = _client.ListCarrierTypes().Result;
            Assert.AreNotEqual(0, types.Count);
        }
    }
}