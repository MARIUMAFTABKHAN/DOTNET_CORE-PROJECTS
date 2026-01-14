using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace JobPortal
{
    /// <summary>
    /// Summary description for ReturnImageHandler
    /// </summary>
    public class ReturnImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!String.IsNullOrEmpty(context.Request.QueryString[0]))
            {
                string id = context.Request.QueryString[0].ToString ();

                string path = HttpContext.Current.Server.MapPath("~/Documents/") + id;
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

        //public Image Getstream(int theID, ref string ctype)
        //{
        //    try
        //    {

              
            

        //     //   return Image;//.FromStream(stream);

        //    }
        //    catch (Exception)
        //    {
        //        return null;

        //    }
        //}
    }
}