using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class Users : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("UserCode");
            dt.Columns.Add("UserName");
            dt.Columns.Add("Designation");
            dt.Columns.Add("UserGroup");
            dt.Columns.Add("Department");
            dt.Columns.Add("Edition");
            dt.Columns.Add("Traffic/Finance");

            // Add static data
            dt.Rows.Add(1, "AA Sheikh", "Atif Ahmed Sheikh", "Marketing Executive", "Marketing Executive","PID","Karachi","No");

            gv.DataSource = dt;
            gv.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}