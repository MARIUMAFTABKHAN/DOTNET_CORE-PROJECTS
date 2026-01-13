<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="UserTerritories.aspx.cs" Inherits="CDSN.UserTerritories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <style type="text/css">
                .row{
                    margin-bottom:5px !important;
                }
            </style>
            <div class="col-md-10 col-md-offset-1">
                <div class="row" style="text-align: center">
                    <h3>Territory privileges</h3>
                </div>
                <div class="row" style="text-align: center">
                    <asp:Label ID="lblMsg" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblException" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        User:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="true" CssClass="form-control"
                           style="min-width:75%" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Country:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control"
                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" style="min-width:75%" >
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Region:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" CssClass="form-control"
                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" style="min-width:75%" >
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-2">
                    <asp:HiddenField ID="DivisionId" runat="server"></asp:HiddenField>
                    <div class="submitButton" align="center">
                        <div class="submitButton" align="center">
                        &nbsp;   &nbsp;  &nbsp; <asp:Button ID="btnSave" Style="min-width: 150px" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-info" />
                            &nbsp;
                             <asp:Button ID="btnCancel" Style="min-width: 150px" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                 CssClass="btn btn-danger" />
                        </div>


                    </div>

                </div>
                <div class="row" style="margin-top:10px !important">
                    <div class="col-md-10 col-md-offset-1" style="margin-top:10px !important">
                        <asp:GridView ID="gvRoles" runat="server" DataKeyNames="Id" Width="100%" AutoGenerateColumns="False"
                            CssClass="EU_DataTable" PageSize="50">
                            <HeaderStyle BackColor="#4D758B" ForeColor="white" HorizontalAlign="Center" Height="25px" />
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID">
                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TerritoryName" HeaderText="Territory Name">
                                    <ItemStyle HorizontalAlign="Left" Width="90%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="formSelector" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem,"IsActive") %>' />
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
