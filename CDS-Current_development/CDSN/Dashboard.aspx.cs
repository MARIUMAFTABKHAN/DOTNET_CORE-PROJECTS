using AjaxControlToolkit.Bundling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDSN
{
    public partial class Dashboard : System.Web.UI.Page
    {
        CDSEntities db = new CDSEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardGrid();
                LoadChartData();
                LoadStackedBarChartData();
            }

        }
        private void LoadDashboardGrid()
        {
            var data=db.dashboard_grid.OrderByDescending(d=>d.OperatorId)//.Take(10)
                .ToList();

            gvRecords.DataSource = data;
            gvRecords.DataBind();
        }

        protected void gvRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRecords.PageIndex = e.NewPageIndex;
            LoadDashboardGrid();
        }

        private void LoadChartData()
        {
            var chartData = db.barcharts
                           .OrderByDescending(b => b.TotalSubscribers) // Ensure descending order
                           .Take(10) 
                           .Select(b => new
                           {
                               label = b.Name,
                               y = (b.TotalSubscribers ?? 0),
                               legendText=b.Name
                           }).ToList();

            // Serialize data to JSON for JavaScript
            JavaScriptSerializer js = new JavaScriptSerializer();
            litChartJson.Text = js.Serialize(chartData);
        }

        private void LoadStackedBarChartData()
        {
            var stackedChartData = db.bar2
                .Select(b => new
                {
                    ChannelName = b.ChannelName,
                    South = (b.South ?? 0),  // Handling null values
                    Center = (b.Center ?? 0),
                    North = (b.North ?? 0)
                }).ToList();

            var stackedChartJson = new
            {
                labels = stackedChartData.Select(c => c.ChannelName).ToArray(),
                datasets = new[]
                {
                    new {
                        label = "South",
                        backgroundColor = "rgba(255, 99, 132, 0.8)",
                        data = stackedChartData.Select(c => c.South).ToArray()
                    },
                    new {
                        label = "Center",
                        backgroundColor = "rgba(54, 162, 235, 0.8)",
                        data = stackedChartData.Select(c => c.Center).ToArray()
                    },
                    new {
                        label = "North",
                        backgroundColor = "rgba(75, 192, 192, 0.8)",
                        data = stackedChartData.Select(c => c.North).ToArray()
                    }
                }
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            litStackedChartJson.Text = js.Serialize(stackedChartJson);
        }

    }
}