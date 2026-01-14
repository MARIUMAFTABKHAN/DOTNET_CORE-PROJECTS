using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
namespace ExpressDigital
{

    public partial class CRVView : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        public decimal TotalUnitCRVAmount;
        public decimal TotalUnitWHTax;
        public decimal TotalUnitGST;
        public decimal TotalUnitClientCRV;
        public decimal TotalUnitRemainingCRV;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCRVStatus();
                PopulateCity();
                PopulateAgency();
                PopulateClient(Int32.Parse(ddlAgency.SelectedItem.Value));
                PopulateAllCRV(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value));
                ///PopulateAllCRV(101, 262);
                // PopulateCRVGrid();
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


        public string ChallanStatus(string CRVId)
        {
            int crvid = Convert.ToInt32(CRVId);


            var objCRV = db.tblCRVs.Where(x => x.CRVId == crvid).SingleOrDefault(); ;
            //Response.Write(CRVId);
            //Response.Write(objCRV.IsChallanRecieved.ToString());

            //Response.End(); 
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
            int crvid = Convert.ToInt32(CRVId);
            var objCRV = db.tblCRVs.Where(x => x.CRVId == crvid).SingleOrDefault(); ;

            //Response.End(); 
            if (objCRV.IsChallanRecieved.ToString() == "True")
            {
                return System.Drawing.Color.Green;
            }
            else
            {
                return System.Drawing.Color.Red;
            }


        }
        public object CRVRemainingStatusColor(string CRVDetailId, string CRVId)
        {
            int crvid = Convert.ToInt32(CRVId);
            int CRVDtId = Convert.ToInt32(CRVDetailId);
            var objCRV = db.tblCRVs.Where(x => x.CRVId == crvid).SingleOrDefault();
            var objCRVDetail = db.tblCRVDetails.Where(x => x.CRVDetailId == CRVDtId).SingleOrDefault();

            if (Math.Round(Convert.ToDecimal(objCRV.CRVAmount + objCRV.WithHoldingTax + objCRV.GST)) == Math.Round(Convert.ToDecimal(objCRVDetail.RemainingAmount)))
            {
                return System.Drawing.Color.Black;
            }
            else
            {
                return System.Drawing.Color.Green;
            }
        }



        public bool isCleared(string status, string CRVid)
        {
            int crvid = Convert.ToInt32(CRVid);
            var objPayment = db.Payments.Where(x => x.CRVId == crvid).ToList();// .SingleOrDefault();  // .usp_GetAllPayment(pay objPaymentDB.GetAllPayment(null, int.Parse(CRVid), null, null, null, null, null, null);
            var _CRVDetail = db.tblCRVDetails.Where(x => x.CRVId == crvid).ToList();//  RVid), null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            if ((status == "Cleared") && (objPayment.Count == 0) && (_CRVDetail.Count == 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isReversed(string status, string CRVid)
        {
            int crvid = Convert.ToInt32(CRVid);
            var objPayment = db.Payments.Where(x => x.CRVId == crvid).ToList();// .SingleOrDefault();  // .usp_GetAllPayment(pay objPaymentDB.GetAllPayment(null, int.Parse(CRVid), null, null, null, null, null, null);
                                                                               //
                                                                               //  _dtPayment = objPaymentDB.GetAllPayment(null, int.Parse(CRVid), null, null, null, null, null, null);



            //  if ((status == "Reversed") || (status == "Closed"))
            if ((status == "Reversed"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetCRVNo(string CRVNo)
        {

            return "'" + CRVNo + "'";


        }

        protected void PopulateAgency()
        {
            //if (chkAgencyByName.Checked == false)
            {
                //DataTable _dtAgency;
                //objAgency = new CTS.Agency();
                //objAgencyDB = new CTS.AgencyDB();

                //_dtAgency = objAgencyDB.GetAllAgency(null,  null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, true,null);
                var _dtAgency = db.Agencies.Where(x => x.Active == true).ToList().OrderBy(x => x.AgencyName);// ..acis.Active  .usp_GetAll A gencyRestoreData; objAgencyDB.GetAllAgencies(0, 1);
                ddlAgency.DataSource = _dtAgency;
                ddlAgency.DataTextField = "AgencyName";
                ddlAgency.DataValueField = "ID";
                ddlAgency.DataBind();
                ListItem lst = new ListItem("--All--", "-1");
                ddlAgency.Items.Insert(0, lst);
            }
            //else
            //{
            //    //parameters.Clear();
            //    ////parameters.Add("@MonthId", ((DateTime)dtPicker.DbSelectedDate).Month);
            //    ////parameters.Add("@YearId", ((DateTime)dtPicker.DbSelectedDate).Year);
            //    //DataSet ds = objDbAccess.FillData("usp_GetAllAgencyDistinctName", parameters, "Name", true);
            //    //DataTable dt = ds.Tables[0];
            //    var dt = db.usp_GetAllAgencyDistinctName(). 
            //    ddlAgency.DataSource = dt;
            //    ddlAgency.DataTextField = "Name";
            //    ddlAgency.DataValueField = "AgencyID";
            //    ddlAgency.DataBind();
            //    ListItem lst = new ListItem("--All--", "-1");
            //    ddlAgency.Items.Insert(0, lst);
            //}
        }

        protected void PopulateClient(Int32 agencyId)
        {
            // if (chkAgencyByName.Checked == false)
            {
                //DataTable _dtClient;
                //objClient = new CTS.Client();
                //objClientDB = new CTS.ClientDB();
                //_dtClient = objClientDB.GetClientOnAgency(agencyId);
                try
                {
                    var client = db.Clients.Where(x => x.AgencyID == agencyId).ToList();
                    ddlClient.DataTextField = "Client1";
                    ddlClient.DataValueField = "ID";
                    ddlClient.DataSource = client;
                    ddlClient.DataBind();
                }
                catch (Exception)
                {

                }

                ListItem lst = new ListItem("--All--", "-1");
                ddlClient.Items.Insert(0, lst);
            }
            //else
            //{
            //    parameters.Clear();
            //    parameters.Add("@AgencyName", ddlAgency.SelectedItem.Text);
            //    //parameters.Add("@YearId", ((DateTime)dtPicker.DbSelectedDate).Year);
            //    DataSet ds = objDbAccess.FillData("usp_GetClientByAgencyName", parameters, "Name", true);
            //    DataTable dt = ds.Tables[0];
            //    ddlClient.DataTextField = "Name";
            //    ddlClient.DataValueField = "ClientId";
            //    ddlClient.DataSource = dt;
            //    ddlClient.DataBind();
            //    ListItem lst = new ListItem("--All--", "-1");
            //    ddlClient.Items.Insert(0, lst);
            //}

        }

        protected void test(object sender, EventArgs e)
        {

            Response.Write("Junaid");
            Response.End();

        }

        protected void btnExportReport_Click(object sender, EventArgs e)
        {
            string crvNo;
            if (ddlCRV.SelectedItem.Value.ToString() == "0")
            {
                crvNo = txtCRVNo.Text;
            }
            else
            {
                crvNo = ddlCRV.SelectedItem.Text.ToString();
            }

            string chqNo = txtChqNo.Text;

            Int32 status = Int32.Parse(ddlStatus.SelectedItem.Value);


            DateTime tempDate;
            if (Helper.SetDateFormat(FromDatePicker.Text) > Helper.SetDateFormat(ToDatePicker.Text))
            {
                tempDate = Helper.SetDateFormat(FromDatePicker.Text);
                FromDatePicker.Text = ToDatePicker.Text;
                ToDatePicker.Text = tempDate.ToString("dd/MM/yyyy");
            }
            string FromDate;
            try
            {
                Convert.ToDateTime(FromDatePicker.Text);
            }
            catch (Exception)
            {
                FromDate = null;
            }
            string TODate;
            try
            {
                Convert.ToDateTime(ToDatePicker.Text);
            }
            catch (Exception)
            {

                TODate = null;
            }



            //  DataTable _dtCRV;
            int agencyid = Convert.ToInt32(ddlAgency.SelectedValue);
            var objCRV = db.tblCRVs.Where(x => x.AgencyId == agencyid).ToList();// DbDigitalDataSet.  CTS.CRVDB();

            //if (chkAgencyByName.Checked == false)
            //{
            //    _dtCRV = objCRVDB.GetAllCRVView(null, crvNo, Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), null, null, chqNo, FromDate, null, null, null, null, status, null, null, null, TODate, Int32.Parse(ddlHOCity.SelectedItem.Value), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId), "-1");
            //}
            //else
            //{

            //    _dtCRV = objCRVDB.GetAllCRVView(null, crvNo, Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), null, null, chqNo, FromDate, null, null, null, null, status, null, null, null, TODate, Int32.Parse(ddlHOCity.SelectedItem.Value), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId), ddlAgency.SelectedItem.Text);
            //}


            // gvCRV.DataSource = _dtCRV;
            // gvCRV.DataBind();

            if (objCRV.Count <= 0)
            {
                lblMsg.Text = "Data not available for Agency Name : " + ddlAgency.SelectedItem.Text;
                return;
            }

            CheckBox cb;
            foreach (GridViewRow challanRecieved in gvCRV.Rows)
            {

                cb = (CheckBox)(challanRecieved.FindControl("CheckBox1"));
                if (cb != null)
                {
                    foreach (var rowcrv in objCRV)
                    {
                        cb.Checked = bool.Parse(rowcrv.IsChallanRecieved.ToString());
                    }

                }

            }
            try
            {
                if (objCRV.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = Helper.ToDataTable(objCRV);
                    XLWorkbook wb = new XLWorkbook();
                    wb.Worksheets.Add(dt);
                    var ws = wb.AddWorksheet("Over Due Summary");
                    string TargetPath = Server.MapPath("~/Downloads/") + "ViewAllCRV.xlsx";
                    wb.SaveAs(TargetPath);
                    FileInfo file = new FileInfo(TargetPath);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Cache.SetCacheability(HttpCacheability.Public);
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.WriteFile(file.FullName);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception)
            {

            }
        }


        protected void PopulateCRVGrid()
        {

            string crvNo;

            if (ddlCRV.SelectedItem.Value.ToString() == "0")
            {
                crvNo = txtCRVNo.Text;
            }
            else
            {
                crvNo = ddlCRV.SelectedItem.Text.ToString();
            }

            if (crvNo.Length == 0)
            {
                //lblMsg.Text = "Please Select CRV From the dropdown";
                //return;
            }
            string chqNo = txtChqNo.Text;



            Int32 status = Int32.Parse(ddlStatus.SelectedItem.Value);


            DateTime tempDate;
            string FromDate;
            string TODate;
            try
            {
                Convert.ToDateTime(FromDatePicker.Text);
                Convert.ToDateTime(ToDatePicker.Text);
                if (Helper.SetDateFormat(FromDatePicker.Text) > Helper.SetDateFormat(ToDatePicker.Text))
                {
                    tempDate = Helper.SetDateFormat(FromDatePicker.Text);
                    FromDatePicker.Text = ToDatePicker.Text;
                    ToDatePicker.Text = tempDate.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception)
            {
                FromDate = null;
                TODate = null;

            }
            int? cityid;
            int? crvid = null;
            string crvno;
            int? agencyid;
            int? clientid;
            int? paymentid;
            int? paymenttypeid;
            string paymentnumber;
            DateTime? crvfromdate;
            DateTime? crvtodate;
            int? currencyid = null;
            DateTime? crvDate;
            Boolean? ischallanreceived;
            DateTime? chequeData;


            if (txtCRVNo.Text.Trim() == " ")
                crvno = null;
            else
                crvno = txtCRVNo.Text;

            if (ddlAgency.SelectedIndex == 0)
                agencyid = null;

            else
                agencyid = Convert.ToInt32(ddlAgency.SelectedValue);

            int ChannelID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ChannelID"]);

            if (ddlClient.SelectedIndex == 0)
                clientid = null;
            else
                clientid = Convert.ToInt32(ddlClient.SelectedValue);

            if (txtChqNo.Text.Trim().Length == 0)
                paymentnumber = "";
            else
                paymentnumber = txtChqNo.Text;

            if (FromDatePicker.Text.Trim().Length == 0 || ToDatePicker.Text.Trim().Length == 0)
            {
                crvfromdate = null;
                crvtodate = null;
            }

            else
            {
                crvfromdate = Helper.SetDateFormat(FromDatePicker.Text);
                crvtodate = Helper.SetDateFormat(ToDatePicker.Text);
            }
            if (ddlHOCity.SelectedIndex == 0)
                cityid = null;
            else
                cityid = Convert.ToInt32(ddlHOCity.SelectedValue);

            if (ddlStatus.SelectedIndex == 0)
                paymenttypeid = null;
            else
                paymenttypeid = Convert.ToInt32(ddlStatus.SelectedValue);


            // objCRVDB.GetAllCRVView(crvNo, Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), chqNo, FromDate, status, TODate, Int32.Parse(ddlHOCity.SelectedItem.Value), channelID , "-1");
            var objCRV = db.usp_GetAllCRVView_4(null, crvNo, agencyid, clientid, null, paymenttypeid, chqNo, crvfromdate, null, null, null, null, null, null, crvtodate, cityid, ChannelID, "").ToList();
            
            
            gvCRV.DataSource = objCRV;
            gvCRV.DataBind();

            if (objCRV.Count <= 0)
            {
                lblMsg.Text = "Data not available for Agency Name : " + ddlAgency.SelectedItem.Text;
            }

            CheckBox cb;
            foreach (GridViewRow challanRecieved in gvCRV.Rows)
            {

                cb = (CheckBox)(challanRecieved.FindControl("CheckBox1"));
                if (cb != null)
                {

                    foreach (var rowcrv in objCRV)
                    {
                        cb.Checked = bool.Parse(rowcrv.IsChallanRecieved.ToString());
                    }

                }

            }

        }



        protected void PopulateCRVStatus()
        {
            DataTable _dtCRVStatus;
            var CRVStatus = db.CRVStatus.Where(x => x.Active == true).ToList(); // objCRVStatusDB.GetAllCRVStatus(null, null, true);

            ddlStatus.DataSource = CRVStatus;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataBind();
            ListItem lst = new ListItem("-- All --", "0");
            ddlStatus.Items.Insert(0, lst);
            ddlStatus.Items.RemoveAt(7); // Remove PDC 
        }

        protected void PopulateAllCRV(Int32 agencyId, Int32 clientId)
        {
            string FromDate;
            try
            {
                Convert.ToDateTime(FromDatePicker.Text);
            }
            catch (Exception)
            {
                FromDate = null;
            }
            string TODate;
            try
            {
                Convert.ToDateTime(ToDatePicker.Text);
            }
            catch (Exception)
            {

                TODate = null;
            }

            int agencyid = Convert.ToInt32(ddlAgency.SelectedValue);
            var objCRV = db.tblCRVs.Where(x => x.AgencyId == agencyid).ToList();// DbDigitalDataSet.  CTS.CRVDB();

            //DataTable _dtCRV;

            //objCRVDB = new CTS.CRVDB();
            //if (chkAgencyByName.Checked == false)
            //{
            //    _dtCRV = objCRVDB.GetAllCRVView_(null, null, Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), null, null, txtChqNo.Text.ToString(), FromDate, null, null, null, null, Int32.Parse(ddlStatus.SelectedItem.Value), null, null, null, TODate, Int32.Parse(ddlHOCity.SelectedItem.Value), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId), null, "-1");
            //}
            //else
            //{
            //    _dtCRV = objCRVDB.GetAllCRVView_(null, null, Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value), null, null, txtChqNo.Text.ToString(), FromDate, null, null, null, null, Int32.Parse(ddlStatus.SelectedItem.Value), null, null, null, TODate, Int32.Parse(ddlHOCity.SelectedItem.Value), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId), null, ddlAgency.SelectedItem.Text);
            //}
            //ddlCRV.Items.Clear();
            //txtCRVNo.Text = _dtCRV.Rows.Count.ToString(); 
            //if (_dtCRV.Rows.Count > 0)
            //{
            ddlCRV.DataSource = objCRV;
            ddlCRV.DataTextField = "CRVNo";
            ddlCRV.DataValueField = "CRVId";
            ddlCRV.DataBind();
            ListItem lst = new ListItem("--All--", "0");
            ddlCRV.Items.Insert(0, lst);
            //}
        }

        //protected void btnCreateNew_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("CRV.aspx");
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateCRVGrid();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //  txtCRVNo.Text = "";
            // ddlStatus.SelectedIndex = 0;
            //  FromDatePicker.SelectedDate = DateTime.Parse("0001.1.1");

            Response.Redirect("CRVView.aspx");
        }
        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {

            PopulateClient(Int32.Parse(ddlAgency.SelectedItem.Value));
            //   PopulateAllCRV(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value));

        }
        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {

            PopulateAllCRV(Int32.Parse(ddlAgency.SelectedItem.Value), Int32.Parse(ddlClient.SelectedItem.Value));

        }

        protected void ddlCRV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCRV.SelectedItem.Value.ToString() == "-1")
            {
                txtCRVNo.Enabled = true;
            }
            else
            {
                txtCRVNo.Enabled = false;
            }
            PopulateCRVGrid();

        }

        protected void gvCRV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "ReverseCRV")
            {
                //objPayment = new CTS.Payment();
                //objPaymentDB = new CTS.PaymentDB();
                int ca = Convert.ToInt32(e.CommandArgument);
                var _dtPayment = db.Payments.Where(x => x.CRVId == ca);
                //  var = objPaymentDB.GetAllPayment(null, int.Parse(Convert.ToString(e.CommandArgument)), null, null, null, null, null, null);

                if (_dtPayment != null)
                {
                    //Response.Write("count = 0");
                    //Response.End();
                    UpdateValues(int.Parse(Convert.ToString(e.CommandArgument)));

                    var objSettlementPlan = db.SettlementPlans.Where(x => x.CRVId == ca).SingleOrDefault();// new CTS.SettlementPlanDB();
                                                                                                           //  DataTable _dtSettlement;
                                                                                                           //  _dtSettlement = objSettlementPlanDB.GetAllSettlementPlan(null, int.Parse(Convert.ToString(e.CommandArgument)), null, null, null, null, null, null, null);
                    if (objSettlementPlan != null)
                    {
                        db.usp_DeleteKnockOfPlan(ca);// .DeleteKnockOfPlan(int.Parse(Convert.ToString(e.CommandArgument)));
                    }
                    PopulateCRVGrid();
                }
                else
                {
                    //Response.Write("count > 0");
                    //Response.End();
                }

            }

            if (e.CommandName == "SettlementPlan")
            {
                // int companyid = gvCRV.DataKeyNames[""]
                //objCRVDB = new CTS.CRVDB();
                //// added by Aijaz - CRVs of other Channels are Visible but Read-only - Req. by Ehsan on 26-Jan-2013


                int crvid = int.Parse(Convert.ToString(e.CommandArgument));//  == 1
                var objCRV = db.tblCRVs.Where(x => x.CRVId == crvid).SingleOrDefault();
                if ((Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ChannelId"]) != objCRV.ChannelId))
                {
                    return;
                    //Response.Redirect("CRVView.aspx");
                }
                if (objCRV.Status == 110000001)
                {
                    Response.Redirect("CRVSettlement.aspx?CRVId=" + int.Parse(Convert.ToString(e.CommandArgument)));
                    // return "'" + Response.Redirect("SettlementPlan.aspx?CRVId="+int.Parse(CRVId)) + "'";
                }
                else if (objCRV.Status == 110000003)
                {
                    Response.Redirect("Payments.aspx?CRVId=" + int.Parse(Convert.ToString(e.CommandArgument)));
                }

            }
        }

        protected void UpdateValues(int CRVId)
        {
            var objCRV = db.tblCRVs.Where(x => x.CRVId == CRVId).SingleOrDefault();

            // objCRVDetail = new CTS.CRVDetail();


            var objcrvDetail = db.tblCRVDetails.Where(x => x.CRVId == CRVId).SingleOrDefault();
            if (objcrvDetail != null)
            {
                if (objCRV.Status.ToString() == "110000003")
                {
                    int chk = 0;

                    if (objCRV.CRVAmount.ToString() == "0.0000")
                    {
                        if ((objCRV.WithHoldingTax == 0) && (objCRV.GST == 0))
                        {
                            chk = 0;
                        }
                        else if ((objCRV.WithHoldingTax != 0 && (objCRV.GST == 0)))
                        {
                            chk = 1; /// Tax
                        }
                        else if ((objCRV.WithHoldingTax == 0 && objCRV.GST != 0))
                        {
                            chk = 2; /// GST
                        }
                        else if ((objCRV.WithHoldingTax == 0 && objCRV.GST != 0))
                        {
                            chk = 3; /// Tax + GST
                        }
                    }
                    else if (objCRV.CRVAmount != 0)
                    {
                        if ((objCRV.WithHoldingTax == 0 && (objCRV.GST == 0)))
                        {
                            chk = 4; /// CRV
                        }
                        else if ((objCRV.WithHoldingTax != 0 && (objCRV.GST == 0)))
                        {
                            chk = 5; /// CRV + Tax
                        }
                        else if ((objCRV.WithHoldingTax == 0 && (objCRV.GST != 0)))
                        {
                            chk = 6; /// CRV + GST
                        }
                        else if ((objCRV.WithHoldingTax != 0 && objCRV.GST != 0))
                        {
                            chk = 7; /// CRV + Tax + GST
                        }
                    }
                    bool crv = false;
                    bool tax = false;
                    bool gst = false;

                    int Agencyid = Convert.ToInt32(objCRV.AgencyId);
                    int channelid = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ChannelId"]);
                    //   DataTable dtNetLedger;
                    //   DataTable dtClientLedger;
                    var lbid = db.usp_IDctr("LedgerBalance").SingleOrDefault().Value;
                    LedgerBalance objLedgerBalance = new LedgerBalance();
                    objLedgerBalance.ID = lbid;
                    objLedgerBalance.AgencyId = int.Parse(objCRV.AgencyId.ToString());
                    objLedgerBalance.ClientId = int.Parse(objcrvDetail.ClientId.ToString());
                    objLedgerBalance.CRVID = CRVId;
                    objLedgerBalance.CompanyID = objCRV.CompanyId;
                    //objLedgerBalance.p.ChannelId = channelid   ;// Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId);
                    int clinetid = objLedgerBalance.ClientId;
                    objLedgerBalance.TransactionDate = DateTime.Now;

                    //var NetLedger = Convert.ToDecimal(db.LedgerBalances.Where(x => x.AgencyId == Agencyid && x.ClientId == clinetid).ToList().Sum(X => X.NetBalance.Value));
                    //var ClientLedger = Convert.ToDecimal(db.LedgerBalances.Where(x => x.AgencyId == Agencyid && x.ClientId == clinetid).ToList().Sum(X => X.ClientBalance.Value));
                    var dtNetLedger = db.GetAgencyBalance(objCRV.CompanyId, objCRV.AgencyId).SingleOrDefault(); ;
                    var dtClientLedger = db.GetClientBalance(objCRV.CompanyId, objCRV.AgencyId, clinetid).SingleOrDefault();
                    if (((chk == 4) || (chk == 5) || (chk == 6) || (chk == 7)) && crv == false)  // CRV 
                    {

                        objLedgerBalance.IsCRV = true;
                        objLedgerBalance.IsTax = false;
                        objLedgerBalance.ReceiptAmount = -decimal.Parse(objCRV.CRVAmount.ToString());
                        objLedgerBalance.NetBalance = dtNetLedger.Value - (-decimal.Parse(objCRV.CRVAmount.ToString()));//decimal.Parse(txtAmount.Text.Trim());
                        objLedgerBalance.ClientBalance = dtClientLedger.Value - (-decimal.Parse(objCRV.CRVAmount.ToString())); //decimal.Parse(txtAmount.Text.Trim());
                        objLedgerBalance.BillAmount = 0;
                        objLedgerBalance.StatusId = 110000008;
                        db.LedgerBalances.Add(objLedgerBalance);
                        db.SaveChanges();
                        crv = true;
                    }

                    if (((chk == 1) || (chk == 3) || (chk == 5) || (chk == 7)) && tax == false)  // Tax
                    {
                        //  dtNetLedger = objLedgerBalanceDB.GetNetBalance(int.Parse(objCRV.AgencyId.ToString()), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId));
                        //  dtClientLedger = objLedgerBalanceDB.GetClientBalance(int.Parse(objCRV.AgencyId.ToString()), int.Parse(dtDetail.Rows[0]["ClientId"].ToString()), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId));

                        objLedgerBalance.IsCRV = false;
                        objLedgerBalance.IsTax = true;
                        objLedgerBalance.BillAmount = 0;
                        objLedgerBalance.ReceiptAmount = -decimal.Parse(objCRV.WithHoldingTax.ToString()); //decimal.Parse(txtTax.Text.Trim());
                        objLedgerBalance.NetBalance = dtNetLedger.Value - (-decimal.Parse(objCRV.CRVAmount.ToString()));//decimal.Parse(txtAmount.Text.Trim());
                        objLedgerBalance.ClientBalance = dtClientLedger.Value - (-decimal.Parse(objCRV.CRVAmount.ToString())); //decimal.Parse(txtAmount.Text.Trim());

                        //objLedgerBalance.NetBalance = NetLedger - (-decimal.Parse(objCRV.WithHoldingTax.ToString())); //decimal.Parse(txtTax.Text.Trim());
                        //objLedgerBalance.ClientBalance = ClientLedger - (-decimal.Parse(objCRV.WithHoldingTax.ToString())); //decimal.Parse(txtTax.Text.Trim());
                        objLedgerBalance.StatusId = 110000009;
                        db.LedgerBalances.Add(objLedgerBalance);
                        db.SaveChanges();
                        tax = true;
                    }

                    if (((chk == 2) || (chk == 3) || (chk == 6) || (chk == 7)) && gst == false)  // GST
                    {
                        //dtNetLedger = objLedgerBalanceDB.GetNetBalance(int.Parse(objCRV.AgencyId.ToString()), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId));
                        //dtClientLedger = objLedgerBalanceDB.GetClientBalance(int.Parse(objCRV.AgencyId.ToString()), int.Parse(dtDetail.Rows[0]["ClientId"].ToString()), Convert.ToInt32(((CTS.User)(Session["UserObject"])).ChannelId));

                        objLedgerBalance.IsCRV = false;
                        objLedgerBalance.IsTax = true;
                        objLedgerBalance.BillAmount = 0;
                        objLedgerBalance.ReceiptAmount = -decimal.Parse(objCRV.GST.ToString()); //decimal.Parse(txtTax.Text.Trim());
                        objLedgerBalance.NetBalance = dtNetLedger.Value - (-decimal.Parse(objCRV.CRVAmount.ToString()));//decimal.Parse(txtAmount.Text.Trim());
                        objLedgerBalance.ClientBalance = dtClientLedger.Value - (-decimal.Parse(objCRV.CRVAmount.ToString())); //decimal.Parse(txtAmount.Text.Trim());
                        objLedgerBalance.StatusId = 110000011;
                        db.LedgerBalances.Add(objLedgerBalance);
                        db.SaveChanges();

                        // objLedgerBalanceDB.InsertLedgerBalance(objLedgerBalance);
                        gst = true;
                    }

                    crv = false;
                    tax = false;
                    gst = false;

                    if (((chk == 4) || (chk == 5) || (chk == 6) || (chk == 7)) && crv == false)  // CRV 
                    {
                        var dtcrv = db.usp_GetAllLedgerBalance_Digital(CRVId, 110000002).ToList(); ;
                        foreach (var crvid in dtcrv)
                        {
                            objLedgerBalance.ID = crvid.ID;
                            objLedgerBalance.StatusId = 110000008;
                            db.SaveChanges();
                        }

                    }

                    if (((chk == 1) || (chk == 3) || (chk == 5) || (chk == 7)) && tax == false)  // Tax
                    {
                        //DataTable dtcrv = objLedgerBalanceDB.GetAllLedgerBalance(null, null, null, CRVId, null, null, null, null, null, null, null, null, null, null, 3);
                        //if (dtcrv.Rows.Count == 1)
                        //{
                        var dtcrv = db.usp_GetAllLedgerBalance_Digital(CRVId, 110000003).ToList(); ;
                        foreach (var crvid in dtcrv)
                        {
                            objLedgerBalance.ID = crvid.ID;
                            objLedgerBalance.StatusId = 110000008;
                            db.SaveChanges();
                        }

                        //objLedgerBalance = objLedgerBalanceDB.GetLedgerBalance(Int64.Parse(dtcrv.Rows[0]["LedgerBalanceId"].ToString()));
                        //objLedgerBalance.StatusId = 9;
                        //objLedgerBalanceDB.UpdateLedgerBalance(objLedgerBalance);
                        //}
                    }

                    if (((chk == 2) || (chk == 3) || (chk == 6) || (chk == 7)) && gst == false)  // GST
                    {
                        var dtcrv = db.usp_GetAllLedgerBalance_Digital(CRVId, 110000003).ToList(); ;
                        foreach (var crvid in dtcrv)
                        {
                            objLedgerBalance.ID = crvid.ID;
                            objLedgerBalance.StatusId = 110000010;
                            db.SaveChanges();
                        }
                        //  DataTable dtcrv = objLedgerBalanceDB.GetAllLedgerBalance(null, null, null, CRVId, null, null, null, null, null, null, null, null, null, null, 10);
                        //if (dtcrv.Rows.Count == 1)
                        //{
                        //    objLedgerBalance = objLedgerBalanceDB.GetLedgerBalance(Int64.Parse(dtcrv.Rows[0]["LedgerBalanceId"].ToString()));
                        //    objLedgerBalance.StatusId = 11;
                        //    objLedgerBalanceDB.UpdateLedgerBalance(objLedgerBalance);
                        //}
                    }
                }
                //-----------------------------------------
                objCRV.Status = 110000006; //short.Parse(ddlStatus.SelectedItem.Value);
                objCRV.LastModifiedBy = ((UserInfo)Session["UserObject"]).ID;//  Convert.ToInt32(((CTS.User)(Session["UserObject"])).UserId);
                objCRV.LastModifiedDate = DateTime.Now;
                db.SaveChanges();
                //objCRVDB.ReversedCRV(objCRV);
                //-------------------------------------------






            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {


            CheckBox checkbox = (CheckBox)sender;
            GridViewRow row = (GridViewRow)checkbox.NamingContainer;



            //objCRV = new CTS.CRV();
            //objCRVDB = new CTS.CRVDB();

            //gvCRV.Columns[0].Visible = true;
            //gvCRV.DataBind();
            //Response.Write(row.Cells[0].Text.ToString());
            //Response.End(); 
            int crvid = int.Parse(row.Cells[0].Text.ToString());
            var objCRVcn = db.tblCRVs.Where(x => x.CRVId == crvid).SingleOrDefault();//  objCRVDB.GetCRV(int.Parse(row.Cells[0].Text.ToString()));
                                                                                     //gvCRV.Columns[0].Visible = false;
            if (objCRVcn != null)
            {
                if (checkbox.Checked == true)
                {
                    objCRVcn.IsChallanRecieved = true;
                }
                else
                {
                    objCRVcn.IsChallanRecieved = false;
                }
                db.SaveChanges();
            }
        }


        protected void gvCRV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "Highlight(this)");

                e.Row.Attributes.Add("onMouseOut", "UnHighlight(this)");

            }
        }

        protected void PopulateCity()
        {
            if (ddlHOCity.Items.Count <= 0)
            {
                //DataTable _dtCity;
                //objCity = new CTS.City();
                //objCityDB = new CTS.CityDB();

                //_dtCity = objCityDB.GetAllCity(null, null, null, true);
                var _dtCity = db.CityManagements.Where(x => x.IsActive == true).ToList();//   objCityDB.GetSpecificCities(null, null, null, true);

                ddlHOCity.DataSource = _dtCity;
                ddlHOCity.DataTextField = "CityName";
                ddlHOCity.DataValueField = "ID";
                ddlHOCity.DataBind();
                ListItem lst = new ListItem("--All--", "0");
                ddlHOCity.Items.Insert(0, lst);
                //ddlHOCity.Items.FindByText("Karachi").Selected = true;
            }
        }

        protected void ddlHOCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblMsg.Text = lblMsg.Text + " selected Index : " + ddlHOCity.SelectedIndex.ToString() + " ; "; 
            if (ddlHOCity.SelectedIndex != 0)
            {
                PopulateAgencyByCity();
            }
            if (ddlHOCity.SelectedIndex == 0)
            {

                PopulateAgency();
            }
        }

        protected void PopulateAgencyByCity()
        {
            //objAgencyDB = new CTS.AgencyDB();
            int cityid = Convert.ToInt32(ddlHOCity.SelectedValue);
            // DataTable _dtAgency;
            var _dtAgency = db.Agencies.Where(x => x.CityID == cityid).ToList().OrderBy(x => x.CityManagement.CityName);
            // _dtAgency.DefaultView.Sort = "Name";
            ddlAgency.DataSource = _dtAgency;
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataValueField = "ID";
            ddlAgency.DataBind();
            ListItem lst = new ListItem("--All--", "-1");
            ddlAgency.Items.Insert(0, lst);

        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            string crvNo;
            if (ddlCRV.SelectedItem.Value.ToString() == "0")
            {
                crvNo = txtCRVNo.Text;
            }
            else
            {
                crvNo = ddlCRV.SelectedItem.Text.ToString();
            }

            string chqNo = txtChqNo.Text;

            Int32 status = Int32.Parse(ddlStatus.SelectedItem.Value);


            string FromDate = "";
            try
            {
                Convert.ToDateTime(FromDatePicker.Text);
            }
            catch (Exception)
            {
                FromDate = null;
            }
            string TODate = "";
            try
            {
                Convert.ToDateTime(ToDatePicker.Text);
            }
            catch (Exception)
            {

                TODate = null;
            }

            int city = 0;
            int agency = -1;
            int client = -1;

            if (ddlHOCity.SelectedItem != null)
                city = Int32.Parse(ddlHOCity.SelectedItem.Value);

            if (ddlAgency.SelectedItem != null)
                agency = Int32.Parse(ddlAgency.SelectedItem.Value);

            if (ddlClient.SelectedItem != null)
                client = Int32.Parse(ddlClient.SelectedItem.Value);

            Dictionary<string, string> dataForReport = new Dictionary<string, string>(8);
            dataForReport.Add("FromDate", FromDate);
            dataForReport.Add("ToDate", TODate);
            dataForReport.Add("City", city.ToString());
            dataForReport.Add("Agency", agency.ToString());
            dataForReport.Add("Client", client.ToString());
            dataForReport.Add("CRV#", crvNo);
            dataForReport.Add("Cheque", chqNo);
            dataForReport.Add("Status", status.ToString());
            //if (chkAgencyByName.Checked == false)
            //{
            //    dataForReport.Add("AgencyName", "-1");
            //}
            //else
            //{

            dataForReport.Add("AgencyName", ddlAgency.SelectedItem.Text);
            //}

            Session["CRVViewReportData"] = dataForReport;
            Response.Redirect("CRVViewReport.aspx");

        }
        protected void chkAgencyByName_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // if (chkAgencyByName.Checked != false)
                {

                    if (ddlHOCity.SelectedIndex != 0)
                    {
                        PopulateAgencyByCity();
                    }
                    else//if(ddlHOCity.SelectedIndex != -2)
                    {

                        PopulateAgency();
                    }
                    //PopulateClient(0);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}



