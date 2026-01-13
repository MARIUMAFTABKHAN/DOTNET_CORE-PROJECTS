<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AddOperatorInfo.aspx.cs" Inherits="CDSN.AddOperatorInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .style2 {
            width: 100%;
        }

        .style3 {
            height: 27px;
        }

        .row {
            margin-bottom: 5px !important;
        }
        /*.lbl{
            font-size:medium;
            font-weight:bold;
        }*/
    </style>


    <script type="text/javascript">
        function pageLoad() {


            $('#<%= txtwef.ClientID  %>').datepicker({
                showOn: 'button',
                buttonImage: "Content/images/calander.png",
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                maxDate: '+0m',
                buttonImageOnly: true

            });

        }
        function ShowMessage(message) {


            if (message == "Saved") {

                jAlert(message, 'CDS Alerts');
            }
            else if (message == "Updated") {

                jAlert(message, 'CDS Alerts');
            }
            else if (message == "Already") {
                jAlert(message, 'CDS Alerts');

            }
            else if (message == "REFERENCE") {
                jAlert(message, 'CDS Alerts');
            }
            else {
                jAlert(message, 'CDS Alerts');
            }
        }

    </script>

    <div class="container">

        <div class="col-md-10 col-md-offset-1">

            <div class="row" style="text-align: center">
                <asp:HiddenField ID="HFOptrInfoID" runat="server" />
                <h3>Operator Info</h3>
            </div>
            <div class="row">
                <asp:Label ID="lblMsg" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblException" runat="server" Visible="false"></asp:Label>

            </div>

            <div class="row">
                <div class="col-md-2">
                    No of Channels :
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtChNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Country:
                </div>
                <div class="col-md-4">

                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control"
                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Region:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Territory:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlTerritory" runat="server" AutoPostBack="true"
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    District:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlDivision" runat="server" AutoPostBack="true"
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    City:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlCity" runat="server"
                        CssClass="form-control" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Operator:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlOp" runat="server" AutoPostBack="true"
                        CssClass="form-control" OnSelectedIndexChanged="ddlOp_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Area/Town:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlArea" CssClass="form-control" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Sub Area/Localty:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlSubArea" runat="server" AutoPostBack="true" CssClass="form-control"
                        OnSelectedIndexChanged="ddlSubArea_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    EF.Date:
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtwef" CssClass="form-control" runat="server" Enabled="False" Width="120px" Style="width: 75%; display: inline"></asp:TextBox>

                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2 col-md-offset-2">
                    <asp:Button ID="btnSave" Style="min-width: 100%" runat="server" CssClass="btn btn-info" OnClick="btnSave_Click" Text="Save" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnCancel" Style="min-width: 100%" runat="server" CssClass="btn btn-danger" OnClick="btnCancel_Click"
                        Text="Cancel" />
                </div>


            </div>

            <div class="row">
                <div class="col-md-4 col-md-offset-2">
                    <asp:Label ID="lblGrid" runat="server" CssClass="lbl" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gvRecords" runat="server" DataKeyNames="ID" AllowPaging="True" 
                        AutoGenerateColumns="False" CssClass="EU_DataTable" Width="100%" OnPageIndexChanging="gvRecords_PageIndexChanging">
                        <HeaderStyle BackColor="#4D758B" ForeColor="white" HorizontalAlign="Center" Height="25px" />
                        <Columns>
                            <%--<asp:BoundField DataField="ID" HeaderText="Info Id">
                                <ItemStyle HorizontalAlign="Left" Width="70" />
                            </asp:BoundField>

                            <asp:BoundField DataField="OperatorId" HeaderText="Operator Id">
                                <ItemStyle HorizontalAlign="Left" Width="70" />
                            </asp:BoundField>--%>

                            <asp:BoundField DataField="Name" HeaderText="Operator Name">
                                <ItemStyle HorizontalAlign="Left" Width="450" />
                            </asp:BoundField>

                            <%--<asp:BoundField DataField="AreaId" HeaderText="Area Id">
                                <ItemStyle HorizontalAlign="Left" Width="70" />
                            </asp:BoundField>--%>

                            <asp:BoundField DataField="AreaName" HeaderText="Area Name">
                                <ItemStyle HorizontalAlign="Left" Width="200" />
                            </asp:BoundField>

                            <%--<asp:BoundField DataField="SubAreaId" HeaderText="SubArea Id">
                                <ItemStyle HorizontalAlign="Left" Width="70" />
                            </asp:BoundField>--%>

                            <asp:BoundField DataField="SubAreaName" HeaderText="Sub Area">
                                <ItemStyle HorizontalAlign="Left" Width="170" />
                            </asp:BoundField>

                            <%--<asp:BoundField DataField="TerritoryId" HeaderText="Territory Id">
                                <ItemStyle HorizontalAlign="Left" Width="70" />
                            </asp:BoundField>--%>

                            <asp:BoundField DataField="TerritoryName" HeaderText="Territory Name">
                                <ItemStyle HorizontalAlign="Left" Width="170" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Subscribers" HeaderText="Subscribers" Visible="false">
                                <ItemStyle HorizontalAlign="Left" Width="70" />
                            </asp:BoundField>

                            <asp:BoundField DataField="WEF" HeaderText="WEF">
                                <ItemStyle HorizontalAlign="Left" Width="200" />
                            </asp:BoundField>

                            <%--<asp:BoundField DataField="DivisionId" HeaderText="Division Id">
                                <ItemStyle HorizontalAlign="Left" Width="70" />
                            </asp:BoundField>--%>

                            <asp:BoundField DataField="DivisionName" HeaderText="District Name">
                                <ItemStyle HorizontalAlign="Left" Width="170" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Edit">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="CENTER" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" ImageUrl="~/Content/Images/News-Edit.ico" runat="server" 
                                        OnClick="btnEdit_Click" ValidationGroup="a"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                           <%-- <asp:TemplateField HeaderText="Delete">
                                 <ItemTemplate>
                                     <asp:ImageButton ID="btnDelete" ImageUrl="~/Content/Images/Trash-Cancel.ico" runat="server"
                                         ValidationGroup="a" OnClick="btnDelete_Click"  CommandArgument='<%#Eval("ID")%>'/>
                                 </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
