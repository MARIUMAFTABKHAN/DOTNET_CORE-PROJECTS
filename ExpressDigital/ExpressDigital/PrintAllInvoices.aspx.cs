using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class PrintAllInvoices : System.Web.UI.Page
    {
        static bool _isSqlTypesLoaded = false;

        DbDigitalEntities db = new DbDigitalEntities();
        // GLEntities db2 = new GLEntities();
      
        public PrintAllInvoices()
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

                //if (ddlInvoiceStatus.SelectedValue.ToString() != "2")
                //{
                //    // btnExecute.Enabled = false;
                //}
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            Int32? AgencyId;
            Int32? ClinetId;
            Int32? companyid;
          
            Int32? invstatus = null;
            String strchk = null;
            string  strCRV = null;
            Int32? cityid;
            String strpayment = null;
            DateTime?   StartDate;
            DateTime?  EnDate;


            if (ddlCity.SelectedIndex == 0)
                cityid = null;
            else
                cityid = Convert.ToInt32(ddlCity.SelectedValue);

            if (ddlstatus.SelectedIndex == 0)
                invstatus = null;
            else
                invstatus = Convert.ToInt32(ddlstatus.SelectedValue);

            if (ddlCompany.SelectedIndex == 0)
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
                }
                else
                {
                    StartDate =  Helper.SetDateFormat (txtSearchROMODateFrom.Text);
                    EnDate = Helper.SetDateFormat (txtSearchROMODateTo.Text);//lblmessage//.AddHours(12);
                }
            }
            catch (Exception)
            {
                StartDate = null;
                EnDate = null;
            }

          



            try
            {
                  var ov = db.usp_GetAllCRVTaxChallan_F(null,strCRV, AgencyId, ClinetId, null, null, strchk,StartDate,null,null,null,null,invstatus, null,null,null,EnDate,cityid,null,null).ToList();

                //var ov = db.usp_GetCRVDetailsForPrint_2(companyid, strCRV, cityid, StartDate, EnDate, AgencyId, ClinetId, invstatus, strchk).ToList();
                string myWords = "";

                
                ReportViewer1.LocalReport.DataSources.Clear();
                if (ddlCompany.SelectedValue == "0")
                {
                    myWords = " ( All Companies )";
                }

                else
                    myWords = " ("+ ddlCompany.SelectedItem.Text +" )";
              

                // if (ddlCompany.SelectedValue == "1")
                //     ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/ExpressPrintLedger.rdlc";
                // else

                // string daybook = txtSearchROMODateFrom.Text + " To " + txtSearchROMODateTo.Text;
                ReportParameter[] rp = new ReportParameter[4];

                if (ddlCompany.SelectedValue.ToString() == "9")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/CRVAll-Digital.rdlc";
                    //  ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/PrintALLCRVDetails.rdlc";
                }
                else
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/CRVAll-Express.rdlc";
                    //  ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/PrintALLCRVDetails_dig.rdlc";

                }
                rp[0] = new ReportParameter("pmFrom", "From: " + txtSearchROMODateFrom.Text);
                rp[1] = new ReportParameter("pmTo", "To: " + txtSearchROMODateTo.Text) ;
                rp[2] = new ReportParameter("pmCompany",  myWords);
                rp[3] = new ReportParameter("pmCompanyID", ddlCompany.SelectedValue.ToString());
               // rp[3] = new ReportParameter("pmCompanyID", ddlCompany.SelectedValue.ToString())
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
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
    }
}