using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{

    public partial class PaymentReversal : System.Web.UI.Page
    {
       
        #region "Declaration"
        DbDigitalEntities db = new DbDigitalEntities();
        #endregion
        #region "Load Event"
        protected void Page_Load(object sender, EventArgs e)
        {
            //if ((Session["UserID"]) != null)
            if(((UserInfo)Session["UserObject"]) != null)
            {

            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                var s = db.Companies.Where(x=> x.Active == true ).ToList();
                ddlCompay.DataTextField = "Company_Name";
                ddlCompay.DataValueField = "Company_Id";
                ddlCompay.DataSource = s;
                ddlCompay.DataBind();
                PopulateCity();
            }

            btnSave.Attributes.Add("onclick", "javascript:if(confirm('Are you sure to reverse the selected payments ?')== false) return false;");
        }
        #endregion
        #region "Dropdown Events"
        protected void ddlCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int channelId = 1;
            int agency = int.Parse(ddlAgency.SelectedItem.Value);
            int client = int.Parse(ddlClient.SelectedItem.Value);
            string criteria = ddlCriteria.SelectedItem.Value;
            int CompanyId = Convert.ToInt32( ddlCompay.SelectedValue);
            switch (criteria)
            {
                case "CRV":
                    PopulateSelectionListForCRV(channelId, CompanyId, agency, client);
                    break;

                case "CN":
                    PopulateSelectionListForCreditNote(channelId, agency, client);
                    break;

                case "0":
                    ClearSeletionList();
                    break;
            }
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAgency();
            ddlCriteria.SelectedIndex = 0;
            ClearSeletionList();
        }
        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateClient();
            ddlCriteria.SelectedIndex = 0;
            ClearSeletionList();
        }
        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriteria.SelectedIndex = 0;
            ClearSeletionList();
        }
        #endregion
        #region "Private Method"
        private void PopulateData()
        {
            int channelId = 1;
            int agencyId = int.Parse(ddlAgency.SelectedItem.Value);
            int clientId = int.Parse(ddlClient.SelectedItem.Value);
            string criteria = ddlCriteria.SelectedItem.Value;
            string selectedListId = ddlSelectionList.SelectedValue;
            int CompanyId = Convert.ToInt32(ddlCompay.SelectedValue);
            //// Response.Redirect(string.Format("ViewPaymentsDetailReport.aspx?agyId={0}&cltId={1}&crt={2}&slnId={3}",agency,client,criteria,selectedListId));
            switch (criteria)
            {
                case "CRV":
                    //// selectedListId=;
                    PopulateCRVInformation(agencyId, clientId, Int32.Parse(selectedListId), channelId, CompanyId);
                    break;

                case "CN":
                    PopulateCNInformation(agencyId, clientId, Int32.Parse(selectedListId), channelId);
                    break;

                ////case "DN":
                ////    PopulateSelectionListForDebitNote(channelId, agency, client);
                ////    break;

                ////case "INV":
                ////    PopulateSelectionListForInvoice(channelId, agency, client);
                ////    break;

                case "0":
                    ClearSeletionList();
                    break;
            }
        }
        private void PopulateCNInformation(Int32 AgencyId, Int32 ClientId, Int32 CNId, Int32 ChannelId)
        {
           // CTS.CRVCNDB objCRVCNDB = new CTS.CRVCNDB();
           // DataTable dtCN = (CNId == 0 ? new DataTable() : objCRVCNDB.GetPaymentAgainstCN(AgencyId, ClientId, CNId, ChannelId));

            int CompanyID = Convert.ToInt32(ddlCompay.SelectedValue);
            var lstCN = db.usp_ViewByCRVForReversal(AgencyId, ClientId, CNId, CompanyID).ToList();;
            DataTable dtCN = new DataTable();
            dtCN = Helper.ToDataTable(lstCN);
            if (dtCN.Rows.Count > 0)
            {
                lblCNNo.Text = dtCN.Rows[0]["CNNO"].ToString();
                lblCNAmount.Text = dtCN.Rows[0]["CNAmount"].ToString();
                lblCNConAmount.Text = dtCN.Rows[0]["CNConsumedAmount"].ToString();
                lblCNRemAmount.Text = dtCN.Rows[0]["CNRemainingAmount"].ToString();

              

                dtCN.Columns["CNId"].ColumnName = "Id";
                gvInvoices.DataSource = dtCN;
                gvInvoices.DataBind();

                tblSave.Visible = true;
                tblGrd.Visible = true;
                lblRecMsg.Visible = false;
            }
            else
            {
                lblCNNo.Text = String.Empty;
                lblCNAmount.Text = String.Empty;
                lblCNConAmount.Text = String.Empty;
                lblCNRemAmount.Text = String.Empty;

                tblSave.Visible = false;
                tblGrd.Visible = false;
                lblRecMsg.Text = "No Record Found.";
                lblRecMsg.Visible = true;
            }
            lblCRVNo.Text = String.Empty;
            lblCRVAmount.Text = String.Empty;
            lblWHTax.Text = String.Empty;
            lblGST.Text = String.Empty;
            lblTotalAmount.Text = String.Empty;
            lblCRVConsumedAmount.Text = String.Empty;
            lblCRVRemainingAmount.Text = String.Empty;
        }
       
        private void PopulateCRVInformation(Int32 AgencyId, Int32 ClientId, Int32 CRVId, Int32 ChannelId, Int32 CompanyId)
        {
            int CompanyID = Convert.ToInt32(ddlCompay.SelectedValue);
            var lstCN = db.usp_ViewByCRVForReversal(AgencyId, ClientId, CRVId, CompanyID).ToList(); ;
            DataTable dtCRV = new DataTable();
            dtCRV = Helper.ToDataTable(lstCN);
           
          
            if (dtCRV.Rows.Count > 0)
            {
                var ShiftedInfo = db.usp_ViewByCRVForReversalShiftedClientInfo(CRVId, CompanyId).ToList();
                //DataTable dtCRVShiftedInfo = (CRVId == 0 ? new DataTable() : objCRVDB.GetViewByCRVForReversalShiftedClientInfo(CRVId, ChannelId, CompanyId));
                DataTable dtCRVShiftedInfo = new DataTable();
                dtCRVShiftedInfo = Helper.ToDataTable(ShiftedInfo);
                if (dtCRVShiftedInfo.Rows.Count > 0)
                {
                    lblCRVNo.Text = dtCRVShiftedInfo.Rows[0]["CRVNO"].ToString();
                    lblCRVAmount.Text = dtCRVShiftedInfo.Rows[0]["CRVAmount"].ToString();
                    lblWHTax.Text = dtCRVShiftedInfo.Rows[0]["WithHoldingTax"].ToString();
                    lblGST.Text = dtCRVShiftedInfo.Rows[0]["GST"].ToString();
                    lblTotalAmount.Text = dtCRVShiftedInfo.Rows[0]["TotalCRVAmount"].ToString();
                    lblCRVConsumedAmount.Text = dtCRVShiftedInfo.Rows[0]["ConsumedAmount"].ToString();
                    lblCRVRemainingAmount.Text = dtCRVShiftedInfo.Rows[0]["RemainingAmount"].ToString();
                }
                dtCRV.Columns["CRVId"].ColumnName = "Id";
                gvInvoices.DataSource = dtCRV;
                gvInvoices.DataBind();

                tblSave.Visible = true;
                tblGrd.Visible = true;
                lblRecMsg.Visible = false;
            }
            else
            {
                lblCRVNo.Text = String.Empty;
                lblCRVAmount.Text = String.Empty;
                lblWHTax.Text = String.Empty;
                lblGST.Text = String.Empty;
                lblTotalAmount.Text = String.Empty;
                lblCRVConsumedAmount.Text = String.Empty;
                lblCRVRemainingAmount.Text = String.Empty;

                tblSave.Visible = false;
                tblGrd.Visible = false;
                lblRecMsg.Text = "No Record Found.";
                lblRecMsg.Visible = true;
            }
            lblCNNo.Text = String.Empty;
            lblCNAmount.Text = String.Empty;
            lblCNConAmount.Text = String.Empty;
            lblCNRemAmount.Text = String.Empty;
        }
        private void PopulateCity()
        {
            var _dtCity = db.CityManagements.ToList();
            ddlCity.DataSource = _dtCity;
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "ID";
            ddlCity.DataBind();

            
            ListItem lst = new ListItem("------ Select ------", "0");
            ddlCity.Items.Insert(0, lst);

           

            ddlAgency.Items.Clear();
            ddlAgency.Items.Insert(0, lst);

            lst = new ListItem("------ Not Mentioned ------", "0");
            ddlClient.Items.Clear();
            ddlClient.Items.Insert(0, lst);
        }
        private void PopulateAgency()
        {
                      
            int cityid = Convert.ToInt32(ddlCity.SelectedValue);
            var _dtData = db.Agencies.Where(x => x.CityID == cityid).OrderBy(x=> x.AgencyName).ToList();
                       
            ddlAgency.DataSource = _dtData;
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataValueField = "ID";
            ddlAgency.DataBind();
            ListItem lst = new ListItem("------ Select ------", "0");
            ddlAgency.Items.Insert(0, lst);

            lst = new ListItem("------ Not Mentioned ------", "0");
            ddlClient.Items.Clear();
            ddlClient.Items.Insert(0, lst);
        }
        private void PopulateClient()
        {
         
            int agencyId = Convert.ToInt32(ddlAgency.SelectedValue);
            var _dtData = db.Clients.Where (x=> x.AgencyID == agencyId).ToList().OrderBy (x=> x.Client1);
                        
            ddlClient.DataSource = _dtData;
            ddlClient.DataTextField = "Client1";
            ddlClient.DataValueField = "ID";
            ddlClient.DataBind();
            ListItem lst = new ListItem("------ Not Mentioned ------", "0");
            ddlClient.Items.Insert(0, lst);
        }
        private void PopulateSelectionListForCRV(int channelId, int CompanyId, int agencyId, int clientId)
        {

            var _dtData = db.usp_GetPaymentByCRVForReversal(agencyId,clientId, CompanyId).ToList().OrderBy(x => x.CRVId);
                        
            ddlSelectionList.DataSource = _dtData;
            ddlSelectionList.DataTextField = "CRVNo";
            ddlSelectionList.DataValueField = "CRVId";
            ddlSelectionList.DataBind();
            ListItem lst = new ListItem("------Select------", "0");
            ddlSelectionList.Items.Insert(0, lst);
        }
        private void PopulateSelectionListForCreditNote(int channelId, int agencyId, int clientId)
        {
            
            var _dtData = db.usp_GetPaymentByCreditNote(agencyId, clientId).ToList().OrderBy(x=> x.CNNo);
                        
            ddlSelectionList.DataSource = _dtData;
            ddlSelectionList.DataTextField = "CNNo";
            ddlSelectionList.DataValueField = "CNId";
            ddlSelectionList.DataBind();
            ListItem lst = new ListItem("------ All ------", "0");
            ddlSelectionList.Items.Insert(0, lst);
        }
        private void ClearSeletionList()
        {
            ddlSelectionList.Items.Clear();
            ListItem lst = new ListItem("------ All ------", "0");
            ddlSelectionList.Items.Insert(0, lst);
        }
        #endregion
        #region "Grid Events"
        protected void gvInvoices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInvoices.PageIndex = e.NewPageIndex;

            int agencyid = Int32.Parse(ddlAgency.SelectedItem.Value);
            int clientid = Int32.Parse(ddlClient.SelectedItem.Value);
            int companyid = Int32.Parse(ddlCompay.SelectedValue);
            int crvid = Int32.Parse(ddlSelectionList.SelectedValue);
            switch (ddlCriteria.SelectedItem.Value)
            {
                
                case "CRV":

                    var lstCRV = db.usp_ViewByCRVForReversal(agencyid, clientid, crvid, companyid).ToList();

                    DataTable dtCRV = Helper.ToDataTable(lstCRV);
                    if (dtCRV.Rows.Count > 0)
                    {
                        dtCRV.Columns["CrvId"].ColumnName = "Id";
                        gvInvoices.DataSource = dtCRV;
                        gvInvoices.DataBind();
                        tblGrd.Visible = true;
                        tblSave.Visible = true;
                        lblRecMsg.Visible = false;
                    }
                    else
                    {
                        tblGrd.Visible = true;
                        tblSave.Visible = false;
                        lblRecMsg.Text = "No Record Found.";
                        lblRecMsg.Visible = true;
                    }
                    break;
                case "CN":

                    var lstCN = db.usp_PaymentAgainstCN(agencyid, clientid, crvid).ToList();


                    DataTable dtCN = Helper.ToDataTable(lstCN);
                    if (dtCN.Rows.Count > 0)
                    {
                        dtCN.Columns["CNId"].ColumnName = "Id";
                        gvInvoices.DataSource = dtCN;
                        gvInvoices.DataBind();
                        tblGrd.Visible = true;
                        tblSave.Visible = true;
                        lblRecMsg.Visible = false;
                    }
                    else
                    {
                        tblGrd.Visible = true;
                        tblSave.Visible = false;
                        lblRecMsg.Text = "No Record Found.";
                        lblRecMsg.Visible = true;
                    }
                    break;
            }


        }
        protected void gvInvoices_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onMouseOver", "Highlight(this)");

                e.Row.Attributes.Add("onMouseOut", "UnHighlight(this)");

            }
        }
        #endregion
        #region "Button Events"     
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int companyid = Convert.ToInt32(ddlCompay.SelectedValue);
            if (gvInvoices.Rows.Count > 0)
            {
                foreach (GridViewRow gvrInvoicesRow in gvInvoices.Rows)
                {
                    CheckBox chkBox;
                    chkBox = (CheckBox)(gvrInvoicesRow.FindControl("chkMultipleInvoices"));
                    if (chkBox.Checked == true)
                    {

                        Int32 CRVId = Int32.Parse(((Label)gvrInvoicesRow.FindControl("lblCRVId")).Text.Trim());
                        Boolean IsDN = ((Label)gvrInvoicesRow.FindControl("lblIsDN")).Text.Trim() == "0" ? false : true;
                        Boolean IsCN = ((Label)gvrInvoicesRow.FindControl("lblIsCN")).Text.Trim() == "0" ? false : true;
                        string Invoice_DN_No = gvrInvoicesRow.Cells[0].Text.Trim();
                        int uid = ((UserInfo)Session["UserObject"]).ID;
                        bool Isdone =false;
                        var  Status = ((byte)db.usp_UpdatePaymentReversal_N11(CRVId, IsDN, IsCN, Invoice_DN_No,uid, companyid, Isdone));                    
    
                        //Boolean Status = objCRVDB.UpdatePaymentReversal(CRVId, IsDN, IsCN, Invoice_DN_No, Int32.Parse(((CTS.User)(Session["UserObject"])).UserId.ToString()),
                          //  ((CTS.User)(Session["UserObject"])).ChannelId, ((CTS.User)(Session["UserObject"])).CompanyId);
                        if (Convert.ToBoolean(Status) == true)
                        {
                            string selectedListId = ddlSelectionList.SelectedValue;

                            if (ddlCriteria.SelectedItem.Value == "CRV")
                            {
                                PopulateSelectionListForCRV(1, int.Parse(ddlCompay.SelectedValue), int.Parse(ddlAgency.SelectedItem.Value), int.Parse(ddlClient.SelectedItem.Value));
                            }
                            else if (ddlCriteria.SelectedItem.Value == "CN")
                            {
                               PopulateSelectionListForCreditNote(1, int.Parse(ddlAgency.SelectedItem.Value), int.Parse(ddlClient.SelectedItem.Value));
                            }
                            if (ddlSelectionList.Items.FindByValue(selectedListId) != null)
                            {
                                ddlSelectionList.Items.FindByValue(selectedListId).Selected = true;
                            }
                            PopulateData();


                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OnSuccess", "alert('Operation done successfully.');", true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OnError", "alert('Operation failed.');", true);
                        }
                    }
                }
            }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            PopulateData();
        }
        #endregion

        protected void ddlSelectionList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}