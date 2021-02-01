using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model.BindingTargets
{
    public class MufapMarketSummary
    {
        public long MufapMarketSummaryId { get; set; }
        public DateTime Date { get; set; }
        public DateTime ValidityDate { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Rating { get; set; }
        public string Nav { get; set; }
        public string Ytd { get; set; }
        public string Mtd { get; set; }
        public string _1Day { get; set; }
        public string _15Days { get; set; }
        public string _30Days { get; set; }
        public string _90Days { get; set; }
        public string _180Days { get; set; }
        public string _270Days { get; set; }
        public string _365Days { get; set; }
        public string Ter { get; set; }
        public string Mf { get; set; }
        public string Sandm { get; set; }
    }
}
