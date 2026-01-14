using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class StationManagement : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var s = db.Countries.Where(x => x.IsActive == true).OrderBy(x => x.CountryName).ToList();
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataSource = s;
                ddlCountry.DataBind();

                ddlCountry_SelectedIndexChanged(null, null);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        var s = db.usp_IDctr("Station").SingleOrDefault();
                        int ID = s.Value;
                        CityManagement obj = new CityManagement();
                        obj.ID = ID;
                        obj.CityName = txtStation.Text;
                        obj.StateID = Convert.ToInt32(ddlStates.SelectedValue);
                        obj.IsActive = ChkIsActive.Checked;
                        db.CityManagements.Add(obj);
                        db.SaveChanges();
                        BindGrid();
                        db.SaveChanges();
                        btnCancel_Click(null, null);
                        scope.Complete();
                        lblmessage.Text = "City  Created Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }
            else
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        int ID = Convert.ToInt32(ViewState["RecordID"]);
                        var obj = db.CityManagements.Where(x => x.ID == ID).SingleOrDefault();
                        obj.ID = ID;
                        obj.CityName = txtStation.Text;
                        obj.StateID = Convert.ToInt32(ddlStates.SelectedValue);
                        obj.IsActive = ChkIsActive.Checked;
                        db.SaveChanges();
                        BindGrid();

                        btnCancel_Click(null, null);
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }
        }

        private void BindGrid()
        {
            try
            {
                int id = Convert.ToInt32(ddlStates.SelectedValue);
                var g = db.CityManagements.Where(x => x.StateID == id && x.IsActive == true).OrderBy(x => x.CityName).ToList();
                DataTable dt = Helper.ToDataTable(g);
                ViewState["dt"] = dt;
                gv.DataSource = dt;
                gv.DataBind();
            }
            catch (Exception)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtStation.Text = string.Empty;
            ChkIsActive.Checked = true;
            btnSave.Text = "Save";
            lblmessage.Text = string.Empty;
            try
            {
                ddlCountry.SelectedIndex = 0;
                ddlCountry_SelectedIndexChanged(null, null);
            }
            catch (Exception)
            {

            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.CityManagements.Where(x => x.ID == ID).SingleOrDefault();
            try
            {
                ddlStates.SelectedValue = obj.StateID.ToString();
            }
            catch (Exception)
            {
                throw;
            }

            txtStation.Text = obj.CityName;

            ChkIsActive.Checked = obj.IsActive;
            btnSave.Text = "Update";
        }
        protected void DelButton_Click(object sender, EventArgs e)
        {
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;
        }

        [WebMethod]
        public static string OnSubmit(string id)
        {
            string mess = "";
            DbDigitalEntities db = new DbDigitalEntities();
            int ID = Convert.ToInt32(id);
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var info = db.CityManagements.Find(ID);
                    if (info != null)
                    {
                        db.CityManagements.Remove(info);
                        db.SaveChanges();
                        LogManagers.RecordID = ID;
                        LogManagers.ActionOnForm = "Station";
                        LogManagers.ActionBy = ((UserInfo)HttpContext.Current.Session["UserObject"]).ID;//Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                        LogManagers.ActionOn = DateTime.Now;
                        LogManagers.ActionTaken = "Delete";
                        LogManagers.SetLog(db);
                        scope.Complete();
                        mess = "Ok";
                    }
                }
                catch (Exception ex)
                {
                    mess = ExceptionHandler.GetException(ex);
                }

            }
            return mess;
        }

        protected void ddlStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtStation.Text = string.Empty;
                int id = Convert.ToInt32(ddlCountry.SelectedValue);
                var s = db.CountryStates.Where(x => x.IsActive == true && x.CountryID == id).ToList().OrderBy(x => x.StateName);
                ddlStates.DataValueField = "ID";
                ddlStates.DataTextField = "StateName";
                ddlStates.DataSource = s;
                ddlStates.DataBind();

                ddlStates_SelectedIndexChanged(null, null);
            }
            catch (Exception)
            {

            }
        }
    }
}