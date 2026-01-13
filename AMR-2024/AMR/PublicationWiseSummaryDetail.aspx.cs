using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AMR.PublicationWiseSummary;

namespace AMR
{
    public partial class PublicationWiseSummaryDetail : BaseClass
    {
        Model1Container db = new Model1Container();
        Int32 nId = 0;
        string ncolumnName;
        Int32 nclientId = 0;
        Int32 ngroupId = 0;
        DateTime nstartDate;
        DateTime nendDate;
        List<int> idvalue;
        bool chsupp;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize the list to prevent nulls
            idvalue = new List<int>();
            bool isValid = true;
            string errorMsg = string.Empty;

            try
            {
                if (!IsPostBack)
                {
                    string qsId = Request.QueryString["id"];
                    string qsColumnName = Request.QueryString["columnName"];
                    string qsClientId = Request.QueryString["clientId"];
                    string qsGroupId = Request.QueryString["groupId"];
                    string qsStartDate = Request.QueryString["startDate"];
                    string qsEndDate = Request.QueryString["endDate"];
                    string qschsupp = Request.QueryString["chsupp"];
                    string qsMode = Request.QueryString["Mode"];

                    if (!string.IsNullOrEmpty(qsId) && int.TryParse(qsId, out nId) &&
                        !string.IsNullOrEmpty(qsClientId) && int.TryParse(qsClientId, out nclientId) &&
                        !string.IsNullOrEmpty(qsGroupId) && int.TryParse(qsGroupId, out ngroupId) &&
                        !string.IsNullOrEmpty(qsStartDate) && DateTime.TryParse(qsStartDate, out nstartDate) &&
                        !string.IsNullOrEmpty(qsEndDate) && DateTime.TryParse(qsEndDate, out nendDate) &&
                        !string.IsNullOrEmpty(qschsupp) && bool.TryParse(qschsupp, out chsupp) &&
                        !string.IsNullOrEmpty(qsColumnName) &&
                        qsMode == "Edit")
                    {
                        ncolumnName = qsColumnName;

                        if (db == null)
                        {
                            lblmessage.Text = "Database context is null.";
                            return;
                        }

                        if (db.Publications == null)
                        {
                            lblmessage.Text = "db.Publications is null.";
                            return;
                        }

                        idvalue = db.Publications
                                    .Where(p => p.Pub_Abreviation.Contains(ncolumnName) && p.Status != "I")
                                    .Select(p => p.Id)
                                    .ToList();

                        if (idvalue != null && idvalue.Count > 0)
                        {
                            string columnIdString = string.Join(",", idvalue);
                            Load(nId, columnIdString, nclientId, ngroupId, nstartDate, nendDate,chsupp);
                            ShowEntry(nId, columnIdString, nclientId, nstartDate, nendDate,chsupp);
                        }
                        else
                        {
                            lblmessage.Text = "No publication IDs found for: " + ncolumnName;
                        }
                    }
                    else
                    {
                        lblmessage.Text = "Missing or invalid query string parameters.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = "Unhandled error: " + ex.Message;
                System.Diagnostics.Debug.WriteLine("Error in Page_Load: " + ex.ToString());
            }
        }



        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    //if ((Request.QueryString["id"] != null) && (Request.QueryString["columnName"] != null)
        //    //    && (Request.QueryString["clientId"] != null)
        //    //    && (Request.QueryString["groupId"] != null)
        //    //    && (Request.QueryString["startDate"] != null)
        //    //    && (Request.QueryString["endDate"] != null)
        //    //    && (Request.QueryString["Mode"] == "Edit"))
        //    //{
        //    //    nId = int.Parse(Request.QueryString["id"].ToString());
        //    //    ncolumnName = Request.QueryString["columnName"].ToString();
        //    //    nclientId = int.Parse(Request.QueryString["clientId"].ToString());
        //    //    ngroupId = int.Parse(Request.QueryString["groupId"].ToString());

        //    //    // Parse startDate and endDate from the query string
        //    //    nstartDate = DateTime.Parse(Request.QueryString["startDate"].ToString());
        //    //    nendDate = DateTime.Parse(Request.QueryString["endDate"].ToString());

        //    //    idvalue= db.Publications
        //    //        .Where(p => p.Pub_Abreviation.Contains(ncolumnName)).Select(p => p.Id)
        //    //        .ToList();
        //    //}
        //    //if (!Page.IsPostBack)
        //    //{
        //    //    if (nId != 0 && !string.IsNullOrEmpty(ncolumnName) && nclientId != 0 && ngroupId != 0 && nstartDate != null && nendDate != null)
        //    //    {
        //    //        string columnIdString = string.Join(",", idvalue);
        //    //        Load(nId, columnIdString, nclientId, ngroupId, nstartDate, nendDate);
        //    //        ShowEntry(nId, columnIdString, nclientId, ngroupId, nstartDate, nendDate);
        //    //    }
        //    //}

        //    // Always initialize your list to prevent null references
        //    idvalue = new List<int>();

        //    // Safe parsing with logging in case of failure
        //    bool isValid = true;
        //    string errorMsg = string.Empty;

        //    // Extract and validate query string values
        //    string qsId = Request.QueryString["id"];
        //    string qsColumnName = Request.QueryString["columnName"];
        //    string qsClientId = Request.QueryString["clientId"];
        //    string qsGroupId = Request.QueryString["groupId"];
        //    string qsStartDate = Request.QueryString["startDate"];
        //    string qsEndDate = Request.QueryString["endDate"];
        //    string qsMode = Request.QueryString["Mode"];

        //    if (!string.IsNullOrEmpty(qsId) && int.TryParse(qsId, out nId) &&
        //        !string.IsNullOrEmpty(qsClientId) && int.TryParse(qsClientId, out nclientId) &&
        //        !string.IsNullOrEmpty(qsGroupId) && int.TryParse(qsGroupId, out ngroupId) &&
        //        !string.IsNullOrEmpty(qsStartDate) && DateTime.TryParse(qsStartDate, out nstartDate) &&
        //        !string.IsNullOrEmpty(qsEndDate) && DateTime.TryParse(qsEndDate, out nendDate) &&
        //        !string.IsNullOrEmpty(qsColumnName) &&
        //        qsMode == "Edit")
        //    {
        //        ncolumnName = qsColumnName;

        //        // Try fetching publication IDs from the database
        //        try
        //        {
        //            idvalue = db.Publications
        //                .Where(p => p.Pub_Abreviation.Contains(ncolumnName))
        //                .Select(p => p.Id)
        //                .ToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            isValid = false;
        //            errorMsg = "Error fetching publication IDs: " + ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        isValid = false;
        //        errorMsg = "Invalid or missing query parameters.";
        //    }

        //    // Only run Load and ShowEntry if everything is valid and we have publication IDs
        //    if (!Page.IsPostBack)
        //    {
        //        if (isValid && idvalue != null && idvalue.Count > 0)
        //        {
        //            string columnIdString = string.Join(",", idvalue);
        //            Load(nId, columnIdString, nclientId, ngroupId, nstartDate, nendDate);
        //            ShowEntry(nId, columnIdString, nclientId, ngroupId, nstartDate, nendDate);
        //        }
        //        else
        //        {
        //            lblmessage.Text = errorMsg != string.Empty ? errorMsg : "No data to display.";
        //        }
        //    }


        //}

        protected void Load(Int32 Id, string columnId, Int32 clientId, Int32 groupId, DateTime startDate, DateTime endDate,bool supp)
        {
            using (var db = new Model1Container())
            {
                string dynamicPivotQuery = @"
                    DECLARE @StartDate DATE = @pStartDate ;
                    DECLARE @EndDate DATE = @pEndDate;
                    DECLARE @ClientCompanyId INT = @pclientId;
                    DECLARE @PublicationId INT = @ppubId;
                    DECLARE @GroupId INT = @pGroupId;
                    DECLARE @TypeId INT = @pId;
                    DECLARE @Supp BIT = @psupp;

                    DECLARE @columns NVARCHAR(MAX);
                    DECLARE @isnullColumns NVARCHAR(MAX); 
                    DECLARE @sql NVARCHAR(MAX);

                    -- Step 1: Generate the dynamic column names for the pivot
                    SELECT @columns = COALESCE(@columns + ', ', '') + QUOTENAME(Abreviation),
                           @isnullColumns = COALESCE(@isnullColumns + ' + ', '') + 'ISNULL(' + QUOTENAME(Abreviation) + ', 0)'
                    FROM (SELECT DISTINCT Abreviation FROM GroupComps) AS Abreviations;

                    -- Step 2: Construct the dynamic SQL for the pivot table with a total column
                    SET @sql = '
                        SELECT 
                            CAST(Publication_Date AS DATE) AS Publication_Date,
                            Client_Id,
                            Client_Name,
                            Group_Id,
                            group_name,
                            Publication,
                            Pub_Abreviation,
                            Type_Id,
                            Type,
                            Brand,
                            Brand_Name,
                            ' + @columns + ',
                            -- Calculate the total for each row by summing all pivoted columns
                            (' + @isnullColumns + ') AS Total
                        FROM (
                            SELECT 
                                CAST(T.Publication_Date AS DATE) AS Publication_Date,
                                CC.Id AS Client_Id,
                                CC.Client_Name,
                                GC.Abreviation,
                                pgd1.Group_Id,
                                PG.group_name,
                                T.Publication,
                                P.Pub_Abreviation,
                                T.Type_Id,
                                ISNULL(TY.Type, '''') AS Type,  -- Correct alias for Type
                                T.Brand,
                                ISNULL(Brands.Brand_Name, '''') AS Brand_Name,  -- Correct alias for Brand_Name
                                SUM(T.Size_CM * T.Col_Size) AS Total
                            FROM Transactions T
                            INNER JOIN ClientCompanies CC ON T.Client_Company = CC.Id
                            INNER JOIN Publications P ON T.Publication = P.Id
                            LEFT JOIN Types TY ON T.Type_Id = TY.Id  -- LEFT JOIN for Types
                            LEFT JOIN Brands ON T.Brand = Brands.Id  -- LEFT JOIN for Brands
                            INNER JOIN PublicationGroupDetails pgd1 ON P.Id = pgd1.Publication
                            INNER JOIN PublicationGroups PG ON pgd1.Group_Id = PG.Id 
                            INNER JOIN GroupComps GC ON T.City_Edition = GC.Abreviation
                            WHERE T.Publication_Date BETWEEN @StartDate AND @EndDate 
                              AND T.Client_Company = @ClientCompanyId
                                AND P.Status <> ''I''
                              AND T.Publication = @PublicationId
                              AND pgd1.Group_Id = @GroupId
                              AND (T.Type_Id = @TypeId OR T.Brand = @TypeId)  -- Single parameter for both Type and Brand
                            and t.IsDeleted=0
                            and t.cExport=@Supp
                            GROUP BY 
                                T.Publication_Date,
                                CC.Id,
                                CC.Client_Name,
                                GC.Abreviation,
                                pgd1.Group_Id,
                                PG.group_name,
                                T.Publication,
                                P.Pub_Abreviation,
                                T.Type_Id,
                                TY.Type,
                                T.Brand,
                                Brands.Brand_Name
                        ) AS SourceTable
                        PIVOT (
                            SUM(Total)
                            FOR Abreviation IN (' + @columns + ')
                        ) AS PivotTable
                        ORDER BY Publication_Date, Client_Name;';

                    -- Step 3: Execute the dynamic SQL
                    EXEC sp_executesql @sql, 
                        N'@StartDate DATE, @EndDate DATE, @ClientCompanyId INT, @PublicationId INT, @GroupId INT, @TypeId INT,@Supp BIT',
                        @StartDate, @EndDate, @ClientCompanyId, @PublicationId, @GroupId, @TypeId,@Supp;                        
                        ";

                var connection = db.Database.Connection;
                DataTable dt = new DataTable();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = dynamicPivotQuery;

                    command.Parameters.Add(new SqlParameter("@pGroupId", groupId));
                    command.Parameters.Add(new SqlParameter("@pStartDate", startDate));
                    command.Parameters.Add(new SqlParameter("@pEndDate", endDate));
                    command.Parameters.Add(new SqlParameter("@pclientId", clientId));
                    command.Parameters.Add(new SqlParameter("@ppubId", columnId));
                    command.Parameters.Add(new SqlParameter("@pId", Id));
                    command.Parameters.Add(new SqlParameter("@psupp", supp));

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    connection.Close();
                }
                gv.Columns.Clear();
                if (dt.Rows.Count > 0)
                {
                    lbldate.Text = " Date: " + nstartDate.ToString("yyyy-MM-dd") + " TO " + nendDate.ToString("yyyy-MM-dd");

                    //string clientName = dt.Rows[0]["Client_Name"].ToString();
                    //lblClientName.Text = "Client: " + clientName;

                    //string typeName = dt.Rows[0]["Type"].ToString();
                    //string brandName = dt.Rows[0]["Brand_Name"].ToString();

                    //string pubName = dt.Rows[0]["Pub_Abreviation"].ToString();
                    //lblpub.Text = "Publication: " + pubName;

                    //if (!string.IsNullOrEmpty(typeName))
                    //{
                    //    lblTypename.Text =  typeName;
                    //}
                    //else if (!string.IsNullOrEmpty(brandName))
                    //{
                    //    lblTypename.Text =  brandName;
                    //}
                    //gv.DataSource = dt;
                    //gv.DataBind();

                    string clientName = dt.Rows[0]["Client_Name"]?.ToString() ?? "";
                    string typeName = dt.Rows[0]["Type"]?.ToString() ?? "";
                    string brandName = dt.Rows[0]["Brand_Name"]?.ToString() ?? "";
                    string pubName = dt.Rows[0]["Pub_Abreviation"]?.ToString() ?? "";

                    lblClientName.Text = "Client: " + clientName;
                    lblpub.Text = "Publication: " + pubName;
                    lblTypename.Text = !string.IsNullOrEmpty(typeName) ? typeName : brandName;

                    gv.Columns.Clear();
                    gv.DataSource = dt;
                    gv.DataBind();

                }
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                // Hide specific columns by index
                e.Row.Cells[1].Visible = false;  // Hide "Client_Id"
                e.Row.Cells[2].Visible = false;  // Hide "Client_Name"
                e.Row.Cells[3].Visible = false;  // Hide "Pub_Abreviation"
                e.Row.Cells[4].Visible = false;  // Hide "Type_Id"
                e.Row.Cells[5].Visible = false;  // Hide "Type"
                e.Row.Cells[6].Visible = false;  // Hide "Brand"
                e.Row.Cells[7].Visible = false;  // Hide "Brand_Name"
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime pubDate;
                    if (DateTime.TryParse(e.Row.Cells[0].Text, out pubDate))
                    {
                        e.Row.Cells[0].Text = pubDate.ToString("yyyy-MM-dd"); // Or your preferred format
                    }
                }
            }
        }


        protected void ShowEntry(Int32 id, string columnId, Int32 clientId,  DateTime startDate, DateTime endDate,bool chsupp)
        {
            using (var db = new Model1Container())
            {
                try
                {
                    int publicationId = int.Parse(columnId.Split(',')[0]);

                    var results = db.Database.SqlQuery<YourTransactionModel>(@"
                            EXEC Get_Transaction_Publicationwisereport 
                                @BrandOrTypeId, 
                                @PublicationId, 
                                @ClientCompanyId, 
                                @StartDate, 
                                @EndDate,
                                @Supp",

                            new SqlParameter("@BrandOrTypeId", id),
                            new SqlParameter("@PublicationId", publicationId),
                            new SqlParameter("@ClientCompanyId", clientId),
                            new SqlParameter("@StartDate", startDate),
                            new SqlParameter("@EndDate", endDate),
                            new SqlParameter("@Supp", chsupp)
                        ).ToList();


                    if (results != null && results.Count > 0)
                    {
                        gventry.DataSource = results;
                        gventry.DataBind();
                    }
                    else
                    {
                        lblmessage.Text = "No detail entries returned.";
                    }
                }
                catch (Exception ex)
                {
                    lblmessage.Text = "Error in ShowEntry: " + ex.Message;
                }
            }
        }


        public class YourTransactionModel
        {
            public int Id { get; set; }
            public DateTime Publication_Date { get; set; }
            public string City { get; set; } = "";
            public int Publication { get; set; }
            public string Pub { get; set; } = "";
            public int Main_Category { get; set; }
            public string MainCategory { get; set; } = "";
            public int Sub_Category { get; set; } 
            public string SubCategory { get; set; } = "";
            public int Client_Company { get; set; }
            public string Client { get; set; } = "";
            public int CM { get; set; }
            public int COL { get; set; }
            public string RO { get; set; } = "";
            public int? Page { get; set; }
            public int? Brand { get; set; }
            public int? Type_Id { get; set; }
            public DateTime Rec_Added_Date { get; set; }
            public bool cExport { get; set; }
              // If needed for edit/delete actions
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
                        //db.Transactions.Remove(record);
                        // record.Rec_Edited_By = Request.Cookies["UserId"]?.Value;
                        record.Rec_Edited_By = $"{Request.Cookies["UserId"]?.Value} (D)";

                        var currentDateTime = db.Database.SqlQuery<DateTime>("SELECT GETDATE()").Single();
                        record.Rec_Edited_Date = currentDateTime.Date;
                        record.Rec_Edited_Time = currentDateTime.Date + currentDateTime.TimeOfDay;

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                        {
                            foreach (var failure in ex.EntityValidationErrors)
                            {
                                foreach (var error in failure.ValidationErrors)
                                {
                                    System.Diagnostics.Debug.WriteLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
                                    lblmessage.Text += $"<br/>Property: {error.PropertyName}, Error: {error.ErrorMessage}";
                                }
                            }
                        }

                        //  db.SaveChanges();
                        scope.Complete();

                        lblmessage.Text = "Record deleted successfully.";
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
            //btnfilter_Click(sender, e);
        }
    }
}