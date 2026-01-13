using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using System.Web.Http.Results;

namespace CDSN
{
    public partial class AddOperatorInfo : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCountry();
                txtwef.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }

        private void LoadCountry()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            var dt = (from u in db.tblusercountries.Where(x => x.tblCountry.active == true && x.UserId == UserId)
                      select new { u.CountryId, u.tblCountry.CountryName }).OrderBy(x => x.CountryName).ToList();

            if (dt.Count > 0)
            {
                ddlCountry.DataSource = dt;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, new ListItem("Select Country", "0"));

        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRegion();
        }

        private void LoadRegion()
        {

            int CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
            var dt = (from u in db.tblRegions.Where(x => x.CountryId == CountryId && x.active == true)
                      select new { u.RegionId, u.RegionName }).OrderBy(x => x.RegionName).ToList();

            ddlRegion.DataSource = dt;
            ddlRegion.DataTextField = "RegionName";
            ddlRegion.DataValueField = "RegionId";
            ddlRegion.DataBind();

            ddlRegion.Items.Insert(0, new ListItem("Select Region", "0"));

            if (dt.Count() == 0)
            {
                ddlRegion.Items.Insert(0, new ListItem { Text = "select me", Value = "0" });
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTerritory();
        }
        
        private void LoadTerritory()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            //int RegionId = Convert.ToInt32(ddlRegion.SelectedValue);

            var tt = (from u in db.tblUserTerritories.Where(x => x.UserId == UserId && x.tblTerritory.active == true)
                      select new { u.TerritoryId, u.tblTerritory.TerritoryName }).OrderBy(x => x.TerritoryName).ToList();
            ddlTerritory.DataSource = tt;
            ddlTerritory.DataTextField = "TerritoryName";
            ddlTerritory.DataValueField = "TerritoryId";
            ddlTerritory.DataBind();

            ddlTerritory.Items.Insert(0, new ListItem("Select Territory", "0"));

            if (tt.Count() == 0)
            {
                ddlTerritory.Items.Insert(0, new ListItem { Text = "select me", Value = "0" });
            }

        }

        protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDivision();
        }

        private void LoadDivision()
        {
            int ID = Convert.ToInt32(ddlTerritory.SelectedValue);
            var dtDivision = (from u in db.TblDivisions.Where(x => x.tblTerritory.Id == ID)
                              select new { u.Id, u.DivisionName }).OrderBy(x => x.DivisionName).ToList(); ;

            if (dtDivision.Count() == 0)
            {
                ddlDivision.Items.Insert(0, new ListItem { Text = "select me", Value = "0" });
            }
            ddlDivision.DataTextField = "DivisionName";
            ddlDivision.DataValueField = "Id";
            ddlDivision.DataSource = dtDivision;
            ddlDivision.DataBind();

            ddlDivision.Items.Insert(0, new ListItem("Select District", "0"));

        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCity();
        }

        private void LoadCity()
        {
            int id = Convert.ToInt32(ddlDivision.SelectedValue);

            var dtCity = db.tblCities.Where(x => x.DivisionId == id).OrderBy(x => x.CityName).ToList();

            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "Id";
            ddlCity.DataSource = dtCity;
            ddlCity.DataBind();

            ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
            if (dtCity.Count() == 0)
            {
                ddlCity.Items.Insert(0, new ListItem { Text = "select me", Value = "0" });
            }

        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOperators();
            LoadArea();
        }

        private void LoadOperators()
        {
            int ID = Convert.ToInt32(ddlCity.SelectedValue);

            var dt = db.tblOperators.Where(x => x.CityId == ID).ToList();
            ddlOp.DataTextField = "Name";
            ddlOp.DataValueField = "Id";
            ddlOp.DataSource = dt;
            ddlOp.DataBind();

            ddlOp.Items.Insert(0, new ListItem("Select Operator", "0"));

            if (dt.Count() == 0)
            {
                ddlOp.Items.Insert(0, new ListItem { Text = "select me", Value = "0" });
            }

        }

        protected void ddlOp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlOp.SelectedValue);
            var counter = db.tblChannelPositions.Where(x => x.ChannelId == id && x.CurPosition > 0).Count();
            txtChNo.Text = counter.ToString();
        }

        private void LoadArea()
        {
            int id = Convert.ToInt32(ddlCity.SelectedValue);

            var dtArea = db.tblAreas.Where(x => x.CityId == id).OrderBy(x => x.CityId == id && x.active == true).ToList();
            ddlArea.DataSource = dtArea;
            ddlArea.DataTextField = "AreaName";
            ddlArea.DataValueField = "AreaId";
            ddlArea.DataBind();

            ddlArea.Items.Insert(0, new ListItem("Select Area", "0"));

            if (dtArea.Count() == 0)
            {
                ddlArea.Items.Insert(0, new ListItem { Text = "select me", Value = "0" });
            }
        }
        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubArea();
        }
        private void LoadSubArea()
        {
            int id = Convert.ToInt32(ddlArea.SelectedValue);

            var dtSubArea = db.tblSubAreas.Where(x => x.AreaId == id).OrderBy(x => x.SubAreaName).ToList();
            ddlSubArea.DataSource = dtSubArea;
            ddlSubArea.DataTextField = "SubAreaName";
            ddlSubArea.DataValueField = "SubAreaId";
            ddlSubArea.DataBind();

            ddlSubArea.Items.Insert(0, new ListItem("Select sub Area", "0"));

            if (dtSubArea.Count() == 0)
            {
                ddlSubArea.Items.Insert(0, new ListItem { Text = "select me", Value = "0" });
            }

        }

        protected void ddlSubArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOperatorInfoGrid();
        }

        private void FillOperatorInfoGrid()
        {
            try
            {
                int id = Convert.ToInt32(ddlDivision.SelectedValue);
                var data = db.sp_GetCDSNOperatorInfoN1(id, 0).OrderBy(x => x.Name).Select(x => new
                {
                    x.ID,
                    x.OperatorId,
                    x.Name,
                    x.AreaId,
                    x.AreaName,
                    x.SubAreaId,
                    x.SubAreaName,
                    x.TerritoryId,
                    x.TerritoryName,
                    x.Subscribers,
                    x.WEF,
                    x.DivisionId,
                    x.DivisionName
                }).ToList();

                DataTable dt = Helper.ToDataTable(data);
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (chkcontrol())
            {

                using (CDSEntities db = new CDSEntities())
                {
                    if (btnSave.Text == "Save")
                    {
                        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                        {
                            try
                            {

                                tblOperatorInfo obj = new tblOperatorInfo();
                                var id = Convert.ToInt32(db.usp_GetIDCTRCounter("tblOptrInfo").SingleOrDefault().Value);

                                obj.ID = id;
                                obj.OperatorId = Convert.ToInt32(ddlOp.SelectedValue);
                                obj.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                                obj.RegionId = Convert.ToInt32(ddlRegion.SelectedValue);
                                obj.TerritoryId = Convert.ToInt32(ddlTerritory.SelectedValue); 
                                obj.DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
                                obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                                obj.AreaId = Convert.ToInt32(ddlArea.SelectedValue);
                                obj.SubAreaId = Convert.ToInt32(ddlSubArea.SelectedValue);
                                obj.Subscribers = 0;// Convert.ToInt32(txtsubscriber.Text);
                                obj.WEF = Helper.SetDateFormat(txtwef.Text);

                                int userId = (int)HttpContext.Current.Session["userid"];
                                obj.Rec_Added_By = userId; ;

                                var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                                obj.Rec_Added_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                                obj.Rec_Edit_Date = null;
                                obj.Rec_Edit_By = null;

                                db.tblOperatorInfoes.Add(obj);
                                db.SaveChanges();

                                logmaintain(id, "Operator Info", "Insert");

                                FillOperatorInfoGrid();
                                scope.Complete();
                                btnCancel_Click(null, null);
                                lblMsg.Text = "Operator Info Added Successfully";
                            }
                            catch (Exception ex)
                            {
                                lblException.Visible = true;
                                lblException.ForeColor = System.Drawing.Color.Red;
                                lblException.Text = ex.Message;
                                lblException.Focus();
                            }
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                        {
                            try
                            {
                                Int32 id = Convert.ToInt32(ViewState["RecordID"]);
                                var obj = db.tblOperatorInfoes.Where(x => x.OperatorId == id).SingleOrDefault();
                                obj.OperatorId = Convert.ToInt32(ddlOp.SelectedValue);
                                obj.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                                obj.RegionId = Convert.ToInt32(ddlRegion.SelectedValue);
                                obj.TerritoryId = Convert.ToInt32(ddlTerritory.SelectedValue);
                                obj.DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
                                obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                                obj.AreaId = Convert.ToInt32(ddlArea.SelectedValue);
                                obj.SubAreaId = Convert.ToInt32(ddlSubArea.SelectedValue);
                                obj.Subscribers = 0;// Convert.ToInt32(txtsubscriber.Text);
                                obj.WEF = Helper.SetDateFormat(txtwef.Text);

                                int userId = (int)HttpContext.Current.Session["userid"];
                                obj.Rec_Edit_By = userId;

                                var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                                obj.Rec_Edit_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                                db.SaveChanges();
                                logmaintain(id, "Operator Info", "Update");
                                FillOperatorInfoGrid();

                                scope.Complete();
                                btnCancel_Click(null, null);
                                lblMsg.Text = "Operator Info Updated  Successfully";
                            }
                            catch (Exception ex)
                            {
                                lblException.Visible = true;
                                lblException.ForeColor = System.Drawing.Color.Red;
                                lblException.Text = ex.Message;
                                lblException.Text = ex.Message;
                                lblException.Focus();
                            }
                        }

                    }
                }
            }
        }

        private Boolean chkcontrol()
        {
            Boolean result = false;
            if (
                Convert.ToInt32(ddlCountry.SelectedValue) > 0 &&
                Convert.ToInt32(ddlRegion.SelectedValue) > 0 &&
                Convert.ToInt32(ddlTerritory.SelectedValue) > 0 &&
                Convert.ToInt32(ddlDivision.SelectedValue) > 0 &&
                Convert.ToInt32(ddlCity.SelectedValue) > 0 &&
                Convert.ToInt32(ddlOp.SelectedValue) > 0 &&
                Convert.ToInt32(ddlArea.SelectedValue) > 0 &&
                Convert.ToInt32(ddlSubArea.SelectedValue) > 0)
            {
                result = true;
            }
            return result;
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
            txtwef.Text = string.Empty;
            txtwef.Text = DateTime.Now.ToString("dd-MM-yyyy");
            ddlCountry.SelectedIndex = 0;
            ddlRegion.SelectedIndex = 0;
            ddlTerritory.SelectedIndex = 0;
            ddlDivision.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlOp.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            ddlSubArea.SelectedIndex = 0;
        }
        
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            btnSave.Text = "Save";
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var s = db.tblOperatorInfoes.Where(x => x.ID == ID).SingleOrDefault();

            if (s != null)
            {
                LoadCountry();
                ddlCountry.SelectedValue = s.CountryId.ToString();
                LoadRegion();
                ddlRegion.SelectedValue = s.RegionId.ToString();
                LoadTerritory();
                ddlTerritory.SelectedValue = s.TerritoryId.ToString();
                LoadDivision();
                ddlDivision.SelectedValue = s.DivisionId.ToString();
                LoadCity();
                ddlCity.SelectedValue = s.CityId.ToString();
                LoadOperators();
                ddlOp.SelectedValue = s.OperatorId.ToString();
                LoadArea();
                ddlArea.SelectedValue = s.AreaId.ToString();
                LoadSubArea();
                ddlSubArea.SelectedValue = s.SubAreaId.ToString();

                txtwef.Text = s.WEF.ToString();

                btnSave.Text = "Update";
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

        protected void gvRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gvRecords.DataSource = dt;
            gvRecords.DataBind();
            gvRecords.PageIndex = e.NewPageIndex;

        }
    }
}