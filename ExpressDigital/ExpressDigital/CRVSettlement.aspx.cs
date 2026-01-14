using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class CRVSettlement : System.Web.UI.Page
    {
        Int32 nCRVId = 0;
        Int16 Edit = 0;
        decimal mybalance = 0;
        decimal balance = 0;

        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CRVId"] != null)
            {
                nCRVId = int.Parse(Request.QueryString["CRVId"].ToString());
            }
            if (Request.QueryString["Edit"] != null)
            {
                Edit = Int16.Parse(Request.QueryString["Edit"].ToString());
                gv.Columns[7].Visible = false;
            }
            else
            {
                gv.Columns[8].Visible = false;
            }
            if (!Page.IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString["Edit"] != null)
                    {
                        Edit = Int16.Parse(Request.QueryString["Edit"].ToString());
                        if (Edit == 1)
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Invoice(s) Selection has been updated in Knock of Plan.";
                            btnSave.Visible = false;
                        }
                    }
                    if (Request.QueryString[0] != null)
                    {
                        int ID = Convert.ToInt32(Request.QueryString[0]);
                        int AgencyId = 0;

                        var s = db.usp_GetAllCRVView_Digital(ID, 0, "").SingleOrDefault();

                        if (s != null)
                        {
                            AgencyId = Convert.ToInt32(s.AgencyId);

                            lblClient.Text = s.Client;
                            lblCounsumedAmount.Text = s.ConsumedAmount.ToString();
                            lblCRVAmount.Text = s.CRVAmount.ToString();
                            lblCRVNumber.Text = s.CRVNo.ToString();
                            lblGST.Text = s.GST.ToString();
                            lblPaymentType.Text = s.PaymentType;
                            lblTaxAmount.Text = s.WithHoldingTax.ToString();
                            lblAgency.Text = s.Agency;
                            lblPaymentMode.Text = s.PaymentMode;
                            lblTotalAmount.Text = (s.CRVAmount + s.GST + s.WithHoldingTax).ToString();
                            lblRemaining.Text = ((s.CRVAmount + s.GST + s.WithHoldingTax) - s.ConsumedAmount).ToString();
                            lblBalanceAmount.Text = lblRemaining.Text;
                            ViewState["crvid"] = s.CRVId;
                            ViewState["agencyid"] = s.AgencyId;
                            ViewState["clientid"] = s.ClientId;
                            ViewState["paymenttypeid"] = s.PaymentTypeId;
                            ViewState["paymentmodeid"] = s.PaymentModeId;

                            if (Convert.ToDouble(lblCounsumedAmount.Text) < Convert.ToDouble(lblBalanceAmount.Text))
                            {
                                lblStatus.Text = "Yet Not Consumed";
                                lblStatus.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                lblStatus.Text = "CRV Fully Consumed";
                                lblStatus.ForeColor = System.Drawing.Color.Green;
                            }
                            
                            var g = db.usp_OutStanding_Inv_DN_New_Checked_new(AgencyId, null, s.CRVId).ToList();
                            gv.DataSource = g;
                            gv.DataBind();

                            //DataTable dt = Helper.ToDataTable(g);
                           // ViewState["data"] = g;
                            SumKnoup(true);
                        }
                    }
                }
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //DataTable dt = (DataTable)ViewState["dt"];
            //gv.PageIndex = e.NewPageIndex;
            //gv.DataSource = dt;
            //gv.DataBind();

            gv.PageIndex = e.NewPageIndex;

            int agencyId = (int)ViewState["agencyid"];
            int crvId = (int)ViewState["crvid"];

            var list = db.usp_OutStanding_Inv_DN_New_Checked_new(agencyId, null, crvId).ToList();

            gv.DataSource = list;
            gv.DataBind();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //CheckBox chk = (CheckBox)e.Row.FindControl("chkSelected");
                //CheckBox chkMultipleInvoices = (CheckBox)e.Row.FindControl("chkMultipleInvoices");                
                //TextBox txt = (TextBox)e.Row.FindControl("txtKnockup");
                //decimal txtBalance = Convert.ToDecimal(e.Row.Cells[4].Text);
                //if ((chk.Checked &&( Convert.ToDecimal (txt.Text) == txtBalance)))
                //{
                //    chkMultipleInvoices.Enabled = false;
                //    txt.Enabled = false;
                //}


            }
        }
        protected void txtKnockup_TextChanged(object sender, EventArgs e)
        {
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (gv.Rows.Count == 0)
            {
                lblMessage.Text = "No Invoice is Available for Settlement";
                return;
            }
            SettlementPlan sp = new SettlementPlan();
            SettlementPlanDetail spd = new SettlementPlanDetail();
            bool isInvoiceChecked = true;
            long Settlement = 0;
            int compnayid = 0;
            if (Request.QueryString["CRVId"] != null)
            {
                nCRVId = int.Parse(Request.QueryString["CRVId"].ToString());
                var com = db.tblCRVs.Where(x => x.CRVId == nCRVId).Take(1).SingleOrDefault();
                compnayid = Convert.ToInt32(com.CompanyId);
            }

            try
            {

                if (gv.Rows.Count > 0)
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        foreach (GridViewRow row in gv.Rows)
                        {
                            CheckBox chkBox;
                            chkBox = (CheckBox)(row.FindControl("chkMultipleInvoices"));
                            HiddenField hdportalid = (HiddenField)row.FindControl("hdportalid");
                            HiddenField hdclientid = (HiddenField)row.FindControl("hdclientid");
                            TextBox txt = (TextBox)row.FindControl("txtKnockup");
                            Label isdn = (Label)row.FindControl("lblIsDN");
                            if (chkBox.Checked == true)
                            {

                                int invoiceid = Convert.ToInt32(gv.DataKeys[row.RowIndex].Value);

                                //// int invoiceid = Convert.ToInt32(row.Cells[0].Text);
                                //var comid = db.InvoiceMasters.Where(x => x.ID == invoiceid).SingleOrDefault();
                                //if (companyid != null)
                                //  companyid = Convert.ToInt32(comid.CompanyID);
                                if (isInvoiceChecked == true)
                                {
                                    var s = db.usp_IDctr("SettlementPlan").SingleOrDefault();
                                    sp.ID = s.Value;
                                    sp.CRVId = (int)ViewState["crvid"];
                                    sp.AgencyId = (int)ViewState["agencyid"];
                                    sp.PaymentDate = DateTime.Now;
                                    sp.PaymentMode = lblPaymentMode.Text;

                                    sp.IsCN = false;
                                    if (ViewState["clientid"] != null)
                                        sp.CRVDetail_CRVCN_ClientId = (int)ViewState["clientid"];
                                    sp.CompanyID = compnayid;
                                    sp.ChannelID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ChannelId"]);
                                    db.SettlementPlans.Add(sp);
                                    //db.SaveChanges();
                                    isInvoiceChecked = false;
                                }

                                spd = new SettlementPlanDetail();
                                var sd = db.usp_IDctr("SettlementPlanDetail").SingleOrDefault();

                                HiddenField hdisdn = (HiddenField)row.FindControl("hdisdn");
                                spd.ID = sd.Value;
                                spd.SettlementPlanId = sp.ID;
                                spd.InvoiceId = invoiceid;// Convert.ToInt32(row.Cells[0].Text);
                                spd.CreationDate = DateTime.Now;
                                spd.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                                spd.ActiveId = 1;
                                try
                                {
                                    spd.ClientId = int.Parse(chkBox.ToolTip);// Convert.ToInt32(hdclientid.Value);
                                }
                                catch (Exception)
                                {

                                }
                                spd.AmountPaid = Convert.ToDecimal(txt.Text);
                                if (((Label)(row.FindControl("lblIsDN"))).Text.ToString() == "1")
                                {
                                    spd.IsDN = true;
                                }
                                else
                                {
                                    spd.IsDN = false;
                                }
                                spd.PortalID = 0;
                                spd.Posted = false;

                                db.SettlementPlanDetails.Add(spd);
                                AdjustCRVAmount(sp.ID, spd.AmountPaid);
                            }
                        }
                        db.SaveChanges();                        
                        scope.Complete();
                        Response.Redirect("CRVSettlement.aspx?CRVId=" + sp.CRVId + "&Edit=" + 1);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;

            }
        }
        decimal Curbalance = 0;
        decimal i = 0;
        decimal consumedamount = 0;
        private void SumKnoup(bool val)
        {

            foreach (GridViewRow row in gv.Rows)
            {
                TextBox txt = (TextBox)row.FindControl("txtKnockup");
                CheckBox chk = (CheckBox)row.FindControl("chkMultipleInvoices");
                HiddenField hdchecked = (HiddenField)row.FindControl("hdchecked");
                try
                {
                    if (Convert.ToString(hdchecked.Value) == "True")
                    {
                        gv.Rows[row.RowIndex].Cells[5].Text = txt.Text;

                        Curbalance = Curbalance + decimal.Parse(txt.Text); // total knockupamount
                        txt.Text = (decimal.Parse(gv.Rows[row.RowIndex].Cells[4].Text) - decimal.Parse(gv.Rows[row.RowIndex].Cells[5].Text)).ToString();


                        i = i + decimal.Parse(txt.Text);


                        lblBalanceAmount.Text = Curbalance.ToString();
                        lblSumofKnockplan.Text = i.ToString();// (Convert.ToDecimal(lblTotalAmount.Text) + (Convert.ToDecimal(lblBalanceAmount.Text)
                        lblRemaining.Text = (Curbalance - i).ToString();
                        lblCounsumedAmount.Text = Curbalance.ToString();
                        txt.Enabled = false;

                    }
                    else
                    {
                        if (chk.Checked == true)
                        {
                            i = i + decimal.Parse(txt.Text);
                            lblBalanceAmount.Text = (Convert.ToDecimal(lblCRVAmount.Text) - i).ToString();
                            lblSumofKnockplan.Text = i.ToString();// (Convert.ToDecimal(lblTotalAmount.Text) + (Convert.ToDecimal(lblBalanceAmount.Text)
                            TextBox txt2 = (TextBox)gv.Rows[row.RowIndex + 1].FindControl("txtKnockup");
                            txt2.Text = lblBalanceAmount.Text;


                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }

            //lblSumofKnockplan.Text = i.ToString();
            //Curbalance = decimal.Parse(lblSumofKnockplan.Text) - decimal.Parse(lblRemaining.Text);
            //if (Curbalance < 0)
            //{
            //    lblBalanceAmount.ForeColor = System.Drawing.Color.Green;
            //}
            //else
            //{
            //    lblBalanceAmount.ForeColor = System.Drawing.Color.Red;
            //}
            //lblBalanceAmount.Text = balance.ToString();
        }





        protected void btnSettlement_Click(object sender, EventArgs e)
        {

        }

        protected void chkSelected_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkMultipleInvoices_CheckedChanged(object sender, EventArgs e)
        {

            SumKnoup(false);
            //CheckBox chk = (CheckBox)sender;
            //GridViewRow myRow = (GridViewRow)chk.Parent.Parent;  // the row
            //TextBox txtKnockup = (TextBox)myRow.FindControl("txtKnockup");
            try
            {
                //    if (chk.Checked == true)
                //    {
                //        mybalance = Convert.ToDecimal(lblSumofKnockplan.Text) + Convert.ToDecimal(txtKnockup.Text);
                //        lblSumofKnockplan.Text = mybalance.ToString();
                //        lblBalanceAmount.Text = (Convert.ToDecimal(lblBalanceAmount.Text) - Convert.ToDecimal(txtKnockup.Text)).ToString();
                //        //lblSumofKnockplan.Text = mybalance.ToString();


                //        balance = decimal.Parse(lblRemaining.Text) - mybalance;
                //        if (balance < 0)
                //        {
                //            lblBalanceAmount.ForeColor = System.Drawing.Color.Green;
                //        }
                //        else
                //        {
                //            lblBalanceAmount.ForeColor = System.Drawing.Color.Red;
                //        }
                //        // ..  lblSumofKnockplan.Text = mybalance.ToString();
                //        //  lblBalanceAmount.Text = balance.ToString();
                //    }
                //    else if (chk.Checked == false)
                //    {
                //        mybalance = Convert.ToDecimal(lblSumofKnockplan.Text) - Convert.ToDecimal(txtKnockup.Text);
                //        // if (Convert.ToDecimal(lblBalanceAmount.Text) < 0)
                //        // lblBalanceAmount.Text = ( (Convert.ToDecimal(lblBalanceAmount.Text)) + Convert.ToDecimal(txtKnockup.Text)).ToString();
                //        //else
                //        lblBalanceAmount.Text = (Convert.ToDecimal(lblBalanceAmount.Text) + Convert.ToDecimal(txtKnockup.Text)).ToString();

                //        if (mybalance < 0)
                //            lblSumofKnockplan.Text = "0.00";
                //        else
                //            lblSumofKnockplan.Text = mybalance.ToString();
                //        if (balance < 0)
                //        {
                //            lblBalanceAmount.ForeColor = System.Drawing.Color.Green;
                //        }
                //        else
                //        {
                //            lblBalanceAmount.ForeColor = System.Drawing.Color.Red;
                //            //}
                //            //lblSumofKnockplan.Text = mybalance.ToString();
                //            //balance = decimal.Parse(lblBalanceAmount.Text) + Convert.ToDecimal(lblSumofKnockplan.Text);
                //            //lblBalanceAmount.Text = balance.ToString();
                //        }
                //    }
            }
            catch (Exception)
            {

            }
            finally
            {
            }
            //    if (Convert.ToDecimal(lblBalanceAmount.Text) < 0)
            //    {
            //        lblMessage.Text = string.Empty;
            //        btnSave.Enabled = false;
            //        lblMessage.Text = "Selected Amount is greater then CRV Amount";
            //    }
            //    else
            //    {
            //        lblMessage.Text = string.Empty;
            //        btnSave.Enabled = true;
            //    }
            //}


        }
        protected void AdjustCRVAmount(int iCRVId, decimal dInvoiceAmount)
        {
            decimal dRemainingAmt;
            decimal dConsumendAmt;
            var dtCRV = db.usp_GetCRVRemainingByCRV(iCRVId).SingleOrDefault();

           // DataTable dtCRV = objCRVDB.GetCRVRemainingByCRV(iCRVId);

            if (dtCRV!= null)
            {

                dConsumendAmt = decimal.Parse(dtCRV.ConsumedAmount.ToString()) + dInvoiceAmount;


                //if(decimal.Parse(dtCRV.Rows[0]["RemainingAmount"].ToString())==decimal.Parse(dtCRV.Rows[0]["RemainingAmount"].ToString()) )
                //{
                //    dRemainingAmt = decimal.Parse(dtCRV.Rows[0]["RemainingAmount"].ToString()) - dConsumendAmt;
                //}   
                //else
                //{
                //    dRemainingAmt = decimal.Parse(dtCRV.Rows[0]["CRVAmount"].ToString()) - dConsumendAmt;
                //}
                int crvdetailid = dtCRV.CRVDetailId;
                var objCRVDetail = db.tblCRVDetails.Where(x => x.CRVDetailId == crvdetailid).SingleOrDefault();

               

                if (objCRVDetail != null)
                {
                    objCRVDetail.ConsumedAmount = Math.Round(dConsumendAmt);
                    db.SaveChanges();
                   
                }

                var dtCRVConsumed = db.usp_GetCRVConsumedByCRV(iCRVId).SingleOrDefault(); ;
                dRemainingAmt = Convert.ToDecimal( dtCRVConsumed.CRVAmount - dtCRVConsumed.ConsumedAmount);
               // objCRVDetail = objCRVDetailDB.GetCRVDetail(int.Parse(dtCRV.Rows[0]["CRVDetailId"].ToString()));

                // int crvDetail = int.Parse(dtCRV.Rows[0]["CRVDetailId"].ToString());

                if (objCRVDetail != null)
                {
                    //objCRVDetail.ConsumedAmount = Math.Round(dConsumendAmt);
                    objCRVDetail.RemainingAmount = Math.Round(dRemainingAmt);
                    db.SaveChanges(); 
                }


                var objCRV = db.tblCRVs.Where (x=> x.CRVId == iCRVId).SingleOrDefault();

                if (objCRV != null)
                {
                    var  dtCRVFinalConsumed =  db.usp_GetCRVConsumedByCRV(iCRVId).SingleOrDefault();

                    if (dtCRVFinalConsumed != null)
                    {
                        if (dtCRVFinalConsumed.RemainingAmount == 0 && dtCRVFinalConsumed.CRVAmount == dtCRVFinalConsumed.ConsumedAmount)
                        {
                            //objCRV = objCRVDB.GetCRV(iCRVId);
                            //objCRVDetail = objCRVDetailDB.GetCRVDetail(crvDetail);
                            if ((objCRV != null) && (objCRVDetail != null))
                            {
                                //objCRV.IsCRVFullyConsumed = true; // IF ALL CRV AMOUNT ADJUSTED WITH INVOICE(S) AMOUNT
                                objCRV.Status = 110000005;                // SET CRV STATUS TO CLOSED 
                                objCRVDetail.IsCRVFullyConsumed = true;
                                db.SaveChanges();
                                //objCRVDetailDB.UpdateCRVDetail(objCRVDetail);
                               // objCRVDB.UpdateCRV(objCRV);
                            }
                        }
                    }
                }
            }
        }

    }
}