using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class AgencyBrandView : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateAgencyDropdown();
                //PopulateBrandDropdown();

            }
        }
        private void BindGrid()
        {
            int selectedAgencyId = Convert.ToInt32(ddlagency.SelectedValue); // Getting the selected Agency_Id from dropdown

            var query = from ab in db.AgencyBrands
                        join a in db.Agencies on ab.Agency equals a.Id into agencyGroup
                        from a in agencyGroup.DefaultIfEmpty() // Left join with Agencies
                        join b in db.Brands on ab.Brand equals b.Id into brandGroup
                        from b in brandGroup.DefaultIfEmpty() // Left join with Brands
                        join ae in db.AgencyExecutives on ab.Agency_Executive equals ae.Id into executiveGroup
                        from ae in executiveGroup.DefaultIfEmpty() // Left join with AgencyExecutives
                        where ab.Agency == selectedAgencyId
                        select new
                        {
                            ab.Id,
                            Agency = ab.Agency,
                            Agency_Name = a.Agency_Name,
                            Brand = ab.Brand,
                            Brand_Name = b.Brand_Name,
                            Agency_Executive = ab.Agency_Executive,
                            Executive_Name = ae.Executive_Name
                        };

            var result = query.ToList(); // Execute the query and get the results

            // Bind the result to the GridView or any other grid control
            gv.DataSource = result;
            gv.DataBind();
        }
        //private void PopulateBrandDropdown()
        //{
        //    var brand = db.Brands
        //        .Where(mc => mc.Status == "A" && mc.Brand_Name != null && mc.Brand_Name != "")
        //        .OrderBy(mc => mc.Brand_Name).//Take(100).
        //        Select(mc => new
        //        {
        //            mc.Id,
        //            mc.Brand_Name
        //        }).ToList();

        //    ddlbrand.DataSource = brand;
        //    ddlbrand.DataValueField = "Id";
        //    ddlbrand.DataTextField = "Brand_Name";
        //    ddlbrand.DataBind();

        //    ddlbrand.Items.Insert(0, new ListItem("Select Brand", ""));
        //}
        private void PopulateAgencyDropdown()
        {
            var agency = db.Agencies
                .Where(mc => mc.Status == "A")
                .OrderBy(mc => mc.Agency_Name).Select(mc => new
                {
                    mc.Id,
                    mc.Agency_Name
                }).ToList();

            ddlagency.DataSource = agency;
            ddlagency.DataValueField = "Id";
            ddlagency.DataTextField = "Agency_Name";
            ddlagency.DataBind();

            ddlagency.Items.Insert(0, new ListItem("Select Agency", ""));
        }
        private void PopulateAgencyExeDropdown()
        {
            if(!string.IsNullOrEmpty(ddlagency.SelectedValue))
            {
                int selectedAgencyId = Convert.ToInt32(ddlagency.SelectedValue);

                var agencyexe = db.AgencyExecutives
                    .Where(mc => mc.Status == "A" && mc.Agency_Id == selectedAgencyId)
                    .OrderBy(mc => mc.Executive_Name).Select(mc => new
                    {
                        mc.Id,
                        mc.Agency_Id,
                        mc.Executive_Name
                    }).ToList();

                ddlagencyexe.DataSource = agencyexe;
                ddlagencyexe.DataValueField = "Id";
                ddlagencyexe.DataTextField = "Executive_Name";
                ddlagencyexe.DataBind();
            }
            

            ddlagencyexe.Items.Insert(0, new ListItem("Select Agency Executive", ""));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlagency.SelectedIndex = 0;
           // ddlbrand.SelectedIndex = 0;
           txtBrand.Text=string.Empty;
            ddlagencyexe.SelectedIndex = 0;
            gv.DataSource = null;
            gv.DataBind();
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow myRow = (GridViewRow)imageButton.Parent.Parent;
            Int32 ID = Convert.ToInt32(gv.DataKeys[myRow.RowIndex].Value.ToString());
            ViewState["RecordID"] = ID;
            var obj = db.AgencyBrands.Where(x => x.Id == ID).SingleOrDefault();
            //PopulateBrandDropdown();
            if (ddlagency.Items.FindByValue(obj.Agency.ToString()) != null)
            {
                ddlagency.SelectedValue = obj.Agency.ToString();
            }
            if (obj.Brand.ToString() != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "populatefetchbrand",
                $"populatefetchbrand({obj.Brand});", true);
            }
            if (ddlagencyexe.Items.FindByValue(obj.Agency_Executive.ToString()) != null)
            {
                ddlagencyexe.SelectedValue = obj.Agency_Executive.ToString();
            }
            //btnSave.Text = "Update";
        }

        protected void ddlagency_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAgencyExeDropdown();
            BindGrid();
        }

        protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlagencyexe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    int ID = Convert.ToInt32(ViewState["RecordID"]);
                    var obj = db.AgencyBrands.Where(x => x.Id == ID).SingleOrDefault();
                    obj.Id = ID;

                    if (string.IsNullOrEmpty(ddlagency.SelectedValue))
                    {
                        obj.Agency = 0;
                    }
                    else if (int.TryParse(ddlagency.SelectedValue, out int agencyid))
                    {
                        obj.Agency = agencyid;
                    }
                    else
                    {
                        obj.Agency = 0;
                    }

                    string selectedBrandId = hiddenBrandId.Value;

                    if (string.IsNullOrEmpty(selectedBrandId))
                    {
                        obj.Brand = 0;
                    }
                    else if (int.TryParse(selectedBrandId, out int brandid))
                    {
                        obj.Brand = brandid;
                    }
                    else
                    {
                        obj.Brand = 0;
                    }

                    if (string.IsNullOrEmpty(ddlagencyexe.SelectedValue))
                    {
                        obj.Agency_Executive = 0;
                    }
                    else if (int.TryParse(ddlagencyexe.SelectedValue, out int agencyexeid))
                    {
                        obj.Agency_Executive = agencyexeid;
                    }
                    else
                    {
                        obj.Agency_Executive = 0;
                    }

                    //string usergroup = Request.Cookies["UserGroup"]?.Value;
                    //int grp;
                    //if (int.TryParse(usergroup, out grp))
                    //{
                    //    obj.Grp = (byte?)grp;
                    //}
                    //else
                    //{
                    //    throw new Exception("Failed to parse UserGroup from cookie.");
                    //}
                    obj.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                    
                    var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                    obj.Rec_Edited_Date = currentDateTime.Date;
                    obj.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;
                    obj.cExport = false;
                    obj.Grp = 0;

                    db.SaveChanges();
                    BindGrid();
                    scope.Complete();
                    btnCancel_Click(null, null);
                    lblmessage.Text = "Agencey Brand Updated Successfully";
                }
                catch (Exception ex)
                {
                    lblmessage.Text = ExceptionHandler.GetException(ex);
                }
            }
        }
        [System.Web.Services.WebMethod]
        public static List<Brand> SearchBrands(string searchText)
        {
            using (var db = new Model1Container())
            {
                return db.Brands
                    .Where(b => b.Status == "A" && b.Brand_Name.Contains(searchText))
                    .Select(b => new Brand
                    {
                        Id = b.Id,
                        Brand_Name = b.Brand_Name
                    })
                    .ToList();

                //return brands;
            }
        }
        public class Brand
        {
            public int Id { get; set; }
            public string Brand_Name { get; set; }
        }
        [System.Web.Services.WebMethod]
        public static BrandDTO PopulateBrandDetails(int clientId)
        {
            using (var db = new Model1Container())
            {
                var result = (from Brand in db.Brands
                              where Brand.Id == clientId
                              select new BrandDTO
                              {
                                  BrandName = Brand.Brand_Name

                              }).FirstOrDefault();

                return result ?? new BrandDTO { BrandName = "No Client Found" };
            }
        }

        public class BrandDTO
        {
            public string BrandName { get; set; }

        }
    }
}