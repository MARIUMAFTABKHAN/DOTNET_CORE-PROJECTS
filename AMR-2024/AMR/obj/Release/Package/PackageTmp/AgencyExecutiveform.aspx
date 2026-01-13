<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgencyExecutiveform.aspx.cs" Inherits="AMR.AgencyExecutiveform" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
        <script type="text/javascript">
            function toggleDropdown() {
                // Get radio buttons and dropdown elements
                var rbBrand = document.getElementById('<%= rbBrand.ClientID %>');
                var rbClient = document.getElementById('<%= rbClient.ClientID %>');
                var brandDropdown = document.getElementById('brandDropdown');
                var clientDropdown = document.getElementById('clientDropdown');

                // Show/Hide based on selected radio button
                if (rbBrand.checked) {
                    brandDropdown.style.display = 'block';
                    clientDropdown.style.display = 'none';
                } else if (rbClient.checked) {
                    clientDropdown.style.display = 'block';
                    brandDropdown.style.display = 'none';
                }
            }
            // Run toggleDropdown on page load to handle postback visibility
            window.onload = function () {
                toggleDropdown();
            };
        </script>
       <div class="col-md-12">
           <div class="col-md-10 col-md-offset-1" style="margin-top: 5px !important">
               <div class="panel panel-default" >
                   <div class="panel-heading" style="background-color: lightgray; font-size:large;">
                       <strong>Agency Executive Record</strong>
                   </div>
                   <%--<br />--%>
                   <div class="panel-body" style="padding-bottom: 9px !important">
                       <div class="col-md-12">
                           <div class="col-md-12 text-center">
                               <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                           </div>
                       </div>

                       <div class="form-group">
                           <div class="row">
                               <div class="col-md-1">
                                   Agency
                               </div>
                               <div class="col-md-3">
                                   <asp:DropDownList ID="ddlAgency" runat="server" CssClass="form-control" Style="font-size: small;" AutoPostBack="true" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged"></asp:DropDownList>
                               </div>
                               <div class="col-md-2"></div>
                               <div class="col-md-1">
                                   Designation
                               </div>
                               <div class="col-md-3">
                                   <asp:TextBox ID="txtdesg" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                               </div>

                           </div>
                       </div>

                       <div class="form-group">
                           <div class="row">
                               <div class="col-md-1">
                                   Name
                               </div>
                               <div class="col-md-3">
                                   <asp:TextBox ID="txtname" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                       ControlToValidate="txtname" ErrorMessage="* Enter Executive Name" ForeColor="Red"></asp:RequiredFieldValidator>
                               </div>
                               <div class="col-md-2"></div>
                               <div class="col-md-1">
                                   Status
                               </div>
                               <div class="col-md-3">
                                   <asp:CheckBox ID="chstatus" runat="server" />
                               </div>
                           </div>
                       </div>
                   </div>
               </div>
           </div>

           <div class="col-md-10 col-md-offset-1" style="margin-top: 5px !important; ">
               <div class="panel panel-default">
                   <div class="panel-heading" style="background-color: lightgray; font-size:large;">
                       <strong>Additional Information</strong>
                   </div>
                   <div class="panel-body" style="padding-bottom: 9px !important">
                       <div class="form-group">
                           <div class="row">
                               <div class="col-md-1">
                                   Office Direct Phone
                               </div>
                               <div class="col-md-3">
                                   <asp:TextBox ID="txtphone" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                               </div>
                               <div class="col-md-1">
                                   Mobile Phone
                               </div>
                               <div class="col-md-3">
                                   <asp:TextBox ID="txtmob" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                               </div>
                               <div class="col-md-1">
                                   Home Phone
                               </div>
                               <div class="col-md-3">
                                   <asp:TextBox ID="txthomeph" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                               </div>

                           </div>
                       </div>

                       <div class="form-group">
                           <div class="row">
                               <div class="col-md-1">
                                   Home Address
                               </div>
                               <div class="col-md-11">
                                   <asp:TextBox ID="txtadd" runat="server" CssClass="form-control" Style="min-width: 100%" ></asp:TextBox>
                               </div>
                           </div>
                       </div>
                       <div class="form-group">
                           <div class="row">
                               <div class="col-md-1">
                                   City
                               </div>
                               <div class="col-md-3">
                                   <asp:TextBox ID="txtcity" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                               </div>
                               <div class="col-md-1">
                                   Postal Code
                               </div>
                               <div class="col-md-3">
                                   <asp:TextBox ID="txtcode" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                               </div>
                               <div class="col-md-1">
                                   Email Address
                               </div>
                               <div class="col-md-3">
                                   <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                               </div>

                           </div>
                       </div>
                       <asp:HiddenField ID="hdnwedannDate" runat="server" />
                       <asp:HiddenField ID="hdndob" runat="server" />
                       <div class="form-group">
                           <div class="row">
                               <div class="col-md-1">
                                   Profile
                               </div>
                               <div class="col-md-3">
                                   <asp:DropDownList ID="ddlprofile" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
                               </div>
                               <div class="col-md-1">
                                   Date of Birth
                               </div>
                               <div class="col-md-3">
                                   <input id="txtbirthdate" type="date" class="form-control" runat="server" Style="font-size: small;" />
                               </div>
                               <div class="col-md-1">
                                   Wedding Anniversary
                               </div>
                               <div class="col-md-3">
                                   <input id="txtann" type="date" class="form-control" runat="server" Style="font-size: small;"/>
                               </div>

                           </div>
                       </div>

                       <div class="form-group">
                           <div class="row">
                               <div class="col-md-1">
                                   Martial Status
                               </div>
                               <div class="col-md-3">
                                   <asp:DropDownList ID="ddlmartial" runat="server" CssClass="form-control" Style="font-size: small;">
                                       <asp:ListItem Text="Select Martial Status" Value="" />
                                       <asp:ListItem Text="Married" Value="M" />
                                       <asp:ListItem Text="Single" Value="S" />
                                   </asp:DropDownList>
                               </div>
                               <div class="col-md-1">
                                   Hobbies/Interest
                               </div>
                               <div class="col-md-7">
                                   <asp:TextBox ID="txthobby" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                               </div>

                           </div>
                       </div>

                   </div>
               </div>
           </div>

           <div class="col-md-10 col-md-offset-1" style="margin-top: 5px !important;">
               <div class="panel panel-default" >
                   <div class="panel-heading" style="background-color: lightgray; font-size:large;">
                       <strong>Contact</strong>
                   </div>
                   <div class="panel-body" style="padding-bottom: 9px !important">
                       <div class="row">
                           <!-- First Sub-Panel -->
                           <div class="col-md-6">
                               <div class="panel panel-default">
                                   <div class="panel-heading" style="background-color: lightgray;">Contact Executive 1</div>
                                   <div class="panel-body" style="padding: 20px;">
                                       <div class="form-group">
                                           <div class="row">
                                               <div class="col-md-4">
                                                   Executive
                           
                                               </div>
                                               <div class="col-md-8">
                                                   <asp:DropDownList ID="ddlexe1" runat="server" CssClass="form-control" Style="font-size: small;" AutoPostBack="true" OnSelectedIndexChanged="ddlexe1_SelectedIndexChanged"></asp:DropDownList>
                                               </div>
                                           </div>
                                           <div class="row">
                                               <div class="col-md-4">
                                                   Rating
                                               </div>
                                               <div class="col-md-8">
                                                   <asp:DropDownList ID="ddlrating1" runat="server" CssClass="form-control" Style="font-size: small;">
                                                       <asp:ListItem Text="Select Executive Rating" Value="" />
                                                       <asp:ListItem Text="A" Value="A" />
                                                       <asp:ListItem Text="B" Value="B" />
                                                       <asp:ListItem Text="C" Value="C" />
                                                   </asp:DropDownList>
                                               </div>
                                           </div>
                                           <div class="row">
                                               <div class="col-md-4">
                                                   Remarks
                           
                                               </div>
                                               <div class="col-md-8">
                                                   <asp:TextBox ID="txtremarks1" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                                               </div>
                                           </div>
                                       </div>
                                   </div>
                               </div>
                           </div>
                           <!-- Second Sub-Panel -->
                           <div class="col-md-6">
                               <div class="panel panel-default">
                                   <div class="panel-heading" style="background-color: lightgray;">Contact Executive 2</div>
                                   <div class="panel-body" style="padding: 20px;">
                                       <div class="form-group row">
                                           <div class="row">
                                               <div class="col-md-4">
                                                   Executive
                           
                                               </div>
                                               <div class="col-md-8">
                                                   <asp:DropDownList ID="ddlexe2" runat="server" CssClass="form-control" Style="font-size: small;" AutoPostBack="true" OnSelectedIndexChanged="ddlexe2_SelectedIndexChanged"></asp:DropDownList>
                                               </div>
                                           </div>
                                           <div class="row">
                                               <div class="col-md-4">
                                                   Rating
                                               </div>
                                               <div class="col-md-8">
                                                   <asp:DropDownList ID="ddlrating2" runat="server" CssClass="form-control" Style="font-size: small;">
                                                       <asp:ListItem Text="Select Executive Rating" Value="" />
                                                       <asp:ListItem Text="A" Value="A" />
                                                       <asp:ListItem Text="B" Value="B" />
                                                       <asp:ListItem Text="C" Value="C" />
                                                   </asp:DropDownList>
                                               </div>
                                           </div>
                                           <div class="row">
                                               <div class="col-md-4">
                                                   Remarks
                           
                                               </div>
                                               <div class="col-md-8">
                                                   <asp:TextBox ID="txtremarks2" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                                                   
                                               </div>
                                           </div>
                                       </div>
                                   </div>
                               </div>
                           </div>

                       </div>
                   </div>
               </div>
           </div>

           <div class="col-md-10 col-md-offset-1" style="margin-top: 5px !important; ">
               <div class="panel panel-default" >
                   <div class="panel-heading" style="background-color: lightgray; font-size:large;">
                       <strong>Brand</strong>
                   </div>
                   <div class="panel-body" style="padding-bottom: 9px !important">

                       <div class="form-group">
                           <div class="row">
                               <div class="col-md-1">
                                   <asp:RadioButton ID="rbBrand" runat="server" GroupName="Selection" Text="Brand" AutoPostBack="true" OnCheckedChanged="rbBrand_CheckedChanged" />
                               </div>
                               <div class="col-md-1">
                                   <asp:RadioButton ID="rbClient" runat="server" GroupName="Selection" Text="Client" AutoPostBack="true" OnCheckedChanged="rbClient_CheckedChanged" />
                               </div>
                           </div>
                       </div>

                       <!-- Brand Dropdown -->
                       <div class="form-group" id="brandDropdown" style="display: none;">
                           <div class="row">
                               <div class="col-md-3">
                                   <asp:DropDownList ID="ddlbrand" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbrand_SelectedIndexChanged" Style="font-size: small;"></asp:DropDownList>
                               </div>
                           </div>
                       </div>

                       <!-- Client Dropdown -->
                       <div class="form-group" id="clientDropdown" style="display: none;">
                           <div class="row">
                               <div class="col-md-3">
                                   <asp:DropDownList ID="ddlClient" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" Style="font-size: small;"></asp:DropDownList>
                               </div>
                           </div>
                       </div>
                       <div class="form-group">
                           <div class="row">
                               <div class="col-md-3">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary"
                                        OnClick="btnAdd_Click" CausesValidation="false" />
                                </div>
                           </div>
                       </div>
                       <asp:HiddenField ID="hfEditIndex" runat="server" Value="-1" />
                       <asp:GridView ID="gvBrands" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                           <Columns>
                               <asp:TemplateField HeaderText="ID">
                                   <ItemTemplate>
                                       <asp:Label ID="lblBrandId" runat="server" Text='<%# Eval("Brand_Id") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Name">
                                   <ItemTemplate>
                                       <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Group" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGroup" runat="server" Text='<%# Eval("Grp") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%--<asp:BoundField DataField="Brand_Name" HeaderText="Brand Name" />--%>
                              <%-- <asp:TemplateField>
                                   <HeaderStyle Width="40px" />
                                   <ItemStyle Width="40px" />
                                   <HeaderTemplate>
                                       Edit
                                   </HeaderTemplate>
                                   <ItemTemplate>
                                       <center>
                                           <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/News-Edit.ico'
                                               ValidationGroup="a" />
                                       </center>
                                   </ItemTemplate>
                               </asp:TemplateField>--%>

                               <asp:TemplateField>
                                   <HeaderTemplate>
                                       Delete
                                   </HeaderTemplate>
                                   <HeaderStyle Width="40px" />
                                   <ItemStyle Width="40px" />
                                   <ItemTemplate>
                                       <center>
                                           <asp:ImageButton runat="server" ID="DelButton" ImageUrl="~/Content/images/Trash-Cancel.ico"
                                               CommandName="Delete" CommandArgument='<%# Eval("Brand_Id") %>' OnClick="DeleteButton_Click" ValidationGroup="a" />
                                       </center>
                                   </ItemTemplate>
                               </asp:TemplateField>
                           </Columns>
                       </asp:GridView>

                   </div>
               </div>
           </div>

           <div class="col-md-10 col-md-offset-1" style="margin-top: 10px !important; margin: 0 auto;">
                <div class="col-md-3 col-md-offset-2">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-dark" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
            <div class="col-md-10 col-md-offset-1" style="margin-top: 10px !important; margin: 0 auto;"></div>

           </div>

    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
