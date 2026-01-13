<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="DWR.aspx.cs" Inherits="CDSN.DWR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/jquery.sumoselect.js"></script>
    <link href="Content/sumoselect.css" rel="stylesheet" />
    <script type="text/javascript">

        function pageLoad() {
            SetDDL();
        }
        $(document).ready(function () {
            SetDDL();

        });
        function SetDDL() {
            $('#MainContent_ddlRegion').SumoSelect({

                placeholder: 'Select Region', includeSelectAllOption: true, okCancelInMulti: true, search: true, searchText: 'Select Region', selectAll: true
            });
            $('#MainContent_ddlTerritory').SumoSelect({

                placeholder: 'Select Territory', includeSelectAllOption: true, okCancelInMulti: true, search: true, searchText: 'Select Territory', selectAll: true
            });
        }
        function showPleaseWait() {
            document.getElementById('PleaseWait').style.display = 'block';
        }
        function OpenPopup() {
            window.open("WeatherChart.aspx?", "_blank", "height=400, width=830, left=100,top=100, " + "location=no, menubar=no, resizable=no, " +
                "scrollbars=yes, titlebar=no, toolbar=no", true);
        }
    </script>
    <style type="text/css">
        input,
        select,
        textarea {
            max-width: 280px;
        }

        .row {
            margin-bottom: 5px !important;
        }
    </style>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="container">

                <div class="col-md-10 col-md-offset-1">
                    <div class="row">
                        <div class="col-md-12">
                            &nbsp;&nbsp;&nbsp;<asp:Label ID="lblRecordHeader" Text="Distribution Weather Report" runat="server" CssClass="lbl" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            &nbsp;&nbsp;&nbsp;<asp:Label ID="Err" runat="server" CssClass="lbl" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Cannel type:
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlChannelType" runat="server" CssClass="form-control"
                            AutoPostBack ="true"    OnSelectedIndexChanged="ddlChannelType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Country:
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true"
                                CssClass="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Region:
                        </div>
                        <div class="col-md-4">
                            <asp:ListBox Multiple="Multiple" SelectionMode="Multiple" ID="ddlRegion" runat="server" AutoPostBack="True" CssClass="form-control"
                                OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:ListBox>
                        </div>
                        <div class="col-md-2">
                            <asp:CheckBox ID="ChkRegion" runat="server" OnCheckedChanged="ChkRegion_CheckedChanged"
                                Text="Regional " />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Territory:
                        </div>
                        <div class="col-md-4">
                            <asp:ListBox Multiple="Multiple" SelectionMode="Multiple" ID="ddlTerritory" runat="server" AutoPostBack="True" CssClass="form-control"
                                OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged"></asp:ListBox>
                        </div>
                    </div>
                    <div class="row">
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
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnshow" OnClick="btnshow_Click" onmouseup="showPleaseWait()" class="btn btn-info" runat="server" Text="Report" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label3" runat="server" Text="Total Headends :" CssClass="lbl" Font-Bold="True"></asp:Label>
                            <asp:Label ID="TotalHeadends" runat="server" Text="Headends" CssClass="lbl" Font-Bold="True"></asp:Label>

                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" Text="Total Viewers :" CssClass="lbl" Font-Bold="True"></asp:Label>
                            <asp:Label ID="TotalViewers" runat="server" Text="Viewers" CssClass="lbl" Font-Bold="True"></asp:Label>

                        </div>

                    </div>
                    <div class="row">
                        <asp:Panel CssClass="panelheader" ID="pnlChart" runat="server" Width="100%"
                            Style="height: auto; overflow: hidden">
                            &nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Distribution Weather Chart"
                                CssClass="lbl" />
                            
                            
                        </asp:Panel>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvRecords" runat="server" AutoGenerateColumns="False" Width="100%"
                            OnRowCreated="gvRecords_RowCreated" CssClass="EU_DataTable" OnRowCommand="gvRecords_RowCommand"
                            OnRowDataBound="gvRecords_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="RegionName" HeaderText="Region" SortExpression="RegionName">
                                    <ItemStyle HorizontalAlign="Left" Width="150" ForeColor="Black" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TerritoryName" HeaderText="Base Station" SortExpression="TerritoryName">
                                    <ItemStyle HorizontalAlign="Left" Width="150" ForeColor="Black" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HeadEnd" HeaderText="Head End" SortExpression="HeadEnd">
                                    <ItemStyle HorizontalAlign="Left" Width="110" ForeColor="Black" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Viewers" HeaderText="viewers" SortExpression="Viewers">
                                    <ItemStyle HorizontalAlign="Left" Width="120" ForeColor="Black" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Percentage" HeaderText="Percentage" SortExpression="Percentage">
                                    <ItemStyle HorizontalAlign="Left" Width="120" ForeColor="Black" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="Channel" HeaderText="Channel" SortExpression="Channel">
                                    <ItemStyle HorizontalAlign="Left" Width="150" ForeColor="Black" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Good" HeaderText="Good" SortExpression="Good">
                                    <ItemStyle HorizontalAlign="Left" Width="100" BackColor="#A4E07D" ForeColor="Black" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Fair" HeaderText="Fair" SortExpression="Fair">
                                    <ItemStyle HorizontalAlign="Left" Width="100" BackColor="#F7F7A8" ForeColor="Black" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ugly" HeaderText="Ugly" SortExpression="Ugly">
                                    <ItemStyle HorizontalAlign="Left" Width="100" BackColor="#F6C3C3" ForeColor="Black" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NotAvailable" HeaderText="Not Available" SortExpression="NotAvailable">
                                    <ItemStyle HorizontalAlign="Left" BackColor="#FA6464" ForeColor="Black" Width="100" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Graph">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton11" runat="server" CommandName="Google" ImageUrl="~/Content/Images/Chart2-Edit.ico"
                                            CommandArgument='<%# Eval("RegionId")+ ";" + Eval("TerritoryId")%>' onmouseup="showPleaseWait()" />
                                          <asp:HiddenField ID="HDRegion" runat="server" Value='<%# Eval("RegionName") %>' />
  <asp:HiddenField ID="HDTerritory" runat="server" Value='<%# Eval("TerritoryName") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="50" />
                                </asp:TemplateField>
                               
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
