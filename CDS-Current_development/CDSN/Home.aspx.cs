using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class Home : System.Web.UI.Page
    {
        string strConn = Helper.GetDBConnectionString();
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
             //   fillGridByUserId();
            }
        }
        protected void gvRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRecords.PageIndex = e.NewPageIndex;
           // fillGridByUserId();
        }
        //private void fillGridByUserId()
        //{
        //    using (CDSEntities db = new CDSEntities())
        //    {
        //        int uid = Convert.ToInt32(Session["UserId"]);
        //        var ds = db.usp_GetEventListByUserIdForUser(uid).ToList();

        //        gvRecords.DataSource = ds;
        //        gvRecords.DataBind();

        //    }
        //}
        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    if (txtReply.Text.Trim().Length > 0)
        //    {
        //        using (CDSEntities db = new CDSEntities())
        //        {
        //            Int32 EventId = 0;
        //            try
        //            {
        //                EventId = Convert.ToInt32(lblresult.Text);
        //                var idctr = db.usp_IDctr("EventDetail");
        //                tblEventDetail obj = new tblEventDetail();

        //                obj.EventId = EventId;
        //                obj.ID = Convert.ToInt32(idctr);
        //                obj.MessageDate = DateTime.Now;
        //                obj.MessageReplyed = txtReply.Text;
        //                obj.StatusId = 0;
        //                obj.MessageBy = Convert.ToInt32(Session["userid"]);
        //                db.tblEventDetails.Add(obj);
        //                db.SaveChanges();
        //                fillGridByUserId();
        //                SetEventDetails(Convert.ToInt32(lblresult.Text));
        //                string Err = "Message replied successfully";
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "ShowMessage('" + Err + "');", true);
        //                fillGridByUserId();
        //                SetEventDetails(Convert.ToInt32(lblresult.Text));

        //            }
        //            catch (Exception)
        //            {
        //                string Err = "Message not replied ";
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "ShowMessage('" + Err + "');", true);

        //            }
        //        }
        //    }
        //    else
        //    {
        //        string Err = "Message text required ";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "ShowMessage('" + Err + "');", true);

        //    }


        //}
        protected void btnCancelP_Click(object sender, EventArgs e)
        {
            txtReply.Text = string.Empty;
        }


        protected void imgbtn_Click(object sender, ImageClickEventArgs e)
        {
            txtReply.Text = "";
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            //SetEventDetails(Convert.ToInt32(gvRecords.DataKeys[gvrow.RowIndex].Value));
            lblresult.Text = gvRecords.DataKeys[gvrow.RowIndex].Value.ToString();
            lblHeadEndName.Text = gvrow.Cells[4].Text;
            this.ModalPopupExtender1.Show();
        }

        //private void SetEventDetails(int EventId)
        //{
        //    gvEventDetails.DataSource = null;
        //    gvEventDetails.DataBind();
        //    using (CDSEntities db = new CDSEntities())
        //    {
        //        var obj = db.usp_GetEventDetailsByEventId(EventId).ToList();
        //        gvEventDetails.DataSource = obj;
        //        gvEventDetails.DataBind();
        //        lblTaskDetail.Text = obj.Take(1).SingleOrDefault().Task ;

        //    }
        //}
        protected void gvEventDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text == "admin")
                {
                    e.Row.BackColor = System.Drawing.Color.AliceBlue;
                    e.Row.ForeColor = System.Drawing.Color.Blue;
                }
            }
        }
    } }