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
    public partial class AgencyGroupform : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            var a = db.AgencyGroups.
                OrderBy(x => x.Group_Caption).Select(x => new {
                    x.RecID,
                    x.Group_Caption
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
                        AgencyGroup obj = new AgencyGroup();
                        obj.RecID = db.usp_IDctr("AgecnyGroup").SingleOrDefault().Value;
                        obj.Group_Caption = txtcap.Text;
                        obj.Cmp = null;

                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        db.AgencyGroups.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Agency Group Created Successfully";
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
                        var obj = db.AgencyGroups.Where(x => x.RecID == ID).SingleOrDefault();
                        obj.RecID = ID;
                        obj.Group_Caption = txtcap.Text;
                        obj.Cmp = null;
                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                        
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date = currentDateTime;
                        obj.Rec_Edited_Time = currentDateTime + currentDateTime.TimeOfDay;

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
            txtcap.Text = string.Empty;
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
            var obj = db.AgencyGroups.Where(x => x.RecID == ID).SingleOrDefault();
            txtcap.Text = obj.Group_Caption;
            btnSave.Text = "Update";
        }
        //protected void DeleteButton_Click(object sender, EventArgs e)
        //{
        //    ImageButton deleteButton = (ImageButton)sender;
        //    int id = Convert.ToInt32(deleteButton.CommandArgument);

        //    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
        //    {
        //        try
        //        {
        //            var record = db.AgencyGroups.SingleOrDefault(x => x.RecID == id);
        //            if (record != null)
        //            {
        //                record.Status = "I";
        //                record.Rec_Edited_By = Request.Cookies["UserId"]?.Value;

        //                var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
        //                record.Rec_Edited_Date = currentDateTime.Date;
        //                record.Rec_Edited_Time = currentDateTime;
        //                string usergroup = Request.Cookies["UserGroup"]?.Value;
        //                int grp;
        //                if (int.TryParse(usergroup, out grp))
        //                {
        //                    record.Grp = (byte?)grp;
        //                }
        //                else
        //                {
        //                    throw new Exception("Failed to parse UserGroup from cookie.");
        //                }

        //                string usercexport = Request.Cookies["UsercExport"]?.Value;
        //                bool cexport;
        //                if (bool.TryParse(usercexport, out cexport))
        //                {
        //                    record.cExport = cexport;
        //                }
        //                else
        //                {
        //                    record.cExport = false;
        //                }

        //                db.SaveChanges();
        //                scope.Complete();

        //                lblmessage.Text = "Record deleted successfully.";
        //            }
        //            else
        //            {
        //                lblmessage.Text = "Record not found.";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            lblmessage.Text = $"Error: {ex.Message}";
        //        }
        //    }
        //    BindGrid();
        //}
    }
}