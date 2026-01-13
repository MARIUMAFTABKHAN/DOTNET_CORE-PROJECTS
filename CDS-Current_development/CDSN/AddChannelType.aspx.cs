using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease;

namespace CDSN
{
    public partial class AddChannelType : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillAreaGrid();
            }
        }

        private void FillAreaGrid()
        {
            try
            {
                var ds = db.tblChannelTypes.Where(x=>x.active==true).
                    OrderBy(x => x.ChannelType).Select(x => new
                    {
                        x.ID,
                        x.ChannelType,
                        Status = x.active == true ? "Active" : "InActive"
                    }).ToList();
                    
                DataTable dt=Helper.ToDataTable(ds);
                ViewState["dt"] = dt;
                gvRecords.DataSource = dt;
                gvRecords.DataBind();

                if (ds.Count > 0)
                {
                    lblGrid.Text = "Total Records : " + ds.Count.ToString();
                }
                else
                {
                    lblGrid.Text = "Records not found ";
                }
            }
            catch (Exception ex)
            {
                lblException.Visible = true;
                lblException.ForeColor = System.Drawing.Color.Red;
                lblException.Text = ex.Message;
                lblException.Focus();
            }
        }

        protected void ClearFields()
        {
            //AreaId.Value = string.Empty;
            txtCountry.Text = string.Empty;
            chkActive.Checked = false;
            lblMsg.Text = string.Empty;
            btnSave.Text = "Save";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        var id = db.usp_GetIDCTRCounter("channeltype").SingleOrDefault().Value;
                        tblChannelType obj = new tblChannelType();
                        obj.ID = Convert.ToInt32(id);
                        obj.ChannelType = txtCountry.Text;
                        obj.active = chkActive.Checked;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Added_By = userId; ;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edit_Date = null;
                        obj.Rec_Edit_By = null;

                        db.tblChannelTypes.Add(obj);
                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(id), "ChannelType", "Insert");
                        
                        FillAreaGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Channel Type Added Successfully";
                    }
                    catch(Exception ex)
                    {
                        lblMsg.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }
            else
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        int id = Convert.ToInt32(ViewState["RecordID"]);
                        var obj = db.tblChannelTypes.Where(x => x.ID == id).SingleOrDefault();
                        obj.ChannelType = txtCountry.Text;
                        obj.active = chkActive.Checked;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(id), "ChannelType", "Update");
                        FillAreaGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Channel Type Updated  Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ExceptionHandler.GetException(ex);
                    }
                }
                        
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void gvRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gvRecords.DataSource = dt;
            gvRecords.DataBind();
            gvRecords.PageIndex = e.NewPageIndex;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imageButton = (ImageButton)sender;
                GridViewRow myrow = (GridViewRow)imageButton.Parent.Parent;
                Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myrow.RowIndex].Value.ToString());
                ViewState["RecordID"] = ID;
                var s = db.tblChannelTypes.Where(x => x.ID == ID).SingleOrDefault();
                txtCountry.Text = s.ChannelType;
                chkActive.Checked = Convert.ToBoolean(s.active);
                btnSave.Text = "Update";

            }
            catch (Exception ex)
            {
                lblException.Visible = true;
                lblException.ForeColor = System.Drawing.Color.Red;
                lblException.Text = ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ImageButton deletebutton=(ImageButton)sender;
            int id = Convert.ToInt32(deletebutton.CommandArgument);

            using(System.Transactions.TransactionScope scope=new System.Transactions.TransactionScope())
            {
                try
                {
                    var record=db.tblChannelTypes.SingleOrDefault(x=>x.ID == id);
                    if(record != null)
                    {
                        record.active = false;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        record.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(id, "ChannelType", "Delete");
                        FillAreaGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Channel Type Deleted  Successfully";
                    }
                }
                catch(Exception ex) { }
            }
        }

        //[WebMethod]
        //public static string OnSubmit(string id)
        //{
        //    string mess = "";
        //    CDSEntities db = new CDSEntities();
        //    int ID = Convert.ToInt32(id);
        //    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
        //    {
        //        try
        //        {
        //            var info = db.tblChannelTypes.Find(ID);
        //            if (info != null)
        //            {
        //                info.active = false;
        //                //db.tblChannelTypes.Remove(info);
        //                db.SaveChanges();
        //                logmaintain(ID,"ChannelType","Delete");
                        
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
        private static void logmaintain(int id,string actiononform,string actiontaken)
        {
            using(CDSEntities db = new CDSEntities())
            {
                int userId = (int)HttpContext.Current.Session["userid"];
                
                if(userId != 0)
                {
                    clsLogManager.RecordID = id;
                    clsLogManager.ActionOnForm = actiononform;
                    clsLogManager.ActionBy = userId;
                    clsLogManager.ActionOn = DateTime.Now;
                    clsLogManager.ActionTaken = actiontaken;
                    clsLogManager.SetLog(db);
                }
                else
                {
                    throw new Exception("User info not found for the given session user ID.");
                }
            }
        }
    }
}