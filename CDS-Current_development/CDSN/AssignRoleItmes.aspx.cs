using CDSN;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{ 
    public partial class AssignRoleItmes : System.Web.UI.Page
    {

        CDSEntities db = new CDSEntities();

        protected void Page_Load(object sender, EventArgs e)
        {


            //Int32 PageSize = Convert.ToInt32(Session["PageSize"]);
            if (!Page.IsPostBack)
            {


                var r = db.Roles.OrderBy(x => x.UserRole).ToList();
                ddlRole.DataTextField = "UserRole";
                ddlRole.DataValueField = "ID";
                ddlRole.DataSource = r;
                ddlRole.DataBind();
                BindGrid();
                SetHeader();

            }
        }

        //private void SetHeader()
        //{
        //    int Gridcount = gv.Rows.Count;
        //    int checkedRows = 0;
        //    foreach (GridViewRow row in gv.Rows)
        //    {
        //        CheckBox chkBxSelect = (CheckBox)row.FindControl("chkRow");
        //        if (chkBxSelect.Checked == true)
        //            checkedRows++;
        //    }

        //    if (Gridcount == checkedRows)
        //    {
        //        CheckBox headerChkBox = ((CheckBox)gv.HeaderRow.FindControl("chkHeader"));
        //        headerChkBox.Checked = true;
        //    }
        //}
        private void SetHeader()
        {
            // Prevent null reference when no rows or header exists
            if (gv.Rows.Count == 0 || gv.HeaderRow == null)
                return;

            int Gridcount = gv.Rows.Count;
            int checkedRows = 0;

            foreach (GridViewRow row in gv.Rows)
            {
                CheckBox chkBxSelect = (CheckBox)row.FindControl("chkRow");
                if (chkBxSelect != null && chkBxSelect.Checked)
                    checkedRows++;
            }

            if (Gridcount == checkedRows)
            {
                CheckBox headerChkBox = (CheckBox)gv.HeaderRow.FindControl("chkHeader");
                if (headerChkBox != null)
                {
                    headerChkBox.Checked = true;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Role role = new Role();

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                int RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                var roleitem = db.RolesItems.Where(x => x.RoleID == RoleID);
                db.RolesItems.RemoveRange(roleitem);
                db.SaveChanges();
                try
                {

                    foreach (GridViewRow dr in gv.Rows)
                    {
                        Int32 ItemID = Convert.ToInt32(gv.DataKeys[dr.RowIndex].Value);

                        RolesItem obj = new RolesItem();

                        CheckBox chk = (CheckBox)dr.FindControl("chkRow");
                        if (chk.Checked == true)
                        {                           
                            
                            var  objval = db.usp_GetIDCTRCounter("RolesItems").SingleOrDefault().Value;
                            int ID = Convert.ToInt32(objval); ;
                            db.SaveChanges();
                            obj = new RolesItem();
                            obj.ID = ID;
                            obj.RoleID = RoleID;
                            obj.ItemID = ItemID;
                            db.RolesItems.Add(obj);
                            db.SaveChanges();
                            lblmessage.Text = "Item Assigned to Role..... ";
                        }
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    lblmessage.Text = ExceptionHandler.GetException(ex);
                }
            }
        }
        private void BindGrid()
        {

            try
            {
                int RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                var s = db.Database.SqlQuery<RoleItemResult>(
                    "EXEC usp_GetRoleItems @RoleID",
                    new SqlParameter("@RoleID", RoleID)
                ).ToList();

                gv.DataSource = s;
                gv.DataBind();
            }
            catch (Exception ex)
            {
                lblmessage.Text = "Grid error: " + ex.Message;
            }


        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            BindGrid();
            SetHeader();

        }
    }
}