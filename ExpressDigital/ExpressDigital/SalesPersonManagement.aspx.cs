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
    public partial class SalesPersonManagement : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var d = db.Designations.OrderBy(x => x.DesignationName).ToList();
                ddlDesignation.DataValueField = "ID";
                ddlDesignation.DataTextField = "DesignationName";
                ddlDesignation.DataSource = d;
                ddlDesignation.DataBind();

                var s = db.CityManagements.OrderBy(x => x.CityName).ToList();
                ddlStation.DataValueField = "ID";
                ddlStation.DataTextField = "CityName";
                ddlStation.DataSource = s;
                ddlStation.DataBind();

                BindGrid();
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
                        var s = db.usp_IDctr("SalesPerson").SingleOrDefault();
                        int ID = s.Value;
                        SalesPerson obj = new SalesPerson();
                        obj.ID = ID;
                        obj.SalesPersonName = txtSalesPerson.Text;
                        obj.ContactNumber = txtContact.Text;
                        obj.EmailAddress = txtEmail.Text;
                        obj.DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                        obj.StationID = Convert.ToInt32(ddlStation.SelectedValue);
                        obj.IsActive = ChkIsActive.Checked;
                        db.SalesPersons.Add(obj);
                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Sales Person Created Successfully";
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
                        var obj = db.SalesPersons.Where(x => x.ID == ID).SingleOrDefault();
                        obj.ID = ID;
                        obj.SalesPersonName = txtSalesPerson.Text;
                        obj.ContactNumber = txtContact.Text;
                        obj.EmailAddress = txtEmail.Text;
                        obj.DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                        obj.StationID = Convert.ToInt32(ddlStation.SelectedValue);
                        obj.IsActive = ChkIsActive.Checked;
                        db.SaveChanges();
                        BindGrid();
                        scope.Complete();
                        btnCancel_Click(null, null);
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
            var g = (from u in db.SalesPersons
                     select new
                     {
                         u.ID,
                         u.SalesPersonName,
                         u.ContactNumber,
                         u.EmailAddress,
                         u.Designation.DesignationName,
                         u.CityManagement.CityName,
                         u.IsActive
                     }).ToList();

            DataTable dt = Helper.ToDataTable(g);
            ViewState["dt"] = dt;
            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtSalesPerson.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlDesignation.SelectedIndex = 0;
            ddlStation.SelectedIndex = 0;
            ChkIsActive.Checked = true;
            btnSave.Text = "Save";
            lblmessage.Text = string.Empty;
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.SalesPersons.Where(x => x.ID == ID).SingleOrDefault();
            txtSalesPerson.Text = obj.SalesPersonName;
            txtContact.Text = obj.ContactNumber;
            txtEmail.Text = obj.EmailAddress;
            ddlDesignation.SelectedValue = obj.DesignationID.ToString();
            ddlStation.SelectedValue = obj.StationID.ToString();
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
                    var info = db.SalesPersons.Find(ID);
                    if (info != null)
                    {
                        db.SalesPersons.Remove(info);
                        db.SaveChanges();
                        LogManagers.RecordID = ID;
                        LogManagers.ActionOnForm = "SalesPerson";
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
    }
}