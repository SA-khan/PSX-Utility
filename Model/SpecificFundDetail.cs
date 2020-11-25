using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model
{
    public class SpecificFundDetail
    {
        public long FundId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal BookCost { get; set; }
        public string MarketPrice { get; set; }
        public decimal MarketValue { get; set; }
        public string AppDep { get; set; }
    }
}
