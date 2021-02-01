using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model
{
    public class ScripInfo
    {
        public long ScripInfoId { get; set; }
        public long Number { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long Code { get; set; }
    }
}
