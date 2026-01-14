using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProdContact
{
    public class BaseClass : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (!AuthenticationHelper.IsUserAuthenticated(Request))
            {
                System.Diagnostics.Debug.WriteLine("User is not authenticated. Redirecting to Login.aspx.");
                Response.Redirect("Login.aspx");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("User is authenticated.");
            }

            base.OnLoad(e);
        }
    }

    public static class AuthenticationHelper
    {
        public static bool IsUserAuthenticated(HttpRequest request)
        {
            //var a =HttpContext.Current.Session["UserName"].ToString();
            HttpCookie userIdCookie = request.Cookies["UserId"];
            
            if (userIdCookie == null)
            {
                // Log or output for debugging
                System.Diagnostics.Debug.WriteLine("UserId cookie not found in the request.");
            }
            return userIdCookie != null && !string.IsNullOrEmpty(userIdCookie.Value);
        }
    }
}