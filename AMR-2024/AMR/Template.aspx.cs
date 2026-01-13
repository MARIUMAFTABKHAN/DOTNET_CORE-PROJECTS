using AMR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class Template : BaseClass
    {
        Model1Container db= new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            var a= db.TemplateMinutes.Where(x=>x.Temp_Status==true).
                Select(x=> new
                {
                    x.ID,
                    x.Minutes_Of_Meeting,
                    Temp_Status=(x.Temp_Status==true)?"Yes":"No",
                    Extra_Info = (x.Extra_Info == true) ? "Yes" : "No"
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
                        TemplateMinute obj = new TemplateMinute();
                        obj.ID = db.usp_IDctr("TemplateMinute").SingleOrDefault().Value;
                        obj.Minutes_Of_Meeting = txtmin.Text;
                        obj.Temp_Status = chstatus.Checked;
                        obj.Extra_Info = chinfo.Checked;

                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        db.TemplateMinutes.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Template of Minutes Created Successfully";
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
                        var obj = db.TemplateMinutes.Where(x => x.ID == ID).SingleOrDefault();
                        obj.ID = ID;
                        obj.Minutes_Of_Meeting = txtmin.Text;
                        obj.Temp_Status = chstatus.Checked;
                        obj.Extra_Info = chinfo.Checked;
                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                        
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date = currentDateTime.Date;
                        obj.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Template of Minutes Updated Successfully";
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
            txtmin.Text = string.Empty;
            chinfo.Checked = false;
            chstatus.Checked = false;
            btnSave.Text = "Save";
            lblmessage.Text = string.Empty;
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
            var obj = db.TemplateMinutes.Where(x => x.ID == ID).SingleOrDefault();
            txtmin.Text = obj.Minutes_Of_Meeting;
            chstatus.Checked = obj.Temp_Status ;
            chinfo.Checked = obj.Extra_Info;
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
                    var record = db.TemplateMinutes.SingleOrDefault(x => x.ID == id);
                    if (record != null)
                    {
                        record.Temp_Status = false;
                        record.Rec_Edited_By = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edited_Date = currentDateTime.Date;
                        record.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

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