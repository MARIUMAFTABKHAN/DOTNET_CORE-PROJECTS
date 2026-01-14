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
    public partial class PrintCRVDetails : System.Web.UI.Page
    {
        static bool _isSqlTypesLoaded = false;
        DbDigitalEntities db = new DbDigitalEntities();

        public PrintCRVDetails()
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

                var sts = db.CRVStatus.ToList();
                ddlstatus.DataValueField = "ID";
                ddlstatus.DataTextField = "Status";
                ddlstatus.DataSource = sts;
                ddlstatus.DataBind();
                ddlstatus.Items.Insert(0, new ListItem("All", "0"));

                txtSearchROMODateFrom.Text = DateTime.Now.AddDays(-31).ToString("dd/MM/yyyy");
                txtSearchROMODateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                btnSearch_Click(null, null);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            Int32? AgencyId;
            Int32? ClinetId;
            Int32? companyid;
            DateTime? StartDate;
            DateTime? EnDate;
            Int32? invstatus;
            String strchk = null;
            String strCRV = null;
            Int32? cityid;
            if (ddlCity.SelectedIndex == 0)
                cityid = null;
            else
                cityid = Convert.ToInt32(ddlCity.SelectedValue);

            if (ddlstatus.SelectedIndex == 0)
                invstatus = null;
            else
                invstatus = Convert.ToInt32(ddlstatus.SelectedValue);

            if (ddlCompany.SelectedValue == "0")
                companyid = null;
            else
                companyid = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlAgency.SelectedIndex == 0)
            {
                AgencyId = null;
            }
            else
                AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);

            if (ddlClient.SelectedIndex == 0)
            {
                ClinetId = null;
            }
            else
                ClinetId = Convert.ToInt32(ddlClient.SelectedValue);
            try
            {
                if (txtSearchROMODateFrom.Text.Length == 0 || txtSearchROMODateTo.Text.Length == 0)
                {
                    StartDate = null;
                    EnDate = null;
                    string stdate = "All";
                    string enddate = "All";

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

            if (txtCrvNo.Text.Trim().Length == 0)
                strCRV = null;
            else
                strCRV = txtCrvNo.Text;

            try
            {
                var ov = db.usp_GetCRVDetailsForPrint(companyid,null,cityid,StartDate,EnDate,AgencyId, ClinetId, invstatus, strCRV).ToList();
                string myWords = "";
                ReportViewer1.LocalReport.DataSources.Clear();
                if (ddlCompany.SelectedValue == "0")
                    myWords = " All Companies ";
                
                else
                    myWords = ddlCompany.SelectedItem.Text;

                ReportParameter[] rp = new ReportParameter[4];
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/PrintCRVSummary.rdlc";
                if (txtSearchROMODateFrom.Text.Length == 0 || txtSearchROMODateTo.Text.Length == 0)
                {
                    rp[0] = new ReportParameter("pmFrom","From Date : Not Available");
                    rp[1] = new ReportParameter("pmTo", "To Date :  Not Available"); 
                }
                else
                {
                    rp[0] = new ReportParameter("pmFrom", "From Date :" + StartDate.Value.ToString("dd-MM-yyyy"));
                    rp[1] = new ReportParameter("pmTo", "To Date : " + EnDate.Value.ToString("dd-MM-yyyy"));
                }

                rp[2] = new ReportParameter("pmCompany", myWords);
                rp[3] = new ReportParameter("pmCompanyID", ddlCompany.SelectedValue.ToString());

               
                ReportDataSource rds = new ReportDataSource("DSCRVDetails", ov);
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
            lblmessage.Text = string.Empty;
            Int32? AgencyId;
            Int32? ClinetId;
            Int32? companyid;
            DateTime? StartDate;
            DateTime? EnDate;
            Int32? invstatus;
            String strchk = null;
            String strCRV = null;
            Int32? cityid;
            if (ddlCity.SelectedIndex == 0)
                cityid = null;
            else
                cityid = Convert.ToInt32(ddlCity.SelectedValue);

            if (ddlstatus.SelectedIndex == 0)
                invstatus = null;
            else
                invstatus = Convert.ToInt32(ddlstatus.SelectedValue);

            if (ddlCompany.SelectedValue == "0")
                companyid = null;
            else
                companyid = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlAgency.SelectedIndex == 0)
            {
                AgencyId = null;
            }
            else
                AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);

            if (ddlClient.SelectedIndex == 0)
            {
                ClinetId = null;
            }
            else
                ClinetId = Convert.ToInt32(ddlClient.SelectedValue);
            try
            {
                if (txtSearchROMODateFrom.Text.Length == 0 || txtSearchROMODateTo.Text.Length == 0)
                {
                    StartDate = null;
                    EnDate = null;
                    string stdate = "All";
                    string enddate = "All";

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

            if (txtCrvNo.Text.Trim().Length == 0)
                strCRV = null;
            else
                strCRV = txtCrvNo.Text;

            try
            {
                var ov = db.usp_GetCRVDetailsForPrint(companyid, null, cityid, StartDate, EnDate, AgencyId, ClinetId, invstatus, strCRV).ToList();
                if (ov.Count > 0)
                {
                    using (XLWorkbook wbb = new XLWorkbook())
                    {
                        DataTable dt = new DataTable();
                        foreach (var item in ov.First().GetType().GetProperties())
                        {
                            dt.Columns.Add(item.Name);
                        }

                        dt.Columns.Add("Consumed", typeof(decimal));
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
                            datarow["Consumed"] = crvAmount + wht + gst;

                            dt.Rows.Add(datarow);
                        }

                        dt.Columns.Remove("agencyID");
                        dt.Columns.Remove("AgencyName");
                        dt.Columns.Remove("CityID");
                        dt.Columns.Remove("CityName");
                        dt.Columns.Remove("Company_Name");
                        dt.Columns.Remove("CRVId");
                        dt.Columns.Remove("PaymentNumber");
                        dt.Columns.Remove("CRVDate");
                        dt.Columns.Remove("CRVClearedDate");
                        dt.Columns.Remove("ChequeDate");
                        dt.Columns.Remove("IsChallanRecieved");
                        dt.Columns.Remove("ChallanDate");
                        dt.Columns.Remove("CreatedDate");
                        dt.Columns.Remove("CompanyId");
                        dt.Columns.Remove("ClientId");
                        dt.Columns.Remove("IsCRVFullyConsumed");
                        dt.Columns.Remove("Client");
                        dt.Columns.Remove("crvstatus");
                        dt.Columns.Remove("NetReceiable");

                        wbb.Worksheets.Add(dt, "New");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                        Response.AddHeader("content-disposition", "attachment;filename=PrintCRVSummary.xlsx");


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