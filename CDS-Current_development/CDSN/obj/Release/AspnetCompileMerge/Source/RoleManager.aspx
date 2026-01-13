<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="RoleManager.aspx.cs" Inherits="CDSN.RoleManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:UpdatePanel ID="up" runat="server">--%>
        <ContentTemplate>
              <script type="text/javascript">
              </script>
            <div class="form-horizontal">
                <div class="col-md-10 col-md-offset-1">
                    <div class="form-group">
                        <div class="div-md-12 text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <h4>Role Management
                    </h4>

                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                Role
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtRoleName" ForeColor="Red" Display="Dynamic"
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                Active
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="ChkIsActive" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-1 col-md-offset-2">
                                <asp:Button ID="btnSave" Style="min-width: 100%" runat="server" CssClass="btn btn-info" Text="Save" OnClick="btnSave_Click" />&nbsp;                    
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnCancel" Style="min-width: 100%" runat="server" ValidationGroup="c" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                            
                        </div>
                        <div class="div-md-12" style="margin-top: 50px;">
                            <asp:GridView ID="gv" PageSize ="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                                AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                                <PagerStyle  HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="UserRole" HeaderText="Role">
                                        <HeaderStyle Width="90%" />
                                    </asp:BoundField>                                   
                                    <asp:BoundField DataField="Status" HeaderText="Active"></asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
                                        <HeaderTemplate>
                                            Edit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <center>                                     
                                    <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/News-Edit.ico'
                                        ValidationGroup="a" OnClick="EditButton_Click"  /></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Delete
                                        </HeaderTemplate>
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
                                        <ItemTemplate>
                                            <center>
                                             <asp:ImageButton runat="server" ID="DelButton" ImageUrl='~/Content/images/Trash-Cancel.ico'                                                   
                                                 ValidationGroup="a"  OnClick="DelButton_Click" CommandArgument='<%#Eval("ID")%>' /></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    <%--</asp:UpdatePanel>--%>

</asp:Content>
