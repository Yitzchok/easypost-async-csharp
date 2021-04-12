/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [TestMethod, Ignore("Manual Test. See comment in create report request")]
        public async Task TestCreateAndRetrieve()
        {
            var createReport = new Report
            {
                IncludeChildren = true,
                // Unfortunately, this can only be run once a day. If you need to test more than that change the date here.
                //StartDate = DateTime.Parse("2016-06-03"),
                //EndDate = DateTime.Parse("2016-06-04"),
            };

            var report = await _client.CreateReport("shipment", createReport);

            Assert.IsNotNull(report.Id);
            Assert.AreEqual(createReport.StartDate, report.StartDate);
            Assert.AreEqual(createReport.EndDate, report.EndDate);
            Assert.AreEqual("new", report.Status);
            Assert.IsTrue(report.IncludeChildren);

            var retrieved = await _client.GetReport("shipment", report.Id);
            Assert.AreEqual(report.Id, retrieved.Id);
        }

        [TestMethod]
        public async Task TestList()
        {
            var reportList = await _client.ListReports("shipment");
            Assert.AreNotEqual(0, reportList.Reports.Count);

            var nextReportList = await reportList.Next(_client);
            Assert.AreNotEqual(reportList.Reports[0].Id, nextReportList.Reports[0].Id);
        }
    }
}