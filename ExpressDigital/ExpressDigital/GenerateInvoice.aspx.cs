using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

namespace ExpressDigital
{
    public partial class GenerateInvoice : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtinvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                var g = db.GroupAgencies.OrderBy(x => x.GroupName).ToList();
                var p = db.Portals.Where(x => x.IsActive == true).OrderBy(x => x.PortalName).ToList();
                ddlSearchPortal.DataValueField = "ID";
                ddlSearchPortal.DataTextField = "PortalName";
                ddlSearchPortal.DataSource = p;
                ddlSearchPortal.DataBind();
                ddlSearchPortal.Items.Insert(0, new ListItem("Select Portal", "0"));

                var gl = db.Companies.Where(x => x.Active == true).ToList();
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataSource = gl;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Select Company", "0"));

                var ag = db.Agencies.Where(x => x.Active == true).OrderBy(x => x.AgencyName).ToList();
                chkagency.DataValueField = "ID";
                chkagency.DataTextField = "AgencyName";
                chkagency.DataSource = ag;
                chkagency.DataBind();

                txtSearchROMODateFrom.Text = DateTime.Now.AddDays(-31).ToString("dd/MM/yyyy");
                txtSearchROMODateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                btnSearch_Click(null, null);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = String.Empty;
            btnExecute.Enabled = true;

            string strIRO;
            string strReleaseOrderID;
            string strClinet;
            string strCampaign;
            string strExternal;
            Boolean? isbuild;
            Int32? companyID;

            if (ddlCompany.SelectedIndex == 0)
                companyID = null;
            else
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            if (ddlInvoiceStatus.SelectedIndex == 0)
                isbuild = null;
            else if (ddlInvoiceStatus.SelectedIndex == 1)
                isbuild = true;
            else
                isbuild = false;

            DateTime? StartDate;
            DateTime? EnDate;
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
                strReleaseOrderID = null;
            else
                strReleaseOrderID = txtSearchReleaseOrder.Text;

            if (txtClient.Text.Length == 0)
                strClinet = null;
            else
                strClinet = txtClient.Text;
            if (txtcampaign.Text.Length == 0)
                strCampaign = null;
            else
                strCampaign = txtcampaign.Text;
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
            // for agency 
            string txt = null;
            foreach (ListItem item in chkagency.Items)
            {
                if (item.Selected)
                {
                    txt = txt + item.Value.ToString() + ";";
                }
            }
            try
            {
                if (txt != null)
                {
                    if (txt.Length > 2)
                        txt = txt.Substring(0, txt.Length - 1);
                }
            }
            catch (Exception)
            {

            }

            int InvoiceStatus = Convert.ToInt32(ddlInvoiceStatus.SelectedValue);
            var s = db.usp_GetDataForInvoice_MujltipalAgency(strReleaseOrderID, strExternal, strIRO, StartDate, EnDate, txt, strClinet, strCampaign, isbuild, companyID).ToList();
            DataTable dt = Helper.ToDataTable(s);
            ViewState["dt"] = dt;
            gv.DataSource = s;
            gv.DataBind();
            if (s.Count > 0)
            {
                if (ddlInvoiceStatus.SelectedIndex == 1 || ddlInvoiceStatus.SelectedIndex == 0)
                    ((CheckBox)gv.HeaderRow.FindControl("chkHeader")).Enabled = false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            foreach (GridViewRow dr in gv.Rows)
            {
                CheckBox chk = (CheckBox)dr.FindControl("chk");
                chk.Checked = false;
            }
        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            btnExecute.Enabled = false;

            lblmessage.Text = string.Empty;
            bool isTaken = false;
            int i = 0;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    InvoiceMaster objMaster = new InvoiceMaster();
                    InvoiceDetail objDetail = new InvoiceDetail();

                    foreach (GridViewRow dr in gv.Rows)
                    {
                        if (dr.RowType == DataControlRowType.DataRow)
                        {
                            string txt = dr.Cells[9].Text;
                            //if (txt == "0")
                            //{
                                CheckBox chk = (CheckBox)dr.FindControl("chk");
                                if (true)
                                {
                                    if (chk.Checked == true)
                                    {
                                        try
                                        {
                                            int ID = Convert.ToInt32(gv.DataKeys[dr.RowIndex].Value);
                                            TextBox txtinvreference = (TextBox)gv.Rows[dr.RowIndex].FindControl("txtInvReference");
                                            var s = db.ReleaseOrderMasters.Where(x => x.ID == ID).SingleOrDefault();
                                            s.IsBilled = 1; // build
                                            s.invoiceReference = txtinvreference.Text;
                                            db.SaveChanges();

                                            var inv = db.usp_IDctr("Invoice").SingleOrDefault();
                                            if (s != null)
                                            {
                                                objMaster = new InvoiceMaster();
                                                objMaster.ID = Convert.ToInt32(inv.Value);
                                                objMaster.ReleaseOrderID = ID; // Releae order master id
                                                objMaster.GrossAmount = 0;// Convert.ToDecimal(s.INV_Gross);
                                                objMaster.NetReceiable = Convert.ToDecimal(s.INV_Net);
                                                objMaster.RecivedAmount = 0;//

                                                objMaster.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                                                if (chkDollar.Checked)
                                                    objMaster.dollarinvoice = 1;
                                                else
                                                    objMaster.dollarinvoice = 0;
                                                objMaster.Billed = true; objMaster.PaymentStatusID = 110000001;
                                                objMaster.ClientID = s.ClientID;
                                                objMaster.AgencyID = s.AgencyID;
                                                objMaster.CompanyID = s.CompnayID;
                                                objMaster.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                                                objMaster.CreationDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());// Helper.SetDateFormat(txtinvoiceDate.Text);//  Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                                objMaster.InvReference = txtinvreference.Text;
                                                objMaster.ChannelID = s.ChannelID;
                                                objMaster.InvoiceDate = Helper.SetDateFormat(txtinvoiceDate.Text);
                                                //objMaster.ModifiedBy = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                                                //objMaster.ModifiedOn = DateTime.Now;
                                                //objMaster.CancelledBy = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                                                // objMaster.IsCancelled = false;
                                                db.InvoiceMasters.Add(objMaster);
                                                db.SaveChanges();
                                                decimal mGross = 0;
                                                decimal mAGC = 0;
                                                decimal mGST = 0;
                                                decimal mDiscount = 0;
                                                decimal mNet = 0;
                                                var Detail = db.ReleaseOrderDetails.Where(x => x.ReleaseOrderID == ID).ToList();
                                                foreach (var id in Detail)
                                                {
                                                    objDetail = new InvoiceDetail();
                                                    var detailid = db.usp_IDctr("Invoice Detail").SingleOrDefault();
                                                    objDetail.ID = detailid.Value;
                                                    objDetail.InvoiceID = objMaster.ID;
                                                    objDetail.ReleaseOrderId = ID;
                                                    objDetail.ReleaseOrderDetailId = id.ID;
                                                    objDetail.FromDate = id.StartDate;
                                                    objDetail.ToDate = id.EndDate;
                                                    objDetail.IsCancelled = false;
                                                    objDetail.PortalID = id.PortalID;
                                                    objDetail.GrossAmount = id.TotalBilled;
                                                    objDetail.NetAmount = id.NetAmount;
                                                    objDetail.GSTAmount = id.GSTAmount;
                                                    objDetail.AGCAmount = id.AGCommission;
                                                    mGross = mGross + id.TotalBilled;
                                                    mNet = mNet + id.NetAmount;
                                                    mGST = mGST + id.GSTAmount;
                                                    mDiscount = mDiscount + id.Discount;
                                                    mAGC = mAGC + id.AGCommission;
                                                    db.InvoiceDetails.Add(objDetail);
                                                    db.SaveChanges();
                                                }
                                                objMaster.GrossAmount = mGross;
                                                objMaster.GSTAmount = mGST;
                                                objMaster.AgencyAmount = mAGC;
                                                objMaster.NetReceiable = mNet;
                                                objMaster.totalDiscount = mDiscount;
                                                objMaster.BalanceAmount = mNet;
                                                db.SaveChanges();

                                                LedgerBalance lb = new LedgerBalance();
                                                lb.ID = db.usp_IDctr("LedgerBalance").SingleOrDefault().Value;
                                                lb.AgencyId = s.AgencyID;
                                                lb.ClientId = s.ClientID;
                                                lb.InvoiceID = objMaster.ID;
                                                lb.IsCRV = false;
                                                lb.ISAdjusted = false;
                                                lb.IsTax = false;
                                                lb.BillAmount = Convert.ToInt32(s.INV_Net);
                                                lb.ReceiptAmount = 0;
                                                var agb = db.GetAgencyBalance(s.CompnayID, s.AgencyID).SingleOrDefault();

                                                lb.NetBalance = Convert.ToDecimal(agb) + Convert.ToDecimal(s.INV_Net);

                                                var clb = db.GetClientBalance(s.CompnayID, s.AgencyID, s.ClientID).SingleOrDefault();
                                                lb.ClientBalance = Convert.ToDecimal(clb) + Convert.ToDecimal(s.INV_Net);

                                                lb.CompanyID = objMaster.CompanyID;
                                                lb.StatusId = 1;
                                                lb.TransactionDate = DateTime.Now;
                                                lb.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                                                lb.ChannelID = 1;
                                                db.LedgerBalances.Add(lb);
                                                db.SaveChanges();

                                                LogManagers.RecordID = objMaster.ID;
                                                LogManagers.ActionOnForm = "Invoice Created";
                                                LogManagers.ActionBy = ((UserInfo)Session["UserObject"]).ID;
                                                LogManagers.ActionOn = DateTime.Now;
                                                LogManagers.ActionTaken = "Invoice Created";
                                                LogManagers.SetLog(db);
                                            }
                                            i++;
                                        }
                                        catch (Exception ex)
                                        {
                                            try
                                            {
                                                if (ex.InnerException.InnerException.Message.Contains("duplicate"))
                                                {
                                                    lblmessage.Text = "Invoice already generated, Please cancel invoice to regenerate";
                                                    return;
                                                }
                                            }
                                            catch (Exception)
                                            {

                                                lblmessage.Text = ex.Message;
                                                return;
                                            }
                                            lblmessage.Text = ex.Message;
                                            return;
                                        }

                                    }
                                }
                            //}
                        }
                    }
                    if (i > 0)
                    {
                        btnSearch_Click(null, null);
                        scope.Complete();
                        btnExecute.Enabled = false;
                        lblmessage.Text = "Invoice Created Successfully";
                    }
                    else
                    {
                        btnExecute.Enabled = true;
                        lblmessage.Text = "Invoice generation failed";
                    }
                }
                catch (Exception ex)
                {
                    btnExecute.Enabled = true;
                    lblmessage.Text = "Invoice orders receiving failed";
                }
            }
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
            CheckBox chk = (CheckBox)e.Row.FindControl("chk");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string txt = e.Row.Cells[9].Text;
                if (txt == "1")
                {
                    chk.Checked = true;
                    chk.Enabled = false;
                }
            }
        }

        protected void ddlInvoiceStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInvoiceStatus.SelectedValue.ToString() != "2")
            {
                // btnExecute.Enabled = false;
            }
            // else
            //  btnExecute.Enabled = true;
        }
    }
}