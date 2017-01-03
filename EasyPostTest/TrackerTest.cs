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
    public class TrackerTest
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
            const string carrier = "USPS";
            const string trackingCode = "EZ1000000001";

            var tracker = _client.CreateTracker(carrier, trackingCode);
            Assert.AreEqual(tracker.TrackingCode, trackingCode);
            Assert.IsNotNull(tracker.EstDeliveryDate);
            Assert.IsNotNull(tracker.Carrier);
            Assert.IsNotNull(tracker.PublicUrl);

            Assert.AreEqual(_client.GetTracker(tracker.Id).Id, tracker.Id);
        }

        [TestMethod]
        public void TestList()
        {
            var trackerList = _client.ListTrackers();
            Assert.AreNotEqual(0, trackerList.Trackers.Count);

            var nextTrackerList = trackerList.Next(_client);
            Assert.AreNotEqual(trackerList.Trackers[0].Id, nextTrackerList.Trackers[0].Id);
        }
    }
}