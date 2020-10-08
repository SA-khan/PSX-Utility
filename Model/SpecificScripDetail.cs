using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model
{
    public class SpecificScripDetail 
    {
        public string SECTOR { get; set; }
        public DateTime DATE { get; set; }
        public string STATUS { get; set; }
        public string SCRIP { get; set; }
        public string SYMBOL { get; set; }
        public decimal LDCP { get; set; }
        public decimal OPEN { get; set; }
        public decimal HIGH { get; set; }
        public decimal LOW { get; set; }
        public decimal CURRENT { get; set; }
        public double CHANGE { get; set; }
        public decimal VOLUME { get; set; }
    }
    
}
