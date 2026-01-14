using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class FrmUploadFiles : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var doct = db.DocumentTypes.ToList();
                ddlvrType.DataValueField = "Id";
                ddlvrType.DataTextField = "Description";
                ddlvrType.DataSource = doct;
                ddlvrType.DataBind();

                lbluserid.Text = ((UserInfo)Session["UserObject"]).ID.ToString();//Convert.ToString(Session["UserID"]);
            }
        }

        protected void vtb_Click(object sender, EventArgs e)
        {
            //  int ID = 110000007;
            //  imgPic.ImageUrl = "~/GetImage.ashx?ID=" + ID.ToString() + "&n=" + DateTime.Now.Second.ToString();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtROSearch.Text.Length > 0)
            {
                string txt = txtROSearch.Text;
                var s = db.ReleaseOrderMasters.Where(x => x.ReleaseOrderNumber.Contains(txt) || x.ReleaseOrderReferenceID.Contains(txt)).ToList();
                if (s.Count > 0)
                {
                    ddlROList.DataValueField = "ID";
                    ddlROList.DataTextField = "ReleaseOrderNumber";
                    ddlROList.DataSource = s;
                    ddlROList.DataBind();

                }
            }
            else
            {
                lblmessage.Text = "Release  Enter Internal Order Order";
            }

        }
    }
}