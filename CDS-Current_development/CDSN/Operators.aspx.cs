using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class Operators : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDivision();
                BindAreaType();
                txtwef.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }

        }
        private void BindDivision()
        {
            var s = db.usp_CDSNGetAllDistrictsByUserId(Convert.ToInt32(Session["UserId"])).ToList();
            if (s.Count() > 0)
            {
                ddldiv.DataTextField = "DivisionName";
                ddldiv.DataValueField = "Id";
                ddldiv.DataSource = s;
                ddldiv.DataBind();

                ddldiv.Items.Insert(0, new ListItem("Select Division", ""));
            }
            else
            {
                ddldiv.Items.Clear();
                ddldiv.Items.Insert(0, new ListItem("Select Division", "0"));
            }
        }

        private void BindCity()
        {
            if (ddldiv.Items.Count > 0)
            {  
                int divisionid = Convert.ToInt32(ddldiv.SelectedValue);

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
               // ddlProprietor_SelectedIndexChanged(null, null);
                    
            }
        }

        private void BindProprietor()
        {
            int cityid = Convert.ToInt32(ddlCity.SelectedValue);

            var ds = db.tblPropertiers.Where(x => x.CityId == cityid && x.active == true).ToList();
            if (ds.Count == 0)
            {
                ddlProprietor.Items.Insert(0, new ListItem("Select Properitor", "0"));
            }
            else
            {
                ddlProprietor.DataTextField = "Name";
                ddlProprietor.DataValueField = "ID";
                ddlProprietor.DataSource = ds;
                ddlProprietor.DataBind();

                ddlProprietor.Items.Insert(0, new ListItem("Select Proprietor", ""));
            }
            //ddlProprietor_SelectedIndexChanged(null, null);
           
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProprietor();
           // ddlProprietor_SelectedIndexChanged(sender, e);
        }

        protected void ddlProprietor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOperatorGrid();
        }

        protected void ddldiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCity();
          //  ddlProprietor_SelectedIndexChanged(sender, e);
            // BindProprietor();
        }
        private void BindAreaType()
        {
            // int cityid = Convert.ToInt32(ddlCity.SelectedValue);
            var ds = db.tbAreaTypes.Where(x => x.active == true).ToList();
            ddlAreaType.DataSource = ds;
            ddlAreaType.DataTextField = "LicenseCatergory";
            ddlAreaType.DataValueField = "LicenseCatergory";
            ddlAreaType.DataBind();
        }


        private void FillOperatorGrid()
        {
            try
            {
                int cityid = Convert.ToInt32(ddlProprietor.SelectedValue);
                var dt = (from u in  db.tblOperators.Where(x => x.proprietorID == cityid && x.active==true)
                          select new {u.Id, u.Name, 
                              u.tblCity.CityName, 
                              u.Cell, u.Email, u.LicCategory, 
                              u.LicNo, u.Lic_Status,
                              Status = u.active == true ? "Active" : "InActive"}).ToList();

                gvRecords.DataSource = dt;
                gvRecords.DataBind();
                if (dt.Count > 0) { lblGrid.Text = "Records ( " + (gvRecords.PageIndex == 0 ? 1 : gvRecords.PageIndex * gvRecords.PageSize) + " to " + (gvRecords.PageIndex + 1) * gvRecords.PageSize + " )" + " of " + dt.Count.ToString(); } else lblGrid.Text = "Records  Could Not Found ";
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
            if (CheckValidation())
            {
                if (btnSave.Text == "Save")
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            int nOperatorId = Convert.ToInt32(db.usp_GetIDCTRCounter("tblOperator").SingleOrDefault().Value);
                            tblOperator obj = new tblOperator();
                            obj.Id = nOperatorId;
                            obj.Email = txtEmail.Text;
                            obj.Name = txtName.Text;
                            obj.Address = txtAddress.Text;
                            obj.LandLine = txtLandline.Text;
                            obj.Cell = txtMobile.Text;
                            obj.active = Chkactive.Checked;
                            obj.Remarks = "";
                            obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                            obj.LicCategory = ddlAreaType.SelectedValue;
                            obj.proprietorID = Convert.ToInt32(ddlProprietor.SelectedValue);
                            obj.Lic_Status = ddllicStatus.SelectedValue;
                            obj.LicNo = txtLic.Text;
                            obj.LicReviewDate = Helper.SetDateFormat2(txtwef.Text);

                            //obj.CreatedOn = DateTime.Now;
                            //obj.EditOn = null;

                            int userId = (int)HttpContext.Current.Session["userid"];
                            obj.Rec_Added_By = userId; ;

                            var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                            obj.CreatedOn = currentDateTime.Date + currentDateTime.TimeOfDay;

                            obj.EditOn = null;
                            obj.Rec_Edit_By = null;

                            db.tblOperators.Add(obj);
                            db.SaveChanges();
                            logmaintain(nOperatorId, "Operator", "Insert");

                            FillOperatorGrid();
                            scope.Complete();
                            btnCancel_Click(null, null);
                            lblMsg.Text = "Operator Added Successfully";
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
                            int nOperatorId = Convert.ToInt32(ViewState["RecordID"]);
                            var obj = db.tblOperators.Where(x => x.Id == nOperatorId).SingleOrDefault();
                            obj.Email = txtEmail.Text;
                            obj.Address = txtAddress.Text;
                            obj.LandLine = txtLandline.Text;
                            obj.Cell = txtMobile.Text;
                            obj.active = Chkactive.Checked;
                            obj.Name = txtName.Text;
                            obj.Remarks = "";
                            obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                            obj.LicCategory = ddlAreaType.SelectedValue;
                            obj.active = Chkactive.Checked;
                            obj.proprietorID = Convert.ToInt32(ddlProprietor.SelectedValue);
                            obj.Lic_Status = ddllicStatus.SelectedValue;
                            obj.LicNo = txtLic.Text;
                            obj.LicReviewDate = Helper.SetDateFormat2(txtwef.Text);

                            //obj.EditOn = DateTime.Now;

                            int userId = (int)HttpContext.Current.Session["userid"];
                            obj.Rec_Edit_By = userId;

                            var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                            obj.EditOn = currentDateTime + currentDateTime.TimeOfDay;

                            db.SaveChanges();
                            logmaintain(nOperatorId, "Operator", "Update");
                            FillOperatorGrid();

                            scope.Complete();
                            btnCancel_Click(null, null);
                            lblMsg.Text = "Operator Updated  Successfully";
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
                ShowMsg("Missing!Operator/District/City");
            }
        }

        private bool CheckValidation()
        {
            bool Result = false;
            if ((ddlProprietor.Items.Count > 0) && (ddldiv.Items.Count > 0) && (ddlCity.Items.Count > 0))
            {
                Result = true;
            }
            return Result;
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
            txtName.Text = String.Empty;
            txtAddress.Text = String.Empty;
            txtMobile.Text = String.Empty;
            txtLandline.Text = String.Empty;
            txtEmail.Text = String.Empty;
            ddllicStatus.SelectedIndex = 0;
            txtLic.Text = String.Empty;
            txtwef.Text = DateTime.Now.ToString("dd-MM-yyyy");
            ddldiv.SelectedIndex = 0;
            //BindCity();
            ddlCity.SelectedIndex = 0;
            ddlAreaType.SelectedIndex = 0;
            ddlProprietor.SelectedIndex = 0;
            btnSave.Text = "Save";
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

            FillOperatorGrid();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var s = db.tblOperators.Where(x => x.Id == ID).SingleOrDefault();
            ddlProprietor.SelectedValue = s.proprietorID.ToString();
            txtName.Text = s.Name.ToString();
            txtAddress.Text = s.Address.ToString();
            txtMobile.Text=s.Cell.ToString();
            txtLandline.Text = s.LandLine.ToString();
            txtEmail.Text = s.Email.ToString();
            ddlAreaType.SelectedValue = s.LicCategory.ToString();
            txtLic.Text=s.LicNo.ToString();
            txtwef.Text=s.LicReviewDate.ToString();
            ddllicStatus.SelectedValue=s.Lic_Status.Trim().ToUpper();
            ddlCity.SelectedValue=s.CityId.ToString();
            Chkactive.Checked = Convert.ToBoolean(s.active);
            btnSave.Text = "Update";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ImageButton deletebutton = (ImageButton)sender;

            // Try to parse the CommandArgument
            int id;
            if (!int.TryParse(deletebutton.CommandArgument, out id))
            {
                lblMsg.Text = "Invalid City ID.";
                return;
            }
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.tblOperators.SingleOrDefault(x => x.Id == id);
                    if (record != null)
                    {
                        record.active = false;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        record.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.EditOn = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(id, "Operator", "Delete");
                        FillOperatorGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Operator Deleted Successfully";
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