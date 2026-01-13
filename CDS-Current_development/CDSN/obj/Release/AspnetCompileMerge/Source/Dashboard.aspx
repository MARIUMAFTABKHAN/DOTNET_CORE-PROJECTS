<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CDSN.Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.canvasjs.com/canvasjs.min.js"></script>
   
    <style>
    .grid-container {
        /*flex-grow:1;*/
        width: 100%;
        height: 100%;
        /*overflow:auto;*/
    }

    .EU_DataTable {
        width: 100%;
        /*height:100%;*/
        border-collapse: collapse;
        table-layout:auto;
    }

    .EU_DataTable th, .EU_DataTable td {
        padding: 2px;
        border: 1px solid #ccc;
        text-align: left;
        word-wrap: break-word;
        font-size: 10px; /* Reduce font size if needed */
    }
    .col-wide {
        min-width: 200px !important; /* Adjust as needed */
    max-width: 300px !important;
    white-space: nowrap;
    }

    /* Set smaller width for other columns */
    .col-narrow {
        min-width: 80px !important;
    max-width: 120px !important;
    white-space: nowrap;
    }
    html, body {
    height: 100%;
    margin: 0;
    padding: 0;
    }
    .MainContent {
    height: 100%;
    display: flex;
    flex-direction: column;
    }
</style>


    <div style="display: flex; justify-content: center; align-items: center; gap: 10px;">

         <!-- Left Side (Charts in one column) -->
        <div style="width: 40%; display: flex; flex-direction: column; align-items: center; gap: 10px;">
            <!-- Donut Chart -->
            <div style="width: 90%; height: 350px; border: 1px solid #ccc; padding: 10px; background-color: #f9f9f9;">
                <div id="chartContainer" style="width:100%;height:330px;"></div>
            </div>

            <!-- Stacked Bar Chart -->
            <div style="width: 90%; height: 350px; border: 1px solid #ccc; padding: 10px; background-color: #f9f9f9;">
                <canvas id="stackedBarChart"></canvas>
            </div>
        </div>

        <!-- Right Side (Grid) -->
        <div style="width: 50%; height: 700px; border: 1px solid #ccc; padding: 10px; background-color: #f9f9f9;  overflow: hidden;">
            <div class="grid-container">
                <asp:GridView ID="gvRecords" runat="server" AllowPaging="true" 
                    AutoGenerateColumns="False" CssClass="EU_DataTable" Width="100%" 
                    Height="680px" OnPageIndexChanging="gvRecords_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="OperatorName" HeaderText="Operator Name">
                            <ItemStyle CssClass="col-wide" Width="40%" />
                            <HeaderStyle CssClass="col-wide" Width="40%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CityName" HeaderText="City Name">
                            <ItemStyle CssClass="col-narrow" Width="15%" />
                            <HeaderStyle CssClass="col-narrow" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ExpEnt_Position" HeaderText="Express Entertainment">
                            <ItemStyle CssClass="col-narrow" Width="15%" />
                            <HeaderStyle CssClass="col-narrow" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ExpNews_Position" HeaderText="Express News">
                            <ItemStyle CssClass="col-narrow" Width="15%" />
                            <HeaderStyle CssClass="col-narrow" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WEF" HeaderText="WEF"  DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" >
                            <ItemStyle CssClass="col-narrow" Width="15%" />
                            <HeaderStyle CssClass="col-narrow" Width="15%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>

    <script type="text/javascript">
        window.onload = function () {

            //donutChart
            var donutChartData = <%= litChartJson.Text %>;

            console.log("Chart Data:", donutChartData); // Debugging step

            if (!donutChartData || donutChartData.length === 0) {
                console.warn("No data found for the chart!");
                return;
            }

            var donutChart = new CanvasJS.Chart("chartContainer", {
                animationEnabled: true,
                title: {
                    text: "Top 10 Operators by Subscribers",
                    fontSize: 18
                },
                legend: {
                    verticalAlign: "center",
                    horizontalAlign:"right",
                    fontSize: 12,
                    fontColor: "#000" 
                },
                data: [{
                    type: "doughnut",
                    showInLegend: true,
                    legendText: "{legendText}", // ✅ Use legendText from dataPoints
                    indexLabel: "{y}",
                    innerRadius: "70%",
                    dataPoints: donutChartData
                }]
            });

            donutChart.render();
        
            // New Stacked Bar Chart
            var stackedChartData = <%= litStackedChartJson.Text %>;

            if (!stackedChartData || stackedChartData.length === 0) {
                console.warn("No data found for the stacked bar chart!");
            } else {
                var ctx = document.getElementById("stackedBarChart").getContext("2d");
                new Chart(ctx, {
                    type: "bar",
                    data: stackedChartData,
                    options: {
                        responsive: true,
                        plugins: {
                            title: {
                                display: true,
                                text: "Avg. Channel No according to Region wise",
                                font: {
                                    size: 18,
                                    weight: "bold",
                                },
                                 color: "#000000"
                            },
                            legend: {
                                labels: {
                                    color: "#000000" 
                                }
                            }
                        },
                        scales: {
                            x: { stacked: true, ticks: { color: "#000000" } },
                            y: { stacked: true, ticks: { color: "#000000" } }
                        }
                    }
                });
            }
        };
    </script>

  <asp:Literal ID="litChartJson" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="litStackedChartJson" runat="server" Visible="false"></asp:Literal>

</asp:Content>
