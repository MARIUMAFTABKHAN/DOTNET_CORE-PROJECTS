<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="HeadendViewershipreport.aspx.cs" Inherits="CDSN.HeadendViewershipreport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
    <style type="text/css">
        .style2 {
            text-align: center;
        }

        .row {
            margin-top: 5px !important;
        }

    </style>
    <script type="text/javascript" language="javascript">

        function showPleaseWait() {

            document.getElementById('PleaseWait').style.display = 'block';
            //ShowBars();
        }

        function pageLoad() {
            SetDDL();
            //ShowBars();

        }
        
        function SetDDL() {

            $('#MainContent_ddlTerritory').SumoSelect({
                includeSelectAllOption: true, okCancelInMulti: true, search: true, searchText: 'Select Territory', selectAll: true, includeSelectAllOption: true
            });

            $('#MainContent_ddlDistricts').SumoSelect({
                includeSelectAllOption: true, okCancelInMulti: true, search: true, searchText: 'Select Territory', selectAll: true, includeSelectAllOption: true

            });

            $('#MainContent_ddlCity').SumoSelect({
                okCancelInMulti: true,
                triggerChangeCombined: true,
                forceCustomRendering: true,
                selectAll: true
            });
            $('#MainContent_ddlArea').SumoSelect({
                includeSelectAllOption: true,
                includeSelectAllOption: true, okCancelInMulti: true, search: false, searchText: 'Area', selectAll: true
            });
        }

        function splitRecord(str) {
            var lBox = $('select[id$=lb]');
            $("#lb").empty()
            var listItems = [];
            for (var i = 0; i < str.length; i++) {

                listItems.push('<option value="' +
                    str[i] + '">' + str[i]
                    + '</option>');
            }
            $(lBox).append(listItems.join(''));
        }

        function renderDynamicPieChart(labels, dataValues) {
            const total = dataValues.reduce((a, b) => a + b, 0);

            // Gentle pastel color palette generator (HSL based)
            function generatePastelColors(count) {
                const backgroundColors = [];
                const borderColors = [];

                for (let i = 0; i < count; i++) {
                    const hue = Math.floor((360 / count) * i);
                    const pastel = `hsl(${hue}, 70%, 80%)`;    // Soft background
                    const border = `hsl(${hue}, 70%, 60%)`;    // Slightly darker border
                    backgroundColors.push(pastel);
                    borderColors.push(border);
                }

                return { backgroundColors, borderColors };
            }

            const { backgroundColors, borderColors } = generatePastelColors(labels.length);

            const data = {
                labels: labels,
                datasets: [{
                    label: 'Viewership',
                    data: dataValues,
                    backgroundColor: backgroundColors,
                    borderColor: borderColors,
                    borderWidth: 1
                }]
            };

            const config = {
                type: 'pie',
                data: data,
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'right',
                            labels: {
                                boxWidth: 20,
                                padding: 10,
                                usePointStyle: true,
                                font: {
                                    size: 14
                                }
                            }
                        },
                        title: {
                            display: true,
                            text: 'Territory Viewership in Country',
                            font: {
                                size: 20
                            }
                        },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    const label = context.label || '';
                                    const value = context.raw || 0;
                                    const percent = ((value / total) * 100).toFixed(2);
                                    return `${label}: ${value.toLocaleString()} (${percent}%)`;
                                }
                            }
                        }
                    },
                    layout: {
                        padding: 20
                    }
                }
            };

            const ctx = document.getElementById('myChart').getContext('2d');
            if (window.myChartInstance) {
                window.myChartInstance.destroy();
            }
            window.myChartInstance = new Chart(ctx, config);
        }


        function renderDynamicPieChart2(labels, dataValues) {
            const total = dataValues.reduce((a, b) => a + b, 0);

            // Gentle pastel color palette generator (HSL based)
            function generatePastelColors(count) {
                const backgroundColors = [];
                const borderColors = [];

                for (let i = 0; i < count; i++) {
                    const hue = Math.floor((360 / count) * i);
                    const pastel = `hsl(${hue}, 70%, 80%)`;    // Soft background
                    const border = `hsl(${hue}, 70%, 60%)`;    // Slightly darker border
                    backgroundColors.push(pastel);
                    borderColors.push(border);
                }

                return { backgroundColors, borderColors };
            }

            const { backgroundColors, borderColors } = generatePastelColors(labels.length);

            const data = {
                labels: labels,
                datasets: [{
                    label: 'Viewership',
                    data: dataValues,
                    backgroundColor: backgroundColors,
                    borderColor: borderColors,
                    borderWidth: 1
                }]
            };

            const config = {
                type: 'pie',
                data: data,
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'right',
                            labels: {
                                boxWidth: 20,
                                padding: 10,
                                usePointStyle: true,
                                font: {
                                    size: 14
                                }
                            }
                        },
                        title: {
                            display: true,
                            text: 'Districts Viewership in Territory',
                            font: {
                                size: 20
                            }
                        },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    const label = context.label || '';
                                    const value = context.raw || 0;
                                    const percent = ((value / total) * 100).toFixed(2);
                                    return `${label}: ${value.toLocaleString()} (${percent}%)`;
                                }
                            }
                        }
                    },
                    layout: {
                        padding: 20
                    }
                }
            };

            const ctx = document.getElementById('myChart').getContext('2d');
            if (window.myChartInstance) {
                window.myChartInstance.destroy();
            }
            window.myChartInstance = new Chart(ctx, config);
        }

        function renderDynamicPieChart3(labels, dataValues) {
            const total = dataValues.reduce((a, b) => a + b, 0);

            // Gentle pastel color palette generator (HSL based)
            function generatePastelColors(count) {
                const backgroundColors = [];
                const borderColors = [];

                for (let i = 0; i < count; i++) {
                    const hue = Math.floor((360 / count) * i);
                    const pastel = `hsl(${hue}, 70%, 80%)`;    // Soft background
                    const border = `hsl(${hue}, 70%, 60%)`;    // Slightly darker border
                    backgroundColors.push(pastel);
                    borderColors.push(border);
                }

                return { backgroundColors, borderColors };
            }

            const { backgroundColors, borderColors } = generatePastelColors(labels.length);

            const data = {
                labels: labels,
                datasets: [{
                    label: 'Viewership',
                    data: dataValues,
                    backgroundColor: backgroundColors,
                    borderColor: borderColors,
                    borderWidth: 1
                }]
            };

            const config = {
                type: 'pie',
                data: data,
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'right',
                            labels: {
                                boxWidth: 20,
                                padding: 10,
                                usePointStyle: true,
                                font: {
                                    size: 14
                                }
                            }
                        },
                        title: {
                            display: true,
                            text: 'City Viewership in District',
                            font: {
                                size: 20
                            }
                        },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    const label = context.label || '';
                                    const value = context.raw || 0;
                                    const percent = ((value / total) * 100).toFixed(2);
                                    return `${label}: ${value.toLocaleString()} (${percent}%)`;
                                }
                            }
                        }
                    },
                    layout: {
                        padding: 20
                    }
                }
            };

            const ctx = document.getElementById('myChart').getContext('2d');
            if (window.myChartInstance) {
                window.myChartInstance.destroy();
            }
            window.myChartInstance = new Chart(ctx, config);
        }

        function renderDynamicPieChart4(labels, dataValues) {
            const total = dataValues.reduce((a, b) => a + b, 0);

            // Gentle pastel color palette generator (HSL based)
            function generatePastelColors(count) {
                const backgroundColors = [];
                const borderColors = [];

                for (let i = 0; i < count; i++) {
                    const hue = Math.floor((360 / count) * i);
                    const pastel = `hsl(${hue}, 70%, 80%)`;    // Soft background
                    const border = `hsl(${hue}, 70%, 60%)`;    // Slightly darker border
                    backgroundColors.push(pastel);
                    borderColors.push(border);
                }

                return { backgroundColors, borderColors };
            }

            const { backgroundColors, borderColors } = generatePastelColors(labels.length);

            const data = {
                labels: labels,
                datasets: [{
                    label: 'Viewership',
                    data: dataValues,
                    backgroundColor: backgroundColors,
                    borderColor: borderColors,
                    borderWidth: 1
                }]
            };

            const config = {
                type: 'pie',
                data: data,
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'right',
                            labels: {
                                boxWidth: 20,
                                padding: 10,
                                usePointStyle: true,
                                font: {
                                    size: 14
                                }
                            }
                        },
                        title: {
                            display: true,
                            text: 'Area Viewership in City',
                            font: {
                                size: 20
                            }
                        },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    const label = context.label || '';
                                    const value = context.raw || 0;
                                    const percent = ((value / total) * 100).toFixed(2);
                                    return `${label}: ${value.toLocaleString()} (${percent}%)`;
                                }
                            }
                        }
                    },
                    layout: {
                        padding: 20
                    }
                }
            };

            const ctx = document.getElementById('myChart').getContext('2d');
            if (window.myChartInstance) {
                window.myChartInstance.destroy();
            }
            window.myChartInstance = new Chart(ctx, config);
        }

    </script>

    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>


            <div class="col-md-10 col-md-offset-1">
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-12">

                            <div class="row">
                                <div class="col-md-2">
                                    Territory:
                                </div>
                                <div class="col-md-8">
                                    <asp:ListBox Multiple="Multiple" SelectionMode="Multiple" class="form-control" ID="ddlTerritory" runat="server"
                                        OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged"
                                        AutoPostBack="True"></asp:ListBox>

                                  <%--  <asp:DropDownList class="form-control" ID="ddlTerritory" runat="server"
                                        OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>--%>
                                </div>
                                <div class="col-md-1">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Content/Images/Chart2-Edit.ico"
                                        OnClick="BtnTerritory_Click" onmouseup="showPleaseWait()" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    Districts:
                                </div>
                                <div class="col-md-8">
                                    <asp:ListBox Multiple="Multiple" SelectionMode="Multiple" class="form-control" ID="ddlDistricts" runat="server"
                                        OnSelectedIndexChanged="ddlDistricts_SelectedIndexChanged"
                                        AutoPostBack="True"></asp:ListBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Content/Images/Chart2-Edit.ico"
                                        OnClick="BtnDistrict_Click" onmouseup="showPleaseWait()" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    City:
                                </div>
                                <div class="col-md-8">
                                    <asp:ListBox Multiple="Multiple" SelectionMode="Multiple" class="form-control" ID="ddlCity" runat="server"
                                        OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"
                                        AutoPostBack="True"></asp:ListBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Content/Images/Chart2-Edit.ico"
                                        OnClick="BtnCity_Click" onmouseup="showPleaseWait()" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    Area:
                                </div>
                                <div class="col-md-8">
                                    <asp:ListBox Multiple="Multiple" SelectionMode="Multiple" class="form-control" ID="ddlArea" runat="server"
                                        OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"
                                        AutoPostBack="True"></asp:ListBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Content/Images/Chart2-Edit.ico"
                                        OnClick="BtnArea_Click" onmouseup="showPleaseWait()" />
                                </div>
                            </div>
                            <div class="row">
                                <asp:Label ID="lblchannelName" runat="server"></asp:Label>
                                <div class="helptext" id="PleaseWait" style="display: none; text-align: right; color: White; vertical-align: top;">
                                    <table id="MyTable" bgcolor="red" align="center">
                                        <tr>
                                            <td style="width: 250px" align="center">
                                                <b><font color="white">Please Wait...</font></b>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />

                    <div class="col-md-12 text-center">
                        <div style="width: 100%; height: 600px;">
                            <canvas id="myChart"></canvas>
                        </div>
                    </div>

                </div>
                <div class="row">

                    <div class="col-md-12">
                        <asp:GridView ID="GridViewTerritory" runat="server" AutoGenerateColumns="False" CssClass="EU_DataTable" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="AreaName" HeaderText="Territory" />
                                <asp:BoundField DataField="Subscribers" HeaderText="Subscribers" />
                                <asp:BoundField DataField="Percentage" HeaderText="Percentage" />
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
                
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
