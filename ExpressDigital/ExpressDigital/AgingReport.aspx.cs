using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using DataSet = System.Data.DataSet;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Runtime.InteropServices.ComTypes;
using ClosedXML.Excel;

namespace ExpressDigital
{
    public partial class AgingReport : System.Web.UI.Page
    {

        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txttilldate.Text = DateTime.Now.ToString();
            }
            
        }

        protected void btnShowData_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;

            DateTime? tilldate=null;
            try
            {
                if (!string.IsNullOrEmpty(txttilldate.Text))
                {
                    tilldate = Helper.SetDateFormat(txttilldate.Text);
                }
            }
            catch (Exception)
            {
                tilldate = null;
            }
            try
            {
                var s = db.Rpt_Aging_Final(tilldate).ToList();
                if (s.Count > 0)
                {
                    using (XLWorkbook wbb = new XLWorkbook())
                    {
                        DataTable dt = new DataTable();
                        foreach (var item in s.First().GetType().GetProperties())
                        {
                            dt.Columns.Add(item.Name);
                        }

                        foreach (var row in s)
                        {
                            var datarow = dt.NewRow();
                            foreach (var prop in row.GetType().GetProperties())
                            {
                                datarow[prop.Name] = prop.GetValue(row, null);
                            }

                            dt.Rows.Add(datarow);
                        }
                        wbb.Worksheets.Add(dt, "New");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=AgingReport.xlsx");

                        using (MemoryStream mymemoryStream = new MemoryStream())
                        {
                            wbb.SaveAs(mymemoryStream);
                            mymemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }

        }
    }
}