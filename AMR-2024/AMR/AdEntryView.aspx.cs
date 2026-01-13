using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class AdEntryView : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //PopulateCityDropdown();
                PopulateCityCheckboxList();
                //PopulatePubDropdown();
                PopulatePubCheckboxList();
                PopulateMainCatDropdown();
            }
        }
        //private void PopulateCityDropdown()
        //{
        //    var city = db.GroupComps
        //        .OrderBy(mc => mc.Abreviation).Select(mc => new
        //        {
        //            mc.GroupComp_Id,
        //            mc.Abreviation
        //        }).ToList();

        //    ddlcity.DataSource = city;
        //    ddlcity.DataValueField = "GroupComp_Id";
        //    ddlcity.DataTextField = "Abreviation";
        //    ddlcity.DataBind();

        //    //ddlcity.Items.Insert(0, new ListItem("Select City Edition", ""));

        //    ddlcity.Items.Insert(0, new ListItem("Select All", "All")); // Added
        //}

        private void PopulateCityCheckboxList()
        {
            var cityList = db.GroupComps
                .OrderBy(c => c.Abreviation)
                .Select(c => new { c.GroupComp_Id, c.Abreviation })
                .ToList();

            chkCity.DataSource = cityList;
            chkCity.DataValueField = "Abreviation";
            chkCity.DataTextField = "Abreviation";
            chkCity.DataBind();
        }
        private List<string> GetSelectedCheckBoxValues(CheckBoxList checkBoxList)
        {
            return checkBoxList.Items.Cast<ListItem>()
                .Where(i => i.Selected && !string.IsNullOrWhiteSpace(i.Value))
                .Select(i => i.Value.ToUpper().Trim())
                .ToList();
        }
        private void PopulatePubCheckboxList()
        {
            var pubList = db.Publications
                .OrderBy(c => c.Publication_Name)
                .Select(c => new { c.Id, c.Pub_Abreviation })
                .ToList();

            chkPub.DataSource = pubList;
            chkPub.DataValueField = "Id";
            chkPub.DataTextField = "Pub_Abreviation";
            chkPub.DataBind();
        }

        private List<int> GetSelectedPublicationIds()
        {
            return chkPub.Items.Cast<ListItem>()
                .Where(i => i.Selected && !string.IsNullOrWhiteSpace(i.Value))
                .Select(i => int.Parse(i.Value))
                .ToList();
        }

        private List<int> GetSelectedMaincatIds()
        {
            return chkmaincat.Items.Cast<ListItem>()
                .Where(i => i.Selected && !string.IsNullOrWhiteSpace(i.Value))
                .Select(i => int.Parse(i.Value))
                .ToList();
        }

        private List<int> GetSelectedSubcatIds()
        {
            return chksubcat.Items.Cast<ListItem>()
                .Where(i => i.Selected && !string.IsNullOrWhiteSpace(i.Value))
                .Select(i => int.Parse(i.Value))
                .ToList();
        }
        protected void chkSelectAllCity_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in chkCity.Items)
            {
                item.Selected = chkSelectAllCity.Checked;
            }
        }

        protected void chkSelectAllPub_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in chkPub.Items)
            {
                item.Selected = chkSelectAllPub.Checked;
            }
        }
        //private void PopulatePubDropdown()
        //{
        //    var pub = db.Publications
        //        .OrderBy(mc => mc.Publication_Name).Select(mc => new
        //        {
        //            mc.Id,
        //            mc.Publication_Name
        //        }).ToList();

        //    ddlpub.DataSource = pub;
        //    ddlpub.DataValueField = "Id";
        //    ddlpub.DataTextField = "Publication_Name";
        //    ddlpub.DataBind();

        //    ddlpub.Items.Insert(0, new ListItem("Select Publication", ""));
        //}

        private void PopulateMainCatDropdown()
        {
            var maincat = db.MainCategories
                .Where(mc => mc.Status == "A")
                .OrderBy(mc => mc.Category_Title).Select(mc => new
                {
                    mc.Id,
                    mc.Category_Title
                }).ToList();

            //ddlmaincat.DataSource = maincat;
            //ddlmaincat.DataValueField = "Id";
            //ddlmaincat.DataTextField = "Category_Title";
            //ddlmaincat.DataBind();

            //ddlmaincat.Items.Insert(0, new ListItem("Select Main Category", ""));
            chkmaincat.DataSource = maincat;
            chkmaincat.DataValueField = "Id";
            chkmaincat.DataTextField = "Category_Title";
            chkmaincat.DataBind();
        }

        //protected void ddlmaincat_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    PopulateSubCatDropdown();
        //}

        protected void chkMainCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected MainCategory IDs
            var selectedMainCats = chkmaincat.Items.Cast<ListItem>()
                .Where(i => i.Selected)
                .Select(i => Convert.ToInt32(i.Value))
                .ToList();

            if (selectedMainCats.Count == 0)
            {
                chksubcat.Items.Clear();
                return;
            }

            // Query all subcategories matching the selected main categories
            var subCats = db.SubCategories
                .Where(sc => selectedMainCats.Contains(sc.Main_Category.Value))
                .OrderBy(sc => sc.Category_Title)
                .Select(sc => new { sc.Id, sc.Category_Title })
                .ToList();

            chksubcat.DataSource = subCats;
            chksubcat.DataTextField = "Category_Title";
            chksubcat.DataValueField = "Id";
            chksubcat.DataBind();
        }

        //private void PopulateSubCatDropdown()
        //{
        //    int selectedmaincatId = Convert.ToInt32(ddlmaincat.SelectedValue);

        //    var subcat = db.SubCategories
        //        .Where(mc => mc.Status == "A" && mc.Main_Category == selectedmaincatId)
        //        .OrderBy(mc => mc.Category_Title).Select(mc => new
        //        {
        //            mc.Id,
        //            mc.Main_Category,
        //            mc.Category_Title
        //        }).ToList();

        //    ddlsubcat.DataSource = subcat;
        //    ddlsubcat.DataValueField = "Id";
        //    ddlsubcat.DataTextField = "Category_Title";
        //    ddlsubcat.DataBind();

        //    ddlsubcat.Items.Insert(0, new ListItem("Select Sub Category", ""));
        //}

        //private void PopulateClientDropdown()
        //{
        //    var client = db.ClientCompanies
        //        .Where(mc => mc.Status == "A" && mc.Client_Name != null && mc.Client_Name != "")
        //        .OrderBy(mc => mc.Client_Name).Take(100).
        //        Select(mc => new
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

        //protected void btnupdate_Click(object sender, EventArgs e)
        //{

        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtfromdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txttodate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtpubdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            //ddlcity.SelectedIndex = 0;
           // ddlpub.SelectedIndex = 0;
            txtClient.Text=string.Empty;
            txtro.Text = string.Empty;
            gv.DataSource = null;
            gv.DataBind();
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            btnfilter_Click(sender, e);
        }

        protected void btnfilter_Click(object sender, EventArgs e)
        {
            string fromDate = txtfromdate.Value;
            string toDate = txttodate.Value;
            string pubDate = txtpubdate.Value;
            string pubDatefrom = txtpubdatefrom.Value;
            //string city = ddlcity.SelectedItem.Text;

            var selectedCities = GetSelectedCheckBoxValues(chkCity);

            // string publication = ddlpub.SelectedValue;
            List<int> selectedPubs = GetSelectedPublicationIds();

            //string maincat = ddlmaincat.SelectedValue;

            List<int> selectedmaincats = GetSelectedMaincatIds();

            //string subcat = ddlsubcat.SelectedValue;

            List<int> selectedsubcats = GetSelectedSubcatIds();

            string selectedClientId = hiddenClientId.Value;
            string pageno =txtpage.Text;
            string ro = txtro.Text.Trim();

            var query = db.Transactions.AsQueryable();

            // query = query.Where(t => t.Rec_Edited_By == null || !t.Rec_Edited_By.Contains("(D)"));

            query = query.Where(t => t.IsDeleted==false);


            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                DateTime startDate = DateTime.Parse(fromDate);
                query = query.Where(t => t.Rec_Added_Date == startDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime startDate = DateTime.Parse(fromDate);
                DateTime endDate = DateTime.Parse(toDate);
                query = query.Where(t => t.Rec_Added_Date >= startDate && t.Rec_Added_Date <= endDate);
            }

            if (!string.IsNullOrEmpty(pubDate) && string.IsNullOrEmpty(pubDatefrom))
            {
                DateTime pub_Date = DateTime.Parse(pubDate);
                query = query.Where(t => t.Publication_Date == pub_Date);
            }
            else if (!string.IsNullOrEmpty(pubDate) && !string.IsNullOrEmpty(pubDatefrom))
            {
                DateTime startDate = DateTime.Parse(pubDate);
                DateTime endDate = DateTime.Parse(pubDatefrom);
                query = query.Where(t => t.Publication_Date >= startDate && t.Publication_Date <= endDate);
            }
            //if (!string.IsNullOrEmpty(city) && city != "Select City Edition")
            //{
            //    string cityId = city.ToString();
            //    query = query.Where(t => t.City_Edition == cityId);
            //}
            if (selectedCities.Any())
            {
                query = query.Where(t => selectedCities.Contains(t.City_Edition.ToUpper().Trim()));
            }

            //if (!string.IsNullOrEmpty(publication))
            //{
            //    int publicationId = int.Parse(publication);
            //    query = query.Where(t => t.Publication == publicationId);
            //}
            if (selectedPubs.Any())
            {
                query = query.Where(t => selectedPubs.Contains(t.Publication.Value));
            }
            //if (!string.IsNullOrEmpty(maincat))
            //{
            //    int maincatId = int.Parse(maincat);
            //    query = query.Where(t => t.Main_Category == maincatId);
            //}
            if (selectedmaincats.Any())
            {
                query = query.Where(t => selectedmaincats.Contains(t.Main_Category.Value));
            }
            //if (!string.IsNullOrEmpty(subcat))
            //{
            //    int subcatId = int.Parse(subcat);
            //    query = query.Where(t => t.Sub_Category == subcatId);
            //}
            if (selectedsubcats.Any())
            {
                query = query.Where(t => selectedsubcats.Contains(t.Sub_Category.Value));
            }
            if (!string.IsNullOrEmpty(selectedClientId))
            {
                int clientId = int.Parse(selectedClientId);
                query = query.Where(t => t.Client_Company == clientId);
            }

            if (!string.IsNullOrEmpty(ro))
            {
                query = query.Where(t => t.RO.Contains(ro));
            }

            if (!string.IsNullOrEmpty(pageno) && int.TryParse(pageno, out int pageNum))
            {
                query = query.Where(t => t.Page == pageNum);
            }

            var filteredData = query
                .Join(db.Publications, t => t.Publication, p => p.Id, (t, p) => new { t, p })
                .Join(db.MainCategories, tp => tp.t.Main_Category, m => m.Id, (tp, m) => new { tp.t, tp.p, m })
                .Join(db.SubCategories, tpm => tpm.t.Sub_Category, s => s.Id, (tpm, s) => new { tpm.t, tpm.p, tpm.m, s })
                .Join(db.ClientCompanies, tpms => tpms.t.Client_Company, c => c.Id, (tpms, c) => new
                {
                    tpms.t.Id,
                    tpms.t.Publication_Date,
                    City = tpms.t.City_Edition,
                    Publication = tpms.p.Publication_Name,
                    tpms.t.Page,
                    MainCategory = tpms.m.Category_Title,
                    SubCategory = tpms.s.Category_Title,
                    Client = c.Client_Name,
                    tpms.t.Size_CM,
                    tpms.t.Col_Size,
                    CM = tpms.t.Size_CM * tpms.t.Col_Size,
                    tpms.t.RO,tpms.t.Colour_BW,tpms.t.Rec_Added_Time
                }).OrderByDescending(tpm=>tpm.Publication_Date).ThenByDescending(tpm=>tpm.Publication).
                ThenByDescending(tpm=>tpm.City).ThenByDescending(tpm=>tpm.Rec_Added_Time)
                .ToList();

            if (filteredData.Count > 0)
            {
                // Compute Total CM BEFORE binding
                int totalCM = filteredData.Sum(x => (x.CM ?? 0));


                gv.DataSource = filteredData;
                gv.DataBind();

                if (gv.FooterRow != null)
                {
                    Label lblTotalCM = (Label)gv.FooterRow.FindControl("lblTotalCM");
                    if (lblTotalCM != null)
                    {
                        lblTotalCM.Text = "Total: " + totalCM.ToString("N2");
                    }
                }

                //gv.DataSource = filteredData;
                //gv.DataBind();

                
            }
            else
            {
                gv.DataSource = null;
                gv.DataBind();
            }
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
                        lblmessage.Text = "Record deleted successfully.";
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
            btnfilter_Click(sender, e);
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Check the user's RoleId from cookie or session
                string roleIdStr = Request.Cookies["RoleId"]?.Value;

                if (!string.IsNullOrEmpty(roleIdStr) && roleIdStr == "110000007")
                {
                    // Hide Edit and Delete controls for this role
                    HyperLink editLink = (HyperLink)e.Row.FindControl("hypEdit");
                    ImageButton deleteBtn = (ImageButton)e.Row.FindControl("DelButton");

                    if (editLink != null)
                        editLink.Visible = false;

                    if (deleteBtn != null)
                        deleteBtn.Visible = false;
                }
            }
        }

        protected void chkselectallMain_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in chkmaincat.Items)
            {
                item.Selected = chkselectallMain.Checked;
            }
        }

        protected void chkselectallsub_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in chksubcat.Items)
            {
                item.Selected = chkselectallsub.Checked;
            }
        }
    }
}