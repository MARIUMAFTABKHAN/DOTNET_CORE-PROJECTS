<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="TerritoryManager.aspx.cs" Inherits="CDSN.TerritoryManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:UpdatePanel ID="up" runat="server">--%>

        <ContentTemplate>
            <style type="text/css">
                .row{
                    margin-bottom:5px !important;
                }
            </style>
            <div class="col-md-10 col-md-offset-1">
                <div class="row" style="text-align: center">
                    <h3>Add Territory</h3>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblException" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Region:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlRegion" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true" Style="min-width: 100%" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        Territory:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtTerritoryName" runat="server" Style="min-width: 100%" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtUser" runat="server" ControlToValidate="txtTerritoryName"
                            Display="None" ErrorMessage="Territory Name is Required." Font-Size="X-Small" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Short Name:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtShortName" MaxLength="4" runat="server" Style="min-width: 100%" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShortName"
                            Display="None" ErrorMessage="Short Name is Required." Font-Size="X-Small" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Active:
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chk" runat="server" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2 col-md-offset-2">
                        <asp:Button ID="btnSave" Style="min-width: 100%" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-info" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnCancel" Style="min-width: 100%" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-2">
                    <asp:Label ID="lblGrid" runat="server" CssClass="lbl" />
                </div>
            </div>

            <div class="row">
                <div class="col-md-10 col-md-offset-1">
                    <asp:GridView ID="gvRecords" DataKeyNames="Id,RegionId" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="EU_DataTable" Width="100%" OnPageIndexChanging="gvRecords_PageIndexChanging">
                        <HeaderStyle BackColor="#4D758B" ForeColor="white" HorizontalAlign="Center" Height="25px" />
                        <Columns>
                            <asp:BoundField DataField="RegionName" HeaderText="Region">
                                <ItemStyle HorizontalAlign="Left" Width="200" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TerritoryName" HeaderText="Territory ">
                                <ItemStyle HorizontalAlign="Left" Width="500" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ShortNames" HeaderText="Short Names ">
                                <ItemStyle HorizontalAlign="Left" Width="150" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status">
                                <ItemStyle HorizontalAlign="Left" Width="100" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="CENTER" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" ImageUrl="~/Content/Images/News-Edit.ico" runat="server" OnClick="btnEdit_Click" ValidationGroup="a" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" ImageUrl="~/Content/Images/Trash-Cancel.ico" runat="server"
                                        ValidationGroup="a" OnClick="btnDelete_Click" CommandName="Delete" CommandArgument='<%#Eval("Id")%>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            </div>





        </ContentTemplate>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
