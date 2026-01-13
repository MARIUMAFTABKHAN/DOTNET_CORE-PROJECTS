<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Propritors.aspx.cs" Inherits="CDSN.Propritors" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .row {
            margin-bottom: 5px !important;
        }
    </style>
   <%-- <asp:UpdatePanel ID="up" runat="server">--%>
        <ContentTemplate>
            <div class="col-md-10 col-md-offset-1">
                <div class="row" style="text-align:center">
                    <h3>Proprietor</h3>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblException" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Division 
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlDivision" runat="server" AutoPostBack="True"
                            CssClass="form-control" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                     <div class="col-md-2">
                        CNIC :
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtcnic" MaxLength="15" CssClass="form-control" runat="server">
                        </asp:TextBox>
                    </div>
                </div>
                 <div class="row">
                     <div class="col-md-2">
                        City:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                     <div class="col-md-2">
                        Cell:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtcell" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Name :
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtUser" runat="server" ControlToValidate="txtName"
                            Display="None" ErrorMessage="Name is Required." Font-Size="X-Small" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvtxtUser">
                        </asp:ValidatorCalloutExtender>
                    </div>
                    <div class="col-md-2">
                        Contact:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtcontact" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Address:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtAddress" runat="server" Style="min-width: 100%; height: 70px;"
                            TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtaddress" runat="server"
                            ControlToValidate="txtAddress" Display="None" ErrorMessage="Address required."
                            Font-Size="X-Small" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                            TargetControlID="rfvtxtaddress">
                        </asp:ValidatorCalloutExtender>
                    </div>
                    <div class="col-md-2">
                        Email:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revtxtEmail" runat="server"
                            ControlToValidate="txtEmail" Display="None"
                            ErrorMessage="Email must be in (abc@abc.com) format" Font-Size="X-Small"
                            ValidationExpression=".+@.+" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                            TargetControlID="revtxtEmail">
                        </asp:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                   
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Active:
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="ChkActive" runat="server" />
                    </div>
                </div>               
                <div class="row">
                    <div class="col-md-4 col-md-offset-2">
                        <div class="submitButton" align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                                CssClass="btn btn-info" Style="min-width: 120px" />&nbsp;
                                                <asp:Button ID="btnCancel" Style="min-width: 120px"
                                                    runat="server" Text="Cancel" CausesValidation="false"
                                                    CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblGrid" runat="server" CssClass="lbl" Text="Properiter" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">

                        <asp:GridView ID="gvRecords" runat="server" AllowSorting="false" DataKeyNames="Id"
                            AutoGenerateColumns="False" CssClass="EU_DataTable" Width="100%" OnPageIndexChanging="gvRecords_PageIndexChanging">
                            <HeaderStyle BackColor="#4D758B" ForeColor="white" HorizontalAlign="Center" Height="25px" />
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id">
                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="Name">
                                    <ItemStyle HorizontalAlign="Left" Width="40%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CityName" HeaderText="City">
                                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CNIC" HeaderText="CNIC">
                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CellNo" HeaderText="Cell No">
                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="Email">
                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
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
  <%--  </asp:UpdatePanel>--%>
</asp:Content>
