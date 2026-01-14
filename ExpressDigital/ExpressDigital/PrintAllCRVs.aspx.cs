using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Collections;
using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing;
using System.IO;
using ClosedXML.Excel;

namespace ExpressDigital
{
    public partial class PrintAllCRVs : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        static bool _isSqlTypesLoaded = false;

        public PrintAllCRVs()
        {
            if (!_isSqlTypesLoaded)
            {
                SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~"));
                _isSqlTypesLoaded = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime? StartDate;
                DateTime? EnDate;
                StartDate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Month, 1);
                EnDate = StartDate.Value.AddMonths(1).AddDays(-1);

                txtSearchROMODateFrom.Text = StartDate.Value.ToString("dd/MM/yyyy");
                txtSearchROMODateTo.Text = EnDate.Value.ToString("dd/MM/yyyy");

                var city = db.CityManagements.Where(x => x.IsActive == true).OrderBy(x => x.CityName).ToList();
                ddlCity.DataValueField = "ID";
                ddlCity.DataTextField = "CityName";
                ddlCity.DataSource = city;
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("All", "0"));
                ddlCity_SelectedIndexChanged(null, null);


                var com = db.Companies.Where(x => x.Active == true).OrderBy(x => x.Company_Name).ToList();
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataSource = com;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("All", "0"));
                ddlCity_SelectedIndexChanged(null, null);

                var sts = db.CRVStatus.ToList();
                ddlstatus.DataValueField = "ID";
                ddlstatus.DataTextField = "Status";
                ddlstatus.DataSource = sts;
                ddlstatus.DataBind();
                ddlstatus.Items.Insert(0, new ListItem("All", "0"));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            int? cityId;
            int? invStatus;
            int? companyId;
            int? agencyId;
            int? clinetId;

            string strchk;
            string strCRV;
            
            DateTime? StartDate;
            DateTime? EnDate;

            
            if (ddlCity.SelectedIndex == 0)
                cityId = null;
            else
                cityId = Convert.ToInt32(ddlCity.SelectedValue);
            
            if (ddlstatus.SelectedIndex == 0)
                invStatus = null;
            else
                invStatus = Convert.ToInt32(ddlstatus.SelectedValue);

            if (ddlCompany.SelectedValue == "0")
                companyId = null;
            else
                companyId = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlAgency.SelectedIndex == 0)
                agencyId = null;
            else
                agencyId = Convert.ToInt32(ddlAgency.SelectedValue);

            if (ddlClient.SelectedIndex == 0)
                clinetId = null;
            else
                clinetId = Convert.ToInt32(ddlClient.SelectedValue);
            try
            {
                if (txtSearchROMODateFrom.Text.Length == 0 || txtSearchROMODateTo.Text.Length == 0)
                {
                    StartDate = null;
                    EnDate = null;
                }
                else
                {
                    StartDate = Helper.SetDateFormat(txtSearchROMODateFrom.Text);
                    EnDate = Helper.SetDateFormat(txtSearchROMODateTo.Text);
                }
            }

            catch (Exception)
            {
                StartDate = null;
                EnDate = null;
            }
            
            if (txtChkNo.Text.Trim().Length == 0)
                strchk = null;
            else
                strchk = txtChkNo.Text;

            try
            {
                if (txtCrvNo.Text.Trim().Length == 0)
                    strCRV = null;
                else
                    strCRV = txtCrvNo.Text;
            }
            catch (Exception)
            {
                strCRV = null;
            }

            try
            {
                var ov = db.usp_GetAllCRVTaxChallan_F2(null, strCRV, agencyId, clinetId, null, null, strchk, StartDate, null, null, null, null, invStatus, null, null, null, EnDate, cityId, null, null, companyId).ToList();
                string myWords = "";

                ReportViewer1.LocalReport.DataSources.Clear();
                if (ddlCompany.SelectedValue == "0")
                    myWords = " ( All Companies )";
                else
                    myWords = " (" + ddlCompany.SelectedItem.Text + " )";

                ReportParameter[] rp = new ReportParameter[4];

                if (ddlCompany.SelectedValue.ToString() == "9")
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/CRVAll-Digital-New2.rdlc";
                else
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/CRVAll-Express-New2.rdlc";

                rp[0] = new ReportParameter("pmFrom", "From: " + txtSearchROMODateFrom.Text);
                rp[1] = new ReportParameter("pmTo", "To: " + txtSearchROMODateTo.Text);
                rp[2] = new ReportParameter("pmCompany", myWords);
                rp[3] = new ReportParameter("pmCompanyID", ddlCompany.SelectedValue.ToString());

                ReportDataSource rds = new ReportDataSource("DSCRV", ov);
                ReportViewer1.LocalReport.SetParameters(rp);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Agencyid = Convert.ToInt32(ddlAgency.SelectedValue);
            var c = db.Clients.Where(x => x.AgencyID == Agencyid).ToList().OrderBy(x => x.Client1);
            ddlClient.DataValueField = "ID";
            ddlClient.DataTextField = "Client1";
            ddlClient.DataSource = c;
            ddlClient.DataBind();
            ddlClient.Items.Insert(0, new ListItem("Select Client", "0"));
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlCity.SelectedValue);
            var g = db.GroupAgencies.Where(x => x.CityID == id).OrderBy(x => x.GroupName).ToList();
            ddlmasteragency.DataValueField = "ID";
            ddlmasteragency.DataTextField = "GroupName";
            ddlmasteragency.DataSource = g;
            ddlmasteragency.DataBind();
            ddlmasteragency.Items.Insert(0, new ListItem("Select Agency", "0"));
            ddlmasteragency_SelectedIndexChanged(null, null);
        }

        protected void ddlmasteragency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlmasteragency.SelectedValue);
            var g = db.Agencies.Where(x => x.GroupID == id).OrderBy(x => x.AgencyName).ToList();
            ddlAgency.DataValueField = "ID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = g;
            ddlAgency.DataBind();
            ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
            ddlAgency_SelectedIndexChanged(null, null);
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {

            //if(ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
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
            //            Response.AddHeader("content-disposition", "attachment; filename=PrintAllCRVs." + fileNameExtension);
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
            int? cityId;
            int? invStatus;
            int? companyId;
            int? agencyId;
            int? clinetId;

            string strchk;
            string strCRV;

            DateTime? StartDate;
            DateTime? EnDate;


            if (ddlCity.SelectedIndex == 0)
                cityId = null;
            else
                cityId = Convert.ToInt32(ddlCity.SelectedValue);

            if (ddlstatus.SelectedIndex == 0)
                invStatus = null;
            else
                invStatus = Convert.ToInt32(ddlstatus.SelectedValue);

            if (ddlCompany.SelectedValue == "0")
                companyId = null;
            else
                companyId = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlAgency.SelectedIndex == 0)
                agencyId = null;
            else
                agencyId = Convert.ToInt32(ddlAgency.SelectedValue);

            if (ddlClient.SelectedIndex == 0)
                clinetId = null;
            else
                clinetId = Convert.ToInt32(ddlClient.SelectedValue);
            try
            {
                if (txtSearchROMODateFrom.Text.Length == 0 || txtSearchROMODateTo.Text.Length == 0)
                {
                    StartDate = null;
                    EnDate = null;
                }
                else
                {
                    StartDate = Helper.SetDateFormat(txtSearchROMODateFrom.Text);
                    EnDate = Helper.SetDateFormat(txtSearchROMODateTo.Text);
                }
            }

            catch (Exception)
            {
                StartDate = null;
                EnDate = null;
            }

            if (txtChkNo.Text.Trim().Length == 0)
                strchk = null;
            else
                strchk = txtChkNo.Text;

            try
            {
                if (txtCrvNo.Text.Trim().Length == 0)
                    strCRV = null;
                else
                    strCRV = txtCrvNo.Text;
            }
            catch (Exception)
            {
                strCRV = null;
            }

            try
            {
                var ov = db.usp_GetAllCRVTaxChallan_F2(null, strCRV, agencyId, clinetId, null, null, strchk, StartDate, null, null, null, null, invStatus, null, null, null, EnDate, cityId, null, null, companyId).ToList();
                if (ov.Count > 0)
                {
                    using (XLWorkbook wbb= new XLWorkbook())
                    {
                        DataTable dt = new DataTable();
                        foreach (var item in ov.First().GetType().GetProperties())
                        {
                            dt.Columns.Add(item.Name);
                        }

                        dt.Columns.Add("ClientAmount", typeof(decimal));
                        dt.Columns.Add("IsCRVFullyConsumedText", typeof(string));

                        foreach (var row in ov)
                        {
                            var datarow = dt.NewRow();
                            foreach (var prop in row.GetType().GetProperties())
                            {
                                datarow[prop.Name] = prop.GetValue(row, null);
                            }
                            decimal crvAmount = Convert.ToDecimal(row.GetType().GetProperty("CRVAmount").GetValue(row, null));
                            decimal wht = Convert.ToDecimal(row.GetType().GetProperty("WithHoldingTax").GetValue(row, null));
                            decimal gst = Convert.ToDecimal(row.GetType().GetProperty("GST").GetValue(row, null));
                            datarow["ClientAmount"] = crvAmount + wht +gst;

                            bool isCRVFullyConsumed = Convert.ToBoolean(row.GetType().GetProperty("IsCRVFullyConsumed").GetValue(row, null));
                            datarow["IsCRVFullyConsumedText"] = isCRVFullyConsumed ? "Yes" : "No";

                            dt.Rows.Add(datarow);
                        }

                        dt.Columns.Remove("CRVId");
                        dt.Columns.Remove("CompanyId");
                        dt.Columns.Remove("ChannelId");
                        dt.Columns.Remove("Channel");
                        dt.Columns.Remove("AgencyId");
                        dt.Columns.Remove("Branch");
                        dt.Columns.Remove("PaymentModeId");
                        dt.Columns.Remove("PaymentTypeId");
                        //dt.Columns.Remove("PaymentNumber");
                        dt.Columns.Remove("CurrencyId");
                        dt.Columns.Remove("IsChallanRecieved");
                        dt.Columns.Remove("ChallanDate");
                        dt.Columns.Remove("ShiftInCRV");
                        dt.Columns.Remove("ShiftAmountInCRV");
                        dt.Columns.Remove("CRVTest1");
                        dt.Columns.Remove("CRVTest2");
                        dt.Columns.Remove("CRVTest3");
                        dt.Columns.Remove("CRVDetailId");
                        dt.Columns.Remove("ClientId");
                        dt.Columns.Remove("CRVDetailAmount");
                        dt.Columns.Remove("ConsumedAmount");
                        dt.Columns.Remove("IsCRVFullyConsumed");
                        dt.Columns.Remove("ShiftCRVDetailId");
                        dt.Columns.Remove("ADN");
                        dt.Columns.Remove("IsShifted");
                        dt.Columns.Remove("test2");
                        dt.Columns.Remove("test3");
                        dt.Columns.Remove("CreatedDate");
                        dt.Columns.Remove("ID");
                        dt.Columns.Remove("Currency");

                        wbb.Worksheets.Add(dt, "New");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        if (ddlCompany.SelectedValue.ToString() == "9")
                        {
                            Response.AddHeader("content-disposition", "attachment;filename=CRVAll-Digital.xlsx");
                        }
                        else
                        {
                            Response.AddHeader("content-disposition", "attachment;filename=CRVAll-Express.xlsx");
                        }
                           
                        using (MemoryStream mymemoryStream=new MemoryStream())
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

        private string GetPropertyName(int index)
        {
            var properties = typeof(usp_GetAllCRVTaxChallan_F2_Result).GetProperties();
            return properties[index].Name;
        }

        // Helper method to get column value based on property name
        private object GetColumnValue(usp_GetAllCRVTaxChallan_F2_Result item, int index)
        {
            var properties = typeof(usp_GetAllCRVTaxChallan_F2_Result).GetProperties();
            return properties[index].GetValue(item, null);
        }
    }
}