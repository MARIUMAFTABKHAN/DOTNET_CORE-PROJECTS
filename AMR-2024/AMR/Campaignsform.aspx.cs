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
    public partial class Campaignsform : BaseClass
    {
        Model1Container db= new Model1Container();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                BindGrid();
            }
        }
        private void BindGrid()
        {
            var a= db.Campaigns.Where(x=>x.Active==true).OrderBy(x=>x.Title).
                Select(x=>new
                {
                    x.ID,x.Title,x.Launch_date,
                    status=x.Active==true?"Active":"InActive",
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        Campaign obj = new Campaign();
                        obj.ID = db.usp_IDctr("Campaign").SingleOrDefault().Value;
                        obj.Title = txttitle.Text;
                        obj.Remarks = txtremarks.Text;
                        string datestring = txtdate.Value;
                        DateTime LaunchDate;
                        if (DateTime.TryParse(datestring, out LaunchDate))
                        {
                            obj.Launch_date = LaunchDate;
                        }
                        else
                        {
                            lblmessage.Text = "Invalid date format.";
                        }
                        obj.Active = chstatus.Checked;

                        obj.Rec_Added_by = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_DateTime = currentDateTime.Date+currentDateTime.TimeOfDay;

                        obj.Rec_Edited_by = null;
                        obj.Rec_Edited_DateTime = null;

                        db.Campaigns.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Campaign Created Successfully";
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
                        var obj = db.Campaigns.Where(x => x.ID == ID).SingleOrDefault();
                        obj.ID = ID;
                        obj.Title = txttitle.Text;
                        obj.Remarks = txtremarks.Text;
                        string datestring = txtdate.Value;
                        DateTime LaunchDate;
                        if (DateTime.TryParse(datestring, out LaunchDate))
                        {
                            obj.Launch_date = LaunchDate;
                        }
                        else
                        {
                            lblmessage.Text = "Invalid date format.";
                        }
                        obj.Active = chstatus.Checked;


                        obj.Rec_Edited_by = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_DateTime = currentDateTime.Date + currentDateTime.TimeOfDay;
                        
                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Campaign Updated Successfully";
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
            txttitle.Text=string.Empty;
            txtdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtremarks.Text = string.Empty;
            chstatus.Checked = false;

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
            var obj = db.Campaigns.Where(x => x.ID == ID).SingleOrDefault();
            txttitle.Text = obj.Title;
            if (obj.Launch_date.HasValue)
            {
                txtdate.Value = obj.Launch_date.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                hdnLaunchDate.Value = string.Empty; // or handle it accordingly
            }
            txtremarks.Text = obj.Remarks;
            chstatus.Checked = obj.Active==true;
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
                    var record = db.Campaigns.SingleOrDefault(x => x.ID == id);
                    if (record != null)
                    {
                        record.Active = false;
                        record.Rec_Edited_by = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edited_DateTime = currentDateTime.Date + currentDateTime.TimeOfDay;
                        
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