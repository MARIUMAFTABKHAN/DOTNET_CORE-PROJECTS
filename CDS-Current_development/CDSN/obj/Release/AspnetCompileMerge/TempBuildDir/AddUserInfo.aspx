<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AddUserInfo.aspx.cs" Inherits="CDSN.AddUserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
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

                    //$('#MainContent_ddlTerritory').SumoSelect({
                    //    placeholder: 'Select Territory', includeSelectAllOption: true, okCancelInMulti: true, search: true, searchText: 'Select Country', selectAll: true
                    //});

                    //$('#MainContent_ddlDivision').SumoSelect({ okCancelInMulti: true, search: true, searchText: 'Enter here.', selectAll: true });

                    //$('#MainContent_ddlCity').SumoSelect({ okCancelInMulti: true, search: true, searchText: 'Enter here.', selectAll: true });

                    $('#MainContent_ddlcountry').SumoSelect({

                        placeholder: 'Select Country', includeSelectAllOption: true, okCancelInMulti: true, search: true, searchText: 'Select Country', selectAll: true
                    });



                }
            </script>
            <style type="text/css">
                input,
                select,
                textarea {
                    max-width: 280px;
                }
            </style>
            <div class="form-horizontal">
                <div class="col-md-10 col-md-offset-1">
                    <div class="form-group">
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <h4>User Management
                    </h4>

                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                User Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtUserName" ForeColor="Red" Display="Dynamic"
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                FirstName
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtfirstname" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtfirstname" ForeColor="Red" Display="Dynamic"
                                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                LastName
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtlastname" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtlastname" ForeColor="Red" Display="Dynamic"
                                    ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Password
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtPWD" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtPWD" ForeColor="Red" Display="Dynamic"
                                    ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Email
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic"
                                    ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                                    Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RegularExpressionValidator>



                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Contact #
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtContact" MaxLength="11" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtContact" ForeColor="Red" Display="Dynamic"
                                    ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtContact"
                                    Display="Dynamic" ValidationExpression="^[0-9]{11}$" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RegularExpressionValidator>

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Role
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Address
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" Rows="4" Style="height: 80px; min-width: 100%" runat="server" CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-2">
                        Country
                    </div>
                    <div class="col-md-4">
                        <asp:ListBox Multiple="Multiple" SelectionMode="Multiple" AutoPostBack="true" runat="server" ID="ddlcountry" CssClass="form-control" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" style="width:78% !important; min-width:78% !important; margin-bottom:3px !important"></asp:ListBox>

                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                City
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Active?
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox ID="ChkIsActive" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Admin?
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox ID="ChkIsAdmin" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-4 col-md-offset-2">
                                <asp:Button ID="btnSave" Style="min-width: 120px" runat="server" CssClass="btn btn-info" Text="Save" OnClick="btnSave_Click" />&nbsp;                    
                                  <asp:Button ID="btnCancel" Style="min-width: 120px" runat="server" ValidationGroup="c" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-md-12" style="margin-top: 25px;">
                <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="UserID" AutoGenerateColumns="false"
                    AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <%--<asp:BoundField DataField="UserID" HeaderText="User ID">
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="UserName" HeaderText="UserName">
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FirstName" HeaderText="FirstName">
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LastName" HeaderText="LastName">
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="Email">
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ContactNo" HeaderText="Contact"></asp:BoundField>
                        <asp:BoundField DataField="UserRole" HeaderText="Role"></asp:BoundField>
                        <asp:BoundField DataField="IsActive" HeaderText="Active?"></asp:BoundField>
                        <asp:BoundField DataField="IsAdmin" HeaderText="Admin?"></asp:BoundField>
                        <asp:TemplateField>
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="40px" />
                            <HeaderTemplate>
                                Edit
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/News-Edit.ico'
                                        ValidationGroup="a" OnClick="EditButton_Click" />
                                </center>
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
                                        ValidationGroup="a" OnClick="DelButton_Click" OnClientClick='<%# "RemoveRecord(" +Eval("UserID") + " );" %>' />
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            </div>

            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
