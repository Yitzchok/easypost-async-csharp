/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyPost
{
    public class ReportList
    {
        /// <summary>
        /// List of reports
        /// </summary>
        public List<Report> Reports { get; set; }

        /// <summary>
        /// True if there are more to retrieve
        /// </summary>
        public bool HasMore { get; set; }

        /// <summary>
        /// Type of the report
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Options used to generate the list
        /// </summary>
        public ReportListOptions Options { get; set; }

        /// <summary>
        /// Get the next page of reports based on the original parameters used to generate the list
        /// </summary>
        /// <param name="client">Easy post client to use</param>
        /// <returns>A new ReportList instance.</returns>
        public async Task<ReportList> Next(
            IEasyPostClient client)
        {
            var options = Options ?? new ReportListOptions();
            options.BeforeId = Reports.Last().Id;
            return await client.ListReports(Type, options);
        }
    }
}
