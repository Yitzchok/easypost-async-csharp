﻿/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            _toAddress = new Address
            {
                Company = "Simpler Postage Inc",
                Street1 = "164 Townsend Street",
                Street2 = "Unit 1",
                City = "San Francisco",
                State = "CA",
                Country = "US",
                Zip = "94107",
            };
            _fromAddress = new Address
            {
                Name = "Andrew Tribone",
                Street1 = "480 Fell St",
                Street2 = "#3",
                City = "San Francisco",
                State = "CA",
                Country = "US",
                Zip = "94102",
            };
            _testShipment = new Shipment
            {
                ToAddress = _toAddress,
                FromAddress = _fromAddress,
                Parcel = new Parcel
                {
                    Length = 8,
                    Width = 6,
                    Height = 5,
                    Weight = 10,
                },
                Reference = "ShipmentRef",
                CustomsInfo = new CustomsInfo
                {
                    CustomsCertify = true,
                    EelPfc = "NOEEI 30.37(a)",
                    CustomsItems = new List<CustomsItem> {
                        new CustomsItem {
                            Description = "description",
                            Quantity = 1
                        }
                    }
                },
                Options = new Options()
            };
        }

        private async Task<Shipment> BuyShipment()
        {
            var shipment = await _client.CreateShipment(_testShipment);
            return await _client.BuyShipment(shipment.Id, shipment.Rates[0].Id);
        }

        [TestMethod]
        public async Task TestCreateAndRetrieve()
        {
            var shipment = await _client.CreateShipment(_testShipment);
            Assert.IsNotNull(shipment.Id);
            Assert.AreEqual(shipment.Reference, "ShipmentRef");
            Assert.IsNotNull(shipment.Rates);
            Assert.AreNotEqual(shipment.Rates.Count, 0);

            var retrieved = await _client.GetShipment(shipment.Id);
            Assert.AreEqual(shipment.Id, retrieved.Id);
            Assert.IsNotNull(retrieved.Rates);
            Assert.AreNotEqual(retrieved.Rates.Count, 0);
        }

        [TestMethod]
        public async Task TestOptions()
        {
            var tomorrow = DateTime.UtcNow.AddDays(1);
            _testShipment.Options = new Options
            {
                LabelDate = tomorrow
            };

            var shipment = await _client.CreateShipment(_testShipment);

            var format = "yyyy-MM-ddTHH:mm:sszzz";
            Assert.AreEqual(tomorrow.ToString(format), shipment.Options.LabelDate.Value.ToString(format));
        }

        [TestMethod]
        public async Task TestPostageInline()
        {
            _testShipment.Options.PostageLabelInline = true;
            Shipment shipment = await BuyShipment();
            Assert.IsNotNull(shipment.PostageLabel.LabelFile);
        }

        [TestMethod]
        public async Task TestPostageZpl()
        {
            _testShipment.Options.LabelFormat = "zpl";
            
            Shipment shipment = await BuyShipment();

            Assert.IsNotNull(shipment.PostageLabel.LabelUrl);
            Assert.IsNotNull(shipment.PostageLabel.LabelZplUrl);
            Assert.AreEqual(shipment.PostageLabel.LabelType, "default");
            Assert.IsNull(shipment.PostageLabel.LabelFile);
            Assert.AreEqual(shipment.PostageLabel.LabelFileType, "application/zpl");
        }

        [TestMethod]
        public async Task TestPostagePdf()
        {
            _testShipment.Options.LabelFormat = "pdf";
            Shipment shipment = await BuyShipment();

            Assert.IsNotNull(shipment.PostageLabel.LabelUrl);
            Assert.IsNotNull(shipment.PostageLabel.LabelPdfUrl);
            Assert.AreEqual(shipment.PostageLabel.LabelFileType, "application/pdf");
            Assert.AreEqual(shipment.PostageLabel.LabelType, "default");
            Assert.IsNull(shipment.PostageLabel.LabelFile);
        }

        [TestMethod]
        public async Task TestPostagePng()
        {
            Shipment shipment = await BuyShipment();

            Assert.IsNotNull(shipment.PostageLabel.LabelUrl);
            Assert.IsNull(shipment.PostageLabel.LabelFileType);
            Assert.AreEqual(shipment.PostageLabel.LabelType, "default");
            Assert.IsNull(shipment.PostageLabel.LabelFile);
        }

        [TestMethod]
        public async Task TestRateErrorMessages()
        {
            var shipment = await _client.CreateShipment(new Shipment
            {
                ToAddress = _toAddress,
                FromAddress = _fromAddress,
                Parcel = new Parcel
                {
                    Weight = 10,
                    PredefinedPackage = "FEDEXBOX",
                },
            }).ConfigureAwait(false);

            Assert.IsNotNull(shipment.Id);
            var easyPostMessage = shipment.Messages[0];

            Assert.IsNotNull(easyPostMessage.Carrier);
            Assert.AreEqual("rate_error", easyPostMessage.Type);
            Assert.IsNotNull(easyPostMessage.Message);
        }

        [TestMethod]
        public async Task TestRegenerateRates()
        {
            var shipment = await _client.CreateShipment(_testShipment);
            await _client.RegenerateRates(shipment);
            Assert.IsNotNull(shipment.Id);
            Assert.IsNotNull(shipment.Rates);
        }

        [TestMethod]
        public async Task TestCreateAndBuyPlusInsurance()
        {
            var shipment = await _client.CreateShipment(_testShipment);
            Assert.IsNotNull(shipment.Rates);
            Assert.AreNotEqual(0, shipment.Rates.Count);

            shipment = await _client.BuyShipment(shipment.Id, shipment.Rates[0].Id);
            Assert.IsNotNull(shipment.PostageLabel);

            shipment = await _client.BuyInsuranceForShipment(shipment.Id, 100.1);
            Assert.AreEqual("100.10", shipment.Insurance);
        }

        [TestMethod]
        public async Task TestBuyTogetherWithInsurance()
        {
            var shipment = await _client.CreateShipment(_testShipment);
            Assert.IsNotNull(shipment.Rates);
            Assert.AreNotEqual(0, shipment.Rates.Count);

            shipment = await _client.BuyShipment(shipment.Id, shipment.Rates[0].Id, 200);

            Assert.IsNotNull(shipment.PostageLabel);

            Assert.AreEqual("200.00", shipment.Insurance);
            Assert.IsTrue(shipment.Fees.Any(x => x.Type == "InsuranceFee"));
            Assert.IsTrue(shipment.Fees.SingleOrDefault(x => x.Type == "InsuranceFee")?.Amount > 0);
        }

        [TestMethod]
        public async Task TestRefund()
        {
            var shipment = await BuyShipment();
            shipment = await _client.RefundShipment(shipment.Id);
            Assert.IsNotNull(shipment.RefundStatus);
        }

        [TestMethod]
        public async Task TestGenerateLabel()
        {
            var shipment = await BuyShipment();

            shipment = await _client.GenerateLabel(shipment.Id, "pdf");
            Assert.IsNotNull(shipment.PostageLabel);
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
        public async Task TestCarrierAccounts()
        {
            var shipment = _testShipment;
            shipment.CarrierAccounts = new List<CarrierAccount> { new CarrierAccount { Id = "ca_qn6QC6fd" } };
            shipment = await _client.CreateShipment(_testShipment);
            if (shipment.Rates.Count > 0)
            {
                Assert.IsTrue(shipment.Rates.TrueForAll(r => r.CarrierAccountId == "ca_qn6QC6fd"));
            }
        }

        [TestMethod]
        public async Task TestList()
        {
            var shipmentList = await _client.ListShipments();
            Assert.AreNotEqual(0, shipmentList.Shipments.Count);

            var nextShipmentList = await shipmentList.Next(_client);
            Assert.AreNotEqual(0, nextShipmentList.Shipments.Count);
            Assert.AreNotEqual(shipmentList.Shipments[0].Id, nextShipmentList.Shipments[0].Id);
        }

        [TestMethod]
        public async Task TestListWithOptions()
        {
            var shipmentList = await _client.ListShipments(new ShipmentListOptions
            {
                EndDatetime = DateTime.UtcNow,
                PageSize = 1
            });

            Assert.AreNotEqual(0, shipmentList.Shipments.Count);
        }
    }
}