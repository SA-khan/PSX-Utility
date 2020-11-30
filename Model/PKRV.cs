using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PSXDataFetchingApp.Model
{
    public class PKRV
    {
        public Int64 Id { get; set; }
        public string Tenor { get; set; }
        public string MidRate { get; set; }
        public string Change { get; set; }
    }
}
