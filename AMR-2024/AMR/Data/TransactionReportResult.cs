using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMR.Data
{
    public class TransactionReportResult
    {
        public int Id { get; set; }
        public string Client_Name { get; set; }
        public byte Edition_Responsible { get; set; }
        public string GroupComp_Name { get; set; }
        public int Group_Id { get; set; }
        public string Group_Name { get; set; }
        public int GrandTotal { get; set; }

    }
}