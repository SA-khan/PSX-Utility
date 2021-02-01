using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model.BindingTargets
{
    public class FundwiseBucketMarketSummary
    {
        public long FundwiseBucketMarketSummaryId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string ReadingStatus { get; set; }
        public long FundId { get; set; }
        public string ShareName { get; set; }
        public string Sector { get; set; }
        public string Symbol { get; set; }
        public string Quantity { get; set; }
        public string AveragePrice { get; set; }
        public string BookCost { get; set; }
        public string MarketPrice { get; set; }
        public string MarketValue { get; set; }
        public string AppDep { get; set; }
        public string ClosingPercentage { get; set; }
    }
}
