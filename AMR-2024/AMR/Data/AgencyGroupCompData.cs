using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMR.Data
{
    public class AgencyGroupCompData
    {
        public int Id { get; set; }
        public string Agency_Name { get; set; }
        public string Agency_Type { get; set; }
        public string Accredited_Status { get; set; }
       // public byte Edition_Responsible { get; set; }
        public string GroupComp_Name { get; set; }
        public string Status { get; set; }
        public string _Status { get; set; }
        public string _AMR { get; set; }
        public string _cExport { get; set; }
    }
}