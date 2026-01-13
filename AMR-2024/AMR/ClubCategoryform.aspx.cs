using AMR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace AMR
{
    public partial class ClubCategoryform : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateMainCatDropdown();
                BindGrid();
            }

        }
        private void BindGrid()
        {
            var query = from clubCategory in db.ClubCategories
                        join mainCategory in db.MainCategories
                        on clubCategory.Main_Category equals mainCategory.Id
                        where clubCategory.Status=="A"
                        select new
                        {
                            clubCategory.ID,
                            StatusDescription = clubCategory.Status == "A" ? "Active" : "Inactive",
                            clubCategory.Main_Category,
                            ClubCategory_Title = clubCategory.Category_Title,
                            MainCategory_Title = mainCategory.Category_Title
                        };

            var resultList = query.ToList();

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
        protected void ddlmaincat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        ClubCategory obj = new ClubCategory();
                        obj.ID = db.usp_IDctr("ClubCategories").SingleOrDefault().Value;
                        obj.Category_Title = txtcat.Text;
                        
                        string mainCategory=ddlmaincat.SelectedValue;
                        obj.Main_Category =Convert.ToInt32( mainCategory);
                        obj.Status = chstatus.Checked ? "A" : "I";

                        string usergroup = Request.Cookies["UserGroup"]?.Value;
                        int grp;
                        if (int.TryParse(usergroup, out grp))
                        {
                            obj.Grp = (byte?)grp;
                        }
                        else
                        {
                            throw new Exception("Failed to parse UserGroup from cookie.");
                        }

                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        db.ClubCategories.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Club Category Created Successfully";
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
                        var obj = db.ClubCategories.Where(x => x.ID == ID).SingleOrDefault();
                        obj.ID = ID;
                        obj.Category_Title = txtcat.Text;
                        string mainCategory = ddlmaincat.SelectedValue;
                        obj.Main_Category = Convert.ToInt32(mainCategory);
                        obj.Status = chstatus.Checked ? "A" : "I";
                        
                        string usergroup = Request.Cookies["UserGroup"]?.Value;
                        int grp;
                        if (int.TryParse(usergroup, out grp))
                        {
                            obj.Grp = (byte?)grp;
                        }
                        else
                        {
                            throw new Exception("Failed to parse UserGroup from cookie.");
                        }
                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date = currentDateTime.Date;
                        obj.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtcat.Text = string.Empty;
            ddlmaincat.SelectedIndex = 0;
            chstatus.Checked = false;
            btnSave.Text = "Save";
            lblmessage.Text = string.Empty;
        }
        private void PopulateMainCatDropdown()
        {
            var maincat = db.MainCategories.OrderBy(mc => mc.Category_Title).
                Where(mc=>mc.Status=="A").
                Select(mc => new
            {
                mc.Id,
                mc.Category_Title
            }).ToList();

            ddlmaincat.DataSource = maincat;
            ddlmaincat.DataValueField = "Id";
            ddlmaincat.DataTextField = "Category_Title";
            ddlmaincat.DataBind();

            ddlmaincat.Items.Insert(0, new ListItem("Select Main Category", ""));
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.ClubCategories.Where(x => x.ID == ID).SingleOrDefault();
            txtcat.Text = obj.Category_Title;
            ddlmaincat.SelectedValue =obj.Main_Category.ToString();
            chstatus.Checked = obj.Status == "A";
            btnSave.Text = "Update";
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            ImageButton deleteButton = (ImageButton)sender;
            int id = Convert.ToInt32(deleteButton.CommandArgument);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.ClubCategories.SingleOrDefault(x => x.ID == id);
                    if (record != null)
                    {
                        record.Status = "I";
                        record.Rec_Edited_By = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edited_Date = currentDateTime.Date;
                        record.Rec_Edited_Time = currentDateTime;
                        string usergroup = Request.Cookies["UserGroup"]?.Value;
                        int grp;
                        if (int.TryParse(usergroup, out grp))
                        {
                            record.Grp = (byte?)grp;
                        }
                        else
                        {
                            throw new Exception("Failed to parse UserGroup from cookie.");
                        }

                        db.SaveChanges();
                        scope.Complete();

                        lblmessage.Text = "Record deleted successfully.";
                    }
                    else
                    {
                        lblmessage.Text = "Record not found.";
                    }
                }
                catch (Exception ex)
                {
                    lblmessage.Text = $"Error: {ex.Message}";
                }
            }
            BindGrid();
        }
    }
}