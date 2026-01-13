using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class AddChannel : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadChannelType();
                FillChannelGrid();
            }

        }

        private void LoadChannelType()
        {
            var s = db.tblChannelTypes.Where(x => x.active == true).OrderBy(x => x.ChannelType).ToList();
            ddltype.DataTextField = "ChannelType";
            ddltype.DataValueField = "ID";
            ddltype.DataSource = s;
            ddltype.DataBind(); 
        }

        private void FillChannelGrid()
        {
            try
            {
                int id = Convert.ToInt32(ddltype.SelectedValue);
                var ds = (from u in db.tblChannels.Where(x => x.TypeID == id).OrderBy(X => X.ChannelName).ToList()
                          select new { u.Id, u.ChannelName, u.IsActive, u.ShortName//, u.tblChannelType.ChannelType
                                                                                   }).ToList();

                gvRecords.DataSource = ds;
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

        private void ShowMsg(String txt)
        {
            lblMsg.Text = txt;
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Visible = true;
            lblMsg.Focus();
        }

        protected void ClearFields()
        {
            txtchannel.Text = string.Empty;
            txtshortname.Text = string.Empty;
            ddltype.SelectedIndex = 0;            
            chkActive.Checked = false;

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        var id = db.usp_GetIDCTRCounter("tblChannel").SingleOrDefault().Value;
                        tblChannel obj = new tblChannel();
                        obj.Id = Convert.ToInt32(id);
                        obj.TypeID = Convert.ToInt32(ddltype.SelectedValue);
                        obj.ShortName = txtshortname.Text;
                        obj.ChannelName = txtchannel.Text;
                        obj.IsActive = chkActive.Checked;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Added_By = userId; ;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edit_Date = null;
                        obj.Rec_Edit_By = null;

                        db.tblChannels.Add(obj);
                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(id), "Channel", "Insert");

                        FillChannelGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Channel Added Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ExceptionHandler.GetException(ex);
                    }
                }

            }
            else if (btnSave.Text == "Update")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        int id = Convert.ToInt32(ViewState["RecordID"]);
                        var obj = db.tblChannels.Where(x => x.Id == id).SingleOrDefault();
                        obj.TypeID = Convert.ToInt32(ddltype.SelectedValue);
                        obj.ShortName = txtshortname.Text;
                        obj.ChannelName = txtchannel.Text;
                        obj.IsActive = chkActive.Checked;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(id), "Channel", "Update");
                        FillChannelGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Channel Updated  Successfully";
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
            btnSave.Text = "Save";
        }

        protected void gvRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gvRecords.DataSource = dt;
            gvRecords.DataBind();
            gvRecords.PageIndex = e.NewPageIndex;

            FillChannelGrid();
        }
        
        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChannelGrid();
        }

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var s = db.tblChannels.Where(x => x.Id == ID).SingleOrDefault();
            ddltype.SelectedValue = s.TypeID.ToString();
            txtshortname.Text = s.ShortName.ToString();
            txtchannel.Text = s.ChannelName.ToString();
            chkActive.Checked = Convert.ToBoolean(s.IsActive);
            btnSave.Text = "Update";
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton deletebutton = (ImageButton)sender;
            int id = Convert.ToInt32(deletebutton.CommandArgument);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.tblChannels.SingleOrDefault(x => x.Id == id);
                    if (record != null)
                    {
                        record.IsActive = false;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        record.Rec_Edit_By = userId;

                        var currentDateTime = db
                        .Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(id, "Channel", "Delete");
                        FillChannelGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Channel Deleted  Successfully";
                    }
                }
                catch (Exception ex) { }
            }
        }
        private static void logmaintain(int id, string actiononform, string actiontaken)
        {
            using (CDSEntities db = new CDSEntities())
            {
                int userId = (int)HttpContext.Current.Session["userid"];

                if (userId != 0)
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