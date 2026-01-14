using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class CRVInProcess : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        Int32 inProcessStatus = 110000001; //In Process - added by zubair to fill records with "In Process" Status only
        Int32 bouncedStatus = 110000002; // Bounce - added by Zubair to Save Cleared Status only 
        Int32 clearedStatus = 110000003; // Cleared - added by Zubair to Save Cleared Status only 

        public decimal TotalUnitCRVAmount;
        public decimal TotalUnitWHTax;
        public decimal TotalUnitGST;
        public decimal TotalUnitClientCRV;
        public decimal TotalUnitRemainingCRV;
        public Int32 channelid = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            channelid = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ChannelId"]);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateCRVGrid();
        }
        protected void btnClearCheque_Click(object sender, EventArgs e)
        {
            UpdateChequeStatus(true);
            lblMsg.Visible = true;
            lblMsg.Text = "Cheque No. '" + txtChqNo.Text + "' cleared successfully.";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("CRVInProcess.aspx");
        }
        protected void btnBounceCheque_Click(object sender, EventArgs e)
        {
            UpdateChequeStatus(false);
            lblMsg.Visible = true;
            lblMsg.Text = "Cheque No. '" + txtChqNo.Text + "' bounce successfully.";
        }

        protected void PopulateCRVGrid()
        {

            string chqNo = txtChqNo.Text;
            DateTime chkDate = Helper.SetDateFormat(CRVClearDatePicker.Text);
            var _dtCRV = db.usp_GetAllCRVView_4(null, "", null, null, null, null, chqNo, null, null, null, null, null, null, null, null, null, null, "").ToList();
            DataTable dt = Helper.ToDataTable(_dtCRV);
            gvCRV.DataSource = _dtCRV;
            gvCRV.DataBind();

            if (_dtCRV.Count <= 0)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Record not found.";
            }
            else
            {
                btnBounceCheque.Enabled = true;
                btnClearCheque.Enabled = true;
            }

            CheckBox cb;
            foreach (GridViewRow challanRecieved in gvCRV.Rows)
            {
                cb = (CheckBox)(challanRecieved.FindControl("CheckBox1"));
                if (cb != null)
                {
                    cb.Checked = bool.Parse(dt.Rows[challanRecieved.RowIndex]["IsChallanRecieved"].ToString());
                }
            }
        }

        private void UpdateChequeStatus(bool isCleared)
        {
            foreach (GridViewRow grd in gvCRV.Rows)
            {
                var hdCRVId = (HiddenField)grd.FindControl("hdCRVId");
                var hdClientId = (HiddenField)grd.FindControl("hdClientId");
                var hdAgencyId = (HiddenField)grd.FindControl("hdAgencyId");
                var chk = (CheckBox)(grd.FindControl("chk"));
                int crvid = Convert.ToInt32(hdCRVId.Value.ToString());

                int clientid = -1;
                if (hdClientId.Value != null)
                    try
                    {
                        clientid = Convert.ToInt32(hdClientId.Value);
                    }
                    catch (Exception)
                    {
                        clientid = -1;
                    }


                if (isCleared)
                {
                    UpdateValues(Convert.ToInt32(hdCRVId.Value), clientid, Convert.ToInt32(hdAgencyId.Value));
                }
                else
                {
                    var objCRV = db.tblCRVs.Where(x => x.CRVId == crvid).SingleOrDefault();
                    objCRV.Status = bouncedStatus; //Bounce Status
                    db.SaveChanges();
                    db.usp_MakePlan_InActive(crvid);
                }
            }
            PopulateCRVGrid();
        }

        protected bool UpdateValues(int CRVId, int ClientId, int AgencyId)
        {
            bool Result = false;
            var objCRV = db.tblCRVs.Where(x => x.CRVId == CRVId).SingleOrDefault();

            //-----------Updating CRV Depending Upon Status:: EXcept Amount Other fields Updateable if Status=Cleared(3)---------------------
            if ((objCRV.Status != 110000003) && (objCRV.Status != 110000005))
            {
                objCRV.Status = 110000003;

                if (objCRV.CRVClearedDate != Helper.SetDateFormat(CRVClearDatePicker.Text))
                {
                    objCRV.CRVClearedDate = Helper.SetDateFormat(CRVClearDatePicker.Text);
                }
                objCRV.ShiftInCRV = 0;   //  For Amount Shift in Different Channel ( Future Purpose )
                objCRV.ShiftAmountInCRV = 0; //  For Amount Shift in Different Channel ( Future Purpose )
                objCRV.LastModifiedBy = ((UserInfo)Session["UserObject"]).ID;
                objCRV.LastModifiedDate = DateTime.Now;
                objCRV.CRVTest1 = 0;
                objCRV.CRVTest2 = 0;
                objCRV.CRVTest3 = 0;
                objCRV.ChannelId = channelid;

                objCRV.CRVId = CRVId;
                objCRV.CRVClearedDate = Helper.SetDateFormat(CRVClearDatePicker.Text);
                db.SaveChanges();

                SaveUpdateDetail(CRVId, Convert.ToDecimal(objCRV.CRVAmount), Convert.ToDecimal(objCRV.WithHoldingTax), Convert.ToDecimal(objCRV.GST));
                if (objCRV.Status == 110000003)
                {
                    try
                    {
                        InsertInLedger(Convert.ToInt32(objCRV.Status), objCRV.CRVId, Convert.ToDateTime(objCRV.CRVClearedDate), Convert.ToDecimal(objCRV.CRVAmount),
                        Convert.ToDecimal(objCRV.WithHoldingTax), Convert.ToDecimal(objCRV.GST), ClientId, AgencyId, channelid, Convert.ToInt32(objCRV.CompanyId));
                        Result = true;
                    }
                    catch (Exception)
                    {


                    }
                    var _dtInvoice = db.usp_OutStanding_Inv_DN_Digital(objCRV.AgencyId, ClientId, objCRV.ChannelId, objCRV.CRVId).ToList();

                    if (_dtInvoice.Count() > 0)
                    {
                        if (AutoSettled(objCRV.CRVId) == true)
                            Result = true;
                        else
                            Result = false;
                    }
                }
            }
            return Result;
        }
        public bool AutoSettled(int CRVId)
        {
            try
            {
                var s = db.usp_CRV_AutoSettled_NewDigital(CRVId);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        protected void InsertInLedger(int status, int crvId, DateTime crvdt, decimal amount, decimal taxs, decimal gstt, int clientId, int agencyId, int channelid, int companyid)
        {
            if (status == 110000003)
            {
                int chk = 0;

                if (amount == 0)
                {
                    if ((taxs == 0 && gstt == 0))
                    {
                        chk = 0;
                    }
                    else if (taxs != 0 && gstt == 0)
                    {
                        chk = 1; /// Tax
                    }
                    else if ((taxs == 0 && gstt != 0))
                    {
                        chk = 2; /// GST
                    }
                    else if ((taxs != 0 && gstt != 0))
                    {
                        chk = 3; /// Tax + GST
                    }
                }
                else if (amount != 0)
                {
                    if ((taxs == 0) && (gstt == 0))
                    {
                        chk = 4; /// CRV
                    }
                    else if ((taxs != 0) && (gstt == 0))
                    {
                        chk = 5; /// CRV + Tax
                    }
                    else if ((taxs == 0) && (gstt != 0))
                    {
                        chk = 6; /// CRV + GST
                    }
                    else if ((taxs != 0) && (gstt != 0))
                    {
                        chk = 7; /// CRV + Tax + GST
                    }
                }

                bool crv = false;
                bool tax = false;
                bool gst = false;


                if (((chk == 4) || (chk == 5) || (chk == 6) || (chk == 7)) && crv == false)  // CRV 
                {
                    var objLedgerBalance = new LedgerBalance();
                    objLedgerBalance.ID = db.usp_IDctr("LedgerBalance").SingleOrDefault().Value;
                    objLedgerBalance.AgencyId = agencyId;
                    objLedgerBalance.ClientId = clientId;
                    objLedgerBalance.CRVID = crvId;
                    objLedgerBalance.CompanyID = companyid;
                    objLedgerBalance.ChannelID = channelid;
                    objLedgerBalance.TransactionDate = crvdt; //DateTime.Now;
                    objLedgerBalance.IsCRV = true;
                    objLedgerBalance.IsTax = false;
                    objLedgerBalance.BillAmount = 0;
                    objLedgerBalance.ReceiptAmount = amount;

                    var dtNetLedger = db.GetAgencyBalance(companyid, agencyId).SingleOrDefault(); ;
                    var dtClientLedger = db.GetClientBalance(companyid, agencyId, clientId).SingleOrDefault();


                    if (dtNetLedger != null)
                        objLedgerBalance.NetBalance = decimal.Parse(dtNetLedger.Value.ToString()) - amount;
                    else
                        objLedgerBalance.NetBalance = amount * -1;

                    if (dtClientLedger != null)
                        objLedgerBalance.ClientBalance = decimal.Parse(dtClientLedger.Value.ToString()) - amount;
                    else
                        objLedgerBalance.ClientBalance = amount * -1;
                    objLedgerBalance.StatusId = 2;
                    db.LedgerBalances.Add(objLedgerBalance);
                    db.SaveChanges();
                    crv = true;
                }

                if (((chk == 1) || (chk == 3) || (chk == 5) || (chk == 7)) && tax == false)  // Tax
                {
                    var objLedgerBalance = new LedgerBalance();
                    objLedgerBalance.ID = db.usp_IDctr("LedgerBalance").SingleOrDefault().Value;

                    objLedgerBalance.AgencyId = agencyId; //int.Parse(ddlAgency.SelectedItem.Value);
                    objLedgerBalance.ClientId = clientId;
                    objLedgerBalance.CRVID = crvId;
                    objLedgerBalance.CompanyID = companyid;
                    objLedgerBalance.ChannelID = channelid;
                    objLedgerBalance.TransactionDate = crvdt; //DateTime.Now;
                    objLedgerBalance.IsCRV = false;
                    objLedgerBalance.IsTax = true;
                    objLedgerBalance.BillAmount = 0;
                    objLedgerBalance.ReceiptAmount = taxs;
                    objLedgerBalance.StatusId = 3;

                    var dtNetLedger = db.GetAgencyBalance(companyid, agencyId).SingleOrDefault(); ;
                    var dtClientLedger = db.GetClientBalance(companyid, agencyId, clientId).SingleOrDefault();
                    objLedgerBalance.NetBalance = decimal.Parse(dtNetLedger.Value.ToString()) - taxs;
                    objLedgerBalance.ClientBalance = decimal.Parse(dtClientLedger.Value.ToString()) - taxs;

                    db.LedgerBalances.Add(objLedgerBalance);
                    db.SaveChanges();
                    tax = true;
                }

                if (((chk == 2) || (chk == 3) || (chk == 6) || (chk == 7)) && gst == false)  // GST
                {
                    var objLedgerBalance = new LedgerBalance();
                    objLedgerBalance.ID = db.usp_IDctr("LedgerBalance").SingleOrDefault().Value;

                    objLedgerBalance.AgencyId = agencyId;
                    objLedgerBalance.ClientId = clientId;
                    objLedgerBalance.CRVID = crvId;
                    objLedgerBalance.CompanyID = companyid;
                    objLedgerBalance.ChannelID = channelid;
                    objLedgerBalance.TransactionDate = crvdt; //DateTime.Now;
                    objLedgerBalance.IsCRV = false;
                    objLedgerBalance.IsTax = true;
                    objLedgerBalance.BillAmount = 0;
                    objLedgerBalance.ReceiptAmount = gstt;
                    objLedgerBalance.StatusId = 10;

                    var dtNetLedger = db.GetAgencyBalance(companyid, agencyId).SingleOrDefault(); ;
                    var dtClientLedger = db.GetClientBalance(companyid, agencyId, clientId).SingleOrDefault();
                    objLedgerBalance.NetBalance = decimal.Parse(dtNetLedger.Value.ToString()) - gstt;
                    objLedgerBalance.ClientBalance = decimal.Parse(dtClientLedger.Value.ToString()) - gstt;

                    db.LedgerBalances.Add(objLedgerBalance);
                    db.SaveChanges();
                    gst = true;
                }
            }
        }

        protected void SaveUpdateDetail(int crvId, decimal amount, decimal tax, decimal gst)
        {
            var objCRV = db.tblCRVDetails.Where(x => x.CRVId == crvId).ToList();
            if (objCRV.Count > 0)
            {
                foreach (var objCRVDetail in objCRV)
                {
                    objCRVDetail.CRVId = crvId;
                    objCRVDetail.CRVAmount = amount + tax + gst;
                    objCRVDetail.ConsumedAmount = 0;
                    objCRVDetail.RemainingAmount = amount + tax + gst;
                    objCRVDetail.IsCRVFullyConsumed = false;
                    objCRVDetail.ShiftCRVDetailId = 0; /// First Time it should be 0 when Client Amount shift it will update
                    objCRVDetail.CreatedBy = ((UserInfo)Session["UserObject"]).ID; 
                    objCRVDetail.CreatedDate = DateTime.Now;
                    objCRVDetail.ModifiedBy = ((UserInfo)Session["UserObject"]).ID;
                    objCRVDetail.ModifiedDate = DateTime.Now;
                    objCRVDetail.test1 = 0;
                    objCRVDetail.test2 = 0;
                    objCRVDetail.test3 = 0;
                }
                db.SaveChanges();
            }
        }
        public decimal GetUnitCRVAmount(decimal CRVAmount)
        {
            TotalUnitCRVAmount += CRVAmount;
            return CRVAmount;
        }

        public decimal GetCRVAmountTotal()
        {
            return TotalUnitCRVAmount;
        }

        public decimal GetUnitWHTax(decimal WHTax)
        {
            TotalUnitWHTax += WHTax;
            return WHTax;
        }

        public decimal GetWHTaxTotal()
        {
            return TotalUnitWHTax;
        }

        public decimal GetUnitGST(decimal GST)
        {
            TotalUnitGST += GST;
            return GST;
        }

        public decimal GetGSTTotal()
        {
            return TotalUnitGST;
        }
        public decimal GetUnitClientCRV(decimal ClientCRV)
        {
            TotalUnitClientCRV += ClientCRV;
            return ClientCRV;
        }

        public decimal GetClientCRVTotal()
        {
            return TotalUnitClientCRV;
        }

        public decimal GetUnitCRVRemAmount(decimal CRVRemAmount)
        {
            TotalUnitRemainingCRV += CRVRemAmount;
            return CRVRemAmount;
        }

        public decimal GetCRVRemTotal()
        {
            return TotalUnitRemainingCRV;
        }

        public string GetCRVNo(string CRVNo)
        {
            return "'" + CRVNo + "'";
        }

        public string ChallanStatus(string CRVId)
        {
            int crvid = Convert.ToInt32(CRVId);
            var objCRV = db.tblCRVs.Where(x => x.CRVId == crvid).SingleOrDefault();
            if (objCRV.IsChallanRecieved.ToString() == "True")
            {
                return "Recieved";
            }
            else
            {
                return "Required";
            }
        }

        public bool isReversed(string status, string CRVid)
        {
            int crvid = Convert.ToInt32(CRVid);
            bool result = false;
            var objPayment = db.Payments.Where(x => x.CRVId == crvid).Take(1).SingleOrDefault();
            if (objPayment != null)
            {
                if ((objPayment.PaymentMode == "Reversed"))
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }

        public object ChallanStatusColor(string CRVId)
        {
            int crvid = Convert.ToInt32(CRVId);
            var objCRV = db.tblCRVs.Where(x => x.CRVId == crvid).SingleOrDefault();
            if (objCRV.IsChallanRecieved.ToString() == "True")
            {
                return System.Drawing.Color.Green;
            }
            else
            {
                return System.Drawing.Color.Red;
            }
        }

        public bool isCleared(string status, string CRVid)
        {
            bool result = false;
            int crvid = Convert.ToInt32(CRVid);
            var _dtPayment = db.Payments.Where(x => x.CRVId == crvid).ToList();
            if (_dtPayment.Count > 0)
            {
                foreach (var ss in _dtPayment)
                {
                    if (ss.PaymentMode == "Cleared" && _dtPayment.Count() == 0 && (_dtPayment.Count == 1))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    break;
                }

            }
            return result;
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox checkbox = (CheckBox)sender;
            //GridViewRow row = (GridViewRow)checkbox.NamingContainer;

            //objCRV = new CTS.CRV();
            //objCRVDB = new CTS.CRVDB();
            //objCRV = objCRVDB.GetCRV(int.Parse(row.Cells[0].Text.ToString()));

            //if (checkbox.Checked == true)
            //{
            //    objCRV.IsChallanRecieved = true;
            //}
            //else
            //{
            //    objCRV.IsChallanRecieved = false;
            //}
            //objCRVDB.UpdateCRV(objCRV);
        }

        protected void gvCRV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "Highlight(this)");
                e.Row.Attributes.Add("onMouseOut", "UnHighlight(this)");
            }
        }
        public object CRVRemainingStatusColor(string CRVDetailId, string CRVId)
        {
            int crvid = Convert.ToInt32(CRVId);
            int crvDetailId = Convert.ToInt32(CRVDetailId);
            var objCRV = db.tblCRVs.Where(x => x.CRVId == crvid).SingleOrDefault();
            var crvDetail = db.tblCRVDetails.Where(x => x.CRVDetailId == crvDetailId).SingleOrDefault();

            if (Math.Round(Convert.ToDecimal(objCRV.CRVAmount) + Convert.ToDecimal(objCRV.WithHoldingTax) + Convert.ToDecimal(objCRV.GST)) == Math.Round(Convert.ToDecimal(crvDetail.RemainingAmount)))
            {
                return System.Drawing.Color.Black;
            }
            else
            {
                return System.Drawing.Color.Green;
            }
        }
        protected void gvCRV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
    }
}