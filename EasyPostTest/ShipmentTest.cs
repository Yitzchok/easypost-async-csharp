/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPost;

namespace EasyPostTest
{
    [TestClass]
    public class ShipmentTest
    {
        private EasyPostClient _client;
        private Shipment _testShipment;
        private Address _fromAddress;
        private Address _toAddress;

        [TestInitialize]
        public void Initialize()
        {
            _client = new EasyPostClient("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");

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
            _testShipment = new Shipment {
                ToAddress = _toAddress,
                FromAddress = _fromAddress,
                Parcel = new Parcel {
                    Length = 8,
                    Width = 6,
                    Height = 5,
                    Weight = 10,
                },
                Reference = "ShipmentRef",
                CustomsInfo = new CustomsInfo {
                    CustomsCertify = true,
                    EelPfc = "NOEEI 30.37(a)",
                    CustomsItems = new List<CustomsItem> {
                        new CustomsItem {
                            Description = "description"
                        }
                    }
                },
            };
        }

        private Shipment BuyShipment()
        {
            var shipment = _client.CreateShipment(_testShipment);
            return _client.BuyShipment(shipment.Id, shipment.Rates[0].Id);
        }

        [TestMethod]
        public void TestCreateAndRetrieve()
        {
            var shipment = _client.CreateShipment(_testShipment);
            Assert.IsNotNull(shipment.Id);
            Assert.AreEqual(shipment.Reference, "ShipmentRef");
            Assert.IsNotNull(shipment.Rates);
            Assert.AreNotEqual(shipment.Rates.Count, 0);

            var retrieved = _client.GetShipment(shipment.Id);
            Assert.AreEqual(shipment.Id, retrieved.Id);
            Assert.IsNotNull(retrieved.Rates);
            Assert.AreNotEqual(retrieved.Rates.Count, 0);
        }

        [TestMethod]
        public void TestOptions()
        {
            var tomorrow = DateTime.Now.AddDays(1);
            _testShipment.Options = new Options {
                LabelDate = tomorrow
            };
            var shipment = _client.CreateShipment(_testShipment);

            shipment.Options.LabelDate = shipment.Options.LabelDate.Value.ToLocalTime();
            Assert.AreEqual(shipment.Options.LabelDate.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"), tomorrow.ToString("yyyy-MM-ddTHH:mm:sszzz"));
        }

        [TestMethod]
        public void TestRateErrorMessages()
        {
            var shipment = _client.CreateShipment(new Shipment {
                ToAddress = _toAddress,
                FromAddress = _fromAddress,
                Parcel = new Parcel {
                    Weight = 10,  
                    PredefinedPackage = "FEDEXBOX",
                },
            });

            Assert.IsNotNull(shipment.Id);
            Assert.AreEqual(shipment.Messages[0].Carrier, "UPS");
            Assert.AreEqual(shipment.Messages[0].Type, "rate_error");
            Assert.AreEqual(shipment.Messages[0].Message, "Unable to retrieve UPS rates for another carrier's predefined_package parcel type.");
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceAlreadyCreated))]
        public void TestCreateWithId()
        {
            _client.CreateShipment(new Shipment { Id = "shp_asdlf" });
        }

        [TestMethod]
        public void TestRegenerateRates()
        {
            var shipment = _client.CreateShipment(_testShipment);
            _client.RegenerateRates(shipment);
            Assert.IsNotNull(shipment.Id);
            Assert.IsNotNull(shipment.Rates);
        }

        [TestMethod]
        public void TestCreateAndBuyPlusInsurance()
        {
            var shipment = _client.CreateShipment(_testShipment);
            Assert.IsNotNull(shipment.Rates);
            Assert.AreNotEqual(shipment.Rates.Count, 0);

            shipment = _client.BuyShipment(shipment.Id, shipment.Rates[0].Id);
            Assert.IsNotNull(shipment.PostageLabel);

            shipment = _client.BuyInsuranceForShipment(shipment.Id, 100.1);
            Assert.AreNotEqual(shipment.Insurance, 100.1);
        }

        [TestMethod]
        public void TestRefund()
        {
            var shipment = BuyShipment();
            shipment = _client.RefundShipment(shipment.Id);
            Assert.IsNotNull(shipment.RefundStatus);
        }

        [TestMethod]
        public void TestGenerateLabelStampBarcode()
        {
            var shipment = BuyShipment();

            shipment = _client.GenerateLabel(shipment.Id, "pdf");
            Assert.IsNotNull(shipment.PostageLabel);

            var url = _client.GenerateStamp(shipment.Id);
            Assert.IsNotNull(url);

            url = _client.GenerateBarcode(shipment.Id);
            Assert.IsNotNull(url);
        }

        [TestMethod]
        public void TestLowestRate()
        {
            var lowestUSPS = new CarrierRate { Rate = 1.0, Carrier = "USPS", Service = "ParcelSelect" };
            var highestUSPS = new CarrierRate { Rate = 10.0, Carrier = "USPS", Service = "Priority" };
            var lowestUPS = new CarrierRate { Rate = 2.0, Carrier = "UPS", Service = "ParcelSelect" };
            var highestUPS = new CarrierRate { Rate = 20.0, Carrier = "UPS", Service = "Priority" };

            var shipment = new Shipment { Rates = new List<CarrierRate> { highestUSPS, lowestUSPS, highestUPS, lowestUPS } };

            var rate = shipment.LowestRate();
            Assert.AreEqual(rate, lowestUSPS);

            rate = shipment.LowestRate(new[] { "UPS" });
            Assert.AreEqual(rate, lowestUPS);

            rate = shipment.LowestRate(includeServices: new[] { "Priority" });
            Assert.AreEqual(rate, highestUSPS);

            rate = shipment.LowestRate(excludeCarriers: new[] { "USPS" });
            Assert.AreEqual(rate, lowestUPS);

            rate = shipment.LowestRate(excludeServices: new[] { "ParcelSelect" });
            Assert.AreEqual(rate, highestUSPS);

            rate = shipment.LowestRate(new[] { "FedEx" });
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void TestCarrierAccounts()
        {
            var shipment = _testShipment;
            shipment.CarrierAccounts = new List<CarrierAccount> { new CarrierAccount { Id = "ca_qn6QC6fd" } };
            shipment = _client.CreateShipment(_testShipment);
            if (shipment.Rates.Count > 0) {
                Assert.IsTrue(shipment.Rates.TrueForAll(r => r.CarrierAccountId == "ca_qn6QC6fd"));
            }
        }

        [TestMethod]
        public void TestList()
        {
            var shipmentList = _client.ListShipments();
            Assert.AreNotEqual(0, shipmentList.Shipments.Count);

            var nextShipmentList = shipmentList.Next(_client);
            Assert.AreNotEqual(0, nextShipmentList.Shipments.Count);
            Assert.AreNotEqual(shipmentList.Shipments[0].Id, nextShipmentList.Shipments[0].Id);
        }
    }
}