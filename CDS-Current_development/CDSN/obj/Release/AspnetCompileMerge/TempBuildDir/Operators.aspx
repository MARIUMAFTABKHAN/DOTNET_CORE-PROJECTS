<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Operators.aspx.cs" Inherits="CDSN.Operators" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <script src="Scripts/jquery-ui-1.13.2.js"></script>

    <link href="Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
    
        function pageLoad() {

            //$("#Button1").unbind();
            $('#<%= txtwef.ClientID  %>').datepicker({
                showOn: 'button',
                buttonImage: "Content/images/calander.png",
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                buttonImageOnly: true

            });

        }
    </script>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <style type="text/css">
                .row {
                    margin-bottom: 5px !important;
                }
            </style>
            <div class="col-md-10 col-md-offset-1">
                <div class="row" style="text-align:center">
                    <h3>Operator</h3>
                </div>
                <div class="row text-center">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    <asp:Label ID="lblException" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Division :
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddldiv" runat="server" CssClass="form-control"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="ddldiv_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                 </div>
                <div class="row">
                    <div class="col-md-2">
                        City:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlCity" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Proprietor:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlProprietor" runat="server"
                            CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProprietor_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvtxtUser" runat="server" ControlToValidate="ddlProprietor"
                            Display="None" ErrorMessage="Propertier is Required." Font-Size="X-Small" />
                    </div>
                </div>
                <div class="row">
                     <div class="col-md-2">
                        Name:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName"
                                Display="None" ErrorMessage="Name is Required." Font-Size="X-Small" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Address
                    </div>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtAddress" MaxLength="250" TextMode="MultiLine" Rows="3"
                            Style="height: 60px; min-width: 100%" CssClass="form-control" runat="server">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress"
                            Display="Dynamic" ErrorMessage="Address is Required." Font-Size="X-Small" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Mobile
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtMobile" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        Land Line:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtLandline" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Email:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revtxtEmail" runat="server" ControlToValidate="txtEmail"
                            Display="None" ErrorMessage="Email must be in (abc@abc.com) format" ValidationExpression=".+@.+"
                            Font-Size="X-Small" />
                    </div>
                    <div class="col-md-2">
                        Review Date:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtwef" runat="server" CssClass="form-control" Style="width: 75%; display: inline" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
            <div class="row">
                <div class="col-md-2">
                    License No:
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtLic" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Lic Status:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddllicStatus" runat="server"
                        CssClass="form-control">
                        <asp:ListItem text="NEW" Value="NEW"></asp:ListItem>
                        <asp:ListItem text="RENEW" Value="RENEW"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Area Type:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlAreaType" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                 <div class="col-md-2">
                   Scbcriber:
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtsubsribers" runat="server" TextMode="Number"
                        CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    Active
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="Chkactive" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <div align="center" class="submitButton">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"
                            CssClass="btn btn-info" Style="min-width: 120px" />
                                 &nbsp;
                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                            CssClass="btn btn-danger" Style="min-width: 120px" />
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblGrid" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gvRecords" runat="server" DataKeyNames="Id"
                        AutoGenerateColumns="False" CssClass="EU_DataTable" Width="100%" OnPageIndexChanging="gvRecords_PageIndexChanging">
                        <HeaderStyle BackColor="#4D758B" ForeColor="white" HorizontalAlign="Center" Height="25px" />
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID">
                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name">
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CityName" HeaderText="City">
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Cell" HeaderText="Mobile No">
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email">
                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LicCategory" HeaderText="Lic.Category">
                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LicNo" HeaderText="Lic.No">
                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Lic_Status" HeaderText="Lic Type">
                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Active">
                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="CENTER" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" ImageUrl="~/Content/Images/News-Edit.ico" runat="server" 
                                        OnClick="btnEdit_Click" ValidationGroup="a"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                 <ItemTemplate>
                                     <asp:ImageButton ID="btnDelete" ImageUrl="~/Content/Images/Trash-Cancel.ico" runat="server"
                                         ValidationGroup="a" OnClick="btnDelete_Click"  CommandArgument='<%#Eval("Id")%>'/>
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
    </asp:UpdatePanel>
</asp:Content>

