using AMR.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMR
{
    public partial class RevenueReport : BaseClass
    {
        Model1Container db= new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtenddate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                PopulatePubgroupDropdown();
                PopulateMainCatDropdown();
            }
            if (IsPostBack)
            {
                // Repopulate publication textbox
                txtSelectedPublications.Text = string.Join(", ", chkPub.Items.Cast<ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Text));

                txtSelectedSubCategories.Text = string.Join(", ", chksubcat.Items.Cast<ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Text));
            }


            //else
            //{
            //    if (ViewState["SearchClicked"] != null)
            //    {
            //        btnSearch_Click(sender, e);
            //    }
            //}

        }

        private void PopulatePubgroupDropdown()
        {
            List<string> selectedValues = new List<string>();

            if (chkPub.Items.Count > 0)
            {
                selectedValues = chkPub.Items.Cast<ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value)
                    .ToList();
            }

            ViewState["SelectedPublications"] = selectedValues;


            var pubgroup = db.Publications
                .OrderBy(mc => mc.Pub_Abreviation).Select(mc => new
                {
                    mc.Id,
                    mc.Pub_Abreviation
                }).ToList();

            chkPub.DataSource = pubgroup;
            chkPub.DataValueField = "Id";
            chkPub.DataTextField = "Pub_Abreviation";
            chkPub.DataBind();

            if (ViewState["SelectedPublications"] is List<string> savedSelections)
            {
                foreach (ListItem item in chkPub.Items)
                {
                    if (savedSelections.Contains(item.Value))
                        item.Selected = true;
                }
            }
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

            chkmaincat.DataSource = maincat;
            chkmaincat.DataValueField = "Id";
            chkmaincat.DataTextField = "Category_Title";
            chkmaincat.DataBind();

        }

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["SearchResults"] = true;

            using (var db = new Model1Container()) 
            {
                // Collect multiple selected publication IDs
                string selectedPublications = string.Join(",", chkPub.Items.Cast<ListItem>()
                                                .Where(i => i.Selected)
                                                .Select(i => i.Value));

                if (string.IsNullOrEmpty(selectedPublications))
                {
                    // Handle no selection error
                    return;
                }

                // Collect multiple selected maincats IDs
                string selectedmaincats = string.Join(",", chkmaincat.Items.Cast<ListItem>()
                                                .Where(i => i.Selected)
                                                .Select(i => i.Value));

                if (string.IsNullOrEmpty(selectedmaincats))
                {
                    // Handle no selection error
                    return;
                }

                // Collect multiple selected maincats IDs
                string selectedsubcats = string.Join(",", chksubcat.Items.Cast<ListItem>()
                                                .Where(i => i.Selected)
                                                .Select(i => i.Value));

                if (string.IsNullOrEmpty(selectedsubcats))
                {
                    return;
                }

                DateTime endDate = Convert.ToDateTime(txtenddate.Value);
                DateTime startDate = Convert.ToDateTime(txtstartdate.Value);
                bool supp=chsup.Checked;

                var a = db.Database.SqlQuery<RevenueReportResult>("EXEC Revenue_report_new @StartDate, @EndDate,@Supp, @PublicationList, @MainCategoryList, @SubCategoryList",
                        new SqlParameter("@StartDate", startDate),
                        new SqlParameter("@EndDate", endDate),
                        new SqlParameter("@Supp", supp),
                        new SqlParameter("@PublicationList", selectedPublications),
                        new SqlParameter("@MainCategoryList", selectedmaincats),
                        new SqlParameter("@SubCategoryList", selectedsubcats)).ToList();


                // Apply conditional logic for RateAmount
                foreach (var result in a)
                {
                    var pageValues = result.Page?.Split(',').Select(p => p.Trim()).ToList(); // Split and trim Page values

                    if (pageValues != null)
                    {
                        foreach (var page in pageValues)
                        {
                            // Apply conditions for each page value
                            if (page == "1" && result.Colour_BW == "F")
                            {
                                result.RateAmount += result.RateAmount * 2.50m; // Add 250%
                                break; // No need to check further for this record
                            }
                            else if (page == "1" && result.Colour_BW == "S")
                            {
                                result.RateAmount += result.RateAmount * 1.25m; // Add 125%
                                break;
                            }
                            else if (page == "0" && result.Colour_BW == "F")
                            {
                                result.RateAmount += result.RateAmount * 2.00m; // Add 200%
                                break;
                            }
                            else if (page == "0" && result.Colour_BW == "S")
                            {
                                result.RateAmount += result.RateAmount * 0.75m; // Add 75%
                                break;
                            }
                            // ✅ New Condition: Page is neither 1 nor 0 AND Colour = F
                            else if (page != "1" && page != "0" && result.Colour_BW == "F")
                            {
                                result.RateAmount += result.RateAmount * 1.50m; // +150%
                                break;
                            }
                        }

                    }
                    result.RateAmount *= result.CM;

                }
                gvfirst.DataSource = a;
                gvfirst.DataBind();

                ViewState["SearchResults"] = a;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //ddlpub.SelectedIndex = 0;
            chkPub.SelectedIndex = 0;

            txtenddate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            //ddlmaincat.SelectedIndex = 0;
            chkmaincat.SelectedIndex = 0;

            gvfirst.DataSource = null;
            gvfirst.DataBind();
            lblMsg.Text = "";
        }

        private DataTable ConvertToDataTable(List<RevenueReportResult> list)
        {
            DataTable table = new DataTable();

            if (list != null && list.Count > 0)
            {
                // Create columns
                foreach (var prop in typeof(RevenueReportResult).GetProperties())
                {
                    // Check if the property type is nullable
                    var propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    table.Columns.Add(prop.Name, propertyType);

                   // table.Columns.Add(prop.Name, prop.PropertyType);
                }

                // Add rows
                foreach (var item in list)
                {
                    var row = table.NewRow();
                    foreach (var prop in typeof(RevenueReportResult).GetProperties())
                    {
                        var value = prop.GetValue(item);
                        row[prop.Name] = value ?? DBNull.Value; // Handle null values

                        //row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    table.Rows.Add(row);
                }
            }

            return table;
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            var results = ViewState["SearchResults"] as List<RevenueReportResult>;

           // DataTable dt = ViewState["SearchResults"] as DataTable;

            if (results != null && results.Count > 0)
            {
                DataTable dt = ConvertToDataTable(results);

                using (XLWorkbook workbook = new XLWorkbook())
                {
                    // Add the DataTable to the worksheet
                    var worksheet = workbook.Worksheets.Add("RevenueReport");
                    worksheet.Cell(1, 1).InsertTable(dt);

                    // Format the total row (last row)
                    int totalRow = dt.Rows.Count + 1; // Adjust based on header row
                    worksheet.Row(totalRow).Style.Font.Bold = true;

                    // Send the Excel file to the browser
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=RevenueReport.xlsx");

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        workbook.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.Close();
                        //Response.End();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
            }
            else
            {
                //Response.Write("<script>alert('No data available to export');</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No data available to export');", true);

            }
        }

        protected void gvfirst_DataBound(object sender, EventArgs e)
        {
            int totalCM = 0;
            decimal totalAmount = 0;

            foreach (GridViewRow row in gvfirst.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Find control for CM
                    var litCM = row.FindControl("CMControl") as Literal;
                    if (litCM != null && int.TryParse(litCM.Text, out int cm))
                        totalCM += cm;

                    // Find control for RateAmount
                    var litRate = row.FindControl("RateAmountControl") as Literal;
                    if (litRate != null && decimal.TryParse(litRate.Text, out decimal rate))
                        totalAmount += rate;
                }
            }

            if (gvfirst.FooterRow != null)
            {
                var lblTotalCM = gvfirst.FooterRow.FindControl("lblTotalCM") as Label;
                var lblTotalAmount = gvfirst.FooterRow.FindControl("lblTotalAmount") as Label;

                if (lblTotalCM != null) lblTotalCM.Text = "Total: " + totalCM;
                if (lblTotalAmount != null) lblTotalAmount.Text = "Total: " + totalAmount.ToString("N2");
            }
        }
    }
}