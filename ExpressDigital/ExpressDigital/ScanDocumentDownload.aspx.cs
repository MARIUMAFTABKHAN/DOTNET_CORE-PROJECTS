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
using DataSet = System.Data.DataSet;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace ExpressDigital
{
    public partial class ScanDocumentDownload : System.Web.UI.Page
    {

        DbDigitalEntities db = new DbDigitalEntities();
        DBScanEntities db1 = new DBScanEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                 lbluserid.Text = ((UserInfo)Session["UserObject"]).ID.ToString();
                
                
            }
            
        }

        public string MakeDownloadFileURL(string roNumber)
        {
            string url = "";
            url = "Download.aspx?id=" + roNumber + "&type=" + ddlDocumentType.SelectedItem.Text;
            return url;
        }

   

        private DataSet GETROScannedDocuments()
        {
            string txt = txtPOSearch.Text.Trim();
            var data = db1.usp_GetDigitalScanDocument(txt, ddlDocumentType.SelectedItem.Text).ToList();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            foreach (var column in GetColumnsFromData(data))
            {
                dt.Columns.Add(column);
            }

            foreach (var item in data)
            {
                DataRow row = dt.NewRow();
                foreach (var prop in item.GetType().GetProperties())
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                dt.Rows.Add(row);
            }

            ds.Tables.Add(dt);

            return ds;
        }

        private IEnumerable<string> GetColumnsFromData(IEnumerable<object> data)
        {
            if (data.Any())
            {
                var firstItem = data.First();
                return firstItem.GetType().GetProperties().Select(p => p.Name);
            }
            return Enumerable.Empty<string>();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = "";
            if (txtPOSearch.Text.Length > 0)
            {
                var ds = GETROScannedDocuments();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                else
                {
                    gv.DataSource = null;
                    gv.DataBind();
                    lblmessage.Text = "No record found.";
                }
            }
            else
            {
                lblmessage.Text = "Please enter release order number.";
            }
        }

        protected void DelButton_Click(object sender, ImageClickEventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var row = (GridViewRow)imageButton.Parent.Parent; 
            var id = gv.DataKeys[row.RowIndex].Values["RONumber"];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Download.aspx?id=" + id + ", '_blank');", true);
            return;

        }

       
    }
}