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
    public class AddressTest
    {
        private EasyPostClient _client;
        private Address _testAddress;

        [TestInitialize]
        public void Initialize()
        {
            _client = new EasyPostClient("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
            _testAddress = new Address {
                Company = "Simpler Postage Inc",
                Street1 = "164 Townsend Street",
                Street2 = "Unit 1",
                City = "San Francisco",
                State = "CA",
                Country = "US",
                Zip = "94107"
            };
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestRetrieveInvalidId()
        {
            _client.GetAddress("not-an-id");
        }

        [TestMethod]
        public void TestCreateAndRetrieve()
        {
            var address = _client.CreateAddress(_testAddress);
            Assert.IsNotNull(address.Id);
            Assert.AreEqual(address.Company, "Simpler Postage Inc");
            Assert.IsNull(address.Name);

            var retrieved = _client.GetAddress(address.Id);
            Assert.AreEqual(address.Id, retrieved.Id);
        }

        [TestMethod]
        public void TestCreateWithVerifications()
        {
            var address = _client.CreateAddress(_testAddress, VerificationFlags.Delivery | VerificationFlags.Zip4);
            Assert.IsNotNull(address.Verifications.Delivery);
            Assert.AreEqual(address.Verifications.Delivery.Success, true);
            Assert.IsNotNull(address.Verifications.Zip4);
            Assert.AreEqual(address.Verifications.Zip4.Success, true);

            address = _client.CreateAddress(new Address {
                    Company = "Simpler Postage Inc",
                    Street1 = "123 Fake Street",
                    Zip = "94107"
                },
                VerificationFlags.Delivery | VerificationFlags.Zip4);
            Assert.AreEqual(address.Verifications.Delivery.Success, false);
            Assert.AreEqual(address.Verifications.Zip4.Success, false);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestCreateWithStrictVerifications()
        {
            _client.CreateAddress(new Address {
                    Company = "Simpler Postage Inc",
                    Street1 = "123 Fake Street",
                    Zip = "94107"
                },
                VerificationFlags.DeliveryStrict | VerificationFlags.Zip4Strict);
        }

        [TestMethod]
        public void TestVerify()
        {
            var address = _client.CreateAddress(_testAddress);
            address = _client.VerifyAddress(address);
            Assert.IsNotNull(address.Id);
            Assert.AreEqual(address.Company, "SIMPLER POSTAGE INC");
            Assert.AreEqual(address.Street1, "164 TOWNSEND ST UNIT 1");
            Assert.IsNull(address.Name);
            Assert.IsTrue((bool)address.Residential);
        }

        [TestMethod]
        public void TestVerifyCarrier()
        {
            var address = _client.CreateAddress(_testAddress);
            address = _client.VerifyAddress(address, "usps");
            Assert.IsNotNull(address.Id);
            Assert.AreEqual(address.Company, "SIMPLER POSTAGE INC");
            Assert.AreEqual(address.Street1, "164 TOWNSEND ST UNIT 1");
            Assert.IsNull(address.Name);
        }

        [TestMethod]
        public void TestVerifyBeforeCreate()
        {
            var address = _client.VerifyAddress(_testAddress);
            Assert.IsNotNull(address.Id);
            Assert.AreEqual(address.Company, "SIMPLER POSTAGE INC");
        }
    }
}