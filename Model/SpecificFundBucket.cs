using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model
{
    public class SpecificFundBucket
    {
        public long SB_ID { get; set; }
        public DateTime FSB_DATE { get; set; }
        public string FSB_STATUS { get; set; }
        public bool FSB_READING_STATUS { get; set; }
        public long FSB_FUND_ID { get; set; }
        public string FSB_FUND_NAME { get; set; }
        public string FSB_SHARE_NAME { get; set; }
        public string FSB_SHARE_SYMBOL { get; set; }
        public string FSB_SHARE_QUANTITY { get; set; }
        public decimal FSB_SHARE_AVG_PRICE { get; set; }
        public string FSB_SHARE_BOOK_COST { get; set; }
        public decimal FSB_SHARE_MARKET_PRICE { get; set; }
        public string FSB_SHARE_MARKET_VALUE { get; set; }
        public string FSB_SHARE_APP_DEP { get; set; }
        public string FSB_SHARE_PERCENTAGE_CLOSING { get; set; }
    }
}
