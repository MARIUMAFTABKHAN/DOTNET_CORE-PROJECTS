<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AMR.Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.canvasjs.com/canvasjs.min.js"></script>

    <style>
                .dashboard-container {
            display: flex;
            flex-direction:column;
            width:90%;
            gap: 20px;
            padding-left:3%;
            align-items: flex-start;
        }
        .filter-panel {
            margin-top:5px;
            display:flex;
            flex-wrap:wrap;
            justify-content:center;
            justify-content: space-between;
            width: 105%;
            height:30%;
            padding: 10px;
            border-radius: 10px;
            background-color:#D3D3D3; /* Light Blue */
            border: 1px solid #D3D3D3;
            box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.2);
        }
        .filter-table {
            width: 100%;
        }
        .form-check input[type=checkbox] {
            margin-right:2px;
        }
        .chart-container {
            display: flex;
            justify-content: center;
            gap: 10px;
            flex-wrap: wrap;
            width: 100%;
        }
        .chart-box {
            width: 500px;
            height: 350px;
            border-radius: 10px;
            border: 1px solid #D3D3D3;
            background-color: #F0F0F0;
            box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.2);
            display: flex;
            align-items: center;
            justify-content: center;
        }
        
        #myDonutChart,#myDonutChart2 ,#myDonutChart3
        {
            width: 100%;
            height: 300px !important; /* Ensure it has height */
            display: block; /* Ensure it's visible */
        }
        .btn-success {
            width: 300%;
            padding: 10px;
            background-color: #D3D3D3;
            border: dotted;
            border-radius: 5px;
            color: black;
            font-weight: bold;
            cursor: pointer;
        }
        .btn-success:hover {
            background-color: #D3D3D3;
        }

        .horizontal-list
        {
            width:stretch;
        }
        .filter-table tr {
        margin-bottom: auto; /* Adds space between rows */
        display: block; /* Ensures spacing applies */
        }
        .filter-table td {
            padding: 5px; /* Adds some padding inside cells */
        }
        .left-panel, .right-panel,.grid {
            box-sizing: border-box;
            padding: 10px;
            background-color: #F0F0F0;
            border-radius: 10px;
            border: 1px solid #D3D3D3;
            box-shadow: 0px 1px 6px rgba(0, 0, 0, 0.1);
        }

        .left-panel {
            flex: 1;
            max-width: 35%;
        }

        .right-panel {
            flex: 2;
            max-width: 75%;
        }
        /*.grid {
            flex: 2;
            max-width: 75%;
        }
*/
        .subcategory-list {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            font-size:10px;
        }
        .bordered-table {
            border-collapse: collapse;
            width: 50%;
        }

        .bordered-table td,
        .bordered-table tr {
            border: 	#696969; /* Light blue border */
            padding: 8px;
        }

        .subcategory-list td {
            border: inset; /* Optional: removes border inside CheckBoxList items */
        }

        .grid{
            width:35%;
            max-height:70%;
        }
        /* Grid background color */
    .data {
        background-color:	#696969; /* Light blue color */
         border-collapse: collapse;
    }
    .data th {
        background-color: 	#D0D0D0;  /* Light blue header color */
        font-weight: bold;          /* Make header text bold */
        padding: 5px;
        text-align: center;
    }
    .data tr:nth-child(odd) {
        background-color: white;
    }

    .data tr:nth-child(even) {
        background-color: 	#D0D0D0; /* Light blue for even rows */
    }

    /* Increase row height */
    .data tr {
        height: 20px; /* Adjust row height as needed */
    }

    /* Optional: Add spacing between rows */
    .data td, .data th {
        padding: 5px;
        text-align:center;
    }
    </style>

    <div class="dashboard-container">
        <div class="filter-panel">

            <div class="left-panel">
                <table class="filter-table">
                    <tr>
                        <td align="left"><strong>Start Date: &nbsp;</strong></td>
                        <td>
                            <input id="txtstartdate" type="date" class="form-control date-input" runat="server" style="width:200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left"><strong>End Date: &nbsp;&nbsp;</strong> </td>
                        <td>
                            <input id="txtenddate" type="date" class="form-control date-input" runat="server" style="width:200px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="left"><strong>Station: &nbsp;</strong></td>
                        <td colspan="4">
                            <asp:CheckBoxList ID="chkcity" runat="server" CssClass="horizontal-list" RepeatDirection="Horizontal" RepeatColumns="4"></asp:CheckBoxList>
                            <asp:CustomValidator ID="CustomValidator1" runat="server"
                                ErrorMessage="Please select at least one Station."
                                ForeColor="Red" Display="Dynamic">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left"><strong>Pub Group: </strong></td>
                        <td>
                            <asp:DropDownList ID="ddlpubgroup" runat="server" CssClass="form-control date-input" style="width:200px;"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPublicationGroup" runat="server"
                                ControlToValidate="ddlpubgroup"
                                ErrorMessage="Please select a publication group."
                                ForeColor="Red"
                                Display="Dynamic"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left"><strong> Category: &nbsp;</strong></td>
                        <td>
                            <asp:DropDownList ID="ddlmaincat" runat="server" CssClass="form-control date-input" AutoPostBack="True" OnSelectedIndexChanged="ddlmaincat_SelectedIndexChanged" style="width:200px;"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="ddlmaincat"
                                ErrorMessage="Please select a Main Category."
                                ForeColor="Red"
                                Display="Dynamic"
                                InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="Button1" runat="server" CssClass="btn-success" Text="SEARCH" OnClick="btnSearch_Click" style="width:350px;"/>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="right-panel">
                <table  class="bordered-table">
                    <tr>
                        <td style="font-weight:bold;"> Sub Category: 
                            <asp:CheckBoxList ID="chkSubCat" runat="server" CssClass="subcategory-list" RepeatDirection="Horizontal" RepeatColumns="10"></asp:CheckBoxList>
                            <asp:CustomValidator ID="cvSubCat" runat="server"
                                ErrorMessage="Please select at least one Sub Category."
                                ForeColor="Red" Display="Dynamic"
                                ClientValidationFunction="validateSubCategories" EnableClientScript="true">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="grid">
                <asp:GridView ID="gvfirst" PageSize="100" runat="server" CssClass="data" AutoGenerateColumns="false" Width="100%" EnableViewState="false" >
                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns></Columns>
                </asp:GridView>
            </div>

        </div>
       
        <div class="chart-container">
            <div class="chart-box">
                <div id="myDonutChart" style="width: 100%; height: 100%;"></div>
            </div>
            <div class="chart-box">
                <div id="myDonutChart2" style="width: 100%; height: 100%;"></div>
            </div>
             <div class="chart-box">
                 <canvas id="myDonutChart3" style="width: 100%; height: 100%;"></canvas>
             </div>
        </div>
    </div>

    <script type="text/javascript">
        window.onload = function () {
            var pubGroupVal = parseInt('<%= litPubGroupValue.Text.Trim() %>');
            console.log("PUB",pubGroupVal);

            var donutChartData = <%= string.IsNullOrWhiteSpace(litChartJson.Text) ? "[]" : litChartJson.Text %>;
            var donutChartData2 = <%= string.IsNullOrWhiteSpace(litChartJson2.Text) ? "[]" : litChartJson2.Text %>;
            var donutChartData3 = <%= string.IsNullOrWhiteSpace(litChartJson3.Text) ? "[]" : litChartJson3.Text %>;

            console.log("Chart Data:", donutChartData);
            console.log("Chart Data2:", donutChartData2);
            console.log("Chart Data3:", donutChartData3);

            if (pubGroupVal == 10000001) {
                var formattedDataPoints = [];
                var total = 0;

                donutChartData.forEach(item => {
                    var dataPoints = [
                        { y: item.DNYA, label: "DNYA", color: "#A98dd6" },
                        { y: item.EXP, label: "EXP", color: "#668ddb" },
                        { y: item.JANG, label: "JANG", color: "#008cd1" },
                        { y: item.NWQ, label: "NWQ", color: "#ff95ae" }

                    ];

                    dataPoints.forEach(dp => total += dp.y);

                    formattedDataPoints.push(...dataPoints);
                });

                formattedDataPoints = formattedDataPoints.map(dp => ({
                    ...dp,
                    indexLabel: `${dp.y}: ${(dp.y / total * 100).toFixed(2)}%`
                }));

                console.log("Formatted Data Points with Percentage:", formattedDataPoints); 
            }
            else if (pubGroupVal == 10000002)
            {
                var formattedDataPoints = [];
                var total = 0;

                donutChartData2.forEach(item => {
                    var dataPoints = [
                        { y: item.DAWN, label: "DAWN", color: "#A98dd6" },
                        { y: item.ET, label: "ET", color: "#668ddb" },
                        { y: item.NEWS, label: "NEWS", color: "#008cd1" },
                        { y: item.NAT, label: "NAT", color: "#ff95ae" }

                    ];

                    dataPoints.forEach(dp => total += dp.y);

                    formattedDataPoints.push(...dataPoints);
                });

                formattedDataPoints = formattedDataPoints.map(dp => ({
                    ...dp,
                    indexLabel: `${dp.y}: ${(dp.y / total * 100).toFixed(2)}%`
                }));

                console.log("Formatted Data Points with Percentage:", formattedDataPoints);
            }
            else if (pubGroupVal == 10000005) {
                var formattedDataPoints = [];
                var total = 0;

                donutChartData3.forEach(item => {
                    var dataPoints = [
                        { y: item.SE, label: "SE", color: "#A98dd6" },
                        { y: item.A_AWZ, label: "A_AWZ", color: "#668ddb" },
                        { y: item.IBRAT, label: "IBRAT", color: "#008cd1" },
                    ];

                    dataPoints.forEach(dp => total += dp.y);

                    formattedDataPoints.push(...dataPoints);
                });

                formattedDataPoints = formattedDataPoints.map(dp => ({
                    ...dp,
                    indexLabel: `${dp.y}: ${(dp.y / total * 100).toFixed(2)}%`
                }));

                console.log("Formatted Data Points with Percentage:", formattedDataPoints);
            }



            var donutChart = new CanvasJS.Chart("myDonutChart", {
                animationEnabled: true,
                title: {
                    text: "NEWS PAPERS CM COMPARISION",
                    fontSize: 18
                },
                legend: {
                    verticalAlign: "center",
                    horizontalAlign: "left",
                    fontSize: 14,
                    fontColor: "#000"
                },
                data: [{
                    type: "doughnut",
                    showInLegend: true,
                    legendText: "{label}",
                    indexLabel: "{indexLabel}",
                    innerRadius: "70%",
                    dataPoints: formattedDataPoints
                }]
            });
            console.log("Rendering Chart Now...");
            donutChart.render();
            //first donut chart end

            //second graph line graph
            //if (typeof data !== "undefined") {
            //    var dataPoints = data.map(item => ({
            //        label: item.MainCategory,
            //        y: parseInt(item.Total)
            //    }));

            //    var chart = new CanvasJS.Chart("myDonutChart2", {
            //        title: { text: "Main Category vs Total Amount" },
            //        axisX: { title: "Main Category" },
            //        axisY: { title: "Total Amount" },
            //        data: [{ type: "line", dataPoints: dataPoints }]
            //    });

            //    chart.render();


            //}

            //start of second bar graph
            if (typeof barChartData !== "undefined") {
                var chart = new CanvasJS.Chart("myDonutChart2", {
                    animationEnabled: true,
                    //theme: "light2",
                    title: {
                        text: "TOTAL CMs By TIMELINE",
                        fontSize: 18
                    },
                    axisY: {
                        title: "Total CM"
                    },
                    //axisX: {
                    //    title: ""
                    //},
                    toolTip: {
                        shared: true
                    },
                    legend: {
                        verticalAlign: "center",
                        horizontalAlign: "right",
                        dockInsidePlotArea: true,
                        fontSize: 14,
                        fontColor: "#000",
                        cursor: "pointer",
                        itemclick: function (e) {
                            if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                                e.dataSeries.visible = false;
                            } else {
                                e.dataSeries.visible = true;
                            }
                            chart.render();
                        },
                    },
                    data: barChartData


                });

                chart.render();
            }

            //end of second bar graph
        };
    </script>

    <asp:Literal ID="litChartJson" runat="server" Visible="FALSE"></asp:Literal>
    <asp:Literal ID="litChartJson2" runat="server" Visible="FALSE"></asp:Literal>
    <asp:Literal ID="litChartJson3" runat="server" Visible="FALSE"></asp:Literal>
    <asp:Literal ID="litPubGroupValue" runat="server" Visible="false"></asp:Literal>

</asp:Content>
