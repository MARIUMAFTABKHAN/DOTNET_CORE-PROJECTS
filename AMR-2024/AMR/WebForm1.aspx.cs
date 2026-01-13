using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPOMS
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
        }
        private void BindGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("BrandName");
            dt.Columns.Add("Company/Client");
            dt.Columns.Add("ClientExecutive");
            dt.Columns.Add("MediaBuyingHouse");
            dt.Columns.Add("Agency");
            dt.Columns.Add("AgencyExecutive");
            dt.Columns.Add("Status");

            dt.Rows.Add(1, "A&A Associate", "A_A Associate", "", "", "", "", "Active");

            gv.DataSource = dt;
            gv.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}