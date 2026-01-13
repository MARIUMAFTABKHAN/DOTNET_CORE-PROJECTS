using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class RoleManager : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities ();
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
                        var ObjID = db.usp_GetIDCTRCounter("Role").SingleOrDefault().Value;
                        Role obj = new Role();
                        obj.ID = Convert.ToInt32(ObjID);

                        obj.UserRole = txtRoleName.Text;
                        obj.IsActive = ChkIsActive.Checked;

                        //int userId = (int)HttpContext.Current.Session["userid"];
                        //obj.Rec_Added_By = userId; ;

                        //var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        //obj.Rec_Added_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                        //obj.Rec_Edit_Date = null;
                        //obj.Rec_Edit_By = null;

                        db.Roles.Add(obj );

                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(ObjID), "Role", "Insert");

                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Role Added Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);

                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        int ID = Convert.ToInt32(ViewState["RecordID"]);
                        var obj = db.Roles.Where(x => x.ID == ID).SingleOrDefault();
                        //obj.ID = ID;
                        obj.UserRole = txtRoleName.Text;
                        obj.IsActive = ChkIsActive.Checked;

                        //int userId = (int)HttpContext.Current.Session["userid"];
                        //obj.Rec_Edit_By = userId;

                        //var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        //obj.Rec_Edit_Date = currentDateTime.Date + currentDateTime.TimeOfDay;
                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(ID), "Role", "Update");
                        BindGrid();

                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Role Updated Successfully";
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
            var g = (from u in db.Roles.OrderBy(X => X.UserRole)
                      select new
                      {
                          u.ID,
                          u.UserRole,
                          Status = u.IsActive == true ? "Active" : "InActive"
                      }).ToList();

            gv.DataSource = g;
            gv.DataBind();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtRoleName.Text = string.Empty;
            ChkIsActive.Checked = false;
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
            ImageButton imageButton = (ImageButton)sender;
            // Try to parse the CommandArgument
            int id;
            if (!int.TryParse(imageButton.CommandArgument, out id))
            {
                lblmessage.Text = "Invalid Division ID.";
                return;
            }


            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.Roles.SingleOrDefault(x => x.ID == id);
                    if (record != null)
                    {
                        record.IsActive = false;

                        //int userId = (int)HttpContext.Current.Session["userid"];
                        //record.Rec_Edit_By = userId;

                        //var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        //record.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(id, "Role", "Delete");
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Role Deleted Successfully";
                    }
                }
                catch (Exception ex) { }
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;

            BindGrid();

        }

        private static void logmaintain(int id, string actiononform, string actiontaken)
        {
            using (CDSEntities db = new CDSEntities())
            {
                int userId = (int)HttpContext.Current.Session["userid"];

                if (userId != 0)
                {
                    clsLogManager.RecordID = id;
                    clsLogManager.ActionOnForm = actiononform;
                    clsLogManager.ActionBy = userId;
                    clsLogManager.ActionOn = DateTime.Now;
                    clsLogManager.ActionTaken = actiontaken;
                    clsLogManager.SetLog(db);
                }
                else
                {
                    throw new Exception("User info not found for the given session user ID.");
                }
            }
        }
    }
}