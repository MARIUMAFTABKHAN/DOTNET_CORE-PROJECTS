using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Hosting;
using System.IO;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.Entity.Validation;

namespace ExpressDigital
{
    /// <summary>
    /// Summary description for ScanHandler
    /// </summary>
    public class ScanHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();

                //deserialize the object
                Employee objUsr = Deserialize<Employee>(strJson);
                using (DBScanEntities db = new DBScanEntities())
                {
                    if (objUsr != null)
                    {
                        string userid = objUsr.userid;
                        string ronumber = objUsr.ronumber;
                        string doctypeid = objUsr.doctypeid;
                        int roid = objUsr.roid;
                        string ext = objUsr.ext;
                        string port = objUsr.port;
                        string remarks = objUsr.remarks;
                        string DataValue = objUsr.datacontent;
                        int id = 0;
                        //db.Users.OrderByDescending(u => u.UserId).FirstOrDefault();
                        try
                        {
                            var attachedid = db.ROAttachedDocuments.OrderByDescending(x => x.ID).FirstOrDefault();
                            id = attachedid.ID + 1;
                        }
                        catch (Exception ex)
                        {
                            id = 1;
                        }

                        //ROAttachedDocument obj = new ROAttachedDocument();
                        //string path = UploadImage(objUsr.datacontent, id.ToString());
                        //var arrByte = File.ReadAllBytes(path);
                        //obj.ROID = roid;
                        //obj.RONumber = ronumber;
                        //obj.AttachedData = arrByte;
                        //obj.DocType = doctypeid;
                        //obj.AppCode = "Digital";
                        //obj.CreatedBy = Convert.ToInt32(userid);
                        //obj.CreatedOn = DateTime.Now;
                        //obj.Remarks = remarks;
                        //obj.docext = ext;
                        //db.ROAttachedDocuments.Add(obj);
                        //db.SaveChanges();

                        try
                        {
                            //FileInfo info = new FileInfo(path);
                            //if (info.Exists)
                            //    info.Delete();
                        }


                        catch (Exception ex)
                        {
                            context.Response.Write(ex.Message);
                        }
                    }
                    else
                    {
                        context.Response.Write("No Data");
                    }
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Document Detail Added");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string UploadImage(string imageData, string id)
        //public static int UploadImage(string productName)
        {
            //var a = productName;
            string fileNameWitPath = HostingEnvironment.MapPath("~/Documents/" + id.ToString() + ".jpg");
            using (var fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(imageData);//convert from base64
                    bw.Write(data);
                    bw.Close();
                }
                return fileNameWitPath;
            }
        }
        public class Employee
        {
            public string userid { get; set; }
            public string ronumber { get; set; }
            public string doctypeid { get; set; }
            public int roid { get; set; }
            public string ext { get; set; }
            public string port { get; set; }
            public string remarks { get; set; }
            public string datacontent { get; set; }
        }
        public T Deserialize<T>(string context)
        {
            string jsonData = context;

            //cast to specified objectType
            var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);
            return obj;
        }
    }
}