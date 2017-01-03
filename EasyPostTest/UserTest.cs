/*
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
    public class UserTest
    {
        private EasyPostClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new EasyPostClient("VJ63zukvLyxz92NKP1k0EQ");
        }

        [TestMethod]
        public void TestRetrieveSelf()
        {
            var user = _client.GetUser();
            Assert.IsNotNull(user.Id);

            var user2 = _client.GetUser(user.Id);
            Assert.AreEqual(user.Id, user2.Id);
        }

        [TestMethod]
        public void TestCrud()
        {
            var user = _client.CreateUser("Test Name");
            Assert.AreEqual(user.ApiKeys.Count, 2);
            Assert.IsNotNull(user.Id);

            var other = _client.GetUser(user.Id);
            Assert.AreEqual(user.Id, other.Id);

            user.Name = "NewTest Name";
            user = _client.UpdateUser(user);
            Assert.AreEqual("NewTest Name", user.Name);

            _client.DestroyUser(user.Id);
            try {
                _client.GetUser(user.Id);
                Assert.Fail();
            } catch (HttpException) {
            }
        }
    }
}