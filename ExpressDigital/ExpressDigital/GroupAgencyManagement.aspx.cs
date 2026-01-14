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
    public partial class GroupAgencyManagement : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                var s = db.CityManagements.OrderBy(x => x.CityName).ToList();
                ddlCity.DataValueField = "ID";
                ddlCity.DataTextField = "CityName";
                ddlCity.DataSource = s;
                ddlCity.DataBind();

                var cm = db.CurrencyModes.ToList();
                ddlCurrencymode.DataValueField = "ID";
                ddlCurrencymode.DataTextField = "BillingCurrency";
                ddlCurrencymode.DataSource = cm;
                ddlCurrencymode.DataBind();


                BindGrid();

                ddlComission_SelectedIndexChanged(null, null);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        var s = db.usp_IDctr("GroupAgency").SingleOrDefault();

                        int ID = s.Value;
                        GroupAgency obj = new GroupAgency();
                        obj.ID = ID;
                        obj.GroupName = txtName.Text;
                        obj.GSTNumber = txtGST.Text;
                        obj.OwenrName = txtOwner.Text;
                        obj.Email = txtEmail.Text;
                        obj.PhoneNumber = txtPhone.Text;
                        obj.FaxNumber = txtFax.Text;
                        obj.CellNumber = txtCell.Text;
                        obj.Address = txtAddress.Text;
                        obj.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                        obj.Remarks = txtRemarks.Text;
                        obj.Active = ChkIsActive.Checked;
                        obj.CurrencyModeID = Convert.ToInt32(ddlCurrencymode.SelectedValue);
                        if (ddlComission.SelectedValue == "Yes")
                            obj.Commission = true;
                        else
                            obj.Commission = false;
                        obj.CommPercentage = Convert.ToDecimal(txtCommission.Text);
                        obj.AgecnyClient = Convert.ToString(ddlAgencyClient.SelectedValue);
                        obj.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                        obj.CreatedOn = DateTime.Now;
                        obj.Suspended = false;
                        obj.CreditLimit = Convert.ToInt32(txtCrLimit.Text);
                        obj.AgencyStatus = ddlLocalInternational.SelectedValue;
                        obj.NTNNumber = txtntn.Text;
                        db.GroupAgencies.Add(obj);
                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        //btnCancel_Click(null, null);
                        lblmessage.Text = "Group Agency Created Successfully";
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
                        var obj = db.GroupAgencies.Where(x => x.ID == ID).SingleOrDefault();
                        obj.ID = ID;
                        obj.GroupName = txtName.Text;
                        obj.GSTNumber = txtGST.Text;
                        obj.OwenrName = txtOwner.Text;
                        obj.Email = txtEmail.Text;
                        obj.PhoneNumber = txtPhone.Text;
                        obj.FaxNumber = txtFax.Text;
                        obj.CellNumber = txtCell.Text;
                        obj.Address = txtAddress.Text;
                        obj.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                        obj.Remarks = txtRemarks.Text;
                        obj.Active = ChkIsActive.Checked;
                        obj.CurrencyModeID = Convert.ToInt32(ddlCurrencymode.SelectedValue);
                        obj.AgencyStatus = ddlLocalInternational.SelectedValue;
                        if (ddlComission.SelectedValue == "Yes")
                            obj.Commission = true;
                        else
                            obj.Commission = false;
                        obj.CommPercentage = Convert.ToDecimal(txtCommission.Text);
                        obj.AgecnyClient = Convert.ToString(ddlAgencyClient.SelectedValue);
                        obj.ModifyBy = ((UserInfo)Session["UserObject"]).ID;
                        obj.ModifyOn = DateTime.Now;
                        obj.CreditLimit = Convert.ToInt32(txtCrLimit.Text);
                        obj.NTNNumber = txtntn.Text;
                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        lblmessage.Text = "Group Agency Updated Successfully";
                        // btnCancel_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }

        }

        private void BindGrid()
        {
            var g = (from u in db.GroupAgencies
                     select new
                     {
                         u.ID,
                         u.GroupName,
                         u.OwenrName,
                         u.PhoneNumber,
                         u.CellNumber,
                         u.FaxNumber,
                         u.Email,
                         u.GSTNumber,
                         u.CityManagement.CityName,
                         u.Active,
                         u.Suspended
                     }).Where(x => x.ID != 110000001).OrderBy(x => x.GroupName).ToList();
            DataTable dt = Helper.ToDataTable(g);
            DataView dv = dt.DefaultView;
            dv.Sort = "GroupName asc";
            DataTable sortedDT = dv.ToTable();
            ViewState["dt"] = sortedDT;
            gv.DataSource = sortedDT;
            gv.DataBind();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            txtOwner.Text = string.Empty;
            txtGST.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtCell.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtAddress.Text = string.Empty;
            ddlCity.SelectedIndex = 0;
            txtRemarks.Text = string.Empty;
            ChkIsActive.Checked = true;
            btnSave.Text = "Save";
            lblmessage.Text = string.Empty;
            txtName.Text = string.Empty;
            txtCommission.Text = "0.00";
            ddlAgencyClient.SelectedIndex = 0;
            ddlCurrencymode.SelectedIndex = 0;
            ddlComission.SelectedIndex = 0;
            txtntn.Text = string.Empty;
            txtCrLimit.Text = "0";
        }

        protected void ChildAgencyButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            Response.Redirect("AgencyManagement.aspx?" + ID, true);

        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.GroupAgencies.Where(x => x.ID == ID).SingleOrDefault();

            txtName.Text = obj.GroupName;
            txtOwner.Text = obj.OwenrName;
            txtGST.Text = obj.GSTNumber;
            txtEmail.Text = obj.Email;
            txtPhone.Text = obj.PhoneNumber;
            txtCell.Text = obj.CellNumber;
            txtFax.Text = obj.FaxNumber;
            txtAddress.Text = obj.Address;
            ddlLocalInternational.SelectedValue = obj.AgencyStatus.ToString();
            ddlCity.SelectedValue = obj.CityID.ToString();
            txtRemarks.Text = obj.Remarks;
            ChkIsActive.Checked = Convert.ToBoolean(obj.Active);
            ddlCurrencymode.SelectedValue = obj.CurrencyModeID.ToString();
            if (obj.Commission == true)
                ddlComission.SelectedIndex = 0;
            else
                ddlComission.SelectedIndex = 1;
            txtCommission.Text = obj.CommPercentage.ToString();
            ddlAgencyClient.SelectedValue = obj.AgecnyClient.ToString();
            chkSuspended.Checked = Convert.ToBoolean(obj.Suspended);
            txtCrLimit.Text = obj.CreditLimit.ToString();
            txtntn.Text = obj.NTNNumber;
            btnSave.Text = "Update";
        }
        protected void DelButton_Click(object sender, EventArgs e)
        {
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
            var url = String.Format("SuspendResotreGroupAgency.aspx?id={0}&Issuspend={1}", ID, isSuspend);
            Response.Redirect(url);


        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            DataView dv = dt.DefaultView;
            dv.Sort = "GroupName asc";
            DataTable sortedDT = dv.ToTable();
            gv.DataSource = sortedDT;
            gv.DataBind();

            //gv.DataSource = dt;
            //gv.DataBind();
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
                    var info = db.GroupAgencies.Find(ID);
                    if (info != null)
                    {
                        db.GroupAgencies.Remove(info);
                        db.SaveChanges();
                        LogManagers.RecordID = ID;
                        LogManagers.ActionOnForm = "GroupAgency";
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

        protected void ddlAgencyClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAgencyClient.SelectedIndex == 1)
            {
                ddlComission.SelectedIndex = 1;
                txtCommission.Text = "0.00";
            }
            else
            {
                ddlComission.SelectedIndex = 0;
                txtCommission.Text = System.Configuration.ConfigurationManager.AppSettings["agcvalue"];
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HiddenField hd = (HiddenField)e.Row.FindControl("hdsuspend");
                bool isSuspend = Convert.ToBoolean(hd.Value);
                ImageButton img = (ImageButton)e.Row.FindControl("ImgSuspendButton");
                if (isSuspend == true)
                {
                    img.ImageUrl = "~/Content/Images/suspend.png";
                    img.ToolTip = "Click to Resotre";
                }
                else
                {
                    img.ImageUrl = "~/Content/Images/Restore.png";
                    img.ToolTip = "Click to Suspend";
                }

            }
        }

        protected void ddlComission_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlComission.SelectedIndex == 1)
            //	txtCommission.Text = "0.00";
            //         else
            //             txtCommission.Text = System.Configuration.ConfigurationManager.AppSettings["agcvalue"];
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                var g = (from u in db.GroupAgencies.Where(x => x.Active == true && x.GroupName.Contains(txtSearch.Text))
                         select new
                         {
                             u.ID,
                             u.GroupName,
                             u.OwenrName,
                             u.PhoneNumber,
                             u.CellNumber,
                             u.FaxNumber,
                             u.Email,
                             u.GSTNumber,
                             u.CityManagement.CityName,
                             u.Active,
                             u.Suspended
                         }).Where(x => x.ID != 110000001).OrderBy(x => x.GroupName).ToList();
                DataTable dt = Helper.ToDataTable(g);
                DataView dv = dt.DefaultView;
                dv.Sort = "GroupName asc";
                DataTable sortedDT = dv.ToTable();
                ViewState["dt"] = sortedDT;
                gv.DataSource = sortedDT;
                gv.DataBind();
            }
        }
    }
}