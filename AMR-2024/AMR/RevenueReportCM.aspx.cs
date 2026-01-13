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
    public partial class RevenueReportCM : BaseClass
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
                txtSelectedPublications.Text = string.Join(", ", chkPub.Items.Cast<ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Text));

                txtSelectedSubCategories.Text = string.Join(", ", chksubcat.Items.Cast<ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Text));
            }

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
                string selectedPublications = string.Join(",", chkPub.Items.Cast<ListItem>()
                                                .Where(i => i.Selected)
                                                .Select(i => i.Value));

                if (string.IsNullOrEmpty(selectedPublications))
                {
                    return;
                }

                string selectedmaincats = string.Join(",", chkmaincat.Items.Cast<ListItem>()
                                                .Where(i => i.Selected)
                                                .Select(i => i.Value));

                if (string.IsNullOrEmpty(selectedmaincats))
                {
                    return;
                }

                string selectedsubcats = string.Join(",", chksubcat.Items.Cast<ListItem>()
                                                .Where(i => i.Selected)
                                                .Select(i => i.Value));

                if (string.IsNullOrEmpty(selectedsubcats))
                {
                    return;
                }

                DateTime endDate = Convert.ToDateTime(txtenddate.Value);
                DateTime startDate = Convert.ToDateTime(txtstartdate.Value);
                bool supp = chsup.Checked;

                DataTable dt = new DataTable();
                using (var conn = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    using (var cmd = new SqlCommand("Revenue_report_CM", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@Supp", supp);
                        cmd.Parameters.AddWithValue("@PublicationList", selectedPublications);
                        cmd.Parameters.AddWithValue("@MainCategoryList", selectedmaincats);
                        cmd.Parameters.AddWithValue("@SubCategoryList", selectedsubcats);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                gvfirst.Columns.Clear();

                foreach (DataColumn col in dt.Columns)
                {
                    BoundField field = new BoundField();

                    field.DataField = col.ColumnName;
                    field.HeaderText = col.ColumnName;
                    gvfirst.Columns.Add(field);
                }
                foreach (DataControlField column in gvfirst.Columns)
                {
                    if (column.HeaderText.Equals("MainCategory", StringComparison.OrdinalIgnoreCase) ||
                        column.HeaderText.Equals("Sub_Category", StringComparison.OrdinalIgnoreCase))
                    {
                        column.Visible = false;
                    }

                }
                gvfirst.DataSource = dt;
                gvfirst.DataBind();

                CalculateFooterTotals(gvfirst, dt);

                // Save for Export
                ViewState["SearchResults"] = dt;

            }
        }

        private void CalculateFooterTotals(GridView gridView, DataTable dt)
        {
            if (gridView.FooterRow != null)
            {
                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    if (gridView.Columns[i] is BoundField boundField)
                    {
                        if (boundField.DataField != "MainCategory" && boundField.DataField != "Sub_Category" && boundField.DataField != "MainCategory_Title" && boundField.DataField != "SubCategory_Title")
                        {
                            decimal total = 0;

                            foreach (DataRow row in dt.Rows)
                            {
                                if (decimal.TryParse(row[boundField.DataField].ToString(), out decimal value))
                                {
                                    total += value;
                                }
                            }

                            gridView.FooterRow.Cells[i].Text = total.ToString();
                        }
                    }
                }
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

        protected void btnexport_Click(object sender, EventArgs e)
        {
            var dt = ViewState["SearchResults"] as DataTable;

            // DataTable dt = ViewState["SearchResults"] as DataTable;

            if (dt != null && dt.Rows.Count > 0)
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    // Add the DataTable to the worksheet
                    var worksheet = workbook.Worksheets.Add("MonthlyCMReport");
                    worksheet.Cell(1, 1).InsertTable(dt);
                    worksheet.Columns().AdjustToContents();

                    // Send the Excel file to the browser
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=MonthlyCMReport.xlsx");

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
        //[Serializable]
        //public class RevenueReportDisplay : RevenueReportResult
        //{

        //    public bool IsGroupSummary { get; set; } = false;
        //}

        //protected void gvfirst_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        var item = e.Row.DataItem as RevenueReportDisplay;
        //        if (item != null && item.IsGroupSummary)
        //        {
        //            e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
        //            e.Row.Font.Bold = true;
        //        }
        //    }
        //}
    }
}