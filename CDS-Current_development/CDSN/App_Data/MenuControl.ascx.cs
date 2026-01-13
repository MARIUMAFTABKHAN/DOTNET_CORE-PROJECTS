using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class MenuControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  if (!Page.IsPostBack)
            {
                Session["HomeURL"] = "CurrentActivity.aspx";
                GetMenu();
            }
        }

        private void GetMenu()
        {
            try
            {
                using (CDSEntities db = new CDSEntities())
                {
                    
                    int UID = Convert.ToInt32(Session["userid"]);
                    int RoleID = Convert.ToInt32(Session["roleid"]);
                    var s = db.usp_MenusControl(UID);



                    StringBuilder strb = new StringBuilder();
                    int ParentID = 0;
                    strb.Append("<div class=\'collapse navbar-collapse\' id=\'bs-example-navbar-collapse-1\' style='color:#bed8eb; margin-left:10px !important;margin-right:10px !important; padding:0px; background-color:#4682B4;'>");
                    strb.Append(" <ul class=\'nav navbar-nav\'>");
                    strb.Append(" <li class=\'active\'>");
                    strb.Append((" <a href=\'"
                     //  + (Session["HomeURL"].ToString() + " \'>Home <span class=\'sr-only\'>(current)</span></a> ")));
                     + (Session["HomeURL"].ToString() + " \' style='padding:0px; padding-top:5px; width:120px; text-align: center; color:black; margin-top:8px; background-color:#4682B4 ;height:40px !important'> Home </a> ")));
                    strb.Append("</li> ");
                    foreach (var x in s)
                    {
                        if (ParentID != Convert.ToInt32(x.MenuID))
                        {
                            ParentID = Convert.ToInt32(x.MenuID);
                            strb.Append("<li class=\'dropdown\'>");
                            strb.Append(("<a href=\'#\' class=\'dropdown-toggle\' data-toggle=\'dropdown\' role=\'button\' aria-expanded=\'false\'>"
                                            + (x.MenuName + "  <span class=\'caret\'></span></a> ")));
                            strb.Append("<ul class=\'dropdown-menu\' role=\'menu\'> ");
                            var ss = db.usp_MenusControl(UID);
                            //Hashtable Al = new Hashtable();

                            foreach (var y in ss)
                            {
                                if (Convert.ToInt32(x.MenuID) == Convert.ToInt32(y.MenuID))
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

            }
            catch (Exception ex)
            {
                var i = ex.Message;
            }
        }
    }
}