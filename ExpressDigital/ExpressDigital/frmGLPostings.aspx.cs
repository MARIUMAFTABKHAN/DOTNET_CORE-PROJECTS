using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;
using System.Data.Entity;

namespace ExpressDigital
{
    public partial class frmGLPostings : System.Web.UI.Page
    {
        GLEntities db2 = new GLEntities();
        DbDigitalEntities db = new DbDigitalEntities();
        //TextInfo tInfo = new CultureInfo("en-US", false).TextInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                divUpdateAccountHead.Visible = false;
                divReasonNote.Visible = false;
                lblvoucherNo.Text = string.Empty;
                DateTime dt = DateTime.Now;
                DateTime stdate = new DateTime(dt.Year, dt.Month, 1);
                DateTime endate = stdate.AddMonths(1).AddDays(-1);
                txtDate.Text = stdate.ToString("dd/MM/yyyy");//, new CultureInfo("en-GB"));
                txtToDate.Text = endate.ToString("dd/MM/yyyy");//, new CultureInfo("en-GB"));

                try
                {
                    GetCompanies();
                    GetCostCenters();
                    GetDepartments();
                    GetPublications();
                    ddlchannel_SelectedIndexChanged(null, null);
                    GetStations();
                    GetFiscalYear();
                    try
                    {
                        ddlGroupCompany_SelectedIndexChanged(null, null);
                    }
                    catch (Exception)
                    {
                    }

                }
                catch (Exception ex)
                {
                    lblmessage.Text = ex.Message;
                }
            }


            //  PopulateChannel(((CTS.User)(Session["UserObject"])).ChannelId);
            PopulateAdjustmentStatus();
            chkReasonNote.Items[0].Selected = true;
            btnGenerateVouher.Enabled = false;
        }

        private void GetCompanies()
        {
            var c = db.Companies.Where(x => x.Active == true).ToList();
            DataTable dt = Helper.ToDataTable(c);
            ViewState["companies"] = dt;
            ddlGroupCompany.DataValueField = "Company_Id";
            ddlGroupCompany.DataTextField = "Company_Name";
            ddlGroupCompany.DataSource = c;
            ddlGroupCompany.DataBind();

        }
        private void GetFiscalYear()
        {
            DataTable dt = new DataTable();
            var yy = db2.FiscalYears.Where(x => x.Status == true).OrderByDescending(x => x.Id).ToList();

            dt.Columns.Add("Id");
            dt.Columns.Add("FiscalYear");
            DataRow row = dt.NewRow();

            foreach (var ss in yy)
            {
                row = dt.NewRow();
                row[0] = Convert.ToInt32(ss.Id);
                row[1] = (ss.Starting_date.ToString("yy") + ss.Ending_date.ToString("yy"));
                dt.Rows.Add(row);
            }
            dt.AcceptChanges();
            ddlFiscalyear.DataValueField = "Id";
            ddlFiscalyear.DataTextField = "FiscalYear";
            ddlFiscalyear.DataSource = dt;
            ddlFiscalyear.DataBind();

            var idctr = db2.usp_VGetCureCityChar().ToList().Take(1).SingleOrDefault(); ;

            if (idctr != null)
                ViewState["CityChar"] = Convert.ToChar(idctr);
            else
                ViewState["CityChar"] = 'X';
        }

        private void GetStations()
        {
            var gc = db2.GroupComps.ToList();//  Helper.FillDropdowns(db, "select * from GroupComps");
            ddlStations.DataValueField = "GroupComp_Id";
            ddlStations.DataTextField = "GroupComp_Name";
            ddlStations.DataSource = gc;
            ddlStations.DataBind();


        }
        private void GetPublications()
        {
            using (DbDigitalEntities db = new DbDigitalEntities())
            {
                var channel = db.Channels.ToList();// () .usp_GetAllChannel(null, null, null).ToList();// .objChannelDB.GetAllChannel(null, null, null, null, true);            
                ViewState["publications"] = Helper.ToDataTable(channel);
                ddlchannel.DataValueField = "Id";
                ddlchannel.DataTextField = "Name";
                ddlchannel.DataSource = channel;
                ddlchannel.DataBind();
            }

        }
        private void GetDepartments()
        {
            var dp = db2.Departments.ToList();
            ddlDepartment.DataValueField = "ID";
            ddlDepartment.DataTextField = "Department_Name";
            ddlDepartment.DataSource = dp;
            ddlDepartment.DataBind();
        }

        private void GetCostCenters()
        {
            var cc = db2.CostCenters.ToList();
            ddlcostCenter.DataValueField = "ID";
            ddlcostCenter.DataTextField = "CostCenter_Title";
            ddlcostCenter.DataSource = cc;
            ddlcostCenter.DataBind();
        }
        protected void PopulateAdjustmentStatus()
        {
            using (DbDigitalEntities db = new DbDigitalEntities())
            {
                var adjS = db.AdjustmentStatus.Where(x => x.IsActive == true).ToList();
                chkReasonNote.DataSource = adjS;
                chkReasonNote.DataTextField = "AdjustmentStatusRemarks";
                chkReasonNote.DataValueField = "ID";
                chkReasonNote.DataBind();
            }
        }
        private bool CheckMonthlyClosing(Int32 companyid, DateTime dt)
        {
            bool Result = false;
            //DateTime vrmonth = new DateTime(dt.Year, dt.Month, 1);
            int vrmonth = dt.Month;
            int vryear = dt.Year;
            var mc = (from u in db2.MonthlyClosings
                      select new { u.Id, mc = u.Closing_Month.Value.Month, u.Company_id, myy = u.Closing_Month.Value.Year })
                      .Where(x => x.Company_id == companyid && x.mc == vrmonth && x.myy == vryear).ToList();
            if (mc.Count == 0)
                Result = true;

            return Result;
        }
        public bool CheckFisaclClosing(Int32 FiscalYearID)
        {
            bool Result = false;
            var ff = db2.FiscalYears.Where(x => x.Id == FiscalYearID && x.Status == true).ToList();
            if (ff.Count > 0)
                Result = true;
            return Result;
        }


        private bool checkHead()
        {
            bool Result = true;
            using (GLEntities db = new GLEntities())
            {

                if (ddlInvoiceType.SelectedItem.Text == "Credit")
                {
                    //foreach (GridViewRow row in gvcredit.Rows)
                    //{
                    //    if (row.RowType == DataControlRowType.DataRow)
                    //    {

                    //        if (row.Cells[1].Text != "Totals")
                    //        {
                    //            if (row.Cells[6].Text.Trim().Length == 0)
                    //            {
                    //                Result = false;
                    //                break;
                    //            }
                    //        }
                    //        if (row.Cells[1].Text == "Totals")
                    //        {
                    //            if ((Convert.ToDecimal(row.Cells[4].Text) != Convert.ToDecimal(row.Cells[5].Text)) && (Convert.ToDecimal(row.Cells[4].Text) > 0))
                    //            {
                    //                Result = false;
                    //                break;
                    //            }
                    //        }

                    //    }

                    //}
                }
                else
                {

                    foreach (GridViewRow row in gv.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            string lblGroupAgency = ((Label)row.FindControl("lblGroupAgency")).Text;

                            if (!lblGroupAgency.Equals("Totals"))
                            {
                                string lblAccountHead = ((Label)row.FindControl("lblAccountHead")).Text;
                                int Companyid = Convert.ToInt32(ddlGroupCompany.SelectedValue);
                                var s = db.ChartOfAccounts.Where(x => x.Is_Transaction_Level == true && x.Comp_Code == Companyid && x.Account_Code == lblAccountHead).SingleOrDefault();
                                if (s == null)
                                {
                                    Result = false;
                                    row.BackColor = System.Drawing.Color.Red;
                                    row.ForeColor = System.Drawing.Color.White;
                                    row.ToolTip = "Account Code Invalid ";
                                    break;
                                }

                            }

                        }
                    }
                }
            }
            return Result;
        }

        public static string ToFinancialYearShort(DateTime dateTime)
        {
            return (dateTime.Month >= 7 ? dateTime.AddYears(1).ToString("yy") : dateTime.ToString("yy"));
        }
        public static string GetVoucherNo(GLEntities db, string vrytpe, string CurrentMMYY, string CompanyCodeChar, string FiscalYear, string StationCodeChar, string CompanyCode)
        {
            string VrNumber = "";
            string FullVrType = "";
            int idx = vrytpe.Length - 1;
            try
            {
                //JournalVoucher161795-0217
                if (vrytpe == "J")
                    FullVrType = "JournalVoucher";
                else if (vrytpe == "R")
                    FullVrType = "ReceiptVoucher";
                string ForTable = FullVrType + FiscalYear + CompanyCode + "-" + CurrentMMYY;
                var _Counter = db.usp_IDctr(ForTable).SingleOrDefault();
                VrNumber = vrytpe + CompanyCodeChar + StationCodeChar + _Counter + CurrentMMYY;
            }
            catch (Exception ex)
            {

            }
            return VrNumber;
        }
        private bool checkdebitcredit()
        {
            bool Result = false;
            decimal debit = 0;
            decimal credit = 0;
            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string lblGroupAgency = ((Label)row.FindControl("lblGroupAgency")).Text;

                    if (!lblGroupAgency.Equals("Totals"))
                    {
                        Label lblDebit = (Label)row.FindControl("lblDebit");
                        Label lblCredit = (Label)row.FindControl("lblCredit");
                        debit += Convert.ToDecimal(lblDebit.Text);
                        credit += Convert.ToDecimal(lblCredit.Text);
                    }

                }
            }
            if ((debit == credit) && (debit > 0 && credit > 0))
            {
                Result = true;
            }
            return Result;
        }
        protected void btnGenerateVouher_Click(object sender, EventArgs e)
        {
            if (checkdebitcredit() == false)
            {
                lblmessage.Text = " Debit / Credit sides are not equal";
                return;
            }

            DateTime vrDate = Helper.SetDateFormat(txtVrDate.Text);
            using (GLEntities db = new GLEntities())
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    var fiscalYear = ToFinancialYearShort(Helper.SetDateFormat(txtVrDate.Text));
                    if (ddlFiscalyear.SelectedItem.Text.Substring(2) != fiscalYear)
                    {
                        lblmessage.Text = "Please check selected fiscal year.";
                        return;
                    }
                    lblvoucherNo.Text = string.Empty;
                    lblmessage.Text = "";
                    txtHeadAccount.Text = string.Empty;
                    divUpdateAccountHead.Visible = false;
                    string FiscalYear = ddlFiscalyear.SelectedItem.Text;
                    string UserName = (Session["UserName"].ToString().ToUpper().Substring(0, 9));
                    string tablename = "transactions" + FiscalYear;
                    string vrNumber = "";
                    if (checkHead() == true)
                    {

                        Int32 FiscalYearID = Convert.ToInt32(ddlFiscalyear.SelectedValue);
                        Int32 CompanyID = Convert.ToInt32(ddlGroupCompany.SelectedValue);
                        DateTime dtFrom = Helper.SetDateFormat(txtVrDate.Text);
                        if (CheckMonthlyClosing(CompanyID, dtFrom) == true)
                        {
                            if (CheckFisaclClosing(FiscalYearID) == true)
                            {

                                DataTable dt = (DataTable)ViewState["companies"]; // GetCompanyDetails(db ,CompanyID);

                                DataView view = new DataView();
                                view.Table = dt;
                                view.RowFilter = "Company_Id = '" + CompanyID + "'";
                                string MMYY = dtFrom.ToString("MMyy");
                                string CoCodeChar = view[0]["Company_Code"].ToString();  // Century company Code
                                string StCodeChar = ViewState["CityChar"].ToString();
                                string VrTypeChar = "J";

                                string CompanyCode = view[0]["GroupComp_Code"].ToString();

                                vrNumber = GetVoucherNo(db, VrTypeChar, MMYY, CoCodeChar, FiscalYear, StCodeChar, CompanyCode);
                                lblvoucherNo.Text = vrNumber;
                                string strsql = "INSERT INTO " + tablename + " (Voucher_No, Voucher_Date, Remarks, Rec_Added_By, Sys_Gen, Rec_Added_Date, Rec_Added_Time, Posting_Status) VALUES ('" + vrNumber + "', '" + vrDate + "', '" + txtRemarks.Text.Trim() + "', '" + UserName + "', 'DIG', GETDATE(), GETDATE(), 'U')";
                                db2.Database.ExecuteSqlCommand(strsql);
                                bool Result = false;
                                // Detail Transactions
                                if (ddlInvoiceType.SelectedItem.Text != "Credit")
                                {
                                    int i = 0;
                                    string ACHead = "";
                                    Decimal Debit = 0;
                                    Decimal Credit = 0;
                                    Decimal Amount = 0;
                                    int InvoiceID = 0;
                                    string ForTable = "TransactionsDetails" + FiscalYear;
                                    foreach (GridViewRow row in gv.Rows)
                                    {
                                        try
                                        {
                                            string lblGroupAgency = ((Label)row.FindControl("lblGroupAgency")).Text;
                                            if (!lblGroupAgency.Equals("Totals"))
                                            {
                                                var _Counter = db.usp_IDctrProcurement(ForTable).SingleOrDefault().Value;
                                                // foreach (var xx in mDetailID)
                                                {
                                                    int DetailID = Convert.ToInt32(_Counter);
                                                    Label lblDebit = (Label)row.FindControl("lblDebit");
                                                    Label lblCredit = (Label)row.FindControl("lblCredit");
                                                    Label lblAccountHead = (Label)row.FindControl("lblAccountHead");
                                                    HiddenField hdInvoiceID = (HiddenField)row.FindControl("hdInvoiceID");
                                                    Debit = Convert.ToDecimal(lblDebit.Text);
                                                    Credit = Convert.ToDecimal(lblCredit.Text);
                                                    if (Debit > 0)
                                                    {
                                                        Amount = Debit;
                                                    }
                                                    if (Credit > 0)
                                                    {
                                                        Amount = Credit * -1;
                                                    }
                                                    if (Amount != 0)
                                                    {
                                                        ACHead = lblAccountHead.Text;
                                                        string strsqlDetail = "INSERT INTO TransactionsDetails" + FiscalYear + " (Id, Voucher_No, Transaction_Date, Trans_Type, Trans_Details, Comp, Entry_Loc, Cost_Center, Publication, Department, Account_Code, Amount) " +
                                                            "VALUES (" + DetailID + ", '" + vrNumber + "', '" + Helper.SetDateFormat(txtVrDate.Text) + "','U' , '" + txtRemarks.Text.Trim() + "', " + Convert.ToInt32(ddlGroupCompany.SelectedValue) + ", " + Convert.ToInt32(ddlStations.SelectedValue) + " " +
                                                            ", " + Convert.ToInt32(ddlcostCenter.SelectedValue) + ", " + Convert.ToInt32(ViewState["pubid"]) + ", " + Convert.ToInt32(ddlDepartment.SelectedValue) + ",'" + ACHead + "', '" + Amount + "')";
                                                        db.Database.ExecuteSqlCommand(strsqlDetail);


                                                    }
                                                    i++;
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            lblmessage.Text = ex.Message;
                                        }
                                    }
                                    if (gv.Rows.Count - 1 == i)
                                        Result = true;

                                }
                                if (Result == true)
                                {
                                    scope.Complete();
                                    scope.Dispose();
                                    int InvoiceID = 0;
                                    foreach (GridViewRow row in gv.Rows)
                                    {
                                        try
                                        {
                                            string lblGroupAgency = ((Label)row.FindControl("lblGroupAgency")).Text;
                                            if (!lblGroupAgency.Equals("Totals"))
                                            {

                                                HiddenField hdInvoiceID = (HiddenField)row.FindControl("hdInvoiceID");
                                                using (DbDigitalEntities obj = new DbDigitalEntities())
                                                {
                                                    InvoiceID = Convert.ToInt32(hdInvoiceID.Value);
                                                    var im = obj.InvoiceMasters.Where(x => x.ID == InvoiceID && x.VrCreated == null).SingleOrDefault();
                                                    if (im != null)
                                                    {
                                                        im.VrCreated = true;
                                                        im.VrNumber = vrNumber;
                                                        obj.SaveChanges();
                                                    }
                                                    VoucherTrack vt = new VoucherTrack();

                                                    string userName = Convert.ToString(Session["UserName"]);//  ((CTS.User)(Session["UserObject"])).FirstName.ToUpperInvariant();
                                                    var vtid = obj.usp_IDctr("vouchertrack").SingleOrDefault();
                                                    vt.ID = Convert.ToInt32(vtid);
                                                    vt.VoucherNo = vrNumber;
                                                    vt.VoucherDate = Helper.SetDateFormat(txtVrDate.Text);
                                                    vt.CreatedOn = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                                    vt.CreatedBy = userName;
                                                    obj.VoucherTracks.Add(vt);
                                                    obj.SaveChanges();

                                                }

                                            }
                                            //else
                                            //    lblmessage.Text = "Voucher Creation Failed.";
                                        }
                                        catch (Exception ex)
                                        {
                                            lblmessage.Text = ex.Message;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void ddlchannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblvoucherNo.Text = string.Empty;
            int idx = ddlchannel.SelectedIndex;
            //DataTable dt = Helper.FillDropdowns(db, "select * from Publications")

            DataTable dt = (DataTable)ViewState["publications"];
            ViewState["pubid"] = dt.Rows[idx]["GLPublication"].ToString();
            // ViewState["companycode"] =    dt.Rows[idx]["GroupComp_Code"].ToString();
            // ViewState["companycodeAbr"] = dt.Rows[idx]["Company_Code"].ToString();


        }
        protected void ddlcostCenter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlGroupCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            // string[] strst = System.Configuration.ConfigurationManager.AppSettings["SalesTax"].Split(';');
            try
            {


                int companyid = Convert.ToInt32(ddlGroupCompany.SelectedValue);

                var acsalestax = db.GLAccounttHeads.Where(x => x.CompanyID == companyid && x.Acc_category == "T").ToList();
                ddlSalesTaxAccount.DataTextField = "AccountTitle";
                ddlSalesTaxAccount.DataValueField = "AccountHead";
                ddlSalesTaxAccount.DataSource = acsalestax;
                ddlSalesTaxAccount.DataBind();

                var ac = db.GLAccounttHeads.Where(x => x.Acc_category == "C" && x.CompanyID == companyid).ToList();
                ddlAgencyComm.DataTextField = "AccountTitle";
                ddlAgencyComm.DataValueField = "AccountHead";
                ddlAgencyComm.DataSource = ac;
                ddlAgencyComm.DataBind();

                var ra = db.GLAccounttHeads.Where(x => x.Acc_category == "R" && x.CompanyID == companyid).ToList();
                ddlRevenueAccount.DataTextField = "AccountTitle";
                ddlRevenueAccount.DataValueField = "AccountHead";
                ddlRevenueAccount.DataSource = ra;
                ddlRevenueAccount.DataBind();
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }
        decimal credit = 0;
        decimal debit = 0;

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                try
                {
                    Label lbl = (Label)e.Row.FindControl("lblGroupAgency");
                    if (lbl.Text == "Totals")
                    {
                        Button btn = (Button)e.Row.FindControl("btnAdd");
                        btn.Visible = false;
                    }
                }
                catch (Exception)
                {

                }


            }
        }
        protected void gvcredit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].Text = Math.Abs(Math.Round(Convert.ToDecimal(e.Row.Cells[4].Text), 2, MidpointRounding.ToEven)).ToString();// ;// Math.Round().ToString(); 
                e.Row.Cells[5].Text = Math.Abs(Math.Round(Convert.ToDecimal(e.Row.Cells[5].Text), 2, MidpointRounding.ToEven)).ToString();// ;// Math.Round().ToString(); 
                try
                {
                    if (e.Row.Cells[1].Text == "Totals" || e.Row.Cells[1].Text == ddlSalesTaxAccount.SelectedItem.Value || e.Row.Cells[1].Text == "Revenue Agencies" || e.Row.Cells[1].Text == "Agency Commission")
                    {
                        Button btn = (Button)e.Row.FindControl("btnAdd");
                        btn.Visible = false;
                    }
                }
                catch (Exception)
                {
                }
            }

        }
        protected void gvcredit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
        }
        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument != "")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                if (ID > 0)
                {
                    hdheadaccount.Value = ID.ToString();
                    lblGroupAgency.Text = e.CommandName;
                    divUpdateAccountHead.Visible = true;
                }
            }
            else
            {
                hdheadaccount.Value = "";
                lblGroupAgency.Text = "";
            }
        }
        protected void btnupdateaccounthead_Click(object sender, EventArgs e)
        {
            //lblmessage.Text = string.Empty;
            //try
            //{
            //    string GLConnectionString = System.Configuration.ConfigurationManager.AppSettings["GLConnectionString"];
            //    DBManager db = new DBManager(GLConnectionString);
            //    db.Open();
            //    DataSet dsT = db.ExecuteDataSet(CommandType.Text, "select * from ChartOfAccounts where Comp_Code = " + ddlGroupCompany.SelectedItem.Value + " and Account_Code = '" + txtHeadAccount.Text + "'");
            //    //DataSet dsT = db.ExecuteDataSet(CommandType.Text, "select * from ChartOfAccounts where Comp_Code = " + ddlGroupCompany.SelectedItem.Value + "");
            //    if (dsT.Tables[0].Rows.Count <= 0)
            //    {
            //        lblmessage.Visible = true;
            //        lblmessage.Text = "<strong>'" + txtHeadAccount.Text + "'</strong> Account Head not listed in GL with the selected company";
            //        return;
            //    }

            //    int GroupAgencyid = Convert.ToInt32(hdheadaccount.Value);
            //    CTS.AgencyDB obj = new CTS.AgencyDB();
            //    if (obj.updateGroupAgency(GroupAgencyid, txtHeadAccount.Text) == true)
            //    {
            //        lblmessage.Text = "Account Head Updated....";
            //        btnshow_Click(null, null);
            //        return;
            //    }
            //}
            //catch (Exception)
            //{
            //    lblmessage.Text = "Error Raised";
            //}


        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblvoucherNo.Text = string.Empty;
            txtHeadAccount.Text = string.Empty;
            divUpdateAccountHead.Visible = false;
        }
        protected void ddlInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblvoucherNo.Text = string.Empty;
            gv.DataSource = null;
            gv.DataBind();
            //  gvcredit.DataSource = null;
            //   gvcredit.DataBind();
            chkReasonNote.Items[0].Selected = true;
            //foreach (ListItem item in chkReasonNote.Items)
            //{
            //    item.Selected = false;
            //}
            if (ddlInvoiceType.SelectedItem.Text == "Invoice")
            {
                divReasonNote.Visible = false;

            }
            else
            {
                divReasonNote.Visible = true;

            }
        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            lblvoucherNo.Text = string.Empty;
            txtHeadAccount.Text = string.Empty;
            divUpdateAccountHead.Visible = false;
        }

        protected void btnupdateaccounthead_Click1(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            try
            {

                int Agencyid = Convert.ToInt32(ViewState["hdAgencyID"].ToString());
                var ag = db.Agencies.Where(x => x.ID == Agencyid).SingleOrDefault();
                if (ag != null)
                {
                    ag.AccountHeads = txtHeadAccount.Text;
                    db.SaveChanges();
                    lblmessage.Text = "Account Head Updated....";
                    LogManagers.ActionBy = ((UserInfo)Session["UserObject"]).ID; 
                    LogManagers.RecordID = Agencyid;
                    LogManagers.ActionOnForm = "frmGLPosting";
                    LogManagers.ActionOn = DateTime.Now;
                    LogManagers.ActionTaken = "GL Posting/Account head updated";
                    LogManagers.SetLog(db);
                    db.SaveChanges();
                    btnCancel_Click1(null, null);

                }
            }
            catch (Exception)
            {
                lblmessage.Text = "Error Raised";
            }
        }



        protected void btnshow_Click(object sender, EventArgs e)
        {

            var fiscalYear = ToFinancialYearShort(Helper.SetDateFormat(txtVrDate.Text));
            if (ddlFiscalyear.SelectedItem.Text.Substring(2) != fiscalYear)
            {
                lblmessage.Text = "Please check selected fiscal year.";
                return;
            }

            DateTime novemberMonth = new DateTime(2019, 11, 1, 0, 0, 0);

            DateTime dtFrom = Helper.SetDateFormat(txtDate.Text);
            DateTime dtTo = Helper.SetDateFormat(txtToDate.Text);
            if (dtFrom.Date >= novemberMonth && ddlSalesTaxAccount.SelectedItem.Value == "000363" || dtTo.Date >= novemberMonth && ddlSalesTaxAccount.SelectedItem.Value == "000363")
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Please select another sales tax number.";
                return;
            }

            lblvoucherNo.Text = string.Empty;
            lblvoucherNo.Text = string.Empty;
            lblmessage.Text = "";
            txtHeadAccount.Text = string.Empty;
            divUpdateAccountHead.Visible = false;
            if (ddlInvoiceType.SelectedItem.Text == "Credit")
            {
                // divinvoice.Visible = false;
                //    divcredit.Visible = true;
                //  CreditVoucher();
            }
            else
            {
                divinvoice.Visible = true;
                // divcredit.Visible = false;
                DebitVoucher();
            }


        }
        private void DebitVoucher()
        {
            DataTable dtInvoices = new DataTable();
            dtInvoices.Columns.Add("InvStatus");
            dtInvoices.Columns.Add("GroupAgency");
            dtInvoices.Columns.Add("adjustmentStatusID");
            dtInvoices.Columns.Add("AdjStatus");
            dtInvoices.Columns.Add("Gross");
            dtInvoices.Columns.Add("Debit");
            dtInvoices.Columns.Add("Credit");
            dtInvoices.Columns.Add("AccountHead");
            dtInvoices.Columns.Add("GroupagencyID");
            dtInvoices.Columns.Add("CompnayID");
            dtInvoices.Columns.Add("InvoiceID");
            dtInvoices.Columns.Add("InvoiceStatus");
            int companyid = Convert.ToInt32(ddlGroupCompany.SelectedValue);
            int channelid = Convert.ToInt32(ddlchannel.SelectedValue);
            DateTime startdate = Helper.SetDateFormat(txtDate.Text);
            DateTime enddate = Helper.SetDateFormat(txtToDate.Text);

            var s = db.ups_GetMonthlywiseCollectionForGL(channelid, startdate, enddate, companyid).ToList();
            if (s.Count > 0)
            {
                int invoiceid = 0;
                int agencyid = 0;
                decimal mgst = 0;
                decimal mnet = 0;
                decimal mgross = 0;
                decimal mac = 0;
                //decimal mcredit = 0;
                //decimal mdebit = 0;
                int Rowcount = 0;

                decimal totalCredit = 0;// Math.Round((credit + mgst), 2, MidpointRounding.ToEven);
                decimal totalDebit = 0;// Math.Round((debit + mac), 2, MidpointRounding.ToEven);
                DataRow drn = dtInvoices.NewRow();
                DataRow drnAG = dtInvoices.NewRow();
                DataRow drnGST = dtInvoices.NewRow();
                foreach (var ss in s)
                {
                    invoiceid = ss.ID;
                    agencyid = ss.AgencyID;
                    // var drVr = db.usp_GetGLCollectionDetils_(invoiceid, companyid, startdate, enddate,agencyid).ToList();
                    //  foreach (var ss in drVr )   

                    if (ss.AgencyAmount > 0)
                    {
                        mac += Math.Round((decimal)ss.AgencyAmount, 2, MidpointRounding.ToEven);
                    }
                    drnAG = dtInvoices.NewRow();
                    drnAG[0] = "Debit";
                    drnAG[1] = "Agency Commission";
                    drnAG[2] = 0;
                    drnAG[3] = 0;
                    drnAG[4] = 0;
                    drnAG[5] = Math.Round((decimal)mac, 2, MidpointRounding.ToEven);
                    drnAG[6] = 0;
                    drnAG[7] = ddlAgencyComm.SelectedValue;
                    drnAG[8] = ss.AgencyID;
                    drnAG[9] = ss.CompnayID;
                    drnAG[10] = ss.ID;
                    drnAG[11] = ss.Status;
                    //dtInvoices.Rows.InsertAt(drn, Rowcount + 1);
                    //dtInvoices.AcceptChanges();

                    totalDebit += Math.Round((decimal)(ss.AgencyAmount), 2, MidpointRounding.ToEven);


                    drn = dtInvoices.NewRow();
                    drn[0] = "Debit";
                    drn[1] = ss.Agency; ;
                    drn[2] = 0;
                    drn[3] = 0;
                    drn[4] = 0;
                    drn[5] = Math.Round((decimal)(ss.TAmt), 2, MidpointRounding.ToEven);
                    drn[6] = 0;
                    drn[7] = ss.AccountHeads;
                    drn[8] = ss.AgencyID;
                    drn[9] = ss.CompnayID;
                    drn[10] = ss.ID;
                    drn[11] = ss.Status;
                    dtInvoices.Rows.InsertAt(drn, Rowcount + 1);
                    dtInvoices.AcceptChanges();
                    totalDebit += Math.Round((decimal)(ss.TAmt), 2, MidpointRounding.ToEven);

                    if (ss.GSTAmount > 0)
                    {
                        mgst += Math.Round((decimal)ss.GSTAmount, 2, MidpointRounding.ToEven);
                    }

                    Rowcount = dtInvoices.Rows.Count;
                    drnGST[0] = "Credit";
                    drnGST[1] = ddlSalesTaxAccount.SelectedItem.Text; ; ;// tInfo.ToTitleCase(ddlSalesTaxAccount.SelectedItem.Value.ToLower()); //drn[1] = "Sales Tax";
                    drnGST[2] = 0;
                    drnGST[3] = 0;
                    drnGST[4] = 0;
                    drnGST[5] = Convert.ToDecimal("0");
                    drnGST[6] = Math.Round((decimal)mgst, 2, MidpointRounding.ToEven);
                    drnGST[7] = ddlSalesTaxAccount.SelectedValue;
                    drnGST[8] = ss.AgencyID;
                    drnGST[9] = ss.CompnayID;
                    drnGST[10] = ss.ID;
                    drnGST[11] = ss.Status;
                    //   dtInvoices.Rows.InsertAt(drn, Rowcount + 1);
                    //   dtInvoices.AcceptChanges();
                    totalCredit += Math.Round((decimal)(ss.GSTAmount), 2, MidpointRounding.ToEven);
                    if (ss.GrossAmount > 0)
                    {
                        mgross += Math.Round((decimal)(ss.GrossAmount), 2, MidpointRounding.ToEven);
                    }
                    Rowcount = dtInvoices.Rows.Count;
                    drn = dtInvoices.NewRow();
                    drn[0] = "Credit";
                    drn[1] = ddlRevenueAccount.SelectedItem.Text; ;
                    drn[2] = 0;
                    drn[3] = 0;
                    drn[4] = 0;
                    drn[5] = Convert.ToDecimal("0");
                    drn[6] = Math.Round((decimal)(mgross), 2, MidpointRounding.ToEven);
                    //drn[6] = r.AGCAmount - dr.GSTAmount)),2,MidpointRounding;// .GrossAmount, 2, MidpointRounding.ToEven));// ;, 2, MidpointRounding.ToEven); ;
                    drn[7] = ddlRevenueAccount.SelectedValue.ToString();
                    drn[8] = ss.AgencyID;
                    drn[9] = ss.CompnayID;
                    drn[10] = ss.ID;
                    drn[11] = ss.Status;
                    ; ;//    //  System.Configuration.ConfigurationManager.AppSettings["AgencyRevenue"];
                       // drn[11] = debit;

                    //dtInvoices.Rows.InsertAt(drn, Rowcount + 1);
                    // dtInvoices.AcceptChanges();
                    totalCredit += Math.Round((decimal)(ss.GrossAmount), 2, MidpointRounding.ToEven);


                    // dtInvoices.AcceptChanges();
                }
                if ((Convert.ToDecimal(drnAG[5]) > 0) || (Convert.ToDecimal(drnAG[6]) > 0))
                    dtInvoices.Rows.InsertAt(drnAG, Rowcount + 1);
                if ((Convert.ToDecimal(drn[5]) > 0) || (Convert.ToDecimal(drn[6]) > 0))
                    dtInvoices.Rows.InsertAt(drn, Rowcount + 1);

                if ((Convert.ToDecimal(drnGST[5]) > 0) || (Convert.ToDecimal(drnGST[6]) > 0))
                    dtInvoices.Rows.InsertAt(drnGST, Rowcount + 1);

                dtInvoices.AcceptChanges();
                drn = dtInvoices.NewRow();
                Rowcount = dtInvoices.Rows.Count;
                drn = dtInvoices.NewRow();
                drn[0] = "Totals";
                drn[1] = "Totals";
                drn[2] = "";
                drn[3] = "";
                drn[4] = "";
                drn[5] = totalDebit;//  Math.Round((debit + (decimal)ss.AgencyAmount), 2, MidpointRounding.ToEven);
                drn[6] = totalCredit;// Math.Round((decimal) credit + (decimal)ss.GSTAmount, 2, MidpointRounding.ToEven); ;
                drn[7] = "";
                drn[8] = "";
                drn[9] = "";
                ;// System.Configuration.ConfigurationManager.AppSettings["AgencyRevenue"];

                dtInvoices.Rows.InsertAt(drn, Rowcount + 1);
                //drn[10] = 0;
                //drn[11] = 0;



                dtInvoices.AcceptChanges();
                gv.DataSource = dtInvoices;
                gv.DataBind();

                if ((totalCredit == totalDebit) && (totalCredit > 0 && totalDebit > 0))
                {
                    btnGenerateVouher.Enabled = true;
                }
                else
                {
                    btnGenerateVouher.Enabled = false;
                }
            }
            else
            {
                lblmessage.Text = "No record found for posting";
            }
        }
        protected void btnPrint_Click1(object sender, EventArgs e)
        {

        }

        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv.EditIndex = e.NewEditIndex;
            ShowData();
        }

        protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


        }

        protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv.EditIndex = -1;
            ShowData();
        }
        private void ShowData(int agencyid)
        {
            int companyid = Convert.ToInt32(ddlGroupCompany.SelectedValue);


        }
        private void ShowData()
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            Button btn = (Button)sender;
            GridViewRow myRow = (GridViewRow)btn.Parent.Parent;  // the row
            HiddenField hdcompanyid = (HiddenField)myRow.FindControl("hdcompanyid");
            HiddenField hdaccountcode = (HiddenField)myRow.FindControl("hdheadaccount");
            HiddenField hdagencyid = (HiddenField)myRow.FindControl("hdGroupagencyID");
            Label lbl = (Label)myRow.FindControl("lblGroupAgency");
            lblvoucherNo.Text = string.Empty;
            lblGroupAgency.Text = lbl.Text;
            ViewState["hdAgencyID"] = hdagencyid.Value.ToString();
            txtHeadAccount.Text = hdaccountcode.Value.ToString();
            divUpdateAccountHead.Visible = true;
            int companyid = Convert.ToInt32(ddlGroupCompany.SelectedValue);
            int agencyid = Convert.ToInt32(hdagencyid.Value);
            using (GLEntities db2 = new GLEntities())
            {
                //  var c = db2.Companies.Where(x => x.Company_Id == companyid).SingleOrDefault();
                //  if (c != null)
                // {
                try
                {
                    int gcode = companyid;// Convert.ToInt32(c.Company_Id);
                    var ch = db2.ChartOfAccounts.Where(x => x.Comp_Code == gcode && x.Account_Code == txtHeadAccount.Text && x.Is_Transaction_Level == true).SingleOrDefault();
                    if (ch != null)
                    {

                    }
                    else
                    {
                        lblmessage.Text = "Invalid Account Account or Not found ";
                        return;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }


        }

    }
}

