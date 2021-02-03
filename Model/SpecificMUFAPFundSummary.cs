using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model
{
    public class SpecificMUFAPFundSummary
    {
        public Int64 FundId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Rating { get; set; }
        public string ValidityDate { get; set; }
        public string NAV { get; set; }
        public string YTD { get; set; }
        public string MTD { get; set; }
        public string _1Day { get; set; }
        public string _15Days { get; set; }
        public string _30Days { get; set; }
        public string _90Days { get; set; }
        public string _180Days { get; set; }
        public string _270Days { get; set; }
        public string _365Days { get; set; }
        public string TER { get; set; }
        public string MF { get; set; }
        public string SANDM { get; set; }

    }
}
