using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model.BindingTargets
{
    public class ClosingMarketSummary
    {
        public long ClosingMarketSummaryId { get; set; }
        public DateTime Date { get; set; }
        public string Symbol { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Closing { get; set; }
        public string Volume { get; set; }
        public string Ldcp { get; set; }        
    }
}
