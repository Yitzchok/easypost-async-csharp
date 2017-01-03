/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPost;

namespace EasyPostTest
{
    [TestClass]
    public class PickupTest
    {
        private EasyPostClient _client;
        private Pickup _testPickup;
        private Address _address, _toAddress, _fromAddress;
        private Shipment _shipment;

        [TestInitialize]
        public void Initialize()
        {
            _client = new EasyPostClient("WzJHJ6SoPnBVYu0ae4aIHA");
            _address = new Address {
                Company = "Simpler Postage Inc",
                Street1 = "164 Townsend Street",
                Street2 = "Unit 1",
                City = "San Francisco",
                State = "CA",
                Country = "US",
                Zip = "94107",
                Phone = "1234567890"
            };
            _toAddress = new Address {
                Company = "Simpler Postage Inc",
                Street1 = "164 Townsend Street",
                Street2 = "Unit 1",
                City = "San Francisco",
                State = "CA",
                Country = "US",
                Zip = "94107",
            };
            _fromAddress = new Address {
                Name = "Andrew Tribone",
                Street1 = "480 Fell St",
                Street2 = "#3",
                City = "San Francisco",
                State = "CA",
                Country = "US",
                Zip = "94102",
            };
            _shipment = _client.CreateShipment(new Shipment {
                Parcel = new Parcel {
                    Length = 8,
                    Width = 6,
                    Height = 5,
                    Weight = 10,
                },
                ToAddress = _toAddress,
                FromAddress = _fromAddress,
                Reference = "ShipmentRef",
            });
            _client.BuyShipment(_shipment.Id, _shipment.LowestRate().Id);
            _testPickup = new Pickup {
                IsAccountAddress = false,
                Address = _address,
                Shipment = _shipment,
                MinDatetime = DateTime.Now,
                MaxDatetime = DateTime.Now,
            };
        }

        [TestMethod]
        public void TestCreateAndRetrieve()
        {
            var pickup = _client.CreatePickup(_testPickup);

            Assert.IsNotNull(pickup.Id);
            Assert.AreEqual(pickup.Address.Street1, "164 Townsend Street");

            var retrieved = _client.GetPickup(pickup.Id);
            Assert.AreEqual(pickup.Id, retrieved.Id);
        }

        [TestMethod]
        public void TestBuyAndCancel()
        {
            var pickup = _client.CreatePickup(_testPickup);

            pickup = _client.BuyPickup(pickup.Id, "UPS", pickup.PickupRates[0].Service);
            Assert.IsNotNull(pickup.Confirmation);

            pickup = _client.CancelPickp(pickup.Id);
            Assert.AreEqual(pickup.Status, "canceled");
        }
    }
}