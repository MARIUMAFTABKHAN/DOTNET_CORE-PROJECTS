using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml.Linq;
using System.Data.Entity.Validation;

namespace AMR
{
    public partial class AdEntry : BaseClass
    {
        Model1Container db= new Model1Container();
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
                txtClient.Attributes["autocomplete"] = "off"; // Disable browser autocomplete

                txtpubdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                filterpubdate.Value= DateTime.Now.ToString("yyyy-MM-dd");
                filterquery();
                PopulatePageNumberDropdown();
                PopulateCMNumberDropdown();
                PopulateColNumberDropdown();
                PopulateCityDropdown();
                PopulatePubDropdown();
                PopulateAgencyDropdown();
                PopulateCampaignDropdown();

                ddltype.Items.Clear(); // Clear any existing items
                ddltype.Items.Add(new ListItem("Brand", "Brand")); // Set "Brand" as text
                ddltype.Enabled = false; // Disable ddltype on page load
                //txtBrand.Enabled = true;
                 ddlBrand.Enabled = true;

                if (nId != 0)
                {
                    LoadValues(nId);
                }

            }
        }
        private void filterquery()
        {
            string insertDate = filterpubdate.Value;
            string user = Request.Cookies["UserId"]?.Value;

            using (var db = new Model1Container())
            {
                string query = @"select t.Id,cast(t.Publication_Date as Date)as PubDate,t.Client_Company,c.Client_Name as Client,
                                t.Size_CM as CM,
                                t.Col_Size as COL,t.Publication,p.Pub_Abreviation as Pub,
                                t.Main_Category as Category,m.Category_Title as Main_Cat,
                                t.Rec_Added_Time
                                from Transactions t
                                inner join ClientCompanies c on t.Client_Company=c.Id
                                inner join Publications p on t.Publication=p.Id
                                inner join MainCategories m on t.Main_Category=m.Id
                                where t.Rec_Added_By =@user
                                and t.Rec_Added_Date=@insertDate and t.isdeleted=0
                                order by t.Rec_Added_Time desc ";
                if (DateTime.TryParse(insertDate, out DateTime parsedDate))
                {
                    var userParam = new System.Data.SqlClient.SqlParameter("@user", user);
                    var dateParam = new System.Data.SqlClient.SqlParameter("@insertDate", parsedDate.Date);

                    var result = db.Database.SqlQuery<FilterResultModel>(query, userParam, dateParam).ToList();

                    // Bind to GridView or use result here
                    gv.DataSource = result;
                    gv.DataBind();
                }
                else
                {
                    // Handle invalid date input
                    lblmessage.Text = "Invalid or missing publication date.";
                }
            }


        }
        public class FilterResultModel
        {
            public int Id { get; set; }
            public DateTime PubDate { get; set; }
            public int Client_Company { get; set; }
            public string Client { get; set; }
            public int CM { get; set; }
            public int COL { get; set; }
            public int Publication { get; set; }
            public string Pub { get; set; }
            public int Category { get; set; }
            public string Main_Cat { get; set; }
            public DateTime Rec_Added_Time { get; set; }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            ImageButton deleteButton = (ImageButton)sender;
            int id = Convert.ToInt32(deleteButton.CommandArgument);

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    var record = db.Transactions.SingleOrDefault(x => x.Id == id);
                    if (record != null)
                    {
                        // Mark as soft deleted
                        record.IsDeleted = true;

                        record.Rec_Edited_By = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edited_Date = currentDateTime.Date;
                        record.Rec_Edited_Time = currentDateTime;

                        // Parse user ID safely from cookie
                        int userId = 0;
                        int.TryParse(Request.Cookies["ID"]?.Value, out userId);

                        db.LogRecords.Add(new LogRecord
                        {
                            RecordID = id,
                            ActionOnForm = "AdEntry",
                            ActionTaken = "Deleted",
                            ActionBy = userId,
                            ActionOn = currentDateTime
                        });

                        db.SaveChanges();
                        scope.Complete();
                        
                        //btnfilter_Click(sender, e); 



                    }
                    else
                    {
                        lblmessage.Text = "Record not found.";
                    }
                }
                catch (Exception ex)
                {
                    lblmessage.Text = $"Error: {ex.Message}";
                }
            }
            lblmessage.Text = "Record deleted successfully.";
            filterquery();
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            //btnfilter_Click(sender, e);
        }

        


        private void PopulatePageNumberDropdown()
        {
            ddlno.Items.Clear();

            for (int i = 1; i <= 250; i++)
            {
                ddlno.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        private void PopulateCMNumberDropdown()
        {
            ddlcm.Items.Clear();

            for (int i = 1; i <= 54; i++)
            {
                ddlcm.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        private void PopulateColNumberDropdown()
        {
            ddlcol.Items.Clear();

            for (int i = 1; i <= 8; i++)
            {
                ddlcol.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        private void PopulateCityDropdown()
        {
            var city = db.GroupComps
                .OrderBy(mc => mc.Abreviation).Select(mc => new
                {
                    mc.GroupComp_Id,
                    mc.Abreviation
                }).ToList();

            ddlcity.DataSource = city;
            ddlcity.DataValueField = "Abreviation";
            ddlcity.DataTextField = "Abreviation";
            ddlcity.DataBind();

            ddlcity.Items.Insert(0, new ListItem("Select City Edition", ""));
        }
        private void PopulatePubDropdown()
        {
            var pub = db.Publications.Where(mc=>mc.Status=="A")
                .OrderBy(mc => mc.Publication_Name).
                Select(mc => new
                {
                    mc.Id,
                    mc.Publication_Name
                }).ToList();

            ddlpub.DataSource = pub;
            ddlpub.DataValueField = "Id";
            ddlpub.DataTextField = "Publication_Name";
            ddlpub.DataBind();

            ddlpub.Items.Insert(0, new ListItem("Select Publication", ""));
        }
       

        protected void btnCancel_Click(object sender, EventArgs e)
        {
           // txtpubdate.Value= DateTime.Now.ToString("yyyy-MM-dd");
           // ddlcity.SelectedIndex = 0;
            //ddlpub.SelectedIndex = 0;
            ddlno.SelectedIndex= 0;
            txtClient.Text = string.Empty;
            txtClient.Enabled = true; // <--- Re-enable input
            lblclientcity.Visible = false;
            lblclientcity.InnerText = string.Empty; // <--- Clear city label text

            // Reset hidden fields
            hiddenClientId.Value = string.Empty;
            hdnSelectedMainCat.Value = string.Empty;
            hiddenSubCatId.Value = string.Empty;
            hiddenBrandId.Value = string.Empty;

            // Reset main category
            ddlmaincat.Items.Clear();
            ddlmaincat.Items.Insert(0, new ListItem("Select Main Category", ""));
            ddlmaincat.SelectedIndex = 0;

            // Reset sub category
            ddlsubcat.Items.Clear();
            ddlsubcat.Items.Insert(0, new ListItem("Select Sub Category", ""));
            ddlsubcat.SelectedIndex = 0;
            ddlsubcat.Enabled = true;

            // Reset brand
            ddlBrand.Items.Clear();
            ddlBrand.Items.Insert(0, new ListItem("Select Brand", ""));
            ddlBrand.SelectedIndex = 0;
            ddlBrand.Enabled = true;

            // Reset type
            ddltype.SelectedIndex = 0;
            ddltype.Enabled = false;
            btnMisc.Checked = false;

            txtidro.Text = string.Empty;

            //btnMisc.Checked=false;
            chbackpage.Checked=false;
            ddlagency.SelectedIndex = 0;
            ddlcampaign.SelectedIndex = 0;
            ddlcolor.SelectedIndex = 0;
            ddlcm.SelectedIndex = 0;
            ddlcol.SelectedIndex = 0;
            chsup.Checked = false;
            chfoc.Checked = false;
            btnSave.Text = "Save";
            txtCopyCount.Text = string.Empty;
            lblmessage.Visible = false;
        }
        
        private void PopulateAgencyDropdown()
        {
            var agency = db.Agencies.Where(sub => sub.Status == "A").OrderBy(sub => sub.Agency_Name)
                       .Select(sub => new
                       {
                           sub.Id,
                           sub.Agency_Name
                       })
                       .ToList();

            ddlagency.DataSource = agency;
            ddlagency.DataValueField = "Id";
            ddlagency.DataTextField = "Agency_Name";
            ddlagency.DataBind();

            ddlagency.Items.Insert(0, new ListItem("Select Agency", ""));
        }
        private void PopulateCampaignDropdown()
        {
            var camp = db.Campaigns.Where(sub => sub.Active == true).OrderBy(sub => sub.Title)
                       .Select(sub => new
                       {
                           sub.ID,
                           sub.Title
                       })
                       .ToList();

            ddlcampaign.DataSource = camp;
            ddlcampaign.DataValueField = "ID";
            ddlcampaign.DataTextField = "Title";
            ddlcampaign.DataBind();

            ddlcampaign.Items.Insert(0, new ListItem("Select Campaign", ""));
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdEntryView.aspx");
        }

        protected void chbackpage_CheckedChanged(object sender, EventArgs e)
        {
            if(chbackpage.Checked)
            {
                ddlno.Enabled = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        Transaction obj = new Transaction();
                        obj.Id = db.usp_IDctr("Transactions").SingleOrDefault().Value;
                        string datestring = txtpubdate.Value;
                        DateTime pubDate;
                        if (DateTime.TryParse(datestring, out pubDate))
                        {
                            obj.Publication_Date = pubDate;
                        }
                        else
                        {
                            lblmessage.Text = "Invalid date format.";
                        }
                        obj.City_Edition = ddlcity.SelectedItem.Text;
                        obj.Publication = Convert.ToInt32(ddlpub.SelectedValue);
                        if (chbackpage.Checked)
                        {
                            obj.Page = 0;
                        }
                        else
                        {
                            obj.Page = Convert.ToInt32(ddlno.SelectedValue);
                        }

                        string selectedClientId = hiddenClientId.Value;
                        string selectedMainCat = hdnSelectedMainCat.Value;
                        string selectedSubCatId = hiddenSubCatId.Value;
                        string selectedBrandId = hiddenBrandId.Value;
                        string selectedTypeId = hiddenTypeId.Value;

                        obj.Main_Category = Convert.ToInt32(selectedMainCat);
                        if (!int.TryParse(selectedSubCatId, out int subCatId))
                        {
                            lblmessage.Text = "Please select a valid Sub Category.";
                            return;
                        }
                        obj.Sub_Category = Convert.ToInt32(selectedSubCatId);
                        obj.Client_Company = Convert.ToInt32(selectedClientId);

                        var brandvar = Convert.ToInt32(selectedClientId);
                        var brandExists = db.Brands.Any(b => b.Company == brandvar);

                        if (brandExists)
                        {
                            obj.Ad_Type = "B";
                        }
                        else
                        {
                            obj.Ad_Type = "N";
                        }

                        if (btnMisc.Checked)
                        {
                            obj.Type_Id = Convert.ToInt32(selectedTypeId);
                            obj.Brand = 0;
                        }
                        else
                        {
                            obj.Brand = Convert.ToInt32(selectedBrandId);
                            obj.Type_Id = null;
                        }

                        int AgencyId;
                        obj.Agency = int.TryParse(ddlagency.SelectedValue, out AgencyId) ? AgencyId : 0;

                       // obj.Agency = Convert.ToInt32(ddlagency.SelectedValue);
                        obj.Caption = null;
                        obj.Size_CM = Convert.ToInt32(ddlcm.SelectedValue);
                        obj.Size_Col = null;
                        string selectedcolor = ddlcolor.SelectedValue.Trim();
                        //if (!int.TryParse(selectedcolor, out int colorId))
                        //{
                        //    lblmessage.Text = "Please select a Color.";
                        //    return;
                        //}
                        switch (selectedcolor)
                        {
                            case "F":
                                obj.Colour_BW = "F";
                                break;
                            case "S":
                                obj.Colour_BW = "S";
                                break;
                            case "B":
                                obj.Colour_BW = "B";
                                break;
                            default:
                                obj.Colour_BW = null;
                                break;
                        }

                        obj.cExport = chsup.Checked;
                        obj.Orignal_ID = Convert.ToInt32(chfoc.Checked);
                        obj.Col_Size = Convert.ToInt32(ddlcol.SelectedValue);

                        int campaignId;
                        obj.Campaign_Id = int.TryParse(ddlcampaign.SelectedValue, out campaignId) ? campaignId : 0;


                        //obj.Campaign_Id = Convert.ToInt32(ddlcampaign.SelectedValue);
                        obj.RO = txtidro.Text;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edited_By = null; 
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        db.Transactions.Add(obj);

                        db.SaveChanges();

                        scope.Complete();

                        
                        
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var eve in ex.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                    //catch (Exception ex)
                    //{
                    //    lblmessage.Text = $"Error: {ex.Message}\n{ex.StackTrace}";
                    //}
                }

                lblmessage.Text = "Ad Created Successfully";

                filterquery();
            }
            else
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        var obj = db.Transactions.Where(x => x.Id == nId).SingleOrDefault();

                        obj.Id = nId;
                        string datestring = txtpubdate.Value;
                        DateTime pubDate;
                        if (DateTime.TryParse(datestring, out pubDate))
                        {
                            obj.Publication_Date = pubDate;
                        }
                        else
                        {
                            lblmessage.Text = "Invalid date format.";
                        }
                        obj.City_Edition = ddlcity.SelectedItem.Text;
                        obj.Publication = Convert.ToInt32(ddlpub.SelectedValue);

                        if (chbackpage.Checked)
                        {
                            obj.Page = 0;
                        }
                        else
                        {
                            obj.Page = Convert.ToInt32(ddlno.SelectedValue);
                        }

                        string selectedClientId = hiddenClientId.Value;
                        string selectedMainCat = hdnSelectedMainCat.Value;
                        string selectedSubCatId = hiddenSubCatId.Value;
                        string selectedBrandId = hiddenBrandId.Value;
                        string selectedTypeId = hiddenTypeId.Value;

                        obj.Main_Category = Convert.ToInt32(selectedMainCat);
                        obj.Sub_Category = Convert.ToInt32(selectedSubCatId);
                        obj.Client_Company = Convert.ToInt32(selectedClientId);

                        var brandvar = Convert.ToInt32(selectedClientId);
                        var brandExists = db.Brands.Any(b => b.Company == brandvar);

                        if (brandExists)
                        {
                            obj.Ad_Type = "B";
                        }
                        else
                        {
                            obj.Ad_Type = "N";
                        }

                        if (btnMisc.Checked)
                        {
                            obj.Type_Id = Convert.ToInt32(selectedTypeId);
                            obj.Brand = 0;
                        }
                        else
                        {
                            obj.Brand = Convert.ToInt32(selectedBrandId);
                            obj.Type_Id = null;
                        }

                        int AgencyId;
                        obj.Agency = int.TryParse(ddlagency.SelectedValue, out AgencyId) ? AgencyId : 0;

                        //obj.Agency = Convert.ToInt32(ddlagency.SelectedValue);
                        obj.Caption = null;
                        obj.Size_CM = Convert.ToInt32(ddlcm.SelectedValue);
                        obj.Size_Col = null;
                        string selectedcolor = ddlcolor.SelectedValue.Trim();
                        switch (selectedcolor)
                        {
                            case "F":
                                obj.Colour_BW = "F";
                                break;
                            case "S":
                                obj.Colour_BW = "S";
                                break;
                            case "B":
                                obj.Colour_BW = "B";
                                break;
                            default:
                                obj.Colour_BW = null;
                                break;
                        }

                        obj.cExport = chsup.Checked;
                        obj.Orignal_ID = Convert.ToInt32(chfoc.Checked);
                        obj.Col_Size = Convert.ToInt32(ddlcol.SelectedValue);

                        int campaignId;
                        obj.Campaign_Id = int.TryParse(ddlcampaign.SelectedValue, out campaignId) ? campaignId : 0;

                        //obj.Campaign_Id = Convert.ToInt32(ddlcampaign.SelectedValue);
                        obj.RO = txtidro.Text;

                        obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edited_Date = currentDateTime.Date;
                        obj.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();

                        scope.Complete();
                        
                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = ExceptionHandler.GetException(ex);
                    }
                    
                }
                btnCancel_Click(null, null);

                lblmessage.Text = "Ad  Updated Successfully";
                filterquery();
            }
        }

        protected void LoadValues(Int32 ID)
        {
            var obj = db.Transactions.Where(x => x.Id == ID).SingleOrDefault();
            
            if (obj.Publication_Date.HasValue)
            {
                txtpubdate.Value = obj.Publication_Date.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                hdnPubDate.Value = string.Empty;
            }
            ddlcity.SelectedValue = obj.City_Edition.ToString();
            ddlpub.SelectedValue = obj.Publication.ToString();
            if (obj.Page == 0)
            {
                chbackpage.Checked = true;
                ddlno.Enabled = false;
            }
            else
            {
                chbackpage.Checked = false;
                ddlno.SelectedValue = obj.Page.ToString();
            }
            if (obj.Brand == 0)
            {
                btnMisc.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "populatefetchtype",
                $"populatefetchtype({obj.Type_Id});", true);
                //txtBrand.Enabled = false;
                ddlBrand.Enabled = false;
            }
            if (obj.Brand != 0)
            {
                btnMisc.Checked = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "populatefetchbrand",
                $"populatefetchbrand({obj.Brand});", true);
                ddltype.Enabled = false;
            }
           
            txtidro.Text = obj.RO;
            ddlagency.SelectedValue = obj.Agency.ToString();
            ddlcampaign.SelectedValue = obj.Campaign_Id.ToString();
            if(obj.Colour_BW!=null)
            {
                ddlcolor.SelectedValue = obj.Colour_BW.ToString();
            }
            else
            {
                ddlcol.ClearSelection();
            }

                ddlcm.SelectedValue = obj.Size_CM.ToString();
            ddlcol.SelectedValue = obj.Col_Size.ToString();
            chsup.Checked = obj.cExport;
            chfoc.Checked = obj.Orignal_ID.HasValue && obj.Orignal_ID.Value > 0;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "populateClientMainCatSubcatAndCity",
            $"populateClientMainCatSubcatAndCity({obj.Client_Company}, {obj.Main_Category}, {obj.Sub_Category});", true);

        }

        

        [System.Web.Services.WebMethod]
        public static List<Client> SearchClients(string searchText)
        {
            using (var db = new Model1Container())
            {

                var query = from cc in db.ClientCompanies
                            join mcat in db.MainCategories on cc.Main_Category equals mcat.Id
                            where cc.Status == "A" && !string.IsNullOrEmpty(cc.Client_Name) && cc.Client_Name.Contains(searchText)
                            orderby cc.Client_Name ascending
                            select new Client
                            {
                                Id = cc.Id,
                                Client_Name = cc.Client_Name + " - " + (cc.Address_Line_4 ?? "") + " - " + (mcat.Category_Title ?? "")
                            };

                return query.ToList();

            }
        }

        public class Client
        {
            public int Id { get; set; }
            public string Client_Name { get; set; }
        }

        [System.Web.Services.WebMethod]
        public static List<MainCategoryDTO> PopulateMainCatDropdown(int clientId)
        {
            using (var db = new Model1Container())
            {
                var maincat = (from client in db.ClientCompanies
                               join mainCategory in db.MainCategories
                               on client.Main_Category equals mainCategory.Id
                               where client.Id == clientId
                               select new MainCategoryDTO
                               {
                                   MainCategoryId = mainCategory.Id,
                                   CategoryTitle = mainCategory.Category_Title
                               }).ToList();

                
                return maincat;

                
            }
         
        }

        public class MainCategoryDTO
        {
            public int MainCategoryId { get; set; }
            public string CategoryTitle { get; set; }
        }
        [System.Web.Services.WebMethod]
        public static List<SubCategoryDTO> PopulateSubCategories(int mainCategoryId)
        {
            using (var db = new Model1Container())
            {
                if(mainCategoryId==1)
                {
                    var subcat = db.SubCategories
                    .Where(sub => sub.Main_Category == 1 && sub.Status == "A" && !new[] {10000098,10000099,10000100,
                        10000101,10000139,10000142,10000143 }.Contains(sub.Id)) .OrderBy(sub=>sub.Id)
                    .Select(sub => new SubCategoryDTO
                    {
                        Id = sub.Id,
                        CategoryTitle = sub.Category_Title
                    })
                    .ToList();

                    return subcat;
                }
                if(mainCategoryId==2)
                {
                    var subcat = db.SubCategories
                    .Where(sub => sub.Main_Category == 2 && sub.Status == "A" && !new[] {10000127,10000134,10000135
                                        ,10000137,10000140,10000141}.Contains(sub.Id)).OrderBy(sub => sub.Id)
                    .Select(sub => new SubCategoryDTO
                    {
                        Id = sub.Id,
                        CategoryTitle = sub.Category_Title
                    })
                    .ToList();

                    return subcat;
                }
                if(mainCategoryId== 20000001)
                {
                    var subcat = db.SubCategories
                    .Where(sub => sub.Main_Category == 20000001 && sub.Status == "A" && !new[] {10000102,10000103,10000104,10000105
                                        ,10000138,10000144}.Contains(sub.Id)).OrderBy(sub => sub.Id) // Apply filtering conditions
                    .Select(sub => new SubCategoryDTO
                    {
                        Id = sub.Id,
                        CategoryTitle = sub.Category_Title
                    })
                    .ToList();

                    return subcat;
                }
                //var subcat = db.SubCategories
                //    .Where(sub => sub.Main_Category == mainCategoryId && sub.Status == "A") // Apply filtering conditions
                //    .Select(sub => new SubCategoryDTO
                //    {
                //        Id = sub.Id,
                //        CategoryTitle = sub.Category_Title
                //    })
                //    .ToList();

                //return subcat;

                return new List<SubCategoryDTO>();
            }
        }

        public class SubCategoryDTO
        {
            public int Id { get; set; }
            public string CategoryTitle { get; set; }
        }
        [System.Web.Services.WebMethod]
        public static string PopulateClientCity(int clientId)
        {
            using (var db = new Model1Container())
            {
                var result = (from client in db.ClientCompanies
                              join groupComp in db.GroupComps
                              on (int)client.Edition_Responsible equals groupComp.GroupComp_Id into gc
                              from groupComp in gc.DefaultIfEmpty()
                              where client.Id == clientId
                              select new
                              {
                                  GroupCompName = groupComp != null ? groupComp.GroupComp_Name : null
                              }).FirstOrDefault();

                return result != null ? result.GroupCompName ?? "No Group Comp Found" : "No Group Comp Found";
            }
        }

        [System.Web.Services.WebMethod]
        public static ClientCompanyDTO PopulateClientDetails(int clientId)
        {
            using (var db = new Model1Container())
            {
                var result = (from client in db.ClientCompanies
                              join groupComp in db.GroupComps
                              on (int)client.Edition_Responsible equals groupComp.GroupComp_Id into gc
                              from groupComp in gc.DefaultIfEmpty()
                              where client.Id == clientId
                              select new ClientCompanyDTO
                              {
                                  ClientName = client.Client_Name,
                                  GroupCompName = groupComp != null ? groupComp.GroupComp_Name : "No Group Comp Found"
                              }).FirstOrDefault();

                return result ?? new ClientCompanyDTO { ClientName = "No Client Found", GroupCompName = "No City Found" };
            }
        }

        public class ClientCompanyDTO
        {
            public string ClientName { get; set; }
            public string GroupCompName { get; set; }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static List<Brand> PopulateBrandDropdown(int clientId)
        {
            using (var db = new Model1Container())
            {
                var brands = (from Brand in db.Brands
                              join company in db.ClientCompanies on Brand.Company equals company.Id
                              where Brand.Company == clientId
                              select new Brand
                              {
                                  Id = Brand.Id,
                                  Brand_Name = Brand.Brand_Name
                              }).ToList();


                return brands;


            }

        }
        //[System.Web.Services.WebMethod]
        //public static List<Brand> SearchBrands(string searchText)
        //{
        //    using (var db = new Model1Container())
        //    {
        //        return db.Brands
        //            .Where(b => b.Status == "A" && b.Brand_Name.Contains(searchText))
        //            .Select(b => new Brand
        //            {
        //                Id = b.Id,
        //                Brand_Name = b.Brand_Name
        //            })
        //            .ToList();

        //        //return brands;
        //    }
        //}
        public class Brand
        {
            public int Id { get; set; }
            public string Brand_Name { get; set; }
        }
        [System.Web.Services.WebMethod]
        public static List<TypeModel> GetTypes()
        {
            using (var db = new Model1Container()) 
            {
                var typedata = db.Types
                    .Select(sub => new TypeModel
                    {
                        Id = sub.Id,
                        Type1 = sub.Type1
                    })
                    .ToList();

                return typedata;
            }
        }

        // Define a model for the types
        public class TypeModel
        {
            public int Id { get; set; }
            public string Type1 { get; set; }
        }
        [System.Web.Services.WebMethod]
        public static List<BrandDTO> PopulateBrandDetails(int clientId)
        {
            using (var db = new Model1Container())
            {
                var result = (from Brand in db.Brands where Brand.Id == clientId
                              select new BrandDTO
                              {
                                  Id=Brand.Id,
                                  BrandName = Brand.Brand_Name
                                  
                              }).ToList();

                return result;
            }
        }

        public class BrandDTO
        {
            public int Id { get; set; }
            public string BrandName { get; set; }
            
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            int copyCount = 0;

            if (!int.TryParse(txtCopyCount.Text.Trim(), out copyCount) || copyCount <= 0)
            {
                lblmessage.Text = "Please enter a valid number of copies.";
                return;
            }

            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < copyCount; i++)
                    {
                        Transaction obj = new Transaction();
                        obj.Id = db.usp_IDctr("Transactions").SingleOrDefault().Value;

                        string datestring = txtpubdate.Value;
                        DateTime pubDate;
                        if (DateTime.TryParse(datestring, out pubDate))
                        {
                            obj.Publication_Date = pubDate;
                        }
                        else
                        {
                            lblmessage.Text = "Invalid date format.";
                            return;
                        }

                        obj.City_Edition = ddlcity.SelectedItem.Text;
                        obj.Publication = Convert.ToInt32(ddlpub.SelectedValue);
                        obj.Page = chbackpage.Checked ? 0 : Convert.ToInt32(ddlno.SelectedValue);
                        obj.Main_Category = Convert.ToInt32(hdnSelectedMainCat.Value);
                        obj.Sub_Category = Convert.ToInt32(hiddenSubCatId.Value);
                        obj.Client_Company = Convert.ToInt32(hiddenClientId.Value);

                        int brandvar = Convert.ToInt32(hiddenClientId.Value);
                        obj.Ad_Type = db.Brands.Any(b => b.Company == brandvar) ? "B" : "N";

                        if (btnMisc.Checked)
                        {
                            obj.Type_Id = Convert.ToInt32(hiddenTypeId.Value);
                            obj.Brand = 0;
                        }
                        else
                        {
                            obj.Brand = Convert.ToInt32(hiddenBrandId.Value);
                            obj.Type_Id = null;
                        }

                        int AgencyId;
                        obj.Agency = int.TryParse(ddlagency.SelectedValue, out AgencyId) ? AgencyId : 0;

                        obj.Caption = null;
                        obj.Size_CM = Convert.ToInt32(ddlcm.SelectedValue);
                        obj.Size_Col = null;

                        string selectedcolor = ddlcolor.SelectedValue.Trim();
                        switch (selectedcolor)
                        {
                            case "F":
                                obj.Colour_BW = "F";
                                break;
                            case "S":
                                obj.Colour_BW = "S";
                                break;
                            case "B":
                                obj.Colour_BW = "B";
                                break;
                            default:
                                obj.Colour_BW = null;
                                break;
                        }

                        obj.cExport = chsup.Checked;
                        obj.Orignal_ID = Convert.ToInt32(chfoc.Checked);
                        obj.Col_Size = Convert.ToInt32(ddlcol.SelectedValue);

                        int campaignId;
                        obj.Campaign_Id = int.TryParse(ddlcampaign.SelectedValue, out campaignId) ? campaignId : 0;

                        obj.RO = txtidro.Text;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Added_By = Request.Cookies["UserId"]?.Value;
                        obj.Rec_Added_Date = currentDateTime.Date;
                        obj.Rec_Added_Time = currentDateTime;

                        obj.Rec_Edited_By = null;
                        obj.Rec_Edited_Date = null;
                        obj.Rec_Edited_Time = null;

                        db.Transactions.Add(obj);
                    }

                    db.SaveChanges();
                    dbTransaction.Commit();
                    lblmessage.Text = $"{copyCount} ad(s) copied successfully.";
                    filterquery();
                }
                catch (Exception ex)
                {
                    lblmessage.Text = $"Error: {ex.Message}\n{ex.StackTrace}";
                }
            }
        }

       
    }
}