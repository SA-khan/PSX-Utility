using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model
{
    public class Status
    {
        public long StatusId { get; set; }
        public DateTime Date { get; set; }
        public string Table { get; set; }
        public string Description { get; set; }
        public DateTime Fetching_Date_Start { get; set; }
        public Boolean Fetching { get; set; }
        public DateTime Fetching_Date_End { get; set; }
        public Boolean Clear { get; set; }
        public Boolean Dump { get; set; }
        public Boolean Comment { get; set; }

    }
}
