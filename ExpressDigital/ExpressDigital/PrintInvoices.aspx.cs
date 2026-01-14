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
    public partial class PrintInvoices : System.Web.UI.Page
    {
        static bool _isSqlTypesLoaded = false;

        DbDigitalEntities db = new DbDigitalEntities();
        // GLEntities db2 = new GLEntities();
      
        public PrintInvoices()
        {
            if (!_isSqlTypesLoaded)
            {
                SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~"));
                _isSqlTypesLoaded = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime? StartDate;
                DateTime? EnDate;
                StartDate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Month, 1);
                EnDate = StartDate.Value.AddMonths(1).AddDays(-1);

                txtSearchROMODateFrom.Text = StartDate.Value.ToString("dd/MM/yyyy");
                txtSearchROMODateTo.Text = EnDate.Value.ToString("dd/MM/yyyy");

                var city = db.CityManagements.Where(x => x.IsActive == true).OrderBy(x => x.CityName).ToList();
                ddlCity.DataValueField = "ID";
                ddlCity.DataTextField = "CityName";
                ddlCity.DataSource = city;
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("All", "0"));
                ddlCity_SelectedIndexChanged(null, null);


                var com = db.Companies.Where(x => x.Active == true).OrderBy(x => x.Company_Name).ToList();
                ddlCompany.DataValueField = "Company_Id";
                ddlCompany.DataTextField = "Company_Name";
                ddlCompany.DataSource = com;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("All", "0"));
                ddlCity_SelectedIndexChanged(null, null);


                var sts = db.PaymentStatus.ToList();
                ddlstatus.DataValueField = "ID";
                ddlstatus.DataTextField = "PaymentType";
                ddlstatus.DataSource = sts;
                ddlstatus.DataBind();
                ddlstatus.Items.Insert(0, new ListItem("All", "0"));







                //var sts = db.CRVStatus.Where(x=> x.Active == true).ToList();
                //ddlstatus.DataValueField = "ID";
                //ddlstatus.DataTextField = "Status";
                //ddlstatus.DataSource = sts;
                //ddlstatus.DataBind();
                //ddlstatus.Items.Insert(0, new ListItem("All", "0"));

                //if (ddlInvoiceStatus.SelectedValue.ToString() != "2")
                //{
                //    // btnExecute.Enabled = false;
                //}
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            Int32? AgencyId;
            Int32? ClinetId;
            Int32? companyid;
            Int32? masteragencyid;
            Int32? invstatus = null;
            //String strchk = null;
            //string  strCRV = null;
            Int32? cityid;
            Int32? invoiceid = null;
            DateTime?    StartDate = null;
            DateTime?   EnDate = null;

            if (txtinvoiceid.Text.Length == 0 )
            {
                invoiceid = null;
                invoiceid = null;
            }
            else
            {
                invoiceid = Convert.ToInt32(txtinvoiceid.Text);
            }


            if (ddlCity.SelectedIndex == 0)
                cityid = null;
            else
                cityid = Convert.ToInt32(ddlCity.SelectedValue);

            if (ddlstatus.SelectedIndex == 0)
                invstatus = null;
            else
                invstatus = Convert.ToInt32(ddlstatus.SelectedValue);

            if (ddlCompany.SelectedIndex == 0)
                companyid = null;
            else
                companyid = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlmasteragency.SelectedIndex == 0)
            {
                masteragencyid = null;
            }
            else
                masteragencyid = Convert.ToInt32(ddlmasteragency.SelectedValue);


            if (ddlAgency.SelectedIndex == 0)
            {
                AgencyId = null;
            }
            else
                AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);

            if (ddlClients.SelectedIndex == 0)
            {
                ClinetId = null;
            }
            else
                ClinetId = Convert.ToInt32(ddlClients.SelectedValue);
            try
            {
                if (txtSearchROMODateFrom.Text.Length == 0 || txtSearchROMODateTo.Text.Length == 0)
                {
                    StartDate = null;
                    EnDate = null;
                }
                else
                {
                    StartDate =  Helper.SetDateFormat (txtSearchROMODateFrom.Text) ;
                    EnDate = Helper.SetDateFormat (txtSearchROMODateTo.Text);//lblmessage//.AddHours(12);
                }
            }


            catch (Exception)
            {
                StartDate = null;
                EnDate = null;
            }

            try
            {
                if (ddlstatus.SelectedIndex == 4)
                {
                    var ov2 = db.printinvoices_paymentsttaus(companyid, cityid, masteragencyid, AgencyId, ClinetId, invoiceid, StartDate, EnDate, "", null).ToList();
                    string myWords2 = "";

                    ReportViewer1.LocalReport.DataSources.Clear();
                    if (ddlCompany.SelectedValue == "0")
                    {
                        myWords2 = " (  Print All Invoices  )";
                    }

                    else
                        myWords2 = " (" + ddlCompany.SelectedItem.Text + " )";

                    ReportParameter[] rp2 = new ReportParameter[5];

                    if (ddlCompany.SelectedValue.ToString() == "9")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/VBilling_Digital.rdlc";
                    }
                    else
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/VBilling_Express.rdlc";

                    }

                    string _client2, _Agency2;
                    _client2 = (ddlClients.SelectedValue == "0" ? "All Clients" : ddlClients.SelectedItem.Text);
                    _Agency2 = (ddlAgency.SelectedValue == "0" ? "All Agencies" : ddlAgency.SelectedItem.Text);
                    rp2[0] = new ReportParameter("pmCompany", myWords2);
                    rp2[1] = new ReportParameter("pmFromdate", txtSearchROMODateFrom.Text);
                    rp2[2] = new ReportParameter("pmTodate", txtSearchROMODateTo.Text);
                    rp2[3] = new ReportParameter("pmAgency", _Agency2);   //IIf |( ddlAgency.SelectedItem.Text);
                    rp2[4] = new ReportParameter("pmClient", _client2);

                    ReportDataSource rds2 = new ReportDataSource("DSBilling", ov2);
                    ReportViewer1.LocalReport.SetParameters(rp2);
                    ReportViewer1.LocalReport.DataSources.Add(rds2);
                    ReportViewer1.LocalReport.Refresh();
                }

                else
                {
                    var ov = db.usp_PrintAllInvoices(companyid, cityid, masteragencyid, AgencyId, ClinetId, invoiceid, StartDate, EnDate, "", invstatus).ToList();

                    string myWords = "";

                    ReportViewer1.LocalReport.DataSources.Clear();
                    if (ddlCompany.SelectedValue == "0")
                    {
                        myWords = " (  Print All Invoices  )";
                    }

                    else
                        myWords = " (" + ddlCompany.SelectedItem.Text + " )";

                    ReportParameter[] rp = new ReportParameter[5];

                    if (ddlCompany.SelectedValue.ToString() == "9")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/VBilling_Digital.rdlc";
                    }
                    else
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/VBilling_Express.rdlc";

                    }

                    string _client, _Agency;
                    _client = (ddlClients.SelectedValue == "0" ? "All Clients" : ddlClients.SelectedItem.Text);
                    _Agency = (ddlAgency.SelectedValue == "0" ? "All Agencies" : ddlAgency.SelectedItem.Text);
                    rp[0] = new ReportParameter("pmCompany", myWords);
                    rp[1] = new ReportParameter("pmFromdate", txtSearchROMODateFrom.Text);
                    rp[2] = new ReportParameter("pmTodate", txtSearchROMODateTo.Text);
                    rp[3] = new ReportParameter("pmAgency", _Agency);   //IIf |( ddlAgency.SelectedItem.Text);
                    rp[4] = new ReportParameter("pmClient", _client);

                    ReportDataSource rds = new ReportDataSource("DSBilling", ov);
                    ReportViewer1.LocalReport.SetParameters(rp);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.Refresh();
                }

            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message;
            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
        }

        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Agencyid = Convert.ToInt32(ddlAgency.SelectedValue);
            var c = db.Clients.Where(x => x.AgencyID == Agencyid).ToList().OrderBy(x => x.Client1);
            ddlClients.DataValueField = "ID";
            ddlClients.DataTextField = "Client1";
            ddlClients.DataSource = c;
            ddlClients.DataBind();
            ddlClients.Items.Insert(0, new ListItem("Select Client", "0"));

        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(ddlCity.SelectedValue);
            var g = db.GroupAgencies.Where(x => x.CityID == id).OrderBy(x => x.GroupName).ToList();
            ddlmasteragency.DataValueField = "ID";
            ddlmasteragency.DataTextField = "GroupName";
            ddlmasteragency.DataSource = g;
            ddlmasteragency.DataBind();
            ddlmasteragency.Items.Insert(0, new ListItem("Select Agency", "0"));
            ddlmasteragency_SelectedIndexChanged(null, null);
        }

        protected void ddlmasteragency_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlmasteragency.SelectedValue);
            var g = db.Agencies.Where(x => x.GroupID == id).OrderBy(x => x.AgencyName).ToList();
            ddlAgency.DataValueField = "ID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = g;
            ddlAgency.DataBind();
            ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
            ddlAgency_SelectedIndexChanged(null, null);

        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            Int32? AgencyId;
            Int32? ClinetId;
            Int32? companyid;
            Int32? masteragencyid;
            Int32? invstatus = null;
            //String strchk = null;
            //string  strCRV = null;
            Int32? cityid;
            Int32? invoiceid = null;
            DateTime? StartDate = null;
            DateTime? EnDate = null;

            if (txtinvoiceid.Text.Length == 0)
            {
                invoiceid = null;
                invoiceid = null;
            }
            else
            {
                invoiceid = Convert.ToInt32(txtinvoiceid.Text);
            }


            if (ddlCity.SelectedIndex == 0)
                cityid = null;
            else
                cityid = Convert.ToInt32(ddlCity.SelectedValue);

            if (ddlstatus.SelectedIndex == 0)
                invstatus = null;
            else
                invstatus = Convert.ToInt32(ddlstatus.SelectedValue);

            if (ddlCompany.SelectedIndex == 0)
                companyid = null;
            else
                companyid = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlmasteragency.SelectedIndex == 0)
            {
                masteragencyid = null;
            }
            else
                masteragencyid = Convert.ToInt32(ddlmasteragency.SelectedValue);


            if (ddlAgency.SelectedIndex == 0)
            {
                AgencyId = null;
            }
            else
                AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);

            if (ddlClients.SelectedIndex == 0)
            {
                ClinetId = null;
            }
            else
                ClinetId = Convert.ToInt32(ddlClients.SelectedValue);
            try
            {
                if (txtSearchROMODateFrom.Text.Length == 0 || txtSearchROMODateTo.Text.Length == 0)
                {
                    StartDate = null;
                    EnDate = null;
                }
                else
                {
                    StartDate = Helper.SetDateFormat(txtSearchROMODateFrom.Text);
                    EnDate = Helper.SetDateFormat(txtSearchROMODateTo.Text);//lblmessage//.AddHours(12);
                }
            }


            catch (Exception)
            {
                StartDate = null;
                EnDate = null;
            }

            try
            {

                var ov = db.usp_PrintAllInvoices(companyid, cityid, masteragencyid, AgencyId, ClinetId, invoiceid, StartDate, EnDate, "", invstatus).ToList();
                if (ov.Count > 0)
                {
                    using (XLWorkbook wbb = new XLWorkbook())
                    {
                        DataTable dt = new DataTable();
                        //foreach (var item in ov.First().GetType().GetProperties())
                        //{
                        //    dt.Columns.Add(item.Name);
                        //}

                        //dt.Columns.Add("RevenueBeforeGST", typeof(decimal));
                        //dt.Columns.Add("GrossWithGST", typeof(decimal));
                        //dt.Columns.Add("Revenue", typeof(decimal));


                        //foreach (var row in ov)
                        //{
                        //    var datarow = dt.NewRow();
                        //    foreach (var prop in row.GetType().GetProperties())
                        //    {
                        //        datarow[prop.Name] = prop.GetValue(row, null);
                        //    }
                        //    decimal gross = Convert.ToDecimal(row.GetType().GetProperty("GrossAmount").GetValue(row, null));
                        //    decimal gst = Convert.ToDecimal(row.GetType().GetProperty("GSTAmount").GetValue(row, null));
                        //    decimal agc = Convert.ToDecimal(row.GetType().GetProperty("AGCAmount").GetValue(row, null));
                        //    datarow["RevenueBeforeGST"] = gross-gst;
                        //    datarow["GrossWithGST"] = gross + gst;
                        //    datarow["Revenue"] = (gross + gst)-agc;

                        //    dt.Rows.Add(datarow);
                        //}


                        // Define the columns in the desired order
                        dt.Columns.Add("InviceDate");
                        dt.Columns.Add("PaymentType");
                        dt.Columns.Add("invoiceID");
                        dt.Columns.Add("ReleaseOrderNumber");
                        dt.Columns.Add("AgencyName");
                        dt.Columns.Add("Client");
                        dt.Columns.Add("GrossAmount");
                        dt.Columns.Add("AGCAmount");
                        dt.Columns.Add("GSTAmount");
                        dt.Columns.Add("RevenueBeforeGST", typeof(decimal));
                        dt.Columns.Add("GrossWithGST", typeof(decimal));
                        dt.Columns.Add("Revenue", typeof(decimal));
                        dt.Columns.Add("Company_Name");
                        dt.Columns.Add("PortalName");

                        // Add rows to the DataTable
                        foreach (var row in ov)
                        {
                            var datarow = dt.NewRow();
                            datarow["InviceDate"] = row.GetType().GetProperty("InviceDate")?.GetValue(row, null);
                            datarow["PaymentType"] = row.GetType().GetProperty("PaymentType")?.GetValue(row, null);
                            datarow["invoiceID"] = row.GetType().GetProperty("invoiceID")?.GetValue(row, null);
                            datarow["ReleaseOrderNumber"] = row.GetType().GetProperty("ReleaseOrderNumber")?.GetValue(row, null);
                            datarow["AgencyName"] = row.GetType().GetProperty("AgencyName")?.GetValue(row, null);
                            datarow["Client"] = row.GetType().GetProperty("Client")?.GetValue(row, null);
                            datarow["GrossAmount"] = row.GetType().GetProperty("GrossAmount")?.GetValue(row, null);
                            datarow["AGCAmount"] = row.GetType().GetProperty("AGCAmount")?.GetValue(row, null);
                            datarow["GSTAmount"] = row.GetType().GetProperty("GSTAmount")?.GetValue(row, null);
                            decimal gross = Convert.ToDecimal(row.GetType().GetProperty("GrossAmount").GetValue(row, null));
                            decimal gst = Convert.ToDecimal(row.GetType().GetProperty("GSTAmount").GetValue(row, null));
                            decimal agc = Convert.ToDecimal(row.GetType().GetProperty("AGCAmount").GetValue(row, null));
                            datarow["RevenueBeforeGST"] = gross - gst;
                            datarow["GrossWithGST"] = gross + gst;
                            datarow["Revenue"] = (gross + gst) - agc;
                            datarow["Company_Name"] = row.GetType().GetProperty("Company_Name")?.GetValue(row, null);
                            datarow["PortalName"] = row.GetType().GetProperty("PortalName")?.GetValue(row, null);

                            dt.Rows.Add(datarow);
                        }


                       // dt.Columns.Remove("ReleaseOrderID");
                       // dt.Columns.Remove("NetReceiable");
                       // dt.Columns.Remove("RecivedAmount");
                       // dt.Columns.Remove("BalanceAmount");
                       // dt.Columns.Remove("PaymentStatusID");
                       // dt.Columns.Remove("ClientID");
                       // dt.Columns.Remove("CompanyID");
                       // dt.Columns.Remove("AgencyID");
                       // //dt.Columns.Remove("PaymentNumber");
                       // dt.Columns.Remove("IsCancelled");
                       // dt.Columns.Remove("CancelledBy");
                       // dt.Columns.Remove("CancelledOn");
                       // dt.Columns.Remove("dollarinvoice");
                       // dt.Columns.Remove("FromDate");
                       // dt.Columns.Remove("ToDate");
                       // dt.Columns.Remove("NetAmount");
                       // dt.Columns.Remove("InvoiceGross");
                       // dt.Columns.Remove("GSTNo");
                       // dt.Columns.Remove("NICNo");
                       // dt.Columns.Remove("NTNNo");
                       // dt.Columns.Remove("clientAddress");
                       // dt.Columns.Remove("ClientCountyID");
                       // dt.Columns.Remove("StateID");
                       // dt.Columns.Remove("ClientCityID");
                       // dt.Columns.Remove("ClientContact");
                       // dt.Columns.Remove("ClientEmail");
                       // dt.Columns.Remove("IsExempted");
                       // dt.Columns.Remove("PhoneNumber");
                       // dt.Columns.Remove("CellNumber");
                       // dt.Columns.Remove("AgencyFaxnumber");
                       // dt.Columns.Remove("AgencyEmail");
                       // dt.Columns.Remove("AgencyAddress");
                       // dt.Columns.Remove("AgencyClinet");
                       // dt.Columns.Remove("AgencyCityID");
                       // dt.Columns.Remove("GSTPercentage");
                       // dt.Columns.Remove("GSTNumber");
                       // dt.Columns.Remove("CommPercentage");
                       // dt.Columns.Remove("NTNNumber");
                       // dt.Columns.Remove("Compaign");
                       // dt.Columns.Remove("Remarks");
                       // dt.Columns.Remove("CreatedBy");
                       //// dt.Columns.Remove("InviceDate");
                       // dt.Columns.Remove("FOCPAID");
                       // dt.Columns.Remove("ROExternal");
                       // dt.Columns.Remove("ROMPDate");
                       // dt.Columns.Remove("CamStartDate");
                       // dt.Columns.Remove("CamEndDate");
                       // dt.Columns.Remove("ReleaseOrderReferenceID");
                       // dt.Columns.Remove("ReleaseOrderDetailId");
                       // dt.Columns.Remove("CurrencyID");
                       // dt.Columns.Remove("CurrencyValue");
                       // dt.Columns.Remove("ConversionRate");
                       // dt.Columns.Remove("CPMRate");
                       // dt.Columns.Remove("Discount");
                       // dt.Columns.Remove("D_NetAmount");
                       // dt.Columns.Remove("d_NetReceiable");
                       // dt.Columns.Remove("d_GSTPercentage");
                       // dt.Columns.Remove("d_GSTAmount");
                       // dt.Columns.Remove("AGCPercentage");
                       // dt.Columns.Remove("AGCommission");
                       // dt.Columns.Remove("d_CreatedBy");
                       // dt.Columns.Remove("TotalBilled");
                       // dt.Columns.Remove("Impressions");
                       // dt.Columns.Remove("IsBilled");
                       // dt.Columns.Remove("PortalID");
                       // dt.Columns.Remove("GroupID");
                       // dt.Columns.Remove("GroupName");
                       // dt.Columns.Remove("PaymentID");
                       

                        wbb.Worksheets.Add(dt, "New");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                      
                        if (ddlCompany.SelectedValue.ToString() == "9")
                        {
                            Response.AddHeader("content-disposition", "attachment;filename=VBilling_Digital.xlsx");
                        }
                        else
                        {
                            Response.AddHeader("content-disposition", "attachment;filename=VBilling_Express.xlsx");
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