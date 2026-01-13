using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace CDSN
{
    public partial class OperatorIncestives : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtwef.Text = DateTime.Now.ToString("dd-MM-yyyy");
                FillTerritory();
                FillIncentiveType();
            }
        }

        private void FillTerritory()
        {
            Int32 uid = Convert.ToInt32(Convert.ToInt32(Session["UserId"]));
            var t = (from u in db.tblUserTerritories.Where(x => x.UserId == uid && x.tblTerritory.active == true)
                     select new { u.TerritoryId , u.tblTerritory.TerritoryName }).Distinct().OrderBy(x => x.TerritoryName).ToList();
            ddlterritory.DataValueField = "TerritoryId";
            ddlterritory.DataTextField = "TerritoryName";
            ddlterritory.DataSource = t;
            ddlterritory.DataBind();

            ddlterritory.Items.Insert(0, new ListItem("Select Territory", "0"));
        }
        protected void ddlterritory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDistrict();
        }

        private void FillDistrict()
        {
            Int32 uid = Convert.ToInt32(ddlterritory.SelectedValue);
            var ds = db.TblDivisions.Where(x => x.active == true && x.TerritoryId == uid).ToList();
            ddlDistricts.DataTextField = "DivisionName";
            ddlDistricts.DataValueField = "id";
            ddlDistricts.DataSource = ds;
            ddlDistricts.DataBind();

            ddlDistricts.Items.Insert(0, new ListItem("Select District", "0"));

            if (ds.Count == 0)
            {
                ddlDistricts.Items.Insert(0, new ListItem("Select District", "0"));
            }
        }
        protected void ddlDistricts_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCity();
        }

        private void FillCity()
        {
            Int32 uid = Convert.ToInt32(ddlDistricts.SelectedValue);
            var ds = db.tblCities.Where(x => x.DivisionId == uid && x.active == true).ToList();
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "id";
            ddlCity.DataSource = ds;
            ddlCity.DataBind();

            ddlCity.Items.Insert(0, new ListItem("Select City", "0"));

            if (ds.Count == 0)
            {
                ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
            }

        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOperators();
        }
        private void LoadOperators()
        {
            Int32 cityid = Convert.ToInt32(ddlCity.SelectedValue);
            
            var ds= (from u in db.tblOperators.Where(x => x.CityId == cityid && x.active == true)
                     select new
                     {
                         u.Id,
                         u.Name
                     }).ToList();

            ddlOp.DataSource = ds;
            ddlOp.DataTextField = "Name";
            ddlOp.DataValueField = "Id";
            ddlOp.DataBind();

            ddlOp.Items.Insert(0, new ListItem("Select Operator", "0"));

            if (ds.Count == 0)
            {
                ddlOp.Items.Insert(0, new ListItem("Select me", "0"));
            }
        }

        protected void ddlOp_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOpIncGrid();
        }

        private void FillIncentiveType()
        {
            var ds = db.tblIncentiveTypes.Where(x => x.IsActive == true).ToList();

            ddlIncentype.DataTextField = "Incentivetype";
            ddlIncentype.DataValueField = "ID";
            ddlIncentype.DataSource = ds;
            ddlIncentype.DataBind();

            ddlIncentype.Items.Insert(0, new ListItem("Select Incentive Type", ""));

            if (ds.Count == 0)
            {
                ddlIncentype.Items.Insert(0, new ListItem("Select Incentive Type", "0"));
            }
        }

        protected void ddlIncentype_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillIncentypeDetails();
        }

        private void FillIncentypeDetails()
        {
            Int32 incetivetype_id = Convert.ToInt32(ddlIncentype.SelectedValue);

            var ds = db.tblIncentiveDetailTypes.Where(x=>x.IncentiveTypeId==incetivetype_id).ToList();
            
            ddlIncentiveDetailType.DataTextField = "IncentiveDetailType";
            ddlIncentiveDetailType.DataValueField = "Id";
            ddlIncentiveDetailType.DataSource = ds;
            ddlIncentiveDetailType.DataBind();

            ddlIncentiveDetailType.Items.Insert(0, new ListItem("Select Incentive Type Detail", "0"));

            if (ds.Count == 0)
            {
                ddlIncentiveDetailType.Items.Insert(0, new ListItem("Select Incentive Type Detail", "0"));
            }
            
        }

        private void ShowMsg(String txt)
        {
            lblMsg.Text = txt;
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Visible = true;
            lblMsg.Focus();
        }

        private void FillOpIncGrid()
        {
            Int32 dt = Convert.ToInt32(ddlOp.SelectedValue);
            var gv = (from u in db.tblOpIncentives.Where(x => x.OperatorId == dt)
                      select new { u.Id, 
                          u.ItemInfo, 
                          u.OperatorId,
                          OperatoName = u.tblOperator.Name, 
                          u.IncentiveTypeId,
                          IncentiveTypename = u.tblIncentiveDetailType.tblIncentiveType.IncentiveType,
                          u.IncentiveDetailId,
                          IncentiveDetailTypename=u.tblIncentiveDetailType.IncentiveDetailType,
                          u.DateIncentive, 
                          status=u.active==true?"Active":"InActive" 
                      }).ToList()
                    .ToList();

            DataTable dt1 = Helper.ToDataTable(gv);
            ViewState["dt1"] = dt1;


            gvRecords.DataSource = dt1;
            gvRecords.DataBind();

            lblGrid.Text = "Total Records :" + dt1.Rows.Count.ToString();
        }

        protected void ClearFields()
        {
            IncentiveId.Value = String.Empty;
            ddlterritory.SelectedIndex = 0;
            ddlDistricts.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlOp.SelectedIndex = 0;
            ddlIncentype.SelectedIndex = 0;
            ddlIncentiveDetailType.SelectedIndex = 0;
            txtInfo.Text = String.Empty;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        int nOperatorIncId = Convert.ToInt32(db.usp_GetIDCTRCounter("tblOptrIncentives").SingleOrDefault().Value);
                        tblOpIncentive obj = new tblOpIncentive();
                        obj.Id = nOperatorIncId;
                        obj.OperatorId = Convert.ToInt32(ddlOp.SelectedValue);
                        obj.IncentiveTypeId = Convert.ToInt32(ddlIncentype.SelectedValue);
                        obj.IncentiveDetailId = Convert.ToInt32(ddlIncentiveDetailType.SelectedValue);
                        obj.ItemInfo = txtInfo.Text;
                        obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                        obj.DateIncentive = Helper.SetDateFormat2(txtwef.Text);

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Added_By = userId; ;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                        obj.Rec_Added_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edit_Date = null;
                        obj.Rec_Edit_By = null;

                        db.tblOpIncentives.Add(obj);
                        db.SaveChanges();
                        logmaintain(nOperatorIncId, "Operator Incentive", "Insert");

                        FillOpIncGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Operator Incentive Added Successfully";
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
                        int nOperatorIncId = Convert.ToInt32(ViewState["RecordID"]);
                        var obj = db.tblOpIncentives.Where(x => x.Id == nOperatorIncId).SingleOrDefault();
                        obj.OperatorId = Convert.ToInt32(ddlOp.SelectedValue);
                        obj.IncentiveTypeId = Convert.ToInt32(ddlIncentype.SelectedValue);
                        obj.IncentiveDetailId = Convert.ToInt32(ddlIncentiveDetailType.SelectedValue);
                        obj.ItemInfo = txtInfo.Text;
                        obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                        obj.DateIncentive = Helper.SetDateFormat2(txtwef.Text);

                        int userId = (int)HttpContext.Current.Session["userid"];
                        obj.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edit_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(nOperatorIncId,"Operator Incentive", "Update");
                        FillOpIncGrid();

                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Operator Incentive Updated  Successfully";
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

            FillOpIncGrid();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var s = db.tblOpIncentives.Where(x => x.Id == ID).SingleOrDefault();
            ddlOp.SelectedValue = s.OperatorId.ToString();
            ddlCity.SelectedValue = s.CityId.ToString();
            ddlIncentype.SelectedValue = s.IncentiveTypeId.ToString();
            ddlIncentiveDetailType.SelectedValue = s.IncentiveDetailId.ToString();
            txtInfo.Text = s.ItemInfo.ToString();
            txtwef.Text = s.DateIncentive.ToString();
            
            btnSave.Text = "Update";
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

        protected void ddlIncentiveDetailType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
    
   