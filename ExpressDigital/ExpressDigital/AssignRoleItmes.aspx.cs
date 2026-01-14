using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class AssignRoleItmes : System.Web.UI.Page
    {

        DbDigitalEntities db = new DbDigitalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
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

        private void SetHeader()
        {
            int Gridcount = gv.Rows.Count;
            int checkedRows = 0;
            foreach (GridViewRow row in gv.Rows)
            {
                CheckBox chkBxSelect = (CheckBox)row.FindControl("chkRow");
                if (chkBxSelect.Checked == true)
                    checkedRows++;
            }
            if (Gridcount == checkedRows)
            {
                CheckBox headerChkBox = ((CheckBox)gv.HeaderRow.FindControl("chkHeader"));
                headerChkBox.Checked = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    int roleID = Convert.ToInt32(ddlRole.SelectedValue);
                    var roleitem = db.RolesItems.Where(x => x.RoleID == roleID);
                    db.RolesItems.RemoveRange(roleitem);
                    db.SaveChanges();

                    foreach (GridViewRow dr in gv.Rows)
                    {
                        CheckBox chk = (CheckBox)dr.FindControl("chkRow");
                        if (chk.Checked == true)
                        {
                            var itemID = Convert.ToInt32(gv.DataKeys[dr.RowIndex].Value);
                            var ID = db.usp_IDctr("RoleItems").SingleOrDefault();
                            var obj = new RolesItem();
                            obj.ID = Convert.ToInt32(ID);
                            obj.RoleID = roleID;
                            obj.ItemID = itemID;
                            db.RolesItems.Add(obj);

                            db.SaveChanges();
                            lblmessage.Text = "Role assignments updated successfully.";
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }
        }
        private void BindGrid()
        {
            var roleID = Convert.ToInt32(ddlRole.SelectedValue);
            var data = db.usp_GetRoleItems(roleID).ToList();
            gv.DataSource = data;
            gv.DataBind();
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            BindGrid();

            SetHeader();
        }
    }
}