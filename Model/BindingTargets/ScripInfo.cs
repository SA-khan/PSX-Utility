using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using Nest;

namespace PSXDataFetchingApp.Model
{
    //[ElasticType(IProperty = "ScripInfoId")]
    public class ScripInfo
    {
        public long ScripInfoId { get; set; }
        public long Number { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long Code { get; set; }
    }
}
