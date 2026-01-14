using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using Microsoft.Reporting.WebForms;

namespace ExpressDigital
{
    public partial class Adjustments : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["ReportDoc"] != null)
                {
                    //crv.ReportSource = (ReportDocument)Session["ReportDoc"];
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                var gl = db.Companies.Where(x => x.Active == true).ToList();
                ddlcompany.DataValueField = "Company_Id";
                ddlcompany.DataTextField = "Company_Name";
                ddlcompany.DataSource = gl;
                ddlcompany.DataBind();
                ddlcompany.SelectedIndex = 0;

                btnApplySuccess.Enabled = false;
                rblDebitCreditNote.Items[0].Selected = true;
                var g = db.GroupAgencies.Where(x => x.Active == true).OrderBy(x => x.GroupName).ToList();

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


                var ad = db.AdjustmentStatus.OrderBy(x => x.ID).ToList();
                ddlReason.DataTextField = "AdjustmentStatusRemarks";
                ddlReason.DataValueField = "ID";
                ddlReason.DataSource = ad;
                ddlReason.DataBind();

            }

        }

        private void ClearData()
        {
            lblGross.Text = "0.00";
            lblAC.Text = "0.00";
            lblNetBeforeGST.Text = "0.00";
            lblGST.Text = "0.00";
            lblGrossWithGST.Text = "0.00";
            lblOtherCharges.Text = "0.00";
            lblNetReceiable.Text = "0.00";

            lblGrossCRDR.Text = "0.00";
            lblACCRDR.Text = "0.00";
            lblGSTCRDR.Text = "0.00";
            lblOTHERCRDR.Text = "0.00";
            lblNETRECDRCR.Text = "0.00";
            lblGrowwWithGSTCRDR.Text = "0.00";
            lblNetBeforeGSTCRDR.Text = "0.00";

            //lblGrossAd.Text = "0.00";
            //lblACAd.Text = "0.00";
            //lblNETBeforeGSTAd.Text = "0.00";
            //lblGSTAd.Text = "0.00";
            //lblGrossWithGSTAd.Text = "0.00";
            //lblOtherChargesAd.Text = "0.00";
            //lblNetReceiableAd.Text = "0.00";

            lblGrossCH.Text = "0.00";
            lblACCH.Text = "0.00";
            lblGSTCH.Text = "0.00";
            lblOtherChargesCH.Text = "0.00";
            lblNetReceiableCH.Text = "0.00";
            lblGrossWithGSTCH.Text = "0.00";
            lblNetBeforeGSTCH.Text = "0.00";



        }
        protected void DelButton_Click(object sender, EventArgs e)
        {

        }
        protected void ddlSearchAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            int AgencyID = Convert.ToInt32(ddlSearchAgency.SelectedValue);
            //if (AgencyID == 0)
            {

                var c = db.Clients.Where(x => x.AgencyID == AgencyID && x.Active == true && x.Suspended == false).OrderBy(x => x.Client1).ToList();
                //var c 
                // var c = db.GetClientsByAgency(AgencyID).OrderBy(x => x.Client).ToList();
                ddlSearchClient.DataValueField = "ID";
                ddlSearchClient.DataTextField = "Client1";
                ddlSearchClient.DataSource = c;
                ddlSearchClient.DataBind();
                ddlSearchClient.Items.Insert(0, new ListItem("Select Client", "0"));

            }
            //else
            //{
            //    var c = db.Clients.Where(x => x.Active == true && x.Suspended == false && x.AgencyID == AgencyID).OrderBy(x => x.Client1).ToList();
            //    ddlSearchClient.DataValueField = "ID";
            //    ddlSearchClient.DataTextField = "Client1";
            //    ddlSearchClient.DataSource = c;
            //    ddlSearchClient.DataBind();
            //    ddlSearchClient.Items.Insert(0, new ListItem("Select Client", "0"));
            //}


        }

        protected void ddlSearchGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearData();
                lstCredit.Items.Clear();
                lstInvoices.Items.Clear();
                lstCredit.Items.Clear();
                lstInvoices.Items.Clear();
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

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //DataTable dt = (DataTable)ViewState["dt"];
            //gv.PageIndex = e.NewPageIndex;
            //gv.DataSource = dt;
            //gv.DataBind();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        public class ResultForCampaign
        {
            public int ID { get; set; }
        }
        public class ResultCR
        {
            public decimal Gross { get; set; }
            public decimal AC { get; set; }
            public decimal GST { get; set; }
            public decimal OtherCharges { get; set; }
            public decimal NetReceivable { get; set; }

        }
        public class ResultDR
        {
            public decimal Gross { get; set; }
            public decimal AC { get; set; }
            public decimal GST { get; set; }
            public decimal OtherCharges { get; set; }
            public decimal NetReceivable { get; set; }

        }
        protected void btnInvoices_Click(object sender, EventArgs e)
        {
            ClearData();
            lstCredit.Items.Clear();
            lstInvoices.Items.Clear();
            if (ddlSearchAgency.SelectedIndex > 0 && ddlSearchClient.SelectedIndex > 0)
            {
                int AgencyID = Convert.ToInt32(ddlSearchAgency.SelectedValue);
                int ClientID = Convert.ToInt32(ddlSearchClient.SelectedValue);
                using (var db = new DbDigitalEntities())
                {
                    var AgParameter = new SqlParameter("@nAgencyId", AgencyID);
                    var ClientParameter = new SqlParameter("@nClientId", ClientID);
                    var result = db.Database
                        .SqlQuery<ResultForCampaign>("usp_GetInvoiceFilterByAgencyClient_ @nAgencyId,@nClientId", AgParameter, ClientParameter)
                        .ToList().Distinct();
                    lstInvoices.DataTextField = "ID";
                    lstInvoices.DataValueField = "ID";
                    lstInvoices.DataSource = result;

                    lstInvoices.DataBind();
                }

                //var s =  db.usp_GetInvoiceFilterByAgencyClient(AgencyID, ClientID);

            }
        }

        protected void btnViewInvoices_Click(object sender, EventArgs e)
        {
            ClearData();

            lblmessage.Text = string.Empty;
            ViewState["fromadjusted"] = "no";
            lblmessage.Text = string.Empty;
            int InvoiceID = 0;
            if (lstInvoices.SelectedValue == "")
            {
                lblmessage.Text = "Please select invoice to proceed";
                return;
            }
            if (lstInvoices.Items.Count != 0)
            {
                //string invoiceIds = "";
                int CountSelected = 0;
                //string invoiceNo = "";

                //for (int i = 0; i < lstInvoices.Items.Count; i++)
                //{
                //    if (lstInvoices.Items[i].Selected == true)
                //    {
                //        CountSelected = CountSelected + 1;

                //        if (invoiceIds == "")
                //        {
                //            invoiceIds = lstInvoices.Items[i].Value.ToString();
                //            invoiceNo = lstInvoices.Items[i].Text.ToString();
                //        }
                //        else
                //        {
                //            invoiceIds = invoiceIds + "," + lstInvoices.Items[i].Value.ToString();
                //            invoiceNo = invoiceNo + " , " + lstInvoices.Items[i].Text.ToString();
                //        }
                //    }
                //}

                InvoiceID = Convert.ToInt32(lstInvoices.SelectedValue);


                for (int i = 0; i < lstCredit.Items.Count; i++)
                {

                    if (lstCredit.Items[i].Selected == true)
                    {
                        CountSelected = CountSelected + 1;
                    }
                }
                if (ChkLoad.Checked == true && CountSelected == 0 && lstCredit.Items.Count > 0)
                {
                    lblmessage.Text = "You must select the Dr/Cr Number from list";
                    return;
                }



                if (ddlSearchAgency.SelectedIndex > 0 && ddlSearchClient.SelectedIndex > 0)
                {

                    int AgencyID = Convert.ToInt32(ddlSearchAgency.SelectedValue);
                    int ClientID = Convert.ToInt32(ddlSearchClient.SelectedValue);
                    int CompanyID = Convert.ToInt32(ddlcompany.SelectedValue);
                    var invoices = db.InvoiceMasters.Where(x => x.CompanyID == CompanyID && x.AgencyID == AgencyID && x.ID == InvoiceID).SingleOrDefault();
                    if (InvoiceID != null)
                    {
                        // ChkLoad.Checked = true;
                        try
                        {
                            string CrIds = "";
                            string DrIds = "";

                            if ((ChkLoad.Checked == true))// && (lstCredit.Items.Count != 0))
                            {

                                for (int i = 0; i < lstCredit.Items.Count; i++)
                                {

                                    if (lstCredit.Items[i].Selected == true)
                                    {
                                        //int _val = Convert.ToInt32(lstCredit.Items[i].Value);
                                        //var _dtCrDr = db.usp_GetCrDrAgainstInv(InvoiceID, _val).ToList();
                                        //if (_dtCrDr != null)
                                        //{
                                        //    foreach (var x in _dtCrDr)
                                        //    {
                                        //        if (bool.Parse(x.IsDebitNote.ToString()) == false)
                                        //        {
                                        //            if (CrIds == "")
                                        //            {
                                        //                CrIds = lstCredit.Items[i].Value.ToString();
                                        //            }
                                        //            else
                                        //            {
                                        //                CrIds = CrIds + "," + lstCredit.Items[i].Value.ToString();
                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            if (DrIds == "")
                                        //            {
                                        //                DrIds = lstCredit.Items[i].Value.ToString();
                                        //            }
                                        //            else
                                        //            {
                                        //                DrIds = DrIds + "," + lstCredit.Items[i].Value.ToString();
                                        //            }
                                        //        }
                                        //    }
                                        //}
                                    }
                                }


                                decimal GrossCr = 0;
                                decimal ACCR = 0;
                                decimal NetBeforeGSTCR = 0;
                                decimal GSTCR = 0;
                                decimal GrossSpotWithGSTCR = 0;
                                decimal OtherChargesCR = 0;
                                decimal NetReceivableCR = 0;

                                var _InvoiceID = new SqlParameter("@InvoiceId", InvoiceID);
                                var _CrId = new SqlParameter("@CrId", CrIds);
                                var dtCr = db.Database
                                    .SqlQuery<ResultCR>("usp_GetCrAmount @InvoiceId,@CrId", _InvoiceID, _CrId)
                                    .ToList().Distinct().SingleOrDefault();

                                if (dtCr != null)
                                {

                                    GrossCr = dtCr.Gross;
                                    ACCR = dtCr.AC;
                                    NetBeforeGSTCR = dtCr.NetReceivable - dtCr.GST;
                                    GSTCR = dtCr.GST;
                                    GrossSpotWithGSTCR = dtCr.Gross + dtCr.GST;
                                    OtherChargesCR = dtCr.OtherCharges;
                                    NetReceivableCR = dtCr.NetReceivable;
                                }

                                decimal GrossDr = 0;
                                decimal ACDR = 0;
                                decimal NetBeforeGSTDR = 0;
                                decimal GSTDR = 0;
                                decimal GrossSpotWithGSTDR = 0;
                                decimal OtherChargesDR = 0;
                                decimal NetReceivableDR = 0;

                                var __InvoiceID = new SqlParameter("@InvoiceId", InvoiceID);
                                var __DrId = new SqlParameter("@DrId", DrIds);
                                var dtDr = db.Database
                                    .SqlQuery<ResultDR>("usp_GetDrAmount @InvoiceId,@DrId", __InvoiceID, __DrId)
                                    .ToList().SingleOrDefault();
                                if (dtDr != null)
                                {
                                    GrossDr = dtDr.Gross;
                                    ACDR = dtDr.AC;
                                    NetBeforeGSTDR = dtDr.NetReceivable - dtDr.GST;
                                    GSTDR = dtDr.GST;
                                    GrossSpotWithGSTDR = dtDr.Gross + dtDr.GST;
                                    OtherChargesDR = dtDr.OtherCharges;
                                    NetReceivableDR = dtDr.NetReceivable;
                                }

                                var adj = db.Adjustments.Where(x => x.InvoiceId == InvoiceID).OrderByDescending(x => x.ID).Take(1).SingleOrDefault();

                                if (adj != null)
                                {
                                    ViewState["fromadjusted"] = "yes";
                                }
                                lblGross.Text = invoices.GrossAmount.ToString();
                                lblGrossCRDR.Text = lblGross.Text;

                                lblAC.Text = invoices.AgencyAmount.ToString();
                                lblACCRDR.Text = lblAC.Text;

                                lblNetBeforeGST.Text = (invoices.GrossAmount - invoices.GSTAmount).ToString();
                                lblNetBeforeGSTCRDR.Text = ((invoices.GrossAmount + invoices.GSTAmount) - invoices.AgencyAmount).ToString();

                                lblGST.Text = invoices.GSTAmount.ToString();
                                lblGSTCRDR.Text = lblGST.Text;

                                lblGrossWithGST.Text = Math.Round((decimal.Parse(lblGross.Text) + decimal.Parse(lblGST.Text)), 2).ToString();
                                lblGrowwWithGSTCRDR.Text = lblGrossWithGST.Text;

                                lblOtherCharges.Text = invoices.totalDiscount.ToString();
                                lblOTHERCRDR.Text = lblOtherCharges.Text;

                                lblNetReceiable.Text = ((invoices.NetReceiable + invoices.totalDiscount)).ToString();
                                lblNETRECDRCR.Text = lblNetReceiable.Text;


                                //lblNetBeforeGST.Text = (invoices.GrossAmount - invoices.GSTAmount).ToString();
                                //lblGST.Text = invoices.GSTAmount.ToString();
                                //lblGrossWithGST.Text = Math.Round((decimal.Parse(lblGross.Text) + decimal.Parse(lblGST.Text)), 2).ToString();
                                //lblOtherCharges.Text = invoices.totalDiscount.ToString();
                                //lblNetReceiable.Text = (invoices.NetReceiable + invoices.totalDiscount).ToString();

                                //lblGrossCRDR.Text = Math.Round((GrossDr - GrossCr), 2).ToString();
                                //lblACCRDR.Text = Math.Round((ACDR - ACCR), 2).ToString();
                                //lblNetBeforeGSTCRDR.Text = Math.Round(((NetBeforeGSTDR - NetBeforeGSTCR)), 2).ToString();
                                //lblGSTCRDR.Text = Math.Round((GSTDR - GSTCR), 2).ToString();
                                //lblGrowwWithGSTCRDR.Text = Math.Round(((GrossDr - GrossCr) + (GSTDR - GSTCR)), 2).ToString();
                                //lblOTHERCRDR.Text = Math.Round((OtherChargesDR - OtherChargesCR), 2).ToString();
                                //lblNETRECDRCR.Text = Math.Round((NetReceivableDR - NetReceivableCR), 2).ToString();


                            }
                            else
                            {
                                lblGross.Text = invoices.GrossAmount.ToString();
                                lblGrossCRDR.Text = lblGross.Text;

                                lblAC.Text = invoices.AgencyAmount.ToString();
                                lblACCRDR.Text = lblAC.Text;

                                lblNetBeforeGST.Text = (invoices.GrossAmount - invoices.GSTAmount).ToString();
                                lblNetBeforeGSTCRDR.Text = lblNetBeforeGST.Text;

                                lblGST.Text = invoices.GSTAmount.ToString();
                                lblGSTCRDR.Text = lblGST.Text;

                                lblGrossWithGST.Text = Math.Round((decimal.Parse(lblGross.Text) + decimal.Parse(lblGST.Text)), 2).ToString();
                                lblGrowwWithGSTCRDR.Text = lblGrossWithGST.Text;

                                lblOtherCharges.Text = invoices.totalDiscount.ToString();
                                lblOTHERCRDR.Text = lblOtherCharges.Text;

                                lblNetReceiable.Text = (invoices.NetReceiable + invoices.totalDiscount).ToString();
                                lblNETRECDRCR.Text = lblNetReceiable.Text;

                                //lblGross.Text = invoices.GrossAmount.ToString();    // Math.Round((invoices.GrossAmount * invoices..ConversionRate), 2).ToString();
                                //lblAC.Text = invoices.AgencyAmount.ToString();//.tostr  .AgencyCommission, 2).ToString();
                                //lblNetBeforeGST.Text = (invoices.NetReceiable - invoices.GSTAmount).ToString();
                                //lblGST.Text = invoices.GSTAmount.ToString();//.GSTAmoun t, 2).ToString();// _dtInvoice.Rows[0]["TotalGST"].ToString();
                                //lblGrossWithGST.Text = Math.Round((decimal.Parse(lblGross.Text) + decimal.Parse(lblGST.Text)), 2).ToString();
                                //lblOtherCharges.Text = invoices.totalDiscount.ToString();
                                //lblNetReceiable.Text = Math.Round(invoices.NetReceiable, 2).ToString();

                            }
                            //else
                            //{
                            //    lblGross.Text = invoices.GrossAmount.ToString();    // Math.Round((invoices.GrossAmount * invoices..ConversionRate), 2).ToString();
                            //    lblAC.Text = invoices.AgencyAmount.ToString();//.tostr  .AgencyCommission, 2).ToString();
                            //    lblNetBeforeGST.Text = (invoices.NetReceiable - invoices.GSTAmount).ToString();
                            //    lblGST.Text = invoices.GSTAmount.ToString();//.GSTAmoun t, 2).ToString();// _dtInvoice.Rows[0]["TotalGST"].ToString();
                            //    lblGrossWithGST.Text = Math.Round((decimal.Parse(lblGross.Text) + decimal.Parse(lblGST.Text)), 2).ToString();
                            //    lblOtherCharges.Text = invoices.totalDiscount.ToString();// ; Math.Round(invoices.dis.TotalDiscount, 2).ToString();
                            //    lblNetReceiable.Text = Math.Round(invoices.NetReceiable, 2).ToString();
                            //}
                            // }
                            // }
                            //}

                        }
                        catch (Exception ex)
                        {
                        }


                    }
                }
            }

        }

        protected void btnLumpsum_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;

            if (((decimal.Parse(txtLumSumPremium.Text) <= 0) || (chkPremium.Checked == false)) && (chkAGCommission.Checked == false) && (chkSalesTax.Checked == false))
            {
                lblmessage.Text = "Please provide premium or Sales Tax/Agency Commission";
                return;
            }


            if (decimal.Parse(txtLumSumPremium.Text) <= 0)
            {
                lblmessage.Text = "Please provide premium ";
                return;
            }

            decimal PremimiumRatio = decimal.Parse(txtLumSumPremium.Text);
            //  PremimiumRatio = decimal.Parse(lblGross.Text) - decimal.Parse(txtLumSumPremium.Text);//  //(decimal.Parse(txtLumSumPremium.Text) * 100) / decimal.Parse(lblGross.Text);

            btnApplySuccess.Enabled = true;
            int invoiceId = 0;
            int AgencyID = Convert.ToInt32(ddlSearchAgency.SelectedValue);
            try
            {
                invoiceId = Convert.ToInt32(lstInvoices.SelectedValue);
            }
            catch (Exception)
            {

            }
            decimal gstRatio = 0;
            decimal agcRatio = 0;
            agcRatio = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["agcvalue"]);

            if (ddlSearchAgency.SelectedIndex > 0 && invoiceId > 0)
            {
                // 15-09-2020 / By Munir  commission from Appconfiguration
                var s = db.InvoiceMasters.Where(x => x.AgencyID == AgencyID && x.ID == invoiceId).SingleOrDefault();
                if (s != null)
                {
                    //02092020       ViewState["portalid"] = s.PortalID;
                    var Clientgst = db.Clients.Where(x => x.ID == s.ClientID && x.AgencyID == s.AgencyID).Take(1).SingleOrDefault();
                    if (Clientgst != null)
                    {
                        //02092020        
                        gstRatio = Convert.ToDecimal(Clientgst.GSTRation);

                    }
                }
            }

            decimal _gross = 0;
            decimal _mgross = 0;
            decimal _ac = 0;
            decimal _gst = 0;
            decimal _otc = Convert.ToDecimal(txtOtherDiscount.Text);
            // lblOTHERCRDR.Text = string.Format("{0:0.00}", Math.Round(_otc, 0));
            int mEffectStatus = 0;
            #region Debit
            if (rblDebitCreditNote.SelectedIndex == 0)
            {
                if (chkPremium.Checked == true && chkSalesTax.Checked == false && chkAGCommission.Checked == false) // Ok 27082021
                {
                    mEffectStatus = 5;// only Premium
                    _mgross = decimal.Parse(lblGross.Text.ToString());

                    if (ChkLumpSump.Checked)
                    {
                        _gross = Math.Round(((_mgross + PremimiumRatio)), 0);
                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblACCRDR.Text)), 0));
                    }
                    else
                    {
                        _gross = Math.Round(((_mgross * PremimiumRatio) / 100) + _mgross, 0);
                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblACCRDR.Text)), 0));
                    }

                    lblGSTCRDR.Text = lblGST.Text;
                    lblACCRDR.Text = lblAC.Text;
                    lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));


                    lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    //(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(txtOtherDiscount.Text)));

                    ;
                    lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    // lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text))+ Convert.ToDecimal(lblGSTCH.Text) + Convert.ToDecimal(lblOtherChargesCH.Text), 0));
                    lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));



                } // premium 

                if (chkPremium.Checked == true && chkSalesTax.Checked == true && chkAGCommission.Checked == false) // Ok - 27082021
                {
                    mEffectStatus = 6;
                    _mgross = decimal.Parse(lblGross.Text.ToString());

                    if (ChkLumpSump.Checked)
                    {
                        _gross = Math.Round(((_mgross + PremimiumRatio)), 0);
                        _ac = (Convert.ToDecimal(lblAC.Text));
                        _gst = ((_gross * gstRatio) / 100);
                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + _gst - Convert.ToDecimal(lblACCRDR.Text)), 0));//.ToString();



                    }
                    else
                    {
                        _gross = Math.Round(((_mgross * PremimiumRatio) / 100) + _mgross, 0);
                        _ac = (Convert.ToDecimal(lblAC.Text));
                        _gst = ((_gross * gstRatio) / 100);

                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + _gst - Convert.ToDecimal(lblACCRDR.Text)), 0));//.ToString();


                    }



                    lblGSTCRDR.Text = string.Format("{0:0.00}", Math.Round(_gst, 0));
                    lblACCRDR.Text = string.Format("{0:0.00}", _ac);

                    lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));
                    lblOTHERCRDR.Text = lblOtherCharges.Text;

                    lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    //(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(lblOtherCharges.Text)));

                    ;
                    lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    //    lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));

                }
                // Premium + AGC
                if (chkPremium.Checked == true && chkSalesTax.Checked == false && chkAGCommission.Checked == true) // Ok-270821
                {
                    mEffectStatus = 3;// 
                    _mgross = decimal.Parse(lblGross.Text.ToString());
                    if (ChkLumpSump.Checked)
                    {
                        _gross = Math.Round(((_mgross + PremimiumRatio)), 0);
                        _ac = ((_gross * agcRatio) / 100); // 
                        _gst = Convert.ToDecimal(lblGST.Text);
                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + _gst - Convert.ToDecimal(lblACCRDR.Text)), 0));//.ToString();



                    }
                    else
                    {
                        _gross = Math.Round(((_mgross * PremimiumRatio) / 100) + _mgross, 0);
                        _gst = (Convert.ToDecimal(lblGST.Text));
                        _ac = ((_gross * agcRatio) / 100);// + Convert.ToDecimal(lblAC.Text);

                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + _gst - _ac), 0));//.ToString();


                    }

                    lblGSTCRDR.Text = string.Format("{0:0.00}", Math.Round(_gst, 0));
                    lblACCRDR.Text = string.Format("{0:0.00}", Math.Round(_ac, 0));

                    lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));
                    lblOTHERCRDR.Text = lblOtherCharges.Text;

                    lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    //(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(lblOtherCharges.Text)));

                    ;
                    lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    //lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));


                }
                if (chkPremium.Checked == true && chkSalesTax.Checked == true && chkAGCommission.Checked == true)//Ok 27082021
                {
                    mEffectStatus = 4;// All Effect

                    _mgross = decimal.Parse(lblGross.Text.ToString());

                    if (ChkLumpSump.Checked)
                    {
                        _gross = Math.Round(((_mgross + PremimiumRatio)), 0);
                        _ac = ((_gross * agcRatio) / 100);
                        _gst = ((_gross * gstRatio) / 100);
                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round(_gross + _gst - _ac), 0);//.ToString();



                    }
                    else
                    {
                        _gross = Math.Round(((_mgross * PremimiumRatio) / 100) + _mgross, 0);
                        _ac = (_gross * agcRatio) / 100;
                        _gst = (_gross * gstRatio) / 100;

                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + _gst - _ac), 0));//.ToString();


                    }



                    lblGSTCRDR.Text = string.Format("{0:0.00}", Math.Round(_gst, 0));
                    lblACCRDR.Text = string.Format("{0:0.00}", Math.Round(_ac, 0));

                    lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));
                    lblOTHERCRDR.Text = lblOtherCharges.Text;

                    lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    //(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(lblOtherCharges.Text)));

                    ;
                    lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    //  lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));

                }


                if (chkPremium.Checked == false && chkSalesTax.Checked == true && chkAGCommission.Checked == true)
                {
                    lblmessage.Text = "Not Allowed";
                    //mEffectStatus = 2;// AGC + GST 

                    //_mgross = decimal.Parse(lblGross.Text.ToString());

                    //if (ChkLumpSump.Checked)
                    //{
                    //    _gross = Math.Round(((_mgross + PremimiumRatio)), 0);
                    //    _ac = ((_gross * agcRatio) / 100);
                    //    _gst = ((_gross * gstRatio) / 100);
                    //    lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                    //    lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round(_gross + _gst - _ac), 0);//.ToString();



                    //}
                    //else
                    //{
                    //    _gross = Math.Round(((_mgross * PremimiumRatio) / 100) + _mgross, 0);
                    //    _ac = (_gross * agcRatio) / 100;
                    //    _gst = (_gross * gstRatio) / 100;

                    //    lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_mgross), 0));
                    //    lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_mgross + _gst - _ac), 0));//.ToString();


                    //}



                    //lblGSTCRDR.Text = string.Format("{0:0.00}", Math.Round(_gst, 0));
                    //lblACCRDR.Text = string.Format("{0:0.00}", Math.Round(_ac, 0));

                    //lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));
                    //lblOTHERCRDR.Text = lblOtherCharges.Text;

                    //lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    //lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    //lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    //lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    ////(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    //lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    //lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(lblOtherCharges.Text)));

                    //;
                    //lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    ////  lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    //lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));




                }

                if (chkPremium.Checked == false && chkSalesTax.Checked == true && chkAGCommission.Checked == false)
                {
                    lblmessage.Text = "Not Allowed";

                    //mEffectStatus = 1;//  only Sales Tax Debit

                    //_mgross = decimal.Parse(lblGross.Text.ToString());

                    //if (ChkLumpSump.Checked)
                    //{
                    //    _gross =  Math.Round(((_mgross )), 0);
                    //    _ac = Convert.ToDecimal (lblAC.Text) ;
                    //    _gst = ((_gross * gstRatio) / 100) + Convert.ToDecimal (txtLumSumPremium.Text);
                    //    lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                    //    lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round(_gross + _gst - _ac), 0);//.ToString();



                    //}
                    //else
                    //{
                    //    _gross = Math.Round(((_mgross)), 0);
                    //    _ac = Convert.ToDecimal(lblAC.Text);
                    //    _gst = ((_gross * gstRatio) / 100);
                    //    lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                    //    lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round(_gross + _gst - _ac), 0);//.ToString();


                    //}



                    //lblGSTCRDR.Text = string.Format("{0:0.00}", Math.Round((_gst  ), 0));
                    //lblACCRDR.Text = string.Format("{0:0.00}", Math.Round(_ac, 0));

                    //lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));
                    //lblOTHERCRDR.Text = lblOtherCharges.Text;

                    //lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    //lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    //lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    //lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    ////(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    //lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    //lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(lblOtherCharges.Text)));

                    //;
                    //lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    ////  lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    //lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));





                }
                if (chkPremium.Checked == false && chkSalesTax.Checked == false && chkAGCommission.Checked == true)
                {
                    mEffectStatus = 0;//  only Agency commission   

                    _mgross = decimal.Parse(lblGross.Text.ToString());
                    _mgross = decimal.Parse(lblGross.Text.ToString());

                    if (ChkLumpSump.Checked)
                    {
                        _gross = Math.Round(((_mgross + PremimiumRatio)), 0);
                        _ac = ((_gross * agcRatio) / 100);
                        _gst = Convert.ToDecimal(lblGST.Text);
                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round(_gross + _gst - _ac), 0);//.ToString();



                    }
                    else
                    {
                        _gross = Math.Round(_mgross, 0);
                        _ac = ((_gross * agcRatio) / 100) + Convert.ToDecimal(lblAC.Text); ;
                        _gst = Convert.ToDecimal(lblGST.Text);

                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_mgross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_mgross + _gst - _ac), 0));//.ToString();


                    }



                    lblGSTCRDR.Text = string.Format("{0:0.00}", Math.Round(_gst, 0));
                    lblACCRDR.Text = string.Format("{0:0.00}", Math.Round(_ac, 0));

                    lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));
                    lblOTHERCRDR.Text = lblOtherCharges.Text;

                    lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    //(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(lblOtherCharges.Text)));

                    ;
                    lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    //  lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));





                }


            }
            #endregion
            #region Credit
            else
            {
                if (chkPremium.Checked == true && chkSalesTax.Checked == false && chkAGCommission.Checked == false)
                {
                    mEffectStatus = 5;// only Premium
                    _mgross = decimal.Parse(lblGross.Text.ToString());

                    if (ChkLumpSump.Checked)
                    {
                        _gross = Math.Round(((_mgross - PremimiumRatio)), 0);
                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblACCRDR.Text)), 0));
                    }
                    else
                    {
                        _gross = Math.Round(_mgross - ((_mgross * PremimiumRatio) / 100), 0);
                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblACCRDR.Text)), 0));
                    }

                    lblGSTCRDR.Text = lblGST.Text;
                    lblACCRDR.Text = lblAC.Text;
                    lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));


                    lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    //(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(txtOtherDiscount.Text)));

                    ;
                    lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    // lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text))+ Convert.ToDecimal(lblGSTCH.Text) + Convert.ToDecimal(lblOtherChargesCH.Text), 0));
                    lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));



                } // premium + GST

                // Credit Note   01-01-2021  Credit Side
                if (chkPremium.Checked == true && chkSalesTax.Checked == true && chkAGCommission.Checked == false)
                {
                    mEffectStatus = 6;

                    _mgross = decimal.Parse(lblGross.Text.ToString());
                    _gross = Math.Round(((_mgross * PremimiumRatio) / 100), 0);

                    lblGrossCRDR.Text = Math.Round((_mgross - _gross), 0).ToString();

                    _ac = (Convert.ToDecimal(lblAC.Text));
                    _gst = (Convert.ToDecimal(lblGrossCRDR.Text) * gstRatio) / 100;

                    lblGSTCRDR.Text = Math.Round(_gst, 0).ToString();
                    lblACCRDR.Text = Math.Round(_ac, 0).ToString();

                    lblNetBeforeGSTCRDR.Text = (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)).ToString();
                    lblOTHERCRDR.Text = lblOtherCharges.Text;

                    lblGrowwWithGSTCRDR.Text = (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))).ToString();
                    lblNETRECDRCR.Text = ((Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))) - (Convert.ToDecimal(lblACCRDR.Text))).ToString();

                    lblGrossCH.Text = (Convert.ToDecimal(lblGross.Text) - (Convert.ToDecimal(lblGrossCRDR.Text))).ToString();
                    lblGSTCH.Text = Math.Round(Convert.ToDecimal(lblGST.Text) - Convert.ToDecimal(lblGSTCRDR.Text), 0).ToString();
                    lblACCH.Text = Math.Round(Convert.ToDecimal(lblAC.Text) - Convert.ToDecimal(lblACCRDR.Text), 0).ToString();

                    lblNetBeforeGSTCH.Text = (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))).ToString();
                    lblOtherChargesCH.Text = (Convert.ToDecimal(lblOtherCharges.Text) - Convert.ToDecimal(lblOTHERCRDR.Text)).ToString();

                    lblGrossWithGSTCH.Text = Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0).ToString();
                    lblNetReceiableCH.Text = (Convert.ToDecimal(lblGrossCH.Text) + (Convert.ToDecimal(lblGSTCH.Text))).ToString();
                }

                if (chkPremium.Checked == true && chkSalesTax.Checked == true && chkAGCommission.Checked == true)
                {
                    mEffectStatus = 4;// All Effect Credit Side
                    _mgross = decimal.Parse(lblGross.Text.ToString());

                    if (ChkLumpSump.Checked)
                    {
                        _gross = Math.Round(((_mgross - PremimiumRatio)), 0);
                        _ac = ((_gross * agcRatio) / 100);
                        _gst = ((_gross * gstRatio) / 100);
                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round(_gross + _gst - _ac), 0);//.ToString();



                    }
                    else
                    {
                        _gross = Math.Round(_mgross - ((_mgross * PremimiumRatio) / 100), 0);
                        _ac = (_gross * agcRatio) / 100;
                        _gst = (_gross * gstRatio) / 100;

                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + _gst - _ac), 0));//.ToString();


                    }



                    lblGSTCRDR.Text = string.Format("{0:0.00}", Math.Round(_gst, 0));
                    lblACCRDR.Text = string.Format("{0:0.00}", Math.Round(_ac, 0));

                    lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));
                    lblOTHERCRDR.Text = lblOtherCharges.Text;

                    lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    //(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(lblOtherCharges.Text)));

                    ;
                    lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    //  lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));

                }

                // Premium + AGC  Credit Side
                if (chkPremium.Checked == true && chkSalesTax.Checked == false && chkAGCommission.Checked == true)
                {
                    mEffectStatus = 3;// 
                    _mgross = decimal.Parse(lblGross.Text.ToString());
                    if (ChkLumpSump.Checked)
                    {
                        _gross = Math.Round(((_mgross - PremimiumRatio)), 0);
                        _ac = ((_gross * agcRatio) / 100); // 
                        _gst = Convert.ToDecimal(lblGST.Text);
                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + _gst - Convert.ToDecimal(lblACCRDR.Text)), 0));//.ToString();



                    }
                    else
                    {
                        _gross = Math.Round(_mgross - ((_mgross * PremimiumRatio) / 100), 0);
                        _gst = (Convert.ToDecimal(lblGST.Text));
                        _ac = ((Convert.ToDecimal(lblAC.Text) * agcRatio) / 100) + Convert.ToDecimal(lblAC.Text);

                        lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                        lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_gross + _gst - _ac), 0));//.ToString();


                    }

                    lblGSTCRDR.Text = string.Format("{0:0.00}", Math.Round(_gst, 0));
                    lblACCRDR.Text = string.Format("{0:0.00}", Math.Round(_ac, 0));

                    lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));
                    lblOTHERCRDR.Text = lblOtherCharges.Text;

                    lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    //(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(lblOtherCharges.Text)));

                    ;
                    lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    //lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));



                }
                if (chkPremium.Checked == false && chkSalesTax.Checked == true && chkAGCommission.Checked == true)
                {
                    lblmessage.Text = "Not Allowed";
                    //mEffectStatus = 2;// AGC + GST   Credit Side
                    //_mgross = decimal.Parse(lblGross.Text.ToString());

                    //if (ChkLumpSump.Checked)
                    //{
                    //    _gross = Math.Round(((_mgross - PremimiumRatio)), 0);
                    //    _ac = ((_gross * agcRatio) / 100);
                    //    _gst = ((_gross * gstRatio) / 100);
                    //    lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                    //    lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round(_gross + _gst - _ac), 0);//.ToString();



                    //}
                    //else
                    //{
                    //    _gross = Math.Round(_mgross - ((_mgross * PremimiumRatio) / 100) , 0);
                    //    _ac = (_gross * agcRatio) / 100;
                    //    _gst = (_gross * gstRatio) / 100;

                    //    lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_mgross), 0));
                    //    lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round((_mgross + _gst - _ac), 0));//.ToString();


                    //}



                    //lblGSTCRDR.Text = string.Format("{0:0.00}", Math.Round(_gst, 0));
                    //lblACCRDR.Text = string.Format("{0:0.00}", Math.Round(_ac, 0));

                    //lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));
                    //lblOTHERCRDR.Text = lblOtherCharges.Text;

                    //lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    //lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    //lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    //lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    ////(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    //lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    //lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(lblOtherCharges.Text)));

                    //;
                    //lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    ////  lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    //lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));




                }

                if (chkPremium.Checked == false && chkSalesTax.Checked == true && chkAGCommission.Checked == false)
                {
                    lblmessage.Text = "Not Allowed";
                    //mEffectStatus = 1;//  only Sales  Credit Side
                    //_mgross = decimal.Parse(lblGross.Text.ToString());

                    //if (ChkLumpSump.Checked)
                    //{
                    //    _gross = Math.Round(((_mgross)), 0);
                    //    _ac = Convert.ToDecimal(lblAC.Text);
                    //    _gst = Convert.ToDecimal (lblGST.Text) - ((_gross * gstRatio) / 100) + Convert.ToDecimal(txtLumSumPremium.Text);
                    //    lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                    //    lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round(_gross + _gst - _ac), 0);//.ToString();



                    //}
                    //else
                    //{
                    //    _gross = Math.Round(((_mgross)), 0);
                    //    _ac = Convert.ToDecimal(lblAC.Text);
                    //    _gst = Convert.ToDecimal(lblGST.Text) - ((_gross * gstRatio) / 100);
                    //    lblGrossCRDR.Text = string.Format("{0:0.00}", Math.Round((_gross), 0));
                    //    lblNETRECDRCR.Text = string.Format("{0:0.00}", Math.Round(_gross + _gst - _ac), 0);//.ToString();


                    //}



                    //lblGSTCRDR.Text = string.Format("{0:0.00}", Math.Round((_gst), 0));
                    //lblACCRDR.Text = string.Format("{0:0.00}", Math.Round(_ac, 0));

                    //lblNetBeforeGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - Convert.ToDecimal(lblGSTCRDR.Text)));
                    //lblOTHERCRDR.Text = lblOtherCharges.Text;

                    //lblGrowwWithGSTCRDR.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) + (Convert.ToDecimal(lblGSTCRDR.Text))));


                    //lblGrossCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCRDR.Text) - (Convert.ToDecimal(lblGross.Text))));
                    //lblACCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblACCRDR.Text) - Convert.ToDecimal(lblAC.Text), 0));
                    //lblGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGSTCRDR.Text) - Convert.ToDecimal(lblGST.Text), 0));

                    ////(Convert.ToDecimal(lblNETRECDRCR.Text) - (Convert.ToDecimal(lblNetReceiable.Text))).ToString();

                    //lblNetBeforeGSTCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGrossCH.Text) - (Convert.ToDecimal(lblGSTCH.Text))));
                    //lblOtherChargesCH.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblOTHERCRDR.Text) - Convert.ToDecimal(lblOtherCharges.Text)));

                    //;
                    //lblGrossWithGSTCH.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(lblGrossCH.Text) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    ////  lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text), 0));
                    //lblNetReceiableCH.Text = string.Format("{0:0.00}", Math.Round((Convert.ToDecimal(lblGrossCH.Text) - Convert.ToDecimal(lblACCH.Text)) + Convert.ToDecimal(lblGSTCH.Text) + _otc, 0));





                }
                if (chkPremium.Checked == false && chkSalesTax.Checked == false && chkAGCommission.Checked == true)
                {
                    mEffectStatus = 0;//  only Agency commission     Credit Side
                    lblmessage.Text = "Not Allowed";

                }

            }
            #endregion

        }
        protected void rblDebitCreditNote_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnApplySuccess_Click(object sender, EventArgs e)
        {
            int invoiceId = 0;
            try
            {
                invoiceId = Convert.ToInt32(lstInvoices.SelectedValue);
            }
            catch (Exception)
            {

            }
            LedgerBalance objLedgerBalance = new LedgerBalance();
            int AgencyID = Convert.ToInt32(ddlSearchAgency.SelectedValue);
            int AdjustmentID = 0;
            int CNID = 0;
            int companyid = 0;
            int clientid = 0;
            try
            {


                if (ddlSearchAgency.SelectedIndex > 0 && invoiceId > 0)
                {
                    //if (ChkLumpSump.Checked == true)
                    if (true)
                    {
                        var s = db.InvoiceMasters.Where(x => x.AgencyID == AgencyID && x.ID == invoiceId).SingleOrDefault();
                        if (s != null)
                        {
                            companyid = Convert.ToInt32(s.CompanyID);
                            clientid = Convert.ToInt32(s.ClientID);
                        }
                        using (System.Transactions.TransactionScope scoope = new System.Transactions.TransactionScope())
                        {
                            Adjustment objAdjustment = new Adjustment();
                            int adjustmentid = db.usp_IDctr("Adjustment").SingleOrDefault().Value;
                            objAdjustment.ID = adjustmentid;
                            AdjustmentID = objAdjustment.ID;
                            objAdjustment.AdjustmentNo = "DN" + objAdjustment.ID.ToString();
                            objAdjustment.IsDebitNote = true;
                            objAdjustment.ClientId = Convert.ToInt32(ddlSearchClient.SelectedValue);
                            objAdjustment.AgencyId = Convert.ToInt32(ddlSearchAgency.SelectedValue);
                            //  objAdjustment.PortalID = (int)s.p ViewState["portalid"];
                            objAdjustment.InvoiceId = invoiceId;
                            objAdjustment.AdjustmentStatusId = Convert.ToInt32(ddlReason.SelectedValue);
                            objAdjustment.Reason = txtReason.Text;
                            objAdjustment.CreatedDate = DateTime.Now;
                            objAdjustment.CreatedBy = ((UserInfo)Session["UserObject"]).ID;

                            DateTime adjustmentDate;
                            string[] dateFormats = { "MM/dd/yyyy", "dd/MM/yyyy", "yyyy-MM-dd" }; // Add more formats if needed
                            if (DateTime.TryParseExact(txtdate.Text, dateFormats,null, System.Globalization.DateTimeStyles.None, out adjustmentDate))
                            {
                                objAdjustment.AdjustmentDate = adjustmentDate;
                            }


                            //objAdjustment.AdjustmentDate = Convert.ToDateTime(txtdate.Text);
                            decimal NetBalance = 0;
                            decimal ClientBalance = 0;

                            var dtNetLedger = db.GetAgencyBalance(companyid, int.Parse(ddlSearchAgency.SelectedItem.Value)).SingleOrDefault();
                            if (dtNetLedger != null)
                                NetBalance = decimal.Parse(dtNetLedger.Value.ToString());
                            var dtClientLedger = db.GetClientBalance(companyid, int.Parse(ddlSearchAgency.SelectedValue), clientid).SingleOrDefault();
                            if (dtClientLedger != null)
                                ClientBalance = dtClientLedger.Value;

                            if (ViewState["fromadjusted"].ToString() == "no")
                            {


                                objAdjustment.Gross = decimal.Parse(lblGross.Text);
                                objAdjustment.GrossAdj = decimal.Parse(lblGross.Text) - decimal.Parse(lblGrossCH.Text);

                                objAdjustment.GST = decimal.Parse(lblGST.Text);
                                objAdjustment.GSTAdj = decimal.Parse(lblGST.Text) - decimal.Parse(lblGSTCH.Text);
                                objAdjustment.AgencyComm = decimal.Parse(lblAC.Text);
                                objAdjustment.AgencyCommAdj = decimal.Parse(lblAC.Text) - decimal.Parse(lblACCH.Text); ;
                                //
                                if (bool.Parse(rblDebitCreditNote.SelectedItem.Value) == false)// for credit note
                                {
                                    objAdjustment.Receiable = decimal.Parse(lblNetReceiableCH.Text);
                                    objAdjustment.ReceiableAdj = ClientBalance - Math.Abs(decimal.Parse(lblNetReceiableCH.Text)); ;
                                }
                                //

                                objAdjustment.OtherCharges  = decimal.Parse(lblOtherCharges.Text);
                                objAdjustment.OtherChargesAdj = decimal.Parse(lblOtherCharges.Text) - decimal.Parse(lblOtherChargesCH.Text); ;



                            }
                            else
                            {

                                objAdjustment.Gross = decimal.Parse(lblGross.Text);
                                objAdjustment.GrossAdj = decimal.Parse(lblGrossCH.Text);
                                objAdjustment.GST = decimal.Parse(lblGST.Text);
                                objAdjustment.GSTAdj = decimal.Parse(lblGSTCH.Text);
                                objAdjustment.AgencyComm = decimal.Parse(lblAC.Text);
                                objAdjustment.AgencyCommAdj = decimal.Parse(lblACCH.Text);
                                objAdjustment.Receiable = decimal.Parse(lblNetReceiableCH.Text);
                                objAdjustment.ReceiableAdj = decimal.Parse(lblNETRECDRCR.Text);

                                //objAdjustment.Receiable = decimal.Parse(lblNetReceiable.Text);
                                // objAdjustment.ReceiableAdj = decimal.Parse(lblNetReceiable.Text) + decimal.Parse(lblNetReceiableCH.Text); ;


                            }
                            objAdjustment.ChannelID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ChannelId"]);
                            objAdjustment.ComnpanyID = Convert.ToInt32(ddlcompany.SelectedValue);

                            db.Adjustments.Add(objAdjustment);
                            db.SaveChanges();
                            AdjustmentDetail objAdjustmentDetail = new AdjustmentDetail();
                            int adjustmentdetailid = db.usp_IDctr("adjustmentdetail").SingleOrDefault().Value;
                            objAdjustmentDetail.AdjustmentDetailId = adjustmentdetailid;
                            objAdjustmentDetail.AdjustmentId = objAdjustment.ID;
                            objAdjustmentDetail.AgencyId = objAdjustment.AgencyId;// int.Parse( ddlAgency.SelectedItem.Value);
                            objAdjustmentDetail.ChannelId = objAdjustment.ChannelID;//  int.Parse(ddlChannel.SelectedItem.Value);
                            objAdjustmentDetail.ClientId = objAdjustment.ChannelID;//  int.Parse(_dtInvoice.Rows[index]["ClientId"].ToString());
                            objAdjustmentDetail.CompanyId = objAdjustment.InvoiceMaster.CompanyID;//   int.Parse(ddlCompany.SelectedItem.Value);
                            int releaseorderid = Convert.ToInt32(objAdjustment.InvoiceMaster.ReleaseOrderID);
                            objAdjustmentDetail.ReleaseOrderId = releaseorderid;//  Int64.Parse(drAdjustment["ReleaseOrderId"].ToString());

                            var rod = db.ReleaseOrderDetails.Where(x => x.ReleaseOrderMaster.ID == releaseorderid).Take(1).SingleOrDefault();
                            if (rod != null)
                            {
                                int releaseorderdetailid = 0;
                                releaseorderdetailid = rod.ID;
                                objAdjustmentDetail.ReleaseOrderDetailId = releaseorderid;

                            }
                            db.AdjustmentDetails.Add(objAdjustmentDetail);
                            db.SaveChanges();



                            //objAdjustment.OtherCharges = decimal.Parse(lblOtherCharges.Text);
                            //objAdjustment.OtherChargesAdj = decimal.Parse(lblOtherChargesAd.Text);
                            //objAdjustment.Reason = txtReason.Text;
                            //objAdjustment.AdjustmentStatusId = Convert.ToInt32(ddlReason.SelectedValue);
                            //objAdjustment.IsLumpSum = ChkLumpSump.Checked;
                            //objAdjustment.LumpSumPremium = decimal.Parse(txtLumSumPremium.Text);
                            //objAdjustment.LumpSumDiscount = 0;
                            //objAdjustment.IsGSTLoad = chkSalesTax.Checked;
                            //objAdjustment.AgainstCrDr = string.Empty;
                            //objAdjustment.CreatedBy = Convert.ToInt32(Session["UserID"]);
                            //objAdjustment.CreatedDate = DateTime.Now;
                            //db.Adjustments.Add(objAdjustment);
                            //db.SaveChanges();

                            LSAdjustmentDetail objLSAdjustmentDetail = new LSAdjustmentDetail();
                            objLSAdjustmentDetail.ID = db.usp_IDctr("LSAAdjustment").SingleOrDefault().Value;
                            objLSAdjustmentDetail.AdjustmentID = AdjustmentID;
                            objLSAdjustmentDetail.AgencyId = int.Parse(ddlSearchAgency.SelectedItem.Value);
                            objLSAdjustmentDetail.ClientId = int.Parse(ddlSearchClient.SelectedValue);
                            objLSAdjustmentDetail.InvoiceId = int.Parse(invoiceId.ToString());
                            objLSAdjustmentDetail.InvoiceNo = invoiceId.ToString();
                            objLSAdjustmentDetail.CreatedDate = DateTime.Now;
                            db.LSAdjustmentDetails.Add(objLSAdjustmentDetail);
                            db.SaveChanges();


                            NetBalance = 0;
                            ClientBalance = 0;

                            dtNetLedger = db.GetAgencyBalance(companyid, int.Parse(ddlSearchAgency.SelectedItem.Value)).SingleOrDefault();
                            if (dtNetLedger != null)
                                NetBalance = decimal.Parse(dtNetLedger.Value.ToString());

                            dtClientLedger = db.GetClientBalance(companyid, int.Parse(ddlSearchAgency.SelectedValue), clientid).SingleOrDefault();
                            if (dtClientLedger != null)
                                ClientBalance = dtClientLedger.Value;
                            //objLedgerBalance.ClientId = 0;

                            objLedgerBalance.ID = db.usp_IDctr("LedgerBalance").SingleOrDefault().Value;
                            objLedgerBalance.AdjustmentID = AdjustmentID;
                            objLedgerBalance.IsCRV = false;
                            objLedgerBalance.IsTax = false;
                            objLedgerBalance.ISAdjusted = true;
                            objLedgerBalance.InvoiceID = Convert.ToInt32(lstInvoices.SelectedValue);





                            objLedgerBalance.AgencyId = Convert.ToInt32(ddlSearchAgency.SelectedValue);
                            objLedgerBalance.ClientId = Convert.ToInt32(ddlSearchClient.SelectedValue);
                            objLedgerBalance.TransactionDate = DateTime.Now;
                            //02092020  //       objLedgerBalance.PortalID = (int)ViewState["portalid"];
                            //if (decimal.Parse(lblNetReceiableCH.Text.ToString()) < 0 && bool.Parse(rblDebitCreditNote.SelectedItem.Value) == false)// for credit note
                            if (bool.Parse(rblDebitCreditNote.SelectedItem.Value) == false)// for credit note
                            {
                                if (decimal.Parse(lblNetReceiableCH.Text) < 0)
                                    objLedgerBalance.BillAmount = decimal.Parse(lblNetReceiableCH.Text);
                                else
                                    objLedgerBalance.BillAmount = decimal.Parse(lblNetReceiableCH.Text) * -1;

                                objLedgerBalance.ReceiptAmount = 0;
                                objLedgerBalance.NetBalance = NetBalance - objLedgerBalance.BillAmount; //decimal.Parse(lblNetReceiableCH.Text.ToString());
                                objLedgerBalance.ClientBalance = ClientBalance - Math.Abs(objLedgerBalance.BillAmount); //decimal.Parse(lblNetReceiableCH.Text.ToString()); ;
                                objLedgerBalance.NetBalance = NetBalance - Math.Abs(objLedgerBalance.BillAmount);
                                objLedgerBalance.StatusId = 5; //Credit

                            }
                            else if (bool.Parse(rblDebitCreditNote.SelectedItem.Value) == true)// for debit note
                            {
                                objLedgerBalance.BillAmount = decimal.Parse(lblNetReceiableCH.Text.ToString());
                                objLedgerBalance.ReceiptAmount = 0;
                                objLedgerBalance.NetBalance = NetBalance + objLedgerBalance.BillAmount;// decimal.Parse(lblNetReceiableCH.Text.ToString());
                                objLedgerBalance.ClientBalance = ClientBalance + objLedgerBalance.BillAmount;// decimal.Parse(lblNetReceiableCH.Text.ToString()); ;
                                objLedgerBalance.StatusId = 4;  // Debit
                            }
                            objLedgerBalance.CompanyID = companyid;
                            objLedgerBalance.ChannelID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ChannelId"]);
                            db.LedgerBalances.Add(objLedgerBalance);
                            db.SaveChanges();
                            if (bool.Parse(rblDebitCreditNote.SelectedItem.Value) == false)// for credit note
                            {
                                tblCRVCN objCRVCN = new tblCRVCN();

                                objCRVCN.CNId = db.usp_IDctr("CRVCN").SingleOrDefault().Value;
                                objCRVCN.CNNo = "CN" + objCRVCN.CNId.ToString();// CNId.ToString("000000"); // Old working update for channel wise 12-Nov-09
                                objCRVCN.InvoiceId = int.Parse(lstInvoices.SelectedItem.Value);
                                objCRVCN.InvoiceNo = lstInvoices.SelectedItem.Text.ToString();
                                objCRVCN.AgencyId = int.Parse(ddlSearchAgency.SelectedItem.Value);
                                objCRVCN.ClientId = int.Parse(ddlSearchClient.SelectedValue);
                                objCRVCN.CNDate = DateTime.Now;

                                objCRVCN.CNAmount = decimal.Parse(lblNetReceiableCH.Text.ToString());

                                objCRVCN.WithHoldingTax = -decimal.Parse(lblGSTCH.Text.ToString());

                                objCRVCN.IsCNFullyConsumed = false;
                                objCRVCN.CNConsumedAmount = 0;
                                objCRVCN.CNRemainingAmount = -decimal.Parse(lblNetReceiableCH.Text.ToString());
                                objCRVCN.CNClearedDate = DateTime.Now;
                                objCRVCN.CreatedDate = DateTime.Now;
                                objCRVCN.CreatedBy = ((UserInfo)Session["UserObject"]).ID;//int.Parse(Session["UserID"].ToString());
                                objCRVCN.Status = 110000003; // Transaction  Tax Status
                                objCRVCN.AdjustmentId = AdjustmentID;
                                //objCRVCN.CNId = CNId;
                                db.tblCRVCNs.Add(objCRVCN);
                                db.SaveChanges();
                            }
                            //var objCRVCVDB = db.cr
                            AdjustmentID = objAdjustment.ID;
                            objAdjustment = db.Adjustments.Where(x => x.ID == AdjustmentID).SingleOrDefault();
                            if (objAdjustment != null)
                            {

                                if (rblDebitCreditNote.Items[1].Selected == true)
                                {
                                    objAdjustment.IsDebitNote = false;
                                    objAdjustment.AdjustmentNo = "CN" + AdjustmentID.ToString();
                                }
                                db.SaveChanges();
                            }
                            if (bool.Parse(rblDebitCreditNote.SelectedItem.Value) == true)// for debit note
                            {
                                InvDN objInvDN = new InvDN();
                                objInvDN.ID = db.usp_IDctr("INVDN").SingleOrDefault().Value;
                                objInvDN.AdjustmentId = AdjustmentID;
                                objInvDN.InvoiceId = int.Parse(lstInvoices.SelectedItem.Value);
                                objInvDN.InvoiceNo = lstInvoices.SelectedItem.Value.ToString();
                                objInvDN.DNNo = "DN" + objInvDN.ID.ToString();
                                objInvDN.PaidAmount = 0;
                                objInvDN.RemainingAmount = decimal.Parse(lblNetReceiableCH.Text.ToString());
                                objInvDN.TotalAmount = decimal.Parse(lblNetReceiableCH.Text.ToString());
                                objInvDN.IsBilled = false;
                                objInvDN.CreatedBy = ((UserInfo)Session["UserObject"]).ID;//(int)Session["UserID"];
                                objInvDN.CreationDate = DateTime.Now;
                                db.InvDNs.Add(objInvDN);
                                db.SaveChanges();
                            }
                            scoope.Complete();
                            //Adjusted Amount  Debit
                            if (rblDebitCreditNote.Items[0].Selected == true)
                            {
                                lblGrossCRDR.Text = Math.Round((decimal.Parse(lblGrossCRDR.Text) + decimal.Parse(lblGrossCH.Text)), 2).ToString();
                                lblACCRDR.Text = Math.Round((decimal.Parse(lblACCRDR.Text) + decimal.Parse(lblACCH.Text)), 2).ToString(); //lblACCH.Text;
                                lblGSTCRDR.Text = Math.Round((decimal.Parse(lblGSTCRDR.Text) + decimal.Parse(lblGSTCH.Text)), 2).ToString(); //lblGSTCH.Text;
                                lblOTHERCRDR.Text = Math.Round((decimal.Parse(lblOTHERCRDR.Text) + decimal.Parse(lblOtherChargesCH.Text)), 2).ToString(); //lblOtherChargesCH.Text;
                                lblNETRECDRCR.Text = Math.Round((decimal.Parse(lblNETRECDRCR.Text) + decimal.Parse(lblNetReceiableCH.Text)), 2).ToString();// lblNetReceiableCH.Text;
                                lblGrowwWithGSTCRDR.Text = Math.Round((decimal.Parse(lblGrowwWithGSTCRDR.Text) + decimal.Parse(lblGrossWithGSTCH.Text)), 2).ToString();// lblGrossWithGSTCH.Text;
                                lblNetBeforeGSTCRDR.Text = Math.Round((decimal.Parse(lblNetBeforeGSTCRDR.Text) + decimal.Parse(lblNetBeforeGSTCH.Text)), 2).ToString();// lblNetBeforeGSTCH.Text;


                                //    lblGrossAd.Text = Math.Round((decimal.Parse(lblGross.Text) + decimal.Parse(lblGrossCRDR.Text)), 2).ToString();
                                //    lblACAd.Text = Math.Round((decimal.Parse(lblAC.Text) + (decimal.Parse(lblACCRDR.Text))), 2).ToString();
                                //    lblNETBeforeGSTAd.Text = Math.Round((decimal.Parse(lblNetBeforeGST.Text) + (decimal.Parse(lblNetBeforeGSTCRDR.Text))), 2).ToString();
                                //    lblGSTAd.Text = Math.Round((decimal.Parse(lblGST.Text) + (decimal.Parse(lblGSTCRDR.Text))), 2).ToString();
                                //    lblGrossWithGSTAd.Text = Math.Round((decimal.Parse(lblGrossWithGST.Text) + (decimal.Parse(lblGrowwWithGSTCRDR.Text))), 2).ToString();
                                //    lblOtherChargesAd.Text = Math.Round((decimal.Parse(lblOtherCharges.Text) + (decimal.Parse(lblOTHERCRDR.Text))), 2).ToString();
                                //    lblNetReceiableAd.Text = Math.Round((decimal.Parse(lblNetReceiable.Text) + (decimal.Parse(lblNETRECDRCR.Text))), 2).ToString();
                            }
                            else
                            {
                                //Adjusted Amount Credit
                                lblGrossCRDR.Text = Math.Round((decimal.Parse(lblGrossCRDR.Text) - decimal.Parse(lblGrossCH.Text)), 2).ToString();
                                lblACCRDR.Text = Math.Round((decimal.Parse(lblACCRDR.Text) - decimal.Parse(lblACCH.Text)), 2).ToString(); //lblACCH.Text;
                                lblGSTCRDR.Text = Math.Round((decimal.Parse(lblGSTCRDR.Text) - decimal.Parse(lblGSTCH.Text)), 2).ToString(); //lblGSTCH.Text;
                                lblOTHERCRDR.Text = Math.Round((decimal.Parse(lblOTHERCRDR.Text) - decimal.Parse(lblOtherChargesCH.Text)), 2).ToString(); //lblOtherChargesCH.Text;
                                lblNETRECDRCR.Text = Math.Round((decimal.Parse(lblNETRECDRCR.Text) - decimal.Parse(lblNetReceiableCH.Text)), 2).ToString();// lblNetReceiableCH.Text;
                                lblGrowwWithGSTCRDR.Text = Math.Round((decimal.Parse(lblGrowwWithGSTCRDR.Text) - decimal.Parse(lblGrossWithGSTCH.Text)), 2).ToString();// lblGrossWithGSTCH.Text;
                                lblNetBeforeGSTCRDR.Text = Math.Round((decimal.Parse(lblNetBeforeGSTCRDR.Text) - decimal.Parse(lblNetBeforeGSTCH.Text)), 2).ToString();// lblNetBeforeGSTCH.Text;

                                //    lblGrossAd.Text = Math.Round((decimal.Parse(lblGross.Text) + decimal.Parse(lblGrossCRDR.Text)), 2).ToString();
                                //    lblACAd.Text = Math.Round((decimal.Parse(lblAC.Text) + (decimal.Parse(lblACCRDR.Text))), 2).ToString();
                                //    lblNETBeforeGSTAd.Text = Math.Round((decimal.Parse(lblNetBeforeGST.Text) - (decimal.Parse(lblNetBeforeGSTCRDR.Text))), 2).ToString();
                                //    lblGSTAd.Text = Math.Round((decimal.Parse(lblGST.Text) + (decimal.Parse(lblGSTCRDR.Text))), 2).ToString();
                                //    lblGrossWithGSTAd.Text = Math.Round((decimal.Parse(lblGrossWithGST.Text) + (decimal.Parse(lblGrowwWithGSTCRDR.Text))), 2).ToString();
                                //    lblOtherChargesAd.Text = Math.Round((decimal.Parse(lblOtherCharges.Text) + (decimal.Parse(lblOTHERCRDR.Text))), 2).ToString();
                                //    lblNetReceiableAd.Text = Math.Round((decimal.Parse(lblNetReceiable.Text) + (decimal.Parse(lblNETRECDRCR.Text))), 2).ToString();
                                //}
                            }
                            lblGrossCH.Text = "0.00";
                            lblACCH.Text = "0.00";
                            lblGSTCH.Text = "0.00";
                            lblOtherChargesCH.Text = "0.00";
                            lblNetReceiableCH.Text = "0.00";
                            lblGrossWithGSTCH.Text = "0.00";
                            lblNetBeforeGSTCH.Text = "0.00";

                            btnApplySuccess.Enabled = false;
                            lblmessage.Text = "Changes applied successfully........";
                            ViewState["adjustmentid"] = AdjustmentID.ToString();



                        }
                    }
                }
            }

            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }
        }


        protected void ChkLoad_CheckedChanged(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            string invoiceIds = "";
            int CountSelected = 0;
            if (lstInvoices.Items.Count == 0)
            {
                lblmessage.Text = "No invoice is availble in list for processing ";
                return;
            }
            for (int i = 0; i < lstInvoices.Items.Count; i++)
            {

                if (lstInvoices.Items[i].Selected == true)
                {
                    CountSelected = CountSelected + 1;

                    if (invoiceIds == "")
                    {
                        invoiceIds = lstInvoices.Items[i].Value.ToString();
                    }
                    else
                    {
                        invoiceIds = invoiceIds + "," + lstInvoices.Items[i].Value.ToString();
                    }
                }
            }
            try
            {
                if (lstInvoices.SelectedItem.Selected == true)
                {

                    Int64 InvoiceID = Int64.Parse(lstInvoices.SelectedItem.Value);
                    var r = db.usp_GetCrDrAgainstInv(InvoiceID, 0);
                    lstCredit.DataTextField = "CRDRNO";
                    lstCredit.DataValueField = "CNID";
                    lstCredit.DataSource = r;
                    lstCredit.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;


            }
            //  PopulateCrDr(Int64.Parse(lstInvoices.SelectedItem.Value));

            try
            {

                if (ChkLoad.Checked == true && CountSelected == 0 && lstCredit.Items.Count > 0)
                {
                    lblmessage.Text = "You must select the Dr/Cr Number from list";
                    return;
                }
                else if (ChkLoad.Checked == false)
                {
                    lstCredit.Items.Clear();
                }

            }
            catch (Exception)
            {

            }

        }

        protected void lstInvoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = 0;
            ID = Convert.ToInt32(lstInvoices.SelectedValue);
            if (ID > 0)
            {
                //  ChkLoad.Checked = true;
                //    ChkLoad_CheckedChanged(null, null);
            }
        }

        protected void ddlSearchClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            lstCredit.Items.Clear();
            lstInvoices.Items.Clear();
        }
        protected void btnPrintInvoice_Click(object sender, EventArgs e)
        { }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
        //protected void btnPrintInvoice_Click(object sender, EventArgs e)
        //{
        //    if (ViewState["adjustmentid"] != null)
        //    {
        //        int AdjustmentID = Convert.ToInt32(ViewState["adjustmentid"]);
        //        if (bool.Parse(rblDebitCreditNote.SelectedItem.Value) == true)
        //        {
        //            PrintReport(AdjustmentID, true);
        //        }
        //        else
        //        {
        //            PrintReport(AdjustmentID, false);
        //        }
        //    }
        //}

        private void PrintReport(int AdjustmentID, bool isDebit)
        {

        }

        protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}