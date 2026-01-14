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
    public partial class PrintDayBook : System.Web.UI.Page
    {
        static bool _isSqlTypesLoaded = false;

        DbDigitalEntities db = new DbDigitalEntities();
        public PrintDayBook()
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
                var com = db.Companies.Where(x => x.Active == true).OrderBy(x => x.Company_Name).ToList();
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataSource = com;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("All", "0"));


                var city = db.CityManagements.Where(x => x.IsActive == true).OrderBy(x => x.CityName).ToList();
                ddlcity.DataValueField = "ID";
                ddlcity.DataTextField = "CityName";
                ddlcity.DataSource = city;
                ddlcity.DataBind();
                ddlcity.Items.Insert(0, new ListItem("All ", "0"));

                ddlcity_SelectedIndexChanged(null, null);

                txtSearchROMODateFrom.Text = DateTime.Now.AddDays(-31).ToString("dd/MM/yyyy");
                txtSearchROMODateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                btnSearch_Click(null, null);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;

            Int32? agencyId;
            Int32? clinetId;
            Int32? companyid;
            Int32? cityid;

            String StartDate;
            String EnDate;

            if (ddlcity.SelectedIndex == 0)
                cityid = null;
            else
                cityid = Convert.ToInt32(ddlcity.SelectedValue);

            if (ddlCompany.SelectedIndex == 0)
                companyid = null;
            else
                companyid = Convert.ToInt32(ddlCompany.SelectedValue);

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
                    StartDate = Helper.SetDateFormat(txtSearchROMODateFrom.Text).ToString("MM/dd/yyyy");
                    EnDate = Helper.SetDateFormat(txtSearchROMODateTo.Text).ToString("MM/dd/yyyy");
                }
            }
            catch (Exception)
            {
                StartDate = null;
                EnDate = null;
            }

            try
            {
                Int32? invstatus = 0;
                if (ddlstatis.SelectedIndex == 0)
                    invstatus = 0;
                else if (ddlstatis.SelectedIndex == 1)
                    invstatus = 1;
                else
                    invstatus = 2;

                var s = db.usp_GetDayBookForPrint_new(StartDate, EnDate, companyid, cityid, agencyId, clinetId, invstatus).ToList();
                string myWords = "";
                ReportViewer1.LocalReport.DataSources.Clear();
                myWords = ddlCompany.SelectedItem.Text;

                string daybook = txtSearchROMODateFrom.Text + " To " + txtSearchROMODateTo.Text;
                ReportParameter[] rp = new ReportParameter[6];
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/DSDayBook.rdlc";
                rp[0] = new ReportParameter("pmCity", "City: " + ddlcity.SelectedItem.Text);
                rp[1] = new ReportParameter("pmAgency", "Agency: " + ddlAgency.SelectedItem.Text);
                rp[2] = new ReportParameter("pmClient", "Client: " + ddlClient.SelectedItem.Text);
                rp[3] = new ReportParameter("pmStatus", "Status:" + " 1");
                rp[4] = new ReportParameter("pmFromToDate", "Date:" + daybook);
                rp[5] = new ReportParameter("pmCompanyId", ddlCompany.SelectedValue.ToString());

                ReportDataSource rds = new ReportDataSource("DSDaybook", s);

                ReportViewer1.LocalReport.SetParameters(rp);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }
        }

        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Agencyid = Convert.ToInt32(ddlAgency.SelectedValue);
            var c = db.Clients.Where(x => x.AgencyID == Agencyid).ToList().OrderBy(x => x.Client1);
            ddlClient.DataValueField = "ID";
            ddlClient.DataTextField = "Client1";
            ddlClient.DataSource = c;
            ddlClient.DataBind();
            ddlClient.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlcity.SelectedValue);
            var g = db.Agencies.Where(x => x.CityID == id).OrderBy(x => x.AgencyName).ToList();
            ddlAgency.DataValueField = "ID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = g;
            ddlAgency.DataBind();
            ddlAgency.Items.Insert(0, new ListItem("All", "0"));
            ddlAgency_SelectedIndexChanged(null, null);
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {

            lblmessage.Text = string.Empty;

            Int32? agencyId;
            Int32? clinetId;
            Int32? companyid;
            Int32? cityid;

            String StartDate;
            String EnDate;

            if (ddlcity.SelectedIndex == 0)
                cityid = null;
            else
                cityid = Convert.ToInt32(ddlcity.SelectedValue);

            if (ddlCompany.SelectedIndex == 0)
                companyid = null;
            else
                companyid = Convert.ToInt32(ddlCompany.SelectedValue);

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
                    StartDate = Helper.SetDateFormat(txtSearchROMODateFrom.Text).ToString("MM/dd/yyyy");
                    EnDate = Helper.SetDateFormat(txtSearchROMODateTo.Text).ToString("MM/dd/yyyy");
                }
            }
            catch (Exception)
            {
                StartDate = null;
                EnDate = null;
            }
            try
            {
                Int32? invstatus = 0;
                if (ddlstatis.SelectedIndex == 0)
                    invstatus = 0;
                else if (ddlstatis.SelectedIndex == 1)
                    invstatus = 1;
                else
                    invstatus = 2;

                var s = db.usp_GetDayBookForPrint_new(StartDate, EnDate, companyid, cityid, agencyId, clinetId, invstatus).ToList();
                if (s.Count > 0)
                {
                    using (XLWorkbook wbb = new XLWorkbook())
                    {
                        DataTable dt = new DataTable();
                       // string[] columnnames = { "InvoiceDate", "Company_Name", "InvoiceNo", "ReleaseOrderNumber", "Currency", "AgencyName", "Client", "INV_AGC" }
                        foreach (var item in s.First().GetType().GetProperties())
                        {
                            dt.Columns.Add(item.Name);
                        }

                        dt.Columns.Add("Reveue", typeof(decimal));

                        foreach (var row in s)
                        {
                            var datarow = dt.NewRow();
                            foreach (var prop in row.GetType().GetProperties())
                            {
                                datarow[prop.Name] = prop.GetValue(row, null);
                            }
                            decimal gross = Convert.ToDecimal(row.GetType().GetProperty("INV_Gross").GetValue(row, null));
                            decimal agc = Convert.ToDecimal(row.GetType().GetProperty("INV_AGC").GetValue(row, null));
                            datarow["Reveue"] = gross - agc;

                            dt.Rows.Add(datarow);
                        }

                        dt.Columns.Remove("ID");
                        dt.Columns.Remove("ReleaseOrderDate");
                        dt.Columns.Remove("CompnayID");
                        //dt.Columns.Remove("Company_Name");
                       // dt.Columns.Remove("Client");
                        dt.Columns.Remove("TransmissionMonth");
                        dt.Columns.Remove("Cam_startDate");
                        dt.Columns.Remove("Cam_endDate");
                        dt.Columns.Remove("ROMPDate");
                        dt.Columns.Remove("FOCPAID");
                        //dt.Columns.Remove("INV_GST");
                        dt.Columns.Remove("INV_Discount");
                        dt.Columns.Remove("IsBilled");
                        dt.Columns.Remove("IsCancelled");
                        dt.Columns.Remove("CityID");
                        dt.Columns.Remove("HeadOfficeCityID");
                        dt.Columns.Remove("ClientID");
                        dt.Columns.Remove("AgencyID");
                        dt.Columns.Remove("InvoiceReference");
                        dt.Columns.Remove("VrNumber");
                        dt.Columns.Remove("CityName");
                        dt.Columns.Remove("RekeaseOrderCreationDate");
                        dt.Columns.Remove("IsInternational");

                        wbb.Worksheets.Add(dt, "New");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=DSDayBook.xlsx");
                        

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