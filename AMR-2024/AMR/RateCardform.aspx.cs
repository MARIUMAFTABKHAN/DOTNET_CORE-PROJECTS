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
using System.Data.Entity;

namespace AMR
{
    public partial class RateCardform : BaseClass
    {
        Model1Container db= new Model1Container();
        Int32 nId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["RateCardId"] != null) && (Request.QueryString["Mode"] == "Edit"))
            {
                nId = int.Parse(Request.QueryString["RateCardId"].ToString());
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Save";
            }

            if (!Page.IsPostBack)
            {
                txteffdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                PopulatePubDropdown();
                PopulateMainCatDropdown();
                PopulateCityDropdown();
                if (nId != 0)
                {
                    LoadValues(nId);
                }
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
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Category_Title", typeof(string));
                //dt.Columns.Add("GroupComp_Id", typeof(int));
                dt.Columns.Add("Abreviation", typeof(string));
                dt.Columns.Add("Full", typeof(double));
                dt.Columns.Add("Half", typeof(double));
                dt.Columns.Add("Day", typeof(string));
                dt.Columns.Add("DayFull", typeof(double));
                dt.Columns.Add("DayHalf", typeof(double));

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

            if (!string.IsNullOrEmpty(ddlsubcat.SelectedValue) && !string.IsNullOrEmpty(ddlcity.SelectedValue))
            {
                int editIndex1 = Convert.ToInt32(hfEditIndex1.Value);
                int editIndex2 = Convert.ToInt32(hfEditIndex2.Value);

                int selectedSubcatId = Convert.ToInt32(ddlsubcat.SelectedValue);
                //int selectedcityId = Convert.ToInt32(ddlcity.SelectedValue);
                string selectedcityId = ddlcity.SelectedValue;
                if (editIndex1 == -1 && editIndex2 == -1)
                {
                    //if (dt.AsEnumerable().Any(row => row.Field<int>("Id") == selectedSubcatId) &&
                    //    dt.AsEnumerable().Any(row => row.Field<string>("Abreviation") == selectedcityId))
                    if (dt.AsEnumerable().Any(row =>
                                row.Field<int>("Id") == selectedSubcatId &&
                                row.Field<string>("Abreviation") == ddlcity.SelectedItem.Text))
                    {
                        lblmessage.Text = "This subcategory already contains this city!";
                        ddlsubcat.SelectedIndex = 0;
                        ddlcity.SelectedIndex = 0;
                        txtfull.Text = string.Empty;
                        txthalf.Text = string.Empty;
                        ddlday.SelectedIndex = 0;
                        txtdayfull.Text = string.Empty;
                        txtdayhalf.Text = string.Empty;
                         return;
                    }

                    DataRow dr = dt.NewRow();
                    dr["Id"] = ddlsubcat.SelectedValue;
                    dr["Category_Title"] = ddlsubcat.SelectedItem.Text;
                    //dr["GroupComp_Id"] = ddlcity.SelectedValue;
                    dr["Abreviation"] = ddlcity.SelectedItem.Text;
                   // dr["GroupComp_Name"] = ddlcity.SelectedItem.Text;
                    dr["Full"] = txtfull.Text;
                    dr["Half"] = txthalf.Text;
                    dr["Day"] = ddlday.SelectedValue=="" ? string.Empty : ddlday.SelectedItem.Text;
                    if (string.IsNullOrEmpty(txtdayfull.Text))
                    {
                        dr["DayFull"] = DBNull.Value; 
                    }
                    else
                    {
                        if (double.TryParse(txtdayfull.Text, out double dayFullValue))
                        {
                            dr["DayFull"] = dayFullValue;
                        }
                        else
                        {
                            throw new FormatException("Invalid input in DayFull field. Please enter a valid number.");
                        }
                    }
                    
                    if (string.IsNullOrEmpty(txtdayhalf.Text))
                    {
                        dr["DayHalf"] = DBNull.Value;
                    }
                    else
                    {
                        if (double.TryParse(txtdayhalf.Text, out double dayHalfValue))
                        {
                            dr["DayHalf"] = dayHalfValue;
                        }
                        else
                        {
                            throw new FormatException("Invalid input in DayHalf field. Please enter a valid number.");
                        }
                    }
                    
                    dt.Rows.Add(dr);
                }
                else
                {
                    DataRow dr = dt.Rows[editIndex1];
                    DataRow dr2 = dt.Rows[editIndex2];
                    dr["Id"] = selectedSubcatId;
                    dr["Category_Title"] = ddlsubcat.SelectedItem.Text;
                    //dr2["GroupComp_Id"] = ddlcity.SelectedValue;
                    dr2["Abreviation"] = ddlcity.SelectedItem.Text;

                    dr["Full"] = txtfull.Text;
                    dr["Half"] = txthalf.Text;
                    dr["Day"] = ddlday.SelectedValue == "" ? string.Empty : ddlday.SelectedItem.Text;
                    if (string.IsNullOrEmpty(txtdayfull.Text))
                    {
                        dr["DayFull"] = DBNull.Value;
                    }
                    else
                    {
                        if (double.TryParse(txtdayfull.Text, out double dayFullValue))
                        {
                            dr["DayFull"] = dayFullValue;
                        }
                        else
                        {
                            throw new FormatException("Invalid input in DayFull field. Please enter a valid number.");
                        }
                    }

                    if (string.IsNullOrEmpty(txtdayhalf.Text))
                    {
                        dr["DayHalf"] = DBNull.Value;
                    }
                    else
                    {
                        if (double.TryParse(txtdayhalf.Text, out double dayHalfValue))
                        {
                            dr["DayHalf"] = dayHalfValue;
                        }
                        else
                        {
                            throw new FormatException("Invalid input in DayHalf field. Please enter a valid number.");
                        }
                    }

                    hfEditIndex1.Value = "-1";
                    hfEditIndex2.Value = "-1";
                }

                ViewState["SelectedData"] = dt;
                BindGrid();
                ddlsubcat.SelectedIndex = 0;
                ddlcity.SelectedIndex = 0;
                txtfull.Text = string.Empty;
                txthalf.Text = string.Empty;
                ddlday.SelectedIndex = 0;
                txtdayfull.Text = string.Empty;
                txtdayhalf.Text = string.Empty;
            }
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)btnDelete.NamingContainer;
            string subcatId = ((Label)gvRow.FindControl("lblsubcatId")).Text;
            string cityId = ((Label)gvRow.FindControl("lblcityName")).Text;

            DataTable dt = ViewState["SelectedData"] as DataTable;
            DataRow[] rows = dt.Select($"Id = '{subcatId}'AND Abreviation='{cityId}'");
            
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

            db.SaveChanges();   // <-- IMPORTANT

            // Clear EF tracker to avoid duplicate state conflict
            db.ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Detached);

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
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        RateCard obj = new RateCard();
                        obj.Id = db.usp_IDctr("RateCard").SingleOrDefault().Value;

                        obj.Publication = ddlpub.SelectedValue;
                        obj.Main_Category =Convert.ToInt32(ddlmaincat.SelectedValue);
                        obj.cExport = chsup.Checked;
                        string effectivedatestring = txteffdate.Value;
                        DateTime effDate;
                        if (DateTime.TryParse(effectivedatestring, out effDate))
                        {
                            obj.EffectiveFrom = effDate;
                        }
                        else
                        {
                            lblmessage.Text = "Invalid date format.";
                        }

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Add_by = Request.Cookies["UserId"]?.Value;
                        obj.Rec_add_dateTime = currentDateTime.Date + currentDateTime.TimeOfDay;

                        obj.Rec_Edit_by = null;
                        obj.Rec_Edit_dateTime = null;

                        db.RateCards.Add(obj);

                        db.SaveChanges();
                        SaveGridViewData(obj.Id);

                        scope.Complete();
                        btnCancel_Click(null, null);
                        lblmessage.Text = "Rate Card  Created Successfully";
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
                        var obj = db.RateCards.Where(x => x.Id == nId).SingleOrDefault();

                        obj.Id = nId;
                        obj.Publication = ddlpub.SelectedValue;
                        obj.Main_Category = Convert.ToInt32(ddlmaincat.SelectedValue);
                        obj.cExport = chsup.Checked;
                        string effectivedatestring = txteffdate.Value;
                        DateTime effDate;
                        if (DateTime.TryParse(effectivedatestring, out effDate))
                        {
                            obj.EffectiveFrom = effDate;
                        }
                        else
                        {
                            lblmessage.Text = "Invalid date format.";
                        }

                        obj.Rec_Edit_by = Request.Cookies["UserId"]?.Value;

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        obj.Rec_Edit_dateTime = currentDateTime.Date + currentDateTime.TimeOfDay;

                        db.SaveChanges();
                        SaveGridViewData(obj.Id);

                        scope.Complete();
                        btnCancel_Click(null, null);

                        lblmessage.Text = "Rate Card  Updated Successfully";
                    }
                    catch (Exception ex)
                    {
                        Exception inner = ex;
                        while (inner.InnerException != null)
                            inner = inner.InnerException;

                        lblmessage.Text = inner.Message;
                    }

                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlpub.SelectedIndex = 0;
            ddlmaincat.SelectedIndex = 0;
            txteffdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            chsup.Checked=false;
            ddlsubcat.SelectedIndex = 0;
            ddlcity.SelectedIndex = 0;
            txtfull.Text = string.Empty;
            txthalf.Text = string.Empty;
            txtdayfull.Text = string.Empty;
            txtdayhalf.Text = string.Empty;
            ddlday.SelectedIndex = 0;
            gv.DataSource = null;
            gv.DataBind();
        }

        protected void LoadValues(Int32 ID)
        {
            var obj = db.RateCards.Where(x => x.Id == ID).SingleOrDefault();

            ddlpub.SelectedValue = obj.Publication.Trim();
            ddlmaincat.SelectedValue = obj.Main_Category.HasValue ? obj.Main_Category.Value.ToString() : string.Empty;
            PopulateSubCatDropdown();
            chsup.Checked = obj.cExport==true;
            
            if (obj.EffectiveFrom.HasValue)
            {
                txteffdate.Value = obj.EffectiveFrom.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                hdneffDate.Value = string.Empty;
            }

            var detailList = (from rcd in db.RateCardDetails
                              join subcat in db.SubCategories on rcd.Sub_Category equals subcat.Id
                              join groupComp in db.GroupComps on rcd.City_edition equals groupComp.Abreviation
                              //where rcd.Rate_Id == ID && rcd.Full_Rate != null && rcd.Full_Rate != 0
                              //        && rcd.Half_Rate != null && rcd.Half_Rate != 0
                              where rcd.Rate_Id == ID

                              orderby subcat.Category_Title ascending
                              select new
                              {
                                  RateId = rcd.Rate_Id,
                                  Id = rcd.Sub_Category,
                                  Category_Title = subcat.Category_Title,
                                  Abreviation = rcd.City_edition,
                                  Full = rcd.Full_Rate,
                                  Half = rcd.Half_Rate,
                                  Day = rcd.WeekDay,
                                  DayFull = rcd.Full_Rate_day,
                                  DayHalf = rcd.Half_Rate_day
                              }).ToList();

            if (detailList.Count > 0)
            {
                DataTable dtdetail = new DataTable();
                dtdetail.Columns.Add("Id", typeof(int));
                dtdetail.Columns.Add("Category_Title", typeof(string));
                //dtdetail.Columns.Add("GroupComp_Id", typeof(int));
                dtdetail.Columns.Add("Abreviation", typeof(string));
                dtdetail.Columns.Add("Full", typeof(double));
                dtdetail.Columns.Add("Half", typeof(double));
                dtdetail.Columns.Add("Day", typeof(string));
                dtdetail.Columns.Add("DayFull", typeof(double));
                dtdetail.Columns.Add("DayHalf", typeof(double));

                foreach (var item in detailList)
                {
                    DataRow dr = dtdetail.NewRow();
                    dr["Id"] = item.Id;
                    dr["Category_Title"] = item.Category_Title;
                    dr["Abreviation"] = item.Abreviation;
                    dr["Full"] = item.Full.HasValue ? (object)Math.Round(Convert.ToDouble(item.Full.Value), 2) : DBNull.Value;
                    dr["Half"] = item.Half.HasValue ? (object)Math.Round(Convert.ToDouble(item.Half.Value), 2) : DBNull.Value;
                    dr["Day"] = item.Day;
                    dr["DayFull"] = item.DayFull.HasValue ? (object)Math.Round(Convert.ToDouble(item.DayFull.Value), 2) : DBNull.Value; // Use DBNull.Value for nulls
                    dr["DayHalf"] = item.DayHalf.HasValue ? (object)Math.Round(Convert.ToDouble(item.DayHalf.Value), 2) : DBNull.Value; // Use DBNull.Value for nulls

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