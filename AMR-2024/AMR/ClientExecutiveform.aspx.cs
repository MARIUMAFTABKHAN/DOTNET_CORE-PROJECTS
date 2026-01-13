using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class ClientExecutiveform : BaseClass
    {
        Model1Container db=new Model1Container();
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
                PopulateBrandDropdown();
                PopulateProfileDropdown();
                PopulateUsersDropdown1();
                PopulateUsersDropdown2();
               // PopulateClientDropdown();
                
                if (nId != 0)
                {
                    LoadValues(nId);
                }
            }
            
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
        protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopulateBrandDropdown()
        {
            var brand = db.Brands
                .Where(mc => mc.Status == "A" && mc.Brand_Name!=null && mc.Brand_Name != "")
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
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = EnsureDataTable();

            if (!string.IsNullOrEmpty(ddlbrand.SelectedValue))
            {
                int editIndex = Convert.ToInt32(hfEditIndex.Value);
                int selectedBrandId = Convert.ToInt32(ddlbrand.SelectedValue);
                if (editIndex == -1)
                {
                    if (dt.AsEnumerable().Any(row => row.Field<int>("Id") == selectedBrandId))
                    {
                        return;
                    }

                    DataRow dr = dt.NewRow();
                    dr["Id"] = ddlbrand.SelectedValue;
                    dr["Brand_Name"] = ddlbrand.SelectedItem.Text;
                    dt.Rows.Add(dr);
                }
                else
                {
                    DataRow dr = dt.Rows[editIndex];
                    dr["Id"] = selectedBrandId;
                    dr["Brand_Name"] = ddlbrand.SelectedItem.Text;
                    hfEditIndex.Value = "-1";
                }

                ViewState["SelectedBrands"] = dt;
                BindGrid();
                ddlbrand.SelectedIndex = 0;
            }

        }
        private DataTable EnsureDataTable()
        {
            DataTable dt = ViewState["SelectedBrands"] as DataTable;
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Brand_Name", typeof(string));

                ViewState["SelectedBrands"] = dt;  
            }
            return dt;
        }
        private void BindGrid()
        {
            DataTable dt = EnsureDataTable();
            gvBrands.DataSource = dt;
            gvBrands.DataBind();
        }
        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)btnDelete.NamingContainer;
            string brandId = ((Label)gvRow.FindControl("lblBrandId")).Text;

            DataTable dt = ViewState["SelectedBrands"] as DataTable;
            DataRow[] rows = dt.Select($"Id = '{brandId}'");

            if (rows.Length > 0)
            {
                dt.Rows.Remove(rows[0]);
                ViewState["SelectedBrands"] = dt; 
                BindGrid();
            }
        }
       

        protected void ddlexe1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlexe2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {

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
        //private void PopulateClientDropdown()
        //{
        //    var client = db.ClientCompanies
        //        .Where(mc => mc.Status == "A")
        //        .OrderBy(mc => mc.Client_Name).Take(100).Select(mc => new
        //        {
        //            mc.Id,
        //            mc.Client_Name
        //        }).ToList();

        //    ddlclient.DataSource = client;
        //    ddlclient.DataValueField = "Id";
        //    ddlclient.DataTextField = "Client_Name";
        //    ddlclient.DataBind();

        //    ddlclient.Items.Insert(0, new ListItem("Select Client", ""));
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        ClientExecutive obj = new ClientExecutive();
                        obj.Id = db.usp_IDctr("ClientExecutives").SingleOrDefault().Value;
                        obj.Status = chstatus.Checked ? "A" : "I";

                        string selectedClientId = hiddenClientId.Value;
                        obj.Client_Id = Convert.ToInt32(selectedClientId);
                        //obj.Client_Id = !string.IsNullOrEmpty(ddlclient.SelectedValue) ? Convert.ToInt32(ddlclient.SelectedValue) : (int?)null;

                        //obj.Client_Id=ddlclient.SelectedIndex;
                        obj.Executive_Name = txtname.Text;
                        obj.Designation=txtdesg.Text;
                        obj.Direct_Phone=txtphone.Text;
                        obj.Mobile_Phone=txtmob.Text;
                        obj.EMail_Address=txtemail.Text;
                        obj.Remarks=txthobby.Text;
                        obj.Address=txtadd.Text;
                        obj.Phone=txthomeph.Text;
                        obj.City=txtcity.Text;
                        obj.Postal_Code =Convert.ToInt32(txtcode.Text);

                        string selectedProfileText = ddlprofile.SelectedItem.Text;
                        obj.Department= selectedProfileText;

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

                        db.ClientExecutives.Add(obj);

                        db.SaveChanges();
                        SaveGridViewData(obj.Id);

                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Client Executive  Created Successfully";
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
                        var obj = db.ClientExecutives.Where(x => x.Id == nId).SingleOrDefault();

                        obj.Id = nId;
                        obj.Status = chstatus.Checked ? "A" : "I";
                        string selectedClientId = hiddenClientId.Value;
                        obj.Client_Id = Convert.ToInt32(selectedClientId);
                        //obj.Client_Id = !string.IsNullOrEmpty(ddlclient.SelectedValue) ? Convert.ToInt32(ddlclient.SelectedValue) : (int?)null;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtClient.Text = string.Empty;
            txtdesg.Text=string.Empty;
            txtname.Text=string.Empty;
            chstatus.Checked = false;
            txtphone.Text=string.Empty;
            txtmob.Text=string.Empty;   
            txthomeph.Text=string.Empty;
            txtadd.Text=string.Empty;   
            txtcity.Text=string.Empty;  
            txtcode.Text=string.Empty;
            txtemail.Text=string.Empty; 
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
            ddlbrand.SelectedIndex = 0;
            gvBrands.DataSource = null;
            gvBrands.DataBind();
        }
        private void SaveGridViewData(int clientExecutiveId)
        {
            var existingRecords = db.CExecutiveBrands.Where(c => c.Executive_Id == clientExecutiveId).ToList();
            db.CExecutiveBrands.RemoveRange(existingRecords);

            foreach (GridViewRow row in gvBrands.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lblBrandId = row.FindControl("lblBrandId") as Label;
                    Label lblBrandName = row.FindControl("lblBrandName") as Label;

                    int brandId = Convert.ToInt32(lblBrandId.Text);
                    string brandName = lblBrandName.Text;

                    var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();


                    CExecutiveBrand detail = new CExecutiveBrand
                    {
                        Id = db.usp_IDctr("CExecutiveBrands").SingleOrDefault().Value,
                        Executive_Id = clientExecutiveId,
                        Brand_Id = brandId,
                        Rec_Added_By = null,
                        Rec_Added_Date = null,
                        Rec_Added_Time = null,
                        Rec_Edited_By = null,
                        Rec_Edited_Date = null,
                        Rec_Edited_Time = null,
                        Grp = null
                    };

                    db.CExecutiveBrands.Add(detail);
                }
            }

            db.SaveChanges();
        }

        protected void LoadValues (Int32 ID)
        {
            var obj=db.ClientExecutives.Where(x=>x.Id==ID).SingleOrDefault();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "populateClient",
            $"populateClient({obj.Client_Id});", true);

            txtdesg.Text = obj.Designation;
            txtname.Text = obj.Executive_Name;
            chstatus.Checked = obj.Status=="A";
            txtphone.Text = obj.Direct_Phone;
            txtmob.Text = obj.Mobile_Phone;
            txthomeph.Text = obj.Phone;
            txtadd.Text = obj.Address;
            txtcity.Text = obj.City;
            txtcode.Text =obj.Postal_Code.ToString();
            txtemail.Text = obj.EMail_Address;
            ddlprofile.SelectedValue=obj.Department;
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
            //txtbirthdate.Value =obj.DOB.ToString();
            ddlmartial.SelectedValue=obj.Marital_Status;
            txthobby.Text = obj.Remarks;

            if (!string.IsNullOrEmpty(obj.Exec1_Id) &&
            ddlexe1.Items.FindByValue(obj.Exec1_Id) != null)
            {
                ddlexe1.SelectedValue = obj.Exec1_Id;
            }
            ddlrating1.SelectedValue=obj.Exec1_Rel;
            txtremarks1.Text = obj.Exec1_Remarks;

            if (!string.IsNullOrEmpty(obj.Exec2_Id) &&
            ddlexe2.Items.FindByValue(obj.Exec2_Id) != null)
            {
                ddlexe2.SelectedValue = obj.Exec2_Id;
            }
            ddlrating2.SelectedValue = obj.Exec2_Rel;
            txtremarks2.Text = obj.Exec2_Remarks;

            var brandList = (from ceb in db.CExecutiveBrands
                             join b in db.Brands on ceb.Brand_Id equals b.Id
                             where ceb.Executive_Id == ID
                             select new
                             {
                                 b.Id,
                                 ceb.Brand_Id,
                                 b.Brand_Name
                             }).ToList();

            if (brandList.Count > 0)
            {
                DataTable dtBrands = new DataTable();
                dtBrands.Columns.Add("Id", typeof(int));
                dtBrands.Columns.Add("Brand_Id", typeof(int));
                dtBrands.Columns.Add("Brand_Name", typeof(string));

                foreach (var item in brandList)
                {
                    DataRow dr = dtBrands.NewRow();
                    dr["Id"] = item.Id;
                    dr["Brand_Id"] = item.Brand_Id;
                    dr["Brand_Name"] = item.Brand_Name;
                    dtBrands.Rows.Add(dr);
                }

                ViewState["SelectedBrands"] = dtBrands;

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
        //protected void EditButton_Click(object sender, EventArgs e)
        //{
        //    ImageButton btnEdit = (ImageButton)sender;
        //    GridViewRow gvRow = (GridViewRow)btnEdit.NamingContainer;
        //    int rowIndex = gvRow.RowIndex;

        //    hfEditIndex.Value = rowIndex.ToString();

        //    string brandId = ((Label)gvRow.FindControl("lblBrandId")).Text;
        //    ddlbrand.SelectedValue = brandId;
        //}
        [System.Web.Services.WebMethod]
        public static List<Client> SearchClients(string searchText)
        {
            using (var db = new Model1Container())
            {
                return db.ClientCompanies
                    .Where(c => c.Status == "A" &&
                                !string.IsNullOrEmpty(c.Client_Name) &&
                                c.Client_Name.Contains(searchText))
                    .Select(c => new Client
                    {
                        Id = c.Id,
                        Client_Name = c.Client_Name
                    })
                    .ToList();
            }
        }

        public class Client
        {
            public int Id { get; set; }
            public string Client_Name { get; set; }
        }
        [System.Web.Services.WebMethod]
        public static ClientCompanyDTO PopulateClientDetails(int clientId)
        {
            using (var db = new Model1Container())
            {
                var result = (from client in db.ClientCompanies
                              
                              where client.Id == clientId
                              select new ClientCompanyDTO
                              {
                                  ClientName = client.Client_Name,
                                  
                              }).FirstOrDefault();

                return result ?? new ClientCompanyDTO { ClientName = "No Client Found"};
            }
        }

        public class ClientCompanyDTO
        {
            public string ClientName { get; set; }
            
        }
    }
}