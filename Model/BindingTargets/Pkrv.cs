using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PSXDataFetchingApp.Model
{
    public class Pkrv
    {
        public long PkrvId { get; set; }
        public string Tenor { get; set; }
        public string MidRate { get; set; }
        public string Change { get; set; }
    }
}
