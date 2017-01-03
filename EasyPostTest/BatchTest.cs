/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using EasyPost;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest
{
    [TestClass]
    public class BatchTest
    {
        private EasyPostClient _client;
        private Shipment _testShipment;
        private Shipment _testBatchShipment;
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
            };
            _testBatchShipment = new Shipment {
                ToAddress = _toAddress,
                FromAddress = _fromAddress,
                Parcel = new Parcel {
                    Length = 8,
                    Width = 6,
                    Height = 5,
                    Weight = 10,
                },
                Carrier = "USPS",
                Service = "Priority",
            };
        }

        [TestMethod]
        public void TestRetrieve()
        {
            var batch = _client.CreateBatch();
            var retrieved = _client.GetBatch(batch.Id);
            Assert.AreEqual(batch.Id, retrieved.Id);
        }

        [TestMethod]
        public void TestAddRemoveShipments()
        {
            var batch = _client.CreateBatch();
            var shipment = _client.CreateShipment(_testShipment);
            var otherShipment = _client.CreateShipment(_testShipment);

            while (batch.State != "created") {
                batch = _client.GetBatch(batch.Id);
            }

            batch = _client.AddShipmentsToBatch(batch.Id, new[] { shipment, otherShipment });

            while (batch.Shipments == null) {
                batch = _client.GetBatch(batch.Id);
            }
            var shipmentIds = batch.Shipments.Select(ship => ship.Id).ToList();
            Assert.AreEqual(batch.NumShipments, 2);
            CollectionAssert.Contains(shipmentIds, shipment.Id);
            CollectionAssert.Contains(shipmentIds, otherShipment.Id);

            batch = _client.RemoveShipmentsFromBatch(batch.Id, new[] { shipment, otherShipment });
            Assert.AreEqual(batch.NumShipments, 0);
        }

        public Batch CreateBatch()
        {
            return _client.CreateBatch(new[] { _testBatchShipment }, "EasyPostCSharpTest");
        }

        [TestMethod]
        public void TestCreateThenBuyThenGenerateLabelAndScanForm()
        {
            var batch = CreateBatch();
            
            Assert.IsNotNull(batch.Id);
            Assert.AreEqual(batch.Reference, "EasyPostCSharpTest");
            Assert.AreEqual(batch.State, "creating");
            
            while (batch.State == "creating") {
                batch = _client.GetBatch(batch.Id);
            }
            batch = _client.BuyLabelsForBatch(batch.Id);
            
            while (batch.State == "created") {
                batch = _client.GetBatch(batch.Id);
            }
            Assert.AreEqual(batch.State, "purchased");
            
            batch = _client.GenerateLabelForBatch(batch.Id, "pdf");
            Assert.AreEqual(batch.State, "label_generating");
            
            batch = _client.GenerateScanFormForBatch(batch.Id);
        }
        
        [TestMethod]
        public void TestGenerateLabelWithOrderBy()
        {
            var batch = CreateBatch();

            while (batch.State == "creating") {
                batch = _client.GetBatch(batch.Id);
            }
            batch = _client.BuyLabelsForBatch(batch.Id);

            while (batch.State == "created") {
                batch = _client.GetBatch(batch.Id);
            }
            batch = _client.GenerateLabelForBatch(batch.Id, "pdf", "reference DESC");
            Assert.AreEqual(batch.State, "label_generating");
        }
    }
}