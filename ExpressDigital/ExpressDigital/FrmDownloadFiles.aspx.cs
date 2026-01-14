using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class FrmDownloadFiles : System.Web.UI.Page
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
            //int ID = 110000007;// Convert.ToInt32(context.Request.Form.GetValues(0).GetValue(0));
            //var i = db.ROAttachedDocuments.Where(x => x.ID == ID).SingleOrDefault();
            //if (i != null)
            //{
            //    try
            //    {                  
            //        string txtid = ID.ToString();
            //        byte[] btImage = (byte[])i.DocFile;
            //        Response.Buffer = true;
            //        Response.ContentType = "Image/jpeg";
            //        Response.AddHeader("content-disposition", "attachment;filename=" + ID.ToString()+".jpg"); // to open file prompt Box open or Save file  
            //        Response.Charset = "";
            //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //        Response.BinaryWrite((byte[]) btImage);
            //        Response.End();

            //    }
            //    catch (Exception ex)
            //    {
            //        string txt = ex.Message;
            //    }
            //    //  int ID = 110000007;
            //    //  imgPic.ImageUrl = "~/GetImage.ashx?ID=" + ID.ToString() + "&n=" + DateTime.Now.Second.ToString();

            //}
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (txtROSearch.Text.Length > 0)
            //{
            //    string txt = txtROSearch.Text;
            //    using (DBScanEntities obj = new DBScanEntities())
            //    {
            //        var s = (from u in obj.ROAttachedDocuments.Where(x => x.docext == ".jpg" && x.RONumber.Contains(txt))
            //                 select new { u.ID, u.Remarks, u.RONumber, u.DocType, u.AppCode }).ToList();
            //        if (s.Count > 0)
            //        {
            //            gv.DataSource = s;
            //            gv.DataBind();

            //        }
            //    }
            //}
            //else
            //{
            //    lblmessage.Text = "Release  Enter Internal Order Order";
            //}

        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)img.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            imgPic.ImageUrl = "~/GetImage.ashx?ID=" + ID.ToString() + "&n=" + DateTime.Now.Second.ToString();
        }

        protected void DelButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)img.Parent.Parent;  // the row
            using (DBScanEntities obj = new DBScanEntities())
            {
                Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            //    var i = obj.ROAttachedDocuments.Where(x => x.docext == ".jpg" && x.ID == ID).SingleOrDefault();
            //    if (i != null)
            //    {
            //        try
            //        {
            //            string txtid = ID.ToString();
            //            imgPic.ImageUrl = "~/GetImage.ashx?ID=" + ID.ToString() + "&n=" + DateTime.Now.Second.ToString();

            //            byte[] btImage = (byte[])i.AttachedData;
            //            Response.Buffer = true;
            //            Response.ContentType = "Image/jpeg";
            //            Response.AddHeader("content-disposition", "attachment;filename=" + ID.ToString() + ".jpg"); // to open file prompt Box open or Save file  
            //            Response.Charset = "";
            //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //            Response.BinaryWrite((byte[])btImage);
            //            Response.End();

            //        }
            //        catch (Exception ex)
            //        {
            //            string txt = ex.Message;
            //        }
            //        //  int ID = 110000007;


            //    }
            }
        }
    }
}