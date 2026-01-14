using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using OfficeOpenXml.Packaging.Ionic.Zip;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using ExpressDigital;
using System.Xml;
using Ionic.Zip;

public partial class Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var id = Request.QueryString["id"];
        var type = Request.QueryString["type"];

        string zipFilePath = string.Empty;

        using (var dbContext = new DBScanEntities()) 
        {
            var documents = dbContext.usp_GetDigitalScanDocument(id,type).ToList();

            if (documents.Any())
            {
                string tempDirectory = Path.Combine(Server.MapPath("~/Temp"), Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempDirectory);

                try
                {
                    foreach (var document in documents)
                    {
                        var filePath = Path.Combine(tempDirectory, document.QuotationFilePath);
                        File.WriteAllBytes(filePath, document.UploadedFile);
                    }

                    zipFilePath = Path.Combine(Server.MapPath("~/Temp"), $"Request_{id}_{DateTime.Now.ToString("yyyy-MMM-dd-HHmmss")}.zip");
                
                    using (ZipFile zip = new ZipFile()) 
                    {
                        zip.AddDirectory(tempDirectory);
                        zip.Save(zipFilePath);
                    }
                   
                    Response.ContentType = "application/zip";
                    Response.AppendHeader("Content-Disposition", $"attachment; filename=Request_{id}.zip");
                    Response.TransmitFile(zipFilePath);
                    Response.Flush();
                    //Response.End();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
                finally
                {
                    Directory.Delete(tempDirectory, true);
                    File.Delete(zipFilePath);

                    Response.End();
                }
            }
            else
            {
                //MessageBox.Show("No file found");
            }
        }
    }

    protected bool SaveData(string path, byte[] data)
    {
        BinaryWriter Writer = null;
        try
        {
            Writer = new BinaryWriter(File.OpenWrite(path));
               
            Writer.Write(data);
            Writer.Flush();
            Writer.Close();
        }
        catch
        {
            return false;
        }

        return true;
    }
}