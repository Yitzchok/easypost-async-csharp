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
    public class ReportTest
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
            var report = _client.CreateReport("shipment", new Report {
                IncludeChildren = true,
                // Unfortunately, this can only be run once a day. If you need to test more than that change the date here.
                //EndDate = DateTime.Parse("2016-06-01"),
            });
            Assert.IsNotNull(report.Id);
            Assert.IsTrue(report.IncludeChildren);

            var retrieved = _client.GetReport("shipment", report.Id);
            Assert.AreEqual(report.Id, retrieved.Id);
        }

        [TestMethod]
        public void TestList()
        {
            var reportList = _client.ListReports("shipment");
            Assert.AreNotEqual(0, reportList.Reports.Count);

            var nextReportList = reportList.Next(_client);
            Assert.AreNotEqual(reportList.Reports[0].Id, nextReportList.Reports[0].Id);
        }
    }
}