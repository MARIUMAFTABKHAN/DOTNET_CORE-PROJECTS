using AjaxControlToolkit.Bundling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace CDSN
{
    public partial class AddSubArea : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtlats.Text = "0,0";
                FillSegment();
                FillTerritory();
            }
        }

        private void FillSegment()
        {
            var ds = db.tblSegments.ToList();

            ddlSeqment.DataTextField = "Segment_FullName";
            ddlSeqment.DataValueField = "Segment_ShortName";
            ddlSeqment.DataSource = ds;
            ddlSeqment.DataBind();

            ddlSeqment.Items.Insert(0, new ListItem("Select Segment", ""));
        }

        private void FillTerritory()
        {
            //ddlTerritory.Items.Clear();
            int uid = Convert.ToInt32(Session["UserID"].ToString());
            var s = (from u in db.tblUserTerritories.Where(x => x.UserId == uid).OrderBy(x => x.tblTerritory.TerritoryName)
                     select new { u.TerritoryId, u.tblTerritory.TerritoryName }).Distinct().ToList();

            ddlTerritory.DataValueField = "TerritoryId";
            ddlTerritory.DataTextField = "TerritoryName";
            ddlTerritory.DataSource = s;
            ddlTerritory.DataBind();
            //ddlTerritory_SelectedIndexChanged(sender, e);
            ddlTerritory.Items.Insert(0, new ListItem("Select Terrirtory", ""));
        }

        protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDistrict();
        }

        private void FillDistrict()
        {
            int id = Convert.ToInt32(ddlTerritory.SelectedValue);
            
            try
            {
                var ds = db.TblDivisions.Where(x => x.TerritoryId == id && x.active == true).OrderBy(x => x.DivisionName).ToList();
                
                ddlDistrict.DataTextField = "DivisionName";
                ddlDistrict.DataValueField = "id";
                ddlDistrict.DataSource = ds;
                ddlDistrict.DataBind();

                ddlDistrict.Items.Insert(0, new ListItem("Select District", ""));
            }
            catch (Exception)
            {
                ddlDistrict.Items.Insert(0, new ListItem("Select District", "0"));
                ddlDistrict_SelectedIndexChanged(null, null);
            }

        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCity();
        }

        private void FillCity()
        {
            if (ddlDistrict.Items.Count > 0)
            {
                int divisionid = Convert.ToInt32(ddlDistrict.SelectedValue);

                var dtCity = db.tblCities.Where(x => x.DivisionId == divisionid).ToList();
                if (dtCity.Count > 0)
                {
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataValueField = "Id";
                    ddlCity.DataSource = dtCity;
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("Select City", ""));
                }
                else
                {
                    ddlCity.Items.Clear();
                    ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
                }

            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillArea();
        }

        private void FillArea()
        {
            try
            {
                int id = Convert.ToInt32(ddlCity.SelectedValue);
                var ar = db.tblAreas.Where(x => x.CityId == id && x.active==true).ToList();
                ddlArea.DataTextField = "AreaName";
                ddlArea.DataValueField = "Areaid";
                ddlArea.DataSource = ar;
                ddlArea.DataBind();

                ddlArea.Items.Insert(0, new ListItem("Select Area", ""));
                
            }
            catch (Exception)
            {

                ddlArea.Items.Insert(0, new ListItem("Select Area", "0"));
                ddlArea_SelectedIndexChanged(null, null);
            }


        }
       
        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubAreaGrid();
        }

        private void FillSubAreaGrid()
        {
            try
            {
                int areaid = 0;
                if (ddlArea.Items.Count > 0) 
                {
                    areaid = Convert.ToInt32(ddlArea.SelectedValue);
                    var dt = db.tblSubAreas.Where(x => x.tblArea.AreaId == areaid).ToList();
                    gvRecords.DataSource = dt;
                    gvRecords.DataBind();
                    lblGrid.Text = "Records: " + dt.Count.ToString();
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
            SubAreaId.Value = "0";
            txtSubAreaName.Text = string.Empty;
            ddlSeqment.SelectedIndex = 0;
            ddlTerritory.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            //ddlTerritory_SelectedIndexChanged(null, null);
            txtSubAreaName.Text = string.Empty;
           // txtlats.Text = "0,0";
            txtSubcribers.Text = "0";
            //gvRecords.DataSource=null; 
            //gvRecords.DataBind();
            //lblGrid.Visible= false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string lat = "0";
            string lng = "0";
            txtlats.Text = "0,0";
            if ((txtlats.Text.Trim().Length > 0) && (IsAddressOk(ref lat, ref lng)))
            {
                if (btnSave.Text == "Save")
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            int nSubAreaId = db.usp_GetIDCTRCounter("tblSubArea").SingleOrDefault().Value;
                            tblSubArea obj = new tblSubArea();
                            obj.SubAreaId = Convert.ToInt32(nSubAreaId);
                            obj.SubAreaName = txtSubAreaName.Text;
                            obj.AreaId = Convert.ToInt32(ddlArea.SelectedValue);
                            obj.Subscribers = Convert.ToInt32(txtSubcribers.Text);
                            obj.Segment = ddlSeqment.SelectedValue;

                            int userId = (int)HttpContext.Current.Session["userid"];
                            obj.Rec_Added_By = userId; ;

                            var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                            obj.Rec_Added_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                            obj.Rec_Edit_Date = null;
                            obj.Rec_Edit_By = null;

                            db.tblSubAreas.Add(obj);
                            db.SaveChanges();
                            logmaintain(Convert.ToInt32(nSubAreaId), "Sub Area", "Insert");

                            FillSubAreaGrid();
                            scope.Complete();
                            btnCancel_Click(null, null);
                            lblMsg.Text = "Sub Area Added Successfully";
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
                            int nSubAreaId = Convert.ToInt32(ViewState["RecordID"]);
                            var obj = db.tblSubAreas.Where(x => x.SubAreaId == nSubAreaId).SingleOrDefault(); ;

                            obj.SubAreaName = txtSubAreaName.Text;
                            obj.AreaId = Convert.ToInt32(ddlArea.SelectedValue);
                            obj.Subscribers = Convert.ToInt32(txtSubcribers.Text);
                            obj.Segment = ddlSeqment.SelectedValue;

                            int userId = (int)HttpContext.Current.Session["userid"];
                            obj.Rec_Edit_By = userId;

                            var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                            obj.Rec_Edit_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                            db.SaveChanges();
                            logmaintain(Convert.ToInt32(nSubAreaId), "Sub Area", "Update");
                            FillSubAreaGrid();

                            scope.Complete();
                            btnCancel_Click(null, null);
                            lblMsg.Text = "Sub Area Updated  Successfully";
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
            }
            else
            {
                lblMsg.Text = "Please Copy  Lat/Lng Coordinate and paste in respective Text Box ";
            }

        }

        private bool IsAddressOk(ref string lat, ref string lng)
        {

            string[] str = txtlats.Text.Split(',');
            if (str.Length == 2)
            {
                lat = Convert.ToString(str[0]);
                lng = Convert.ToString(str[1]);
                return true;

            }
            else
            {
                return false;
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

            FillSubAreaGrid();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var s = db.tblSubAreas.Where(x => x.SubAreaId == ID).SingleOrDefault();
            ddlSeqment.SelectedValue = s.Segment.ToString();
            ddlArea.SelectedValue = s.AreaId.ToString();
            txtSubAreaName.Text = s.SubAreaName.ToString();
            txtSubcribers.Text = s.Subscribers.ToString();
            
            btnSave.Text = "Update";
        }

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    ImageButton deletebutton = (ImageButton)sender;

        //    // Try to parse the CommandArgument
        //    int id;
        //    if (!int.TryParse(deletebutton.CommandArgument, out id))
        //    {
        //        lblMsg.Text = "Invalid City ID.";
        //        return;
        //    }
        //    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
        //    {
        //        try
        //        {
        //            var record = db.tblSubAreas.SingleOrDefault(x => x.SubAreaId == id);
        //            if (record != null)
        //            {
        //                record.active = false;

        //                int userId = (int)HttpContext.Current.Session["userid"];
        //                record.Rec_Edit_By = userId;

        //                var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
        //                record.EditOn = currentDateTime + currentDateTime.TimeOfDay;

        //                db.SaveChanges();
        //                logmaintain(id, "Operator", "Delete");
        //                FillOperatorGrid();
        //                scope.Complete();
        //                btnCancel_Click(null, null);
        //                lblMsg.Text = "Operator Deleted Successfully";
        //            }
        //        }
        //        catch (Exception ex) { }
        //    }
        //}

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