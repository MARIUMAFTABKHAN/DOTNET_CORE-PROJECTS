using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class AddDivisions : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTerr();
            }
        }

        private void LoadTerr()
        {
            var s = db.tblTerritories.Where(x => x.active == true).OrderBy(x => x.TerritoryName).ToList();
            ddlterr.DataTextField = "TerritoryName";
            ddlterr.DataValueField = "Id";
            ddlterr.DataSource = s;
            ddlterr.DataBind();

            ddlterr.Items.Insert(0, new ListItem("Select Territory", ""));
        }
        protected void ddlterr_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDivGrid();
            txtdiv.Text = string.Empty;
        }
        private void FillDivGrid()
        {
            try
            {
                int id = Convert.ToInt32(ddlterr.SelectedValue);

                var ds = (from u in db.TblDivisions.Where(x => x.TerritoryId== id && x.active==true).OrderBy(X => X.DivisionName)
                          select new { u.Id, 
                              u.DivisionName,
                              Status = u.active == true ? "Active" : "InActive",
                              u.TerritoryId,
                              u.tblTerritory.TerritoryName }).ToList();

                gvRecords.DataSource = ds;
                gvRecords.DataBind();

                if (ds.Count > 0)
                {
                    lblGrid.Text = "Records : " + ds.Count.ToString();
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
            txtdiv.Text = String.Empty;           
           // ddlterr.SelectedIndex = 0;
            chkActive.Checked = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
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
                        var id = db.usp_GetIDCTRCounter("tblDivision").SingleOrDefault().Value;
                        TblDivision obj = new TblDivision();
                        obj.Id = Convert.ToInt32(id);
                        obj.DivisionName = txtdiv.Text;
                        obj.active = chkActive.Checked;
                        obj.TerritoryId = Convert.ToInt32(ddlterr.SelectedValue);

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Added_By = userId; ;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edit_Date = null;
                        obj.Rec_Edit_By = null;

                        db.TblDivisions.Add(obj);

                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(id), "Division", "Insert");

                        FillDivGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Division Added Successfully";

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
                        var obj = db.TblDivisions.Where(x => x.Id == id).SingleOrDefault();
                        obj.DivisionName = txtdiv.Text;
                        obj.active = chkActive.Checked;
                        obj.TerritoryId = Convert.ToInt32(ddlterr.SelectedValue);

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edit_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(id), "Division", "Update");
                        FillDivGrid();

                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Division Updated  Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }
        }

        protected void gvRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gvRecords.DataSource = dt;
            gvRecords.DataBind();
            gvRecords.PageIndex = e.NewPageIndex;

            FillDivGrid();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var s = db.TblDivisions.Where(x => x.Id == ID).SingleOrDefault();
            ddlterr.SelectedValue = s.TerritoryId.ToString(); ;
            txtdiv.Text = s.DivisionName.ToString();
            chkActive.Checked = Convert.ToBoolean(s.active);
            btnSave.Text = "Update";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ImageButton deletebutton = (ImageButton)sender;

            // Try to parse the CommandArgument
            int id;
            if (!int.TryParse(deletebutton.CommandArgument, out id))
            {
                lblMsg.Text = "Invalid Division ID.";
                return;
            }

            // int id = Convert.ToInt32(deletebutton.CommandArgument);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.TblDivisions.SingleOrDefault(x => x.Id == id);
                    if (record != null)
                    {
                        record.active = false;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        record.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(id, "Division", "Delete");
                        FillDivGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Division Deleted Successfully";
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