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
    public partial class ContactReportView : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            filterquery();
        }
        private void filterquery()
        {
            ViewState["SearchResults"] = true;

            string user = Request.Cookies["UserId"]?.Value;

            DateTime? fromDate = string.IsNullOrEmpty(txtfromdate.Value)
                    ? (DateTime?)null
                    : DateTime.Parse(txtfromdate.Value);

            DateTime? toDate = string.IsNullOrEmpty(txttodate.Value)
                ? (DateTime?)null
                : DateTime.Parse(txttodate.Value);

            string visitingEmployee = txtemp.Text.Trim();

            using (var db = new Model1Container())
            {
                string query = @"SELECT 
                                cr.id,
                                main_cat as maincat,
                                mc.Category_Title AS Main_Category,
                                sub_cat,
                                sc.Category_Title AS Sub_Category,
                                brand_id,
                                br.Brand_Name,
                                visit_to,
                                agency_client_id,
                                CASE 
                                    WHEN cr.visit_to = 'A' THEN ag.Agency_Name
                                    WHEN cr.visit_to = 'C' THEN cc.Client_Name
                                    ELSE NULL
                                END AS VisitPartyName,
                                visit_date,
                                ISNULL(crd.Latitude, 0) AS Latitude,
                                ISNULL(crd.Longitude, 0) AS Longitude,
                                followup_visit_date as NextFollowUp_Date,
	                            cr.Rec_Added_by,u.User_Name,cr.minutes_of_meetings
                            FROM contactreport_mobile cr 
                            LEFT JOIN Contactreportmobile_Detail crd ON cr.id = crd.contactreport
                            LEFT JOIN MainCategories mc ON cr.main_cat = mc.Id
                            LEFT JOIN SubCategories sc ON cr.sub_cat = sc.Id
                            LEFT JOIN Brands br ON cr.brand_id = br.Id
                            LEFT JOIN Agencies ag ON cr.agency_client_id = ag.Id
                            LEFT JOIN ClientCompanies cc ON cr.agency_client_id = cc.Id
                            inner JOIN Users u ON cr.Rec_Added_by = u.User_Id
                             WHERE 1 = 1";


                var parameters = new List<System.Data.SqlClient.SqlParameter>();
                

                if (!(user == "SAL" || user == "ALIMRAN" || user == "TUBA.NASEER" || user == "admin"))
                {
                    if (user == "KAMRANK")
                    {
                        query += " AND cr.Rec_Added_by IN ('KAMRANK','ZOHAIB')";
                    }
                    else
                    {
                        query += " AND cr.Rec_Added_by = @user";
                        parameters.Add(new System.Data.SqlClient.SqlParameter("@user", user));
                    }
                }

                // 🔹 Date From
                if (fromDate.HasValue)
                {
                    query += " AND cr.visit_date >= @fromDate";
                    parameters.Add(new System.Data.SqlClient.SqlParameter("@fromDate", fromDate.Value));
                }

                // 🔹 Date To
                if (toDate.HasValue)
                {
                    query += " AND cr.visit_date <= @toDate";
                    parameters.Add(new System.Data.SqlClient.SqlParameter("@toDate", toDate.Value));
                }

                // 🔹 Visiting Employee (name search)
                if (!string.IsNullOrEmpty(visitingEmployee))
                {
                    query += " AND u.User_Name LIKE @emp";
                    parameters.Add(new System.Data.SqlClient.SqlParameter("@emp", "%" + visitingEmployee + "%"));
                }

                query += " ORDER BY cr.visit_date DESC";

                var result = db.Database.SqlQuery<ContactModel>(
                    query, parameters.ToArray()).ToList();

                gv.DataSource = result;
                gv.DataBind();

                ViewState["SearchResults"] = result;

            }


        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            gv.PageIndex = 0; // reset paging
            filterquery();
        }


        [Serializable]
        public class ContactModel
        {
            public int Id { get; set; }
            public int? MainCat { get; set; }
            public string Main_Category { get; set; }
            public int? Sub_Cat { get; set; }
            public string Sub_Category { get; set; }
            public int? Brand_Id { get; set; }
            public string Brand_Name { get; set; }
            public string Visit_To { get; set; }
            public int? Agency_Client_Id { get; set; }
            public string VisitPartyName { get; set; }
            public DateTime Visit_Date { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public DateTime? NextFollowUp_Date { get; set; }
            public string Rec_Added_by { get; set; }
            public string User_Name { get; set; }
            public string minutes_of_meetings { get; set; }
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            var list = ViewState["SearchResults"] as List<ContactModel>;

            // DataTable dt = ViewState["SearchResults"] as DataTable;

            if (list != null && list.Any())
            {
                DataTable dt = ToDataTable(list); // use same ToDataTable<T> helper as abov

                using (XLWorkbook workbook = new XLWorkbook())
                {
                    // Add the DataTable to the worksheet
                    var worksheet = workbook.Worksheets.Add("ContactReport");
                    worksheet.Cell(1, 1).InsertTable(dt);
                    worksheet.Columns().AdjustToContents();

                    // Send the Excel file to the browser
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ContactReport.xlsx");

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        workbook.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.Close();
                        //Response.End();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
            }
            else
            {
                //Response.Write("<script>alert('No data available to export');</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No data available to export');", true);

            }
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
            //gv.PageIndex = e.NewPageIndex;
            //filterquery();

            gv.PageIndex = e.NewPageIndex;
            gv.DataSource = ViewState["SearchResults"];
            gv.DataBind();
        }

    }
}