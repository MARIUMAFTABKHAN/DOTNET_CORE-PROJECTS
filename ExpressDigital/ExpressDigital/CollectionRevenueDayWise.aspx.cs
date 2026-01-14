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

namespace ExpressDigital
{
    public partial class CollectionRevenueDayWise : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtDateFrom.Text = DateTime.Now.AddDays(-31).ToString("dd/MM/yyyy");
                txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                btnSearch_Click(null, null);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            var companyId = 0;
            var dateFrom = Convert.ToDateTime(Helper.SetDateFormatString(txtDateFrom.Text));
            var dateTo = Convert.ToDateTime(Helper.SetDateFormatString(txtDateTo.Text)).AddDays(1);

            try
            {
                var data = db.usp_CollectionRevenueDateWise(companyId, dateFrom.ToShortDateString(), dateTo.ToShortDateString()).ToList();
                ReportViewer1.LocalReport.DataSources.Clear();
                dateTo=dateTo.AddDays(-1);
                ReportParameter[] rp = new ReportParameter[3];
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/CollectionRevenueDateWise.rdlc";
                rp[0] = new ReportParameter("pmCompanyId", "0");
                rp[1] = new ReportParameter("pmFromDate", dateFrom.ToShortDateString());
                rp[2] = new ReportParameter("pmToDate", dateTo.ToShortDateString());

                ReportDataSource rds = new ReportDataSource("dsBillingByDate", data);

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
            lblmessage.Text = string.Empty;
            var companyId = 0;
            var dateFrom = Convert.ToDateTime(Helper.SetDateFormatString(txtDateFrom.Text));
            var dateTo = Convert.ToDateTime(Helper.SetDateFormatString(txtDateTo.Text)).AddDays(1);

            try
            {
                var data = db.usp_CollectionRevenueDateWise(companyId, dateFrom.ToShortDateString(), dateTo.ToShortDateString()).ToList();
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

                        Response.AddHeader("content-disposition", "attachment;filename=CollectionRevenueDateWise.xlsx");


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