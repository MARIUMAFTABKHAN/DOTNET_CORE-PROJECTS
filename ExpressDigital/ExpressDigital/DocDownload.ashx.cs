using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace ExpressDigital
{
    /// <summary>
    /// Summary description for DocDownload
    /// </summary>
    public class DocDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DBScanEntities obj = new DBScanEntities();
            int ID = Convert.ToInt32(HttpContext.Current.Request.Params["id"]);
            var item = obj.ROAttachedDocuments.Where(x => x.ID == ID).SingleOrDefault();
            //if (item != null)
            //{
            //    // Regex reg = new Regex("[*'\",_&#^@]");
            //    string fileName = Regex.Replace(item.RONumber, @"\t|\n|\r", "");
            //    //DownLoadFileLocal(item.QuotationFilePath, (byte[])item.UploadedFile);
            //    //string  fileName = reg.Replace(item.RONumber, string.Empty);
            //    string fileExtension = item.docext;
            //    var path = context.Server.MapPath("~/Documents");

            //    if (SaveData(path, item.AttachedData, fileName, fileExtension))
            //    {
            //        //DownLoadFileLocal(item.QuotationFilePath, (byte[])item.UploadedFile);
            //        fileName = Regex.Replace(item.RONumber, @"\t|\n|\r", "");
            //        fileExtension = item.docext;

            //        // Set Response.ContentType
            //        context.Response.ContentType = GetContentType(fileExtension);
            //        string fullpath = path + "\\" + (fileName + fileExtension);
            //        // Append header
            //        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fullpath);

            //        // Write the file to the Response
            //        context.Response.WriteFile(fullpath);
            //        System.IO.File.Delete(fullpath);

            //        context.Response.End();
            //    }
            //}
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        protected bool SaveData(string path, byte[] data, string filename, string ext)
        {
            BinaryWriter Writer = null;
            //var path = Path.Combine(Server.MapPath("~/DownloadDocument"), fileName);
            //string Name = Server.MapPath("~/UploadDocument");
            try
            {
                // Create a new stream to write to the file
                string fullpath = path + "\\" + (filename + ext);

                Writer = new BinaryWriter(File.OpenWrite(fullpath));

                // Writer raw data                
                Writer.Write(data);
                Writer.Flush();
                Writer.Close();
            }
            catch
            {
                //...
                return false;
            }

            return true;
        }

        private string GetContentType(string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                return string.Empty;

            string contentType = string.Empty;
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                    contentType = "text/HTML";
                    break;

                case ".txt":
                    contentType = "text/plain";
                    break;

                case ".doc":
                case ".rtf":
                case ".docx":
                    contentType = "Application/msword";
                    break;

                case ".xls":
                case ".xlsx":
                    contentType = "Application/x-msexcel";
                    break;

                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;

                case ".gif":
                    contentType = "image/GIF";
                    break;

                case ".pdf":
                    contentType = "application/pdf";
                    break;
            }

            return contentType;
        }


        //BinaryWriter writer = null;
        //    //DbDigitalEntities db = new DbDigitalEntities();
        //    DBScanEntities obj = new DBScanEntities();
        //    int ID = Convert.ToInt32(HttpContext.Current.Request.Params["id"]);            
        //    var i = obj.ROAttachedDocuments.Where(x => x.ID == ID).SingleOrDefault();
        //    if (i != null)
        //    {
        //        try
        //        {
        //            string txtid = ID.ToString();
        //            byte[] btImage = (byte[])i.AttachedData;
        //            context.Response.Clear();
        //            context.Response.ContentType = "application/octet-stream";
        //            context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + (ID.ToString() + i.docext)  + "\"");
        //            context.Response.AddHeader("Content-Length", Convert.ToString(btImage.Length));
        //            context.Response.BinaryWrite(btImage);
        //            Response.TransmitFile
        //            context.Response.Flush();
        //            context.Response.Close();
        //            context.Response.End();



        //string txtid = ID.ToString();
        //byte[] btImage = (byte[])i.AttachedData ;
        //context.Response.Buffer = true;
        //context.Response.ContentType = "application/octet-stream";
        //context.Response.AddHeader("content-disposition", "attachment;filename=" + ID.ToString() + i.docext); // to open file prompt Box open or Save file  
        //context.Response.Charset = "";
        //context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //context.Response.BinaryWrite(btImage);
        //context.Response.End();

        //string txtid = ID.ToString();
        //byte[] btImage = (byte[])i.AttachedData ;                  
        //context.Response.Buffer = true;
        //context.Response.ContentType = "Image/jpeg";
        //context.Response.AddHeader("content-disposition", "attachment;filename=" + ID.ToString() + ".jpg"); // to open file prompt Box open or Save file  
        //context.Response.Charset = "";
        //context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //context.Response.BinaryWrite((byte[])btImage);
        //context.Response.End();

        //        }
        //        catch (Exception ex)
        //        {
        //           // string txt = ex.Message;
        //        }
        //    }
        //}
        //public static byte[] ReadFully(Stream input)
        //{
        //    byte[] buffer = new byte[16 * 1024];
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        int read;
        //        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            ms.Write(buffer, 0, read);
        //        }
        //        return ms.ToArray();
        //    }


    }

}

