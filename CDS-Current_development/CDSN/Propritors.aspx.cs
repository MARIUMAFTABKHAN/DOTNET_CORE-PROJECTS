using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class Propritors : System.Web.UI.Page
    {

        CDSEntities db = new CDSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDivision();
                // BindCity();
                //FillPropGrid();
            }

        }
        private void BindDivision()
        {
            var s = db.usp_CDSNGetAllDistrictsByUserId(Convert.ToInt32(Session["UserId"])).ToList();

            if (s.Count() > 0)
            {

                ddlDivision.DataTextField = "DivisionName";
                ddlDivision.DataValueField = "Id";
                ddlDivision.DataSource = s;
                ddlDivision.DataBind();

                ddlDivision.Items.Insert(0, new ListItem("Select Division", ""));
            }
            else
            {
                ddlDivision.Items.Clear();
                ddlDivision.Items.Insert(0, new ListItem("Select Division", "0"));
            }

        }

        private void BindCity()
        {
            if (ddlDivision.Items.Count > 0)
            {
                int divisionid = Convert.ToInt32(ddlDivision.SelectedValue);
                var dtCity = db.tblCities.Where(x => x.DivisionId == divisionid).ToList();
                if (dtCity.Count > 0)
                {
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataValueField = "Id";
                    ddlCity.DataSource = dtCity;
                    ddlCity.DataBind();

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
            try
            {
                FillPropGrid();
            }
            catch (Exception ex)
            {
                lblException.Visible = true;
                lblException.ForeColor = System.Drawing.Color.Red;
                lblException.Text = ex.Message;
                lblException.Focus();
            }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCity();
            ddlCity_SelectedIndexChanged(sender, e);
        }

        private void FillPropGrid()
        {
            try
            {
                int cityid = Convert.ToInt32(ddlCity.SelectedValue);
                var dt = (from u in db.tblPropertiers.Where(x => x.CityId == cityid && x.active==true)
                          select new { u.Id, 
                              u.Prp_Address, 
                              Status = u.active == true ? "Active" : "InActive",
                              u.CellNo, 
                              u.CNIC, 
                              u.ContactNo, 
                              u.Email, 
                              u.Name, 
                              u.tblCity.CityName   }).ToList(); 

                gvRecords.DataSource = dt;
                gvRecords.DataBind();
                lblGrid.Text = "Records : " + dt.Count().ToString();
            }
            catch (Exception ex)
            {
                lblException.Visible = true;
                lblException.ForeColor = System.Drawing.Color.Red;
                lblException.Text = ex.Message;
                lblException.Focus();
            }
        }
        private bool CheckValication()
        {
            bool Result = false;
            if ((ddlDivision.Items.Count > 0) && (ddlCity.Items.Count > 0))
            {
                Result = true;
            }
            return Result;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckValication())
            {
                if (btnSave.Text == "Save")
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            int id = db.usp_GetIDCTRCounter("tblPropertier").SingleOrDefault().Value;
                            tblPropertier obj = new tblPropertier();
                            obj.Id = Convert.ToInt32(id);
                            obj.Email = txtEmail.Text;
                            obj.CNIC = txtcnic.Text;
                            obj.CellNo = txtcell.Text;
                            obj.ContactNo = txtcontact.Text;
                            obj.Name = txtName.Text;
                            obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                            obj.Prp_Address = txtAddress.Text;
                            obj.active = ChkActive.Checked;

                            int userId = (int)HttpContext.Current.Session["userid"];
                            obj.Rec_Added_By = userId; ;

                            var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();

                            obj.Rec_Added_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                            obj.Rec_Edit_Date = null;
                            obj.Rec_Edit_By = null;

                            db.tblPropertiers.Add(obj);
                            db.SaveChanges();
                            logmaintain(Convert.ToInt32(id), "Propertier", "Insert");

                            FillPropGrid();
                            scope.Complete();
                            btnCancel_Click(null, null);
                            lblMsg.Text = "Propertier Added Successfully";
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
                            int id = Convert.ToInt32(ViewState["RecordID"]);
                            var obj = db.tblPropertiers.Where(x => x.Id == id).SingleOrDefault();
                            if (obj != null)
                            {
                                obj.Email = txtEmail.Text;
                                obj.CNIC = txtcnic.Text;
                                obj.CellNo = txtcell.Text;
                                obj.ContactNo = txtcontact.Text;
                                obj.Name = txtName.Text;
                                obj.CityId = Convert.ToInt32(ddlCity.SelectedValue);
                                obj.Prp_Address = txtAddress.Text;
                                obj.active = ChkActive.Checked;

                                int userId = (int)HttpContext.Current.Session["userid"];
                                obj.Rec_Edit_By = userId;

                                var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                                obj.Rec_Edit_Date = currentDateTime.Date + currentDateTime.TimeOfDay;

                                db.SaveChanges();
                                logmaintain(Convert.ToInt32(id), "Propertier", "Update");
                                FillPropGrid();

                                scope.Complete();
                                btnCancel_Click(null, null);
                                lblMsg.Text = "Propertier Updated  Successfully";
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
                }
            }
            else
            {
                ShowMsg("Missing!Operator/District/City");
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
            txtName.Text = String.Empty;
            txtcnic.Text = String.Empty;
            txtcontact.Text = String.Empty;
            txtcell.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtAddress.Text = string.Empty;
            ChkActive.Checked = true;
            lblException.Text = string.Empty;
            lblMsg.Text = string.Empty;
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

            FillPropGrid();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var s = db.tblPropertiers.Where(x => x.Id == ID).SingleOrDefault();
            ddlCity.SelectedValue = s.CityId.ToString();
            txtName.Text = s.Name.ToString();
            txtcnic.Text = s.CNIC.ToString();
            txtcontact.Text = s.ContactNo.ToString();
            txtcell.Text = s.CellNo.ToString();
            txtEmail.Text = s.Email.ToString();
            txtAddress.Text = s.Prp_Address.ToString();
            ChkActive.Checked = Convert.ToBoolean(s.active);
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
                    var record = db.tblPropertiers.SingleOrDefault(x => x.Id == id);
                    if (record != null)
                    {
                        record.active = false;

                        int userId = (int)HttpContext.Current.Session["userid"];
                        record.Rec_Edit_By = userId;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edit_Date = currentDateTime + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        logmaintain(id, "Propertier", "Delete");
                        FillPropGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblMsg.Text = "Propertier Deleted Successfully";
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