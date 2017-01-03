/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using EasyPost;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest
{

    [TestClass]
    public class CustomsInfoTest
    {
        private EasyPostClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new EasyPostClient("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestCreateAndRetrieve()
        {
            var info = _client.CreateCustomsInfo(new CustomsInfo {
                CustomsCertify = true,
                EelPfc = "NOEEI 30.37(a)",
                CustomsItems = new List<CustomsItem> {
                    new CustomsItem {
                        Description = "TShirt",
                        Quantity = 1,
                        Weight = 8,
                        OriginCountry = "US",
                    },
                },
            });

            var retrieved = _client.GetCustomsInfo(info.Id);
            Assert.AreEqual(info.Id, retrieved.Id);
            Assert.IsNotNull(retrieved.CustomsItems);
        }
    }
}