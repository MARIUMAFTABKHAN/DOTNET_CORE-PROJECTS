using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Mapping;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;

namespace CDSN
{

    public partial class CurrentActivity : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlmtaskuser.Attributes.Add("onchange", "showAddressByamit()");
                hduserid.Value = Session["UserID"].ToString();
                hdisadmin.Value = Session["IsAdmin"].ToString();

                var ad = db.tblUsers.Where(x => x.IsActive == true).ToList();

                ddlActivitySentFrom.DataValueField = "UserID";
                ddlActivitySentFrom.DataTextField = "FirstName";
                ddlActivitySentFrom.DataSource = ad;
                ddlActivitySentFrom.DataBind();

                ddlActivitySentto.DataValueField = "UserID";
                ddlActivitySentto.DataTextField = "FirstName";
                ddlActivitySentto.DataSource = ad;
                ddlActivitySentto.DataBind();

                ddlmtaskuser.DataValueField = "UserID";
                ddlmtaskuser.DataTextField = "FirstName";
                ddlmtaskuser.DataSource = ad;
                ddlmtaskuser.DataBind();

                ddlmSentto.DataValueField = "UserID";
                ddlmSentto.DataTextField = "FirstName";
                ddlmSentto.DataSource = ad;
                ddlmSentto.DataBind();

                fillTasksUsers();
                txtmwef.Text = DateTime.Now.ToString("dd-MM-yyyy");
                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

                txtwef.Text = firstDayOfMonth.ToString("dd-MM-yyyy");
                txtwet.Text = lastDayOfMonth.ToString("dd-MM-yyyy");
                BindControls();
                chklist.RepeatDirection = RepeatDirection.Horizontal;
                checkAll.Checked = false;
                pnlactivity.Visible = false;
                try
                {
                    GetRecords();
                }
                catch (Exception)
                {

                    throw;
                }


            }
        }

        #region new activity
        private void fillTasksUsers()
        {
            try
            {
                int uid = Convert.ToInt32(Session["UserID"].ToString());
                
                var obj = db.tblUsers.Where(x => x.IsActive == true && x.UserId == uid).OrderBy(x => x.UserName).ToList();
                ddlmtaskuser.DataSource = obj;
                ddlmtaskuser.DataTextField = "FirstName";
                ddlmtaskuser.DataValueField = "UserId";
                ddlmtaskuser.DataBind();
                
                ddlmuser_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                lblmmessage.Text = ex.Message; lblmmessage.Visible = true;
            }

        }
        private void fillOperator()
        {
            Int32 uid = Convert.ToInt32(Convert.ToInt32(ddlmtaskuser.SelectedValue));
            try
            {
                int UserId = Convert.ToInt32(ddlmtaskuser.SelectedValue);
                
                var ds = db.usp_GetHeadEndsByUserId(UserId).OrderBy(x => x.Name).ToList(); ;
                
                ddlmoperator.DataSource = ds;
                ddlmoperator.DataTextField = "Name";
                ddlmoperator.DataValueField = "Id";

                ddlmoperator.DataBind();
                ddlmOperator_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                lblmmessage.Text = ex.Message; lblmmessage.Visible = true;

            }
        }


        #endregion
        private void GetRecords()
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

            string strchk = "";
            try
            {
                foreach (ListItem li in chklist.Items)
                {
                    if (li.Selected)
                    {
                        strchk += ";" + li.Value.ToString();
                    }
                }
                string str = strchk.Substring(1);
                //DataSet ds = new DataSet();
                //ds = Helper.GetContactReportAdmin(db, Convert.ToInt32(Session["UserId"]), str);
                DateTime stdate, endate;
                try
                {
                    stdate = Helper.SetDateFormat2(txtwef.Text);
                    endate = Helper.SetDateFormat2(txtwet.Text);
                }
                catch (Exception)
                {
                    firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

                    stdate = firstDayOfMonth;
                    endate = lastDayOfMonth;
                }

                var x = db.N3SP_UserArea(Convert.ToInt32(Session["UserId"]), stdate, endate, str).ToList();

                /// var x = db.sp_GetContactDetailsAdminCDSN1N(Convert.ToInt32(Session["UserId"]), stdate, endate).ToList();
                DataTable ds = Helper.ToDataTable(x);

                ViewState["dt"] = ds;
                gvContact.DataSource = ds;
                gvContact.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        private void BindControls()
        {
            FillCheckBox();
        }
        private void FillCheckBox()
        {
            var ds = db.usp_GetTerritoryList(Convert.ToInt32(Session["UserId"]));
            chklist.DataSource = ds;
            chklist.DataValueField = "Id";
            chklist.DataTextField = "ShortNames";
            chklist.DataBind();
        }
        protected void imgViewed_Click(object sender, ImageClickEventArgs e)
        {

            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            gvrow.ControlStyle.ForeColor = System.Drawing.Color.Black;
            HiddenField hdRecordID = (HiddenField)gvrow.FindControl("hdRecordID");
            //  hdrecord.Value = hdRecordID.Value;
            lblHeadEndName.Text = gvrow.Cells[5].Text;
            txtMessage.Text = gvrow.Cells[0].Text;
            // Helper.UpdateContact(db, Convert.ToInt32(hdrecord.Value));
        }




        private void SetEventDetails(int opid)
        {


            //obj.isViewed= true;
            //db.SaveChanges();
            pnlactivity.Visible = true;
            var ds = db.sp_GetContactDetailsByOperatorIDCDSN1N2(opid).OrderByDescending(x => x.ID).ToList();

            gvActivityDetails.DataSource = ds;
            gvActivityDetails.DataBind();

        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int RecId = Helper.GetCounter(db, "Contact");
                //int TerritoryID = Helper.GetTerritory(db, Convert.ToInt32(Session["UserID"]));
                string txt = txtReply.Text;
                bool isAdmin = Convert.ToBoolean(Session["IsAdmin"]);
                tblContact obj = new tblContact();
                obj.ID = RecId;
                obj.Messagetxt = txt.Replace("?", "");
                obj.SentBy = Convert.ToInt32(Session["UserID"]);
                obj.TerritoryID = Convert.ToInt32(lblterritoryid.Text);
                obj.OperatorID = Convert.ToInt32(lbloperatorid.Text);
                obj.IsAdminContact = isAdmin;
                obj.isViewed = false;
                obj.IsResponded = false;
                db.tblContacts.Add(obj);

                FillGrid(true, db);
                // SetEventDetails(Convert.ToInt32(hdoperator.Value));
                db.SaveChanges();
            }
            catch (Exception)
            {

            }
        }
        private void FillGrid(CDSEntities db)
        {
            string strchk = "";
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

            DataTable dt = new DataTable();
            DateTime stdate, endate;
            try
            {
                stdate = Helper.SetDateFormat2(txtwef.Text);
                endate = Helper.SetDateFormat2(txtwet.Text);
            }
            catch (Exception)
            {
                firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

                stdate = firstDayOfMonth;
                endate = lastDayOfMonth;
            }



            foreach (ListItem li in chklist.Items)
            {
                if (li.Selected == true)
                {
                    strchk += ";" + li.Value.ToString();
                }
            }
            try
            {
                string str = strchk.Substring(1);
                var x = db.GetNewActivityListCDSN12(Convert.ToInt32(Session["UserId"]), str, stdate, endate).ToList();
                if (x.Count > 0)
                {
                    dt = Helper.ToDataTable(x);
                    ViewState["dt"] = dt;
                    gvContact.DataSource = dt;
                    gvContact.DataBind();
                }
                else
                {
                    ViewState["dt"] = null;
                    gvContact.DataSource = null;
                    gvContact.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void FillGrid(bool result, CDSEntities db)
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

            DataTable dt = new DataTable();
            DateTime stdate, endate;
            try
            {
                stdate = Helper.SetDateFormat2(txtwef.Text);
                endate = Helper.SetDateFormat2(txtwet.Text);
            }
            catch (Exception)
            {
                firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

                stdate = firstDayOfMonth;
                endate = lastDayOfMonth;
            }
            if (result == true)
            {
                string strchk = "";
                try
                {
                    foreach (ListItem li in chklist.Items)
                    {
                        if (li.Selected == true)
                        {
                            strchk += ";" + li.Value.ToString();
                        }
                    }
                    string str = strchk.Substring(1);
                    //ds = Helper.GetContactReportAdmin(db, Convert.ToInt32(Session["UserId"]), str);
                    var x = db.GetNewActivityListCDSN12(Convert.ToInt32(Session["UserId"]), str, stdate, endate).ToList();


                    dt = Helper.ToDataTable(x);
                    ViewState["dt"] = dt;
                    gvContact.DataSource = dt;
                    gvContact.DataBind();
                    //ds = Helper .GetContactActivityDetatilsbyOperatorID (
                }
                catch (Exception)
                {

                    //throw;
                }
            }
            else
            {
                ViewState["dt"] = null;
                gvContact.DataSource = null;
                gvContact.DataBind();
            }

        }
        protected void chklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Totalchk = chklist.Items.Cast<ListItem>().Count();
            int selectedCount = chklist.Items.Cast<ListItem>().Count(li => li.Selected);
            if (Totalchk == selectedCount)
                checkAll.Checked = true;
            else
                checkAll.Checked = false;



            //int selectedCount = chklist.Items.Cast<ListItem>().Count(li => li.Selected);
        }


        protected void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAll.Checked == true)
            {
                foreach (ListItem li in chklist.Items)
                {
                    li.Selected = true;
                }
                //  FillGrid(db);
            }
            else
            {
                foreach (ListItem li in chklist.Items)
                {
                    li.Selected = false;
                }
                //ViewState["dt"] = null;
                //gvContact.DataSource = null;
                //gvContact.DataBind();
            }

        }


        protected void BtnCheckbox_Click(object sender, EventArgs e)
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

            string strchk = "";
            try
            {
                foreach (ListItem li in chklist.Items)
                    if (li.Selected)
                        strchk += ";" + li.Value.ToString();

                string str = strchk.Substring(1);
                DateTime stdate, endate;
                try
                {
                    stdate = Helper.SetDateFormat2(txtwef.Text);
                    endate = Helper.SetDateFormat2(txtwet.Text);
                }
                catch (Exception)
                {
                    firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

                    stdate = firstDayOfMonth;
                    endate = lastDayOfMonth;
                }
                
            }
            catch (Exception)
            {


            }

        }
        protected void gvActivityDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Int32 operatopid = Convert.ToInt32(gvActivityDetails.DataKeys[e.Row.RowIndex].Values[0]);
                Image img = (Image)e.Row.FindControl("imgbtn");
                
                int val = GetActivityStatus(operatopid);
                switch (val)
                {
                    case 3:
                        e.Row.Cells[0].ForeColor = Color.White;
                        e.Row.Cells[0].BackColor = Color.Red;
                        break;
                    case 2:
                        e.Row.Cells[0].ForeColor = Color.White;
                        e.Row.Cells[0].BackColor = Color.DarkGreen;
                        break;
                    case 1:
                        e.Row.Cells[0].ForeColor = Color.White;
                        e.Row.Cells[0].BackColor = Color.Blue;
                        break;
                }


            }

        }


        protected void gvContact_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContact.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["dt"];
            gvContact.DataSource = dt;
            gvContact.DataBind();
        }

        protected void ShowActivityDetails_Click(object sender, ImageClickEventArgs e)
        {
            txtReply.Text = "";
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            HiddenField hdOperatorID = (HiddenField)gvrow.FindControl("hdOperatorID");
            HiddenField hdTerritoryID = (HiddenField)gvrow.FindControl("hdTerritoryID");
         
            lblHeadEndName.Text = gvrow.Cells[0].Text;
            lblActivityDetailHeader.Text = gvrow.Cells[2].Text;
            lblactivityid.Text = hdOperatorID.Value.ToString();

            pnlactivity.Visible = true;

        }

        protected void imgbtn_Command(object sender, CommandEventArgs e)
        {

        }

        protected void ShowActivityDetails_Command(object sender, CommandEventArgs e)
        {

        }

        protected void btnactivity_Click(object sender, EventArgs e)
        {
            GetRecords();
        }
        private ArrayList GetStatus(Int32 operatopid)
        {
            var cont = db.tblContacts.Where(x => x.OperatorID == operatopid && x.isClosed == false).ToList();
            Int32 rv = cont.Where(x => x.IsResponded == true && x.isViewed == true).Count();
            Int32 v = cont.Where(x => x.IsResponded == false && x.isViewed == true).Count();
            Int32 nv = cont.Where(x => x.IsResponded == false && x.isViewed == false).Count();

            ArrayList AL = new ArrayList();

            Int32 sts = cont.Count;

            AL.Add(rv);
            AL.Add(v);
            AL.Add(nv);
            AL.Add(sts);
            return AL;
        }

        private int GetActivityStatus(Int32 operatopid)
        {
            int sts = 0;
            var cont = db.tblContacts.Where(x => x.OperatorID == operatopid && x.isClosed == false).ToList();
            Int32 rv = cont.Where(x => x.IsResponded == true && x.isViewed == true).Count();
            Int32 v = cont.Where(x => x.IsResponded == false && x.isViewed == true).Count();
            Int32 nv = cont.Where(x => x.IsResponded == false && x.isViewed == false).Count();

            try
            {
                if (nv > 0)
                    sts = 3;
                else if (v > 0)
                    sts = 2;
                else if (rv > 0)
                    sts = 1;
            }
            catch (Exception)
            {
                return sts;

            }
            return sts;
        }
        protected String LabelProperty
        {
            get
            {
                return hdactstatus.Value;
            }
            set
            {
                hdactstatus.Value = value;
            }
        }

        protected void gvContact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Int32 operatopid = Convert.ToInt32(gvContact.DataKeys[e.Row.RowIndex].Values[0]);

                    ArrayList sts = GetStatus(operatopid);


                    // LabelProperty  = sts.ToString();

                    //Label lbltotal = (Label)e.Row.FindControl("lbltotal");
                    //lbltotal.ForeColor = Color.Red;
                    //Label lblv = (Label)e.Row.FindControl("lblv");
                    //Label lblrv = (Label)e.Row.FindControl("lblrv");
                    //Label lblnv = (Label)e.Row.FindControl("lblnv");

                    //  bool priority = Convert.ToBoolean(  DataBinder.Eval(e.Row.DataItem, "IsResponded"));
                    //  bool isViewed = Convert.ToBoolean( DataBinder.Eval(e.Row.DataItem, "isViewed"));
                    //lbltotal.Text = sts[3].ToString();

                    //lblv.Text =  sts[1].ToString();
                    //lblrv.Text = sts[0].ToString();
                    //lblnv.Text = sts[2].ToString();

                }

                catch (Exception)
                {

                    throw;
                }
            }
        }

        protected void BtnmCancel_Click(object sender, EventArgs e)
        {

        }


        protected void ddlmuser_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillOperator();
        }

        protected void ddlmOperator_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvRecords_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvRecords_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void BtnmSave_Click(object sender, EventArgs e)
        {
            lblmmessage.Text = string.Empty;
            if (txtmtask.Text.Trim().Length == 0)
            {
                lblmmessage.Text = "Please enter task details";
                return;
            }
        }

        protected void txtwet_TextChanged(object sender, EventArgs e)
        {

        }

        protected void chklist_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod]
        public static string GetOperatorAllActivities(int RecordID)
        {
            string mess = "";
            try
            {
                using (CDSEntities db = new CDSEntities())
                {
                    try
                    {
                        int id = Convert.ToInt32(RecordID.ToString());

                        // int TerriroryId = Convert.ToInt32(ddlTerritory.SelectedValue);

                        var xx = db.sp_GetContactDetailsByOperatorIDCDSN1N2(id).OrderByDescending(x => x.MessageDate).ThenBy(x => x.OperatorName).ToList(); ;
                        JavaScriptSerializer jscript = new JavaScriptSerializer();
                        return jscript.Serialize(xx);



                    }
                    catch (Exception ex)
                    {

                        mess = ex.Message;
                    }
                    return mess;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        protected void btngo_Click(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(hdoptrvalue.Value);
            int id = Convert.ToInt32(Session["UserID"]) ;

            SetEventDetails(id);
        }
    }
}
