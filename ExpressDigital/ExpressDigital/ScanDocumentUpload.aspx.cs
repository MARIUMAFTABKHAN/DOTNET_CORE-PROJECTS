using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.Entity.Validation;

namespace ExpressDigital
{
    public partial class ScanDocumentUpload : System.Web.UI.Page
    {

        DbDigitalEntities db = new DbDigitalEntities();
        DBScanEntities db2 = new DBScanEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdCreatedBy.Value = ((UserInfo)Session["UserObject"]).ID.ToString();
                ddlDocumentType_SelectedIndexChanged(null, null);
                SETDate();
                
            }
            
        }
        protected void ddlDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDocumentType.SelectedItem.Value == "RO")
                lblTextToShow.Text = "RO Number :";
            else
                lblTextToShow.Text = "Reference No. :";
        }
        private void SETDate()
        {
            DateTime date = DateTime.Now;


            if (txtFromDate.Text.Trim().Length <= 0)
            {
                date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                txtFromDate.Text = firstDayOfMonth.ToString("dd/MM/yyyy");
                //txtToDate.Text = lastDayOfMonth.ToString("dd/MM/yyyy");
            }
            if (txtToDate.Text.Trim().Length <= 0)
            {
                date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                //txtFromDate.Text = firstDayOfMonth.ToShortDateString();
                txtToDate.Text = lastDayOfMonth.ToString("dd/MM/yyyy");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = "";
            if (txtPONumber.Text.Length > 0)
            {
                switch (ddlDocumentType.SelectedValue)
                {
                    case "RO":
                        GETReleaseOrder();
                        break;
                    case "ACLetter":
                        GETACLetter();
                        break;
                }
            }
            else
            {
                lblmessage.Text = "Please enter release order number.";
            }
        }

        private void GETReleaseOrder()
        {
            //SETDate();

            string poNumber = txtPONumber.Text.Trim();

            var dsRoSource = db.usp_get_RoSourceInfo(0, poNumber, 1, Helper.SetDateFormat(txtFromDate.Text), Helper.SetDateFormat(txtToDate.Text)).ToList();
            if (dsRoSource.Count>0)
            {
                hdRONumber.Value = dsRoSource[0].ReleaseOrderNumber;
                hdReleaseOrderId.Value = dsRoSource[0].ID.ToString();

                btnUpload.Enabled = true;
                FileUpload1.Enabled = true;

            }
            else
            {
                lblmessage.Text = "No record found.";
                btnUpload.Enabled = false;
                FileUpload1.Enabled = false;
                hdRONumber.Value = "";
            }
        }
        private void GETACLetter()
        {
            string poNumber = txtPONumber.Text.Trim();

            var dsRoSource = db.usp_GetDocumentByReferenceNO(poNumber).ToList();
            if (dsRoSource.Count > 0)
            {
                hdRONumber.Value = dsRoSource[0].ReferenceNo;
                hdReleaseOrderId.Value = dsRoSource[0].ID.ToString();

                btnUpload.Enabled = true;
                FileUpload1.Enabled = true;
            }
            else
            {
                lblmessage.Text = "No record found.";
                btnUpload.Enabled = false;
                FileUpload1.Enabled = false;
                hdRONumber.Value = "";
            }
        }
        private byte[] GetFileBytes(HttpPostedFile fu)
        {
            using (Stream fs = fu.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    return bytes;
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                HttpPostedFile file = FileUpload1.PostedFile;
                byte[] fileBytes = new byte[file.ContentLength];
                file.InputStream.Read(fileBytes, 0, file.ContentLength);
                var type = file.ContentType;
                int createdby;
                try
                {
                    using (DBScanEntities db = new DBScanEntities())
                    {
                        if(int.TryParse(hdCreatedBy.Value,out createdby))
                        {
                            //string documentType = ddlDocumentType.SelectedItem.Text;
                            if (type.Length > 50)
                            {
                                type = type.Substring(0, 50);
                            }

                            DigitalDocument newDocument = new DigitalDocument
                            {
                                RONumber = hdRONumber.Value,
                                FileName = file.FileName,
                                FileSize = file.ContentLength,
                                FileType = Path.GetExtension(file.FileName),
                                UploadedFile = fileBytes,
                                CreatedDate = DateTime.Now,
                                CreatedBy =createdby,
                                QuotationFilePath = file.FileName,
                                DocumentType = type, 
                                ContentType = ddlDocumentType.SelectedItem.Text,
                                Remarks = txtRemarks.Text.Trim(),
                                AppCode = "DIGITAL"
                            };
                            db.DigitalDocuments.Add(newDocument);
                            db.SaveChanges();

                            lblmessage.Text = "File uploaded successfully";
                            txtPONumber.Text = "";
                            txtRemarks.Text = "";
                            btnUpload.Enabled = false;
                            FileUpload1.Enabled = false;
                        }
                        
                        

                        
                    }
                }
                catch (DbEntityValidationException ex)
                {

                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                        
                            lblmessage.Text += string.Format("Property: {0} Error: {1}",
                                                              validationError.PropertyName,
                                                              validationError.ErrorMessage);
                        }
                    }

                 
                    //lblmessage.Text = "Error uploading file: " + ex.Message;
                }
            }
            else
            {
                lblmessage.Text = "Please select a file to upload.";
            }
        }
    }
}