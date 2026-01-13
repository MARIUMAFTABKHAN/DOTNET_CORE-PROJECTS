<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ChartJS.aspx.cs" Inherits="CDSN.ChartJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>

    <asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   
     <asp:UpdatePanel ID="up" runat="server">
 <ContentTemplate>
    <style type="text/css">
    .style2 {
        text-align: right;
    }

    .row {
        margin-top: 5px !important;
    }

    .EU_DataTable tr th {
        background-color: #a8987e;
    }

    .EU_DataTable tr:nth-child(2n+2) {
        background-color: #f7edbb;
    }
</style>
<script type="text/javascript" language="javascript">
    var myChart;
    var xValues = [];
    var yValues = [];
    var myLabels = [];
    var myData = [];
    var myLabels1 = [];
    var myData1 = [];
    var myLabels2 = [];
    var myData2 = [];
    var myLabels3 = [];
    var myData3 = [];
    var myLabels4 = [];
    var myData4 = [];
    var barColors = ["red", "green", "blue", "orange", "brown"];

    function showPleaseWait() {

        document.getElementById('PleaseWait').style.display = 'block';
        //ShowBars();
    }

    function pageLoad() {
        SetDDL();
        //ShowBars();

    }
    function showalert(val) {
        ShowBars(val);
    }
    document.addEventListener('DOMContentLoaded', function () {
        var ctx = document.getElementById('myChart').getContext('2d');
        myChart = new Chart(ctx, {

        });
    });
    $(document).ready(function () {
        SetDDL();

    });


    function SetDDL() {

        $('#MainContent_ddlTerritory').SumoSelect({
            placeholder: 'Select Territory', includeSelectAllOption: true, okCancelInMulti: true, search: true, searchText: 'Select Territory', selectAll: true
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




    function updateChart(response) {
        debugger;

        if (myChart) {
            myChart.destroy(); // Destroy previous chart instance
        }
        var myLabels = [];
        var myData = [];
        var myLabels1 = [];
        var myData1 = [];
        var myLabels2 = [];
        var myData2 = [];
        var myLabels3 = [];
        var myData3 = [];
        var myLabels4 = [];
        var myData4 = [];
        var myOperators;
        var myOperators1
        var myOperators2;
        var myOperators3;
        var myOperators4;
        var barColors = ["red", "green", "blue", "orange", "brown"];
        debugger;

        response.forEach(item => {

            item.NewBars.forEach(bar => {
                myLabels.push(bar.ChannelName);
                myData.push(bar.CurrentPosition);
                myOperators = bar.OperatorName;

            });
        });


        response.forEach(item => {

            item.NewBars1.forEach(bar3 => {
                myLabels1.push(bar3.ChannelName);
                myData1.push(bar3.CurrentPosition);
                myOperators1 = bar3.OperatorName;
            });
        });
        response.forEach(item => {

            item.NewBars2.forEach(bar1 => {
                myLabels2.push(bar1.ChannelName);
                myData2.push(bar1.CurrentPosition);
                myOperators2 = bar1.OperatorName;
            });
        });
        response.forEach(item => {

            item.NewBars3.forEach(bar => {
                myLabels3.push(bar.ChannelName);
                myData3.push(bar.CurrentPosition);
                myOperators3 = bar.OperatorName;
            });
        });
        response.forEach(item => {

            item.NewBars4.forEach(bar => {
                myLabels4.push(bar.ChannelName);
                myData4.push(bar.CurrentPosition);
                myOperators4 = bar.OperatorName;
            });
        });
        var ctx = document.getElementById('myChart').getContext('2d');
        myChart = new Chart(ctx, {
            type: 'line',
            data: {
                datasets: [{
                    data: myData,
                    colors: barColors,
                    labels: myLabels
                    // this dataset is drawn below

                }, {

                    data: myData1,
                    colors: barColors,
                    labels: myLabels1
                    // this dataset is drawn on top

                }, {

                    data: myData2,
                    colors: barColors,
                    labels: myLabels2
                    // this dataset is drawn on top

                }, {

                    data: myData3,
                    colors: barColors,
                    labels: myLabels3
                    // this dataset is drawn on top

                }, {

                    data: myData4,
                    colors: barColors,
                    labels: myLabels4
                    // this dataset is drawn on top
                }
                ],
                labels: [myOperators1, myOperators2, myOperators3, myOperators4]
            },

            options: {
                tooltips: {
                    callbacks: {
                        title: function (tooltipItem, data) {
                            return data['labels'][tooltipItem[0]['index']];
                        },
                        label: function (tooltipItem, data) {
                            return data['datasets'][0]['data'][tooltipItem['index']];
                        },
                        afterLabel: function (tooltipItem, data) {
                            var dataset = data['datasets'][0];                                
                            return  data['datasets'][0]['data'][tooltipItem['index']];
                        }
                    },
                    backgroundColor: '#FFF',
                    titleFontSize: 16,
                    titleFontColor: '#0066ff',
                    bodyFontColor: '#000',
                    bodyFontSize: 14,
                    displayColors: false
                }
            }
        });
    }

    function ShowBars() {

        var territoryid = $('#<%= ddlTerritory.ClientID %>').val();
        var channelid = 110000002;

        $.ajax({
            type: "POST",
            url: "/Services/DataSetBars.asmx/GetBarData",
            data: JSON.stringify({ territoryid: territoryid, channelid: channelid }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                debugger;
                updateChart(data.d);
            },
            error: function (error) {
                console.error("Error fetching chart data", error);
            }
        });

    }
</script>
    


        <div class="col-md-10 col-md-offset-1">
            <div class="row">
                <div class="col-md-6">
                    <div class="col-md-12">

                        <div class="row">
                            <div class="col-md-2">
                                Territory:
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlTerritory" runat="server"
                                    OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
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

                <div class="col-md-6">

                    <div class="col-md-12">
                        <canvas id="myChart"></canvas>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="col-md-12">
                    <asp:GridView ID="GridViewViewership"
                        DataKeyNames="OperatorId,ChannelName,CurPosition"
                        CssClass="EU_DataTable" runat="server" AutoGenerateColumns="False"
                        Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="W.E.F">
                                <ItemTemplate>
                                    <asp:Label ID="Labwef" runat="server" Text='<%# Eval("WEF", "{0:MM/dd/yyyy}") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channel">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("ChannelName") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Operator">
                                <ItemTemplate>
                                    <asp:Label ID="Label16" runat="server" Text='<%# Eval("Name")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Division">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("DivisionName") %>'>
                                    </asp:Label>
                                </ItemTemplate>


                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Curr.P">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("CurPosition") %>'>
                                    </asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pre.P">
                                <ItemTemplate>
                                    <asp:Label ID="Labwefa" runat="server" Text='<%# Eval("PrevPosition") %>'>
                                    </asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
  
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


