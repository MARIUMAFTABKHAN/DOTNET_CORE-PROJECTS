using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class InvoiceUpDate : System.Web.UI.Page
    {
        static bool _isSqlTypesLoaded = false;

        DbDigitalEntities db = new DbDigitalEntities();
        public InvoiceUpDate()
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
                var c = db.Companies.Where(x => x.Active == true).OrderBy(x => x.Company_Name).ToList();
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataValueField = "Company_ID";
                ddlCompany.DataSource = c;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Select Company", "0"));

                var p = db.Portals.Where(x => x.IsActive == true).OrderBy(x => x.PortalName).ToList();
                ddlSearchPortal.DataValueField = "ID";
                ddlSearchPortal.DataTextField = "PortalName";
                ddlSearchPortal.DataSource = p;
                ddlSearchPortal.DataBind();
                ddlSearchPortal.Items.Insert(0, new ListItem("Select Portal", "0"));
                if (ddlInvoiceStatus.SelectedValue.ToString() != "2")
                {

                }
                txtSearchROMODateFrom.Text = DateTime.Now.AddDays(-31).ToString("dd/MM/yyyy");
                txtSearchROMODateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                btnSearch_Click(null, null);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strIRO;
            int? InvoiceID;
            string strAgency;
            string strClinet;
            string strCampaign;
            string strExternal;
            Boolean? isbuild;
            Int32? CompanyId;
            DateTime? StartDate;
            DateTime? EnDate;

            if (ddlInvoiceStatus.SelectedIndex == 0)
                isbuild = false;
            else
                isbuild = true;

            int? PortalID;

            if (txtIRO.Text.Length == 0)
                strIRO = null;
            else
                strIRO = txtIRO.Text;

            if (ddlSearchPortal.SelectedIndex == 0)
                PortalID = null;
            else
                PortalID = Convert.ToInt32(ddlSearchPortal.SelectedValue);

            if (txtRefNumber.Text.Length == 0)
                strExternal = null;
            else
                strExternal = txtRefNumber.Text;

            if (txtSearchReleaseOrder.Text.Length == 0)
                InvoiceID = null;
            else
                InvoiceID = Convert.ToInt32(txtSearchReleaseOrder.Text);

            if (txtAgency.Text.Length == 0)
                strAgency = null;
            else
                strAgency = txtAgency.Text;

            if (txtClient.Text.Length == 0)
                strClinet = null;
            else
                strClinet = txtClient.Text;
            if (txtcampaign.Text.Length == 0)
                strCampaign = null;
            else
                strCampaign = txtcampaign.Text;

            if (ddlCompany.SelectedIndex == 0)
                CompanyId = null;
            else
                CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
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
            var s = db.usp_GetDataForInvoice_Cancellation_New2(InvoiceID, strExternal, strIRO, StartDate, EnDate, strAgency, strClinet, strCampaign, isbuild, CompanyId).ToList();

            DataTable dt = Helper.ToDataTable(s);
            ViewState["dt"] = dt;
            gv.DataSource = s;
            gv.DataBind();
            try
            {
            }
            catch (Exception)
            {

            }
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton img = (ImageButton)e.Row.FindControl("btnremove");
                if (e.Row.Cells[10].Text == "Yes")
                {

                    img.Enabled = false;
                }
                else
                    img.Enabled = true;
            }
        }

        protected void ddlInvoiceStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            Int32 InvoiceID = Convert.ToInt32(gv.DataKeys[row.RowIndex].Value);
            printReport(InvoiceID);
        }

        private void printReport(int InvoiceID)
        {
            try
            {

            }
            catch (Exception ex)
            {
                string txt = ex.InnerException.Message;
            }
        }

        protected void btnremove_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
            Int32 InvoiceID = Convert.ToInt32(gv.DataKeys[row.RowIndex].Value);
            lblmessage.Text = string.Empty;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                var p = db.PaymentDetails.Where(x => x.InvoiceId == InvoiceID).SingleOrDefault();
                if (p == null)
                {
                    var sp = db.SettlementPlanDetails.Where(x => x.InvoiceId == InvoiceID).Take(1).SingleOrDefault();
                    if (sp == null)
                    {
                        var inv = db.InvoiceMasters.Where(x => x.ID == InvoiceID).SingleOrDefault();
                        if (inv != null)
                        {
                            inv.IsCancelled = true;
                            inv.CancelledOn = DateTime.Now;
                            inv.CancelledBy = ((UserInfo)Session["UserObject"]).ID;
                            db.SaveChanges();
                        }
                        Int32 ROID = Convert.ToInt32(inv.ReleaseOrderID);
                        var RO = db.ReleaseOrderMasters.Where(x => x.ID == ROID && x.IsCancelled == false).SingleOrDefault();
                        if (RO != null)
                            RO.IsBilled = 0;
                        db.SaveChanges();

                        var lb = db.LedgerBalances.Where(x => x.InvoiceID == InvoiceID && x.StatusId == 1).SingleOrDefault();
                        lb.StatusId = 12;
                        lb.NetBalance = 0;
                        lb.ReceiptAmount = 0;
                        lb.BillAmount = 0;
                        db.SaveChanges();

                        LogManagers.RecordID = InvoiceID;
                        LogManagers.ActionOnForm = "Invoiceupdate";
                        LogManagers.ActionBy = ((UserInfo)Session["UserObject"]).ID;
                        LogManagers.ActionOn = DateTime.Now;
                        LogManagers.ActionTaken = "Invoice Cancelled";
                        LogManagers.SetLog(db);
                        scope.Complete();
                        lblmessage.Text = "Invoice has been marked cancelled";
                    }
                    else
                    {
                        lblmessage.Text = "Invoice has been settled and can't be cancelled";
                        return;
                    }
                }
                else
                {
                    lblmessage.Text = "Invoice payment has been Setteled and can't be cancelled";
                    return;
                }
            }
        }
    }
}