using AjaxControlToolkit.Bundling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class AddArea : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCity();
                if (ddlCity.Items.Count > 0)
                {
                    FillAreaGrid();
                }
            }

        }

        private void LoadCity()
        {
            int? uid = Convert.ToInt32(Session["UserId"]);

            if (Convert.ToBoolean(Session["isadmin"]) == true)
            {
                uid = null;
                var ds = db.usp_CDSNGetCityByUserId(uid).ToList();
                ddlCity.DataTextField = "CityName";
                ddlCity.DataValueField = "Id";
                ddlCity.DataSource = ds;
                ddlCity.DataBind();
            }
            else
            {
                var ds = db.usp_CDSNGetCityByUserId(uid).ToList();
                ddlCity.DataTextField = "CityName";
                ddlCity.DataValueField = "Id";
                ddlCity.DataSource = ds;
                ddlCity.DataBind();
            }
        }
        
        private void FillAreaGrid()
        {
            try
            {
                int cityid = Convert.ToInt32(ddlCity.SelectedValue);
                var ds = db.tblAreas.Where(x => x.CityId == cityid).OrderBy(X => X.AreaName).Select(x => new
                        {
                            x.AreaId, x.AreaName,x.CityId,
                            Status=x.active==true?"Active":"InActive"
                        }).ToList();
                gvRecords.DataSource = ds;
                gvRecords.DataBind();

                if (ds.Count > 0)
                {
                    lblGrid.Text = "Total Records :" + ds.Count.ToString();
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
           // AreaId.Value = String.Empty;
            txtAreaName.Text = String.Empty;
            //ddlCity.SelectedIndex = 0;
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
                        var id = db.usp_GetIDCTRCounter("tblArea").SingleOrDefault().Value;
                        tblArea obj = new tblArea();
                        obj.AreaId = Convert.ToInt32(id);
                        obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                        obj.AreaName = txtAreaName.Text;
                        obj.active = chkActive.Checked;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Added_By = userId; ;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edit_Date = null;
                        obj.Rec_Edit_By = null;

                        db.tblAreas.Add(obj);
                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(id), "Area", "Insert");

                        FillAreaGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Area Added Successfully";
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
                        var obj = db.tblAreas.Where(x => x.AreaId == id).SingleOrDefault();
                        obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                        obj.AreaName = txtAreaName.Text;
                        obj.active = chkActive.Checked;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(id), "Area", "Update");
                        FillAreaGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Area Updated  Successfully";
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

            FillAreaGrid();
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillAreaGrid();

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var s = db.tblAreas.Where(x => x.AreaId == ID).SingleOrDefault();
            ddlCity.SelectedValue = s.CityId.ToString();
            txtAreaName.Text = s.AreaName.ToString();
            chkActive.Checked = Convert.ToBoolean(s.active);
            btnSave.Text = "Update";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ImageButton deletebutton = (ImageButton)sender;
            int id = Convert.ToInt32(deletebutton.CommandArgument);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.tblAreas.SingleOrDefault(x => x.AreaId == id);
                    if (record != null)
                    {
                        record.active = false;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        record.Rec_Edit_By = userId;

                        var currentDateTime = db
                        .Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(id, "Area", "Delete");
                        FillAreaGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Area Deleted  Successfully";
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