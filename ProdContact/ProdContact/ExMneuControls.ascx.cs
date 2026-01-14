using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProdContact
{
    public partial class ExMneuControls : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            GetMenu();


        }
        public List<MenuData> GetMenuData(int userId)
        {

            Model1Container db = new Model1Container();
            //var menuData = (from u in db.usp_GetMenuNew(userId).ToList()
            //                select new MenuData
            //                {
            //                    RoleId = u.RoleID,
            //                    MenuName = u.MenuName,
            //                    ItemName = u.ItemName,
            //                    OrderBy = u.OrderBy,
            //                    ItemID = u.MenuItemId,
            //                    FormName = u.FormName,
            //                    MenuId = u.ParentMenuId

            //                }).ToList();

            var menuData = (from u in db.usp_GetMenuInfo(userId).ToList()
                            select new MenuData
                            {
                                RoleId = u.RoleId,
                                MenuName = u.MenuName,
                                ItemName = u.ItemName,
                                OrderBy = u.OrderBy,
                                ItemID = u.ItemID,
                                FormName = u.FormName,
                                MenuId = u.ParentMenuId

                            }).ToList();

            return menuData;

        }

        private void GetMenu()
        {
            try
            {
                int UID= Convert.ToInt32(Request.Cookies["ID"].Value);
                string username= Request.Cookies["UserId"]?.Value;
                

                var menuItems = GetMenuData(UID);
                StringBuilder strb = new StringBuilder();
                int ParentID = 0;
                strb.Append("<div class='header'>");
                strb.Append("<img src='Content/Images/Logo.png' alt='Logo' class='logo'>");

                strb.Append("<div class='header-text'>");
                strb.Append("<div class='main-title'>Advertisement Monitoring System</div>");
                strb.Append("<div class='sub-title'>Express Publication</div>");
                strb.Append("</div>");
                //new code
                // Bootstrap 3-style dropdown button
                strb.Append("<div class='dropdown header-menu'>");
                strb.Append("<button class='btn btn-default dropdown-toggle' type='button' data-toggle='dropdown'>");
                strb.Append("<span class='glyphicon glyphicon-menu-hamburger'></span>");
                strb.Append("</button>");
                strb.Append("<ul class='dropdown-menu dropdown-menu-right'>");

                // Home
                strb.Append("<li><a href='/Dashboard.aspx'>Dashboard</a></li>");
                strb.Append("<li class='divider'></li>");

                // Dynamic menu items
                foreach (var x in menuItems)
                {
                    if (ParentID != x.MenuId)
                    {
                        ParentID = x.MenuId;
                        strb.Append("<li class='dropdown-submenu'>");
                        strb.Append($"<a tabindex='-1' >{x.MenuName}</a>");
                        strb.Append("<ul class='dropdown-menu'>");

                        foreach (var y in menuItems)
                        {
                            if (x.MenuId == y.MenuId)
                            {
                                string mpath = y.FormName ?? "#";
                                string mitem = y.ItemName ?? "Untitled";
                                strb.Append($"<li><a href='{Page.ResolveUrl("/" + mpath)}'>{mitem}</a></li>");
                            }
                        }

                        strb.Append("</ul>");
                        strb.Append("</li>");
                    }
                }

                strb.Append("</ul>");
                strb.Append("</div>"); // end of dropdown
                strb.Append("</div>"); // end of header

                // Sub-bar
                strb.Append("<div class='sub-bar'>");
                strb.Append("<span class='sub-label'>Welcome, " + Server.HtmlEncode(username) + "</span>");
                strb.Append("<a href='Login.aspx' class='logout-link'>Logout</a>");
                strb.Append("</div>");

                //new code end 

                //strb.Append("<button class='navbar-toggler ms-auto header-toggler' type='button' data-bs-toggle='offcanvas' data-bs-target='#offcanvasNavbar' aria-controls='offcanvasNavbar' aria-label='Toggle navigation'>");
                //strb.Append("<span class='navbar-toggler-icon'></span>");
                //strb.Append("</button>");

                //strb.Append("</div>"); // end of header
                //strb.Append("<div class='sub-bar'>");
                //strb.Append("<span class='sub-label'>Welcome, " + Server.HtmlEncode(username)+"</span>");
                //strb.Append("<a href='Logout.aspx' class='logout-link'>Logout</a>");
                //strb.Append("</div>");

                //// Offcanvas menu
                //strb.Append("<div class='offcanvas offcanvas-end' tabindex='1' id='offcanvasNavbar' aria-labelledby='offcanvasNavbarLabel'>");

                //// Offcanvas header
                //strb.Append("<div class='offcanvas-header ' >");

                //strb.Append("<img src = '../Content/Images/SM-Logo.png' alt='header SU' class='center' />");

                //strb.Append("<button type='button' class='btn-close' data-bs-dismiss='offcanvas' aria-label='Close'></button>");
                //strb.Append("</div>");

                //// Offcanvas body with navigation items
                //strb.Append("<div class='offcanvas-body'>");
                //strb.Append("<ul class='navbar-nav justify-content-end flex-grow-1 pe-3'>");

                //// Static "Home" menu item
                //strb.Append("<li class='nav-item'>");
                //strb.Append("<a class='nav-link active' href='/Default.aspx' aria-current='page'>Home</a>");
                //strb.Append("</li>");

                // Dynamic menu items
                //foreach (var x in menuItems)
                //{
                //    if (ParentID != x.MenuId)
                //    {
                //        ParentID = x.MenuId;

                //        // Parent menu item with dropdown toggle
                //        strb.Append("<li class='nav-item dropdown'>");
                //        strb.Append($"<a class='nav-link dropdown-toggle' href='#' role='button' data-bs-toggle='dropdown' aria-expanded='false'>{x.MenuName}</a>");
                //        strb.Append("<ul class='dropdown-menu'>");

                //        // Add child menu items
                //        foreach (var y in menuItems)
                //        {
                //            if (x.MenuId == y.MenuId)
                //            {
                //                string mpath = y.FormName ?? "#";
                //                string mitem = y.ItemName ?? "Untitled";
                //                strb.Append($"<li><a class='dropdown-item' href='{Page.ResolveUrl("/" + mpath)}'>{mitem}</a></li>");
                //            }
                //        }

                //        strb.Append("</ul>");
                //        strb.Append("</li>");
                //    }
                //}
                //strb.Append("</ul>");
                //strb.Append("</div>");
                //strb.Append("</div>");
                //strb.Append("</nav>");
                Literal ltr = new Literal
                {
                    Text = strb.ToString()
                };
                PlaceHolderMenu.Controls.Add(ltr);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
            }
        }


    }
}