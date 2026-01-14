using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal
{
    /// <summary>
    /// Summary description for CaptchImgeHandler
    /// </summary>
    public class CaptchImgeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!String.IsNullOrEmpty(context.Request.QueryString[0]))
            {
                string id = context.Request.QueryString[0].ToString();

                string path = HttpContext.Current.Server.MapPath("~/Content/Captcha/") + id;
                context.Response.ContentType = "image/png";
                context.Response.WriteFile(path);

            }
            else
            {
                context.Response.ContentType = "text/html";
                context.Response.Write("<p>Need a valid id</p>");
            }

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}