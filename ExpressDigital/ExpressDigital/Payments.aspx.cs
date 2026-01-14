using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{

    public partial class Payments : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        //  GLEntities db2 = new GLEntities();
        Int32 nCRVId = 0;
        #region old
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Session["UserID"] != null)
            if (((UserInfo)Session["UserObject"]) != null)
            {

            }
            else
            {
                Response.Redirect("Logout.aspx");
            }

            if (Request.QueryString["CRVId"] != null)
            {
                nCRVId = int.Parse(Request.QueryString["CRVId"].ToString());
            }

            if (!Page.IsPostBack)
            {
                PopulatePaymentType();
                PopulateCompany();
                PopulateCity();


                //  PopulateChannel(((CTS.User)(Session["UserObject"])).ChannelId, Int32.Parse(ddlCompany.SelectedItem.Value));



                LoadValues(nCRVId);


            }
            lblMsg.Visible = false;
        }

        private void PopulateMasterAgency(int cityid, int masterid, int agencyid, int clientid)
        {
            //ddlAgency.Items.Clear();
            //ddlAgency.DataSource = null;
            //ddlAgency.DataBind();
            int ID = Convert.ToInt32(ddlCity.SelectedValue);
            var _dtAgency = db.GroupAgencies.Where(x => x.CityID == ID).OrderBy(x => x.GroupName).ToList();// .Agencies.Where(x => x.GroupID == groupid).OrderBy(x => x.AgencyName).ToList();
            ddlMasterGroup.DataTextField = "GroupName";
            ddlMasterGroup.DataValueField = "ID";
            ddlMasterGroup.DataSource = _dtAgency;
            ddlMasterGroup.DataBind();
            ListItem lst = new ListItem("--- All ---", "0");
            ddlMasterGroup.Items.Insert(0, lst);

            ddlMasterGroup.SelectedValue = masterid.ToString();
            ddlMasterGroup_SelectedIndexChanged(null, null);

            ddlAgency.SelectedValue = agencyid.ToString();
            PopulateClient(agencyid, clientid);

            // ddlAgency.SelectedValue = agencyid.ToString();
        }
        public void LoadValues(int nCRVId)
        {

            //var  dt = db.usp_GetAllCRVView_Payment_3(nCRVId, "", -1, -1, null, null, "", null, null, null, null, null, 0, null, null, null, null, 0, 0,"").ToList();
            //  var dt = db.usp_GetAllCRVView_Payment_3(nCRVId, null, -1, -1, null, null, null, null, null, null, null, null, 0, null, null, null, null, null, null, null).ToList();
            var s = db.usp_AllCRVView_Digital(nCRVId, "", -1, -1, null, null, "", null, null, null, null, null, 0, null, null, null, null, 0, 0, "-1", null).SingleOrDefault();


            Int32 _cityid = 0;
            if (s != null)
            {
                var objcity = db.tblCRVs.Where(x => x.CRVId == nCRVId).FirstOrDefault();
                if (objcity != null)
                    _cityid = Convert.ToInt32(objcity.Agency.GroupAgency.CityID);

                ddlCompany.SelectedValue = s.CompanyId.ToString(); ;
                /////////////// For City /////////////////////
                ddlCity.SelectedValue = _cityid.ToString();//  s.ci .CityID.ToString();//.Items.FindByText(s.CityName) != null)                
                ddlCity_SelectedIndexChanged(null, null);
                //Int32 cityid = Convert.ToInt32(ddlCity.SelectedValue);
                // ddlAgency.SelectedValue = s.AgencyId.ToString();
                int agid = Convert.ToInt32(s.AgencyId);
                Int32 masterid = objcity.Agency.GroupAgency.ID;//   db.Agencies.Where(x => x.ID == agid).SingleOrDefault();                
                Int32 clientid = Convert.ToInt32(s.ClientId);
                PopulateMasterAgency(_cityid, masterid, agid, clientid);

                // ddlType.SelectedValue = objcity.PaymentTypeId.ToString();

                /////////////// For Agency /////////////////////
                ///





                /////////////// For Payment Type /////////////////////
                if (ddlType.Items.FindByValue(s.PaymentTypeId.ToString()) != null)
                {
                    ddlType.ClearSelection();
                    //ddlType.Items.FindByValue(s.PaymentTypeId.ToString()).Selected = true;
                    ddlType.SelectedValue = s.PaymentTypeId.ToString();

                }
                else
                {
                    ddlType.SelectedIndex = 0;
                }

                // PopulateClearedCRVDetails(nCRVId);

                if (PopulateClearedCRV(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlType.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1) == true)
                {
                    /////////////// For CRV /////////////////////
                    if (ddlCRV.Items.FindByValue(nCRVId.ToString()) != null)
                    {
                        ddlCRV.ClearSelection();
                        ddlCRV.Items.FindByValue(nCRVId.ToString()).Selected = true;

                    }
                    else
                    {
                        ddlCRV.SelectedIndex = 0;
                    }

                    PopulateClearedCRVDetails(int.Parse(ddlCRV.SelectedItem.Value));

                    PopulateInvoiceGridView(int.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1);

                    if (gvInvoices.Rows.Count != 0 && ((ddlCRV.SelectedItem != null) || (ddlCN.SelectedItem != null)))
                    {
                        btnSaveCRV.Visible = true;
                    }
                    else
                    {
                        btnSaveCRV.Visible = false;
                    }
                }


            }
        }

        protected void PopulateCompany()
        {
            var _dtCompany = db.Companies.Where(x => x.Active == true).ToList();

            ddlCompany.DataSource = _dtCompany;
            ddlCompany.DataTextField = "Company_Name";
            ddlCompany.DataValueField = "Company_Id";
            ddlCompany.DataBind();
        }
        //protected void PopulateChannel(Int32 ChannelId, Int32 companyId)
        //{
        //    objChannelDB = new CTS.ChannelDB();
        //    DataTable _dtChannel = objChannelDB.GetAllChannel(ChannelId, null, null, companyId, true);
        //    ddlChannel.DataSource = _dtChannel;
        //    ddlChannel.DataTextField = "Name";
        //    ddlChannel.DataValueField = "ChannelId";
        //    ddlChannel.DataBind();
        //}

        protected void PopulateInvoiceGridView(int AgencyId, int ClientId, int ChannelId)
        {
            if (ClientID == null || ClientId == 0)
                ClientId = -1;
            var _dtInvoice = db.usp_GetAllAgencyAndClientInvoiceWithName_Digital(AgencyId, ClientId, 0).ToList();
            gvInvoices.DataSource = _dtInvoice;
            gvInvoices.DataBind();
        }

        protected void PopulateCity()
        {
            //   _dtCity = objCityDB.GetAllCity(null, null, null, true);
            var _dtCity = db.CityManagements.Where(x => x.IsActive == true).ToList();
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "ID";
            ddlCity.DataSource = _dtCity;
            ddlCity.DataBind();
            ListItem lst = new ListItem("-- All --", "0");
            ddlCity.Items.Insert(0, lst);

            ddlCity_SelectedIndexChanged(null, null);
        }
        protected void PopulateMasterAgency(int cityid)
        {
            var s = db.GroupAgencies.Where(x => x.Active == true && x.CityID == cityid).ToList().OrderBy(x => x.GroupName);
            ddlMasterGroup.DataTextField = "GroupName";
            ddlMasterGroup.DataValueField = "ID";
            ddlMasterGroup.DataSource = s;
            ddlMasterGroup.DataBind();
            ListItem lst = new ListItem("-- All --", "0");
            ddlMasterGroup.Items.Insert(0, lst);
        }

        protected void PopulateClient(Int32 agencyId, Int32 clientID)
        {

            var _dtClient = db.Clients.Where(x => x.AgencyID == agencyId).ToList();

            ddlClient.DataTextField = "Client1";
            ddlClient.DataValueField = "ID";
            ddlClient.DataSource = _dtClient;
            ddlClient.DataBind();

            //ListItem lst = new ListItem("-- All --", "-1");
            //ddlClient.Items.Insert(0, lst);
            ListItem lst1 = new ListItem("--Not Mentioned--", "0");
            ddlClient.Items.Insert(0, lst1);
            ddlClient.SelectedValue = clientID.ToString();

        }
        protected void PopulateClient(Int32 agencyId)
        {

            var _dtClient = db.Clients.Where(x => x.AgencyID == agencyId).ToList();

            ddlClient.DataTextField = "Client1";
            ddlClient.DataValueField = "ID";
            ddlClient.DataSource = _dtClient;
            ddlClient.DataBind();

            //ListItem lst = new ListItem("-- All --", "-1");
            //ddlClient.Items.Insert(0, lst);
            ListItem lst1 = new ListItem("--Not Mentioned--", "0");
            ddlClient.Items.Insert(0, lst1);

        }
        protected void PopulatePaymentType()
        {


            var _dtPaymentType = db.PaymentTypes.Where(x => x.Active == true).ToList();
            ddlType.DataSource = _dtPaymentType;
            ddlType.DataTextField = "PaymentTypeStatus";
            ddlType.DataValueField = "ID";
            ddlType.DataBind();
            ListItem lst1 = new ListItem("--Not Mentioned--", "0");
            ddlType.Items.Insert(0, lst1);
        }
        // Populate CN Dropdown when Load CN is Checked
        protected bool PopulateCRVCN(Int32 agencyId, Int32 clientId, Int32 channelId)
        {

            var _dtCRVCN = db.usp_GetClearedCRVCN(agencyId, clientId, 1).ToList();
            ddlCN.Items.Clear();

            if (_dtCRVCN.Count() > 0)
            {
                ddlCN.DataSource = _dtCRVCN;
                ddlCN.DataTextField = "CNNO";
                ddlCN.DataValueField = "CNId";
                ddlCN.DataBind();
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool PopulateClearedCRV(Int32 agencyId, Int32 typeId, Int32 clientId, Int32 channelId)
        {
            var _dtCRV = db.usp_GetClearedCRV(agencyId, typeId, clientId).ToList();
            //  _dtCRV = objCRVDB.GetCRVRemainingByCRV);
            ddlCRV.Items.Clear();

            if (_dtCRV.Count() > 0)
            {

                ddlCRV.DataTextField = "CRVNO";
                ddlCRV.DataValueField = "CRVId";
                ddlCRV.DataSource = _dtCRV;
                ddlCRV.DataBind();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Populate the details/Summary of Credit Notes (CN)

        protected void PopulateCRVCNDetails(int cnId)
        {
            var objCRVCN = db.tblCRVCNs.Where(x => x.CNId == cnId).SingleOrDefault();

            if (objCRVCN != null)
            {

                lblCRVAmount.Text = String.Format("{0:#,##0;(#,##0);0}", Math.Round(Convert.ToDecimal(objCRVCN.CNAmount)));
                lblWHTax.Text = String.Format("{0:#,##0;(#,##0);0}", Math.Round(Convert.ToDecimal(objCRVCN.WithHoldingTax)));
                lblGST.Text = "0";
                lblTotalAmount.Text = String.Format("{0:#,##0;(#,##0);0}", Math.Round(Convert.ToDecimal(objCRVCN.CNAmount + objCRVCN.WithHoldingTax)));
                lblCRVConsumedAmount.Text = String.Format("{0:#,##0;(#,##0);0}", Math.Round(Convert.ToDecimal(objCRVCN.CNConsumedAmount)));
                lblCRVRemainingAmount.Text = String.Format("{0:#,##0;(#,##0);0}", Math.Round(Convert.ToDecimal(objCRVCN.CNRemainingAmount)));// + Math.Round(objCRV.WithHoldingTax)
                                                                                                                                             //if ((objCRV.IsCRVFullyConsumed) && (objCRV.CRVConsumedAmount == objCRV.CRVAmount))
                if (Math.Round(Convert.ToDecimal(objCRVCN.CNConsumedAmount)) == Math.Round(Convert.ToDecimal(objCRVCN.CNAmount)) + Math.Round(Convert.ToDecimal(objCRVCN.WithHoldingTax)))
                {

                    lblCRVStatus.Text = "Fully Consumed";
                    lblCRVStatus.ForeColor = System.Drawing.Color.Red;

                    //objCRVCN = new CTS.CRVCN();
                    //objCRVCNDB = new CTS.CRVCNDB();

                    var _objCRVCN = db.tblCRVCNs.Where(x => x.CNId == cnId).SingleOrDefault();// .usp_GetCRVCN(cnId

                    _objCRVCN.IsCNFullyConsumed = true; // IF ALL CRVCN AMOUNT ADJUSTED WITH INVOICE(S) AMOUNT
                    _objCRVCN.Status = 110000005;                // SET CRVCN STATUS TO CLOSE
                    db.SaveChanges();
                    //objCRVCNDB .sav.UpdateCRVCN(objCRVCN);           //UPDATE CRVCN TABLE WITH LATEST STATUS & VALUES

                }
                //else if ((!objCRV.IsCRVFullyConsumed) && (objCRV.CRVConsumedAmount == 0) && (objCRV.CRVRemainingAmount == objCRV.CRVAmount))
                else if ((!Convert.ToBoolean(objCRVCN.IsCNFullyConsumed)) && (objCRVCN.CNConsumedAmount == 0) && (Math.Round(Convert.ToDecimal(objCRVCN.CNRemainingAmount)) == Math.Round(Convert.ToDecimal(objCRVCN.CNAmount) + Convert.ToDecimal(objCRVCN.WithHoldingTax)))) //CRVCNAmount+WithHoldingTax
                {
                    lblCRVStatus.Text = "Not Consumed Yet";
                    lblCRVStatus.ForeColor = System.Drawing.Color.Green;
                }
                else if ((!Convert.ToBoolean(objCRVCN.IsCNFullyConsumed)) && (Math.Round(Convert.ToDecimal(objCRVCN.CNConsumedAmount)) < Math.Round(Convert.ToDecimal(objCRVCN.CNAmount))) && (Math.Round(Convert.ToDecimal(objCRVCN.CNRemainingAmount)) > 0))
                {
                    lblCRVStatus.Text = "Partially Consumed";
                    lblCRVStatus.ForeColor = System.Drawing.Color.Blue;
                }
            }
        }

        protected void PopulateClearedCRVDetails(int crvId)
        {
            var _dtCRVDetail = db.usp_GetCRVRemainingByCRV(crvId).SingleOrDefault();
            if (_dtCRVDetail != null)
            {

                var _dtCRVConsumed = db.usp_GetCRVConsumedByCRV(crvId).SingleOrDefault();

                lblCRVAmount.Text = String.Format("{0:#,##0;(#,##0);0}", Math.Round(decimal.Parse(_dtCRVDetail.CRVAmount.ToString())));
                lblWHTax.Text = String.Format("{0:#,##0;(#,##0);0}", Math.Round(decimal.Parse(_dtCRVDetail.WithHoldingTax.ToString())));
                lblGST.Text = String.Format("{0:#,##0;(#,##0);0}", Math.Round(decimal.Parse(_dtCRVDetail.GST.ToString())));
                lblTotalAmount.Text = String.Format("{0:#,##0;(#,##0);0}", Math.Round(decimal.Parse(_dtCRVDetail.CRVAmount.ToString()) + decimal.Parse(_dtCRVDetail.WithHoldingTax.ToString()) + decimal.Parse(_dtCRVDetail.GST.ToString())));
                lblCRVConsumedAmount.Text = String.Format("{0:#,##0;(#,##0);0}", Math.Round(decimal.Parse(_dtCRVConsumed.ConsumedAmount.ToString())));
                lblCRVRemainingAmount.Text = String.Format("{0:#,##0;(#,##0);0}", Math.Round(decimal.Parse(_dtCRVConsumed.RemainingAmount.ToString())));
                //if ( (decimal.Parse(_dtCRVDetail.Rows[0]["IsCRVFullyConsumed"].ToString())) && (decimal.Parse(_dtCRVDetail.Rows[0]["ConsumedAmount"].ToString()) == decimal.Parse(_dtCRVDetail.Rows[0]["CRVAmount"].ToString())))
                if (Math.Round(decimal.Parse(_dtCRVConsumed.ConsumedAmount.ToString())) == Math.Round(decimal.Parse(_dtCRVDetail.CRVAmount.ToString()) + decimal.Parse(_dtCRVDetail.WithHoldingTax.ToString()) + decimal.Parse(_dtCRVDetail.GST.ToString())))
                {

                    lblCRVStatus.Text = "Fully Consumed";
                    lblCRVStatus.ForeColor = System.Drawing.Color.Red;

                    var objCRV = db.tblCRVs.Where(x => x.CRVId == crvId).SingleOrDefault();
                    //objCRV.IsCRVFullyConsumed = true; // IF ALL CRV AMOUNT ADJUSTED WITH INVOICE(S) AMOUNT
                    // ddlCRV.SelectedValue = ddlCRV.Items.FindByText(objCRV.PaymentNumber).Text;
                    objCRV.Status = 110000005;                // SET CRV STATUS TO CLOSE
                    db.SaveChanges();
                    //objCRVDB.UpdateCRV(objCRV);           //UPDATE CRV TABLE WITH LATEST STATUS & VALUES

                }
                //else if ((!objCRV.IsCRVFullyConsumed) && (objCRV.CRVConsumedAmount == 0) && (objCRV.CRVRemainingAmount == objCRV.CRVAmount))
                else if ((!bool.Parse(_dtCRVDetail.IsCRVFullyConsumed.ToString())) && (Math.Round(decimal.Parse(_dtCRVDetail.ConsumedAmount.ToString())) == 0) && (Math.Round(decimal.Parse(_dtCRVConsumed.RemainingAmount.ToString())) == Math.Round(decimal.Parse(_dtCRVDetail.CRVAmount.ToString()) + decimal.Parse(_dtCRVDetail.WithHoldingTax.ToString()) + decimal.Parse(_dtCRVDetail.GST.ToString())))) //CRVAmount+WithHoldingTax+GST
                {
                    lblCRVStatus.Text = "Not Consumed Yet";
                    lblCRVStatus.ForeColor = System.Drawing.Color.Green;
                }
                else if ((!bool.Parse(_dtCRVDetail.IsCRVFullyConsumed.ToString())) && (Math.Round(decimal.Parse(_dtCRVConsumed.ConsumedAmount.ToString())) < Math.Round(decimal.Parse(_dtCRVDetail.CRVAmount.ToString()))) && (Math.Round(decimal.Parse(_dtCRVDetail.RemainingAmount.ToString())) > 0))
                {
                    lblCRVStatus.Text = "Partially Consumed";
                    lblCRVStatus.ForeColor = System.Drawing.Color.Blue;
                }
                try
                {

                }
                catch (Exception)
                {


                }
            }
        }

        protected void ClearAllLabelControls()
        {
            lblCRVAmount.Text = "";
            lblWHTax.Text = "";
            lblGST.Text = "";
            lblTotalAmount.Text = "";
            lblCRVConsumedAmount.Text = "";
            lblCRVRemainingAmount.Text = "";
            lblCRVStatus.Text = "";

            if (Request.QueryString["CRVId"] != null)
            {
                nCRVId = int.Parse(Request.QueryString["CRVId"].ToString());
                PopulateClearedCRVDetails(nCRVId);
            }


        }



        protected void btnSaveCRV_Click(object sender, EventArgs e)
        {

        }
        int iInvoiceId;
        protected void SaveValues()
        {

            //A------------------------------------------------------        
            decimal dInvoiceSumAmount = 0;
            decimal dCRVAmount = 0;
            decimal dInvoiceAmount;
            bool isInvoiceChecked = false;
            int iClientId;
            iInvoiceId = 0; ;

            int lPaymentId;
            try
            {
                //B------------------------------------------------------        

                try
                {
                    dCRVAmount = decimal.Parse(lblCRVRemainingAmount.Text);
                }
                catch (Exception e)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Selection Required! You Must Select CRV From The 'CRV List' To Process Transaction.";
                    return;
                }

                if (gvInvoices.Rows.Count > 0)
                {
                    //C------------------------------------------------------        

                    foreach (GridViewRow gvrInvoicesRow in gvInvoices.Rows)
                    {
                        CheckBox chkBox;
                        chkBox = (CheckBox)(gvrInvoicesRow.FindControl("chkMultipleInvoices"));
                        if (chkBox.Checked == true)
                        {
                            isInvoiceChecked = true;

                            if (gvrInvoicesRow.Cells[6].Text != string.Empty)
                            {
                                dInvoiceSumAmount += decimal.Parse(gvrInvoicesRow.Cells[6].Text.ToString());
                            }
                        }
                    }
                    if (isInvoiceChecked && dCRVAmount > 0)
                    {
                        //IMPLEMENTATION OF CROSS COMPARISION VALIDATION
                        /*************************************************************************************************/
                        //CASE-I 
                        //IF INVOICE SUM AMOUNT IS GREATER THAN CRV AMOUNT,  PROMPT MESSAGE  AND CONTINUE TO SETTLE PAYMENT

                        //CASE-II
                        //IF INVOICE SUM AMOUNT IS LESS THAN CRV AMOUNT, LESS CRV AMOUNT AND UPDATE INVOICE STATUS

                        //CASE-III
                        //IF INVOICE SUM AMOUNT IS EQUAL TO CRV AMOUNT, UPDATE INVOICE STATUS ISBILLED AND SET CRV STATUS TO CLOSED
                        /*************************************************************************************************/


                        /*INSERT ENTRY INTO PAYMENT TABLE*/


                        Payment objPayment = new Payment();
                        objPayment.ID = db.usp_IDctr("Payment").SingleOrDefault().Value;
                        if (cbLoadCN.Checked)
                        {
                            objPayment.CRVId = int.Parse(ddlCN.SelectedItem.Value);
                        }
                        else
                        {
                            objPayment.CRVId = int.Parse(ddlCRV.SelectedItem.Value);
                        }

                        objPayment.AgencyId = int.Parse(ddlAgency.SelectedItem.Value);
                        objPayment.PaymentDate = DateTime.Now;
                        objPayment.PaymentMode = ddlType.SelectedItem.Text;
                        objPayment.CompanyId = int.Parse(ddlCompany.SelectedItem.Value);
                        objPayment.ChannelId = 1;
                        if (cbLoadCN.Checked)
                        {
                            objPayment.IsCN = true;
                        }
                        else
                        {
                            objPayment.IsCN = false;
                        }
                        db.Payments.Add(objPayment);
                        db.SaveChanges();
                        lPaymentId = objPayment.ID;// objPaymentDB.InsertPayment(objPayment);

                        foreach (GridViewRow gvrInvoicesRow in gvInvoices.Rows)
                        {
                            /*INSERT ENTRY INTO PAYMENT DETAIL TABLE*/
                            CheckBox chkBox;
                            Label lblInv;


                            chkBox = (CheckBox)(gvrInvoicesRow.FindControl("chkMultipleInvoices"));
                            lblInv = (Label)(gvrInvoicesRow.FindControl("lblInvoiceId"));
                            HiddenField hdclientid = (HiddenField)gvrInvoicesRow.FindControl("hdclientid");

                            if ((chkBox.Checked == true) && dCRVAmount > 0)
                            {

                                if (gvrInvoicesRow.Cells[6].Text != string.Empty) //Check Total Amount must not null
                                {
                                    dInvoiceAmount = decimal.Parse(gvrInvoicesRow.Cells[6].Text.ToString());

                                    iClientId = int.Parse(hdclientid.Value);
                                    iInvoiceId = int.Parse(gvrInvoicesRow.Cells[0].Text);// int.Parse(lblInv.Text.ToString());
                                    if (((Label)(gvrInvoicesRow.FindControl("lblIsDN"))).Text.ToString() == "0")
                                    {
                                        //------------------Get Invoice Iteratively-------------------------------------------------------
                                        // objInvoiceDB = new CTS.InvoiceDB();
                                        // objInvoice = new CTS.Invoice();
                                        // var  objInvoice = db.InvoiceMasters.Where(x => x.ID == iInvoiceId).SingleOrDefault(); ;
                                    }
                                    else
                                    {
                                        //------------------Get DN Iteratively-------------------------------------------------------
                                        //objInvDNDB = new CTS.InvDNDB();
                                        //objInvDN = new CTS.InvDN();
                                        // var objInvDN = db.InvDNs.Where(x => x.ID == iInvoiceId).SingleOrDefault(); ; // iInvoiceId is DNId incase of DN
                                    }
                                    if (cbLoadCN.Checked == false)
                                    {
                                        //---------------------Get CRVS Iteratively-------------------------------------------------------
                                        //objCRV = new CTS.CRV();
                                        //objCRVDB = new CTS.CRVDB();
                                        int crvid = int.Parse(ddlCRV.SelectedItem.Value);
                                        var dtCRV = db.usp_GetCRVRemainingByCRV(crvid).SingleOrDefault();

                                        if (dtCRV != null)
                                        {
                                            dCRVAmount = Math.Round(decimal.Parse(dtCRV.RemainingAmount.ToString()));// redefining CRv Remaining Amount Iteratively
                                        }
                                        else
                                        {
                                            dCRVAmount = 0;
                                        }
                                        //-------------------------------------------------------------------------------------------------
                                    }
                                    else
                                    {
                                        //---------------------Get CN Iteratively-------------------------------------------------------
                                        //objCRVCN = new CTS.CRVCN();
                                        //objCRVCNDB = new CTS.CRVCNDB();
                                        int cnit = int.Parse(ddlCN.SelectedItem.Value);
                                        var objCRVCN = db.tblCRVCNs.Where(x => x.CNId == cnit).SingleOrDefault();
                                        dCRVAmount = Math.Round(Convert.ToDecimal(objCRVCN.CNRemainingAmount));// redefining CN Remaining Amount Iteratively
                                                                                                               //-------------------------------------------------------------------------------------------------

                                    }
                                    PaymentDetail objPaymentDetail = new PaymentDetail();
                                    int PaymentDetailID = db.usp_IDctr("PaymentDetails").SingleOrDefault().Value;
                                    objPaymentDetail.PaymentId = lPaymentId;
                                    objPaymentDetail.ClientId = iClientId;
                                    objPaymentDetail.InvoiceId = iInvoiceId;
                                    /*IF dInvoiceSumAmount IS GREATER THAN dCRVAmount*/
                                    // if (dInvoiceSumAmount > dCRVAmount)
                                    // if (dInvoiceAmount > dCRVAmount)
                                    if (((Label)(gvrInvoicesRow.FindControl("lblIsDN"))).Text.ToString() == "1")
                                    {
                                        var objInvDN = db.InvDNs.Where(x => x.ID == iInvoiceId).SingleOrDefault(); ; // iInvoiceId is DNId incase of DN
                                        // When Normal Debit Note (DN) 
                                        if (objInvDN.RemainingAmount > dCRVAmount)
                                        {

                                            objPaymentDetail.AmountPaid = dCRVAmount;
                                        }
                                        else
                                        {

                                            objPaymentDetail.AmountPaid = objInvDN.RemainingAmount;
                                        }

                                        objPaymentDetail.IsDN = true;

                                    }
                                    else
                                    {
                                        // When Normal Invoice 
                                        var objInvoice = db.InvoiceMasters.Where(x => x.ID == iInvoiceId).SingleOrDefault(); ;


                                        if (objInvoice.BalanceAmount > dCRVAmount)
                                        {

                                            objPaymentDetail.AmountPaid = dCRVAmount;
                                        }
                                        else
                                        {

                                            objPaymentDetail.AmountPaid = objInvoice.BalanceAmount;
                                        }

                                        objPaymentDetail.IsDN = false;

                                    }
                                    // objPaymentDetailDB.InsertPaymentDetail(objPaymentDetail);
                                    db.PaymentDetails.Add(objPaymentDetail);
                                    db.SaveChanges();
                                    /*UPDATE INVOICE STATUS TO ISBIILED*/

                                    if (cbLoadCN.Checked)
                                    {
                                        AdjustCNAmount(int.Parse(ddlCN.SelectedItem.Value), objPaymentDetail.AmountPaid);
                                        //------------------------------------------------------------------------
                                        PopulateCRVCNDetails(int.Parse(ddlCN.SelectedItem.Value));
                                        //------------------------------------------------------------------------
                                    }
                                    else
                                    {
                                        AdjustCRVAmount(int.Parse(ddlCRV.SelectedItem.Value), objPaymentDetail.AmountPaid);
                                        //------------------------------------------------------------------------
                                        PopulateClearedCRVDetails(int.Parse(ddlCRV.SelectedItem.Value));
                                        //------------------------------------------------------------------------
                                    }



                                    if (((Label)(gvrInvoicesRow.FindControl("lblIsDN"))).Text.ToString() == "0")
                                    {
                                        var objInvoice = db.InvoiceMasters.Where(x => x.ID == iInvoiceId).SingleOrDefault();
                                        //----------------------------- Update Invoice ---------------------------------------------------------------------------
                                        if ((dCRVAmount + objInvoice.RecivedAmount) >= objInvoice.NetReceiable)
                                        {

                                            UpdateInvoice(iInvoiceId, objInvoice.NetReceiable, 0);
                                        }
                                        else // if((dCRVAmount+objInvoice.PaidAmount) < dInvoiceAmount)
                                        {
                                            UpdateInvoice(iInvoiceId, objInvoice.RecivedAmount + dCRVAmount, objInvoice.BalanceAmount - dCRVAmount);
                                        }


                                        //--------------------------------------------------------------------------------------------------------
                                    }
                                    else
                                    {
                                        var objInvDN = db.InvDNs.Where(x => x.ID == iInvoiceId).SingleOrDefault();
                                        //-------------------------------- Update Debit Note (DN) -------------------------------------------------------
                                        if ((dCRVAmount + objInvDN.PaidAmount) >= objInvDN.TotalAmount)
                                        {
                                            UpdateInvDN(iInvoiceId, objInvDN.TotalAmount, 0);
                                        }
                                        else // if((dCRVAmount+objInvoice.PaidAmount) < dInvoiceAmount)
                                        {
                                            UpdateInvDN(iInvoiceId, objInvDN.PaidAmount + dCRVAmount, objInvDN.RemainingAmount - dCRVAmount);
                                        }
                                        // UpdateInvoice(iInvoiceId, objPaymentDetail.AmountPaid + dCRVAmount, dCRVAmount - objPaymentDetail.AmountPaid);

                                        //--------------------------------------------------------------------------------------------------------
                                    }
                                }

                                if (cbLoadCN.Checked == false)
                                {
                                    //var objInvDN = db.InvDNs.Where(x => x.ID == iInvoiceId).SingleOrDefault();
                                    var dtCRVCons = db.usp_GetCRVRemainingByCRV(int.Parse(ddlCRV.SelectedItem.Value)).SingleOrDefault();

                                    if (dtCRVCons != null)
                                    {
                                        dCRVAmount = Math.Round(decimal.Parse(dtCRVCons.RemainingAmount.ToString()));// redefining CRv Remaining Amount Iteratively
                                    }
                                    else
                                    {
                                        dCRVAmount = 0;
                                    }
                                }
                                else
                                {
                                    int crvcnid = int.Parse(ddlCN.SelectedItem.Value);
                                    var objCRVCN = db.tblCRVCNs.Where(x => x.CNId == crvcnid).SingleOrDefault();
                                    dCRVAmount = Math.Round(Convert.ToDecimal(objCRVCN.CNRemainingAmount));
                                    ddlCRV.Items.Clear();
                                }

                            }
                        }

                        //------------------------------------------------------------------------------

                        if (ddlAgency.Items.Count > 0)
                        {
                            PopulateInvoiceGridView(int.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1);   //RE-BIND INVOICE(S) DATAGRIDVIEW CONTROL
                        }

                        if (PopulateClearedCRV(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlType.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1) == true)
                        {
                            PopulateClearedCRVDetails(int.Parse(ddlCRV.SelectedItem.Value));
                        }
                        lblMsg.Visible = true;
                        lblMsg.Text = "";

                        if (dInvoiceSumAmount > dCRVAmount)
                        {
                            lblMsg.Text = "CRV Remaining Amount Is Less Than  Invoice(s) Sum Amount; However Processing Transaction ... <BR>";
                            //return;
                        }
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = lblMsg.Text.ToString() + "Transaction processed successfully!";
                        //}
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Transaction Failed! Invoice(s) Selection Is Required To Execute Transaction.";
                        return;
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Transaction Failed! Invoice(s) Selection Is Required To Execute Transaction.";
                    return;
                }

                //================================================
                if (ddlAgency.Items.Count > 0)
                {
                    PopulateInvoiceGridView(int.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1);
                }

                if (ddlType.SelectedItem != null)
                {
                    ClearAllLabelControls();

                    if (PopulateClearedCRV(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlType.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1) == true)
                    {
                        PopulateClearedCRVDetails(int.Parse(ddlCRV.SelectedItem.Value));
                    }
                }

                //================================================
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Error Occured: Payment Adjustment Process Failed.";
            }

        }

        protected void UpdateInvoiceStatus(int invoiceId)
        {

            var objInvoiceDB = db.InvoiceMasters.Where(x => x.ID == invoiceId).SingleOrDefault();// new CTS.InvoiceDB();
            objInvoiceDB.Billed = true;//.UpdateInvoiceById(invoiceId, true);
            db.SaveChanges();
        }

        // Update InvDN

        protected void UpdateInvDN(int invoiceId, Decimal PaidAmount, Decimal RemainingAmount)
        {

            var objInvDNDB = db.InvDNs.Where(x => x.InvoiceId == invoiceId).SingleOrDefault();
            objInvDNDB.PaidAmount = PaidAmount;
            objInvDNDB.RemainingAmount = RemainingAmount;
            db.SaveChanges();
            //objInvDNDB.UpdateInvDNAmount(invoiceId, true, Math.Round(PaidAmount), Math.Round(RemainingAmount));
        }

        // Update Invoice
        protected void UpdateInvoice(int invoiceId, Decimal PaidAmount, Decimal RemainingAmount)
        {
            var objInvoiceDB = db.InvoiceMasters.Where(x => x.ID == invoiceId).SingleOrDefault();
            objInvoiceDB.RecivedAmount = PaidAmount;
            objInvoiceDB.BalanceAmount = RemainingAmount;
            db.SaveChanges();


            //  objInvoiceDB = new CTS.InvoiceDB();
            //  objInvoiceDB.UpdateInvoiceAmount(invoiceId, true, Math.Round(PaidAmount), Math.Round(RemainingAmount));
        }

        //------- Adjust CN Amount ----------------
        protected void AdjustCNAmount(int iCNId, decimal dInvoiceAmount)
        {
            decimal dRemainingAmt;
            decimal dConsumendAmt;

            var objCRVCN = db.tblCRVCNs.Where(x => x.CNId == iCNId).SingleOrDefault();//  objCRVCNDB.GetCRVCN(iCNId);

            if (objCRVCN != null)
            {

                //dConsumendAmt = objCRV.CRVConsumedAmount + dInvoiceSumAmount;
                dConsumendAmt = Convert.ToDecimal(objCRVCN.CNConsumedAmount + dInvoiceAmount);

                //dRemainingAmt = objCRV.CRVAmount - dConsumendAmt;
                dRemainingAmt = Convert.ToDecimal((objCRVCN.CNAmount + objCRVCN.WithHoldingTax) - dConsumendAmt);

                //objCRVCN.CNConsumedAmount = Math.Round(dConsumendAmt);
                //objCRVCN.CNRemainingAmount = Math.Round(dRemainingAmt);

                objCRVCN.CNConsumedAmount = dConsumendAmt;
                objCRVCN.CNRemainingAmount = dRemainingAmt;

                if ((dRemainingAmt == 0) && (dConsumendAmt == objCRVCN.CNAmount))
                {
                    objCRVCN.IsCNFullyConsumed = true; // IF ALL CRV AMOUNT ADJUSTED WITH INVOICE(S) AMOUNT
                    objCRVCN.Status = 110000005;                // SET CN STATUS TO CLOSED 
                }

                db.SaveChanges();
                // objCRVCNDB.UpdateCRVCN(objCRVCN);           //UPDATE CN TABLE WITH LATEST STATUS & VALUES
            }
        }

        //------- Adjust CRV Amount ----------------

        protected void AdjustCRVAmount(int iCRVId, decimal dInvoiceAmount)
        {
            decimal dRemainingAmt;
            decimal dConsumendAmt;

            //objCRV = new CTS.CRV();
            //objCRVDB = new CTS.CRVDB();
            //objCRVDetail = new CTS.CRVDetail();
            //objCRVDetailDB = new CTS.CRVDetailDB();

            var dtCRV = db.usp_GetCRVRemainingByCRV(iCRVId).SingleOrDefault();

            if (dtCRV != null)
            {

                dConsumendAmt = decimal.Parse(dtCRV.ConsumedAmount.ToString()) + dInvoiceAmount;
                int crvDetailID = dtCRV.CRVDetailId;

                var objCRVDetail = db.tblCRVDetails.Where(x => x.CRVDetailId == crvDetailID).SingleOrDefault();// .usp_GetCRVDetail.w(( objCRVDetailDB.GetCRVDetail(int.Parse(dtCRV.Rows[0]["CRVDetailId"].ToString()));

                //  int crvDetail = int.Parse(dtCRV.Rows[0]["CRVDetailId"].ToString());

                if (objCRVDetail != null)
                {
                    objCRVDetail.ConsumedAmount = Math.Round(dConsumendAmt);
                    //objCRVDetail.RemainingAmount = Math.Round(dRemainingAmt);
                    // objCRVDetailDB.UpdateCRVDetail(objCRVDetail);
                    db.SaveChanges();
                }

                var dtCRVConsumed = db.usp_GetCRVRemainingByCRV(iCRVId).SingleOrDefault();
                dRemainingAmt = decimal.Parse(dtCRVConsumed.CRVAmount.ToString()) - decimal.Parse(dtCRVConsumed.ConsumedAmount.ToString());
                //objCRVDetail = objCRVDetailDB.GetCRVDetail(int.Parse(dtCRV.Rows[0]["CRVDetailId"].ToString()));

                // int crvDetail = int.Parse(dtCRV.Rows[0]["CRVDetailId"].ToString());

                if (objCRVDetail != null)
                {
                    //objCRVDetail.ConsumedAmount = Math.Round(dConsumendAmt);
                    objCRVDetail.RemainingAmount = Math.Round(dRemainingAmt);
                    db.SaveChanges();
                    // objCRVDetailDB.UpdateCRVDetail(objCRVDetail);
                }


                //objCRV = objCRVDB.GetCRV(iCRVId); // pending status either to update or not 
                ////if (objCRV != null)
                ////{
                ////    objCRVDB.UpdateCRV(objCRV);           //UPDATE CRV TABLE WITH LATEST STATUS & VALUES
                ////}

                var dtCRVFinalConsumed = db.usp_GetCRVRemainingByCRV(iCRVId).SingleOrDefault();
                if (dtCRVFinalConsumed != null)
                {
                    if ((decimal.Parse(dtCRVFinalConsumed.RemainingAmount.ToString()) == 0) && (decimal.Parse(dtCRVFinalConsumed.CRVAmount.ToString()) == decimal.Parse(dtCRVFinalConsumed.ConsumedAmount.ToString())))
                    {
                        //objCRV = objCRVDB.GetCRV(iCRVId);
                        //objCRVDetail = objCRVDetailDB.GetCRVDetail(crvDetail);
                        if ((dtCRV != null) && (objCRVDetail != null))
                        {
                            //objCRV.IsCRVFullyConsumed = true; // IF ALL CRV AMOUNT ADJUSTED WITH INVOICE(S) AMOUNT
                            dtCRV.Status = 110000005;                // SET CRV STATUS TO CLOSED 
                            objCRVDetail.IsCRVFullyConsumed = true;
                            db.SaveChanges();
                            //objCRVDetailDB.UpdateCRVDetail(objCRVDetail);
                            // objCRVDB.UpdateCRV(objCRV);
                        }
                    }
                }

            }
        }

        protected string FormatSuffix(string val)
        {
            string returnValue = "";
            if (val.Length == 1)
            {
                returnValue = "000000000" + val;
            }
            else if (val.Length == 2)
            {
                returnValue = "00000000" + val;
            }
            else if (val.Length == 3)
            {
                returnValue = "0000000" + val;
            }
            else if (val.Length == 4)
            {
                returnValue = "000000" + val;
            }
            else if (val.Length == 5)
            {
                returnValue = "00000" + val;
            }
            else if (val.Length == 6)
            {
                returnValue = "0000" + val;
            }
            else if (val.Length == 7)
            {
                returnValue = "000" + val;
            }
            else if (val.Length == 8)
            {
                returnValue = "00" + val;
            }
            else if (val.Length == 9)
            {
                returnValue = "0" + val;
            }
            else if (val.Length == 10)
            {
                returnValue = val;
            }
            return returnValue;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Payment.aspx");
        }



        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlAgency.SelectedItem != null)
            {
                if (!cbLoadCN.Checked)
                {
                    ClearAllLabelControls();
                    if (PopulateClearedCRV(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlType.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1) == true)
                    {
                        PopulateClearedCRVDetails(int.Parse(ddlCRV.SelectedItem.Value));

                    }
                }

            }
            if (gvInvoices.Rows.Count != 0 && ((ddlCRV.SelectedItem != null) || (ddlCN.SelectedItem != null)))
            {
                btnSaveCRV.Visible = true;
            }
            else
            {
                btnSaveCRV.Visible = false;
            }


        }
        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                PopulateClient(Int32.Parse(ddlAgency.SelectedItem.Value));

                if (ddlAgency.Items.Count > 0)
                {
                    PopulateInvoiceGridView(int.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1);
                }

                if (cbLoadCN.Checked)
                {
                    ClearAllLabelControls();

                    if (PopulateCRVCN(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1) == true)
                    {
                        PopulateCRVCNDetails(int.Parse(ddlCN.SelectedItem.Value));
                    }
                }
                else
                {

                    if (ddlType.SelectedIndex > 0)
                    {
                        ClearAllLabelControls();

                        if (PopulateClearedCRV(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlType.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1) == true)
                        {
                            PopulateClearedCRVDetails(int.Parse(ddlCRV.SelectedItem.Value));
                        }
                    }
                }

                if (gvInvoices.Rows.Count != 0 && ((ddlCRV.SelectedItem != null) || (ddlCN.SelectedItem != null)))
                {
                    btnSaveCRV.Visible = true;
                }
                else
                {
                    btnSaveCRV.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
        protected void ddlCRV_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllLabelControls();
            PopulateClearedCRVDetails(int.Parse(ddlCRV.SelectedItem.Value));
            ddlType_SelectedIndexChanged(null, null);
        }

        protected void gvInvoices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInvoices.PageIndex = e.NewPageIndex;
            if (ddlAgency.Items.Count > 0)
            {
                PopulateInvoiceGridView(int.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1);
            }

            if (ddlType.SelectedItem != null)
            {
                ClearAllLabelControls();
                if (PopulateClearedCRV(Int32.Parse(ddlAgency.SelectedItem.Value), Int16.Parse(ddlType.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1) == true)
                {
                    PopulateClearedCRVDetails(int.Parse(ddlCRV.SelectedItem.Value));
                }
            }
            // PopulateClearedCRVDetails(int.Parse(ddlCRV.SelectedItem.Value));

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            // PopulateChannel(((CTS.User)(Session["UserObject"])).ChannelId, Int32.Parse(ddlCompany.SelectedItem.Value));
        }

        protected void gvInvoices_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onMouseOver", "Highlight(this)");

                e.Row.Attributes.Add("onMouseOut", "UnHighlight(this)");

            }
        }
        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAgency.Items.Count > 0)
            {
                PopulateInvoiceGridView(int.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1);
            }


            if (cbLoadCN.Checked)
            {
                ClearAllLabelControls();

                if (PopulateCRVCN(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1) == true)
                {
                    PopulateCRVCNDetails(int.Parse(ddlCN.SelectedItem.Value));
                }
            }
            else
            {

                if (ddlType.SelectedItem != null)
                {
                    ClearAllLabelControls();
                    if (PopulateClearedCRV(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlType.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1) == true)
                    {
                        PopulateClearedCRVDetails(int.Parse(ddlCRV.SelectedItem.Value));
                    }
                }
            }

            if (gvInvoices.Rows.Count != 0 && ((ddlCRV.SelectedItem != null) || (ddlCN.SelectedItem != null)))
            {
                btnSaveCRV.Visible = true;
            }
            else
            {
                btnSaveCRV.Visible = false;
            }


        }
        protected void cbLoadCN_CheckedChanged(object sender, EventArgs e)
        {

            if (cbLoadCN.Checked)
            {
                ddlCRV.Items.Clear();

                ClearAllLabelControls();

                if (PopulateCRVCN(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1) == true)
                {
                    PopulateCRVCNDetails(int.Parse(ddlCN.SelectedItem.Value));

                    if (ddlAgency.Items.Count > 0)
                    {
                        PopulateInvoiceGridView(int.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1);
                    }
                }
            }
            else
            {
                ddlCN.Items.Clear();


                if (ddlType.SelectedItem != null)
                {
                    ClearAllLabelControls();
                    if (PopulateClearedCRV(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlType.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), 1) == true)
                    {
                        PopulateClearedCRVDetails(int.Parse(ddlCRV.SelectedItem.Value));
                    }
                }
            }

            if (gvInvoices.Rows.Count != 0 && ((ddlCRV.SelectedItem != null) || (ddlCN.SelectedItem != null)))
            {
                btnSaveCRV.Visible = true;
            }
            else
            {
                btnSaveCRV.Visible = false;
            }
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlCity.SelectedItem.Value.ToString() == "0")
            //{
            //    PopulateClient(0);

            //}
            // PopulateMasterAgency();
            //PopulateClient(Int32.Parse(ddlAgency.SelectedItem.Value));
            ////// for making null
            // gvInvoices.DataBind();
            ddlCRV.Items.Clear();
            ddlCN.Items.Clear();
            ClearAllLabelControls();

            btnSaveCRV.Visible = false;


        }
        protected void ddlCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAllLabelControls();
            PopulateCRVCNDetails(int.Parse(ddlCN.SelectedItem.Value));

        }

        protected void ddlCompany_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ddlMasterGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(ddlMasterGroup.SelectedValue);
            var _dtAgency = db.Agencies.Where(x => x.Active == true && x.GroupID == ID).OrderBy(x => x.AgencyName).ToList();
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataValueField = "ID";
            ddlAgency.DataSource = _dtAgency;
            ddlAgency.DataBind();
            if (_dtAgency.Count == 0)
            {
                ListItem lst = new ListItem("--- All ---", "0");
                ddlAgency.Items.Insert(0, lst);
            }
            ddlAgency_SelectedIndexChanged(null, null);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSaveCRV.Text == "Save")
            {
                SaveValues();
            }
            else if (btnSaveCRV.Text == "Update")
            {
                //UpdateValues(nCRVId);
                btnSaveCRV.Text = "Save";
            }
            //    Response.Redirect("CRVView.aspx");  
        }


    }
}
#endregion