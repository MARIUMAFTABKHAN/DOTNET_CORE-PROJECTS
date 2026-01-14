using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using AjaxControlToolkit;
using System.Globalization;

namespace ExpressDigital
{
    public partial class BookReleaseOrder : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtReleaseOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                hduid.Value = ((UserInfo)Session["UserObject"]).ID.ToString(); //Session["UserID"].ToString();
                DataTable dt = new DataTable();
                //. lst.Add(obj);
                //fileUpload.Attributes.Add("disabled", "true");               
                dt.Columns.Add("ID");
                dt.Columns.Add("CurrencyID");
                dt.Columns.Add("CurrencyValue", typeof(Decimal));
                dt.Columns.Add("ConversionRate", typeof(Decimal));
                dt.Columns.Add("CPMRate");
                dt.Columns.Add("PortalId");
                dt.Columns.Add("AGCPercentage", typeof(Decimal));
                dt.Columns.Add("GSTPercentage", typeof(Decimal));
                dt.Columns.Add("Weight");
                dt.Columns.Add("Impressions");
                dt.Columns.Add("StartDate");
                dt.Columns.Add("EndDate");
                dt.Columns.Add("NetAmount", typeof(Decimal));
                dt.Columns.Add("Discount", typeof(Decimal));
                dt.Columns.Add("GSTAmount", typeof(Decimal));
                dt.Columns.Add("AGCommission", typeof(Decimal));
                dt.Columns.Add("DeleiveredExpresssions");
                dt.Columns.Add("TotalBilled", typeof(Decimal));
                dt.AcceptChanges();
                ViewState["dt"] = dt;

                BindControls();
                RestControl();

                if (Request.QueryString.Count > 0)
                {
                    try
                    {
                        Int32 RoId = Convert.ToInt32(Request.QueryString["ID"]);
                        FillData(RoId);

                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
        }

        private void FillData(int RoID)
        {
            var obj = db.ReleaseOrderMasters.Where(x => x.ID == RoID).SingleOrDefault();
            if (obj != null)
            {
                if (obj.IsBilled == 0)
                    lblBilled.Text = "Un-Billed";
                else
                {
                    lblBilled.Text = "Billed";
                    btnSave.Enabled = false;

                }
                txtReleaseOrderDate.Text = Convert.ToDateTime(obj.CreatedOn).ToString("dd/MM/yyyy");
                ViewState["releaseorderid"] = RoID.ToString();
                txtReleaseOrder.Text = obj.ReleaseOrderNumber;  //Convert.ToInt32(lblID.Text);
                txtInternalID.Text = obj.ReleaseOrderReferenceID;// txt impressions_E ReleaseOrder.Text;                                                      
                ddlAgency.SelectedValue = obj.AgencyID.ToString();
                ddlAgency_SelectedIndexChanged(null, null);
                ddlFOCPaid.SelectedValue = obj.FOCPAID;
                ddlclient.SelectedValue = obj.ClientID.ToString(); ;
                ddlclient_SelectedIndexChanged(null, null);
                txtCampaign.Text = obj.Compaign;
                try
                {
                    ddlSalesPerson.SelectedValue = obj.SalesPersonID.ToString();
                }
                catch (Exception)
                {
                    ddlSalesPerson.SelectedIndex = 0;
                }
                try
                {
                    txtROMPDate.Text = Convert.ToDateTime(obj.ROMPDate).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    if (txtROMPDate.Text == "01/01/0001")
                        txtROMPDate.Text = string.Empty;
                }
                catch (Exception)
                {
                    txtROMPDate.Text = string.Empty;
                }
                txtExternalID.Text = obj.ROExternal;
                chkIsCancelled.Checked = Convert.ToBoolean(obj.IsCancelled);
                txtRemrks.Text = obj.Remarks;
                lblTotalCommission.Text = obj.INV_AGC.ToString();
                lblTotalDiscount.Text = obj.INV_Discount.ToString();
                lblGrossAmount.Text = obj.INV_Gross.ToString();
                lblTotalGST.Text = obj.INV_GST.ToString();
                lblNetAmount.Text = obj.INV_Net.ToString();
                dtCamStart.Text = Convert.ToDateTime(obj.Cam_startDate).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                {
                    if (dtCamStart.Text == "01/01/0001")
                        dtCamStart.Text = string.Empty; ;
                }
                dtCamEnd.Text = Convert.ToDateTime(obj.Cam_endDate).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                if (dtCamEnd.Text == "01/01/0001")
                    dtCamEnd.Text = string.Empty; ;

                ddlCompany.SelectedValue = obj.CompnayID.ToString();
                btnSave.Text = "Update";
                UpdateDetail(RoID);
                setData();
            }
        }

        private void UpdateDetail(Int32 RoID)
        {

            var objDetail = db.ReleaseOrderDetails.Where(x => x.ReleaseOrderID == RoID).ToList();
            gv.DataSource = objDetail;
            gv.DataBind();
            InitialGrid(RoID);

        }

        private void RestControl()
        {
            InitialGrid(0);
            lblmessage.Visible = true;
            lblmessage.Text = string.Empty;
            txtReleaseOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblagc.Text = System.Configuration.ConfigurationManager.AppSettings["agcvalue"];//  "0.00";
            lblgst.Text = "0"; // System.Configuration.ConfigurationManager.AppSettings["gstvalue"];
            txtagcrate_E.Text = lblagc.Text;
            txtagc_E.Text = "0";
            txtcpm_E.Text = "0";
            txtcr_E.Text = "0";
            txtDeliveredimpressions_E.Text = "0";
            txtdiscount_E.Text = "0";
            txtgross_E.Text = "0";
            txtgstrate_E.Text = "0";
            txtgst_E.Text = "0";
            txtimpressions_E.Text = "0";
            txtnet_E.Text = "0";
            txtusd_E.Text = "0";
            dtfrom_E.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
            if (dtfrom_E.Text == "01/01/0001")
                dtfrom_E.Text = string.Empty; ;

            dtto_E.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
            if (dtfrom_E.Text == "01/01/0001")
                dtfrom_E.Text = string.Empty; ;
            btnSave.Enabled = true;
            ddlMasterGroup.SelectedIndex = 0;
            ddlMasterGroup.SelectedIndex = 0;
            ddlSalesPerson.SelectedIndex = 0;
            ddlportal_E.SelectedIndex = 0;
            ddlcurrency_E.SelectedIndex = 0;
            ddlAgency.SelectedIndex = 0;
            txtRemrks.Text = string.Empty;
            ddlclient.SelectedIndex = 0;
            txtCampaign.Text = string.Empty;
            txtExternalID.Text = string.Empty;
            txtInternalID.Text = string.Empty;
            txtROMPDate.Text = string.Empty;
            txtROMPDate.Text = string.Empty;

            lblTotalGST.Text = string.Empty;
            dtCamStart.Text = string.Empty;
            dtCamEnd.Text = string.Empty;
            lblBilled.Text = "Not-Billed";
            chkIsCancelled.Checked = false;
            txtReleaseOrder.Text = "RO-";
            ddlFOCPaid.SelectedIndex = 0;
            txtagcrate_E.Text = "0.00";
            lblGrossAmount.Text = "0.00";
            lblGrossAmountPkr.Text = "0.00";
            lblTotalDiscount.Text = "0.00";
            lblTotalCommission.Text = "0.00";
            lblTotalGST.Text = "0.00";
            lblNetAmount.Text = "0.00";
            btnSave.Text = "Save";
        }
        private void BindControls()
        {
            //GLEntities db2 = new GLEntities();
            var gl = db.Companies.Where(x => x.Active == true).ToList();
            ddlCompany.DataValueField = "Company_Id";
            ddlCompany.DataTextField = "Company_Name";
            ddlCompany.DataSource = gl;
            ddlCompany.DataBind();

            var g = db.GroupAgencies.Where(x => x.Active == true).OrderBy(x => x.GroupName).ToList();
            ddlMasterGroup.DataValueField = "ID";
            ddlMasterGroup.DataTextField = "GroupName";
            ddlMasterGroup.DataSource = g;
            ddlMasterGroup.DataBind();

            // ddlMasterGroup_SelectedIndexChanged(null, null);


            var s = db.SalesPersons.Where(x => x.IsActive == true).OrderBy(x => x.SalesPersonName).ToList();
            ddlSalesPerson.DataValueField = "ID";
            ddlSalesPerson.DataTextField = "SalesPersonName";
            ddlSalesPerson.DataSource = s;
            ddlSalesPerson.DataBind();
            ddlSalesPerson.Items.Insert(0, new ListItem("Select Sales Person", "0"));

            var p = db.Portals.Where(x => x.IsActive == true).OrderBy(x => x.ID).ToList();
            ddlportal_E.DataValueField = "ID";
            ddlportal_E.DataTextField = "PortalName";
            ddlportal_E.DataSource = p;
            ddlportal_E.DataBind();
            ddlportal_E.Items.Insert(0, new ListItem("Portal", "0"));

            //ddlregion.Items.Insert(0, new ListItem("Region", "0"));


            var ss = db.CurrencyModes.Where(x => x.IsActive == true).OrderBy(x => x.ID).ToList();
            ddlcurrency_E.DataValueField = "ID";
            ddlcurrency_E.DataTextField = "BillingCurrency";
            ddlcurrency_E.DataSource = ss;
            ddlcurrency_E.DataBind();
            ddlcurrency_E.Items.Insert(0, new ListItem("Currency", "0"));

            var gg = db.vAgencyWithCities.OrderBy(x => x.AgencyName).ToList();
            ddlAgency.DataValueField = "ID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = gg;
            ddlAgency.DataBind();

            ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
            ddlclient.Items.Insert(0, new ListItem("Select Client", "0"));
            setAGC();
            txtgstrate_E.Text = lblgst.Text;
            txtagcrate_E.Text = lblagc.Text;
        }
        protected void btnCancelDocument_Click(object sender, EventArgs e)
        {

        }
        protected void AsyncFileUpload2_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {

        }
        protected void AsyncFileUpload2_UploadedFileError(object sender, AsyncFileUploadEventArgs e)
        {
            string str = e.ToString();
        }
        DataTable dtP = new DataTable();
        protected void DownloadButton_Cilck(object sender, EventArgs e)
        {
            //ImageButton imageButton = (ImageButton)sender;
            //GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            //Int32 ID = Convert.ToInt32(gvDocument.DataKeys[myRow.RowIndex].Value.ToString());
            //var d = db.EmployeeDocuments.Where(x => x.ID == ID).SingleOrDefault();
            //if (d != null)
            //{

            //    string filename = (string)d.FileName;
            //    byte[] bytes = (byte[])d.DocumentImage;
            //    try
            //    {

            //        Response.Clear();
            //        Response.Buffer = true;
            //        Response.Charset = "";
            //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //        Response.ContentType = "application/octet-stream";
            //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            //        Response.BinaryWrite(bytes);
            //        Response.Flush();
            //        Response.End();

            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
        }
        private void InitialGrid()
        {
            DataTable dt = (DataTable)ViewState["dt"];
            {

                DataRow row = dt.NewRow();

                row["ID"] = 0;
                row["PortalId"] = Convert.ToInt32(ddlportal_E.SelectedValue);
                row["StartDate"] = dtfrom_E.Text;//  .ToString("dd/MM/yyyy"),
                row["EndDate"] = dtto_E.Text;
                row["CurrencyID"] = ddlcurrency_E.SelectedValue.ToString();
                row["CurrencyValue"] = Convert.ToDouble(txtusd_E.Text).ToString();
                row["ConversionRate"] = Convert.ToDouble(txtcr_E.Text).ToString();
                row["CPMRate"] = txtcpm_E.Text;
                row["AGCPercentage"] = Convert.ToDouble(txtagcrate_E.Text).ToString();//   Convert.ToDouble(lblagc.Text),
                row["GSTPercentage"] = Convert.ToDouble(txtgstrate_E.Text).ToString();
                row["Weight"] = 1;
                row["Impressions"] = txtimpressions_E.Text;
                row["DeleiveredExpresssions"] = txtDeliveredimpressions_E.Text;
                row["Discount"] = Convert.ToDouble(txtdiscount_E.Text).ToString();
                row["TotalBilled"] = Convert.ToDouble(txtgross_E.Text).ToString();
                double grossvalue, gstrate, agcrate, netvalue, gst, agc, discount, total;
                netvalue = 0;
                grossvalue = Convert.ToDouble(txtgross_E.Text);
                discount = Convert.ToDouble(txtdiscount_E.Text);

                total = grossvalue - discount;
                gstrate = Convert.ToDouble(txtgstrate_E.Text);
                agcrate = Convert.ToDouble(txtagcrate_E.Text);
                gst = Convert.ToDouble(txtgstrate_E.Text);
                agc = Convert.ToDouble(txtagcrate_E.Text);
                if (gst > 0 && agc > 0)
                {
                    gst = Math.Round(((total * gstrate) / 100), 0);
                    agc = Math.Round(((total * agcrate) / 100), 0);
                    netvalue = Math.Round(((total + gst) - agc), 0);
                }
                if (gst > 0 && agc == 0)
                {
                    gst = Math.Round(((total * gstrate) / 100), 0);
                    agc = 0;
                    netvalue = Math.Round(((total + gst) - agc), 0);
                }
                if (gst == 0 && agc > 0)
                {
                    agc = Math.Round(((total * agcrate) / 100), 0);
                    gst = 0;
                    netvalue = Math.Round(((total + gst) - agc), 0);
                }
                if (gst == 0 && agc == 0)
                {
                    gst = 0;
                    agc = 0;
                    netvalue = Math.Round(total, 0);
                }


                row["GSTAmount"] = gst.ToString();
                row["AGCommission"] = agc.ToString();
                row["NetAmount"] = netvalue.ToString();
                txtgst_E.Text = gst.ToString();
                txtagc_E.Text = agc.ToString();
                txtnet_E.Text = netvalue.ToString();

                dt.Rows.Add(row);
                dt.AcceptChanges();

                ViewState["dt"] = dt;

            }
            gv.DataSource = dt;
            gv.DataBind();
        }
        private void InitialGrid(int RoID)
        {
            var objDetail = db.ReleaseOrderDetails.Where(x => x.ReleaseOrderID == RoID).ToList();
            DataTable dt = new DataTable();
            dt = Helper.ToDataTable(objDetail);
            ViewState["dt"] = dt;
            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblmessage.Text = "";
                int AgencyID = Convert.ToInt32(ddlAgency.SelectedValue);
                var ssss = db.Agencies.Where(x => x.ID == AgencyID).SingleOrDefault();
                if (ssss != null)
                {
                    ddlMasterGroup.SelectedValue = ssss.GroupID.ToString();
                    if (ssss.Active == false)
                    {
                        lblmessage.Text = "Agency/Client is de-activated";
                        return;
                    }
                    if (ssss.Suspended == true)
                    {
                        lblmessage.Text = "Agency/Client is suspended";
                        return;
                    }
                    var c = db.Clients.Where(x => x.AgencyID == AgencyID).OrderBy(x => x.Client1).ToList();
                    ddlclient.DataValueField = "ID";
                    ddlclient.DataTextField = "Client1";
                    ddlclient.DataSource = c;
                    ddlclient.DataBind();
                    ddlclient.Items.Insert(0, new ListItem("Select Client", "0"));
                }
                else
                {
                    DataTable dtn = new DataTable();
                    ddlclient.DataSource = dtn;
                    ddlclient.DataBind();
                    ddlclient.Items.Insert(0, new ListItem("Select Client", "0"));
                }
                setAGC();


            }
            catch (Exception ex)
            {

            }

        }

        private void SetAgency()
        {
            try
            {
                lblmessage.Text = "";

                int AgencyID = Convert.ToInt32(ddlAgency.SelectedValue);

                var ssss = db.Agencies.Where(x => x.ID == AgencyID).SingleOrDefault();
                if (ssss != null)
                {
                    //  lblgst.Text = ssss.GSTPercentage.ToString();
                    if (ssss.Active == false)
                    {
                        lblmessage.Text = "Agency/Client is de-activated";
                        return;
                    }

                    if (ssss.Suspended == true)
                    {
                        lblmessage.Text = "Agency/Client is suspended";
                        return;
                    }
                    var c = db.Clients.Where(x => x.AgencyID == AgencyID).OrderBy(x => x.Client1).ToList();
                    ddlclient.DataValueField = "ID";
                    ddlclient.DataTextField = "Client1";
                    ddlclient.DataSource = c;
                    ddlclient.DataBind();
                    ddlclient.Items.Insert(0, new ListItem("Select Client", "0"));
                }

                else
                {
                    DataTable dtn = new DataTable();
                    ddlclient.DataSource = dtn;
                    ddlclient.DataBind();
                    ddlclient.Items.Insert(0, new ListItem("Select Client", "0"));
                }

                setAGC();


            }
            catch (Exception ex)
            {

            }
        }

        private void setAGC()
        {
            try
            {



                txtgstrate_E.Text = lblgst.Text;
                txtagcrate_E.Text = lblagc.Text;
                double dgross = Convert.ToDouble(txtgross_E.Text);
                double ddiscount = Convert.ToDouble(txtdiscount_E.Text);

                //double dnet = Convert.ToDouble(txtnet.Text);

                double agcamount = (((dgross - ddiscount) * Convert.ToDouble(lblagc.Text)) / 100);
                double gstamount = (((dgross - ddiscount) * Convert.ToDouble(lblgst.Text)) / 100);

                txtdiscount_E.Text = ddiscount.ToString();
                txtgross_E.Text = dgross.ToString();

                txtagc_E.Text = agcamount.ToString();
                txtgst_E.Text = gstamount.ToString();

                txtnet_E.Text = (((dgross - ddiscount) + gstamount) - agcamount).ToString();

            }
            catch (Exception eex)
            {

            }




            //}

        }
        private void setAGC1()
        {

        }
        protected void ddlcampaign_server(object sender, ServerValidateEventArgs e)
        {
            if (e.Value == "0")
            {
                e.IsValid = false;

            }
            else
            {
                e.IsValid = true;
            }

        }
        protected void ddlMasterGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(ddlMasterGroup.SelectedValue);
                var c = db.GroupAgencies.Where(x => x.ID == ID).SingleOrDefault();
                if (c != null)
                {
                    if (c.Active == false)
                    {
                        lblmessage.Text = "Master Agency/Client is de-activated";
                    }
                    if (c.Suspended == true)
                    {
                        lblmessage.Text = "Master Agency/Client is suspended";
                    }

                    ViewState["AGStatus"] = c.AgencyStatus;
                    if (c.AgecnyClient == "C")
                    {
                    }
                    else
                    {
                    }
                    var g = db.vAgencyWithCities.Where(x => x.GroupID == ID).OrderBy(x => x.AgencyName).ToList();
                    ddlAgency.DataValueField = "ID";
                    ddlAgency.DataTextField = "AgencyName";
                    ddlAgency.DataSource = g;
                    ddlAgency.DataBind();
                    ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
                }
                else
                {
                    var g = db.Agencies.Where(x => x.GroupID == 0).OrderBy(x => x.AgencyName).ToList();
                    ddlAgency.DataValueField = "ID";
                    ddlAgency.DataTextField = "AgencyName";
                    ddlAgency.DataSource = g;
                    ddlAgency.DataBind();
                    ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
                }
                setAGC();
                ddlAgency_SelectedIndexChanged(null, null);

            }

            catch (Exception)
            {


            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlportal = (DropDownList)e.Row.FindControl("ddlportal");
                var p = db.Portals.Where(x => x.IsActive == true).OrderBy(x => x.ID).ToList();
                ddlportal.DataValueField = "ID";
                ddlportal.DataTextField = "PortalName";
                ddlportal.DataSource = p;
                ddlportal.DataBind();
                ddlportal.Items.Insert(0, new ListItem("Portal", "0"));

                DropDownList ddlcurrency = (DropDownList)e.Row.FindControl("ddlcurrency");
                var s = db.CurrencyModes.Where(x => x.IsActive == true).OrderBy(x => x.ID).ToList();
                ddlcurrency.DataValueField = "ID";
                ddlcurrency.DataTextField = "BillingCurrency";
                ddlcurrency.DataSource = s;
                ddlcurrency.DataBind();
                ddlcurrency.Items.Insert(0, new ListItem("Currency", "0"));
                HiddenField hdPortalID = (HiddenField)e.Row.FindControl("hdPortalID");
                HiddenField hdCurrencyID = (HiddenField)e.Row.FindControl("hdCurrencyID");
                HiddenField hdReleaseOrderID = (HiddenField)e.Row.FindControl("hdReleaseOrderID");
                ddlportal.SelectedValue = hdPortalID.Value;
                ddlcurrency.SelectedValue = hdCurrencyID.Value;

            }

        }

        protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddlregion = (DropDownList)grow.FindControl("ddlregion");
            int regionid = Convert.ToInt32(ddlregion.SelectedValue);
        }

        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 ID = Convert.ToInt32(ddlclient.SelectedValue);
                var S = db.Clients.Where(x => x.ID == ID).SingleOrDefault();
                if (S != null)
                {
                    lblgst.Text = S.GSTRation.ToString();
                    txtgstrate_E.Text = lblgst.Text;
                }
                else
                    lblgst.Text = "0.00";
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void btnColone_Click(object sender, EventArgs e)
        {
            DataTable dtP = new DataTable();
            dtP = AddRowDataTable();
            gv.DataSource = dtP;
            gv.DataBind();
        }

        private DataTable AddRowDataTable()
        {

            DataTable dt = (DataTable)ViewState["dt"];
            dt.AcceptChanges();
            DataRow row = dt.NewRow();
            foreach (GridViewRow datarow in gv.Rows)
            {
                if (datarow.RowType == DataControlRowType.DataRow)
                {

                }

                break;
            }
            return dt;
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            //  InitialGrid();
            //DataTable dt = new DataTable();
            RestControl();
            gv.DataSource = null;
            gv.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            lblgvdetailmesages.Text = string.Empty;
            bool invalid = false;
            if (gv.Rows.Count > 0)
            {
                foreach (GridViewRow row in gv.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddlportal = (DropDownList)row.FindControl("ddlportal");
                        DropDownList ddlcurrency = (DropDownList)row.FindControl("ddlcurrency");
                        TextBox txtnet = (TextBox)row.FindControl("txtnet");

                        if (ddlportal.SelectedIndex == 0)
                        {
                            lblgvdetailmesages.Text = "Please select Portal";
                            invalid = true;
                            break;

                        }
                        else if (ddlcurrency.SelectedIndex == 0)
                        {
                            lblgvdetailmesages.Text = "Please select Currency";
                            invalid = true;
                            break;
                        }
                        else if (txtnet.Text == "" || (Convert.ToDouble(txtnet.Text) == 0))
                        {
                            try
                            {
                                lblgvdetailmesages.Text = "Missing payment details";
                                invalid = true;
                                break;
                            }
                            catch (Exception)
                            {
                                lblgvdetailmesages.Text = "Missing payment details";
                                invalid = true;
                                break;
                            }

                        }
                    }
                }
                if (invalid == true)
                    return;
                bool _Val = false;
                foreach (GridViewRow row in gv.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtnet = (TextBox)row.FindControl("txtnet");
                        if (Convert.ToDecimal(lblNetAmount.Text) > 0)
                        {
                            _Val = true;
                            break;
                        }
                    }
                }
                if (_Val == true)
                {
                    if (Page.IsValid)
                    {
                        if (btnSave.Text == "Save")
                        {


                            lblmessage.Text = string.Empty;
                            int IDD = Convert.ToInt32(ddlMasterGroup.SelectedValue);
                            var sss = db.GroupAgencies.Where(x => x.ID == IDD).SingleOrDefault();
                            if (sss.Active == false)
                            {
                                lblmessage.Text = "Master Agency/Client is de-activated";
                                return;
                            }
                            if (sss.Suspended == true)
                            {
                                lblmessage.Text = "Master Agency/Client is suspended";
                                return;
                            }
                            int IDDD = Convert.ToInt32(ddlAgency.SelectedValue);
                            var ssss = db.Agencies.Where(x => x.ID == IDDD).SingleOrDefault();
                            if (ssss.Active == false)
                            {
                                lblmessage.Text = "Agency/Client is de-activated";
                                return;
                            }
                            if (ssss.Suspended == true)
                            {
                                lblmessage.Text = "Agency/Client is suspended";
                                return;
                            }
                            int IDDDD = Convert.ToInt32(ddlclient.SelectedValue);
                            var sssss = db.Clients.Where(x => x.ID == IDDDD).SingleOrDefault();
                            if (ssss.Active == false)
                            {
                                lblmessage.Text = "Client is de-activated";
                                return;
                            }

                            if (ssss.Suspended == true)
                            {
                                lblmessage.Text = "Client is suspended";
                                return;
                            }

                            Decimal totalgst = 0, totalagc = 0, totalbill = 0, netreceiable = 0, totalusd = 0; ;
                            decimal totaldiscount = 0;
                            if (btnSave.Text == "Save")

                            {
                                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                                    try
                                    {
                                        ReleaseOrderMaster obj = new ReleaseOrderMaster();
                                        var s = db.usp_IDctr("ReleaseOrder").SingleOrDefault();
                                        int ID = s.Value;
                                        obj.ID = ID;
                                        txtReleaseOrder.Text = "RO-" + (ID.ToString().PadLeft(9, '0'));
                                        ViewState["releaseorderid"] = ID.ToString();
                                        // if (txtReleaseOrder.Text.Trim().Length == 0)
                                        //     obj.ReleaseOrderNumber = ID.ToString();
                                        //  else
                                        obj.ReleaseOrderNumber = txtReleaseOrder.Text;  //Convert.ToInt32(lblID.Text);

                                        obj.ReleaseOrderReferenceID = txtInternalID.Text; ;// lblID.Text;// txt impressions_E ReleaseOrder.Text;
                                        obj.ReleseOrderSTI = Convert.ToInt32(ViewState["stino"]);
                                        obj.AgencyID = Convert.ToInt32(ddlAgency.SelectedValue);
                                        obj.FOCPAID = ddlFOCPaid.SelectedValue.ToString();
                                        obj.ClientID = Convert.ToInt32(ddlclient.SelectedValue);
                                        obj.Compaign = txtCampaign.Text;
                                        obj.ChannelID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ChannelId"]);
                                        if (ddlSalesPerson.SelectedIndex > 0)
                                        {
                                            obj.SalesPersonID = Convert.ToInt32(ddlSalesPerson.SelectedValue);
                                        }
                                        else

                                            obj.SalesPersonID = null;//  Convert.ToInt32(ddlSalesPerson.SelectedValue);

                                        try
                                        {

                                            obj.ROMPDate = Helper.SetDateFormat(txtROMPDate.Text);
                                        }
                                        catch (Exception)
                                        {
                                            obj.ROMPDate = null;

                                        }
                                        // DateTime.Now;//    Helper.SetDateFormat(txtROMPDate.Text);


                                        obj.ROExternal = txtExternalID.Text; // txtExternal.Text;
                                        obj.IsCancelled = chkIsCancelled.Checked;
                                        obj.Remarks = txtRemrks.Text;
                                        obj.INV_AGC = Convert.ToDecimal(lblTotalCommission.Text);
                                        obj.INV_Discount = Convert.ToDecimal(lblTotalDiscount.Text);
                                        obj.INV_Gross = Convert.ToDecimal(lblGrossAmount.Text);
                                        obj.INV_GST = Convert.ToDecimal(lblTotalGST.Text);
                                        obj.INV_Net = Convert.ToDecimal(lblNetAmount.Text);
                                        obj.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                                        obj.CreatedOn = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                        obj.Cam_startDate = Helper.SetDateFormat(dtCamStart.Text);
                                        obj.Cam_endDate = Helper.SetDateFormat(dtCamEnd.Text);
                                        obj.IsExecuted = false;
                                        obj.CompnayID = Convert.ToInt32(ddlCompany.SelectedValue);
                                        try
                                        {
                                            obj.ReleaseOrderDate = Helper.SetDateFormat(txtReleaseOrderDate.Text);
                                        }
                                        catch (Exception ex)
                                        {
                                            obj.ReleaseOrderDate = Helper.SetDateFormat(txtReleaseOrderDate.Text);

                                        }


                                        db.ReleaseOrderMasters.Add(obj);
                                        db.SaveChanges();
                                        //txtDelivered.Text = txtImpressions.Text;

                                        ReleaseOrderDetail objDetail = new ReleaseOrderDetail();
                                        foreach (GridViewRow row in gv.Rows)
                                        {
                                            objDetail = new ReleaseOrderDetail();
                                            DropDownList ddlportal = (DropDownList)row.FindControl("ddlportal");
                                            DropDownList ddlcurrency = (DropDownList)row.FindControl("ddlcurrency");
                                            TextBox txtimpressions = (TextBox)row.FindControl("txtimpressions");
                                            TextBox txtdeliveredimpressions = (TextBox)row.FindControl("txtDeliveredimpressions");
                                            TextBox txtgross = (TextBox)row.FindControl("txtgross");
                                            TextBox txtgst = (TextBox)row.FindControl("txtgst");
                                            TextBox txtagc = (TextBox)row.FindControl("txtagc");
                                            TextBox txtnet = (TextBox)row.FindControl("txtnet");
                                            TextBox txtdiscount = (TextBox)row.FindControl("txtdiscount");
                                            TextBox txtqty = (TextBox)row.FindControl("txtqty");
                                            TextBox txtcpm = (TextBox)row.FindControl("txtcpm");
                                            TextBox txtcr = (TextBox)row.FindControl("txtcr");
                                            TextBox txtusd = (TextBox)row.FindControl("txtusd");
                                            TextBox txtgstrate = (TextBox)row.FindControl("txtgstrate");
                                            TextBox txtagcrate = (TextBox)row.FindControl("txtagcrate");

                                            TextBox txtgstamount = (TextBox)row.FindControl("txtgst");
                                            TextBox txtagcamount = (TextBox)row.FindControl("txtagc");
                                            totalagc = totalagc + Convert.ToDecimal(txtagcamount.Text);
                                            totalgst = totalgst + Convert.ToDecimal(txtgstamount.Text); ;
                                            totalbill = totalbill + Convert.ToDecimal(txtgross.Text);
                                            netreceiable = netreceiable + Convert.ToDecimal(txtnet.Text);
                                            totalusd = totalusd + Convert.ToDecimal(txtusd.Text);
                                            totaldiscount = totaldiscount + Convert.ToDecimal(txtdiscount.Text);
                                            var sd = db.usp_IDctr("RODetails").SingleOrDefault();
                                            objDetail.ID = sd.Value;
                                            objDetail.ReleaseOrderID = obj.ID;

                                            objDetail.PortalID = Convert.ToInt32(ddlportal.SelectedValue);
                                            objDetail.CurrencyID = Convert.ToInt32(ddlcurrency.SelectedValue);
                                            objDetail.Remarks = txtRemrks.Text;
                                            objDetail.Impressions = txtimpressions.Text;
                                            objDetail.DeleiveredExpresssions = txtdeliveredimpressions.Text;
                                            objDetail.CPMRate = txtcpm.Text;
                                            objDetail.GSTAmount = Convert.ToDecimal(txtgstamount.Text);
                                            objDetail.Discount = Convert.ToDecimal(txtdiscount.Text);
                                            objDetail.AGCommission = Convert.ToDecimal(txtagcamount.Text);
                                            objDetail.TotalBilled = Convert.ToDecimal(txtgross.Text);
                                            objDetail.NetAmount = Convert.ToDecimal(txtnet.Text);
                                            objDetail.NetReceiable = Convert.ToDecimal(txtnet.Text);// Convert.ToDecimal(TotalAmount) + Convert.ToDecimal(GST);
                                            objDetail.AGCPercentage = Convert.ToDecimal(txtagcrate.Text);
                                            objDetail.GSTPercentage = Convert.ToDecimal(txtgstrate.Text);
                                            objDetail.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                                            objDetail.CreatedOn = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                            objDetail.StartDate = DateTime.Now;// Helper.SetDateFormat(dtfrom.Text);
                                            objDetail.EndDate = DateTime.Now;// Helper.SetDateFormat(dtto.Text);
                                            objDetail.Weight = 0;// Convert.ToInt32(txtqty.Text);
                                            objDetail.ConversionRate = Convert.ToDecimal(txtcr.Text);
                                            objDetail.CurrencyValue = Convert.ToDecimal(txtusd.Text);
                                            db.ReleaseOrderDetails.Add(objDetail);
                                            db.SaveChanges();
                                        }
                                        scope.Complete();
                                        // string script = string.Format("alert('{0}');", txtReleaseOrder.Text);
                                        //if (Page != null && !Page.ClientScript.IsClientScriptBlockRegistered("alert"))
                                        //{
                                        //    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "alert", script, true /* addScriptTags */);
                                        //}
                                        //  Response.Write("<script>alert('"+ txtReleaseOrder.Text + "'Release Order Created Successfully');</script>");
                                        lblmessage.Visible = true;
                                        lblmessage.Text = "Release Order Created Successfully";
                                        btnSave.Enabled = false;
                                        //btncancel_Click(null, null);
                                        //fileUpload.Attributes.Add("disabled", "false");
                                    }
                                    catch (Exception ex)
                                    {
                                        lblmessage.Visible = true;
                                        lblmessage.Text = ExceptionHandler.GetException(ex);
                                    }
                                    finally
                                    {
                                        // setData();
                                        // btncancel_Click(null, null);
                                    }
                            }
                        }
                        else
                        {
                            bool ChkDetails = false;
                            Decimal totalgst = 0, totalagc = 0, totalbill = 0, netreceiable = 0, totalusd = 0; ;
                            decimal totaldiscount = 0;
                            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                            {
                                try
                                {
                                    int ID = Convert.ToInt32(ViewState["releaseorderid"]);



                                    var obj = db.ReleaseOrderMasters.Where(x => x.ID == ID).SingleOrDefault();

                                    var objDetail = db.ReleaseOrderDetails.Where(x => x.ReleaseOrderID == ID).SingleOrDefault();
                                    if (objDetail == null)
                                        ChkDetails = false;
                                    else
                                        ChkDetails = true;

                                    obj.ReleaseOrderReferenceID = txtInternalID.Text;//   txtExternal.Text;// // txt impressions_E ReleaseOrder.Text;
                                    obj.ReleseOrderSTI = Convert.ToInt32(ViewState["stino"]);
                                    obj.AgencyID = Convert.ToInt32(ddlAgency.SelectedValue);
                                    obj.FOCPAID = ddlFOCPaid.SelectedValue.ToString();
                                    obj.ClientID = Convert.ToInt32(ddlclient.SelectedValue);
                                    obj.Compaign = txtCampaign.Text;

                                    if (ddlSalesPerson.SelectedIndex > 0)
                                    {
                                        obj.SalesPersonID = Convert.ToInt32(ddlSalesPerson.SelectedValue);
                                    }
                                    else

                                        obj.SalesPersonID = null;//  Convert.ToInt32(ddlSalesPerson.SelectedValue);

                                    try
                                    {
                                        obj.ROMPDate = Helper.SetDateFormat(txtROMPDate.Text);
                                    }
                                    catch (Exception)
                                    {
                                        obj.ROMPDate = null;

                                    }
                                    // DateTime.Now;//    Helper.SetDateFormat(txtROMPDate.Text);

                                    // corrected due to wrong label name 
                                    obj.ROExternal =txtExternalID.Text;// txtExternal.Text;
                                    obj.IsCancelled = chkIsCancelled.Checked;
                                    obj.Remarks = txtRemrks.Text;
                                    obj.INV_AGC = Convert.ToDecimal(lblTotalCommission.Text);
                                    obj.INV_Discount = Convert.ToDecimal(lblTotalDiscount.Text);
                                    obj.INV_Gross = Convert.ToDecimal(lblGrossAmount.Text);
                                    obj.INV_GST = Convert.ToDecimal(lblTotalGST.Text);
                                    obj.INV_Net = Convert.ToDecimal(lblNetAmount.Text);
                                    obj.CreatedBy = ((UserInfo)Session["UserObject"]).ID; 
                                    obj.CreatedOn = Helper.SetDateFormat(txtReleaseOrderDate.Text);
                                    obj.Cam_startDate = Helper.SetDateFormat(dtCamStart.Text);
                                    obj.Cam_endDate = Helper.SetDateFormat(dtCamEnd.Text);
                                    obj.IsExecuted = false;
                                    obj.CompnayID = Convert.ToInt32(ddlCompany.SelectedValue);
                                    try
                                    {
                                        obj.ReleaseOrderDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                    }
                                    catch (Exception ex)
                                    {
                                        obj.ReleaseOrderDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                                    }
                                    db.SaveChanges();
                                    foreach (GridViewRow row in gv.Rows)
                                    {


                                        DropDownList ddlportal = (DropDownList)row.FindControl("ddlportal");
                                        DropDownList ddlcurrency = (DropDownList)row.FindControl("ddlcurrency");
                                        TextBox txtimpressions = (TextBox)row.FindControl("txtimpressions");
                                        TextBox txtdeliveredimpressions = (TextBox)row.FindControl("txtDeliveredimpressions");
                                        TextBox txtgross = (TextBox)row.FindControl("txtgross");
                                        TextBox txtgst = (TextBox)row.FindControl("txtgst");
                                        TextBox txtagc = (TextBox)row.FindControl("txtagc");
                                        TextBox txtnet = (TextBox)row.FindControl("txtnet");
                                        TextBox txtdiscount = (TextBox)row.FindControl("txtdiscount");
                                        TextBox txtqty = (TextBox)row.FindControl("txtqty");
                                        TextBox txtcpm = (TextBox)row.FindControl("txtcpm");
                                        TextBox txtcr = (TextBox)row.FindControl("txtcr");
                                        TextBox txtusd = (TextBox)row.FindControl("txtusd");
                                        TextBox txtgstrate = (TextBox)row.FindControl("txtgstrate");
                                        TextBox txtagcrate = (TextBox)row.FindControl("txtagcrate");

                                        TextBox txtgstamount = (TextBox)row.FindControl("txtgst");
                                        TextBox txtagcamount = (TextBox)row.FindControl("txtagc");
                                        totalagc = totalagc + Convert.ToDecimal(txtagcamount.Text);
                                        totalgst = totalgst + Convert.ToDecimal(txtgstamount.Text); ;
                                        totalbill = totalbill + Convert.ToDecimal(txtgross.Text);
                                        netreceiable = netreceiable + Convert.ToDecimal(txtnet.Text);
                                        totalusd = totalusd + Convert.ToDecimal(txtusd.Text);
                                        totaldiscount = totaldiscount + Convert.ToDecimal(txtdiscount.Text);


                                        if (ChkDetails == true)
                                        {
                                            int releaseorderid = Convert.ToInt32(gv.DataKeys[row.RowIndex].Value);
                                            objDetail = db.ReleaseOrderDetails.Where(x => x.ID == releaseorderid).SingleOrDefault();
                                        }
                                        else
                                        {
                                            objDetail = new ReleaseOrderDetail();
                                            var sd = db.usp_IDctr("RODetails").SingleOrDefault();
                                            objDetail.ID = sd.Value;
                                            objDetail.ReleaseOrderID = obj.ID;

                                        }
                                        objDetail.PortalID = Convert.ToInt32(ddlportal.SelectedValue);
                                        objDetail.CurrencyID = Convert.ToInt32(ddlcurrency.SelectedValue);
                                        objDetail.Remarks = txtRemrks.Text;
                                        objDetail.Impressions = txtimpressions.Text;
                                        objDetail.DeleiveredExpresssions = txtdeliveredimpressions.Text;
                                        objDetail.CPMRate = txtcpm.Text;
                                        objDetail.GSTAmount = Convert.ToDecimal(txtgstamount.Text);
                                        objDetail.Discount = Convert.ToDecimal(txtdiscount.Text);
                                        objDetail.AGCommission = Convert.ToDecimal(txtagcamount.Text);
                                        objDetail.TotalBilled = Convert.ToDecimal(txtgross.Text);
                                        objDetail.NetAmount = Convert.ToDecimal(txtnet.Text);
                                        objDetail.NetReceiable = Convert.ToDecimal(txtnet.Text);// Convert.ToDecimal(TotalAmount) + Convert.ToDecimal(GST);
                                        objDetail.AGCPercentage = Convert.ToDecimal(txtagcrate.Text);
                                        objDetail.GSTPercentage = Convert.ToDecimal(txtgstrate.Text);
                                        objDetail.CreatedBy = ((UserInfo)Session["UserObject"]).ID;
                                        objDetail.CreatedOn = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                        objDetail.StartDate = DateTime.Now;// Helper.SetDateFormat(dtfrom.Text);
                                        objDetail.EndDate = DateTime.Now;// Helper.SetDateFormat(dtto.Text);
                                        objDetail.Weight = 0;// Convert.ToInt32(txtqty.Text);
                                        objDetail.ConversionRate = Convert.ToDecimal(txtcr.Text);
                                        objDetail.CurrencyValue = Convert.ToDecimal(txtusd.Text);

                                        if (ChkDetails == true)
                                        {
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            db.ReleaseOrderDetails.Add(objDetail);
                                            db.SaveChanges();
                                        }
                                    }

                                    scope.Complete();

                                    lblmessage.Visible = true;
                                    lblmessage.Text = "Release Order Updated Successfully";

                                    btnSave.Text = "Save";
                                    btnSave.Enabled = false;
                                    // Response.Redirect("BookReleaseOrder.aspx", true);



                                }
                                catch (Exception ex)
                                {
                                    lblmessage.Visible = true;
                                    lblmessage.Text = ExceptionHandler.GetException(ex);
                                }
                                finally
                                {
                                    //btncancel_Click(null, null);
                                    // setData();
                                }
                            }
                        }

                    }
                    else
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "Please provide atleast payment informations";
                        return;
                    }


                }

            }
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            //RestControl();     
            // btnSave.Enabled = true;

            lblmessage.Text = string.Empty;
            try
            {
                if (Convert.ToDouble(txtgross_E.Text) > 0 && Convert.ToDouble(txtnet_E.Text) > 0)
                {
                    InitialGrid();
                    setData();
                }
                else
                {
                    lblmessage.Text = "Please provide mendatory inputs";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = "Please provide mendatory inputs";
                return;

            }
        }

        private void setData()
        {
            decimal mnet = 0, mdiscount = 0, magc = 0, mgst = 0, mgross = 0, musd = 0;
            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    TextBox txtnet = (TextBox)row.FindControl("txtnet");
                    TextBox txtdiscount = (TextBox)row.FindControl("txtdiscount");
                    TextBox txtagc = (TextBox)row.FindControl("txtagc");
                    TextBox txtgst = (TextBox)row.FindControl("txtgst");
                    TextBox txtgross = (TextBox)row.FindControl("txtgross");
                    TextBox txtusd = (TextBox)row.FindControl("txtusd");

                    mnet += Convert.ToDecimal(txtnet.Text);
                    mdiscount += Convert.ToDecimal(txtdiscount.Text);
                    mgst += Convert.ToDecimal(txtgst.Text);
                    magc += Convert.ToDecimal(txtagc.Text);
                    musd += Convert.ToDecimal(txtusd.Text);
                    mgross += Convert.ToDecimal(txtgross.Text);
                    lblGrossAmount.Text = mgross.ToString();
                    lblGrossAmountPkr.Text = mgross.ToString();
                    lblTotalDiscount.Text = mdiscount.ToString();
                    lblTotalCommission.Text = magc.ToString();
                    lblTotalGST.Text = mgst.ToString();
                    lblNetAmount.Text = mnet.ToString();

                }
            }
        }

        protected void AsyncFileUpload1_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {

        }

        protected void AsyncFileUpload1_UploadedFileError(object sender, AsyncFileUploadEventArgs e)
        {

        }

        protected void imgResetagency_Click(object sender, ImageClickEventArgs e)
        {
            var gg = db.vAgencyWithCities.OrderBy(x => x.AgencyName).ToList();
            ddlAgency.DataValueField = "ID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = gg;
            ddlAgency.DataBind();

            ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
        }

        protected void btnremove_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            if (btnSave.Text == "Update")
            {
                var dobj = db.ReleaseOrderDetails.Where(x => x.ID == ID).SingleOrDefault();
                if (dobj != null)
                {
                    db.ReleaseOrderDetails.Remove(dobj);
                    db.SaveChanges();
                }
                DataTable dt = (DataTable)ViewState["dt"];
                dt.Rows.RemoveAt(myRow.RowIndex);
                dt.AcceptChanges();
                ViewState["dt"] = dt;
                gv.DataSource = dt;
                gv.DataBind();
                setData();
                //  gv.DeleteRow(myRow.RowIndex);
            }
            else
            {
                DataTable dt = (DataTable)ViewState["dt"];
                dt.Rows.RemoveAt(myRow.RowIndex);
                dt.AcceptChanges();
                ViewState["dt"] = dt;
                gv.DataSource = dt;
                gv.DataBind();
                setData();
            }
        }


        protected void btnAddNewRec_Click(object sender, EventArgs e)
        {
            RestControl();
        }
    }
}