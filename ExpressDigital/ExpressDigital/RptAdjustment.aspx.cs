using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class RptAdjustment : System.Web.UI.Page
    {
        static bool _isSqlTypesLoaded = false;
        DbDigitalEntities db = new DbDigitalEntities();

        public RptAdjustment()
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
                var gl = db.Companies.Where(x => x.Active == true).ToList();
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataSource = gl;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Select Company", "0"));

                txtSearchROMODateFrom.Text = DateTime.Now.AddDays(-31).ToString("dd/MM/yyyy");
                txtSearchROMODateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                btnSearch_Click(null, null);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strReleaseOrderID;
            string strAgency;
            string strClinet;
            int? companyID;
            DateTime? StartDate;
            DateTime? EnDate;

            if (ddlCompany.SelectedIndex == 0)
                companyID = null;
            else
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);            

            if (txtSearchReleaseOrder.Text.Length == 0)
                strReleaseOrderID = "";
            else
                strReleaseOrderID = txtSearchReleaseOrder.Text;

            if (txtAgency.Text.Length == 0)
                strAgency = "";
            else
                strAgency = txtAgency.Text;

            if (txtClient.Text.Length == 0)
                strClinet = "";
            else
                strClinet = txtClient.Text;

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
            var s = db.usp_RptGetCreditDebitList(StartDate, EnDate, companyID, strReleaseOrderID, strAgency, strClinet).ToList();
            DataTable dt = Helper.ToDataTable(s);
            ViewState["dt"] = dt;
            gv.DataSource = s;
            gv.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.PageIndex = e.NewPageIndex;
            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddlInvoiceStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnprint_Click(object sender, EventArgs e)
        {

        }

        private void printReport(int AdjustmentId, int AgencyId)
        {
            lblmessage.Text = string.Empty;
            try
            {

                var ss = db.usp_DebitCreditNote_new(AdjustmentId, AgencyId).ToList();
                if (ss != null)
                {
                    var s = ss.Take(1).SingleOrDefault();
                    decimal _amount = Math.Abs(Convert.ToDecimal(s.TotalAmount));
                    string myWords = DecimalToWordExtension.ToWords(Convert.ToDecimal(_amount));

                    ReportParameter[] rp = new ReportParameter[2];

                    string note = "";
                    string itemText = s.AdjustmentNo.Substring(0, 2);
                    note = itemText == "DN" ? "Debit Note" : "Credit Note";

                    rp[0] = new ReportParameter("pmToWords", myWords);
                    rp[1] = new ReportParameter("pmDebitCredit", note);

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    //    //set path of the Local report  
                    if (Convert.ToInt32(ddlCompany.SelectedValue) == 9)
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/Digital-DebitCreditNote.rdlc";
                    else
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/Express-DebitCreditNote.rdlc";

                    ReportDataSource rds = new ReportDataSource("DataSetAdjustment", ss);
                    ReportViewer1.LocalReport.SetParameters(rp);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else
                {
                    lblmessage.Text = "Invoice Data Not Available For Printing";
                }
            }
            catch (Exception ex)
            {
                string txt = ex.InnerException.Message;
            }
        }

        protected void btnprint_Click1(object sender, EventArgs e)
        {
            Button imageButton = (Button)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 AdjustmentId = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Values[0].ToString());
            int AgencyId = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Values[1].ToString());
            printReport(AdjustmentId, AgencyId);
        }
    }
}