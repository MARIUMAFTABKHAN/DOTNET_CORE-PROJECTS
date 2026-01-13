using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMR
{
    [Serializable]
    public class RevenueReportResult
    {
        public int Publication { get; set; }
        public string Pub_Abreviation { get; set; }
        public DateTime Publication_Date { get; set; }
        public int Client_Company { get; set; }
        public string Client_Name { get; set; }
        public string Address_Line_4 { get; set; }
        public int MainCategory { get; set; }
        public string MainCategory_Title { get; set; }
        public string Sub_Category { get; set; }
        public string SubCategory_Title { get; set; }
        public int Size_CM { get; set; }
        public int Col_Size { get; set; }
        public int CM { get; set; }
        public string City_Editions { get; set; }
        public string Colour_BW { get; set; }
        public string RO { get; set; }
        public string Page { get; set; }
        public bool cExport { get; set; }
        public int Orignal_ID { get; set; }
        public string Type { get; set; }
        public int Brand { get; set; }
        public string Brand_Name { get; set; }
        public decimal? RateAmount { get; set; }
    }
}