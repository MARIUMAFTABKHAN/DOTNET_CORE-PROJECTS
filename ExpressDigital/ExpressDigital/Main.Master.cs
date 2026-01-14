using System;
using System.Web.UI;

namespace ExpressDigital
{
    public partial class Main : System.Web.UI.MasterPage
    {
        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((UserInfo)Session["UserObject"]) == null)
                Response.Redirect("Login.aspx", true);

            //if (Session["HomeURL"] == null || Session["HomeURL"].ToString() == "")
            //{
            //    Response.Redirect("Login.aspx", true);
            //}

            lbllogin.Text = Session["UserName"].ToString();
            //Session["HomeURL"] = "Home.aspx";
            string pageName = Page.ToString().Replace("ASP.", "").Replace("_", ".");
            Helper.UID = ((UserInfo)Session["UserObject"]).ID;//(Convert.ToInt32(Session["UserID"]));
            Helper.Page = pageName;

            if ((Session["UserName"] != null))
                lbllogin.Text = Session["UserName"].ToString();

            else
                Response.Redirect("Login.aspx", true);
            if (pageName != "rptinvoice.aspx")
            {
                if (Helper.CheckValidPage(db) == false)
                {
                    Response.Redirect("Login.aspx", true);
                }
            }


        }
        //private void GetMenu()
        //{
        //int UID = Convert.ToInt32(Session["UserID"]);
        //int RoleID = Convert.ToInt32(Session["RoleID"]);
        //var s = db.usp_GetMenu(UID).ToList();

        //DataTable dt = Helper.ToDataTable(s);
        //StringBuilder strb = new StringBuilder();
        //int ParentID = 0;
        //strb.Append("<div class=\'collapse navbar-collapse\' id=\'bs-example-navbar-collapse-1\' style='padding:0px; background-color:#9ddcf7; !important;margin-top: 18px;'>");
        //strb.Append(" <ul class=\'nav navbar-nav\' style='margin-top:-15px;!important'>");
        //strb.Append(" <li class=\'active\'>");
        //strb.Append((" <a href=\'"
        //    //  + (Session["HomeURL"].ToString() + " \'>Home <span class=\'sr-only\'>(current)</span></a> ")));
        // + (Session["HomeURL"].ToString() + " \' style='padding:0px; padding-top:5px; background-color:#9ddcf7;height:48px !important'> <img src='Content/Images/Logo.png' alt='logo' '\' style='margin-top: -8px !important' /></a> ")));
        //strb.Append("</li> ");
        //foreach (DataRow x in dt.Rows)
        //    {
        //    if (ParentID != Convert.ToInt32(x["ParentMenu_Id"]))
        //        {
        //        ParentID = Convert.ToInt32(x["ParentMenu_Id"]);
        //        strb.Append("<li class=\'dropdown\'>");
        //        strb.Append(("<a href=\'#\' class=\'dropdown-toggle\' data-toggle=\'dropdown\' role=\'button\' aria-expanded=\'false\'>"
        //                        + (x["MenuName"] + "  <span class=\'caret\'></span></a> ")));
        //        strb.Append("<ul class=\'dropdown-menu\' role=\'menu\'> ");
        //        foreach (DataRow y in dt.Rows)
        //            {
        //            if (Convert.ToInt32(x["ParentMenu_Id"]) == Convert.ToInt32(y["ParentMenu_Id"]))
        //                {
        //                string mpath = (y["FormName"].ToString());
        //                string mitem = y["ItemName"].ToString();
        //                strb.Append(("<li> <a href= \'"
        //                                + (mpath + ("\'>"
        //                                + (mitem + "</a></li>")))));
        //                }

        //            }

        //        strb.Append("</ul>");
        //        strb.Append("</li>");
        //        }

        //    }

        //strb.Append(" </ul>");
        //strb.Append(" </div>");
        //Literal ltr = new Literal();
        //ltr.Text = strb.ToString();
        //ph.Controls.Add(ltr);

        //}
    }
}

