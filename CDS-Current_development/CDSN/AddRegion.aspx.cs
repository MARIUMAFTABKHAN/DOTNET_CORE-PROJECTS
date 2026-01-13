using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class AddRegion : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCountry();
            }
        }

        private void LoadCountry()
        {
            var s = db.tblCountries.Where (x=> x.active ==true).OrderBy (x=> x.CountryName).ToList();
        
            ddlcountry.DataTextField = "CountryName";
            ddlcountry.DataValueField = "CountryId";
            ddlcountry.DataSource = s;
            ddlcountry.DataBind();

            ddlcountry.Items.Insert(0, new ListItem("Select Country", ""));
        }
        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRegionGrid();
            txtregion.Text = string.Empty;
        }
        private void FillRegionGrid()
        {
            try
            {
                int id = Convert.ToInt32(ddlcountry.SelectedValue);
                var ds = (from u in db.tblRegions.Where (x=> x.CountryId == id && x.active == true
                          ).OrderBy(X => X.RegionName)
                          select new {u.RegionId , 
                              u.RegionName, 
                              Status = u.active == true ? "Active" : "InActive",
                              u.CountryId,
                              u.tblCountry.CountryName 
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
            txtregion.Text = String.Empty;
            chkActive.Checked = false;
            //ddlcountry.SelectedIndex = 0;
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

                        var id = db.usp_GetIDCTRCounter("tblRegion").SingleOrDefault().Value;
                        tblRegion obj = new tblRegion();
                        obj.RegionId = id;
                        obj.CountryId = Convert.ToInt32(ddlcountry.SelectedValue);
                        obj.RegionName = txtregion.Text;
                        obj.active = chkActive.Checked;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Added_By = userId; ;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edit_Date = null;
                        obj.Rec_Edit_By = null;

                        db.tblRegions.Add(obj);
                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(id), "Region", "Insert");

                        FillRegionGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Region Added Successfully";
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
                        var obj = db.tblRegions.Where(x => x.RegionId == id).SingleOrDefault();
                        obj.RegionName = txtregion.Text;
                        obj.active = chkActive.Checked;
                        obj.CountryId = Convert.ToInt32(ddlcountry.SelectedValue);

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(Convert.ToInt32(id), "Region", "Update");
                        FillRegionGrid();

                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Region Updated  Successfully";
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

            FillRegionGrid();
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var s = db.tblRegions.Where(x => x.RegionId == ID).SingleOrDefault();
            ddlcountry.SelectedValue = s.CountryId.ToString(); ;
            txtregion.Text = s.RegionName.ToString();
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
                lblMsg.Text = "Invalid Region ID.";
                return;
            }


           // int id = Convert.ToInt32(deletebutton.CommandArgument);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.tblRegions.SingleOrDefault(x => x.RegionId == id);
                    if (record != null)
                    {
                        record.active = false;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        record.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(id, "Region", "Delete");
                        FillRegionGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Region Deleted Successfully";
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