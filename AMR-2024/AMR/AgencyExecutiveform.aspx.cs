using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class AgencyExecutiveform : BaseClass
    {
        Model1Container db = new Model1Container();
        Int32 nId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["Id"] != null) && (Request.QueryString["Mode"] == "Edit"))
            {
                nId = int.Parse(Request.QueryString["Id"].ToString());
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Save";
            }
            if (!Page.IsPostBack)
            {
                txtann.Value = DateTime.Now.ToString("yyyy-MM-dd");
                txtbirthdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                PopulateProfileDropdown();
                PopulateUsersDropdown1();
                PopulateUsersDropdown2();
                PopulateAgencyDropdown();

                if (nId != 0)
                {
                    LoadValues(nId);
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleDropdown", "toggleDropdown();", true);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = EnsureDataTable();
            if(rbBrand.Checked)
            {
                if (!string.IsNullOrEmpty(ddlbrand.SelectedValue))
                {
                    int editIndex = Convert.ToInt32(hfEditIndex.Value);
                    int selectedBrandId = Convert.ToInt32(ddlbrand.SelectedValue);
                    if (editIndex == -1)
                    {
                        if (dt.AsEnumerable().Any(row => row.Field<int>("Brand_Id") == selectedBrandId))
                        {
                            return;
                        }

                        DataRow dr = dt.NewRow();
                        dr["Brand_Id"] = ddlbrand.SelectedValue;
                        dr["Name"] = ddlbrand.SelectedItem.Text;
                        dr["Grp"]=0;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.Rows[editIndex];
                        dr["Brand_Id"] = selectedBrandId;
                        dr["Name"] = ddlbrand.SelectedItem.Text;
                        dr["Grp"] = 0;
                        hfEditIndex.Value = "-1";
                    }

                    ViewState["SelectedItems"] = dt;
                    BindGrid();
                    ddlbrand.SelectedIndex = 0;
                    //ddlbrand.Visible = false;
                }
            }

            else if (rbClient.Checked)
            {
                if (!string.IsNullOrEmpty(ddlClient.SelectedValue))
                {
                    int editIndex = Convert.ToInt32(hfEditIndex.Value);
                    int selectedClientId = Convert.ToInt32(ddlClient.SelectedValue);

                    if (editIndex == -1)
                    {
                        // Check if the Client ID already exists in the DataTable
                        if (dt.AsEnumerable().Any(row => row.Field<int>("Brand_Id") == selectedClientId))
                        {
                            return;
                        }

                        DataRow dr = dt.NewRow();
                        dr["Brand_Id"] = selectedClientId;  // Assuming you're storing client IDs as Brand_Id
                        dr["Name"] = ddlClient.SelectedItem.Text;
                        dr["Grp"] = 1;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.Rows[editIndex];
                        dr["Brand_Id"] = selectedClientId;
                        dr["Name"] = ddlClient.SelectedItem.Text;
                        dr["Grp"] = 1;
                        hfEditIndex.Value = "-1";
                    }

                    ViewState["SelectedItems"] = dt;
                    BindGrid();
                    ddlClient.SelectedIndex = 0;
                    //ddlClient.Visible = false;
                }
            }
        }
        private DataTable EnsureDataTable()
        {
            DataTable dt = ViewState["SelectedItems"] as DataTable;
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Brand_Id", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Grp", typeof(int));

                ViewState["SelectedItems"] = dt;
            }
            return dt;
        }
        private void BindGrid()
        {
            DataTable dt = EnsureDataTable();
            gvBrands.DataSource = dt;
            gvBrands.DataBind();
        }
        private void PopulateProfileDropdown()
        {
            var profiles = new List<ListItem>
            {
                new ListItem("President", "1"),
                new ListItem("H.O Consumer Bank", "1"),
                new ListItem("Director Marketing", "2"),
                new ListItem("General Manager", "3"),
                new ListItem("Business Manager", "4"),
                new ListItem("Marketing Manager", "5"),
                new ListItem("Asst. Marketing Manager", "6"),
                new ListItem("Asst. Business Manager", "7"),
                new ListItem("Other", "8")
            };

            ddlprofile.DataSource = profiles;
            ddlprofile.DataTextField = "Text";
            ddlprofile.DataValueField = "Value";
            ddlprofile.DataBind();

            ddlprofile.Items.Insert(0, new ListItem("Select Profile", ""));
        }
        private void PopulateUsersDropdown1()
        {
            var user = db.Users
                .Where(mc => mc.User_Active == true)
                .OrderBy(mc => mc.User_Name).Select(mc => new
                {
                    mc.User_Id,
                    mc.User_Name
                }).ToList();

            ddlexe1.DataSource = user;
            ddlexe1.DataValueField = "User_Id";
            ddlexe1.DataTextField = "User_Name";
            ddlexe1.DataBind();

            ddlexe1.Items.Insert(0, new ListItem("Select  Executive", ""));
        }
        private void PopulateUsersDropdown2()
        {
            var user = db.Users
                .Where(mc => mc.User_Active == true)
                .OrderBy(mc => mc.User_Name).Select(mc => new
                {
                    mc.User_Id,
                    mc.User_Name
                }).ToList();

            ddlexe2.DataSource = user;
            ddlexe2.DataValueField = "User_Id";
            ddlexe2.DataTextField = "User_Name";
            ddlexe2.DataBind();

            ddlexe2.Items.Insert(0, new ListItem("Select  Executive", ""));
        }
        protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PopulateAgencyDropdown()
        {
            var agency = db.Agencies
                .Where(mc => mc.Status == "A")
                .OrderBy(mc => mc.Agency_Name).Select(mc => new
                {
                    mc.Id,
                    mc.Agency_Name
                }).ToList();

            ddlAgency.DataSource = agency;
            ddlAgency.DataValueField = "Id";
            ddlAgency.DataTextField = "Agency_Name";
            ddlAgency.DataBind();

            ddlAgency.Items.Insert(0, new ListItem("Select Agency", ""));
        }
        protected void ddlexe1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlexe2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        AgencyExecutive obj = new AgencyExecutive();
                        obj.Id = db.usp_IDctr("AgencyExecutives").SingleOrDefault().Value;
                        obj.Status = chstatus.Checked ? "A" : "I";
                        obj.Agency_Id = !string.IsNullOrEmpty(ddlAgency.SelectedValue) ? Convert.ToInt32(ddlAgency.SelectedValue) : (int?)null;

                        //obj.Client_Id=ddlclient.SelectedIndex;
                        obj.Executive_Name = txtname.Text;
                        obj.Designation = txtdesg.Text;
                        obj.Direct_Phone = txtphone.Text;
                        obj.Mobile_Phone = txtmob.Text;
                        obj.EMail_Address = txtemail.Text;
                        obj.Remarks = txthobby.Text;
                        obj.Address = txtadd.Text;
                        obj.Phone = txthomeph.Text;
                        obj.City = txtcity.Text;
                        obj.Postal_Code = Convert.ToInt32(txtcode.Text);

                        string selectedProfileText = ddlprofile.SelectedItem.Text;
                        obj.Department = selectedProfileText;

                        string birthdatestring = txtbirthdate.Value;
                        DateTime birthDate;
                        if (DateTime.TryParse(birthdatestring, out birthDate))
                        {
                            obj.DOB = birthDate;
                        }
                        else
                        {
                            lblmessage.Text = "Invalid date format.";
                        }

                        string selectedmartialstatus = ddlmartial.SelectedValue.Trim();
                        lblmessage.Text = $"Selected martial status: {selectedmartialstatus}";
                        switch (selectedmartialstatus)
                        {
                            case "M":
                                obj.Marital_Status = "M";
                                break;
                            case "S":
                                obj.Marital_Status = "S";
                                break;
                            default:
                                obj.Marital_Status = null;
                                break;
                        }

                        string anndatestring = txtann.Value;
                        DateTime annDate;
                        if (DateTime.TryParse(anndatestring, out annDate))
                        {
                            obj.Wedding_Ann = annDate;
                        }
                        else
                        {
                            lblmessage.Text = "Invalid date format.";
                        }

                        if (string.IsNullOrEmpty(ddlexe1.SelectedValue))
                        {
                            obj.Exec1_Id = "";
                        }
                        else
                        {
                            obj.Exec1_Id = ddlexe1.SelectedValue;
                        }

                        string selectedratexe1 = ddlrating1.SelectedValue.Trim();
                        //lblmessage.Text = $"Selected martial status: {selectedmartialstatus}";
                        switch (selectedratexe1)
                        {
                            case "A":
                                obj.Exec1_Rel = "A";
                                break;
                            case "B":
                                obj.Exec1_Rel = "B";
                                break;
                            case "C":
                                obj.Exec1_Rel = "C";
                                break;
                            default:
                                obj.Exec1_Rel = null;
                                break;
                        }

                        obj.Exec1_Remarks = txtremarks1.Text;

                        if (string.IsNullOrEmpty(ddlexe2.SelectedValue))
                        {
                            obj.Exec2_Id = "";
                        }
                        else
                        {
                            obj.Exec2_Id = ddlexe2.SelectedValue;
                        }

                        string selectedratexe2 = ddlrating2.SelectedValue.Trim();
                        //lblmessage.Text = $"Selected martial status: {selectedmartialstatus}";
                        switch (selectedratexe2)
                        {
                            case "A":
                                obj.Exec2_Rel = "A";
                                break;
                            case "B":
                                obj.Exec2_Rel = "B";
                                break;
                            case "C":
                                obj.Exec2_Rel = "C";
                                break;
                            default:
                                obj.Exec2_Rel = null;
                                break;
                        }

                        obj.Exec2_Remarks = txtremarks2.Text;


                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        obj.Grp = 0;
                        obj.cExport = false;

                        db.AgencyExecutives.Add(obj);

                        db.SaveChanges();
                        SaveGridViewData(obj.Id);

                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Agency Executive  Created Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = $"Error: {ex.Message}\n{ex.StackTrace}";
                    }
                }
            }
            else
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        var obj = db.AgencyExecutives.Where(x => x.Id == nId).SingleOrDefault();

                        obj.Id = nId;
                        obj.Status = chstatus.Checked ? "A" : "I";
                        obj.Agency_Id = !string.IsNullOrEmpty(ddlAgency.SelectedValue) ? Convert.ToInt32(ddlAgency.SelectedValue) : (int?)null;
                        obj.Executive_Name = txtname.Text;
                        obj.Designation = txtdesg.Text;
                        obj.Direct_Phone = txtphone.Text;
                        obj.Mobile_Phone = txtmob.Text;
                        obj.EMail_Address = txtemail.Text;
                        obj.Remarks = txthobby.Text;
                        obj.Address = txtadd.Text;
                        obj.Phone = txthomeph.Text;
                        obj.City = txtcity.Text;
                        obj.Postal_Code = Convert.ToInt32(txtcode.Text);

                        string selectedProfileText = ddlprofile.SelectedItem.Text;
                        obj.Department = selectedProfileText;

                        string birthdatestring = txtbirthdate.Value;
                        DateTime birthDate;
                        if (DateTime.TryParse(birthdatestring, out birthDate))
                        {
                            obj.DOB = birthDate;
                        }
                        else
                        {
                            lblmessage.Text = "Invalid date format.";
                        }

                        string selectedmartialstatus = ddlmartial.SelectedValue.Trim();
                        lblmessage.Text = $"Selected martial status: {selectedmartialstatus}";
                        switch (selectedmartialstatus)
                        {
                            case "M":
                                obj.Marital_Status = "M";
                                break;
                            case "S":
                                obj.Marital_Status = "S";
                                break;
                            default:
                                obj.Marital_Status = null;
                                break;
                        }

                        string anndatestring = txtann.Value;
                        DateTime annDate;
                        if (DateTime.TryParse(anndatestring, out annDate))
                        {
                            obj.Wedding_Ann = annDate;
                        }
                        else
                        {
                            lblmessage.Text = "Invalid date format.";
                        }

                        if (string.IsNullOrEmpty(ddlexe1.SelectedValue))
                        {
                            obj.Exec1_Id = "";
                        }
                        else
                        {
                            obj.Exec1_Id = ddlexe1.SelectedValue;
                        }

                        string selectedratexe1 = ddlrating1.SelectedValue.Trim();
                        //lblmessage.Text = $"Selected martial status: {selectedmartialstatus}";
                        switch (selectedratexe1)
                        {
                            case "A":
                                obj.Exec1_Rel = "A";
                                break;
                            case "B":
                                obj.Exec1_Rel = "B";
                                break;
                            case "C":
                                obj.Exec1_Rel = "C";
                                break;
                            default:
                                obj.Exec1_Rel = null;
                                break;
                        }

                        obj.Exec1_Remarks = txtremarks1.Text;

                        if (string.IsNullOrEmpty(ddlexe2.SelectedValue))
                        {
                            obj.Exec2_Id = "";
                        }
                        else
                        {
                            obj.Exec2_Id = ddlexe2.SelectedValue;
                        }

                        string selectedratexe2 = ddlrating2.SelectedValue.Trim();
                        //lblmessage.Text = $"Selected martial status: {selectedmartialstatus}";
                        switch (selectedratexe2)
                        {
                            case "A":
                                obj.Exec2_Rel = "A";
                                break;
                            case "B":
                                obj.Exec2_Rel = "B";
                                break;
                            case "C":
                                obj.Exec2_Rel = "C";
                                break;
                            default:
                                obj.Exec2_Rel = null;
                                break;
                        }

                        obj.Exec2_Remarks = txtremarks2.Text;

                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date = currentDateTime.Date;
                        obj.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;
                        obj.Grp = 0;
                        obj.cExport = false;

                        db.SaveChanges();
                        SaveGridViewData(obj.Id);

                        scope.Complete();
                        btnCancel_Click(null, null);

                        lblmessage.Text = "Client Executive  Updated Successfully";
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                }
            }
        }
        private void SaveGridViewData(int agencyExecutiveId)
        {
            var existingRecords = db.AExecutiveBrands.Where(c => c.Executive_Id == agencyExecutiveId).ToList();
            db.AExecutiveBrands.RemoveRange(existingRecords);

            foreach (GridViewRow row in gvBrands.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lblBrandId = row.FindControl("lblBrandId") as Label;
                    Label lblName = row.FindControl("lblName") as Label;
                    Label lblGrp = row.FindControl("lblGroup") as Label;

                    int brandId = Convert.ToInt32(lblBrandId.Text);
                    string Name = lblName.Text;
                    int group =Convert.ToInt32(lblGrp.Text);
                    byte? groupvalue = Convert.ToByte(group);

                    var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();


                    AExecutiveBrand detail = new AExecutiveBrand
                    {
                        Id = db.usp_IDctr("AExecutiveBrands").SingleOrDefault().Value,
                        Executive_Id = agencyExecutiveId,
                        Brand_Id = brandId,
                        Rec_Added_By = null,
                        Rec_Added_Date = null,
                        Rec_Added_Time = null,
                        Rec_Edited_By = null,
                        Rec_Edited_Date = null,
                        Rec_Edited_Time = null,
                        Grp = groupvalue
                    };

                    db.AExecutiveBrands.Add(detail);
                }
            }

            db.SaveChanges();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlAgency.SelectedIndex = 0;
            txtdesg.Text = string.Empty;
            txtname.Text = string.Empty;
            chstatus.Checked = false;
            txtphone.Text = string.Empty;
            txtmob.Text = string.Empty;
            txthomeph.Text = string.Empty;
            txtadd.Text = string.Empty;
            txtcity.Text = string.Empty;
            txtcode.Text = string.Empty;
            txtemail.Text = string.Empty;
            ddlprofile.SelectedIndex = 0;
            txtann.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtbirthdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            ddlmartial.SelectedIndex = 0;
            txthobby.Text = string.Empty;
            ddlexe1.SelectedIndex = 0;
            ddlrating1.SelectedIndex = 0;
            txtremarks1.Text = string.Empty;
            ddlexe2.SelectedIndex = 0;
            ddlrating2.SelectedIndex = 0;
            txtremarks2.Text = string.Empty;
           // ddlbrand.SelectedIndex = 0;
            gvBrands.DataSource = null;
            gvBrands.DataBind();
        }

        protected void rbBrand_CheckedChanged(object sender, EventArgs e)
        {
            PopulateBrandDropdown();

            // Call the JavaScript function to toggle visibility on the client side
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleDropdown", "toggleDropdown();", true);

        }
        private void PopulateBrandDropdown()
        {
            var brand = db.Brands
                .Where(mc => mc.Status == "A" && mc.Brand_Name != null && mc.Brand_Name != "")
                .OrderBy(mc => mc.Brand_Name).Take(100).
                Select(mc => new
                {
                    mc.Id,
                    mc.Brand_Name
                }).ToList();

            ddlbrand.DataSource = brand;
            ddlbrand.DataValueField = "Id";
            ddlbrand.DataTextField = "Brand_Name";
            ddlbrand.DataBind();

            ddlbrand.Items.Insert(0, new ListItem("Select Brand", ""));
        }

        protected void rbClient_CheckedChanged(object sender, EventArgs e)
        {
            PopulateClientDropdown();

            // Call the JavaScript function to toggle visibility on the client side
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleDropdown", "toggleDropdown();", true);
        }
        private void PopulateClientDropdown()
        {
            var client = db.ClientCompanies
                .Where(mc => mc.Status == "A" && mc.Client_Name != null && mc.Client_Name != "")
                .OrderBy(mc => mc.Client_Name).Take(100).
                Select(mc => new
                {
                    mc.Id,
                    mc.Client_Name
                }).ToList();

            ddlClient.DataSource = client;
            ddlClient.DataValueField = "Id";
            ddlClient.DataTextField = "Client_Name";
            ddlClient.DataBind();

            ddlClient.Items.Insert(0, new ListItem("Select Client", ""));
        }

        protected void LoadValues(Int32 ID)
        {
            var obj = db.AgencyExecutives.Where(x => x.Id == ID).SingleOrDefault();
            ddlAgency.SelectedValue = obj.Agency_Id.ToString();
            txtdesg.Text = obj.Designation;
            txtname.Text = obj.Executive_Name;
            chstatus.Checked = obj.Status == "A";
            txtphone.Text = obj.Direct_Phone;
            txtmob.Text = obj.Mobile_Phone;
            txthomeph.Text = obj.Phone;
            txtadd.Text = obj.Address;
            txtcity.Text = obj.City;
            txtcode.Text = obj.Postal_Code.ToString();
            txtemail.Text = obj.EMail_Address;
            ddlprofile.SelectedValue = obj.Department;
            if (obj.Wedding_Ann.HasValue)
            {
                txtann.Value = obj.Wedding_Ann.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                hdnwedannDate.Value = string.Empty;
            }

            if (obj.DOB.HasValue)
            {
                txtbirthdate.Value = obj.DOB.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                hdndob.Value = string.Empty;
            }
            //txtann.Value = obj.Wedding_Ann.ToString();
            //txtbirthdate.Value = obj.DOB.ToString();
            ddlmartial.SelectedValue = obj.Marital_Status;
            txthobby.Text = obj.Remarks;

            //if (ddlexe1.Items.FindByValue(obj.Exec1_Id.ToString()) != null)
            //{
            //    ddlexe1.SelectedValue = obj.Exec1_Id.ToString();
            //}
            if (!string.IsNullOrEmpty(obj.Exec1_Id) &&
            ddlexe1.Items.FindByValue(obj.Exec1_Id) != null)
            {
                ddlexe1.SelectedValue = obj.Exec1_Id;
            }

            ddlrating1.SelectedValue = obj.Exec1_Rel;
            txtremarks1.Text = obj.Exec1_Remarks;

            //if (ddlexe2.Items.FindByValue(obj.Exec2_Id.ToString()) != null)
            //{
            //    ddlexe2.SelectedValue = obj.Exec2_Id.ToString();
            //}
            if (!string.IsNullOrEmpty(obj.Exec2_Id) &&
            ddlexe2.Items.FindByValue(obj.Exec2_Id) != null)
            {
                ddlexe2.SelectedValue = obj.Exec2_Id;
            }

            ddlrating2.SelectedValue = obj.Exec2_Rel;
            txtremarks2.Text = obj.Exec2_Remarks;

            var query = from aeb in db.AExecutiveBrands
                        join b in db.Brands on aeb.Brand_Id equals b.Id into brandGroup
                        from b in brandGroup.DefaultIfEmpty()
                        join cc in db.ClientCompanies on aeb.Brand_Id equals cc.Id into clientGroup
                        from cc in clientGroup.DefaultIfEmpty()
                        where aeb.Executive_Id == ID
                        select new
                        {
                            aeb.Id,
                            aeb.Executive_Id,
                            aeb.Brand_Id,
                            Name = aeb.Grp == 0 ? b.Brand_Name : aeb.Grp == 1 ? cc.Client_Name : null,
                            aeb.Grp
                        };

            //var query = "select AExecutiveBrands.Id,AExecutiveBrands.Executive_Id,AExecutiveBrands.Brand_Id," +
            //            "CASE WHEN AExecutiveBrands.Grp = 0 THEN Brands.Brand_Name" +
            //            "WHEN AExecutiveBrands.Grp = 1 THEN ClientCompanies.Client_Name" +
            //            "ELSE NULL" +
            //            "END AS Name,AExecutiveBrands.Grp from AExecutiveBrands" +
            //            "left join Brands on AExecutiveBrands.Grp=0 and AExecutiveBrands.Brand_Id=Brands.Id" +
            //            "left join ClientCompanies on AExecutiveBrands.Grp=1 and AExecutiveBrands.Brand_Id=ClientCompanies.Id" +
            //            "where AExecutiveBrands.Executive_Id=" + ID + " ";


            var result = query.ToList();

            if (result.Count > 0)
            {
                DataTable dtBrands = new DataTable();
                dtBrands.Columns.Add("Id", typeof(int));
                dtBrands.Columns.Add("Brand_Id", typeof(int));
                dtBrands.Columns.Add("Name", typeof(string));
                dtBrands.Columns.Add("Grp", typeof(int));

                foreach (var item in result)
                {
                    DataRow dr = dtBrands.NewRow();
                    dr["Id"] = item.Id;
                    dr["Brand_Id"] = item.Brand_Id;
                    dr["Name"] = item.Name;
                    dr["Grp"] = item.Grp;
                    dtBrands.Rows.Add(dr);
                }

                ViewState["SelectedItems"] = dtBrands;

                gvBrands.DataSource = dtBrands;
                gvBrands.DataBind();
            }
            else
            {
                lblmessage.Text = "No associated brands found.";
                gvBrands.DataSource = null;
                gvBrands.DataBind();
            }
        }
        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)btnDelete.NamingContainer;
            string brandId = ((Label)gvRow.FindControl("lblBrandId")).Text;

            DataTable dt = ViewState["SelectedItems"] as DataTable;
            DataRow[] rows = dt.Select($"Brand_Id = '{brandId}'");

            if (rows.Length > 0)
            {
                dt.Rows.Remove(rows[0]);
                ViewState["SelectedItems"] = dt;
                BindGrid();
            }
        }
    }
}