using AMR.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
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
    public partial class PublicationWiseSummary : BaseClass
    {
        Model1Container db= new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtfromdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                txttodate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                PopulatePubgroupDropdown();
                PopulateCityDropdown();
                PopulateMainCatDropdown();
            }
            else
            {
                if (ViewState["SearchClicked"] != null)
                {
                    btnSearch_Click(sender, e);

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

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSubcatDropdown", "$('.dropdown-checkbox-list').show();", true);

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Clear previous search ViewState
            ViewState["SearchClicked"] = true;
            ViewState["SearchResults"] = null;  // Clear any previous data

            int groupId = Convert.ToInt32(ddlpubgroup.SelectedValue);
            DateTime startDate = Convert.ToDateTime(txtfromdate.Value);
            DateTime endDate = Convert.ToDateTime(txttodate.Value);
            //int cityId = Convert.ToInt32(ddlcity.SelectedValue);

            int? cityId = string.IsNullOrEmpty(ddlcity.SelectedValue)
              ? (int?)null
              : Convert.ToInt32(ddlcity.SelectedValue);



            string selectedClientId = hiddenClientId.Value;

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

            bool supp = chsup.Checked;

            using (var db = new Model1Container()) 
            {
                // Step: Construct the dynamic SQL query as a string
                string dynamicPivotQuery = @"
     
                    -- Set your parameters here
                    DECLARE @GroupId INT = @pGroupId;
                    DECLARE @StartDate DATE = @pStartDate;
                    DECLARE @EndDate DATE = @pEndDate;
                    DECLARE @CityId INT=@pCityId;
                    DECLARE @ClientId INT=@pClientId;
                    DECLARE @MainCatIds  VARCHAR(MAX) = @pmaincat;
	                DECLARE @SubCatIds VARCHAR(MAX) = @psubcat;
                    DECLARE @Supp BIT=@psupp;

                    DECLARE @DynamicPivotQuery AS NVARCHAR(MAX);
                    DECLARE @ColumnNames AS NVARCHAR(MAX);
                    DECLARE @IsNullColumns AS NVARCHAR(MAX);
                    DECLARE @SumColumns AS NVARCHAR(MAX);

                    -- Step 1: Dynamically get all unique Pub_Abreviation values from Publications table based on Group_Id using FOR XML PATH
                    SELECT @ColumnNames = STUFF((SELECT DISTINCT ',' + QUOTENAME(Pub.Pub_Abreviation)
                                                 FROM PublicationGroupDetails pgd
                                                 INNER JOIN Publications Pub ON pgd.Publication = Pub.Id
                                                 WHERE pgd.Group_Id = @GroupId  AND Pub.Status <> 'I' -- Filter based on Group_Id
                                                 FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '');

                    -- Step 2: Prepare dynamic column names with ISNULL for the SELECT statement
                    SELECT @IsNullColumns = STUFF((SELECT DISTINCT ', ISNULL(' + QUOTENAME(Pub.Pub_Abreviation) + ', 0) AS ' + QUOTENAME(Pub.Pub_Abreviation)
                                                   FROM PublicationGroupDetails pgd
                                                   INNER JOIN Publications Pub ON pgd.Publication = Pub.Id
                                                   WHERE pgd.Group_Id = @GroupId AND Pub.Status <> 'I' -- Filter based on Group_Id
                                                   FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '');

                    -- Step 3: Prepare dynamic column names for SUM in the SELECT statement
                    SELECT @SumColumns = STUFF((SELECT DISTINCT ' + ISNULL(' + QUOTENAME(Pub.Pub_Abreviation) + ', 0) '
                                                 FROM PublicationGroupDetails pgd
                                                 INNER JOIN Publications Pub ON pgd.Publication = Pub.Id
                                                 WHERE pgd.Group_Id = @GroupId AND Pub.Status <> 'I' -- Filter based on Group_Id
                                                 FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 3, '');

                    -- Step 4: Construct the dynamic PIVOT SQL query using the view
                    SET @DynamicPivotQuery = N'
                    SELECT * FROM
                    (
                        SELECT 
                        ClientCompanyId AS Id, 
                        Client_Name AS Client,
                        GroupComp_Id AS City_Id,
                        GroupComp_Name AS City,
                        Main_Category,MainCategory,
		                Sub_Category,SubCategory,
                        ' + @IsNullColumns + ',
                        SUM(' + @SumColumns + ') AS GrandTotal
                    FROM 
                    (
                        SELECT 
                            v.ClientCompanyId,
                            v.Client_Name,
                            v.GroupComp_Id,
                            v.GroupComp_Name,
                            v.Main_Category,v.MainCategory,
                            v.Sub_Category,v.SubCategory,
                            v.Pub_Abreviation,
                            SUM(v.Total) AS Total
                        FROM 
                            vw_TransactionRawData v
                            INNER JOIN SplitString_string(@MainCatIds, '','') mc ON v.Main_Category = mc.ID
                            INNER JOIN SplitString_string(@SubCatIds, '','') sc ON v.Sub_Category = sc.ID
                        WHERE 
                            v.Publication_Date BETWEEN @StartDate AND @EndDate 
                            AND (@CityId IS NULL OR v.GroupComp_Id = @CityId)
                            AND (@ClientId IS NULL OR V.ClientCompanyId = @ClientId)
                            AND v.cExport = @Supp
                        GROUP BY  
                            v.ClientCompanyId,
                            v.Client_Name,
                            v.GroupComp_Id,
                            v.GroupComp_Name,
                            v.Main_Category,v.MainCategory,
                            v.Sub_Category,v.SubCategory,
                            v.Pub_Abreviation,
                            v.Type_Id,
                            v.Brand_Id
                    ) AS SourceTable
                    PIVOT 
                    (
                        SUM(Total)
                        FOR Pub_Abreviation IN (' + @ColumnNames + ')
                    ) AS PivotTable
                    GROUP BY ClientCompanyId, Client_Name,GroupComp_Id, GroupComp_Name,Main_Category,Sub_Category,MainCategory,SubCategory, ' + @ColumnNames + ' -- Dynamic GROUP BY Columns
                    ) AS FinalResult     
                    where GrandTotal >0
                    ORDER BY GrandTotal DESC;';

                    -- Step 5: Execute the dynamically generated SQL query
                    EXEC sp_executesql @DynamicPivotQuery, N'@StartDate DATE, @EndDate DATE,@CityId INT,@ClientId INT,@MainCatIds VARCHAR(MAX), @SubCatIds VARCHAR(MAX),@Supp BIT', @StartDate, @EndDate,@CityId,@ClientId, @MainCatIds, @SubCatIds,@Supp;
                ";

                var connection = db.Database.Connection;
                DataTable dt = new DataTable();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = dynamicPivotQuery;
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 300; // 🔥 FIX

                    command.Parameters.Add(new SqlParameter("@pGroupId", groupId));
                    command.Parameters.Add(new SqlParameter("@pStartDate", startDate));
                    command.Parameters.Add(new SqlParameter("@pEndDate", endDate));
                    command.Parameters.Add(new SqlParameter("@pCityId", (object)cityId ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@pClientId", string.IsNullOrEmpty(selectedClientId) ? DBNull.Value : (object)Convert.ToInt32(selectedClientId)));
                    command.Parameters.Add(new SqlParameter("@pmaincat", selectedmaincats));
                    command.Parameters.Add(new SqlParameter("@psubcat", selectedsubcats));
                    command.Parameters.Add(new SqlParameter("@psupp", supp));

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
                        if (column.ColumnName == "Client")
                        {
                            TemplateField templateField = new TemplateField();
                            templateField.HeaderText = column.ColumnName;

                            templateField.ItemTemplate = new GridViewTemplate(ListItemType.Item, column.ColumnName);
                            gvfirst.Columns.Add(templateField);
                        }
                        else
                        {
                            BoundField boundField = new BoundField();
                            boundField.DataField = column.ColumnName;
                            boundField.HeaderText = column.ColumnName;
                            gvfirst.Columns.Add(boundField);
                        }
                    }
                    string[] hideCols = { "Id", "City_Id", "Main_Category", "Sub_Category" };

                    foreach (DataControlField column in gvfirst.Columns)
                    {
                        if (hideCols.Contains(column.HeaderText))
                        {
                            column.Visible = false;
                        }
                    }
                    
                    gvfirst.DataSource = dt;
                    gvfirst.DataBind();

                    CalculateFooterTotals(gvfirst, dt);

                    // Store results in ViewState for export if needed
                    ViewState["SearchResults"] = dt;

                    
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


        private void CalculateFooterTotals(GridView gridView, DataTable dt)
        {
            if (gridView.FooterRow != null)
            {
                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    if (gridView.Columns[i] is BoundField boundField)
                    {
                        if (boundField.DataField != "Id" && boundField.DataField != "Client" && boundField.DataField != "City")
                        {
                            decimal total = 0;

                            foreach (DataRow row in dt.Rows)
                            {
                                if (decimal.TryParse(row[boundField.DataField].ToString(), out decimal value))
                                {
                                    total += value;
                                }
                            }

                            gridView.FooterRow.Cells[i].Text = total.ToString("N2");
                        }
                    }
                }
            }
        }


        public class GridViewTemplate : ITemplate
        {
            private ListItemType templateType;
            private string columnName;

            public GridViewTemplate(ListItemType type, string colName)
            {
                templateType = type;
                columnName = colName;

            }

            public void InstantiateIn(Control container)
            {
                if (templateType == ListItemType.Item)
                {
                    LinkButton lnkBtn = new LinkButton();
                    lnkBtn.ID = "lnkClientName";
                    lnkBtn.DataBinding += new EventHandler(lnkBtn_DataBinding);
                    lnkBtn.CommandName = "SelectClient";
                    lnkBtn.Click += new EventHandler(lnkBtn_Click);
                    container.Controls.Add(lnkBtn);

                    //new work
                    // Create a placeholder for the client details panel
                    Panel clientDetailsPanel = new Panel();
                    clientDetailsPanel.ID = "clientDetailsPanel";
                    clientDetailsPanel.Visible = false; // Initially hidden
                    container.Controls.Add(clientDetailsPanel);

                    // Create a GridView inside the Panel for client details
                    GridView gvClientDetails = new GridView();
                    gvClientDetails.ID = "gvClientDetails";
                    gvClientDetails.AutoGenerateColumns = false;
                    gvClientDetails.CssClass = "table table-bordered";
                    clientDetailsPanel.Controls.Add(gvClientDetails);

                    // Export button
                    // Inside InstantiateIn
                    // Export button
                    //Button btnExportDetails = new Button();
                    //btnExportDetails.ID = "btnExportDetails";
                    //btnExportDetails.Text = "Export to Excel";
                    //btnExportDetails.CssClass = "btn btn-success";
                    //btnExportDetails.CommandName = "ExportDetails";
                    //btnExportDetails.CommandArgument = ""; // will be set later

                    //// ✅ add it into the panel so it renders
                    //clientDetailsPanel.Controls.Add(btnExportDetails);

                    //new work end

                }
            }

            private void lnkBtn_DataBinding(object sender, EventArgs e)
            {
                LinkButton lnkBtn = (LinkButton)sender;
                GridViewRow container = (GridViewRow)lnkBtn.NamingContainer;
                lnkBtn.Text = DataBinder.Eval(container.DataItem, columnName).ToString();
                lnkBtn.CommandArgument = DataBinder.Eval(container.DataItem, "Id").ToString();  // Bind Client_Id

                System.Diagnostics.Debug.WriteLine("LinkButton Text: " + lnkBtn.Text);
                System.Diagnostics.Debug.WriteLine("LinkButton CommandArgument (Client ID): " + lnkBtn.CommandArgument);

            }
            private void lnkBtn_Click(object sender, EventArgs e)
            {
                LinkButton lnkBtn = (LinkButton)sender;
                string clientId = lnkBtn.CommandArgument;

                
                //new work start
                // Find the parent row
                GridViewRow row = (GridViewRow)lnkBtn.NamingContainer;

                // Find the clientDetailsPanel and gvClientDetails within the row
                Panel clientDetailsPanel = (Panel)row.FindControl("clientDetailsPanel");
                GridView gvClientDetails = (GridView)row.FindControl("gvClientDetails");
                Button btnExportDetails = (Button)row.FindControl("btnExportDetails"); // ✅ find export button

                if (clientDetailsPanel != null && gvClientDetails != null)
                {
                    // Now you can make the panel visible
                    clientDetailsPanel.Visible = !clientDetailsPanel.Visible; // Toggle visibility of the panel

                    // You can load client details in gvClientDetails here
                    ((PublicationWiseSummary)((GridViewRow)lnkBtn.NamingContainer).NamingContainer.Page).LoadClientDetails(gvClientDetails, clientId);

                    // ✅ assign CommandArgument for export button
                    //if (btnExportDetails != null)
                    //{
                    //    //btnExportDetails.CommandName = "ExportDetails";
                    //    btnExportDetails.CommandArgument = clientId;
                    //}
                }
                //new work end
            }
        }
        
        public void LoadClientDetails(GridView gvClientDetails, string clientId = null)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                clientId = hfClientId.Value;
            }

            if (!int.TryParse(clientId, out int client_Id))
            {
                // Handle gracefully instead of crashing
                System.Diagnostics.Debug.WriteLine($"Invalid clientId: {clientId}");
                return;
            }


           // int client_Id = int.Parse(clientId);
            var clientDetails = db.ClientCompanies.Where(c => c.Id == client_Id).FirstOrDefault();
            int groupId = Convert.ToInt32(ddlpubgroup.SelectedValue);
            DateTime startDate = Convert.ToDateTime(txtfromdate.Value);
            DateTime endDate = Convert.ToDateTime(txttodate.Value);
            bool supp = chsup.Checked;

            if (clientDetails != null)
            {
                using (var db = new Model1Container())
                {
                    string dynamicPivotQuery = @"
                        DECLARE @ClientCompany INT = @pClientCompany;
                        DECLARE @GroupId INT = @pGroupId;
                        DECLARE @StartDate DATE = @pStartDate;
                        DECLARE @EndDate DATE = @pEndDate;
                        DECLARE @Supp BIT = @psupp;

                        DECLARE @DynamicPivotQuery AS NVARCHAR(MAX);
                        DECLARE @ColumnNames AS NVARCHAR(MAX);

                        -- Step 1: Dynamically get all unique Pub_Abreviation values from Publications table based on Group_Id using FOR XML PATH
                        SELECT @ColumnNames = STUFF((SELECT DISTINCT ',' + QUOTENAME(Pub.Pub_Abreviation)
                                                     FROM PublicationGroupDetails pgd
                                                     INNER JOIN Publications Pub ON pgd.Publication = Pub.Id
                                                     WHERE pgd.Group_Id = @GroupId  -- Filter based on Group_Id
                                                     FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '');

                        -- Step 2: Construct the dynamic PIVOT SQL query
                        SET @DynamicPivotQuery = N'SELECT 
                                                       ID,
                                                       Name,
                                                       ' + @ColumnNames + ',
                                                       ISNULL(' + REPLACE(@ColumnNames, ',', ', 0) + ISNULL(') + ', 0) AS GrandTotal
                                                   FROM (
                                                       SELECT 
                                                           ISNULL(v.Type_Id, '''') + ''-'' + ISNULL(v.Brand_Id , '''') AS ID,
                                                           
                                                           ISNULL(v.Type, '''') + '' '' + ISNULL(v.Brand_Name, '''') AS Name,
                                                           v.Pub_Abreviation,
                                                           SUM(v.Total) AS Total
                                                       FROM 
                                                           vw_TransactionRawData v
                                                       inner join PublicationGroupDetails pgd ON pgd.Publication = v.Publication
                                                       WHERE 
                                                           v.Publication_Date BETWEEN @StartDate AND @EndDate
                                                           AND v.ClientCompanyId = @ClientCompany
                                                           AND pgd.Group_Id = @GroupId
                                                           AND v.cExport = @Supp
                                                       GROUP BY 
                                                           v.Pub_Abreviation,
                                                           v.Type_Id,
                                                           v.Type,
                                                           v.Brand_Id,
                                                           v.Brand_Name
                                                   ) AS SourceTable
                                                   PIVOT 
                                                   (
                                                       SUM(Total)
                                                       FOR Pub_Abreviation IN (' + @ColumnNames + ')
                                                   ) AS PivotTable
                                                   ORDER BY Name;';

                        -- Step 3: Execute the dynamically generated SQL query
                        EXEC sp_executesql @DynamicPivotQuery,
                                           N'@StartDate DATE, @EndDate DATE, @ClientCompany INT, @GroupId INT,@Supp BIT',
                                           @StartDate, @EndDate, @ClientCompany, @GroupId,@Supp;
                    ";

                    var connection = db.Database.Connection;
                    DataTable dt = new DataTable();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = dynamicPivotQuery;

                        command.Parameters.Add(new SqlParameter("@pClientCompany", client_Id));
                        command.Parameters.Add(new SqlParameter("@pGroupId", groupId));
                        command.Parameters.Add(new SqlParameter("@pStartDate", startDate));
                        command.Parameters.Add(new SqlParameter("@pEndDate", endDate));
                        command.Parameters.Add(new SqlParameter("@psupp", supp));

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                        connection.Close();
                    }
                    gvClientDetails.Columns.Clear(); // Clear existing columns
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            if(column.ColumnName == "Name")
                            {
                                BoundField boundField = new BoundField();
                                boundField.DataField = column.ColumnName;
                                boundField.HeaderText = column.ColumnName;
                                gvClientDetails.Columns.Add(boundField);
                            }
                            else
                            {
                                TemplateField templateField = new TemplateField();
                                templateField.HeaderText = column.ColumnName;

                                // templateField.ItemTemplate = new GridViewTemplateDetails(ListItemType.Item, column.ColumnName);
                                // gvClientDetails.Columns.Add(templateField);

                                templateField.ItemTemplate = new GridViewTemplateDetails(ListItemType.Item, column.ColumnName, client_Id, groupId, startDate, endDate, supp);
                                gvClientDetails.Columns.Add(templateField);
                            }
                            foreach (DataControlField column1 in gvClientDetails.Columns)
                            {
                                if (column1.HeaderText == "ID")
                                {
                                    column1.Visible = false;
                                    break;
                                }
                            }
                            //BoundField boundField = new BoundField();
                            //boundField.DataField = column.ColumnName;
                            //boundField.HeaderText = column.ColumnName;
                            //gvClientDetails.Columns.Add(boundField);
                        }

                        gvClientDetails.DataSource = dt;
                        gvClientDetails.DataBind();
                        // in LoadClientDetails
                       // Session["ClientDetails_" + clientId] = dt;


                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#clientDetailsModal').modal('show');", true);
                    }
                }
            }
        }

        public class GridViewTemplateDetails : ITemplate
        {
            private ListItemType templateType;
            private string columnName;
            private int client_Id;
            private int groupId;
            private DateTime startDate;
            private DateTime endDate;
            private bool chsupp;

            public GridViewTemplateDetails(ListItemType type, string colName, int clientId, int group, DateTime start, DateTime end,bool supp)
            {
                templateType = type;
                columnName = colName;
                client_Id = clientId;
                groupId = group;
                startDate = start;
                endDate = end;
                chsupp = supp;
;            }

            public void InstantiateIn(Control container)
            {
                if (templateType == ListItemType.Item)
                {
                    Literal ltrAnchor = new Literal();
                    ltrAnchor.DataBinding += new EventHandler(ltrAnchor_DataBinding);
                    container.Controls.Add(ltrAnchor);
                }
            }
            private void ltrAnchor_DataBinding(object sender, EventArgs e)
            {
                Literal ltrAnchor = (Literal)sender;
                GridViewRow container = (GridViewRow)ltrAnchor.NamingContainer;

                string id = DataBinder.Eval(container.DataItem, "ID").ToString();
                string rowHeaderText = DataBinder.Eval(container.DataItem, columnName).ToString();
                string formattedStartDate = startDate.ToString("yyyy-MM-dd");
                string formattedEndDate = endDate.ToString("yyyy-MM-dd");
                bool supp = chsupp;

                ltrAnchor.Text = $"<a href='PublicationWiseSummaryDetail.aspx?id={id}&columnName={columnName}&clientId={client_Id}" +
                    $"&groupId={groupId}&startDate={formattedStartDate}&endDate={formattedEndDate}&chsupp={supp}&Mode=Edit' target='_blank'>{rowHeaderText}</a>";
            }

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlpubgroup.SelectedIndex = 0;
            txtfromdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txttodate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            gvfirst.DataSource = null;
            gvfirst.DataBind();
            lblMsg.Text = "";
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            DataTable dt = ViewState["SearchResults"] as DataTable;
            
            if (dt != null && dt.Rows.Count > 0)
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    // Add the DataTable to the worksheet
                    var worksheet = workbook.Worksheets.Add("PublicationWiseSummary");
                    worksheet.Cell(1, 1).InsertTable(dt);

                    // Format the total row (last row)
                    int totalRow = dt.Rows.Count + 1; // Adjust based on header row
                    worksheet.Row(totalRow).Style.Font.Bold = true;

                    // Send the Excel file to the browser
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=PublicationWiseSummary.xlsx");

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



        //protected void gvfirst_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "ExportDetails")
        //    {
        //        string clientId = e.CommandArgument.ToString();

        //        // Find the row/grid
        //        DataTable dt = Session["ClientDetails_" + clientId] as DataTable;

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            string clientName = dt.Columns.Contains("Name")
        //                ? dt.Rows[0]["Name"].ToString().Replace(" ", "_")
        //                : "Client";

        //            ExportToExcel(dt, clientName);
        //        }
        //        else
        //        {
        //            Response.Write("<script>alert('No data available to export');</script>");
        //        }
        //    }
        //}



        //private void ExportToExcel(DataTable dt, string clientName)
        //{
        //    using (XLWorkbook workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("ClientDetails");
        //        worksheet.Cell(1, 1).InsertTable(dt);

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename=ClientDetails_" + clientName + ".xlsx");

        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            workbook.SaveAs(memoryStream);
        //            memoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.Close();
        //            HttpContext.Current.ApplicationInstance.CompleteRequest();
        //        }
        //    }
        //}

        
    }
}