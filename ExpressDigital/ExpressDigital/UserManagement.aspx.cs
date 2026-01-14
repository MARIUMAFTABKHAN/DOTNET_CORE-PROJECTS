using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
namespace ExpressDigital
{

    public partial class UserManagement : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var r = db.Roles.OrderBy(x => x.UserRole).ToList();
                ddlRole.DataValueField = "ID";
                ddlRole.DataTextField = "UserRole";
                ddlRole.DataSource = r;
                ddlRole.DataBind();
                
                BindGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        var s = db.usp_IDctr("UserManagement").SingleOrDefault();
                        int ID = s.Value;
                        UserInfo obj = new UserInfo();
                        obj.ID = ID;
                        obj.UserName = txtUserName.Text;
                        obj.UserID = txtUserID.Text;
                        obj.UserPassword = txtPWD.Text;
                        obj.Email = txtEmail.Text;
                        obj.Contact = txtContact.Text;
                        obj.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                        obj.IsActive = ChkIsActive.Checked;
                        obj.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                        obj.CreatedOn = DateTime.Now;
                        db.UserInfoes.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "User Created Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }
            else
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        int ID = Convert.ToInt32(ViewState["RecordID"]);
                        var obj = db.UserInfoes.Where(x => x.ID == ID).SingleOrDefault();
                        obj.ID = ID;
                        obj.UserName = txtUserName.Text;
                        obj.UserID = txtUserID.Text;
                        obj.UserPassword = txtPWD.Text;
                        obj.Email = txtEmail.Text;
                        obj.Contact = txtContact.Text;
                        obj.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                        obj.IsActive = ChkIsActive.Checked;
                        obj.UpdatedBy = ((UserInfo)Session["UserObject"]).ID;
                        obj.UpdatedOn = DateTime.Now;
                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }
            txtPWD.TextMode = TextBoxMode.SingleLine;
        }

        private void BindGrid()
        {
            var g = (from u in db.UserInfoes.ToList()
                     select new
                     {
                         u.ID,
                         u.UserName,
                         u.UserID,
                         u.Email,
                         u.Contact,
                         u.Role.UserRole,
                         u.IsActive
                     }).ToList();

            DataTable dt = Helper.ToDataTable(g);
            ViewState["dt"] = dt;
            gv.DataSource = dt;
            gv.DataBind();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtPWD.TextMode = TextBoxMode.Password;
            txtUserName.Text = string.Empty;
            txtUserID.Text = string.Empty;
            txtPWD.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtContact.Text = string.Empty;
            ddlRole.SelectedIndex = 0;
            ChkIsActive.Checked = true;
            btnSave.Text = "Save";
            lblmessage.Text = string.Empty;
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.UserInfoes.Where(x => x.ID == ID).SingleOrDefault();
            txtUserName.Text = obj.UserName;
            txtUserID.Text = obj.UserID;
            txtPWD.TextMode = TextBoxMode.SingleLine;
            txtPWD.Text = obj.UserPassword;
            txtEmail.Text = obj.Email;
            txtContact.Text = obj.Contact;
            ddlRole.SelectedValue = Convert.ToString(obj.RoleID);
            ChkIsActive.Checked = obj.IsActive;
            btnSave.Text = "Update";
        }
        protected void DelButton_Click(object sender, EventArgs e)
        {
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
                    var info = db.UserInfoes.Where(x => x.ID == ID).SingleOrDefault();
                    if (info != null)
                    {
                        info.IsActive = false;
                        db.SaveChanges();
                        LogManagers.RecordID = ID;
                        LogManagers.ActionOnForm = "UserManagement";
                        LogManagers.ActionBy = ((UserInfo)HttpContext.Current.Session["UserObject"]).ID;//Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                        LogManagers.ActionOn = DateTime.Now;
                        LogManagers.ActionTaken = "DeActive";
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
    }
}