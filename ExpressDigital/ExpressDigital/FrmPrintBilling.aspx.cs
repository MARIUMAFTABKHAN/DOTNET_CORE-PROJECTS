using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class FrmPrintBilling : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        Int32? companyID;
        Int32? agencyID;

        DateTime? StartDate;
        DateTime? EnDate;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                var gl = db.Companies.Where(x => x.Active == true).ToList();
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataSource = gl;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Select Company", "0"));
                ddlCompany.SelectedIndex = 0;
                                
                var ma = db.GroupAgencies.OrderBy(x=> x.GroupName).Where(x => x.Active == true).ToList();
                ddlMasteragency.DataValueField = "ID";
                ddlMasteragency.DataTextField = "GroupName";
                ddlMasteragency.DataSource = ma;
                ddlMasteragency.DataBind();
                ddlMasteragency.Items.Insert(0, new ListItem("Select Company", "0"));
            }
        }
        protected void btnExecute_Click(object sender, EventArgs e)
        {
            
           // Int32? clientID;
            if (ddlCompany.SelectedIndex == 0)
                companyID = null;
            else
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            try
            {
                if (ddlAgency.SelectedIndex == 0)
                    agencyID = null;
                else
                    agencyID = Convert.ToInt32(ddlAgency.SelectedValue);
            }
            catch (Exception ex)
            {

                agencyID = 0;// Convert.ToInt32(ddlAgency.SelectedValue);
            }
           

            //if (ddlc.SelectedIndex == 0)
            //    clientID = null;
            //else
            //    clientID = Convert.ToInt32(ddlCompany.SelectedValue);
            
             
            
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
            try
            {
                string myWords = "";
                var ss = db.usp_GetLedgerReport(companyID, StartDate, EnDate, agencyID, null).ToList();
                ReportViewer1.LocalReport.DataSources.Clear();
                myWords = "Agency Ledger";
                if (ddlCompany.SelectedValue == "1")
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/ExpressPrintLedger.rdlc";
                else
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/DigitalPrintLedger.rdlc";
                ReportParameter rp = new ReportParameter("pmTitle", myWords);
                ReportDataSource rds = new ReportDataSource("DSLedger", ss);
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                ReportViewer1.LocalReport.DataSources.Add(rds);

                ReportViewer1.LocalReport.Refresh();

                ReportViewer1.LocalReport.SubreportProcessing +=
                new Microsoft.Reporting.WebForms.SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                ReportViewer1.LocalReport.Refresh();

              //  ReportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;              
               
                //Add ReportDataSource  
               
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }

        }
        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            var sr = db.usp_LedgerSummarySubLedgerReport(companyID, agencyID, StartDate, EnDate).ToList();            
            ReportDataSource ds = new ReportDataSource("DSLedgerSummary", sr);

         //   if (e.ReportPath == "LegerSubReport")
         //   {
               // var employeeDetails = new ReportDataSource() { Name = "DSLedgerSummary", Value = sr };
                e.DataSources.Add(ds);
        //    }
        }
        protected void ddlMasteragency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(ddlMasteragency.SelectedValue);
            var ca = db.Agencies.Where(x =>x.GroupID == ID &&x.Active == true).OrderBy(x=> x.AgencyName).ToList();
            ddlAgency.DataValueField = "ID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = ca;
            ddlAgency.DataBind();
            ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
        }

        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int agc = Convert.ToInt32(ddlAgency.SelectedValue);
                var cl = db.Clients.Where(x => x.AgencyID == agc).OrderBy(x => x.Client1).ToList();
                ddlclient.DataValueField = "ID";
                ddlclient.DataTextField = "Client1";
                ddlclient.DataSource = cl;
                ddlclient.DataBind();
                ddlclient.Items.Insert(0, new ListItem("Select Client", "0"));
            }
            catch (Exception ex)
            {

                lblmessage.Text = ex.Message;
            }
            

        }

        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}