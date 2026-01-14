using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressDigital
{
    public partial class ClientManagement : System.Web.UI.Page
    {
        DbDigitalEntities db = new DbDigitalEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["imgPath"] = "";
                BindControl();
                int agid = 0;
                if (Request.QueryString.Count > 0)
                {
                    agid = Convert.ToInt32(Request.QueryString[0]);
                    BindGrid(agid);
                }
                else
                {
                    BindGrid();
                }
                lblmessage.Text = string.Empty;

                var obj = db.Clients.Where(x => x.AgencyID == agid).Take(1).SingleOrDefault();
                if (obj != null)
                {
                    ddlGroup.SelectedValue = obj.Agency.GroupID.ToString();
                    ddlGroup_SelectedIndexChanged(null, null);
                    try
                    {
                        //   ddlAgency.SelectedValue = agid.ToString();
                        //   ddlAgency_SelectedIndexChanged(null, null);
                    }
                    catch (Exception)
                    {
                        ddlAgency.SelectedIndex = 0;
                    }


                }
                //    txtClient.Text = obj.Client1;
                //    try
                //    {
                //        ddlAgency.SelectedValue = obj.AgencyID.ToString();
                //    }
                //    catch (Exception ex)
                //    {
                //        ddlAgency.SelectedIndex = 0;
                //    }
                //    finally
                //    {

                //    }

                //    txtContactPerson.Text = obj.ContactPerson;
                //    txtContactnumber.Text = obj.ContactNo;
                //    txtEmail.Text = obj.Email;
                //    txtGST.Text = obj.GSTNo.ToString();
                //    txtGSTRate.Text = obj.GSTRation.ToString();
                //    TxtNTNNO.Text = obj.NTNNo;
                //    txtCNIC.Text = obj.NICNo;
                //    txtAddress.Text = obj.Address;
                //    ddlGroup.SelectedValue = obj.Agency.GroupID.ToString();
                //    ddlGroup_SelectedIndexChanged(null, null);
                //    ddlAgency.SelectedValue = obj.AgencyID.ToString();
                //    ddlCountry.SelectedValue = obj.CountryID.ToString();
                //    ddlCountry_SelectedIndexChanged(null, null);
                //    try
                //    {
                //        ddlState.SelectedValue = obj.StateID.ToString();
                //        ddlState_SelectedIndexChanged(null, null);
                //        ddlCity.SelectedValue = obj.CityID.ToString();
                //    }
                //    catch (Exception)
                //    {
                //        ddlState.SelectedIndex = 0;// obj.StateID.ToString();
                //        ddlCity.SelectedIndex = 0;
                //    }

                //    //ddlGSTProvince_SelectedIndexChanged(null, null);
                //    ChkIsActive.Checked = Convert.ToBoolean(obj.Active);
                //    IsSuspended.Checked = Convert.ToBoolean(obj.Suspended);
                //    chkExempted.Checked = Convert.ToBoolean(obj.IsExempted);
                //    IsGovt.Checked = Convert.ToBoolean(obj.IsGovernament);
                //    btnSave.Text = "Update";

                //}
                //else
                //{
                //    lblmessage.Text = "No Client Associated with this Agency";
                //}


            }
        }
        private void BindControl()
        {
            var d = db.GroupAgencies.OrderBy(x => x.GroupName).ToList();
            ddlGroup.DataValueField = "ID";
            ddlGroup.DataTextField = "GroupName";
            ddlGroup.DataSource = d;
            ddlGroup.DataBind();
            ddlGroup_SelectedIndexChanged(null, null);

            var c = db.Countries.Where(x => x.IsActive == true).OrderBy(x => x.CountryName).ToList();
            ddlCountry.DataValueField = "ID";
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataSource = c;
            ddlCountry.DataBind();
            ddlCountry_SelectedIndexChanged(null, null);

            var gst = db.GSTProvinces.OrderBy(x => x.ID).ToList();
            ddlGSTProvince.DataValueField = "ID";
            ddlGSTProvince.DataTextField = "Province";
            ddlGSTProvince.DataSource = gst;
            ddlGSTProvince.DataBind();
            ddlGSTProvince_SelectedIndexChanged(null, null);
            ddlGSTProvince.SelectedIndex = 0;


            var eff = db.EffectiveProvinces.OrderBy(x => x.ID).ToList();
            ddlGSTStation.DataValueField = "ID";
            ddlGSTStation.DataTextField = "Province";
            ddlGSTStation.DataSource = eff;
            ddlGSTStation.DataBind();


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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (chkExempted.Checked == true && Convert.ToDecimal(txtGSTRate.Text) > 0)
                {
                    txtGSTRate.Text = "0";
                }
                if (chkExempted.Checked == false && Convert.ToDecimal(txtGSTRate.Text) == 0)
                {
                    lblmessage.Text = "Please provide General Sales Tax Ratio";
                    return;
                }
                lblmessage.Text = string.Empty;
                int IDD = Convert.ToInt32(ddlGroup.SelectedValue);
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

                //if (ddlCountry.SelectedIndex != -1 && ddlState.SelectedIndex != -1 && ddlCity.SelectedIndex != -1 && ddlAgency.SelectedIndex != 0 && ddlGroup.SelectedIndex != -1)
                //{
                if (btnSave.Text == "Save")
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            var s = db.usp_IDctr("Client").SingleOrDefault();
                            int ID = s.Value;
                            Client obj = new Client();
                            obj.ID = ID;
                            obj.Client1 = txtClient.Text;
                            obj.AgencyID = Convert.ToInt32(ddlAgency.SelectedValue);
                            obj.ContactPerson = txtContactPerson.Text;
                            obj.ContactNo = txtContactnumber.Text;
                            obj.Email = txtEmail.Text;
                            obj.GSTNo = txtGST.Text;
                            obj.NTNNo = TxtNTNNO.Text;
                            obj.NICNo = txtCNIC.Text;
                            obj.Address = txtAddress.Text;
                            obj.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                            if (ddlState.SelectedIndex == 0)
                            {
                                obj.StateID = null;
                                obj.CityID = null;
                            }
                            else
                            {
                                obj.StateID = Convert.ToInt32(ddlState.SelectedValue);

                            }
                            if (ddlCity.SelectedIndex == 0)
                            {
                                obj.CityID = null;
                            }
                            else
                            {
                                obj.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                            }
                            obj.Active = ChkIsActive.Checked;
                            obj.Suspended = IsSuspended.Checked;
                            obj.GSTRation = Convert.ToDecimal(txtGSTRate.Text);
                            obj.IsExempted = chkExempted.Checked;
                            obj.CreatedBy = ((UserInfo)Session["UserObject"]).ID; 
                            obj.CreatedOn = DateTime.Now;
                            obj.IsGovernament = IsGovt.Checked;
                            obj.GSTEffectiveStation = Convert.ToInt32(ddlGSTStation.SelectedValue);
                            obj.IsInternational = chkLocalInternational.Checked;
                            //if ((Session["imgPath"] != null) || (Session["imgPath"].ToString() != ""))
                            //{
                            //    string FileFromSession = Session["imgPath"].ToString();
                            //    if (!string.IsNullOrEmpty(FileFromSession))
                            //    {
                            //        string str_uploadpath = HttpContext.Current.Server.MapPath("~/Documents/");
                            //        string fileName = FileFromSession.ToString();
                            //        string filePath = Path.Combine(str_uploadpath, fileName);

                            //        /*                    string filePath = Server.MapPath("~/Documents/" + FileFromSession); */
                            //        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                            //        if (fs.Length > 8388608)
                            //        {
                            //            lblmessage.Text = "File can not be more than 8 MB";
                            //            return;
                            //        }
                            //        BinaryReader br = new BinaryReader(fs);
                            //        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            //        br.Close();
                            //        fs.Close();
                            //        obj.UploadedFile = bytes;
                            //        obj.UploadedFileName = fileName;
                            //    }
                            //}

                            db.Clients.Add(obj);
                            db.SaveChanges();

                            LogManagers.RecordID = ID;
                            LogManagers.ActionOnForm = "Client";
                            LogManagers.ActionBy = ((UserInfo)Session["UserObject"]).ID;
                            LogManagers.ActionOn = DateTime.Now;
                            LogManagers.ActionTaken = "Inserted";
                            LogManagers.SetLog(db);

                            BindGrid();
                            btnCancel_Click(null, null);
                            scope.Complete();
                            lblmessage.Text = "Client Created Successfully";
                        }
                        catch (Exception ex)
                        {
                            lblmessage.Text = ExceptionHandler.GetException(ex);

                        }
                    }
                }
                else
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            int ID = Convert.ToInt32(ViewState["RecordID"]);
                            var obj = db.Clients.Where(x => x.ID == ID).SingleOrDefault();
                            obj.ID = ID;
                            obj.Client1 = txtClient.Text;
                            obj.AgencyID = Convert.ToInt32(ddlAgency.SelectedValue);
                            obj.ContactPerson = txtContactPerson.Text;
                            obj.ContactNo = txtContactnumber.Text;
                            obj.Email = txtEmail.Text;
                            obj.GSTNo = txtGST.Text;
                            obj.NTNNo = TxtNTNNO.Text;
                            obj.NICNo = txtCNIC.Text;
                            obj.Address = txtAddress.Text;
                            obj.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                            obj.StateID = Convert.ToInt32(ddlState.SelectedValue);
                            obj.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                            obj.Active = ChkIsActive.Checked;
                            obj.Suspended = IsSuspended.Checked;
                            obj.IsExempted = chkExempted.Checked;
                            obj.GSTRation = Convert.ToDecimal(txtGSTRate.Text);
                            obj.ModifyBy = ((UserInfo)Session["UserObject"]).ID;
                            obj.ModifyOn = DateTime.Now;
                            obj.IsGovernament = IsGovt.Checked;
                            obj.GSTEffectiveStation = Convert.ToInt32(ddlGSTStation.SelectedValue);
                            obj.IsInternational = chkLocalInternational.Checked;
                            //if ((Session["imgPath"] != null) || (Session["imgPath"].ToString() != ""))
                            //{
                            //    string FileFromSession = Session["imgPath"].ToString();
                            //    if (!string.IsNullOrEmpty(FileFromSession))
                            //    {
                            //        string str_uploadpath = HttpContext.Current.Server.MapPath("~/Documents/");
                            //        string fileName = FileFromSession.ToString();
                            //        string filePath = Path.Combine(str_uploadpath, fileName);

                            //        /*                    string filePath = Server.MapPath("~/Documents/" + FileFromSession); */
                            //        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                            //        if (fs.Length > 8388608)
                            //        {
                            //            lblmessage.Text = "File can not be more than 8 MB";
                            //            return;
                            //        }
                            //        BinaryReader br = new BinaryReader(fs);
                            //        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            //        br.Close();
                            //        fs.Close();
                            //        obj.UploadedFile = bytes;
                            //        obj.UploadedFileName = fileName;
                            //    }
                            //}


                            db.SaveChanges();

                            LogManagers.RecordID = ID;
                            LogManagers.ActionOnForm = "Client";
                            LogManagers.ActionBy = ((UserInfo)Session["UserObject"]).ID;
                            LogManagers.ActionOn = DateTime.Now;
                            LogManagers.ActionTaken = "Updated";
                            LogManagers.SetLog(db);
                            BindGrid();

                            btnCancel_Click(null, null);
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            lblmessage.Text = ExceptionHandler.GetException(ex);
                        }
                    }
                }

            }
            else
            {
                lblmessage.Text = "Missing drop down values";
                return;
            }

        }
        private void BindGrid(int val)
        {
            try
            {
                //   int Id = Convert.ToInt32(ddlAgency.SelectedValue);
                var g = (from u in db.Clients.Where(x => x.Active == true && x.AgencyID == val) //.AgencyID == Id)
                         select new
                         {
                             u.ID,
                             u.Client1,
                             u.ContactPerson,
                             u.Email,
                             u.GSTNo,
                             u.NICNo,
                             u.NTNNo,
                             u.CountryState.StateName,
                             u.CityManagement.CityName,
                             u.Active,
                             u.Suspended,
                             u.AgencyID,
                             u.Agency.GroupID

                         }).OrderBy(x => x.Client1).ToList();
                DataTable dt = Helper.ToDataTable(g);
                ViewState["dt"] = dt;
                gv.DataSource = dt;
                gv.DataBind();


            }
            catch (Exception)
            {


            }

        }
        private void BindGrid()
        {
            try
            {
                //   int Id = Convert.ToInt32(ddlAgency.SelectedValue);
                var g = (from u in db.Clients.Where(x => x.Active == true) //.AgencyID == Id)
                         select new
                         {
                             u.ID,
                             u.Client1,
                             u.ContactPerson,
                             u.Email,
                             u.GSTNo,
                             u.NICNo,
                             u.NTNNo,
                             u.CountryState.StateName,
                             u.CityManagement.CityName,
                             u.Active,
                             u.Suspended,
                             u.AgencyID,
                             u.Agency.GroupID

                         }).OrderBy(x => x.Client1).ToList();
                DataTable dt = Helper.ToDataTable(g);
                ViewState["dt"] = dt;
                gv.DataSource = dt;
                gv.DataBind();
            }
            catch (Exception)
            {


            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            txtClient.Text = string.Empty;
            // ddlAgency.SelectedIndex = 0;
            txtContactPerson.Text = string.Empty;
            txtContactnumber.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtGST.Text = string.Empty;
            TxtNTNNO.Text = string.Empty;
            txtCNIC.Text = string.Empty;
            txtAddress.Text = string.Empty;
            //ddlCountry.SelectedIndex = 0;
            //ddlState.SelectedIndex = 0;
            //ddlCity.SelectedIndex = 0;
            ChkIsActive.Checked = true;
            chkExempted.Checked = false;
            IsSuspended.Checked = false;
            txtGSTRate.Text = "0.00";
            IsGovt.Checked = false;
            btnSave.Text = "Save";
            BindGrid();
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;

            var obj = db.Clients.Where(x => x.ID == ID).SingleOrDefault();
            txtClient.Text = obj.Client1;
            try
            {
                ddlAgency.SelectedValue = obj.AgencyID.ToString();
            }
            catch (Exception ex)
            {
                ddlAgency.SelectedIndex = 0;
            }
            finally
            {

            }

            txtContactPerson.Text = obj.ContactPerson;
            txtContactnumber.Text = obj.ContactNo;
            txtEmail.Text = obj.Email;
            txtGST.Text = obj.GSTNo.ToString();
            txtGSTRate.Text = obj.GSTRation.ToString();
            TxtNTNNO.Text = obj.NTNNo;
            txtCNIC.Text = obj.NICNo;
            txtAddress.Text = obj.Address;
            ddlGroup.SelectedValue = obj.Agency.GroupID.ToString();
            ddlGroup_SelectedIndexChanged(null, null);
            ddlAgency.SelectedValue = obj.AgencyID.ToString();
            ddlCountry.SelectedValue = obj.CountryID.ToString();
            ddlCountry_SelectedIndexChanged(null, null);
            try
            {
                ddlState.SelectedValue = obj.StateID.ToString();
                ddlState_SelectedIndexChanged(null, null);
                ddlCity.SelectedValue = obj.CityID.ToString();
            }
            catch (Exception)
            {
                ddlState.SelectedIndex = 0;// obj.StateID.ToString();
                ddlCity.SelectedIndex = 0;
            }

            //ddlGSTProvince_SelectedIndexChanged(null, null);
            ChkIsActive.Checked = Convert.ToBoolean(obj.Active);
            IsSuspended.Checked = Convert.ToBoolean(obj.Suspended);
            chkExempted.Checked = Convert.ToBoolean(obj.IsExempted);
            IsGovt.Checked = Convert.ToBoolean(obj.IsGovernament);
            btnSave.Text = "Update";
            chkLocalInternational.Checked = Convert.ToBoolean(obj.IsInternational);

        }
        protected void DelButton_Click(object sender, EventArgs e)
        {
        }

        protected void ImgSuspendButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;  // the row
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            HiddenField hd = (HiddenField)myRow.FindControl("hdsuspend");
            bool isSuspend = Convert.ToBoolean(hd.Value);
            ViewState["RecordID"] = ID;
            var url = String.Format("SuspendResotreClient.aspx?id={0}&Issuspend={1}", ID, isSuspend);
            Response.Redirect(url);

        }
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            gv.DataSource = dt;
            gv.DataBind();
            gv.PageIndex = e.NewPageIndex;

        }
        [WebMethod]
        public static string OnSubmit(string id)
        {
            string mess = "";
            DbDigitalEntities db = new DbDigitalEntities();
            int ID = Convert.ToInt32(id);
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var info = db.Clients.Find(ID);
                    if (info != null)
                    {
                        db.Clients.Remove(info);
                        db.SaveChanges();
                        LogManagers.RecordID = ID;
                        LogManagers.ActionOnForm = "Client";
                        LogManagers.ActionBy = ((UserInfo)HttpContext.Current.Session["UserObject"]).ID;
                        LogManagers.ActionOn = DateTime.Now;
                        LogManagers.ActionTaken = "Delete";
                        LogManagers.SetLog(db);
                        scope.Complete();
                        mess = "Ok";
                    }
                }
                catch (Exception ex)
                {
                    mess = ExceptionHandler.GetException(ex);
                }

            }
            return mess;
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(ddlGroup.SelectedValue);
                var d = db.vAgencyWithCities.Where(x => x.GroupID == ID).ToList();
                ddlAgency.DataValueField = "ID";
                ddlAgency.DataTextField = "AgencyName";
                ddlAgency.DataSource = d;
                ddlAgency.DataBind();
                ddlAgency.Items.Insert(0, new ListItem("Select Agency", "0"));
                //if (d.Count > 1)
                //{
                //    ddlAgency.SelectedIndex = 1;
                //ddlAgency_SelectedIndexChanged(null, null);
                //}
            }
            catch (Exception)
            {

            }
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(ddlCountry.SelectedValue);
            var s = db.CountryStates.Where(x => x.CountryID == ID).OrderBy(x => x.StateName).ToList();
            ddlState.DataValueField = "ID";
            ddlState.DataTextField = "StateName";
            ddlState.DataSource = s;
            ddlState.DataBind();
            ddlState_SelectedIndexChanged(null, null);
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int ID = Convert.ToInt32(ddlState.SelectedValue);
                //try
                //{

                //    var gst = db.CountryStates.Where(x => x.ID == ID).SingleOrDefault();
                //    if (gst != null)
                //    {
                //        txtGSTRate.Text = gst.GSTRate.ToString();
                //    }
                //    else
                //    {
                //        txtGSTRate.Text = "0.00";
                //    }
                //}
                //catch (Exception)
                //{

                //}
                var s = db.CityManagements.Where(x => x.StateID == ID).OrderBy(x => x.CityName).ToList();
                ddlCity.DataValueField = "ID";
                ddlCity.DataTextField = "CityName";
                ddlCity.DataSource = s;
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("Select City", "0"));

            }
            catch (Exception)
            {
                ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
            }
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    HiddenField hd = (HiddenField)e.Row.FindControl("hdsuspend");
                    bool isSuspend = Convert.ToBoolean(hd.Value);
                    ImageButton img = (ImageButton)e.Row.FindControl("ImgSuspendButton");
                    if (isSuspend == true)
                    {
                        img.ImageUrl = "~/Content/Images/suspend.png";
                        img.ToolTip = "Click to Restore";
                    }
                    else
                    {
                        img.ImageUrl = "~/Content/Images/Restore.png";
                        img.ToolTip = "Click to Suspend";

                    }

                }
            }
        }
        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int ID = Convert.ToInt32(ddlAgency.SelectedValue);
            //var obj = db.Clients.Where(x => x.ID == ID).SingleOrDefault();
            //txtClient.Text = obj.Client1;           

            //txtContactPerson.Text = obj.ContactPerson;
            //txtContactnumber.Text = obj.ContactNo;
            //txtEmail.Text = obj.Email;
            //txtGST.Text = obj.GSTNo.ToString();
            //txtGSTRate.Text = obj.GSTRation.ToString();
            //TxtNTNNO.Text = obj.NTNNo;
            //txtCNIC.Text = obj.NICNo;
            //txtAddress.Text = obj.Address;
            //ddlGroup.SelectedValue = obj.Agency.GroupID.ToString();
            //ddlGroup_SelectedIndexChanged(null, null);
            //ddlAgency.SelectedValue = obj.AgencyID.ToString();
            //ddlCountry.SelectedValue = obj.CountryID.ToString();
            //ddlCountry_SelectedIndexChanged(null, null);
            //try
            //{
            //    ddlState.SelectedValue = obj.StateID.ToString();
            //    ddlState_SelectedIndexChanged(null, null);
            //    ddlCity.SelectedValue = obj.CityID.ToString();
            //}
            //catch (Exception)
            //{
            //    ddlState.SelectedIndex = 0;// obj.StateID.ToString();
            //    ddlCity.SelectedIndex = 0;
            //}

            ////ddlGSTProvince_SelectedIndexChanged(null, null);
            //ChkIsActive.Checked = Convert.ToBoolean(obj.Active);
            //IsSuspended.Checked = Convert.ToBoolean(obj.Suspended);
            //chkExempted.Checked = Convert.ToBoolean(obj.IsExempted);
            //IsGovt.Checked = Convert.ToBoolean(obj.IsGovernament);
            // BindGrid();
        }

        //protected void chkdirectclient_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkdirectclient.Checked)
        //    {
        //        var s = db.Agencies.Where(x => x.GroupID == 110000001 && x.ID == 110000001).SingleOrDefault(); // Don't change in any case 
        //        if (s != null)
        //        {
        //            ddlGroup.SelectedValue = s.GroupID.ToString(); // Direct Client
        //            ddlGroup_SelectedIndexChanged(null, null); 
        //            ddlAgency.SelectedValue = s.ID.ToString();
        //        }
        //    }
        //}

        protected void ddlGSTProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 ID = Convert.ToInt32(ddlGSTProvince.SelectedValue);
                var gst = db.GSTProvinces.Where(x => x.ID == ID).SingleOrDefault();
                if (gst != null)
                {
                    txtGSTRate.Text = gst.GSTRate.ToString();
                    ListItem selectedListItem = ddlGSTStation.Items.FindByValue(ID.ToString());
                    if (selectedListItem != null)
                    {
                        selectedListItem.Selected = true;
                    }
                    else
                    {

                    }
                }
                else
                {
                    txtGSTRate.Text = "0.00";
                }
            }
            catch (Exception)
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // var s=  db.Clients.Where (u=> u.Client1.Contains(txtClient.Text)
            if (txtSearch.Text.Trim().Length > 0)
            {
                var g = (from u in db.Clients.Where(u => u.Active == true && u.Client1.Contains(txtSearch.Text)) //.AgencyID == Id)
                         select new
                         {
                             u.ID,
                             u.Client1,
                             u.ContactPerson,
                             u.Email,
                             u.GSTNo,
                             u.NICNo,
                             u.NTNNo,
                             u.CountryState.StateName,
                             u.CityManagement.CityName,
                             u.Active,
                             u.Suspended,
                             u.AgencyID,
                             u.Agency.GroupID

                         }).OrderBy(x => x.Client1).ToList();
                DataTable dt = Helper.ToDataTable(g);
                ViewState["dt"] = dt;
                gv.DataSource = dt;
                gv.DataBind();
            }
        }
    }
}