using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Net.Sockets;

namespace AMR
{
    public partial class RatePolicyform : BaseClass
    {
        Model1Container db= new Model1Container();
        Int32 nId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["RatePolicyId"] != null) && (Request.QueryString["Mode"] == "Edit"))
            {
                nId = int.Parse(Request.QueryString["RatePolicyId"].ToString());
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Save";
            }

            if (!Page.IsPostBack)
            {
                //txteffdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                PopulatePubDropdown();
                PopulateMainCatDropdown();
                PopulateCityDropdown();
                PopulatePageNumberDropdown();
                if (nId != 0)
                {
                    LoadValues(nId);
                }
            }
        }
        private void PopulatePageNumberDropdown()
        {
            ddlno.Items.Clear();

            for (int i = 1; i <= 250; i++)
            {
                ddlno.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        private void PopulatePubDropdown()
        {
            var pub = db.Publications.Where(mc => mc.Status == "A")
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
        private void PopulateMainCatDropdown()
        {
            var maincat = db.MainCategories
                .Where(mc => mc.Status == "A")
                .OrderBy(mc => mc.Category_Title).Select(mc => new
                {
                    mc.Id,
                    mc.Category_Title
                }).ToList();

            ddlmaincat.DataSource = maincat;
            ddlmaincat.DataValueField = "Id";
            ddlmaincat.DataTextField = "Category_Title";
            ddlmaincat.DataBind();

            ddlmaincat.Items.Insert(0, new ListItem("Select Main Category", ""));
        }
        private void PopulateSubCatDropdown()
        {
            int selectedmaincatId = Convert.ToInt32(ddlmaincat.SelectedValue);

            var subcat = db.SubCategories
                .Where(mc => mc.Status == "A" && mc.Main_Category == selectedmaincatId)
                .OrderBy(mc => mc.Category_Title).Select(mc => new
                {
                    mc.Id,
                    mc.Main_Category,
                    mc.Category_Title
                }).ToList();

            ddlsubcat.DataSource = subcat;
            ddlsubcat.DataValueField = "Id";
            ddlsubcat.DataTextField = "Category_Title";
            ddlsubcat.DataBind();

            ddlsubcat.Items.Insert(0, new ListItem("Select Sub Category", ""));
        }
        private void PopulateCityDropdown()
        {
            var city = db.GroupComps
                .OrderBy(mc => mc.Abreviation).Select(mc => new
                {
                    mc.GroupComp_Id,
                    mc.Abreviation,
                    mc.GroupComp_Name
                }).ToList();

            ddlcity.DataSource = city;
            ddlcity.DataValueField = "GroupComp_Id";
            ddlcity.DataTextField = "Abreviation";
            ddlcity.DataBind();

            ddlcity.Items.Insert(0, new ListItem("Select City Edition", ""));
        }
        protected void ddlmaincat_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateSubCatDropdown();
        }

        private DataTable EnsureDataTable()
        {
            DataTable dt = ViewState["SelectedData"] as DataTable;
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("GroupComp_Id", typeof(int));
                dt.Columns.Add("Abreviation", typeof(string));
                
                ViewState["SelectedData"] = dt;
            }
            return dt;
        }
        private void BindGrid()
        {
            DataTable dt = EnsureDataTable();
            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = EnsureDataTable();

            if (!string.IsNullOrEmpty(ddlcity.SelectedValue))
            {
                int editIndex1 = Convert.ToInt32(hfEditIndex1.Value);
                
                int selectedcityId = Convert.ToInt32(ddlcity.SelectedValue);
                //string selectedcityId = ddlcity.SelectedValue;
                if (editIndex1 == -1)
                {
                    if (dt.AsEnumerable().Any(row => row.Field<int>("GroupComp_Id") == selectedcityId))
                    {
                        lblmessage.Text = "Record already enters!";
                        
                        ddlcity.SelectedIndex = 0;
                        
                        return;
                    }

                    DataRow dr = dt.NewRow();
                    dr["GroupComp_Id"] = ddlcity.SelectedValue;
                    dr["Abreviation"] = ddlcity.SelectedItem.Text;

                    dt.Rows.Add(dr);
                }
                else
                {
                    DataRow dr = dt.Rows[editIndex1];
                    
                    dr["GroupComp_Id"] = ddlcity.SelectedValue;
                    dr["Abreviation"] = ddlcity.SelectedItem.Text;

                    hfEditIndex1.Value = "-1";
                    
                }

                ViewState["SelectedData"] = dt;
                BindGrid();
                
                ddlcity.SelectedIndex = 0;
                
            }
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)btnDelete.NamingContainer;
            
            string cityId = ((Label)gvRow.FindControl("lblcityId")).Text;

            DataTable dt = ViewState["SelectedData"] as DataTable;
            DataRow[] rows = dt.Select($"GroupComp_Id = '{cityId}'");
            

            if (rows.Length > 0)
            {
                dt.Rows.Remove(rows[0]);
                ViewState["SelectedData"] = dt;
                BindGrid();
            }
        }

        private void SaveGridViewData(int ratecardId)
        {
            var existingRecords = db.RateCardDetails.Where(c => c.Rate_Id == ratecardId).ToList();
            db.RateCardDetails.RemoveRange(existingRecords);

            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lblsubcatId = row.FindControl("lblsubcatId") as Label;
                    Label lblsubcatName = row.FindControl("lblsubcatName") as Label;
                  //  Label lblcityId = row.FindControl("lblcityId") as Label;
                    Label lblcityName = row.FindControl("lblcityName") as Label;
                    Label lblfull = row.FindControl("lblfull") as Label;
                    Label lblhalf = row.FindControl("lblhalf") as Label;
                    Label lblday = row.FindControl("lblday") as Label;
                    Label lbldayfull = row.FindControl("lbldayfull") as Label;
                    Label lbldayhalf = row.FindControl("lbldayhalf") as Label;

                    int subcatId = Convert.ToInt32(lblsubcatId.Text);
                    string subcatName = lblsubcatName.Text;
                   // string cityabb=lblcityId.Text;
                    string cityname = lblcityName.Text;
                    double full = Convert.ToDouble(lblfull.Text);
                    double half = Convert.ToDouble(lblhalf.Text);
                    string day = lblday.Text;
                    // Use TryParse to safely convert lbldayfull and lbldayhalf
                    double dayfull = 0;
                    double dayhalf = 0;

                    //double dayfull = Convert.ToDouble(lbldayfull.Text);
                    //double dayhalf = Convert.ToDouble(lbldayhalf.Text);

                    double.TryParse(lbldayfull.Text, out dayfull); // Safely parse, defaulting to 0 if conversion fails
                    double.TryParse(lbldayhalf.Text, out dayhalf); // Safely parse, defaulting to 0 if conversion fails

                    var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();


                    RateCardDetail detail = new RateCardDetail
                    {
                        Rate_Id = ratecardId,
                        Sub_Category = subcatId,
                        City_edition = cityname,
                        Full_Rate = (float)full,
                        Half_Rate = (float)half,
                        WeekDay = day,
                        Full_Rate_day = (float)dayfull,
                        Half_Rate_day = (float)dayhalf
                    };

                    db.RateCardDetails.Add(detail);
                }
            }

            db.SaveChanges();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //if (btnSave.Text == "Save")
            //{
            //    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            //    {
            //        try
            //        {
            //            RateCard obj = new RateCard();
            //            obj.Id = db.usp_IDctr("RateCard").SingleOrDefault().Value;

            //            obj.Publication = ddlpub.SelectedValue;
            //            obj.Main_Category =Convert.ToInt32(ddlmaincat.SelectedValue);
            //            obj.cExport = chsup.Checked;
            //            string effectivedatestring = txteffdate.Value;
            //            DateTime effDate;
            //            if (DateTime.TryParse(effectivedatestring, out effDate))
            //            {
            //                obj.EffectiveFrom = effDate;
            //            }
            //            else
            //            {
            //                lblmessage.Text = "Invalid date format.";
            //            }

            //            var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
            //            obj.Rec_Add_by = Request.Cookies["UserId"]?.Value;
            //            obj.Rec_add_dateTime = currentDateTime.Date + currentDateTime.TimeOfDay;

            //            obj.Rec_Edit_by = null;
            //            obj.Rec_Edit_dateTime = null;

            //            db.RateCards.Add(obj);

            //            db.SaveChanges();
            //            SaveGridViewData(obj.Id);

            //            scope.Complete();
            //            btnCancel_Click(null, null);
            //            lblmessage.Text = "Rate Card  Created Successfully";
            //        }
            //        catch (Exception ex)
            //        {
            //            lblmessage.Text = $"Error: {ex.Message}\n{ex.StackTrace}";
            //        }
            //    }
            //}
            //else
            //{
            //    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            //    {
            //        try
            //        {
            //            var obj = db.RateCards.Where(x => x.Id == nId).SingleOrDefault();

            //            obj.Id = nId;
            //            obj.Publication = ddlpub.SelectedValue;
            //            obj.Main_Category = Convert.ToInt32(ddlmaincat.SelectedValue);
            //            obj.cExport = chsup.Checked;
            //            string effectivedatestring = txteffdate.Value;
            //            DateTime effDate;
            //            if (DateTime.TryParse(effectivedatestring, out effDate))
            //            {
            //                obj.EffectiveFrom = effDate;
            //            }
            //            else
            //            {
            //                lblmessage.Text = "Invalid date format.";
            //            }

            //            obj.Rec_Edit_by = Request.Cookies["UserId"]?.Value;

            //            var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
            //            obj.Rec_Edit_dateTime = currentDateTime.Date + currentDateTime.TimeOfDay;

            //            db.SaveChanges();
            //            SaveGridViewData(obj.Id);

            //            scope.Complete();
            //            btnCancel_Click(null, null);

            //            lblmessage.Text = "Rate Card  Updated Successfully";
            //        }
            //        catch (Exception ex)
            //        {
            //            lblmessage.Text = ExceptionHandler.GetException(ex);
            //        }
            //    }
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlpub.SelectedIndex = 0;
            ddlmaincat.SelectedIndex = 0;
            chsup.Checked=false;
            ddlsubcat.SelectedIndex = 0;
            ddlcity.SelectedIndex = 0;
            gv.DataSource = null;
            gv.DataBind();
        }

        protected void LoadValues(Int32 ID)
        {
            var obj = db.RatePolicies.Where(x => x.ID == ID).SingleOrDefault();

            ddlpub.SelectedValue = obj.Publication.ToString();
            ddlmaincat.SelectedValue = obj.Main_Category.ToString();
            PopulateSubCatDropdown();
            ddlsubcat.SelectedValue = obj.Sub_category.HasValue ? obj.Sub_category.Value.ToString() : string.Empty;
            txtamt.Text = obj.Rate.ToString();
            ddlno.SelectedValue = obj.Page.ToString();
            chbase.Checked = obj.Base_station == true;
            txtminno.Text=obj.No_Station.ToString();
            chper.Checked = obj.Percentage == true;

            var detailList = (from rps  in db.RatePolicyStations
                   join groupComp in db.GroupComps on rps.City_edition equals groupComp.Abreviation
                   where rps.Policy_id==ID
                   select new
                   {
                       ID=rps.Id,
                       PolicyId=rps.Policy_id,
                       GroupComp_Id = groupComp.GroupComp_Id,
                       Abreviation = rps.City_edition
                   }).ToList();

            
            if (detailList.Count > 0)
            {
                DataTable dtdetail = new DataTable();
                
                dtdetail.Columns.Add("GroupComp_Id", typeof(int));
                dtdetail.Columns.Add("Abreviation", typeof(string));
                
                foreach (var item in detailList)
                {
                    DataRow dr = dtdetail.NewRow();
                    dr["GroupComp_Id"] = item.GroupComp_Id;
                    dr["Abreviation"] = item.Abreviation;
                    
                    dtdetail.Rows.Add(dr);
                }

                ViewState["SelectedData"] = dtdetail;

                gv.DataSource = dtdetail;
                gv.DataBind();
            }
            else
            {
                lblmessage.Text = "No associated record found.";
                gv.DataSource = null;
                gv.DataBind();
            }
        }

    }
}