using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class Acknowlegement : System.Web.UI.Page
    {
        private string InvoiceNumbers = "";
        static bool _isSqlTypesLoaded = false;
        DbDigitalEntities db = new DbDigitalEntities();

        public Acknowlegement()
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
                txtPrintDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                var gl = db.Companies.Where(x => x.Active == true).ToList();
                ddlcompany.DataValueField = "Company_Id";
                ddlcompany.DataTextField = "Company_Name";
                ddlcompany.DataSource = gl;
                ddlcompany.DataBind();
                //ddlcompany.Items.Insert(0, new ListItem("Select Company", "0"));

                var g = db.GroupAgencies.OrderBy(x => x.GroupName).ToList();

                var ag = db.Agencies.Where(x => x.Active == true).OrderBy(x => x.AgencyName).ToList();
                chkagency.DataValueField = "ID";
                chkagency.DataTextField = "AgencyName";
                chkagency.DataSource = ag;
                chkagency.DataBind();

                txtSearchROMODateFrom.Text = DateTime.Now.AddDays(-31).ToString("dd/MM/yyyy");
                txtSearchROMODateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                btnSearch_Click(null, null);

                

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            //if (ddlcompany.SelectedIndex > 0)
            {
                int companyid = Convert.ToInt32(ddlcompany.SelectedValue);
                string strIRO;
                string strReleaseOrderID;

                string strClinet;
                string strCampaign;
                string strExternal;

                DateTime? StartDate;
                DateTime? EnDate;
                if (txtIRO.Text.Length == 0)
                    strIRO = null;
                else
                    strIRO = txtIRO.Text;


                if (txtRefNumber.Text.Length == 0)
                    strExternal = null;
                else
                    strExternal = txtRefNumber.Text;

                if (txtSearchReleaseOrder.Text.Length == 0)
                    strReleaseOrderID = null;
                else
                    strReleaseOrderID = txtSearchReleaseOrder.Text;

                string txt = null;
                foreach (ListItem item in chkagency.Items)
                {
                    if (item.Selected)
                    {
                        txt = txt + item.Value.ToString() + ";";
                    }
                }
                //try
                //{
                //    if (txt.Length > 2)
                //        txt = txt.Substring(0, txt.Length - 1);
                //}
                //catch (Exception)
                //{
                //    txt = null;
                //}

                if (txtClient.Text.Length == 0)
                    strClinet = null;
                else
                    strClinet = txtClient.Text;
                if (txtcampaign.Text.Length == 0)
                    strCampaign = null;
                else
                    strCampaign = txtcampaign.Text;
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
                        EnDate = Helper.SetDateFormat(txtSearchROMODateTo.Text);
                    }
                }
                catch (Exception)
                {
                    StartDate = null;
                    EnDate = null;
                }

                var s = db.usp_GetDataForAcknowledgeWithAgencyList(strReleaseOrderID, strExternal, strIRO, StartDate, EnDate, txt, strClinet, strCampaign, true, companyid).ToList();
                DataTable dt = Helper.ToDataTable(s);
                ViewState["dt"] = dt;
                gv.DataSource = s;
                gv.DataBind();
            }
            //else
            //{
            //    lblmessage.Text = "Pease select company";
            //    return;
            //}
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.PageIndex = e.NewPageIndex;
            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnprint_Click(object sender, EventArgs e)
        {

            lblmessage.Text = string.Empty;
            //if (ddlcompany.SelectedIndex > 0)
            {
                GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
                Int32 InvoiceID = Convert.ToInt32(gv.DataKeys[row.RowIndex].Values[3]);
                Int32 companyid = Convert.ToInt32(ddlcompany.SelectedValue);

                if (chkAll.Checked == true)
                {
                    string txt = null;
                    foreach (GridViewRow item in gv.Rows)
                    {
                        CheckBox chk = (CheckBox)item.FindControl("chk");
                        if (chk.Checked)
                        {
                            txt = txt + item.Cells[0].Text + ";";
                        }
                    }
                    try
                    {
                        if (txt.Length > 2)
                            txt = txt.Substring(0, txt.Length - 1);
                    }
                    catch (Exception)
                    {
                        txt = null;
                    }
                    printReport(txt, companyid);
                }
                else
                    printReportSingle(InvoiceID, companyid);
            }
        }
        private void printReport(string agtxt, int companyid)
        {
            try
            {
                var s = db.usp_Ack_WithCompany(0, companyid, agtxt).ToList();

                AcknowledgeReference ack = new AcknowledgeReference();
                var AckID = db.usp_AckIDctr().SingleOrDefault(); ;
                if (companyid == 1)
                    AckID = "EP-" + AckID.ToString();
                else
                    AckID = "ED-" + AckID.ToString();
                foreach (var inv in s)
                {
                    ack = new AcknowledgeReference();
                    var AID = db.usp_IDctr("Acknowledgement").SingleOrDefault().Value;
                    ack.ID = Convert.ToInt32(AID);
                    ack.InvoiceNo = inv.InvoiceID;
                    ack.ReferenceNo = AckID.ToString();
                    ack.CreatedDate = DateTime.Now;
                    ack.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                    ack.AcknowledgeDate = Helper.SetDateFormat(txtPrintDate.Text);

                    db.AcknowledgeReferences.Add(ack);
                    db.SaveChanges();
                }

                ReportParameter[] rp = new ReportParameter[3];// ("pmPrintDate",txtPrintDate.Text );
                rp[0] = new ReportParameter("pmPrintDate", txtPrintDate.Text);
                rp[1] = new ReportParameter("pmAcknowledge", DateTime.Now.ToString("dd/MM/yyyy"));
                rp[2] = new ReportParameter("pmReference", AckID.ToString());

                if (companyid == 1)
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/PrintInvoice_Ack_Century.rdlc";
                }
                else
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/PrintInvoice_Ack.rdlc";
                }
                ReportDataSource rds = new ReportDataSource("DsAcknowledge", s);
                ReportViewer1.LocalReport.SetParameters(rp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                //using (DbDigitalEntities context = new DbDigitalEntities())
                //{
                //    string txt = null;
                //    foreach (ListItem item in chkagency.Items)
                //    {
                //        if (item.Selected)
                //        {
                //            txt = txt + item.Value.ToString();
                //        }
                //    }


                //    tblDocument newdocument = new tblDocument
                //    {
                //        AgencyID = Convert.ToInt32(txt),
                //        CreationDate = Helper.SetDateFormat(txtPrintDate.Text),
                //        ReferenceNumber=AckID,
                //        Status="Dispatched",
                //        PersonName= ((UserInfo)Session["UserObject"]).UserName.ToString()
                //    };
                //    context.tblDocuments.Add(newdocument);
                //    db.SaveChanges();

                //    int documenid = newdocument.DocumentID;
                    

                //}
            }
            catch (Exception ex)
            {
                string txtt = ex.InnerException.Message;
            }
        }

        private void printReportSingle(int InvoiceID, int companyid)
        {
            try
            {
                
                var ss = db.usp_AckByInvoiceWithCompany(InvoiceID, companyid).Take(1).SingleOrDefault();
                var s = db.usp_AckByInvoiceWithCompany(InvoiceID, companyid).ToList();
                AcknowledgeReference ack = new AcknowledgeReference();
                var AckID = db.usp_AckIDctr().SingleOrDefault(); ;
                if (companyid == 1)
                    AckID = "EP-" + AckID.ToString();
                else
                    AckID = "ED-" + AckID.ToString();
                foreach (var inv in s)
                {
                    var AID = db.usp_IDctr("Acknowledgement").SingleOrDefault().Value;
                    ack.ID = Convert.ToInt32(AID);
                    ack.InvoiceNo = inv.InvoiceID;
                    ack.ReferenceNo = AckID.ToString();
                    ack.CreatedDate = DateTime.Now;
                    ack.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                    ack.AcknowledgeDate = Helper.SetDateFormat(txtPrintDate.Text);

                    db.AcknowledgeReferences.Add(ack);
                    db.SaveChanges();
                }
                ReportParameter[] rp = new ReportParameter[3];
                rp[0] = new ReportParameter("pmPrintDate", txtPrintDate.Text);
                rp[1] = new ReportParameter("pmAcknowledge", DateTime.Now.ToString("dd/MM/yyyy"));
                rp[2] = new ReportParameter("pmReference", AckID.ToString());

                if (companyid == 1)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/PrintInvoice_Ack_Century.rdlc";
                else
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports") + "/PrintInvoice_Ack.rdlc";

                ReportDataSource rds = new ReportDataSource("DsAcknowledge", s);
                ReportViewer1.LocalReport.SetParameters(rp);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
            catch (Exception ex)
            {
                string txt = ex.InnerException.Message;
            }
        }

        
        //private void savedetaildocument(int documentid)
        //{
        //    using(DbDigitalEntities context=new DbDigitalEntities())
        //    {
        //        string txt = null;
        //        foreach (ListItem item in chkagency.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                txt = txt + item.Value.ToString();
        //            }
        //        }

        //        DataTable dtinvoicedata=new DataTable();

        //       // dtinvoicedata = db.usp_GetInvoicesValidationData_InvoiceNo(txt,);
        //    }
        //}
    }
}