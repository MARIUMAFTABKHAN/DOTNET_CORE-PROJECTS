using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class MenuControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  if (!Page.IsPostBack)
            {
                GetMenu();
            }
        }

        private void GetMenu()
        {
            try
            {

                DbDigitalEntities db = new DbDigitalEntities();
                int UID = ((UserInfo)Session["UserObject"]).ID;
                int RoleID = ((UserInfo)Session["UserObject"]).RoleID;//Convert.ToInt32(Session["roleid"]);
                var s = db.usp_GetMenu(UID);


                StringBuilder strb = new StringBuilder();
                int ParentID = 0;
                strb.Append("<div class=\'collapse navbar-collapse\' id=\'bs-example-navbar-collapse-1\' style='color:#FFF; margin-left:10px !important;margin-right:10px !important; padding:0px; background-color:#000; !important;margin-top: 18px;'>");
                strb.Append(" <ul class=\'nav navbar-nav\' style='margin-top:-15px;!important'>");
                strb.Append(" <li class=\'active\'>");
                strb.Append((" <a href=\'"
                 //  + (Session["HomeURL"].ToString() + " \'>Home <span class=\'sr-only\'>(current)</span></a> ")));
                 + (Session["HomeURL"].ToString() + " \' style='padding:0px; padding-top:5px; color:#FFF; margin-top:8px; background-color:#000;height:48px !important'> Home </a> ")));
                strb.Append("</li> ");
                foreach (var x in s)
                {
                    if (ParentID != Convert.ToInt32(x.ParentMenu_Id))
                    {
                        ParentID = Convert.ToInt32(x.ParentMenu_Id);
                        strb.Append("<li class=\'dropdown\'>");
                        strb.Append(("<a href=\'#\' class=\'dropdown-toggle\' data-toggle=\'dropdown\' role=\'button\' aria-expanded=\'false\'>"
                                        + (x.MenuName + "  <span class=\'caret\'></span></a> ")));
                        strb.Append("<ul class=\'dropdown-menu\' role=\'menu\'> ");
                        var ss = db.usp_GetMenu(UID);
                        //Hashtable Al = new Hashtable();

                        foreach (var y in ss)
                        {
                            if (Convert.ToInt32(x.ParentMenu_Id) == Convert.ToInt32(y.ParentMenu_Id))
                            {
                                string mpath = (y.FormName.ToString());
                                string mitem = y.ItemName.ToString();
                                strb.Append(("<li> <a href= \'"
                                                + (mpath + ("\'>"
                                                + (mitem + "</a></li>")))));
                                // Al.Add(y.ItemID, y.FormName);
                            }

                        }
                        //  Session["frmhash"] = Al;
                        strb.Append("</ul>");
                        strb.Append("</li>");
                    }

                }

                strb.Append(" </ul>");
                strb.Append(" </div>");
                Literal ltr = new Literal();
                ltr.Text = strb.ToString();
                ph.Controls.Add(ltr);

            }
            catch (Exception ex)
            {
                var i = ex.Message;
            }
        }
    }
}