using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    context.Session["imgPath"] = file.FileName;
                    string fname = context.Server.MapPath("~/Documents/" + file.FileName);
                    file.SaveAs(fname);

                }
                context.Response.ContentType = "text/plain";
                context.Response.Write("File Uploaded Successfully!");
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