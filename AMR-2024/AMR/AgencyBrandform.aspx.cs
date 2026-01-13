using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class AgencyBrandform : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateAgencyDropdown();
                //PopulateBrandDropdown();
                // Initialize DataTable and store in ViewState
                ViewState["AgencyBrands"] = GetDataTable();

            }
        }
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Agency", typeof(int));
            dt.Columns.Add("Agency_Name", typeof(string));
            dt.Columns.Add("Brand", typeof(int));
            dt.Columns.Add("Brand_Name", typeof(string));
            dt.Columns.Add("Agency_Executive", typeof(int));
            dt.Columns.Add("Executive_Name", typeof(string));
            return dt;
        }
        private void BindGrid()
        {
            gv.DataSource = ViewState["AgencyBrands"] as DataTable;
            gv.DataBind();
        }
        //private void PopulateBrandDropdown()
        //{
        //    var brand = db.Brands
        //        .Where(mc => mc.Status == "A" && mc.Brand_Name != null && mc.Brand_Name != "")
        //        .OrderBy(mc => mc.Brand_Name).Take(100).
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

            ddlagencyexe.Items.Insert(0, new ListItem("Select Agency Executive", ""));
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        foreach (GridViewRow row in gv.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                Label lblAgencyId = row.FindControl("lblAgencyId") as Label;
                                Label lblAgencyName = row.FindControl("lblAgencyName") as Label;
                                Label lblBrandId = row.FindControl("lblBrandId") as Label;
                                Label lblBrandName = row.FindControl("lblBrandName") as Label;
                                Label lblAgencyExeId = row.FindControl("lblAgencyExeId") as Label;
                                Label lblAgencyExecutiveName = row.FindControl("lblAgencyExecutiveName") as Label;

                                int brandId = 0, agencyId = 0, agencyexeId = 0;
                                string brandName = string.Empty, agencyName = string.Empty, agencyexeName = string.Empty;

                                if (!string.IsNullOrEmpty(lblBrandId.Text))
                                {
                                    brandId = Convert.ToInt32(lblBrandId.Text);
                                }
                                brandName = lblBrandName.Text;

                                if (!string.IsNullOrEmpty(lblAgencyId.Text))
                                {
                                    agencyId = Convert.ToInt32(lblAgencyId.Text);
                                }
                                agencyName = lblAgencyName.Text;

                                if (!string.IsNullOrEmpty(lblAgencyExeId.Text))
                                {
                                    agencyexeId = Convert.ToInt32(lblAgencyExeId.Text);
                                }
                                agencyexeName = lblAgencyExecutiveName.Text;

                                //int brandId = Convert.ToInt32(lblBrandId.Text);
                                //string brandName = lblBrandName.Text;
                                //int agencyId = Convert.ToInt32(lblAgencyId.Text);
                                //string agencyName = lblBrandName.Text;
                                //int agencyexeId = Convert.ToInt32(lblAgencyExeId.Text);
                                //string agencyexeName = lblAgencyExecutiveName.Text;

                                var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                                //byte? grpValue = null;
                                //if (byte.TryParse(Request.Cookies["UserGroup"]?.Value, out byte parsedValue))
                                //{
                                //    grpValue = parsedValue;
                                //}

                                AgencyBrand detail = new AgencyBrand
                                {
                                    Id = db.usp_IDctr("AgencyBrands").SingleOrDefault().Value,
                                    Agency = agencyId,
                                    Brand = brandId,
                                    Agency_Executive = agencyexeId,
                                    Rec_Added_By = Request.Cookies["UserId"]?.Value,
                                    Rec_Added_Date = currentDateTime.Date,
                                    Rec_Added_Time = currentDateTime.Date + currentDateTime.TimeOfDay,
                                    Rec_Edited_By = null,
                                    Rec_Edited_Date = null,
                                    Rec_Edited_Time = null,
                                    //Grp= grpValue,
                                    Grp = 0,
                                    cExport = false
                                };

                                db.AgencyBrands.Add(detail);
                            }
                        }

                        db.SaveChanges();
                        scope.Complete();
                        ddlagency.SelectedIndex = 0;
                        //ddlbrand.SelectedIndex = 0;
                        txtBrand.Text = string.Empty;
                        ddlagencyexe.SelectedIndex = 0;
                        gv.DataSource = null;
                        gv.DataBind();
                        lblmessage.Text = "Client Executive  Created Successfully";

                    }
                    catch (Exception ex)
                    {
                        lblmessage.Text = $"Error: {ex.Message}\n{ex.StackTrace}";

                    }
                }
            }
        }


        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = ViewState["AgencyBrands"] as DataTable;

            if (dt != null)
            {
                int index = e.RowIndex;
                dt.Rows.RemoveAt(index);
                ViewState["AgencyBrands"] = dt;
                BindGrid();
            }
        }

        protected void ddlagency_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAgencyExeDropdown();
        }

        protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlagencyexe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = ViewState["AgencyBrands"] as DataTable ?? GetDataTable();

            if (dt != null)
            {
                DataRow dr = dt.NewRow();
                dr["Id"] = dt.Rows.Count + 1; // Assuming auto-increment ID for temporary data
                //dr["Agency"] = Convert.ToInt32(ddlagency.SelectedValue);
                //dr["Agency_Name"] = ddlagency.SelectedItem.Text;
                //dr["Brand"] = Convert.ToInt32(ddlbrand.SelectedValue);
                //dr["Brand_Name"] = ddlbrand.SelectedItem.Text;
                //dr["Agency_Executive"] = Convert.ToInt32(ddlagencyexe.SelectedValue);
                //dr["Executive_Name"] = ddlagencyexe.SelectedItem.Text;

                if (!string.IsNullOrEmpty(ddlagency.SelectedValue))
                {
                    dr["Agency"] = Convert.ToInt32(ddlagency.SelectedValue);
                    dr["Agency_Name"] = ddlagency.SelectedItem.Text;
                }
                else
                {
                    dr["Agency"] = DBNull.Value;
                    dr["Agency_Name"] = DBNull.Value;
                }
                //if (!string.IsNullOrEmpty(ddlbrand.SelectedValue))
                //{
                //    dr["Brand"] = Convert.ToInt32(ddlbrand.SelectedValue);
                //    dr["Brand_Name"] = ddlbrand.SelectedItem.Text;
                //}
                //else
                //{
                //    dr["Brand"] = DBNull.Value;
                //    dr["Brand_Name"] = DBNull.Value;
                //}
                if (!string.IsNullOrEmpty(hiddenBrandId.Value))
                {
                    dr["Brand"] = Convert.ToInt32(hiddenBrandId.Value);
                    dr["Brand_Name"] = txtBrand.Text;
                }
                else
                {
                    dr["Brand"] = DBNull.Value;
                    dr["Brand_Name"] = DBNull.Value;
                }

                // Assign Agency Executive dropdown value
                if (!string.IsNullOrEmpty(ddlagencyexe.SelectedValue))
                {
                    dr["Agency_Executive"] = Convert.ToInt32(ddlagencyexe.SelectedValue);
                    dr["Executive_Name"] = ddlagencyexe.SelectedItem.Text;
                }
                else
                {
                    dr["Agency_Executive"] = DBNull.Value;
                    dr["Executive_Name"] = DBNull.Value;
                }

                dt.Rows.Add(dr);
                ViewState["AgencyBrands"] = dt;

                BindGrid(); // Rebind GridView to show the new row
                btnSave.Visible = true;
                ddlagency.SelectedIndex = 0;
                //ddlbrand.SelectedIndex = 0;
                txtBrand.Text=string.Empty;
                ddlagencyexe.SelectedIndex = 0;
            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgencyBrandView.aspx");
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
    }
}