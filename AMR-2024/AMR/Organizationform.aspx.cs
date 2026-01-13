using AMR;
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
    public partial class Organizationform : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateUsersDropdown();
                BindGrid();
            }
        }
        private void BindGrid()
        {
            var a = from org in db.Organisations
                        join user in db.Users
                            on org.User_Id equals user.User_Id
                        select new
                        {
                            org.ID,
                            org.Title,
                            org.User_Id,
                            user.User_Name
                        };

            var resultList = a.ToList();
            DataTable dt = Helper.ToDataTable(resultList);
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        Organisation obj = new Organisation();
                        obj.ID = db.usp_IDctr("Organisation").SingleOrDefault().Value;
                        obj.Id_Old = null;
                        obj.Title = txttitle.Text;
                        obj.User_Id = ddluser.SelectedValue;
                        obj.cExport = false;
                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        db.Organisations.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Organisation Created Successfully";
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
                        int ID = Convert.ToInt32(ViewState["RecordID"]);
                        var obj = db.Organisations.Where(x => x.ID == ID).SingleOrDefault();
                        obj.ID = ID;
                        obj.Id_Old = null;
                        obj.Title = txttitle.Text;
                        obj.User_Id = ddluser.SelectedValue;
                        obj.cExport = false;

                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                        
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date = currentDateTime.Date;
                        obj.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Organisation Updated Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.Organisations.Where(x => x.ID == ID).SingleOrDefault();
            txttitle.Text = obj.Title;
            ddluser.SelectedValue = obj.User_Id;
            btnSave.Text = "Update";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txttitle.Text = string.Empty;
            ddluser.SelectedIndex = 0;
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void ddluser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopulateUsersDropdown()
        {
            var user = db.Users
                .Where(mc => mc.User_Active == true)
                .OrderBy(mc => mc.User_Name).Select(mc => new
                {
                    mc.User_Id,
                    mc.User_Name
                }).ToList();

            ddluser.DataSource = user;
            ddluser.DataValueField = "User_Id";
            ddluser.DataTextField = "User_Name";
            ddluser.DataBind();

            ddluser.Items.Insert(0, new ListItem("Select  User", ""));
        }
    }
}