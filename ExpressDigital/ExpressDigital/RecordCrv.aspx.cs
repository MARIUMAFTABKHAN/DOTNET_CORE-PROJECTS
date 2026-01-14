using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class RecordCrv : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                divClearDate.Visible = false;
                int nCRVDetailId = 0;
                var g = db.GroupAgencies.Where(x => x.Active == true && x.Suspended == false).OrderBy(x => x.GroupName).ToList();
                ddlGroupAgency.DataValueField = "ID";
                ddlGroupAgency.DataTextField = "GroupName";
                ddlGroupAgency.DataSource = g;
                ddlGroupAgency.DataBind();

                ddlGroupAgency.Items.Insert(0, new ListItem("Select Group Agency", "0"));
                ddlGroupAgency_SelectedIndexChanged(null, null);

                //-----------

                ddlSearchGroup.DataValueField = "ID";
                ddlSearchGroup.DataTextField = "GroupName";
                ddlSearchGroup.DataSource = g;
                ddlSearchGroup.DataBind();
                ddlSearchGroup.Items.Insert(0, new ListItem("Select Group Agency", "0"));


                ddlSearchGroup_SelectedIndexChanged(null, null);
                //var c = db.Clients.Where(x => x.Active == true).OrderBy(x => x.Client1).ToList();
                //ddlSearchClient.DataValueField = "ID";
                //ddlSearchClient.DataTextField = "Client1";
                //ddlSearchClient.DataSource = c;
                //ddlSearchClient.DataBind();
                //ddlSearchClient.Items.Insert(0, new ListItem("Select Client", "0"));

                //-----------


                var pm = db.PaymentModes.OrderBy(x => x.PaymnetType).ToList();
                ddlPaymentMode.DataValueField = "ID";
                ddlPaymentMode.DataTextField = "PaymnetType";
                ddlPaymentMode.DataSource = pm;
                ddlPaymentMode.DataBind();

                var pt = db.PaymentTypes.Where(x => x.Active == true).OrderBy(x => x.PaymentTypeStatus).ToList();
                ddlPaymentType.DataValueField = "ID";
                ddlPaymentType.DataTextField = "PaymentTypeStatus";
                ddlPaymentType.DataSource = pt;
                ddlPaymentType.DataBind();

                var cr = db.CurrencyModes.Where(x => x.IsActive == true).ToList();
                ddlcurrency.DataTextField = "BillingCurrency";
                ddlcurrency.DataValueField = "ID";
                ddlcurrency.DataSource = cr;
                ddlcurrency.DataBind();


                var crvs = db.CRVStatus.OrderBy(x => x.ID).ToList();
                ddlCRVStatus.DataValueField = "ID";
                ddlCRVStatus.DataTextField = "Status";
                ddlCRVStatus.DataSource = crvs;
                ddlCRVStatus.DataBind();

                //  GLEntities db2 = new GLEntities();
                var gl = db.Companies.Where(x => x.Active == true).ToList();
                ddlcompany.DataValueField = "Company_Id";
                ddlcompany.DataTextField = "Company_Name";
                ddlcompany.DataSource = gl;
                ddlcompany.DataBind();

                ddlCRVStatus.Enabled = false;
                dtCRVDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                dtChequeDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                txtSearchROMODateFrom.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                txtSearchROMODateTo.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));


                try
                {
                    if (Request.QueryString.Count > 0)
                    {
                        if (Request.QueryString[0] != null)
                        {
                            divClearDate.Visible = true;
                            ddlCRVStatus.Enabled = true;
                            int ID = Convert.ToInt32(Request.QueryString[0]);
                            ViewState["RecordID"] = ID;
                            var crv = db.tblCRVs.Where(x => x.CRVId == ID).SingleOrDefault();
                            dtCRVDate.Text = Convert.ToDateTime(crv.CRVDate).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                            txtAmount.Text = crv.CRVAmount.ToString();
                            txtGST.Text = crv.GST.ToString();
                            txtWithhoding.Text = crv.WithHoldingTax.ToString();
                            txtBank.Text = crv.Bank;
                            txtBranch.Text = crv.Branch;
                            ddlGroupAgency.SelectedValue = crv.Agency.GroupID.ToString();
                            ddlGroupAgency_SelectedIndexChanged(null, null);
                            ddlAgency.SelectedValue = crv.AgencyId.ToString();
                            ddlAgency_SelectedIndexChanged(null, null);
                            ddlCRVStatus.SelectedValue = crv.Status.ToString();
                            ddlPaymentMode.SelectedValue = crv.PaymentModeId.ToString();
                            ddlPaymentType.SelectedValue = crv.PaymentTypeId.ToString();
                            txtChequeNo.Text = crv.PaymentNumber;
                            try
                            {
                                txtChallanDate.Text = Convert.ToDateTime(crv.ChallanDate).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                                if (txtChallanDate.Text == "01/01/0001")
                                    txtChallanDate.Text = string.Empty;
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                txtClearDate.Text = Convert.ToDateTime(crv.ChequeDate).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                                if (txtClearDate.Text == "01/01/0001")
                                    txtClearDate.Text = string.Empty;
                            }
                            catch (Exception)
                            {
                            }

                            var crvd = db.tblCRVDetails.Where(x => x.CRVId == ID).SingleOrDefault();
                            if (crvd.ClientId != null)

                                ddlClient.SelectedValue = crvd.ClientId.ToString();

                            var p = db.Payments.Where(x => x.CRVId == ID).ToList();
                            if ((p.Count() == 0) && (crv.Status.ToString() == "110000002"))
                            {

                            }
                            btnSave.Text = "Update";

                            if (Request.QueryString[1] != null)
                            {
                                nCRVDetailId = int.Parse(Request.QueryString[1]);
                            }

                        }

                    }
                }
                catch (Exception)
                {


                }

            }

        }

        protected void ddlGroupAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblmessage.Text = string.Empty;
                int ID = Convert.ToInt32(ddlGroupAgency.SelectedValue);
                var c = db.GroupAgencies.Where(x => x.ID == ID).SingleOrDefault();
                if (c != null)
                {
                    if (c.Active == false)
                    {
                        lblmessage.Text = "Master Agency/Client is de-activated";
                    }
                    if (c.Suspended == true)
                    {
                        lblmessage.Text = "Master Agency/Client is suspended";
                    }
                    var g = db.vAgencyWithCities.Where(x => x.GroupID == ID).OrderBy(x => x.AgencyName).ToList();
                    ddlAgency.DataValueField = "ID";
                    ddlAgency.DataTextField = "AgencyName";
                    ddlAgency.DataSource = g;
                    ddlAgency.DataBind();

                    ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
                }
                else
                {
                    var g = db.Agencies.Where(x => x.GroupID == 0).OrderBy(x => x.AgencyName).ToList();
                    ddlAgency.DataValueField = "ID";
                    ddlAgency.DataTextField = "AgencyName";
                    ddlAgency.DataSource = g;
                    ddlAgency.DataBind();
                    ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
                }
                ddlAgency_SelectedIndexChanged(null, null);
            }
            catch (Exception)
            {
            }
        }
        protected void ddlValidation_server(object sender, ServerValidateEventArgs e)
        {
            if (e.Value == "0")
            {
                e.IsValid = false;

            }
            else
            {
                e.IsValid = true;
            }

        }
        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int AgencyID = Convert.ToInt32(ddlAgency.SelectedValue);
                var ssss = db.Agencies.Where(x => x.ID == AgencyID).SingleOrDefault();
                if (ssss != null)
                {
                    if (ssss.Active == false)
                    {
                        lblmessage.Text = "Agency/Client is de-activated";
                    }
                    if (ssss.Suspended == true)
                    {
                        lblmessage.Text = "Agency/Client is suspended";
                    }
                    var c = db.Clients.Where(x => x.Active == true && x.Suspended == false && x.AgencyID == AgencyID).OrderBy(x => x.Client1).ToList();

                    //var c = db.GetClientsByAgency(AgencyID).OrderBy(x => x.Client).ToList();

                    ddlClient.DataValueField = "ID";
                    ddlClient.DataTextField = "Client1";
                    ddlClient.DataSource = c;
                    ddlClient.DataBind();
                }
                ddlClient.Items.Insert(0, new ListItem("Select Client", "0"));


            }
            catch (Exception ex)
            {

            }

        }

        protected void ViewButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            String str = "ViewCRVAll.aspx?ID=" + ID.ToString();
            Response.Redirect(str, true);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {

                if (btnSave.Text == "Save")
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            tblCRV crv = new tblCRV();
                            tblCRVDetail crvd = new tblCRVDetail();
                            crv.CRVId = db.usp_IDctr("CRVMaster").SingleOrDefault().Value;
                            crv.CRVNo = "CRV-" + crv.CRVId.ToString();
                            crv.AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);
                            crv.Bank = txtBank.Text;
                            crv.Branch = txtBranch.Text;
                            crv.PaymentModeId = Convert.ToInt32(ddlPaymentMode.SelectedValue);
                            crv.PaymentNumber = txtChequeNo.Text;
                            crv.CRVDate = Helper.SetDateFormat(dtCRVDate.Text);
                            crv.CRVAmount = Convert.ToDecimal(txtAmount.Text);
                            crv.GST = Convert.ToDecimal(txtGST.Text);
                            crv.WithHoldingTax = Convert.ToDecimal(txtWithhoding.Text);
                            crv.ChequeDate = Helper.SetDateFormat(dtChequeDate.Text);
                            crv.Status = Convert.ToInt32(ddlCRVStatus.SelectedValue);
                            crv.ChannelId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ChannelId"]);
                            crv.IsChallanRecieved = chkChallanReceived.Checked;
                            if (txtChallanDate.Text.Length > 0)
                                crv.ChallanDate = Helper.SetDateFormat(txtChallanDate.Text);
                            else
                                crv.ChallanDate = null;

                            if (ddlCRVStatus.SelectedItem.Text.Equals("Cleared"))
                            {
                                try
                                {
                                    crv.CRVClearedDate = Convert.ToDateTime(Helper.SetDateFormat(txtClearDate.Text));
                                }
                                catch (Exception)
                                {
                                    crv.CRVClearedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                }

                            }
                            crv.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                            crv.CreatedDate = DateTime.Now;
                            crv.PaymentTypeId = Convert.ToInt32(ddlPaymentType.SelectedValue);
                            crv.CompanyId = int.Parse(ddlcompany.SelectedValue);
                            db.tblCRVs.Add(crv);
                            db.SaveChanges();

                            // CRV Details
                            crvd.CRVDetailId = db.usp_IDctr("CRVDetail").SingleOrDefault().Value;
                            crvd.CRVId = crv.CRVId;
                            crvd.CRVAmount = Convert.ToDecimal(crv.CRVAmount);// + crv.GST + crv.WithHoldingTax);
                            crvd.ConsumedAmount = 0;
                            crvd.RemainingAmount = crvd.CRVAmount;
                            if (ddlClient.SelectedValue != "0")
                                crvd.ClientId = Convert.ToInt32(ddlClient.SelectedValue);
                            else
                                crvd.ClientId = null;
                            crvd.IsCRVFullyConsumed = false;
                            crvd.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                            crvd.CreatedDate = DateTime.Now;
                            crvd.test1 = 0;
                            crvd.test2 = 0;
                            crvd.test3 = 0;
                            db.tblCRVDetails.Add(crvd);
                            db.SaveChanges();

                            LogManagers.RecordID = crv.CRVId;
                            LogManagers.ActionOnForm = "CRV";
                            LogManagers.ActionBy = ((UserInfo)Session["UserObject"]).ID;//Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                            LogManagers.ActionOn = DateTime.Now;
                            LogManagers.ActionTaken = "CRV";
                            LogManagers.SetLog(db);




                            //  insert in ledger
                            if (crv.Status == 110000003)
                            {
                                InsertInLedger(Convert.ToInt32(crv.Status), crv.CRVId, Convert.ToDateTime(crv.CRVClearedDate));


                                var _dtInvoice = db.usp_OutStanding_Inv_DN_Digital(crv.AgencyId, int.Parse(ddlClient.SelectedItem.Value), null, crv.CRVId);

                                if (_dtInvoice != null)
                                {
                                    AutoSettled(crv.CRVId);
                                }
                            }
                            if ((crv.Status != 110000003) && (crv.Status != 110000001))
                            {

                                db.usp_MakePlan_InActive(crv.CRVId);
                            }

                            scope.Complete();
                            //  Response.Redirect("CRVView.aspx");
                            Response.Redirect("ViewCRVALL.aspx");

                            lblmessage.Text = "Cheque Received Voucher has been created successfully.....";

                        }
                        catch (Exception ex)
                        {
                            // btnSave.Enabled = true;
                            lblmessage.Text = ex.Message;
                        }
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        btnSave.Enabled = false;
                        int ID = Convert.ToInt32((ViewState)["RecordID"]);
                        var crv = db.tblCRVs.Where(x => x.CRVId == ID).SingleOrDefault(); ;
                        var crvd = db.tblCRVDetails.Where(x => x.CRVId == ID).SingleOrDefault();

                        if (crv.Status != 110000003 && crv.Status != 110000005)
                        {
                            try
                            {
                                crv.AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);
                                crv.Bank = txtBank.Text;
                                crv.Branch = txtBranch.Text;
                                crv.PaymentModeId = Convert.ToInt32(ddlPaymentMode.SelectedValue);
                                crv.PaymentNumber = txtChequeNo.Text;
                                crv.CRVDate = Helper.SetDateFormat(dtCRVDate.Text);
                                crv.CRVAmount = Convert.ToDecimal(txtAmount.Text);
                                crv.GST = Convert.ToDecimal(txtGST.Text);
                                crv.WithHoldingTax = Convert.ToDecimal(txtWithhoding.Text);
                                crv.ChequeDate = Helper.SetDateFormat(dtChequeDate.Text);
                                crv.Status = Convert.ToInt32(ddlCRVStatus.SelectedValue);
                                crv.LastModifiedBy = ((UserInfo)Session["UserObject"]).ID;
                                crv.LastModifiedDate = DateTime.Now;
                                crv.PaymentTypeId = Convert.ToInt32(ddlPaymentType.SelectedValue);
                                crv.PaymentModeId = Convert.ToInt32(ddlPaymentMode.SelectedValue);
                                //crv.CompanyID = int.Parse(ddlcompany.SelectedValue);
                                if (ddlCRVStatus.SelectedItem.Equals("Cleared"))
                                {
                                    try
                                    {
                                        crv.CRVClearedDate = Helper.SetDateFormat(txtClearDate.Text);
                                    }
                                    catch (Exception)
                                    {
                                        crv.CRVClearedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                    }
                                }
                                db.SaveChanges();
                                crvd.CRVAmount = crv.CRVAmount + crv.GST + crv.WithHoldingTax;
                                crvd.ConsumedAmount = 0;
                                crvd.RemainingAmount = crvd.CRVAmount;

                                if (ddlClient.SelectedValue != "0")
                                    crvd.ClientId = Convert.ToInt32(ddlClient.SelectedValue);
                                else
                                    crvd.ClientId = null;

                                crvd.IsCRVFullyConsumed = false;
                                crvd.ModifiedBy = ((UserInfo)Session["UserObject"]).ID;
                                crvd.ModifiedDate = DateTime.Now;

                                db.SaveChanges();

                                LogManagers.RecordID = crvd.CRVDetailId;
                                LogManagers.ActionOnForm = "CRVDetail;";
                                LogManagers.ActionBy = ((UserInfo)Session["UserObject"]).ID;//Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                                LogManagers.ActionOn = DateTime.Now;
                                LogManagers.ActionTaken = "CRV";
                                LogManagers.SetLog(db);

                                // updatevalue(crv.CRVId);

                                //   updateLedger(crv.CRVId);

                                if (crv.Status == 110000003)
                                {

                                    var objCRVDetail = db.tblCRVDetails.Where(x => x.CRVDetailId == crvd.CRVDetailId).SingleOrDefault();
                                    objCRVDetail.ClientId = int.Parse(ddlClient.SelectedItem.Value);
                                    db.SaveChanges();

                                    InsertInLedger(Convert.ToInt32(crv.Status), crv.CRVId, Convert.ToDateTime(crv.CRVClearedDate));

                                    //objInvoiceDB = new CTS.InvoiceDB();
                                    var _dtInvoice = db.usp_OutStanding_Inv_DN_Digital(crv.AgencyId, int.Parse(ddlClient.SelectedItem.Value), null, crv.CRVId).ToList();

                                    if (_dtInvoice.Count() > 0)
                                    {
                                        AutoSettled(crv.CRVId);
                                    }
                                    // }
                                    //if (crv.CRVStatusID == 110000002)
                                    //{
                                }
                                if ((crv.Status != 110000003) && (crv.Status != 110000001))
                                {

                                    db.usp_MakePlan_InActive(crv.CRVId);
                                }


                                scope.Complete();

                                // Response.Redirect("CRVView.aspx");
                                Response.Redirect("ViewCRVALL.aspx");


                                lblmessage.Text = "Cheque Received Voucher has been updated successfully.....";
                                btnSave.Text = "Save";


                            }
                            catch (Exception ex)
                            {
                                // btnSave.Enabled = true;
                                lblmessage.Text = ex.Message;
                            }
                        }
                        else if (crv.Status == 110000003)
                        {
                            crv.Bank = txtBank.Text.Trim();
                            crv.Branch = txtBranch.Text.Trim();
                            crv.PaymentModeId = Int32.Parse(ddlPaymentMode.SelectedItem.Value);
                            crv.PaymentTypeId = Int32.Parse(ddlPaymentType.SelectedItem.Value);
                            if (Convert.ToDecimal(txtGST.Text) > 0)
                            {
                                crv.PaymentNumber = ddlCRVStatus.Text.ToString();
                            }
                            else
                            {
                                crv.PaymentNumber = txtChequeNo.Text.Trim();
                            }

                            if (dtChequeDate.Text.Length > 0)
                            {
                                try
                                {
                                    crv.ChequeDate = Helper.SetDateFormat(dtChequeDate.Text);
                                }
                                catch (Exception)
                                {
                                    crv.ChequeDate = null;
                                }

                            }
                            else
                            {
                                crv.ChequeDate = null;
                            }

                            crv.IsChallanRecieved = chkChallanReceived.Checked;

                            if (txtChallanDate.Text.Length > 0)
                            {
                                try
                                {
                                    crv.ChallanDate = Helper.SetDateFormat(txtChallanDate.Text);
                                }
                                catch (Exception)
                                {

                                    crv.ChallanDate = null;
                                }

                            }
                            else
                            {
                                crv.ChallanDate = null;
                            }
                            crv.LastModifiedBy = ((UserInfo)Session["UserObject"]).ID;
                            crv.LastModifiedDate = DateTime.Now;

                            crv.CRVId = ID;
                            db.SaveChanges();
                            var objCRVDetail = db.tblCRVDetails.Where(x => x.CRVDetailId == crvd.CRVDetailId).SingleOrDefault();
                            objCRVDetail.ClientId = int.Parse(ddlClient.SelectedItem.Value);
                            db.SaveChanges();



                            var _dtPayment = db.Payments.Where(x => x.CRVId == ID).SingleOrDefault();

                            if (_dtPayment != null)
                            {

                                var dt = db.tblCRVDetails.Where(x => x.CRVId == ID).SingleOrDefault();
                                if (dt != null)
                                {
                                    if (Convert.ToInt32(dt.ClientId) == 0)
                                    {




                                        var dtCRV = db.LedgerBalances.Where(x => x.CRVID == ID && x.StatusId == 2).SingleOrDefault();
                                        if (dtCRV != null)
                                        {
                                            var objLedgerBalance = db.usp_GetAllLedgerBalance(true, 2).SingleOrDefault();
                                            if (objLedgerBalance != null)
                                            {

                                                objLedgerBalance.ClientId = int.Parse(ddlClient.SelectedItem.Value);
                                                db.SaveChanges();
                                            }
                                        }
                                        var dtTax = db.LedgerBalances.Where(x => x.CRVID == ID && x.StatusId == 3).SingleOrDefault();
                                        if (dtTax != null)
                                        {
                                            var objLedgerBalance = db.usp_GetAllLedgerBalance(true, 2).SingleOrDefault();
                                            if (objLedgerBalance != null)
                                            {
                                                objLedgerBalance.ClientId = int.Parse(ddlClient.SelectedItem.Value);
                                                db.SaveChanges();
                                            }
                                        }
                                        var dtGST = db.LedgerBalances.Where(x => x.CRVID == ID && x.StatusId == 3).SingleOrDefault();
                                        if (dtGST != null)
                                        {
                                            var objLedgerBalance = db.usp_GetAllLedgerBalance(true, 10).SingleOrDefault();
                                            if (objLedgerBalance != null)
                                            {
                                                objLedgerBalance.ClientId = int.Parse(ddlClient.SelectedItem.Value);
                                                db.SaveChanges();
                                            }
                                        }

                                    }
                                }

                            }



                        }
                        else if (crv.Status == 110000005)
                        {
                            DateTime dt;

                            if (txtChallanDate.Text.Length > 0)
                            {
                                dt = Helper.SetDateFormat(txtChallanDate.Text);
                            }
                            else
                            {
                                dt = DateTime.Now;
                            }
                            crv.IsChallanRecieved = true;
                            crv.ChallanDate = dt;
                            crv.LastModifiedDate = DateTime.Now;
                            crv.LastModifiedBy = ((UserInfo)Session["UserObject"]).ID;
                            db.SaveChanges();

                        }

                        scope.Complete();
                        lblmessage.Text = "Record Updated Successfuly";
                        Accordion1.SelectedIndex = 0;
                    }
                }
                // crv.pay

            }

        }
        public void AutoSettled(int CRVId)
        {

            var _dtInvoice = db.usp_CRV_AutoSettled_Digital(CRVId);

        }

        protected void InsertInLedger(int CRVStatusID, int CRVID, DateTime CRVClearedDate)
        {
            if (CRVStatusID == 110000003)
            {

                bool crv = false;
                bool tax = false;
                bool gst = false;
                if (ddlCRVStatus.SelectedItem.Text.Equals("Cleared"))
                {
                    int chk = 0;

                    if (Convert.ToDecimal(txtAmount.Text) == 0)
                    {
                        if ((Convert.ToDecimal(txtWithhoding.Text) == 0) && (Convert.ToDecimal(txtGST.Text) == 0))
                        {
                            chk = 0;
                        }
                        else if ((Convert.ToDecimal(txtWithhoding.Text) != 0) && (Convert.ToDecimal(txtGST.Text) == 0))
                        {
                            chk = 1; /// Tax
						}
                        else if ((Convert.ToDecimal(txtWithhoding.Text) == 0) && (Convert.ToDecimal(txtGST.Text) != 0))
                        {
                            chk = 2; /// GST
						}
                        else if ((Convert.ToDecimal(txtWithhoding.Text) != 0) && (Convert.ToDecimal(txtGST.Text) != 0))
                        {
                            chk = 3; /// Tax + GST

                        }
                    }
                    else if (Convert.ToDecimal(txtAmount.Text) != 0)
                    {
                        if ((Convert.ToDecimal(txtWithhoding.Text) == 0) && (Convert.ToDecimal(txtGST.Text) == 0))
                        {
                            chk = 4; /// CRV
						}
                        else if ((Convert.ToDecimal(txtWithhoding.Text) != 0) && (Convert.ToDecimal(txtGST.Text) == 0))
                        {
                            chk = 5; /// CRV + Tax
						}
                        else if ((Convert.ToDecimal(txtWithhoding.Text) == 0) && (Convert.ToDecimal(txtGST.Text) != 0))
                        {
                            chk = 6; /// CRV + GST
						}
                        else if ((Convert.ToDecimal(txtWithhoding.Text) != 0) && (Convert.ToDecimal(txtGST.Text) != 0))
                        {
                            chk = 7; /// CRV + Tax + GST
						}
                    }


                    //var spd = db.SettlementPlanDetails .Where (x=> x.crv.SettlementPlanId == crv)

                    //objLedgerBalance = new CTS.LedgerBalance();
                    //objLedgerBalanceDB = new CTS.LedgerBalanceDB();
                    //DataTable dtNetLedger;
                    //DataTable dtClientLedger;
                    LedgerBalance objLedgerBalance = new LedgerBalance();
                    var idctr = db.usp_IDctr("LedgerBalance").SingleOrDefault().Value;
                    objLedgerBalance.ID = Convert.ToInt32(idctr);
                    objLedgerBalance.AgencyId = int.Parse(ddlAgency.SelectedItem.Value);

                    if (ddlClient.SelectedIndex != 0)
                        objLedgerBalance.ClientId = Convert.ToInt32(ddlClient.SelectedValue);
                    else
                        objLedgerBalance.ClientId = 0;

                    // objLedgerBalance.ClientId = int.Parse(ddlClient.SelectedItem.Value);

                    objLedgerBalance.CRVID = CRVID;// CRVStatusID;
                    objLedgerBalance.CompanyID = int.Parse(ddlcompany.SelectedValue);
                    objLedgerBalance.TransactionDate = Convert.ToDateTime(txtClearDate.Text);//   CRVClearedDate; //DateTime.Now;

                    //if (((chk == 4) || (chk == 5) || (chk == 6) || (chk == 7)) && crv == false)  // CRV 
                    if (((chk == 4) || (chk == 5) || (chk == 6) || (chk == 7)) && crv == false)  // CRV 
                    {
                        // objLedgerBalance = new LedgerBalance();

                        //dtNetLedger = objLedgerBalanceDB.GetNetBalance(int.Parse(ddlAgency.SelectedItem.Value), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId));
                        //dtClientLedger = objLedgerBalanceDB.GetClientBalance(int.Parse(ddlAgency.SelectedItem.Value), int.Parse(ddlClient.SelectedItem.Value), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId));
                        objLedgerBalance.IsCRV = true;
                        objLedgerBalance.IsTax = false;
                        objLedgerBalance.BillAmount = 0;
                        objLedgerBalance.ReceiptAmount = decimal.Parse(txtAmount.Text.Trim());
                        //objLedgerBalance.NetBalance = decimal.Parse(dtNetLedger.Rows[0][0].ToString()) - decimal.Parse(txtAmount.Text.Trim());
                        //objLedgerBalance.ClientBalance = decimal.Parse(dtClientLedger.Rows[0][0].ToString()) - decimal.Parse(txtAmount.Text.Trim());
                        objLedgerBalance.StatusId = 2;
                        //objLedgerBalanceDB.InsertLedgerBalance(objLedgerBalance);
                        //  
                        db.LedgerBalances.Add(objLedgerBalance);
                        db.SaveChanges();
                        crv = true;
                    }

                    if (((chk == 1) || (chk == 3) || (chk == 5) || (chk == 7)) && tax == false)  // Tax
                    {
                        // objLedgerBalance = new LedgerBalance();
                        //    dtNetLedger = objLedgerBalanceDB.GetNetBalance(int.Parse(ddlAgency.SelectedItem.Value), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId));
                        //    dtClientLedger = objLedgerBalanceDB.GetClientBalance(int.Parse(ddlAgency.SelectedItem.Value), int.Parse(ddlClient.SelectedItem.Value), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId));

                        objLedgerBalance.IsCRV = false;
                        objLedgerBalance.IsTax = true;
                        objLedgerBalance.BillAmount = 0;
                        objLedgerBalance.ReceiptAmount = decimal.Parse(txtWithhoding.Text.Trim());
                        //  objLedgerBalance.NetBalance = decimal.Parse(dtNetLedger.Rows[0][0].ToString()) - decimal.Parse(txtWithhoding.Text.Trim());
                        //  objLedgerBalance.ClientBalance = decimal.Parse(dtClientLedger.Rows[0][0].ToString()) - decimal.Parse(txtWithhoding.Text.Trim());
                        objLedgerBalance.StatusId = 3;
                        // objLedgerBalanceDB.InsertLedgerBalance(objLedgerBalance);
                        //tax = true;
                        db.LedgerBalances.Add(objLedgerBalance);
                        db.SaveChanges();
                        tax = true;
                    }

                    if (((chk == 2) || (chk == 3) || (chk == 6) || (chk == 7)) && gst == false)  // GST
                    {
                        //  objLedgerBalance = new LedgerBalance();
                        //dtNetLedger = objLedgerBalanceDB.GetNetBalance(int.Parse(ddlAgency.SelectedItem.Value), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId));
                        //dtClientLedger = objLedgerBalanceDB.GetClientBalance(int.Parse(ddlAgency.SelectedItem.Value), int.Parse(ddlClient.SelectedItem.Value), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId));

                        //objLedgerBalance.IsCRV = false;
                        objLedgerBalance.IsTax = true;
                        objLedgerBalance.BillAmount = 0;
                        objLedgerBalance.ReceiptAmount = decimal.Parse(txtGST.Text.Trim());
                        //  objLedgerBalance.NetBalance = decimal.Parse(dtNetLedger.Rows[0][0].ToString()) - decimal.Parse(txtGST.Text.Trim());
                        //  objLedgerBalance.ClientBalance = decimal.Parse(dtClientLedger.Rows[0][0].ToString()) - decimal.Parse(txtGST.Text.Trim());
                        objLedgerBalance.StatusId = 10;
                        // objLedgerBalanceDB.InsertLedgerBalance(objLedgerBalance);
                        // gst = true;
                        db.LedgerBalances.Add(objLedgerBalance);
                        db.SaveChanges();
                        gst = true;
                    }

                    // lblmessage.Text = "CRV Updated Successfully ";
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            txtAmount.Text = "0";
            txtGST.Text = "0";
            txtWithhoding.Text = "0";
            txtBank.Text = string.Empty;
            ddlGroupAgency.SelectedIndex = 0;
            ddlAgency.SelectedIndex = 0;
            ddlClient.SelectedIndex = 0;
            txtChequeNo.Text = string.Empty;
            dtCRVDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dtChequeDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlPaymentMode.SelectedIndex = 0;
            ddlCRVStatus.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
            txtBranch.Text = string.Empty;
            btnSave.Text = "Save";
            divClearDate.Visible = false;

        }

        protected void ddlCRVStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCRVStatus.SelectedItem.Text.Equals("Cleared"))
            {
                divClearDate.Visible = true;
            }
            else
            {
                divClearDate.Visible = false;
            }
        }
        protected void ddlSearchAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int AgencyID = Convert.ToInt32(ddlSearchAgency.SelectedValue);
            //if (AgencyID == 0)
            //    {
            //    var c = db.Clients.Where(x => x.Active == true && x.Suspended == false).OrderBy(x => x.Client1).ToList();
            //    ddlSearchClient.DataValueField = "ID";
            //    ddlSearchClient.DataTextField = "Client1";
            //    ddlSearchClient.DataSource = c;
            //    ddlSearchClient.DataBind();
            //    ddlSearchClient.Items.Insert(0, new ListItem("Select Client", "0"));
            //    }
            //else
            //    {
            //    var c = db.Clients.Where(x => x.Active == true && x.Suspended == false && x.AgencyID == AgencyID).OrderBy(x => x.Client1).ToList();
            //    ddlSearchClient.DataValueField = "ID";
            //    ddlSearchClient.DataTextField = "Client1";
            //    ddlSearchClient.DataSource = c;
            //    ddlSearchClient.DataBind();
            //    ddlSearchClient.Items.Insert(0, new ListItem("Select Client", "0"));
            //    }
        }

        protected void ddlSearchClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSearchGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(ddlSearchGroup.SelectedValue);
                if (ID == 0)
                {
                    var c = db.Agencies.Where(x => x.Active == true && x.Suspended == false).ToList();
                    ddlSearchAgency.DataValueField = "ID";
                    ddlSearchAgency.DataTextField = "AgencyName";
                    ddlSearchAgency.DataSource = c;
                    ddlSearchAgency.DataBind();
                    ddlSearchAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
                }
                else
                {
                    var c = db.Agencies.Where(x => x.Active == true && x.Suspended == false && x.GroupID == ID).ToList();
                    ddlSearchAgency.DataValueField = "ID";
                    ddlSearchAgency.DataTextField = "AgencyName";
                    ddlSearchAgency.DataSource = c;
                    ddlSearchAgency.DataBind();
                    ddlSearchAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
                }
                ddlSearchAgency_SelectedIndexChanged(null, null);
            }
            catch (Exception)
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            btnCancel_Click(null, null);
            if (ddlSearchAgency.SelectedIndex > 0 || ddlSearchClient.SelectedIndex > 0 ||
                txtSearchROMODateFrom.Text.Length > 0 || txtSearchROMODateTo.Text.Length > 0)
            {
                int? AgencyID;
                DateTime? StartDate;
                DateTime? EnDate;
                if (ddlSearchAgency.SelectedIndex == 0)
                    AgencyID = null;
                else
                    AgencyID = Convert.ToInt32(ddlSearchAgency.SelectedValue);
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
                var s = db.usp_GetCRVListForEdit_New(StartDate, EnDate, AgencyID, 0).ToList();
                DataTable dt = new DataTable();
                dt = Helper.ToDataTable(s);
                ViewState["dt"] = dt;
                gv.DataSource = s;
                gv.DataBind();

            }
        }

        protected void DelButton_Click(object sender, EventArgs e)
        { }
        protected void EditButton_Click(object sender, EventArgs e)
        {

            divClearDate.Visible = true;
            ddlCRVStatus.Enabled = true;
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            double CRVAmount = 0;
            CRVAmount = Convert.ToDouble(myRow.Cells[3].Text);
            ViewState["RecordID"] = ID;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                var r = db.tblCRVs.Where(x => x.CRVId == ID).SingleOrDefault();
                if (r != null)
                {
                    try
                    {
                        if (Convert.ToBoolean(r.ShiftInCRV) == true)
                        {

                            lblmessage.Text = "Cash Receive Voucher Can't be updated";
                            return;
                        }
                        else
                        {
                            Accordion1.SelectedIndex = 1;
                            ddlcompany.SelectedValue = r.CompanyId.ToString();
                            ddlcompany.Enabled = false;
                            dtCRVDate.Text = Convert.ToDateTime(r.CRVDate).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                            txtAmount.Text = r.CRVAmount.ToString();
                            txtWithhoding.Text = r.WithHoldingTax.ToString();
                            txtGST.Text = r.GST.ToString();
                            txtBank.Text = r.Bank;
                            txtBranch.Text = r.Branch;
                            ddlCRVStatus.SelectedValue = r.Status.ToString();
                            ddlPaymentMode.SelectedValue = r.PaymentModeId.ToString();
                            ddlPaymentType.SelectedValue = r.PaymentTypeId.ToString();
                            ddlGroupAgency.SelectedValue = r.Agency.GroupID.ToString();
                            ddlGroupAgency_SelectedIndexChanged(null, null);
                            ddlAgency.SelectedValue = r.AgencyId.ToString();
                            ddlAgency_SelectedIndexChanged(null, null);

                            txtChequeNo.Text = r.PaymentNumber;
                            try
                            {
                                dtChequeDate.Text = Convert.ToDateTime(r.ChequeDate).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                            }
                            catch (Exception)
                            {

                            }


                            var crvd = db.tblCRVDetails.Where(x => x.CRVId == ID).SingleOrDefault();
                            if (crvd.ClientId != null)
                                ddlClient.SelectedValue = crvd.ClientId.ToString();
                            else
                                ddlClient.SelectedIndex = 0;
                            btnSave.Text = "Update";
                            //db.SaveChanges();

                        }
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ex.Message;
                    }
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

        }
    }
}
