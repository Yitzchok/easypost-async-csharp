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
    public class EventTest
    {
        private EasyPostClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new EasyPostClient("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestRetrieve()
        {
            var e = _client.GetEvent("evt_8ff440c1bcef40c6a825171d190c3bdb").Result;
            Assert.AreEqual(e.Result["id"], "sf_8b4c8eb46fa0459e9e7be9f2e784da03");
        }
    }
}