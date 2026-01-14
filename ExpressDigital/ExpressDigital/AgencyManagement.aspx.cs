using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class AgencyManagement : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                try
                {
                    var r = db.GroupAgencies.Where(x => x.Active == true && x.ID != 110000001 && x.Suspended == false).OrderBy(x => x.GroupName).ToList();
                    ddlAgency.DataValueField = "ID";
                    ddlAgency.DataTextField = "GroupName";
                    ddlAgency.DataSource = r;
                    ddlAgency.DataBind();



                    var s = db.CityManagements.OrderBy(x => x.CityName).ToList();
                    ddlCity.DataValueField = "ID";
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataSource = s;
                    ddlCity.DataBind();



                    if (Request.QueryString.Count > 0)
                    {
                        int GID = Convert.ToInt32(Request.QueryString[0]);
                        ddlAgency.SelectedValue = GID.ToString();
                        BindGrid(GID);
                        ddlAgency_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        BindGrid();
                        ddlAgency_SelectedIndexChanged(null, null);
                    }
                }
                catch (Exception ex)
                {

                }

            }

        }

        protected void ImgSuspendButton_Click(object sender, EventArgs e)
        {
            Session["imgPath"] = null;
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            HiddenField hd = (HiddenField)myRow.FindControl("hdsuspend");
            bool isSuspend = Convert.ToBoolean(hd.Value);

            ViewState["RecordID"] = ID;
            var url = String.Format("SuspendResotreAgency.aspx?id={0}&Issuspend={1}", ID, isSuspend);
            Response.Redirect(url);

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int gcid = Convert.ToInt32(ddlAgency.SelectedValue);
            var gcity = db.GroupAgencies.Where(x => x.ID == gcid).SingleOrDefault().ID;
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        var s = db.usp_IDctr("Agency").SingleOrDefault();

                        int ID = s.Value;
                        Agency obj = new Agency();
                        obj.ID = ID;
                        obj.GroupID = Convert.ToInt32(ddlAgency.SelectedValue);
                        obj.AgencyName = txtAgency.Text;
                        obj.ContactPerson = txtContactPerson.Text;
                        obj.Designation = txtDesignation.Text;
                        try
                        {
                            obj.CommissionDate = Helper.SetDateFormat(txtCommissionDate.Text);
                        }
                        catch (Exception)
                        {
                            obj.CommissionDate = DateTime.Now;
                        }

                        obj.PhoneNumber = txtPhone.Text;
                        obj.CellNumber = txtCell.Text;
                        obj.FaxNumber = txtFax.Text;
                        obj.Email = txtEmail.Text;
                        obj.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                        obj.Createdon = DateTime.Now;
                        obj.Address = txtAddress.Text;
                        obj.Active = chkActive.Checked;
                        obj.Remarks = txtRemarks.Text;
                        obj.Suspended = false;
                        obj.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                        obj.AgencyClinet = (string)ViewState["agencyclient"];
                        obj.HeadOfficeCityId = gcid;

                        db.Agencies.Add(obj);
                        db.SaveChanges();
                        BindGrid();

                        btnCancel_Click(null, null);
                        scope.Complete();
                        lblmessage.Text = "Agency Created Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);

                    }
                }
            }
            else
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        int ID = Convert.ToInt32(ViewState["RecordID"]);
                        var obj = db.Agencies.Where(x => x.ID == ID).SingleOrDefault();
                        obj.ID = ID;
                        obj.GroupID = Convert.ToInt32(ddlAgency.SelectedValue);
                        obj.AgencyName = txtAgency.Text;
                        obj.ContactPerson = txtContactPerson.Text;
                        obj.Designation = txtDesignation.Text;
                        obj.CommissionDate = Helper.SetDateFormat(txtCommissionDate.Text);
                        obj.PhoneNumber = txtPhone.Text;
                        obj.CellNumber = txtCell.Text;
                        obj.FaxNumber = txtFax.Text;
                        obj.Email = txtEmail.Text;
                        obj.ModifyBy = ((UserInfo)Session["UserObject"]).ID; 
                        obj.ModifyOn = DateTime.Now;
                        obj.Address = txtAddress.Text;
                        obj.Active = chkActive.Checked;
                        obj.Remarks = txtRemarks.Text;
                        obj.Suspended = false;
                        obj.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                        obj.AgencyClinet = (string)ViewState["agencyclient"];
                        obj.HeadOfficeCityId = gcid;
                        db.SaveChanges();
                        BindGrid();
                        btnCancel_Click(null, null);
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }

        }
        private void BindGrid(int GUID)
        {
            var g = (from u in db.Agencies.Where(x => x.GroupID == GUID)
                     select new
                     {
                         u.ID,
                         u.AgencyName,
                         u.ContactPerson,
                         u.Designation,
                         u.PhoneNumber,
                         u.CellNumber,
                         u.FaxNumber,
                         u.Email,
                         u.CityManagement.CityName,
                         u.Active,
                         u.AgencyClinet,
                         u.Suspended,
                         u.GroupID,
                         u.GroupAgency.GroupName
                     }).Where(x => x.ID != 110000001).OrderBy(x => x.AgencyName).ToList();
            DataTable dt = Helper.ToDataTable(g);
            ViewState["dt"] = dt;
            gv.DataSource = dt;
            gv.DataBind();

        }
        private void BindGrid()
        {
            var g = (from u in db.Agencies
                     select new
                     {
                         u.ID,
                         u.AgencyName,
                         u.ContactPerson,
                         u.Designation,
                         u.PhoneNumber,
                         u.CellNumber,
                         u.FaxNumber,
                         u.Email,
                         u.CityManagement.CityName,
                         u.Active,
                         u.AgencyClinet,
                         u.Suspended,
                         u.GroupID,
                         u.GroupAgency.GroupName
                     }).Where(x => x.ID != 110000001).OrderBy(x => x.AgencyName).ToList();
            DataTable dt = Helper.ToDataTable(g);
            ViewState["dt"] = dt;
            gv.DataSource = dt;
            gv.DataBind();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            ddlAgency.SelectedIndex = 0;
            ddlAgency_SelectedIndexChanged(null, null);
            txtAgency.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtDesignation.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtCell.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCommissionDate.Text = string.Empty;
            txtAddress.Text = string.Empty;
            ddlCity.SelectedIndex = 0;
            txtRemarks.Text = string.Empty;
            chkActive.Checked = false;
            RdoSuspend.Checked = false;
            btnSave.Text = "Save";
        }

        protected void ChildAgencyButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            Response.Redirect("ClientManagement.aspx?" + ID, true);

        }

        private void ClearData()
        {


        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.Agencies.Where(x => x.ID == ID).SingleOrDefault();
            ddlAgency.SelectedValue = obj.GroupID.ToString();
            ddlAgency.SelectedValue = obj.GroupID.ToString();
            txtAgency.Text = obj.AgencyName;
            txtContactPerson.Text = obj.ContactPerson;
            txtDesignation.Text = obj.Designation;
            txtPhone.Text = obj.PhoneNumber;
            txtCell.Text = obj.CellNumber;
            txtFax.Text = obj.FaxNumber;
            txtEmail.Text = obj.Email;
            txtCommissionDate.Text = Helper.SetDateFormatString(obj.CommissionDate.ToString());
            txtAddress.Text = obj.Address;
            ddlCity.SelectedValue = obj.CityID.ToString();
            txtRemarks.Text = obj.Remarks;
            chkActive.Checked = obj.Active;
            RdoSuspend.Checked = obj.Suspended;
            ViewState["agencyclient"] = obj.AgencyClinet;


            btnSave.Text = "Update";
        }


        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;

        }
        [WebMethod]
        public static string OnSubmit(string id)
        {
            string mess = "";
            DbDigitalEntities db = new DbDigitalEntities();
            int ID = Convert.ToInt32(id);
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var info = db.Agencies.Find(ID);
                    if (info != null)
                    {
                        db.Agencies.Remove(info);
                        db.SaveChanges();
                        LogManagers.RecordID = ID;
                        LogManagers.ActionOnForm = "Agency";
                        LogManagers.ActionBy = ((UserInfo)HttpContext.Current.Session["UserObject"]).ID;
                        LogManagers.ActionOn = DateTime.Now;
                        LogManagers.ActionTaken = "Delete";
                        LogManagers.SetLog(db);
                        scope.Complete();
                        mess = "Ok";
                    }
                }
                catch (Exception ex)
                {
                    mess = ExceptionHandler.GetException(ex);
                }

            }
            return mess;
        }

        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(ddlAgency.SelectedValue);
            var obj = db.GroupAgencies.Where(x => x.ID == ID && x.ID != 110000001).SingleOrDefault();
            ViewState["agencyclient"] = obj.AgecnyClient;


            lblClientAgency.Text = "(" + " " + obj.AgecnyClient + " " + ")";
            txtAgency.Text = obj.GroupName;
            txtContactPerson.Text = obj.OwenrName;
            txtEmail.Text = obj.Email;
            txtPhone.Text = obj.PhoneNumber;
            txtCell.Text = obj.CellNumber;
            txtAddress.Text = obj.Address;
            ddlCity.SelectedValue = obj.CityID.ToString();


        }


        protected void chkActive_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    HiddenField hd = (HiddenField)e.Row.FindControl("hdsuspend");
                    bool isSuspend = Convert.ToBoolean(hd.Value);
                    ImageButton img = (ImageButton)e.Row.FindControl("ImgSuspendButton");
                    if (isSuspend == true)
                    {
                        img.ImageUrl = "~/Content/Images/suspend.png";
                        img.ToolTip = "Click to Restore";
                    }
                    else
                    {
                        img.ImageUrl = "~/Content/Images/Restore.png";
                        img.ToolTip = "Click to Suspend";

                    }

                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                var g = (from u in db.Agencies.Where(x => x.Active == true && x.AgencyName.Contains(txtSearch.Text))
                         select new
                         {
                             u.ID,
                             u.AgencyName,
                             u.ContactPerson,
                             u.Designation,
                             u.PhoneNumber,
                             u.CellNumber,
                             u.FaxNumber,
                             u.Email,
                             u.CityManagement.CityName,
                             u.Active,
                             u.AgencyClinet,
                             u.Suspended,
                             u.GroupID,
                             u.GroupAgency.GroupName
                         }).Where(x => x.ID != 110000001).OrderBy(x => x.AgencyName).ToList();
                DataTable dt = Helper.ToDataTable(g);
                ViewState["dt"] = dt;
                gv.DataSource = dt;
                gv.DataBind();
            }

        }
        //protected void ChildAgencyButton_Click(object sender, EventArgs e)
        //{

        //}
    }
}