using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class InvoiceList : System.Web.UI.Page
    {
        static bool _isSqlTypesLoaded = false;

        DbDigitalEntities db = new DbDigitalEntities();
        // GLEntities db2 = new GLEntities();
        public InvoiceList()
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


                var g = db.GroupAgencies.OrderBy(x => x.GroupName).ToList();
                var p = db.Portals.Where(x => x.IsActive == true).OrderBy(x => x.PortalName).ToList();
                ddlSearchPortal.DataValueField = "ID";
                ddlSearchPortal.DataTextField = "PortalName";
                ddlSearchPortal.DataSource = p;
                ddlSearchPortal.DataBind();
                ddlSearchPortal.Items.Insert(0, new ListItem("Select Portal", "0"));


                if (ddlInvoiceStatus.SelectedValue.ToString() != "2")
                {
                    // btnExecute.Enabled = false;
                }

                //  GLEntities db2 = new GLEntities();
                var gl = db.Companies.Where(x => x.Active == true).ToList();
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataSource = gl;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Select Company", "0"));
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {


            string strIRO;
            string strReleaseOrderID;
            string strAgency;
            string strClinet;
            string strCampaign;
            string strExternal;
            int? companyID;

            Boolean? isbuild;

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
                strReleaseOrderID = txtSearchReleaseOrder.Text.Trim();

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
            int InvoiceStatus = Convert.ToInt32(ddlInvoiceStatus.SelectedValue);
            var s = db.usp_GetDataForInvoice_WithInvoiceMaster(strReleaseOrderID, strExternal, strIRO, StartDate, EnDate, strAgency, strClinet, strCampaign, true, companyID).ToList();
            DataTable dt = Helper.ToDataTable(s);
            ViewState["dt"] = dt;
            gv.DataSource = s;
            gv.DataBind();
            try
            {
                //if (ddlInvoiceStatus.SelectedIndex == 1 || ddlInvoiceStatus.SelectedIndex == 0)
                //    ((CheckBox)gv.HeaderRow.FindControl("chkHeader")).Enabled = false;
            }
            catch (Exception)
            {

            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            //foreach (GridViewRow dr in gv.Rows)
            //{
            //    CheckBox chk = (CheckBox)dr.FindControl("chk");
            //    chk.Checked = false;
            //}
        }

        //protected void btnExecute_Click(object sender, EventArgs e)
        //{
        //    lblmessage.Text = string.Empty;
        //    bool isTaken = false;
        //    int i = 0;
        //    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
        //    {
        //        try
        //        {
        //            InvoiceMaster objMaster = new InvoiceMaster();
        //            InvoiceDetail objDetail = new InvoiceDetail();



        //            foreach (GridViewRow dr in gv.Rows)
        //            {
        //                if (dr.RowType == DataControlRowType.DataRow)
        //                {
        //                    string txt = dr.Cells[8].Text;
        //                    if (txt == "0")
        //                    {
        //                        CheckBox chk = (CheckBox)dr.FindControl("chk");

        //                        //string txt = dr.Cells[10].Text;
        //                        // if (txt == "False")
        //                        if (true)
        //                        {
        //                            if (chk.Checked == true)
        //                            {
        //                                try
        //                                {
        //                                    int ID = Convert.ToInt32(gv.DataKeys[dr.RowIndex].Value);
        //                                    var s = db.ReleaseOrderMasters.Where(x => x.ID == ID).SingleOrDefault();
        //                                    s.IsBilled = 1; // build
        //                                    db.SaveChanges();

        //                                    var inv = db.usp_IDctr("Invoice").SingleOrDefault();
        //                                    // var d = db.usp_InvoiceMasterDataByReleaseOrderID(ID).usp_InvoiceMasterDataByReleaseOrderID(ID).SingleOrDefault();
        //                                    if (s != null)
        //                                    {

        //                                        objMaster = new InvoiceMaster();
        //                                        objMaster.ID = Convert.ToInt32(inv.Value);
        //                                        objMaster.ReleaseOrderID = ID; // Releae order master id
        //                                        objMaster.GrossAmount = 0;// Convert.ToDecimal(s.INV_Gross);
        //                                        objMaster.NetReceiable = Convert.ToDecimal(s.INV_Net);
        //                                        objMaster.RecivedAmount = 0;//
        //                                        if (chkDollar.Checked)
        //                                            objMaster.dollarinvoice = 1;
        //                                        else
        //                                            objMaster.dollarinvoice = 0;

        //                                        objMaster.PaymentStatusID = 110000001;
        //                                        objMaster.ClientID = s.ClientID;
        //                                        objMaster.AgencyID = s.AgencyID;

        //                                        objMaster.CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
        //                                        objMaster.CreationDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        //                                        //objMaster.ModifiedBy = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
        //                                        //objMaster.ModifiedOn = DateTime.Now;
        //                                        //objMaster.CancelledBy = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
        //                                        // objMaster.IsCancelled = false;
        //                                        db.InvoiceMasters.Add(objMaster);
        //                                        db.SaveChanges();

        //                                        var Detail = db.ReleaseOrderDetails.Where(x => x.ReleaseOrderID == ID).ToList();
        //                                        foreach (var id in Detail)
        //                                        {
        //                                            objDetail = new InvoiceDetail();
        //                                            var detailid = db.usp_IDctr("Invoice Detail").SingleOrDefault();
        //                                            objDetail.ID = detailid.Value;
        //                                            objDetail.InvoiceID = objMaster.ID;
        //                                            objDetail.ReleaseOrderId = ID;
        //                                            objDetail.ReleaseOrderDetailId = id.ID;
        //                                            objDetail.FromDate = id.StartDate;
        //                                            objDetail.ToDate = id.EndDate;
        //                                            objDetail.IsCancelled = false;
        //                                            objDetail.PortalID = id.PortalID;
        //                                            objDetail.GrossAmount = id.TotalBilled;
        //                                            objDetail.NetAmount = id.NetAmount;
        //                                            objDetail.GSTAmount = id.GSTAmount;
        //                                            objDetail.AGCAmount = id.AGCommission;
        //                                            db.InvoiceDetails.Add(objDetail);
        //                                            db.SaveChanges();
        //                                        }

        //                                        LedgerBalance lb = new LedgerBalance();
        //                                        //  var LedgerBalance = db.usp_GetNetBalance(s.AgencyID, s.PortalID).SingleOrDefault();
        //                                        //  var ClientBalance = db.usp_GetClientBalance(d.AgencyID, d.ClientID, d.PortalID).SingleOrDefault(); ;
        //                                        lb.ID = db.usp_IDctr("LedgerBalance").SingleOrDefault().Value;
        //                                        lb.AgencyId = s.AgencyID;
        //                                        lb.ClientId = s.ClientID;
        //                                        lb.InvoiceID = objMaster.ID;
        //                                        lb.IsCRV = false;
        //                                        lb.ISAdjusted = false;
        //                                        lb.IsTax = false;
        //                                        // lb.b  = Co;onvert.ToDecimal(s.INV_Net);
        //                                        lb.BillAmount = Convert.ToInt32(s.INV_Net);
        //                                        lb.ReceiptAmount = 0;


        //                                        lb.StatusId = 1;
        //                                        lb.TransactionDate = DateTime.Now;
        //                                        // lb.PortalID = Convert.ToInt32(s.PortalID);
        //                                        db.LedgerBalances.Add(lb);
        //                                        db.SaveChanges();

        //                                        LogManagers.RecordID = objMaster.ID;
        //                                        LogManagers.ActionOnForm = "Invoice Created";
        //                                        LogManagers.ActionBy = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
        //                                        LogManagers.ActionOn = DateTime.Now;
        //                                        LogManagers.ActionTaken = "Invoice Created";
        //                                        LogManagers.SetLog(db);
        //                                    }
        //                                    i++;
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    try
        //                                    {
        //                                        if (ex.InnerException.InnerException.Message.Contains("duplicate"))
        //                                        {
        //                                            lblmessage.Text = "Invoice already generated, Please cancel invoice to regenerate";
        //                                            return;
        //                                        }
        //                                    }
        //                                    catch (Exception)
        //                                    {

        //                                        lblmessage.Text = ex.Message;
        //                                        return;
        //                                    }
        //                                    lblmessage.Text = ex.Message;
        //                                    return;
        //                                }

        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            if (i > 0)
        //            {

        //                //   btnSearch_Click(null, null);
        //                scope.Complete();
        //                lblmessage.Text = "Invoice Created Successfully";
        //            }
        //            else
        //            {
        //                lblmessage.Text = "Invoice orders receiving failed";
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            lblmessage.Text = "Invoice orders receiving failed";
        //        }
        //    }
        //}

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
            Button btn = (Button)e.Row.FindControl("btnprint");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string txt = e.Row.Cells[9].Text;
                if (txt == "True")
                {
                    chk.Checked = true;
                    chk.Enabled = false;
                    btn.Enabled = false;
                }
            }
        }

        protected void ddlInvoiceStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlInvoiceStatus.SelectedValue.ToString() != "2")
            //{
            //   // btnExecute.Enabled = false;
            //}
            //else
            //  //  btnExecute.Enabled = true;
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
        }

        private void printReport(int InvoiceID)
        {
            lblmessage.Text = string.Empty;
            // Response.Redirect("RptInvoice.aspx?id=" + InvoiceID.ToString());
            try
            {
                //   var ss = db.usp_PrintInvoice(InvoiceID).Take(1).SingleOrDefault();
                //    var s = db.usp_PrintInvoice(InvoiceID).ToList();
                //    string myWords = "";// NumberToWords.ConvertAmount(Convert.ToDouble(ss.NetReceiable));
                //    ReportParameter rp = new ReportParameter("pmToWords", myWords);
                //    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RptInvoice.rdlc");
                //    ReportDataSource rds = new ReportDataSource("DataSetInvoice", s);
                //    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                //    ReportViewer1.LocalReport.DataSources.Clear();
                //    //Add ReportDataSource  
                //    ReportViewer1.LocalReport.DataSources.Add(rds);
                //       int InvoiceID = Convert.ToInt32(Request.QueryString[0]);

                var ss = db.usp_PrintInvoice(InvoiceID).Take(1).SingleOrDefault();
                if (ss != null)
                {
                    var s = db.usp_PrintInvoice(InvoiceID).ToList();

                    string myWords = DecimalToWordExtension.ToWords(Convert.ToDecimal(ss.NetReceiable));
                    ReportParameter[] rp = new ReportParameter[2];
                    rp[0] = new ReportParameter("pmToWords", myWords);
                    rp[1] = new ReportParameter("pmInvoiceNo", ss.invoiceID.ToString());
                    //    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    //    //set path of the Local report  
                    if (ss.CompanyID == 1)
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/PrintInvoiceCentury.rdlc";

                    }
                    else
                    {

                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/PrintInvoice.rdlc";

                    }
                    //creating object of DataSet dsEmployee and filling the DataSet using SQLDataAdapter  
                    //dsEmployee dsemp = new dsEmployee();
                    //SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Sample;Integrated Security=true;");
                    //con.Open();
                    //SqlDataAdapter adapt = new SqlDataAdapter("select * from tbl_Employee", con);
                    //adapt.Fill(dsemp, "DataTable1");
                    //con.Close();
                    //Providing DataSource for the Report  
                    ReportDataSource rds = new ReportDataSource("DSInvoice", s);

                    ReportViewer1.LocalReport.SetParameters(rp);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    //Add ReportDataSource  
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
            Int32 InvoiceID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());

            printReport(InvoiceID);
            //TextBox txtRate = (TextBox)row.FindControl("txtRate");
        }
    }

}
