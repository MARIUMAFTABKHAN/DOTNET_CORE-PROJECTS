using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class tblUsers : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                PopulateDesgnDropdown();
                PopulateUserGroupDropdown();
                PopulateEditionDropdown();
                PopulateDeptDropdown();
            }
        }
        private void BindGrid()
        {
            var a = db.Users.Where(x => x.User_Active == true)
                //OrderBy(x => x.Category_Title)
                .Select(x => new {
                    x.User_Id,
                    x.User_Name,
                    x.User_Desig
                }).ToList();

            DataTable dt = Helper.ToDataTable(a);
            ViewState["dt"] = dt;
            if (gv != null)
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
            else
            {
                lblmessage.Text = "Error: GridView control is not available.";
            }
        }
        private void PopulateDesgnDropdown()
        {
            var data = db.Designations
                
                .OrderBy(mc => mc.Desgn_Name).Select(mc => new
                {
                    mc.Id,
                    mc.Desgn_Name
                }).ToList();

            ddldesgn.DataSource = data;
            ddldesgn.DataValueField = "Desgn_Name";
            ddldesgn.DataTextField = "Desgn_Name";
            ddldesgn.DataBind();

            ddldesgn.Items.Insert(0, new ListItem("Select Designation", ""));
        }
        private void PopulateUserGroupDropdown()
        {
            var data = db.UserGroups
                        .Select(mc => new
                {
                    mc.Group_Id,
                    mc.Group_Desc
                }).ToList();

            ddlgroup.DataSource = data;
            ddlgroup.DataValueField = "Group_Id";
            ddlgroup.DataTextField = "Group_Desc";
            ddlgroup.DataBind();

            ddlgroup.Items.Insert(0, new ListItem("Select Group", ""));
        }
        private void PopulateEditionDropdown()
        {
            var data = db.GroupComps
                        .Select(mc => new
                        {
                            mc.GroupComp_Id,
                            mc.GroupComp_Name
                        }).ToList();

            ddledition.DataSource = data;
            ddledition.DataValueField = "GroupComp_Id";
            ddledition.DataTextField = "GroupComp_Name";
            ddledition.DataBind();

            ddledition.Items.Insert(0, new ListItem("Select Edition", ""));
        }
        private void PopulateDeptDropdown()
        {
            var data = db.Departments
                        .Select(mc => new
                        {
                            mc.Id,
                            mc.DeptName
                        }).ToList();

            ddldept.DataSource = data;
            ddldept.DataValueField = "Id";
            ddldept.DataTextField = "DeptName";
            ddldept.DataBind();

            ddldept.Items.Insert(0, new ListItem("Select Department", ""));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        User obj = new User();
                        obj.User_Id = txtusercode.Text;
                        obj.User_Name = txtusername.Text;
                        obj.User_Pass = txtPWD.Text;

                        if (string.IsNullOrEmpty(ddldesgn.SelectedValue))
                        {
                            obj.User_Desig = "";
                        }
                        else 
                        {
                            obj.User_Desig = ddldesgn.SelectedValue;
                        }
                        if (string.IsNullOrEmpty(ddlgroup.SelectedValue))
                        {
                            obj.User_Groups = 0;
                        }
                        else if (int.TryParse(ddlgroup.SelectedValue, out int clientexe))
                        {
                            obj.User_Groups = clientexe;
                        }
                        obj.User_Level = 0;
                        obj.User_Active = chactive.Checked;
                        obj.cExport = false;

                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        db.Users.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "User Created Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = $"Error: {ex.Message}\n{ex.StackTrace}";

                    }
                }
            }
            else
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        string ID = ViewState["RecordID"] as string;
                        var obj = db.Users.Where(x => x.User_Id == ID).SingleOrDefault();
                        //obj.User_Id = ID;

                        obj.User_Id = txtusercode.Text;
                        obj.User_Name = txtusername.Text;
                        obj.User_Pass = txtPWD.Text;

                        if (string.IsNullOrEmpty(ddldesgn.SelectedValue))
                        {
                            obj.User_Desig = "";
                        }
                        else
                        {
                            obj.User_Desig = ddldesgn.SelectedValue;
                        }
                        if (string.IsNullOrEmpty(ddlgroup.SelectedValue))
                        {
                            obj.User_Groups = 0;
                        }
                        else if (int.TryParse(ddlgroup.SelectedValue, out int clientexe))
                        {
                            obj.User_Groups = clientexe;
                        }
                        obj.User_Level = 0;
                        obj.User_Active = chactive.Checked;
                        obj.cExport = false;

                       
                        obj.cExport = false;

                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date = currentDateTime.Date;
                        obj.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "User Updated Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            string ID = gv.DataKeys[myRow.RowIndex].Value.ToString();
            ViewState["RecordID"] = ID;
            var obj = db.Users.Where(x => x.User_Id == ID).SingleOrDefault();
            txtusercode.Text = obj.User_Id;

            txtusername.Text = obj.User_Name;
            txtPWD.Text = obj.User_Pass;

            if (!string.IsNullOrEmpty(obj.User_Desig))
            {
                ListItem item = ddldesgn.Items.FindByText(obj.User_Desig);
                if (item != null)
                {
                    ddldesgn.ClearSelection();
                    item.Selected = true;
                }
            }
            if (ddlgroup.Items.FindByValue(obj.User_Groups.ToString()) != null)
            {
                ddlgroup.SelectedValue = obj.User_Groups.ToString();
            }
            //if (ddldept.Items.FindByValue(obj.User_Desig.ToString()) != null)
            //{
            //    ddldesgn.SelectedValue = obj.User_Desig.ToString();
            //}
            
            chactive.Checked = obj.User_Active == true;
            btnSave.Text = "Update";
        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}