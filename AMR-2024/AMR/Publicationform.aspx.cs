using AMR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease;

namespace AMR
{
    public partial class Publicationform : BaseClass
    {
        Model1Container db = new Model1Container();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                
            }
        }
        private void BindGrid()
        {
            var a = db.Publications.Where(x => x.Status == "A").
                OrderBy(x => x.Publication_Name).Select(x=>new {x.Id,x.Publication_Name,
                x.Pub_Abreviation,
                In_House_Publication = x.In_House_Publication ? "Yes" : "No",
                AMR_Comparision=x.AMR_Comparision?"Yes":"No",
                Status = x.Status=="A"?"Active":"InActive" }).ToList();
            
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
                        Publication obj = new Publication();
                        obj.Id = db.usp_IDctr("Publications").SingleOrDefault().Value;
                        obj.Pub_Abreviation = txtabb.Text;
                        obj.Publication_Name = txtname.Text;
                        obj.In_House_Publication = chhouse.Checked;
                        obj.AMR_Comparision = chcom.Checked;
                        obj.Status = chstatus.Checked ? "A" : "I";
                        
                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
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

                        string usercexport = Request.Cookies["UsercExport"]?.Value;
                        bool cexport;
                        if (bool.TryParse(usercexport, out cexport))
                        {
                            obj.cExport = cexport;
                        }
                        else
                        {
                            obj.cExport = false;
                        }
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null; 

                        db.Publications.Add(obj);

                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Publication Created Successfully";
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
                        var obj = db.Publications.Where(x => x.Id == ID).SingleOrDefault();
                        obj.Id = ID;
                        obj.Pub_Abreviation = txtabb.Text;
                        obj.Publication_Name = txtname.Text;
                        obj.In_House_Publication = chhouse.Checked;
                        obj.AMR_Comparision = chcom.Checked;
                        obj.Status = chstatus.Checked ? "A" : "I";
                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                        //string usergroup = Request.Cookies["UserGroup"]?.Value;
                        //int grp;
                        //if (int.TryParse(usergroup, out grp))
                        //{
                        //    obj.Grp = (byte?)grp;
                        //}
                        //else
                        //{
                        //    throw new Exception("Failed to parse UserGroup from cookie.");
                        //}
                        obj.Grp = 0;
                        //string usercexport = Request.Cookies["UsercExport"]?.Value;
                        //bool cexport;
                        //if (bool.TryParse(usercexport, out cexport))
                        //{
                        //    obj.cExport = cexport;
                        //}
                        //else
                        //{
                        //    obj.cExport = false;
                        //}
                        obj.cExport = false;
                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date=currentDateTime;
                        obj.Rec_Edited_Time=currentDateTime+currentDateTime.TimeOfDay;

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
            txtabb.Text = string.Empty;
            txtname.Text = string.Empty;
            chhouse.Checked = false;
            chcom.Checked = false;
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
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.Publications.Where(x => x.Id==ID).SingleOrDefault();
            txtabb.Text = obj.Pub_Abreviation;
            txtname.Text = obj.Publication_Name;
            chhouse.Checked = obj.In_House_Publication;
            chcom.Checked = obj.AMR_Comparision;
            chstatus.Checked = obj.Status=="A";
            btnSave.Text = "Update";
        }
        //[WebMethod]
        //public static string OnSubmit(string id)
        //{
        //    string mess = "";
        //    ContactEntities db = new ContactEntities();
        //    int ID = Convert.ToInt32(id);
        //    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
        //    {
        //        try
        //        {
        //            var info = db.Publications.Find(ID);
        //            if (info != null)
        //            {
        //                db.Publications.Remove(info);
        //                db.SaveChanges();
        //                LogManagers.RecordID = ID;
        //                LogManagers.ActionOnForm = "Portal";
        //                LogManagers.ActionBy = ((UserInfo)HttpContext.Current.Session["UserObject"]).ID;//Convert.ToInt32(HttpContext.Current.Session["UserID"]);
        //                LogManagers.ActionOn = DateTime.Now;
        //                LogManagers.ActionTaken = "Delete";
        //                LogManagers.SetLog(db);
        //                scope.Complete();
        //                mess = "Ok";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            mess = ExceptionHandler.GetException(ex);
        //        }
        //    }
        //    return mess;
        //}
        
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            ImageButton deleteButton = (ImageButton)sender;
            int id = Convert.ToInt32(deleteButton.CommandArgument);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.Publications.SingleOrDefault(x => x.Id == id);
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

                        string usercexport = Request.Cookies["UsercExport"]?.Value;
                        bool cexport;
                        if (bool.TryParse(usercexport, out cexport))
                        {
                            record.cExport = cexport;
                        }
                        else
                        {
                            record.cExport = false;
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
        //protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Delete")
        //    {
        //        int id = Convert.ToInt32(e.CommandArgument);

        //        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
        //        {
        //            try
        //            {
        //                var record = db.Publications.SingleOrDefault(x => x.Id == id);
        //                if (record != null)
        //                {
        //                    record.Status = "I"; // Mark as Inactive
        //                    record.Rec_Edited_By = Request.Cookies["UserId"]?.Value;

        //                    var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
        //                    record.Rec_Edited_Date = currentDateTime.Date;
        //                    record.Rec_Edited_Time = currentDateTime;

        //                    db.SaveChanges();
        //                    scope.Complete();

        //                    lblmessage.Text = "Record marked as inactive successfully.";
        //                }
        //                else
        //                {
        //                    lblmessage.Text = "Record not found.";
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                lblmessage.Text = $"Error: {ex.Message}";
        //            }
        //        }

        //        // Rebind GridView to reflect changes
        //        BindGrid();
        //    }
        //}



    }
}