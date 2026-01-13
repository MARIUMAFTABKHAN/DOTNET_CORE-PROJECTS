using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AMR.AdEntry;

namespace AMR
{
    public partial class ContactReportVisit : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadContactReport();
        }
        private void LoadContactReport()
        {
            string user = Request.Cookies["UserId"]?.Value;

            if (string.IsNullOrEmpty(user))
                return;

            using (var db = new Model1Container())
            {
                var param = new System.Data.SqlClient.SqlParameter("@User_Id", user);

                var result = db.Database
                    .SqlQuery<ContactModel>(
                        "EXEC contactreport_visit @User_Id",
                        param)
                    .ToList();

                gv.DataSource = result;
                gv.DataBind();

                ViewState["SearchResults"] = result;
            }
        }

        

        [Serializable]
        public class ContactModel
        {
            public int Id { get; set; }
            public int? MainCat { get; set; }
            public string Main_Category { get; set; }
            public int? Sub_Cat { get; set; }
            public string Sub_Category { get; set; }
            public int? Agency_Client_Id { get; set; }
            public string VisitPartyName { get; set; }
            public int? Brand_Id { get; set; }
            public string Brand_Name { get; set; }
            public DateTime Visit_Date { get; set; }
            public DateTime? Followup_Visit_Date { get; set; }
            public string User_Name { get; set; }
        }

        

        // Utility method to convert list to DataTable
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties()
                         .Where(p => p.Name!= "Id" && p.Name != "MainCat" && p.Name != "Sub_Cat"
                         && p.Name != "Brand_Id" && p.Name != "Agency_Client_Id" && p.Name != "User_Name"
                         && p.Name != "Latitude" && p.Name != "Longitude")
                         .ToArray();

            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            gv.DataSource = ViewState["SearchResults"];
            gv.DataBind();
        }

    }
}