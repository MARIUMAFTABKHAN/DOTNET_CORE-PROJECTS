using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CDSN.Controls
{
    public partial class menuCtrl : System.Web.UI.UserControl
    {

        StringBuilder oStringBuilder = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            
                using (CDSEntities db = new CDSEntities())
                {

                    //DynamicMednu(db);


                    CreatMenu(db);
                }
            }
        

        private void GetMenu(CDSEntities db)
        {
          
            //int UID = Convert.ToInt32(Session["userid"]);
            //int RoleID = Convert.ToInt32(Session["roleid"]);
            //var s = db.usp_GetMenu(UID);


            //StringBuilder strb = new StringBuilder();
            //int ParentID = 0;
            //strb.Append("<div class=\'collapse navbar-collapse\' id=\'bs-example-navbar-collapse-1\' style='padding:0px; background-color:#9ddcf7; !important;margin-top: 18px;'>");
            //strb.Append(" <ul class=\'nav navbar-nav\' style='margin-top:-15px;!important'>");
            //strb.Append(" <li class=\'active\'>");
            //strb.Append((" <a href=\'"
            // //  + (Session["HomeURL"].ToString() + " \'>Home <span class=\'sr-only\'>(current)</span></a> ")));
            // + (Session["HomeURL"].ToString() + " \' style='padding:0px; padding-top:5px; background-color:#9ddcf7;height:48px !important'> <img src='Content/Images/Logo.png' alt='logo' '\' style='margin-top: -8px !important' /></a> ")));
            //strb.Append("</li> ");
            //foreach (var x in s)
            //{
            //    if (ParentID != Convert.ToInt32(x.ParentMenu_Id))
            //    {
            //        ParentID = Convert.ToInt32(x.ParentMenu_Id);
            //        strb.Append("<li class=\'dropdown\'>");
            //        strb.Append(("<a href=\'#\' class=\'dropdown-toggle\' data-toggle=\'dropdown\' role=\'button\' aria-expanded=\'false\'>"
            //                        + (x.MenuName + "  <span class=\'caret\'></span></a> ")));
            //        strb.Append("<ul class=\'dropdown-menu\' role=\'menu\'> ");
            //        var ss = db.usp_GetMenu(UID);
            //        //Hashtable Al = new Hashtable();

            //        foreach (var y in ss)
            //        {
            //            if (Convert.ToInt32(x.ParentMenu_Id) == Convert.ToInt32(y.ParentMenu_Id))
            //            {
            //                string mpath = (y.FormName.ToString());
            //                string mitem = y.ItemName.ToString();
            //                strb.Append(("<li> <a href= \'"
            //                                + (mpath + ("\'>"
            //                                + (mitem + "</a></li>")))));
            //                // Al.Add(y.ItemID, y.FormName);
            //            }

            //        }
            //        //  Session["frmhash"] = Al;
            //        strb.Append("</ul>");
            //        strb.Append("</li>");
            //    }

            //}

            //strb.Append(" </ul>");
            //strb.Append(" </div>");
            //Literal ltr = new Literal();
            //ltr.Text = strb.ToString();
            //PHMenu.Controls.Add(ltr);

        }
        private void DynamicMednu()
        {

        }

        private DataTable GetSubMenu(int HeaderID, CDSEntities db)
        {
            int uid = Convert.ToInt32(Session["userid"]);
            var hm = db.usp_GetUserMenuByHeaderID(HeaderID, uid).ToList();
            DataTable dtMenuSub = Helper.ToDataTable(hm);

            return dtMenuSub;
        }

        private void CreatMenu(CDSEntities db)
        {

            if (Session["userid"] != null)
            {
                int uid = Convert.ToInt32(Session["userid"]);
                var hm = db.usp_GetUserMenu(uid).ToList();

                oStringBuilder.AppendLine("<ul id='menu'>");
                int header = 0;
                int i = 0;
                //header = dtMenuHeader.Rows[0][0].ToString();
                foreach (var x in hm)
                {

                    if (header != x.MenuHeaderId)
                    {
                        string MenuURL = x.MenuHeader;
                        string MenuName = x.MenuHeader;
                        string line = "";
                        if (MenuURL == "999")
                        {
                            MenuURL = "Login.aspx";
                        }
                        else
                        {
                            MenuURL = "#";
                        }

                        line = String.Format(@"<li ><a href=""{0}"">{1}</a>", MenuURL, MenuName);
                        oStringBuilder.Append(line);
                        // string MenuID = dr["MenuID"].ToString();
                        int ParentID = x.MenuHeaderId;
                        //DataTable dt = getdt(ParentID);
                        var view = db.usp_GetUserMenuByHeaderID(ParentID, uid).ToList();
                        if (view.Count > 0)
                        {
                            var subMenuBuilder = new StringBuilder();
                            oStringBuilder.AppendLine("<ul>");
                            foreach (var y in view)
                            {
                                string url = y.FormName.Replace(" ", ""); //Regex.Replace(dri[1].ToString(), @"\s+", "");
                                line = String.Format(@"<li ><a  href=""{0}"">{1}</a>", url + ".aspx", y.MenuItemName);
                                oStringBuilder.Append(line);
                                oStringBuilder.Append("</li>");

                            }
                            oStringBuilder.AppendLine("</ul>");
                        }
                    }
                }
                Literal ltr = new Literal();
                ltr.Text = oStringBuilder.ToString();
                menu.Controls.Add(ltr);
                PHMenu.Controls.Add(menu);

            }
        }
    }
}
