using ClosedXML.Excel;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class FrmRptLedger : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        Int32? companyID;
        Int32? agencyID;
        Int32? groupagencyID;

        DateTime? StartDate;
        DateTime? EnDate;
       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                StartDate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Month, 1);
                EnDate = StartDate.Value.AddMonths(1).AddDays(-1);

                txtSearchROMODateFrom.Text = StartDate.Value.ToString("dd/MM/yyyy");
                txtSearchROMODateTo.Text = EnDate.Value.ToString("dd/MM/yyyy"); 
                var gl = db.Companies.Where(x => x.Active == true).ToList();
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataSource = gl;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Select Company", "0"));
                ddlCompany.SelectedIndex = 0;

                var ma = db.GroupAgencies.Where(x => x.Active == true).OrderBy(x => x.GroupName).ToList();
                ddlMasteragency.DataValueField = "ID";
                ddlMasteragency.DataTextField = "GroupName";
                ddlMasteragency.DataSource = ma;
                ddlMasteragency.DataBind();
                ddlMasteragency.Items.Insert(0, new ListItem("Select Agency", "0"));
                ddlMasteragency_SelectedIndexChanged(null, null);
            }
        }
        protected void btnExecute_Click(object sender, EventArgs e)
        {
            string myWords = "";
            string AgencyAddress = "";
            string AgencyNTN = "";
            string AgencySalesTax = "";
            string AgencyName = "";
            string title = "";
            // Int32? clientID;
            if (ddlCompany.SelectedValue == "0")
                companyID = null;
            else
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            try
            {
                if (ddlMasteragency.SelectedIndex == 0)
                {
                    groupagencyID = null;

                    title = "All Agencies";
                    AgencyAddress = "NA";
                    AgencyNTN = "NA";
                    AgencySalesTax = "NA";
                    AgencyName = "ALL";
                }
                else
                {
                    groupagencyID = Convert.ToInt32(ddlMasteragency.SelectedValue);

                    var objG = db.GroupAgencies.Where(g => g.ID == groupagencyID).FirstOrDefault();
                    if (objG != null)
                    {
                        title = "Group Agency Ledger";
                        AgencyAddress = objG.Address;
                        if (objG.NTNNumber == null)
                            AgencyNTN = "NA";
                        else
                            
                           AgencyNTN =  objG.NTNNumber;

                        if (objG.GSTNumber == null)
                            AgencySalesTax = "NA";
                        else

                            AgencySalesTax = objG.GSTNumber;
                                              
                        AgencyName = objG.GroupName;

                    }

                }
            }
            catch (Exception ex)
            {

                groupagencyID = null;// Convert.ToInt32(ddlAgency.SelectedValue);
            }
            try
            {
                if (ddlAgency.SelectedValue == "0")
                {
                   
                    agencyID = null;
                }
                else
                {
                    title = "Agency Account";
                    agencyID = Convert.ToInt32(ddlAgency.SelectedValue);
                    var objG = db.Agencies.Where(x => x.ID  == agencyID && x.Active == true).SingleOrDefault();
                    if (objG != null)
                    {
                        AgencyAddress = objG.Address;
                        AgencyNTN = "NA" ;
                        AgencySalesTax ="NA";
                        AgencyName = objG.AgencyName;

                    }


                }
            }
            catch (Exception ex)
            {
                agencyID = null;// Convert.ToInt32(ddlAgency.SelectedValue);
            }

           

            //if (ddlc.SelectedIndex == 0)
            //    clientID = null;
            //else
            //    clientID = Convert.ToInt32(ddlCompany.SelectedValue);


            try
            {
                if (txtSearchROMODateFrom.Text.Length == 0 || txtSearchROMODateTo.Text.Length == 0)
                {
                    StartDate = null;// Helper.SetDateFormat("01/01/1985");
                    EnDate = null;// Helper.SetDateFormat(DateTime.Now.ToString("dd/MM/yyyy"));// txtSearchROMODateTo.Text);
                }
                else
                {
                    StartDate = Helper.SetDateFormat(txtSearchROMODateFrom.Text);
                    EnDate = Helper.SetDateFormat(txtSearchROMODateTo.Text);
                }

            }
            catch (Exception)
            {
                StartDate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Month, 1);
                EnDate = StartDate.Value.AddMonths(1).AddDays(-1);

                //StartDate = DateTime.Now.AddDays(DateTime.Now.);
                //EnDate = null;
            }
            try
            {
                
                //var ss = db.usp_GetLedgerReport(companyID, StartDate, EnDate, agencyID, null).ToList();
               
               

                if (ddlCompany.SelectedValue == "9")

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/DigitalPrintLedger.rdlc";
                else if (ddlCompany.SelectedValue == "0")
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/DigitalLedger.rdlc";
                else
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/ExpressPrintLedger.rdlc";
                if (ddlCompany.SelectedValue == "0")
                {
                    var ss = db.usp_GetLedgerReportWithMaster(companyID, StartDate, EnDate, agencyID, null, groupagencyID).ToList();


                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportParameter[] rp = new ReportParameter[3];
                    rp[0] = new ReportParameter("pmTitle", title);
                    rp[1] = new ReportParameter("pmFrom", "Date From: " + (StartDate.Value.ToString("dd-MM-yyyy")));
                    rp[2] = new ReportParameter("pmTo", "Date To: " + EnDate.Value.ToString("dd-MM-yyyy")); ;

                    ReportDataSource rds = new ReportDataSource("DSLedger", ss);
                    ReportViewer1.LocalReport.SetParameters(rp);

                    ReportViewer1.LocalReport.DataSources.Add(rds);


                }
                else
                {
                    var ss = db.usp_GetLedgerReportWithMaster(companyID,  StartDate, EnDate, agencyID, null, groupagencyID).ToList();

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportParameter[] rp = new ReportParameter[7];
                    rp[0] = new ReportParameter("pmTitle", title);
                    rp[1] = new ReportParameter("pmFrom", "Date From: " + (StartDate.Value.ToString("dd-MM-yyyy")));
                    rp[2] = new ReportParameter("pmTo", "Date To: " + EnDate.Value.ToString("dd-MM-yyyy")); ;
                    rp[3] = new ReportParameter("pmagencyname", AgencyName);
                    rp[4] = new ReportParameter("pmntn", AgencyNTN );
                    rp[5] = new ReportParameter("pmtax", AgencySalesTax);
                    rp[6] = new ReportParameter("pmaddress", AgencyAddress);

                    ReportDataSource rds = new ReportDataSource("DSLedger", ss);
                    ReportViewer1.LocalReport.SetParameters(rp);

                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                ReportViewer1.LocalReport.SubreportProcessing +=
                new Microsoft.Reporting.WebForms.SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                ReportViewer1.LocalReport.Refresh();
                //  ReportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing; 
                //Add ReportDataSource  

            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }

        }
        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            var sr = db.usp_LedgerSummarySubLedgerReportWithMaster(companyID, agencyID, StartDate, EnDate, groupagencyID).ToList();
            ReportDataSource ds = new ReportDataSource("DSLedgerSummary", sr);

            //   if (e.ReportPath == "LegerSubReport")
            //   {
            // var employeeDetails = new ReportDataSource() { Name = "DSLedgerSummary", Value = sr };
            e.DataSources.Add(ds);
            //    }
        }
        protected void ddlMasteragency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(ddlMasteragency.SelectedValue);
            { 

                var ca = db.Agencies.Where(x => x.GroupID == ID && x.Active == true).ToList();
                ddlAgency.DataValueField = "ID";
                ddlAgency.DataTextField = "AgencyName";
                ddlAgency.DataSource = ca;
                ddlAgency.DataBind();
                ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
            }
        }

        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //int ID = Convert.ToInt32(ddlAgency.SelectedValue);
            //if (ID > 0)
            //{
            //    var objG = db.Agencies.Where(g => g.ID == ID).FirstOrDefault();
            //    AgencyAddress = objG.Address ;
            //    AgencyNTN = "NA";
            //    AgencySalesTax = "NA";
            //    agencyID = ID;
            //    AgencyName = objG.AgencyName;
            //    title = "Agency Ledger";
            //}
            //else
            //{
            //    ID = Convert.ToInt32(ddlMasteragency.SelectedValue);
            //    var objG = db.GroupAgencies.Where(g => g.ID == ID).FirstOrDefault();
            //    AgencyAddress = objG.Address;
            //    AgencyNTN = objG.NTNNumber;
            //    AgencySalesTax = objG.GSTNumber;
            //    groupagencyID = objG.ID;
            //    AgencyName = objG.GroupName;
            //    title = "Group Agency Ledger";
            //}
        }


        protected void btnexport_Click(object sender, EventArgs e)
        {
            string myWords = "";
            string AgencyAddress = "";
            string AgencyNTN = "";
            string AgencySalesTax = "";
            string AgencyName = "";
            string title = "";
            // Int32? clientID;
            if (ddlCompany.SelectedValue == "0")
                companyID = null;
            else
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            try
            {
                if (ddlMasteragency.SelectedIndex == 0)
                {
                    groupagencyID = null;

                    title = "All Agencies";
                    AgencyAddress = "NA";
                    AgencyNTN = "NA";
                    AgencySalesTax = "NA";
                    AgencyName = "ALL";
                }
                else
                {
                    groupagencyID = Convert.ToInt32(ddlMasteragency.SelectedValue);

                    var objG = db.GroupAgencies.Where(g => g.ID == groupagencyID).FirstOrDefault();
                    if (objG != null)
                    {
                        title = "Group Agency Ledger";
                        AgencyAddress = objG.Address;
                        if (objG.NTNNumber == null)
                            AgencyNTN = "NA";
                        else

                            AgencyNTN = objG.NTNNumber;

                        if (objG.GSTNumber == null)
                            AgencySalesTax = "NA";
                        else

                            AgencySalesTax = objG.GSTNumber;

                        AgencyName = objG.GroupName;

                    }

                }
            }
            catch (Exception ex)
            {

                groupagencyID = null;// Convert.ToInt32(ddlAgency.SelectedValue);
            }
            try
            {
                if (ddlAgency.SelectedValue == "0")
                {

                    agencyID = null;
                }
                else
                {
                    title = "Agency Account";
                    agencyID = Convert.ToInt32(ddlAgency.SelectedValue);
                    var objG = db.Agencies.Where(x => x.ID == agencyID && x.Active == true).SingleOrDefault();
                    if (objG != null)
                    {
                        AgencyAddress = objG.Address;
                        AgencyNTN = "NA";
                        AgencySalesTax = "NA";
                        AgencyName = objG.AgencyName;

                    }


                }
            }
            catch (Exception ex)
            {
                agencyID = null;// Convert.ToInt32(ddlAgency.SelectedValue);
            }

            try
            {
                if (txtSearchROMODateFrom.Text.Length == 0 || txtSearchROMODateTo.Text.Length == 0)
                {
                    StartDate = null;// Helper.SetDateFormat("01/01/1985");
                    EnDate = null;// Helper.SetDateFormat(DateTime.Now.ToString("dd/MM/yyyy"));// txtSearchROMODateTo.Text);
                }
                else
                {
                    StartDate = Helper.SetDateFormat(txtSearchROMODateFrom.Text);
                    EnDate = Helper.SetDateFormat(txtSearchROMODateTo.Text);
                }

            }
            catch (Exception)
            {
                StartDate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Month, 1);
                EnDate = StartDate.Value.AddMonths(1).AddDays(-1);

            }
            try
            {
                var ss = db.usp_GetLedgerReportWithMaster(companyID, StartDate, EnDate, agencyID, null, groupagencyID).ToList();

                if (ss.Count > 0)
                {
                    using (XLWorkbook wbb = new XLWorkbook())
                    {
                        DataTable dt = new DataTable();

                        //foreach (var item in ss.First().GetType().GetProperties())
                        //{
                        //    dt.Columns.Add(item.Name);
                        //}

                        //foreach (var row in ss)
                        //{
                        //    var datarow = dt.NewRow();
                        //    foreach (var prop in row.GetType().GetProperties())
                        //    {
                        //        datarow[prop.Name] = prop.GetValue(row, null);
                        //    }

                        //    dt.Rows.Add(datarow);
                        //}

                        if (ddlCompany.SelectedValue.ToString() == "9")
                        {

                            // Define the columns in the desired order
                            dt.Columns.Add("InvoiceDate");
                            dt.Columns.Add("CRVROINV");
                            dt.Columns.Add("PaymentNumber");
                            dt.Columns.Add("ClientName");
                            dt.Columns.Add("BillAmount");
                            dt.Columns.Add("ReceiptAmount");
                            dt.Columns.Add("NetBalance");

                            // Add rows to the DataTable
                            foreach (var row in ss)
                            {
                                var datarow = dt.NewRow();
                                datarow["InvoiceDate"] = row.GetType().GetProperty("InvoiceDate")?.GetValue(row, null);
                                datarow["CRVROINV"] = row.GetType().GetProperty("CRVROINV")?.GetValue(row, null);
                                datarow["PaymentNumber"] = row.GetType().GetProperty("PaymentNumber")?.GetValue(row, null);
                                datarow["ClientName"] = row.GetType().GetProperty("ClientName")?.GetValue(row, null);
                                datarow["BillAmount"] = row.GetType().GetProperty("BillAmount")?.GetValue(row, null);
                                datarow["ReceiptAmount"] = row.GetType().GetProperty("ReceiptAmount")?.GetValue(row, null);
                                datarow["NetBalance"] = row.GetType().GetProperty("NetBalance")?.GetValue(row, null);

                                dt.Rows.Add(datarow);
                            }


                            //dt.Columns.Remove("AgencyId");
                            //dt.Columns.Remove("ClientId");
                            //dt.Columns.Remove("INVCRVID");
                            //dt.Columns.Remove("CompanyID");
                            //dt.Columns.Remove("ClientBalance");
                            //dt.Columns.Remove("AgencyName");
                            //dt.Columns.Remove("CellNumber");
                            //dt.Columns.Remove("PhoneNumber");
                            //dt.Columns.Remove("FaxNumber");
                            //dt.Columns.Remove("Address");
                            //dt.Columns.Remove("CityName");
                            //dt.Columns.Remove("ReleaseOrderID");
                            //dt.Columns.Remove("Company_Name");
                            //dt.Columns.Remove("IsCRV");
                            //dt.Columns.Remove("IsTax");
                            //dt.Columns.Remove("StatusId");
                            //dt.Columns.Remove("CRVId");
                            //dt.Columns.Remove("GroupID");
                            //dt.Columns.Remove("CreatedOn");
                            //dt.Columns.Remove("TransactionDate");

                            wbb.Worksheets.Add(dt, "New");
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;filename=DigitalPrintLedger.xlsx");
                        }
                        else if (ddlCompany.SelectedValue.ToString() == "0")
                        {

                            // Define the columns in the desired order
                            dt.Columns.Add("AgencyName");
                            dt.Columns.Add("InvoiceDate");
                            dt.Columns.Add("CRVROINV");
                            dt.Columns.Add("PaymentNumber");
                            dt.Columns.Add("ClientName");
                            dt.Columns.Add("BillAmount");
                            dt.Columns.Add("ReceiptAmount");
                            dt.Columns.Add("NetBalance");

                            // Add rows to the DataTable
                            foreach (var row in ss)
                            {
                                var datarow = dt.NewRow();
                                datarow["AgencyName"] = row.GetType().GetProperty("AgencyName")?.GetValue(row, null);
                                datarow["InvoiceDate"] = row.GetType().GetProperty("InvoiceDate")?.GetValue(row, null);
                                datarow["CRVROINV"] = row.GetType().GetProperty("CRVROINV")?.GetValue(row, null);
                                datarow["PaymentNumber"] = row.GetType().GetProperty("PaymentNumber")?.GetValue(row, null);
                                datarow["ClientName"] = row.GetType().GetProperty("ClientName")?.GetValue(row, null);
                                datarow["BillAmount"] = row.GetType().GetProperty("BillAmount")?.GetValue(row, null);
                                datarow["ReceiptAmount"] = row.GetType().GetProperty("ReceiptAmount")?.GetValue(row, null);
                                datarow["NetBalance"] = row.GetType().GetProperty("NetBalance")?.GetValue(row, null);

                                dt.Rows.Add(datarow);
                            }


                            // dt.Columns.Remove("AgencyId");
                            // dt.Columns.Remove("ClientId");
                            // dt.Columns.Remove("INVCRVID");
                            // dt.Columns.Remove("CompanyID");
                            // dt.Columns.Remove("ClientBalance");
                            //// dt.Columns.Remove("AgencyName");
                            // dt.Columns.Remove("CellNumber");
                            // dt.Columns.Remove("PhoneNumber");
                            // dt.Columns.Remove("FaxNumber");
                            // dt.Columns.Remove("Address");
                            // dt.Columns.Remove("CityName");
                            // dt.Columns.Remove("ReleaseOrderID");
                            // dt.Columns.Remove("Company_Name");
                            // dt.Columns.Remove("IsCRV");
                            // dt.Columns.Remove("IsTax");
                            // dt.Columns.Remove("StatusId");
                            // dt.Columns.Remove("CRVId");
                            // dt.Columns.Remove("GroupID");
                            // dt.Columns.Remove("CreatedOn");
                            // dt.Columns.Remove("TransactionDate");

                            wbb.Worksheets.Add(dt, "New");
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;filename=DigitalLedger.xlsx");
                        }
                        else
                        {

                            // Define the columns in the desired order
                            dt.Columns.Add("InvoiceDate");
                            dt.Columns.Add("CRVROINV");
                            dt.Columns.Add("PaymentNumber");
                            dt.Columns.Add("ClientName");
                            dt.Columns.Add("BillAmount");
                            dt.Columns.Add("ReceiptAmount");
                            dt.Columns.Add("NetBalance");

                            // Add rows to the DataTable
                            foreach (var row in ss)
                            {
                                var datarow = dt.NewRow();
                                datarow["InvoiceDate"] = row.GetType().GetProperty("InvoiceDate")?.GetValue(row, null);
                                datarow["CRVROINV"] = row.GetType().GetProperty("CRVROINV")?.GetValue(row, null);
                                datarow["PaymentNumber"] = row.GetType().GetProperty("PaymentNumber")?.GetValue(row, null);
                                datarow["ClientName"] = row.GetType().GetProperty("ClientName")?.GetValue(row, null);
                                datarow["BillAmount"] = row.GetType().GetProperty("BillAmount")?.GetValue(row, null);
                                datarow["ReceiptAmount"] = row.GetType().GetProperty("ReceiptAmount")?.GetValue(row, null);
                                datarow["NetBalance"] = row.GetType().GetProperty("NetBalance")?.GetValue(row, null);

                                dt.Rows.Add(datarow);
                            }


                            //dt.Columns.Remove("AgencyId");
                            //dt.Columns.Remove("ClientId");
                            //dt.Columns.Remove("INVCRVID");
                            //dt.Columns.Remove("CompanyID");
                            //dt.Columns.Remove("ClientBalance");
                            //dt.Columns.Remove("AgencyName");
                            //dt.Columns.Remove("CellNumber");
                            //dt.Columns.Remove("PhoneNumber");
                            //dt.Columns.Remove("FaxNumber");
                            //dt.Columns.Remove("Address");
                            //dt.Columns.Remove("CityName");
                            //dt.Columns.Remove("ReleaseOrderID");
                            //dt.Columns.Remove("Company_Name");
                            //dt.Columns.Remove("IsCRV");
                            //dt.Columns.Remove("IsTax");
                            //dt.Columns.Remove("StatusId");
                            //dt.Columns.Remove("CRVId");
                            //dt.Columns.Remove("GroupID");
                            //dt.Columns.Remove("CreatedOn");
                            //dt.Columns.Remove("TransactionDate");

                            wbb.Worksheets.Add(dt, "New");
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;filename=ExpressPrintLedger.xlsx");
                        }

                        using (MemoryStream mymemoryStream = new MemoryStream())
                        {
                            wbb.SaveAs(mymemoryStream);
                            mymemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }

        }
    }
}