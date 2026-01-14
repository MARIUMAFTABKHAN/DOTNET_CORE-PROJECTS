using ClosedXML.Excel;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ExpressDigital
{
    public partial class BillingRevenue : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var com = db.Companies.Where(x => x.Active == true).OrderBy(x => x.Company_Name).ToList();
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataSource = com;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("All", "0"));

                BindYear();
                btnSearch_Click(null, null);
            }
        }

        private void BindYear()
        {
            var thisYear = DateTime.Now.Year;
            var thisMonth = DateTime.Now.Month;
            if (thisMonth < 7)
                thisYear--;

            for (int i = thisYear; i >= 2021; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString() + "-" + (i + 1).ToString(), i.ToString()));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            //var companyId = Convert.ToInt32(ddlCompany.SelectedValue);
            int? companyId;
            var currentYear = Convert.ToInt32(ddlYear.SelectedItem.Value);
            var quaterVal = Convert.ToInt32(ddlQuater.SelectedItem.Value);
            var thisMonth = DateTime.Now.Month;
            if (thisMonth < 7)
                currentYear--;

            if (ddlCompany.SelectedIndex == 0)
                companyId = null;
            else
                companyId = Convert.ToInt32(ddlCompany.SelectedValue);

            var firstDayOfYear = new DateTime(currentYear+1, 7, 1).ToShortDateString();
            var lastDayOfYear = new DateTime(currentYear + 2, 6, 30).ToShortDateString();

            if (quaterVal == 1)
            {
                firstDayOfYear = new DateTime(currentYear+1, 7, 1).ToShortDateString();
                lastDayOfYear = new DateTime(currentYear+1, 9, 30).ToShortDateString();
            }
            if (quaterVal == 2)
            {
                firstDayOfYear = new DateTime(currentYear + 1, 10, 1).ToShortDateString();
                lastDayOfYear = new DateTime(currentYear + 1, 12, 31).ToShortDateString();
            }
            if (quaterVal == 3)
            {
                firstDayOfYear = new DateTime(currentYear + 2, 1, 1).ToShortDateString();
                lastDayOfYear = new DateTime(currentYear + 2, 3, 31).ToShortDateString();
            }
            if (quaterVal == 4)
            {
                firstDayOfYear = new DateTime(currentYear + 2, 4, 1).ToShortDateString();
                lastDayOfYear = new DateTime(currentYear + 2, 6, 30).ToShortDateString();
            }

            try
            {
                var data = db.usp_BillingRevenue(companyId, firstDayOfYear, lastDayOfYear).ToList();
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportParameter[] rp = new ReportParameter[4];
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/BillingRevenue.rdlc";
                rp[0] = new ReportParameter("pmCompanyId", ddlCompany.SelectedValue.ToString());
                rp[1] = new ReportParameter("pmFromDate", firstDayOfYear);
                rp[2] = new ReportParameter("pmToDate", lastDayOfYear);
                rp[3] = new ReportParameter("pmQuater", quaterVal.ToString());

                ReportDataSource rds = new ReportDataSource("dsBilling", data);

                ReportViewer1.LocalReport.SetParameters(rp);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            //if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "FullPostBack", "__doPostBack('', '');", true);
            //}
            //else
            //{
            //    try
            //    {
            //        string mimeType;
            //        string encoding;
            //        string fileNameExtension;
            //        string[] streams;
            //        Warning[] warnings;

            //        byte[] bytes = ReportViewer1.LocalReport.Render(
            //            "Excel", null, out mimeType, out encoding, out fileNameExtension,
            //            out streams, out warnings);

            //        using (MemoryStream memoryStream = new MemoryStream())
            //        {
            //            Response.Clear();
            //            Response.Buffer = true;
            //            Response.ContentType = mimeType;
            //            Response.AddHeader("content-disposition", "attachment; filename=BillingRevenue." + fileNameExtension);
            //            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //            Response.BinaryWrite(bytes);

            //            HttpContext.Current.Response.Flush();


            //            HttpContext.Current.ApplicationInstance.CompleteRequest();

            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "DownloadScript", "downloadFile();", true);
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        lblmessage.Text = ex.Message;
            //    }
            //}

            lblmessage.Text = string.Empty;
            //var companyId = Convert.ToInt32(ddlCompany.SelectedValue);
            int? companyId;
            var currentYear = Convert.ToInt32(ddlYear.SelectedItem.Value);
            var quaterVal = Convert.ToInt32(ddlQuater.SelectedItem.Value);
            var thisMonth = DateTime.Now.Month;
            if (thisMonth < 7)
                currentYear--;

            if (ddlCompany.SelectedIndex == 0)
                companyId = null;
            else
                companyId = Convert.ToInt32(ddlCompany.SelectedValue);

            var firstDayOfYear = new DateTime(currentYear+1, 7, 1).ToShortDateString();
            var lastDayOfYear = new DateTime(currentYear + 2, 6, 30).ToShortDateString();

            if (quaterVal == 1)
            {
                firstDayOfYear = new DateTime(currentYear+1, 7, 1).ToShortDateString();
                lastDayOfYear = new DateTime(currentYear + 1, 9, 30).ToShortDateString();
            }
            if (quaterVal == 2)
            {
                firstDayOfYear = new DateTime(currentYear + 1, 10, 1).ToShortDateString();
                lastDayOfYear = new DateTime(currentYear + 1, 12, 31).ToShortDateString();
            }
            if (quaterVal == 3)
            {
                firstDayOfYear = new DateTime(currentYear + 2, 1, 1).ToShortDateString();
                lastDayOfYear = new DateTime(currentYear + 2, 3, 31).ToShortDateString();
            }
            if (quaterVal == 4)
            {
                firstDayOfYear = new DateTime(currentYear + 2, 4, 1).ToShortDateString();
                lastDayOfYear = new DateTime(currentYear + 2, 6, 30).ToShortDateString();
            }

            try
            {
                var data = db.usp_BillingRevenue(companyId, firstDayOfYear, lastDayOfYear).ToList();
                if (data.Count > 0)
                {
                    using (XLWorkbook wbb = new XLWorkbook())
                    {
                        DataTable dt = new DataTable();
                        foreach (var item in data.First().GetType().GetProperties())
                        {
                            dt.Columns.Add(item.Name);
                        }


                        foreach (var row in data)
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
                      
                        Response.AddHeader("content-disposition", "attachment;filename=BillingRevenue.xlsx");
                        

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