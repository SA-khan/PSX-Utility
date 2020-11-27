using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model
{
    public class MarketSummary
    {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Symbol { get; set; }
            public string CURRENT { get; set; }

            public string LDCP { get; set; }
            public string OPEN { get; set; }
            public string HIGH { get; set; }
            public string LOW { get; set; }
            public double Change { get; set; }
            public string Volume { get; set; }

            public DateTime Date { get; set; }
            public Int64 Category { get; set; }
            public string Closing { get; set; }

    }
}
