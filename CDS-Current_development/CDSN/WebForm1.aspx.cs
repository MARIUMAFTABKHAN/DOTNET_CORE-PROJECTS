using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        CDSEntities db=new CDSEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }   
        }

        private void BindGrid()
        {
            var g=db.tblChannelTypes.OrderBy(x=>x.ChannelType).ToList();

            DataTable dt= Helper.ToDataTable(g);
            ViewState["dt"] = dt;
            gvRecords.DataSource = dt;
            gvRecords.DataBind();

        }

        protected void gvRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myrow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gvRecords.DataKeys[myrow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var s = db.tblChannelTypes.Where(x => x.ID == ID).SingleOrDefault();
            txtCountry.Text = s.ChannelType;
            chkActive.Checked = Convert.ToBoolean(s.active);
            btnSave.Text = "Update";
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
        {

        }
    }
}