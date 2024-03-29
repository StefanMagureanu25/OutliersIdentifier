﻿using CsvHelper.Configuration.Attributes;

namespace OutliersIdentifier.Models
{
    /// <summary>
    /// Model which retrieves the data from csv files
    /// </summary>
    public class DataPoint
    {
        [Index(0)]
        public string Ticker { get; set; }
        [Index(1)]
        public string Timestamp { get; set; }
        [Index(2)]
        public string StockPrice { get; set; }
    }
}
