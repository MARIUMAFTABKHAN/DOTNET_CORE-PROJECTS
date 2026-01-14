using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ExpressDigital
{
    /// <summary>
    /// Summary description for FileHandler
    /// </summary>
    public class FileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            using (DBScanEntities db = new DBScanEntities())
            {
            //    if (context.Request.Files.Count > 0)
            //    {
            //        string guid = context.Request.QueryString[0].ToString();
            //        //Fetch the Uploaded File.
            //        HttpPostedFile postedFile = context.Request.Files[0];
            //        byte[] fileData = null;
            //        using (var binaryReader = new BinaryReader(context.Request.Files[0].InputStream))
            //        {
            //            fileData = binaryReader.ReadBytes(context.Request.Files[0].ContentLength);
            //        }
            //        ROAttachedDocument obj = new ROAttachedDocument();
            //        string[] RONO = context.Request.QueryString[1].ToString().Split('-');
            //        obj.ROID = Convert.ToInt32(RONO[1]);
            //        obj.RONumber = context.Request.QueryString[1].ToString();
            //        obj.Remarks = postedFile.FileName;
            //        FileInfo info = new FileInfo(postedFile.FileName);
            //        obj.DocType = "110000001";
            //        obj.AppCode = "Digital";
            //        obj.AttachedData = fileData;
            //        obj.CreatedBy = Convert.ToInt32(context.Request.QueryString[2].ToString());
            //        obj.CreatedOn = DateTime.Now;
            //        obj.docext = info.Extension;
            //        db.ROAttachedDocuments.Add(obj);
            //        db.SaveChanges();


            //        //Set the Folder Path.
            //        //  string folderPath = context.Server.MapPath("~/Documents/");

            //        //Set the File Name.
            //        //     string fileName = Path.GetFileName(postedFile.FileName);
            //        //   string ext = Path.GetExtension(postedFile.FileName);
            //        //Save the File in Folder.
            //        //     postedFile.SaveAs(folderPath + guid + ext);

            //        //Send File details in a JSON Response.
            //        //   string json = new JavaScriptSerializer().Serialize(
            //        //     new
            //        //      {
            //        //      name = fileName
            //        //      });
            //        context.Response.StatusCode = (int)HttpStatusCode.OK;
            //        context.Response.ContentType = "text/json";
            //        //   context.Response.Write(json);
            //        context.Response.End();
            //    }
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