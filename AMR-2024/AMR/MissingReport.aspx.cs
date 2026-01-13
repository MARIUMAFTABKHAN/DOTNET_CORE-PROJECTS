using AMR.Data;
using ClosedXML.Excel;
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
    public partial class MissingReport : BaseClass
    {
        Model1Container db= new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtenddate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                
                PopulatePubgroupDropdown();
                PopulateMainCatDropdown();
                PopulateCityDropdown();
            }
            else
            {
                if (ViewState["SearchClicked"] != null)
                {
                    btnSearch_Click(sender, e);
                }
            }
        }
        
        private void PopulatePubgroupDropdown()
        {
            var pubgroup = db.PublicationGroups
                .OrderBy(mc => mc.Priority).Select(mc => new
                {
                    mc.Id,
                    mc.group_name
                }).ToList();

            ddlpubgroup.DataSource = pubgroup;
            ddlpubgroup.DataValueField = "Id";
            ddlpubgroup.DataTextField = "group_name";
            ddlpubgroup.DataBind();

            ddlpubgroup.Items.Insert(0, new ListItem("Select Publication Group", ""));
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

        //    ddlsubcats.DataSource = subcat;
        //    ddlsubcats.DataValueField = "Id";
        //    ddlsubcats.DataTextField = "Category_Title";
        //    ddlsubcats.DataBind();

        //    ddlsubcats.Items.Insert(0, new ListItem("Select Sub Category", ""));
        //}
        private void PopulateSubCatCheckboxList()
        {
            int selectedMainCatId = Convert.ToInt32(ddlmaincat.SelectedValue);

            var subcat = db.SubCategories
                .Where(mc => mc.Status == "A" && mc.Main_Category == selectedMainCatId)
                .OrderBy(mc => mc.Category_Title)
                .Select(mc => new
                {
                    mc.Id,
                    mc.Category_Title
                }).ToList();

            chkSubCat.DataSource = subcat;
            chkSubCat.DataValueField = "Id";
            chkSubCat.DataTextField = "Category_Title";
            chkSubCat.DataBind();
        }
        private string GetSelectedSubCategories()
        {
            var selectedItems = chkSubCat.Items.Cast<ListItem>()
                .Where(item => item.Selected)
                .Select(item => item.Value)
                .ToArray();

            return string.Join(",", selectedItems); // Comma-separated list
        }
        private void PopulateCityDropdown()
        {
            var pubgroup = db.GroupComps
                .OrderBy(mc => mc.GroupComp_Name).Select(mc => new
                {
                    mc.GroupComp_Id,
                    mc.GroupComp_Name
                }).ToList();

            ddlcity.DataSource = pubgroup;
            ddlcity.DataValueField = "GroupComp_Id";
            ddlcity.DataTextField = "GroupComp_Name";
            ddlcity.DataBind();

            ddlcity.Items.Insert(0, new ListItem("Select City", ""));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Clear previous search ViewState
            ViewState["SearchClicked"] = true;
            // ViewState["SearchResults"] = null;  // Clear any previous data

            int maincatId = Convert.ToInt32(ddlmaincat.SelectedValue);
            int groupId = Convert.ToInt32(ddlpubgroup.SelectedValue);
            DateTime endDate = Convert.ToDateTime(txtenddate.Value);
            int no_intervals = int.Parse(txtintervals.Text);

            DateTime startDate = endDate.AddDays(-no_intervals);


            string selectedSubCategories = GetSelectedSubCategories();
            int cityId= Convert.ToInt32(ddlcity.SelectedValue);
            using (var db = new Model1Container()) 
            {
                // Step: Construct the dynamic SQL query as a string
                string dynamicPivotQuery = @"
     
                    -- Set your parameters here
                    DECLARE @GroupId INT = @pGroupId;  -- Replace with your actual GroupId
                    DECLARE @EndDate DATE = @pEndDate;  -- Replace with your actual end date
                    DECLARE @IntervalDays INT = @pintervals;  -- Number of days for the interval
                    DECLARE @MainCategory VARCHAR(100) = @pmaincat;
                    DECLARE @SubCategory VARCHAR(max) = @psubcat;  -- Add the SubCategory parameter (can be NULL if no filter is needed)
                    DECLARE @CityId INT = @pcityId;

                    -- Calculate start date
                    DECLARE @StartDate DATE = DATEADD(DAY, -@IntervalDays, @EndDate);

                    DECLARE @DynamicPivotQuery NVARCHAR(MAX);
                    DECLARE @ColumnNames NVARCHAR(MAX);
                    DECLARE @IsNullColumns NVARCHAR(MAX);
                    DECLARE @SumColumns NVARCHAR(MAX);

                    -- Step 1: Get all unique Pub_Abreviation values as columns
                    SELECT @ColumnNames = STRING_AGG(QUOTENAME(Pub.Pub_Abreviation), ',')
                    FROM PublicationGroupDetails pgd
                    INNER JOIN Publications Pub ON pgd.Publication = Pub.Id
                    WHERE pgd.Group_Id = @GroupId
                      AND Pub.Pub_Abreviation IN ('JANG', 'NWQ', 'EXP', 'DNYA','DAWN', 'ET', 'NEWS','NAT','KWSH','IBRAT','A.AWZ','SE');

                    -- Step 2: ISNULL-wrapped columns for SELECT
                    SELECT @IsNullColumns = STRING_AGG('ISNULL(' + QUOTENAME(Pub.Pub_Abreviation) + ', 0) AS ' + QUOTENAME(Pub.Pub_Abreviation), ', ')
                    FROM PublicationGroupDetails pgd
                    INNER JOIN Publications Pub ON pgd.Publication = Pub.Id
                    WHERE pgd.Group_Id = @GroupId
                      AND Pub.Pub_Abreviation IN ('JANG', 'NWQ', 'EXP', 'DNYA','DAWN', 'ET', 'NEWS','NAT','KWSH','IBRAT','A.AWZ','SE');

                    -- Step 3: Sum logic to exclude zero rows
                    SELECT @SumColumns = STRING_AGG('ISNULL(' + QUOTENAME(Pub.Pub_Abreviation) + ', 0)', ' + ')
                    FROM PublicationGroupDetails pgd
                    INNER JOIN Publications Pub ON pgd.Publication = Pub.Id
                    WHERE pgd.Group_Id = @GroupId
                      AND Pub.Pub_Abreviation IN ('JANG', 'NWQ', 'EXP', 'DNYA','DAWN', 'ET', 'NEWS','NAT','KWSH','IBRAT','A.AWZ','SE');

                    -- Step 4: Build dynamic pivot query
                    SET @DynamicPivotQuery = N'
                    SELECT 
                        Id,
                        Client,
                        Brand,
                        City,
                        ' + @IsNullColumns + ',
                        (' + @SumColumns + ') AS Total
                    FROM (
                        SELECT 
                            v.ClientCompanyId AS Id,
                            v.Client_Name AS Client,
                            v.Brand_Name AS Brand,
                            v.GroupComp_Name AS City,
                            v.Pub_Abreviation,
                            SUM(v.Total) AS Total
                        FROM vw_TransactionRawData_missing v
                        WHERE 
                            v.Publication_Date BETWEEN @StartDate AND @EndDate
                            AND (@SubCategory IS NULL OR v.Sub_Category IN (SELECT Id FROM SplitString(@SubCategory, '','')))
                            AND v.GroupComp_Id = @CityId
                        GROUP BY
                            v.ClientCompanyId,
                            v.Client_Name,
                            v.Brand_Name,
                            v.GroupComp_Name,
                            v.Pub_Abreviation
                    ) AS SourceTable
                    PIVOT (
                        SUM(Total)
                        FOR Pub_Abreviation IN (' + @ColumnNames + ')
                    ) AS PivotTable
                    WHERE (' + @SumColumns + ') >= 60
                    ORDER BY Client, Brand;
                    ';

                    -- Optional: Debug print
                    -- PRINT @DynamicPivotQuery;

                    -- Step 5: Execute
                    EXEC sp_executesql 
                        @DynamicPivotQuery, 
                        N'@StartDate DATE, @EndDate DATE, @SubCategory VARCHAR(MAX), @CityId INT',
                        @StartDate, @EndDate, @SubCategory, @CityId;
                ";

                var connection = db.Database.Connection;
                DataTable dt = new DataTable();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = dynamicPivotQuery;

                    command.Parameters.Add(new SqlParameter("@pGroupId", groupId));
                    command.Parameters.Add(new SqlParameter("@pEndDate", endDate));
                    command.Parameters.Add(new SqlParameter("@pintervals", no_intervals));
                    command.Parameters.Add(new SqlParameter("@pmaincat", maincatId));
                    command.Parameters.Add(new SqlParameter("@psubcat", selectedSubCategories ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@pcityId", cityId));
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    connection.Close();
                }
                gvfirst.Columns.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        BoundField boundField = new BoundField();
                        boundField.DataField = column.ColumnName;
                        boundField.HeaderText = column.ColumnName;
                        if(column.ColumnName=="Client")
                        {
                            boundField.ItemStyle.Width = Unit.Pixel(400);
                            boundField.HeaderStyle.Width = Unit.Pixel(400);
                        }
                        if (column.ColumnName == "Brand")
                        {
                            boundField.ItemStyle.Width = Unit.Pixel(400);
                            boundField.HeaderStyle.Width = Unit.Pixel(400);
                        }
                        //if (column.ColumnName == "EXP")
                        //{
                        //    boundField.ItemStyle.Width = Unit.Pixel(100);
                        //    boundField.HeaderStyle.Width = Unit.Pixel(100);
                        //}
                        //if (column.ColumnName == "JANG")
                        //{
                        //    boundField.ItemStyle.Width = Unit.Pixel(100);
                        //    boundField.HeaderStyle.Width = Unit.Pixel(100);
                        //}
                        //if (column.ColumnName == "NWQ")
                        //{
                        //    boundField.ItemStyle.Width = Unit.Pixel(100);
                        //    boundField.HeaderStyle.Width = Unit.Pixel(100);
                        //}
                        gvfirst.Columns.Add(boundField);
                        
                    }
                    foreach (DataControlField column in gvfirst.Columns)
                    {
                        if (column.HeaderText == "Id")
                        {
                            column.Visible = false;
                            break;
                        }
                    }

                    string startDateText = startDate.ToString("yyyy-MM-dd");
                    string endDateText = endDate.ToString("yyyy-MM-dd");
                    string mainCatName = ddlmaincat.SelectedItem.Text;
                    string cityName = ddlcity.SelectedItem.Text;

                    // Get selected Sub Category names
                    var selectedSubCatNames = chkSubCat.Items.Cast<ListItem>()
                        .Where(li => li.Selected)
                        .Select(li => li.Text);

                    string subCatsText = string.Join(", ", selectedSubCatNames);

                    // Final heading text
                    string reportHeading = $"<strong>Missing Report</strong> from <u>{startDateText}</u> to <u>{endDateText}</u> | " +
                       $"<strong>Main Category:</strong> {mainCatName} | " +
                       $"<strong>Sub Categories:</strong> {subCatsText} | " +
                       $"<strong>City:</strong> {cityName}";

                    lblReportHeading.Text = reportHeading;



                    gvfirst.DataSource = dt;
                    gvfirst.DataBind();

                    ViewState["SearchResults"] = dt;

                    // Sum for numeric columns
                    GridViewRow footerRow = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal);
                    footerRow.Font.Bold = true;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string colName = dt.Columns[i].ColumnName;

                        if (colName == "Id") continue; // ✅ skip hidden 'Id' column

                        TableCell footerCell = new TableCell();

                        if (colName == "Client")
                            footerCell.Text = "Total:";
                        else if (dt.Columns[i].DataType == typeof(int) || dt.Columns[i].DataType == typeof(decimal))
                        {
                            decimal colSum = dt.AsEnumerable().Sum(row =>
                                row.IsNull(i) ? 0 : Convert.ToDecimal(row[i]));
                            footerCell.Text = colSum.ToString("N0");
                        }
                        else
                        {
                            footerCell.Text = "";
                        }


                        footerRow.Cells.Add(footerCell);
                    }

                    gvfirst.FooterRow.Font.Bold = true;
                    gvfirst.Controls[0].Controls.Add(footerRow); // Add the footer to the GridView


                }
                else
                {
                    gvfirst.DataSource = null;
                    gvfirst.DataBind();

                    lblMsg.Text = "No data found for the selected criteria.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                    ViewState["SearchResults"] = null;
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlpubgroup.SelectedIndex = 0;
            txtenddate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtintervals.Text = string.Empty;
            ddlmaincat.SelectedIndex = 0;
            //   ddlsubcats.SelectedIndex = 0;
            foreach (ListItem item in chkSubCat.Items)
            {
                item.Selected = false;
            }
            gvfirst.DataSource = null;
            gvfirst.DataBind();
            lblMsg.Text = "";
        }

        protected void ddlmaincat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PopulateSubCatDropdown();
            PopulateSubCatCheckboxList();
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            DataTable dt = ViewState["SearchResults"] as DataTable;

            if (dt != null && dt.Rows.Count > 0)
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    // Add the DataTable to the worksheet
                    var worksheet = workbook.Worksheets.Add("MissingReport");
                    worksheet.Cell(1, 1).InsertTable(dt);

                    // Format the total row (last row)
                    int totalRow = dt.Rows.Count + 1; // Adjust based on header row
                    worksheet.Row(totalRow).Style.Font.Bold = true;

                    // Send the Excel file to the browser
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=MissingReport.xlsx");

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
                Response.Write("<script>alert('No data available to export');</script>");
            }
        }
    }
}