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
    public partial class RoleManagement : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
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
                        var s = db.usp_IDctr("Role").SingleOrDefault();
                        int ID = s.Value;
                        Role obj = new Role();
                        obj.ID = ID;
                        obj.UserRole = txtRoleName.Text;
                        obj.IsActive = ChkIsActive.Checked;
                        db.Roles.Add(obj);
                        db.SaveChanges();
                        BindGrid();
                        db.SaveChanges();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Role Created Successfully";
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
                        var obj = db.Roles.Where(x => x.ID == ID).SingleOrDefault();
                        obj.ID = ID;
                        obj.UserRole = txtRoleName.Text;
                        obj.IsActive = ChkIsActive.Checked;
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
        }

        private void BindGrid()
        {
            var g = db.Roles.OrderBy(x => x.UserRole).ToList();

            DataTable dt = Helper.ToDataTable(g);
            ViewState["dt"] = dt;
            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtRoleName.Text = string.Empty;
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
            var obj = db.Roles.Where(x => x.ID == ID).SingleOrDefault();
            txtRoleName.Text = obj.UserRole;
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
                    var info = db.Roles.Find(ID);
                    if (info != null)
                    {
                        db.Roles.Remove(info);
                        db.SaveChanges();
                        LogManagers.RecordID = ID;
                        LogManagers.ActionOnForm = "Role";
                        LogManagers.ActionBy = ((UserInfo)HttpContext.Current.Session["UserObject"]).ID;//Convert.ToInt32(HttpContext.Current.Session["UserID"]);
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
    }
}