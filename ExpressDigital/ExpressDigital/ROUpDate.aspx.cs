using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class ROUpDate : System.Web.UI.Page
    {
        static bool _isSqlTypesLoaded = false;
        DbDigitalEntities db = new DbDigitalEntities();
        public ROUpDate()
        {
            if (!_isSqlTypesLoaded)
            {
                SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~"));
                _isSqlTypesLoaded = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var com = db.Companies.Where(x => x.Active == true).OrderBy(x => x.Company_Name).ToList();
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataSource = com;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Select Company", "0"));

                var g = db.Agencies.OrderBy(x => x.AgencyName).ToList();
                ddlAgency.DataValueField = "ID";
                ddlAgency.DataTextField = "AgencyName";
                ddlAgency.DataSource = g;
                ddlAgency.DataBind();
                ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
                ddlAgency_SelectedIndexChanged(null, null);

                txtSearchROMODateFrom.Text = DateTime.Now.AddDays(-31).ToString("dd/MM/yyyy");
                txtSearchROMODateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                btnSearch_Click(null, null);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            string strIRO;
            string strCampaign;
            string strExternal;
            string strReleaseOrderID;
            Int32? AgencyId;
            Int32? ClinetId;
            Int32? companyid;
            DateTime? StartDate;
            DateTime? EnDate;
            
            if (txtIRO.Text.Length == 0)
                strIRO = "";
            else
                strIRO = txtIRO.Text;

            if (ddlCompany.SelectedIndex == 0)
                companyid = null;
            else
                companyid = Convert.ToInt32(ddlCompany.SelectedValue);

            if (txtRefNumber.Text.Length == 0)
                strExternal = "";
            else
                strExternal = txtRefNumber.Text;

            if (txtSearchReleaseOrder.Text.Length == 0)
                strReleaseOrderID = "";
            else
                strReleaseOrderID = txtSearchReleaseOrder.Text;

            if (ddlAgency.SelectedIndex == 0)
                AgencyId = null;
            else
                AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);

            if (ddlClient.SelectedIndex == 0)
                ClinetId = null;
            else
                ClinetId = Convert.ToInt32(ddlClient.SelectedValue);

            if (txtcampaign.Text.Length == 0)
                strCampaign = "";
            else
                strCampaign = txtcampaign.Text;
            try
            {
                if (txtSearchROMODateFrom.Text.Length == 0 || txtSearchROMODateTo.Text.Length == 0)
                {
                    StartDate = null;
                    EnDate = null;
                }
                else
                {
                    StartDate = Helper.SetDateFormat(txtSearchROMODateFrom.Text);
                    EnDate = Helper.SetDateFormat(txtSearchROMODateTo.Text);
                }
            }
            catch (Exception)
            {
                StartDate = null;
                EnDate = null;
            }
            var s = db.usp_SerachReleaseOrder_New2(companyid, strReleaseOrderID, AgencyId, ClinetId, strIRO, strExternal, StartDate, EnDate, strCampaign).ToList();
            try
            {
                DataTable dt = Helper.ToDataTable(s);
                ViewState["dt"] = dt;
                gv.DataSource = s;
                gv.DataBind();
            }
            catch (Exception)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            var RO = db.ReleaseOrderMasters.Where(x => x.ID == ID).SingleOrDefault();
            if (RO != null)
            {
                RO.IsCancelled = true;
                db.SaveChanges();
                lblmessage.Text = "Release Order Cancelled";
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.PageIndex = e.NewPageIndex;
            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string isbilled = e.Row.Cells[8].Text;
                string iscancelled = e.Row.Cells[9].Text;
                if (isbilled == "1")
                {
                    ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                    ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    e.Row.Cells[8].Text = "Yes";
                }
                else
                {
                    e.Row.Cells[8].Text = "No";
                }

                if (iscancelled == "True")
                {
                    ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                    ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    e.Row.Cells[9].Text = "No";
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            Response.Redirect("BookReleaseOrder.aspx?ID=" + ID, true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
        }

        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Agencyid = Convert.ToInt32(ddlAgency.SelectedValue);
            var c = db.Clients.Where(x => x.AgencyID == Agencyid).ToList().OrderBy(x => x.Client1);
            ddlClient.DataValueField = "ID";
            ddlClient.DataTextField = "Client1";
            ddlClient.DataSource = c;
            ddlClient.DataBind();
            ddlClient.Items.Insert(0, new ListItem("Select Client", "0"));
        }
    }
}