using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class TerritoryManager : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateRegion();
            }
        }
        private void PopulateRegion()
        {
            var p = db.tblRegions.Where(x => x.active == true).ToList();
            
            ddlRegion.DataSource = p;
            ddlRegion.DataValueField = "RegionId";
            ddlRegion.DataTextField = "RegionName";
            ddlRegion.DataBind();
            
            ddlRegion.Items.Insert(0, new ListItem("Select Region",""));
        }
        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTerritoryGrid();
            txtTerritoryName.Text=string.Empty;
            txtShortName.Text = string.Empty;
        }
        private void FillTerritoryGrid()
        {
            try
            {
                int id = Convert.ToInt32(ddlRegion.SelectedValue);

                var t = (from u in db.tblTerritories.Where(x => x.RegionId == id && x.active==true)
                         select new { u.Id, 
                             u.tblRegion.RegionName, 
                             u.tblRegion.RegionId, 
                             u.TerritoryName, 
                             u.ShortNames, 
                             Status=u.active==true?"Active":"InActive"
                         }).ToList();

                DataTable dt = Helper.ToDataTable(t);
                ViewState["dt"] = dt;

                gvRecords.DataSource = dt;
                gvRecords.DataBind();

                lblGrid.Text = "Total Records :" + dt.Rows.Count.ToString();
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
            //DivisionId.Value = string.Empty;
            txtTerritoryName.Text = string.Empty;
            txtShortName.Text = string.Empty;
            ddlRegion.SelectedIndex = 0;
            chk.Checked = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if ((txtTerritoryName.Text.Length > 0))
            {
                if (btnSave.Text == "Save")
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            var id = db.usp_GetIDCTRCounter("tblTerritory").SingleOrDefault().Value;
                            tblTerritory obj = new tblTerritory();
                            obj.Id = Convert.ToInt32(id);
                            obj.RegionId = Convert.ToInt32(ddlRegion.SelectedValue);
                            obj.TerritoryName = txtTerritoryName.Text;
                            obj.ShortNames = txtShortName.Text;
                            obj.active = chk.Checked;

                            int userId = (int)HttpContext.Current.Session["userid"];
                            obj.Rec_Added_By = userId; ;

                            var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                            obj.Rec_Added_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                            obj.Rec_Edit_Date = null;
                            obj.Rec_Edit_By = null;

                            db.tblTerritories.Add(obj);
                            db.SaveChanges();
                            logmaintain(Convert.ToInt32(id), "Territory", "Insert");

                            FillTerritoryGrid();
                            scope.Complete();
                            btnCancel_Click(null, null);
                            lblMsg.Text = "Territory Added Successfully";
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
                            var obj = db.tblTerritories.Where(x => x.Id == id).SingleOrDefault();
                            obj.RegionId = Convert.ToInt32(ddlRegion.SelectedValue);
                            obj.TerritoryName = txtTerritoryName.Text;
                            obj.ShortNames = txtShortName.Text;
                            obj.active = chk.Checked;

                            int userId = (int)HttpContext.Current.Session["userid"];
                            obj.Rec_Edit_By = userId;

                            var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                            obj.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                            db.SaveChanges();
                            logmaintain(Convert.ToInt32(id), "Territory", "Update");
                            FillTerritoryGrid();
                            scope.Complete();
                            btnCancel_Click(null, null);
                            lblMsg.Text = "Territory Updated  Successfully";
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = ExceptionHandler.GetException(ex);
                        }
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

            FillTerritoryGrid();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var s = db.tblTerritories.Where(x => x.Id == ID).SingleOrDefault();
            ddlRegion.SelectedValue = s.RegionId.ToString(); ;
            txtTerritoryName.Text = s.TerritoryName.ToString();
            txtShortName.Text = s.ShortNames;
            chk.Checked = Convert.ToBoolean( s.active);
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
                    var record = db.tblTerritories.SingleOrDefault(x => x.Id == id);
                    if (record != null)
                    {
                        record.active = false;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        record.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(id, "Territory", "Delete");
                        FillTerritoryGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Territory Deleted  Successfully";
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
