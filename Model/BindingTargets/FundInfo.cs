using System;
using System.Collections.Generic;
using System.Text;

namespace PSXDataFetchingApp.Model.BindingTargets
{
    public class FundInfo
    {
        public long FundInfoId { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
    }
}
