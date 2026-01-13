using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System.Web.Script.Serialization;
using static AMR.PublicationWiseSummary;

namespace AMR
{
    public partial class Dashboard : BaseClass
    {
        Model1Container db = new Model1Container();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtenddate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                //txtstartdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                txtenddate.Value = "2025-07-14";
                txtstartdate.Value = "2025-07-07";
                PopulateMainCatDropdown();
                PopulatePubgroupDropdown();
                PopulateCityDropdown();
                PopulateSubCatCheckboxList();
                //LoadChartData();

                litPubGroupValue.Text = Server.HtmlEncode(ddlpubgroup.SelectedValue);

                int groupId = string.IsNullOrEmpty(ddlpubgroup.SelectedValue) ? 0 : Convert.ToInt32(ddlpubgroup.SelectedValue);
                if (groupId == 10000001)
                {
                    LoadChartData();
                }
                Expgraph();
                SubCatgraph();
                clientgrid();
            }
        }
     
        private void PopulateCityDropdown()
        {

            var city = db.GroupComps
                .OrderBy(mc => mc.Abreviation).Select(mc => new
                {
                    //mc.GroupComp_Id,
                    mc.Abreviation
                }).ToList();

            chkcity.DataSource = city;
            chkcity.DataValueField = "Abreviation";
            chkcity.DataTextField = "Abreviation";
            chkcity.DataBind();

            System.Web.UI.WebControls.ListItem item = chkcity.Items.FindByValue("KHI");
            if (item != null)
            {
                item.Selected = true; // Make "KHI" selected
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

            //ddlpubgroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Publication Group", ""));
            

            // Set the item with Id = 10000001 as selected on page load
            System.Web.UI.WebControls.ListItem item = ddlpubgroup.Items.FindByValue("10000001");
            if (item != null)
            {
                item.Selected = true; // Make the item with Id 10000001 selected
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

            ddlmaincat.DataSource = maincat;
            ddlmaincat.DataValueField = "Id";
            ddlmaincat.DataTextField = "Category_Title";
            ddlmaincat.DataBind();

            // ddlmaincat.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Main Category", ""));
            System.Web.UI.WebControls.ListItem item = ddlmaincat.Items.FindByValue("1");
            if (item != null)
            {
                item.Selected = true; // Make the item with Id 10000001 selected
            }
        }

        protected void ddlmaincat_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateSubCatCheckboxList();
        }

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

            var selectedIds = new[] { "3", "5", "2", "10000093" };

            foreach (var selectedId in selectedIds)
            {
                System.Web.UI.WebControls.ListItem item = chkSubCat.Items.FindByValue(selectedId);
                if (item != null)
                {
                    item.Selected = true;
                }
            }

            

        }
        private void LoadChartData()
        {
            List<DashboardReport2> data = GetDashboardData();
            string jsonData = ConvertToChartJson(data);

            litChartJson.Text = jsonData;
        }


        private List<DashboardReport2> GetDashboardData()
        {
            using (var db = new Model1Container())
            {
                int groupId = string.IsNullOrEmpty(ddlpubgroup.SelectedValue) ? 0 : Convert.ToInt32(ddlpubgroup.SelectedValue);

                DateTime startDate = string.IsNullOrEmpty(txtstartdate.Value) ? DateTime.Today : Convert.ToDateTime(txtstartdate.Value).Date;
                DateTime endDate = string.IsNullOrEmpty(txtenddate.Value) ? DateTime.Today : Convert.ToDateTime(txtenddate.Value).Date;

                int mainCategory = string.IsNullOrEmpty(ddlmaincat.SelectedValue) ? 0 : Convert.ToInt32(ddlmaincat.SelectedValue);

                string subCategories = string.Join(",", chkSubCat.Items.Cast<System.Web.UI.WebControls.ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value));

                string cityEditions = string.Join(",", chkcity.Items.Cast<System.Web.UI.WebControls.ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value));

                var groupIdParam = new SqlParameter("@GroupId", groupId);
                var startDateParam = new SqlParameter("@StartDate", startDate.Date);
                var endDateParam = new SqlParameter("@EndDate", endDate.Date);
                var mainCategoryParam = new SqlParameter("@Main_Category", mainCategory);
                var subCategoriesParam = new SqlParameter("@Sub_Categories", string.IsNullOrEmpty(subCategories) ? (object)DBNull.Value : subCategories);
                var cityEditionsParam = new SqlParameter("@City_Editions", string.IsNullOrEmpty(cityEditions) ? (object)DBNull.Value : cityEditions);

                var result = db.Database.SqlQuery<DashboardReport2>(
                    "EXEC dashboard_report2 @GroupId, @StartDate, @EndDate, @Main_Category, @Sub_Categories, @City_Editions",
                    groupIdParam, startDateParam, endDateParam, mainCategoryParam, subCategoriesParam, cityEditionsParam
                ).ToList();

                return result;

            }
        }

        private string ConvertToChartJson(List<DashboardReport2> data)
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        private void LoadChartData2()
        {
            List<DashboardReport3> data = GetDashboardData2();
            string jsonData = ConvertToChartJson2(data);

            litChartJson2.Text = jsonData;
        }


        private List<DashboardReport3> GetDashboardData2()
        {
            using (var db = new Model1Container())
            {
                int groupId = string.IsNullOrEmpty(ddlpubgroup.SelectedValue) ? 0 : Convert.ToInt32(ddlpubgroup.SelectedValue);

                DateTime startDate = string.IsNullOrEmpty(txtstartdate.Value) ? DateTime.Today : Convert.ToDateTime(txtstartdate.Value).Date;
                DateTime endDate = string.IsNullOrEmpty(txtenddate.Value) ? DateTime.Today : Convert.ToDateTime(txtenddate.Value).Date;

                int mainCategory = string.IsNullOrEmpty(ddlmaincat.SelectedValue) ? 0 : Convert.ToInt32(ddlmaincat.SelectedValue);

                string subCategories = string.Join(",", chkSubCat.Items.Cast<System.Web.UI.WebControls.ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value));

                string cityEditions = string.Join(",", chkcity.Items.Cast<System.Web.UI.WebControls.ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value));

                var groupIdParam = new SqlParameter("@GroupId", groupId);
                var startDateParam = new SqlParameter("@StartDate", startDate.Date);
                var endDateParam = new SqlParameter("@EndDate", endDate.Date);
                var mainCategoryParam = new SqlParameter("@Main_Category", mainCategory);
                var subCategoriesParam = new SqlParameter("@Sub_Categories", string.IsNullOrEmpty(subCategories) ? (object)DBNull.Value : subCategories);
                var cityEditionsParam = new SqlParameter("@City_Editions", string.IsNullOrEmpty(cityEditions) ? (object)DBNull.Value : cityEditions);

                var result = db.Database.SqlQuery<DashboardReport3>(
                    "EXEC dashboard_report3 @GroupId, @StartDate, @EndDate, @Main_Category, @Sub_Categories, @City_Editions",
                    groupIdParam, startDateParam, endDateParam, mainCategoryParam, subCategoriesParam, cityEditionsParam
                ).ToList();

                return result;

            }
        }

        private string ConvertToChartJson2(List<DashboardReport3> data)
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        private void LoadChartData3()
        {
            List<DashboardReport4> data = GetDashboardData3();
            string jsonData = ConvertToChartJson3(data);

            litChartJson3.Text = jsonData;
        }


        private List<DashboardReport4> GetDashboardData3()
        {
            using (var db = new Model1Container())
            {
                int groupId = string.IsNullOrEmpty(ddlpubgroup.SelectedValue) ? 0 : Convert.ToInt32(ddlpubgroup.SelectedValue);

                DateTime startDate = string.IsNullOrEmpty(txtstartdate.Value) ? DateTime.Today : Convert.ToDateTime(txtstartdate.Value).Date;
                DateTime endDate = string.IsNullOrEmpty(txtenddate.Value) ? DateTime.Today : Convert.ToDateTime(txtenddate.Value).Date;

                int mainCategory = string.IsNullOrEmpty(ddlmaincat.SelectedValue) ? 0 : Convert.ToInt32(ddlmaincat.SelectedValue);

                string subCategories = string.Join(",", chkSubCat.Items.Cast<System.Web.UI.WebControls.ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value));

                string cityEditions = string.Join(",", chkcity.Items.Cast<System.Web.UI.WebControls.ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value));

                var groupIdParam = new SqlParameter("@GroupId", groupId);
                var startDateParam = new SqlParameter("@StartDate", startDate.Date);
                var endDateParam = new SqlParameter("@EndDate", endDate.Date);
                var mainCategoryParam = new SqlParameter("@Main_Category", mainCategory);
                var subCategoriesParam = new SqlParameter("@Sub_Categories", string.IsNullOrEmpty(subCategories) ? (object)DBNull.Value : subCategories);
                var cityEditionsParam = new SqlParameter("@City_Editions", string.IsNullOrEmpty(cityEditions) ? (object)DBNull.Value : cityEditions);

                var result = db.Database.SqlQuery<DashboardReport4>(
                    "EXEC dashboard_report4 @GroupId, @StartDate, @EndDate, @Main_Category, @Sub_Categories, @City_Editions",
                    groupIdParam, startDateParam, endDateParam, mainCategoryParam, subCategoriesParam, cityEditionsParam
                ).ToList();

                return result;

            }
        }

        private string ConvertToChartJson3(List<DashboardReport4> data)
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            litPubGroupValue.Text = Server.HtmlEncode(ddlpubgroup.SelectedValue);

            int groupId = string.IsNullOrEmpty(ddlpubgroup.SelectedValue) ? 0 : Convert.ToInt32(ddlpubgroup.SelectedValue);
            if (groupId == 10000001) {
                LoadChartData();
            }
            else if (groupId == 10000002)
            {
                LoadChartData2();
            }
            else if (groupId == 10000005)
            {
                LoadChartData3();
            }
            Expgraph();
            SubCatgraph();
            clientgrid();
        }

        private void Expgraph()
        {
            DataTable dt = new DataTable();

            using (var db=new Model1Container())
            {
                int groupId = string.IsNullOrEmpty(ddlpubgroup.SelectedValue) ? 0 : Convert.ToInt32(ddlpubgroup.SelectedValue);
                DateTime startDate = string.IsNullOrEmpty(txtstartdate.Value) ? DateTime.Today : Convert.ToDateTime(txtstartdate.Value).Date;
                DateTime endDate = string.IsNullOrEmpty(txtenddate.Value) ? DateTime.Today : Convert.ToDateTime(txtenddate.Value).Date;

                var data = db.Database.SqlQuery<DashboardReport_Express>(
                    "EXEC dashboard_report_Express @GroupId, @StartDate, @EndDate",
                    new SqlParameter("@GroupId", groupId),
                    new SqlParameter("@StartDate", startDate),
                    new SqlParameter("@EndDate", endDate)
                ).ToList();

                //second graph line graph
                //var groupedData = data.GroupBy(d => d.MainCategory)
                //                      .Select(g => new
                //                      {
                //                          MainCategory = g.Key,
                //                          TotalData = g.Select(x => new { x.Total }).ToList()
                //                      }).ToList();



                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //string jsonData = serializer.Serialize(data);


                //Pass data to JavaScript
                //ClientScript.RegisterStartupScript(this.GetType(), "data",
                //    "<script>var data = " + jsonData + ";</script>", false);

                var groupedData = data
                                 .GroupBy(d => d.MainCategory)
                                 .Select(g => new {
                                     type = "column",
                                     name = g.Key,
                                     showInLegend = true,
                                     color = g.Key == "Government" ? "#A98dd6" :
                                             g.Key == "Commercial" ? "#668ddb" :
                                             "#ff95ae",
                                     dataPoints = g.Select(x => new {
                                         label = x.Pub_Abreviation, // e.g., Jan, Feb, Mar
                                         y = x.Total
                                     }).ToList()
                                 }).ToList();

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonData = serializer.Serialize(groupedData);

                ClientScript.RegisterStartupScript(this.GetType(), "data",
                        "<script>var barChartData  = " + jsonData + ";</script>", false);

            }
        }

        private void SubCatgraph()
        {
            DataTable dt = new DataTable();

            using (var db = new Model1Container())
            {
                int groupId = string.IsNullOrEmpty(ddlpubgroup.SelectedValue) ? 0 : Convert.ToInt32(ddlpubgroup.SelectedValue);
                DateTime startDate = string.IsNullOrEmpty(txtstartdate.Value) ? DateTime.Today : Convert.ToDateTime(txtstartdate.Value).Date;
                DateTime endDate = string.IsNullOrEmpty(txtenddate.Value) ? DateTime.Today : Convert.ToDateTime(txtenddate.Value).Date;

                var data = db.Database.SqlQuery<DashboardReport_SubCat>(
                    "EXEC dasboard_subcategory @GroupId, @StartDate, @EndDate",
                    new SqlParameter("@GroupId", groupId),
                    new SqlParameter("@StartDate", startDate),
                    new SqlParameter("@EndDate", endDate)
                ).ToList();

                var labels = data.Select(d => d.SubCategory).ToList();
                var values = data.Select(d => d.GrandTotal).ToList();

                string jsonLabels = Newtonsoft.Json.JsonConvert.SerializeObject(labels);
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(values);

                string script = $@"
            <script>
                var ctx = document.getElementById('myDonutChart3').getContext('2d');
                new Chart(ctx, {{
                    type: 'line',
                    data: {{
                        labels: {jsonLabels},
                        datasets: [{{
                            label: 'CM by SubCategories',
                            data: {jsonData},
                            borderColor: '#ff95ae',
                            backgroundColor: '#F4CCCC',
                            indexLabel: ""{{label}} - {{y}}"",
                            toolTipContent: ""{{label}}: {{y}}"",
                            tension: 0.1,
                            fill: true
                        }}]
                    }},
                    options: {{
                        responsive: true,
                        plugins: {{
                            legend: {{
                                display: true,
                                position: 'top'
                            }}
                        }},
                        scales: {{
                            y: {{
                                beginAtZero: true
                            }}
                        }}
                    }}
                }});
            </script>";

                ClientScript.RegisterStartupScript(this.GetType(), "subCatChartScript", script);

            }
        }

        private void clientgrid()
        {
            int groupId = string.IsNullOrEmpty(ddlpubgroup.SelectedValue) ? 0 : Convert.ToInt32(ddlpubgroup.SelectedValue);
            DateTime startDate = string.IsNullOrEmpty(txtstartdate.Value) ? DateTime.Today : Convert.ToDateTime(txtstartdate.Value).Date;
            DateTime endDate = string.IsNullOrEmpty(txtenddate.Value) ? DateTime.Today : Convert.ToDateTime(txtenddate.Value).Date;

            using (var db= new Model1Container())
            {
                var connection = db.Database.Connection;
                DataTable dt = new DataTable();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "usp_GetTop10ClientPivotSummary";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@GroupId", groupId));
                    command.Parameters.Add(new SqlParameter("@StartDate", startDate));
                    command.Parameters.Add(new SqlParameter("@EndDate", endDate));

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    connection.Close();
                }
                //gvfirst.Columns.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if(column.ColumnName == "Client" || column.ColumnName == "GrandTotal")
                        {
                            BoundField boundField = new BoundField();
                            boundField.DataField = column.ColumnName;
                            boundField.HeaderText = column.ColumnName; // or set a custom header text
                            gvfirst.Columns.Add(boundField);
                        }
                        
                    }
                    //foreach (DataControlField column in gvfirst.Columns)
                    //{
                    //    if (column.HeaderText == "Id")
                    //    {
                    //        column.Visible = false;
                    //        //break;
                    //    }
                    //    if (column.HeaderText == "City")
                    //    {
                    //        column.Visible = false;
                    //        break;
                    //    }
                    //}
                    gvfirst.DataSource = dt;
                    gvfirst.DataBind();

                }
            }
        }

    }
}