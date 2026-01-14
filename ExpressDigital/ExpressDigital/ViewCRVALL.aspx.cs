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
    public partial class ViewCRVALL : System.Web.UI.Page
    {
        static bool _isSqlTypesLoaded = false;
        public decimal TotalUnitCRVAmount;
        public decimal TotalUnitWHTax;
        public decimal TotalUnitGST;
        public decimal TotalUnitClientCRV;
        public decimal TotalUnitRemainingCRV;
        DbDigitalEntities db = new DbDigitalEntities();

        public ViewCRVALL()//Need to ask
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
                if (Request.QueryString["ID"] != null)
                {
                    Int32 nCRVId = int.Parse(Request.QueryString["ID"].ToString());
                    txtCrvNo.Text = "CRV-" + nCRVId.ToString();
                }

                BindDropDowns();

                //txtSearchROMODateFrom.Text = DateTime.Now.AddDays(-31).ToString("dd/MM/yyyy");
                //txtSearchROMODateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
               // btnSearch_Click(null, null);
            }
        }

        private void BindDropDowns()
        {
            var city = db.CityManagements.Where(x => x.IsActive == true).OrderBy(x => x.CityName).ToList();
            ddlCity.DataValueField = "ID";
            ddlCity.DataTextField = "CityName";
            ddlCity.DataSource = city;
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("All", "0"));
            ddlCity_SelectedIndexChanged(null, null);

            var com = db.Companies.Where(x => x.Active == true).OrderBy(x => x.Company_Name).ToList();
            ddlCompany.DataValueField = "Company_Id";
            ddlCompany.DataTextField = "Company_Name";
            ddlCompany.DataSource = com;
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("All", "0"));

            var sts = db.CRVStatus.ToList();
            ddlstatus.DataValueField = "ID";
            ddlstatus.DataTextField = "Status";
            ddlstatus.DataSource = sts;
            ddlstatus.DataBind();
            ddlstatus.Items.Insert(0, new ListItem("All", "0"));
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblcounter.Text = String.Empty;
            lblmessage.Text = string.Empty;
            Int32? AgencyId;
            Int32? ClinetId;
            Int32? companyid;
            DateTime? StartDate;
            DateTime? EnDate;
            Int32? invstatus = null;
            String strchk = null;
            String strCRV = null;
            Int32? cityid;
            string agencyname = "";
            if (ddlCity.SelectedIndex == 0)
                cityid = 0;
            else
                cityid = Convert.ToInt32(ddlCity.SelectedValue);

            if (ddlstatus.SelectedIndex == 0)
                invstatus = 0;
            else
                invstatus = Convert.ToInt32(ddlstatus.SelectedValue);

            if (ddlCompany.SelectedValue == "0")
                companyid = null;
            else
                companyid = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlmasteragency.SelectedIndex == 0)
            {
                agencyname = "-1";

            }
            else
                agencyname = (ddlmasteragency.SelectedItem.Text);

            if (ddlAgency.SelectedIndex == 0)
            {
                AgencyId = -1;

            }
            else
                AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);

            if (ddlClient.SelectedIndex == 0)
            {
                ClinetId = -1;
            }
            else
                ClinetId = Convert.ToInt32(ddlClient.SelectedValue);
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

            if (txtChkNo.Text.Trim().Length == 0)
                strchk = " ";
            else
                strchk = txtChkNo.Text;

            if (txtCrvNo.Text.Trim().Length == 0)
                strCRV = null;
            else
                strCRV = txtCrvNo.Text;

            try
            {
                var g = db.usp_CRVView_aLL(null, strCRV, AgencyId, ClinetId, null, null, strchk, StartDate, null, null, null, null, invstatus, null, null, null, EnDate, cityid, null, "-1", companyid).OrderBy(x => x.CRVId).ToList();
                lblcounter.Text = "Total Record Found: " + g.Count.ToString();
                gvCRV.DataSource = g;
                gvCRV.DataBind();

            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
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

        public decimal GetUnitCRVRemAmount(object CRVRemAmount)
        {
            decimal mCRVRemAmount = 0;
            // 26-02-2021 temp changing
            try
            {

                mCRVRemAmount = Convert.ToDecimal(CRVRemAmount);
                TotalUnitRemainingCRV += mCRVRemAmount;
            }
            catch (Exception)
            {

                TotalUnitRemainingCRV += 0;
            }


            return mCRVRemAmount;
        }

        public decimal GetCRVRemTotal()
        {
            return TotalUnitRemainingCRV;
        }

        public string ChallanStatus(string CRVId)
        {
            int _crvid = Convert.ToInt32(CRVId);
            var objCRV = db.tblCRVs.Where(x => x.CRVId == _crvid).FirstOrDefault();
            if (objCRV.IsChallanRecieved.ToString() == "True")
            {
                return "Recieved";
            }
            else
            {
                return "Required";
            }
        }

        public object ChallanStatusColor(string CRVId)
        {

            int _crvid = Convert.ToInt32(CRVId);
            var objCRV = db.tblCRVs.Where(x => x.CRVId == _crvid).FirstOrDefault();

            if (objCRV.IsChallanRecieved.ToString() == "True")
                return System.Drawing.Color.Green;
            else
                return System.Drawing.Color.Red;
        }
        public object CRVRemainingStatusColor(string CRVDetailId, string CRVId)
        {
            int _crvid = Convert.ToInt32(CRVId);
            int _crddetailid = Convert.ToInt32(CRVDetailId);
            var objCRV = db.tblCRVs.Where(x => x.CRVId == _crvid).FirstOrDefault();
            var objCRVDetail = db.tblCRVDetails.Where(x => x.CRVDetailId == _crddetailid).FirstOrDefault();

            decimal mRemainingAmount = 0;
            try
            {
                mRemainingAmount = Convert.ToInt32(objCRVDetail.RemainingAmount);
            }
            catch (Exception)
            {

                mRemainingAmount = 0;
            }

            if (Math.Round(Convert.ToDecimal(objCRV.CRVAmount) + Convert.ToDecimal(objCRV.WithHoldingTax) + Convert.ToDecimal(objCRV.GST)) == Math.Round(mRemainingAmount))
                return System.Drawing.Color.Black;
            else
                return System.Drawing.Color.Green;
        }

        public bool isCleared(string status, string CRVid)
        {
            int _crvid = Convert.ToInt32(CRVid);
            var _dtPayment = db.Payments.Where(x => x.CRVId == _crvid).Take(1).SingleOrDefault();
            var _dtCRVDetail = db.tblCRVDetails.Where(x => x.CRVId == _crvid).SingleOrDefault();

            if ((status == "Cleared") && (_dtPayment != null) && (_dtCRVDetail != null))//. .cou.Rows.Count == 1))
                return true;
            else
                return false;
        }
        public bool isReversed(string status, string CRVid)
        {
            int _crvid = Convert.ToInt32(CRVid);
            var _dtPayment = db.Payments.Where(x => x.CRVId == _crvid).FirstOrDefault();
            if ((status == "Reversed"))
                return false;
            else
                return true;
        }

        public string GetCRVNo(string CRVNo)
        {
            return "'" + CRVNo + "'";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
        }

        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Agencyid = Convert.ToInt32(ddlAgency.SelectedValue);
            var c = db.Clients.Where(x => x.AgencyID == Agencyid).ToList().OrderBy(x => x.Client1);
            ddlClient.DataValueField = "ID";
            ddlClient.DataTextField = "Client1";
            ddlClient.DataSource = c;
            ddlClient.DataBind();
            ddlClient.Items.Insert(0, new ListItem("Select Client", "0"));

        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(ddlCity.SelectedValue);
            var g = db.GroupAgencies.Where(x => x.CityID == id).OrderBy(x => x.GroupName).ToList();
            ddlmasteragency.DataValueField = "ID";
            ddlmasteragency.DataTextField = "GroupName";
            ddlmasteragency.DataSource = g;
            ddlmasteragency.DataBind();
            ddlmasteragency.Items.Insert(0, new ListItem("Select Agency", "0"));
            ddlmasteragency_SelectedIndexChanged(null, null);
        }

        protected void ddlmasteragency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlmasteragency.SelectedValue);
            var g = db.Agencies.Where(x => x.GroupID == id).OrderBy(x => x.AgencyName).ToList();
            ddlAgency.DataValueField = "ID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = g;
            ddlAgency.DataBind();
            ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
            ddlAgency_SelectedIndexChanged(null, null);

        }
        protected void gvCRV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbSettlement = (LinkButton)e.Row.FindControl("LinkSettlement");
                HyperLink lbEdit = (HyperLink)e.Row.FindControl("HyperLink1");
                string _sts = e.Row.Cells[20].Text;
                if (_sts == "Cleared")
                {

                }
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow row = (GridViewRow)checkbox.NamingContainer;

            var objCRV = db.tblCRVs.Where(x => x.CRVId == (int.Parse(row.Cells[0].Text))).FirstOrDefault();

            if (objCRV != null)
            {
                if (checkbox.Checked == true)
                    objCRV.IsChallanRecieved = true;
                else
                    objCRV.IsChallanRecieved = false;

                db.SaveChanges();
            }
        }

        protected void gvCRV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SettlementPlan")
            {
                LinkButton lb = (LinkButton)e.CommandSource;
                GridViewRow gvr = (GridViewRow)lb.NamingContainer;
                int id = (Int32)gvCRV.DataKeys[gvr.RowIndex].Value;
                var objCRVDB = db.tblCRVs.Where(x => x.CRVId == id).FirstOrDefault();

                if (objCRVDB.Status == 110000001)
                    Response.Redirect("CRVSettlement.aspx?CRVId=" + int.Parse(Convert.ToString(e.CommandArgument)));
                else if (objCRVDB.Status == 110000003)
                    Response.Redirect("Payments.aspx?CRVId=" + int.Parse(Convert.ToString(e.CommandArgument)));
            }
        }
        protected void btnSearch_Click_New(object sender, EventArgs e)
        {
            var dateFrom = Convert.ToDateTime(Helper.SetDateFormatString(txtSearchROMODateFrom.Text));
            var dateTo = Convert.ToDateTime(Helper.SetDateFormatString(txtSearchROMODateTo.Text)).AddDays(1);
            var crvNo = txtCrvNo.Text.Trim();
            var companyId = Convert.ToInt32(ddlCompany.SelectedValue);


            if (!string.IsNullOrEmpty(crvNo))
            {
                var dtFrom = new DateTime(2021, 1, 1).ToString("dd/MM/yyyy");
                dateFrom = Convert.ToDateTime(Helper.SetDateFormatString(dtFrom));
                dateTo = Convert.ToDateTime(Helper.SetDateFormatString(DateTime.Now.AddDays(1).ToString("dd/MM/yyyy")));
            }

            var data = from x in db.tblCRVs where x.CRVDate >= dateFrom && x.CRVDate <= dateTo select x;
            var request = from x in data
                          select new
                          {
                              //CRVId = x.CRVId,
                              //CRVNo = x.CRVNo,
                              //DepartmentID = x.compa,
                              //CompanyID = x.CompanyID,
                              //EmailSent = x.EmailSent,
                              //UserName = x.UserInfo.UserName,
                              //DesignationName = x.UserInfo.Designation.DesignationName,
                              //HODName = x.UserInfo1.UserName,
                              //PriorityStatus = x.PriorityStatus == false ? "Normal" : "High",
                              //BuyingType = x.BuyingType == "L" ? "Local" : "Imported",
                              //ApprovalStatus = x.ApprovalStatus,
                              //DateRequest = x.DateRequest,
                              //ApprovedDate = x.ApprovedDate,
                              //SentForApprovalDate = x.SentForApprovalDate,
                              ////Purpose = x.Purpose,
                              //Purpose = x.Purpose.Length > 30 ? x.Purpose.Substring(0, 30) + " ..." : x.Purpose,
                              //PurposeToolTip = x.Purpose,
                              //Product = x.RequisitionDetails.Where(y => y.IsFinanceApproved == true && y.RequestedToProcure == true).Select(y => y.Product),
                              //UserInfo = x.UserInfo,
                              //SentForQuotationDate = x.SentForQuotationDate,
                              //CityName = x.City.CityName,
                              //PurchaseFrom = x.IsHeadOfficePurchase == true ? "HO" : "Local",
                              //Company = x.Company.Company_Name
                          };



            lblcounter.Text = String.Empty;
            lblmessage.Text = string.Empty;
            Int32? AgencyId;
            Int32? ClinetId;
            Int32? companyid;
            DateTime? StartDate;
            DateTime? EnDate;
            Int32? invstatus = null;
            String strchk = null;
            String strCRV = null;
            Int32? cityid;
            string agencyname = "";
            if (ddlCity.SelectedIndex == 0)
                cityid = 0;
            else
                cityid = Convert.ToInt32(ddlCity.SelectedValue);

            if (ddlstatus.SelectedIndex == 0)
                invstatus = 0;
            else
                invstatus = Convert.ToInt32(ddlstatus.SelectedValue);

            if (ddlCompany.SelectedValue == "0")
                companyid = null;
            else
                companyid = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlmasteragency.SelectedIndex == 0)
            {
                agencyname = "-1";

            }
            else
                agencyname = (ddlmasteragency.SelectedItem.Text);

            if (ddlAgency.SelectedIndex == 0)
            {
                AgencyId = -1;

            }
            else
                AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);

            if (ddlClient.SelectedIndex == 0)
            {
                ClinetId = -1;
            }
            else
                ClinetId = Convert.ToInt32(ddlClient.SelectedValue);
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

            if (txtChkNo.Text.Trim().Length == 0)
                strchk = " ";
            else
                strchk = txtChkNo.Text;

            if (txtCrvNo.Text.Trim().Length == 0)
                strCRV = null;
            else
                strCRV = txtCrvNo.Text;

            try
            {
                var g = db.usp_CRVView_aLL(null, strCRV, AgencyId, ClinetId, null, null, strchk, StartDate, null, null, null, null, invstatus, null, null, null, EnDate, cityid, null, "-1", companyid).OrderBy(x => x.CRVId).ToList();
                lblcounter.Text = "Total Record Found: " + g.Count.ToString();
                gvCRV.DataSource = g;
                gvCRV.DataBind();

            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }

        }

    }
}