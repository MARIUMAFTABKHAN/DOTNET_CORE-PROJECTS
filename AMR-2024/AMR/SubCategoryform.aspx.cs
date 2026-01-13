using AMR;
using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class SubCategoryform : System.Web.UI.Page
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateMainCatDropdown();
                PopulateClubCatDropdown();
                BindGrid();
            }
        }
        private void BindGrid()
        {
            var query = from subCategory in db.SubCategories
                        join mainCategory in db.MainCategories
                        on subCategory.Main_Category equals (int?)mainCategory.Id into mainCatJoin
                        from mainCategory in mainCatJoin.DefaultIfEmpty()
                        join clubCategory in db.ClubCategories
                        on subCategory.Club_Category equals (int?)clubCategory.ID into clubCatJoin
                        from clubCategory in clubCatJoin.DefaultIfEmpty()
                        where subCategory.Status=="A"
                        select new
                        {
                            subCategory.Id,
                            subCategory.Category_Title,
                            StatusDescription = subCategory.Status == "A" ? "Active" : "Inactive",
                            subCategory.Main_Category,
                            subCategory.Club_Category,
                            IsBrand=(subCategory.Is_Brand??false)?"Yes":"No",
                            AMR_Comparision = (subCategory.Amr ?? false) ? "Yes" : "No",
                            MainCategory = mainCategory != null ? mainCategory.Category_Title : null,
                            ClubCategory = clubCategory != null ? clubCategory.Category_Title : null
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtcat.Text = string.Empty;
            ddlmaincat.SelectedIndex = 0;
            ddlclubcat.SelectedIndex = 0;
            chstatus.Checked = false;
            chamr.Checked = false;
            chbrand.Checked = false;
            chidro.Checked = false;
            btnSave.Text = "Save";
            lblmessage.Text = string.Empty;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        SubCategory obj = new SubCategory();
                        obj.Id = db.usp_IDctr("SubCategories").SingleOrDefault().Value;
                        obj.Category_Title = txtcat.Text;

                        string mainCategory = ddlmaincat.SelectedValue;
                        obj.Main_Category = Convert.ToInt32(mainCategory);

                        if (string.IsNullOrEmpty(ddlclubcat.SelectedValue))
                        {
                            obj.Club_Category = 0;
                        }
                        else if (int.TryParse(ddlclubcat.SelectedValue, out int clubcategory))
                        {
                            obj.Club_Category = clubcategory;
                        }
                        else
                        {
                            obj.Club_Category = 0;
                        }


                        //string clubCategory = ddlclubcat.SelectedValue;
                        //obj.Club_Category = Convert.ToInt32(clubCategory);

                        obj.Status = chstatus.Checked ? "A" : "I";
                        obj.Is_Brand=chbrand.Checked;
                        obj.Amr = chamr.Checked;
                        obj.cExport = chidro.Checked;

                        obj.Urdu_Title = null;
                        obj.Address_to = null;
                        obj.Address_1=null;
                        obj.Address_2=null;
                        obj.Address_3=null;
                        obj.Address_4=null;
                        obj.Telephone_No = null;
                        obj.Email_Address = null;

                        //string usergroup = Request.Cookies["UserGroup"]?.Value;
                        //int grp;
                        //if (int.TryParse(usergroup, out grp))
                        //{
                            //obj.Grp = (byte?)grp;
                        obj.Grp = 0;
                        //}
                        //else
                        //{
                        //    throw new Exception("Failed to parse UserGroup from cookie.");
                        //}

                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        db.SubCategories.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Sub Category Created Successfully";
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
                        var obj = db.SubCategories.Where(x => x.Id == ID).SingleOrDefault();
                        obj.Id = ID;
                        obj.Category_Title = txtcat.Text;

                        string mainCategory = ddlmaincat.SelectedValue;
                        obj.Main_Category = Convert.ToInt32(mainCategory);

                        if (string.IsNullOrEmpty(ddlclubcat.SelectedValue))
                        {
                            obj.Club_Category = 0;
                        }
                        else if (int.TryParse(ddlclubcat.SelectedValue, out int clubcategory))
                        {
                            obj.Club_Category = clubcategory;
                        }
                        else
                        {
                            obj.Club_Category = 0;
                        }

                        obj.Status = chstatus.Checked ? "A" : "I";
                        obj.Is_Brand = chbrand.Checked;
                        obj.Amr = chamr.Checked;
                        obj.cExport = chidro.Checked;

                        obj.Urdu_Title = null;
                        obj.Address_to = null;
                        obj.Address_1 = null;
                        obj.Address_2 = null;
                        obj.Address_3 = null;
                        obj.Address_4 = null;
                        obj.Telephone_No = null;
                        obj.Email_Address = null;

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
                        lblmessage.Text = "Sub Category Updated Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;
        }
        private void PopulateMainCatDropdown()
        {
            var maincat = db.MainCategories
                .Where(mc=>mc.Status=="A")
                .OrderBy(mc => mc.Category_Title).Select(mc => new
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
        private void PopulateClubCatDropdown()
        {
            var clubcat = db.ClubCategories.
                Where(mc => mc.Status == "A").
                OrderBy(cc => cc.Category_Title).Select(cc => new
            {
                cc.ID,
                cc.Category_Title
            }).ToList();

            ddlclubcat.DataSource = clubcat;
            ddlclubcat.DataValueField = "Id";
            ddlclubcat.DataTextField = "Category_Title";
            ddlclubcat.DataBind();

            ddlclubcat.Items.Insert(0, new ListItem("Select Club Category", ""));
        }

        protected void ddlmaincat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlclubcat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.SubCategories.Where(x => x.Id == ID).SingleOrDefault();
            txtcat.Text = obj.Category_Title;
            ddlmaincat.SelectedValue = obj.Main_Category.ToString();
            //ddlclubcat.SelectedValue = obj.Club_Category.ToString();
            if (ddlclubcat.Items.FindByValue(obj.Club_Category.ToString()) != null)
            {
                ddlclubcat.SelectedValue = obj.Club_Category.ToString();
            }
            chstatus.Checked = obj.Status == "A";
            chbrand.Checked = obj.Is_Brand ?? false;
            chamr.Checked = obj.Amr ?? false;
            chidro.Checked = obj.cExport;
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
                    var record = db.SubCategories.SingleOrDefault(x => x.Id == id);
                    if (record != null)
                    {
                        record.Status = "I";
                        record.Rec_Edited_By = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edited_Date = currentDateTime.Date;
                        record.Rec_Edited_Time = currentDateTime.Date+currentDateTime.TimeOfDay;
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