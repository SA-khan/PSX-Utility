using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model
{
    class FundMarket
    {
        public int SERIAL { get; set; }
        public  string NAME { get; set; }
        public  string SYMBOL { get; set; }
        public  string LDCP { get; set; }
        public  string OPEN { get; set; }
        public  string HIGH { get; set; }
        public  string LOW { get; set; }
        public string CURRENT { get; set; }
        public  string CHANGE { get; set; }
        public  string VOLUME { get; set; }

        public string APPRECIATION_DEPRECIATION { get; set; }
    }
}
