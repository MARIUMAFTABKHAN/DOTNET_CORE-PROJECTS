<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="AMR.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="up" runat="server">
     <ContentTemplate>
         <script type="text/javascript">

             function RemoveRecord(val) {
                 bootbox.confirm("Are you sure to delete ?", function (result) {
                     var link = "Users.aspx";
                     if (result) {
                         $.ajax({
                             type: "POST",
                             url: "Users.aspx/OnSubmit",
                             data: JSON.stringify({ id: val }),
                             contentType: 'application/json; charset=utf-8',
                             dataType: 'json',
                             error: function (XMLHttpRequest, textStatus, errorThrown) {
                                 bootbox.alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown + "")
                             },
                             success: function (result) {
                                 if (result.d == "Ok") {
                                     document.location.href = link;
                                 }
                                 else {
                                     bootbox.alert(result.d, function () {
                                     });
                                 }
                             }
                         });
                     }
                 });
             }
         </script>
         <asp:UpdateProgress ID="pu" runat="server">
             <ProgressTemplate>
                 <div class="dialog-background">
                     <div class="dialog-loading-wrapper">
                         <img src="Content/Images/loading6.gif" />
                     </div>
                 </div>
             </ProgressTemplate>
         </asp:UpdateProgress>

         <div class="form-horizontal">
             <div class="col-md-10 col-md-offset-1">
                 <div class="form-group">
                     <div class="div-md-12 text-center">
                         <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                     </div>
                 </div>
                 <h4>User Management</h4>
                 <div class="form-group">
                     <div class="div-md-12"> 
                         <div class="col-md-2">
                             User Code
                         </div>
                         <div class="col-md-4">
                             <asp:TextBox ID="txtusercode" runat="server" CssClass="form-control"></asp:TextBox>
                             <asp:RequiredFieldValidator ControlToValidate="txtusercode" ForeColor="Red" Display="Dynamic"
                                 ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                         </div>
                     </div>
                 </div>
                 <div class="form-group">
                    <div class="div-md-12">
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
                    <div class="div-md-12"> 
                        <div class="col-md-2">
                            User Name
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtusername" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtusername" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="div-md-12"> 
                        <div class="col-md-2">
                            Designation
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtdesg" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtdesg" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                 <div class="form-group">
                    <div class="div-md-12">
                        <div class="col-md-2">
                            User Group
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlgroup" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                Department
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                  </div>
                  <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                Edition
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddledition" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                  </div>
                 <div class="form-group">
                    <div class="div-md-12">
                        <div class="col-md-2">
                            Traffic/Finance
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chtrfin" runat="server" />
                        </div>
                    </div>
                </div>

                 <%--grid start--%>
                 <div class="form-group">
                    <div class="div-md-12">
                        <div class="col-md-3 col-md-offset-2">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                    <div class="div-md-12" style="margin-top: 50px;">
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField DataField="UserCode" HeaderText="User Code"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="UserName" HeaderText="User Name"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="UserGroup" HeaderText="User Group"></asp:BoundField>
                                <asp:BoundField DataField="Department" HeaderText="Department"></asp:BoundField>
                                <asp:BoundField DataField="Edition" HeaderText="Edition"></asp:BoundField>
                                <asp:BoundField DataField="Traffic/Finance" HeaderText="Traffic/Finance"></asp:BoundField>
                                <asp:TemplateField>
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
                                                ValidationGroup="a"  OnClientClick='<%# "RemoveRecord(" +Eval("ID") + " );" %>'
                                                />
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                 <%--grid end--%>
              </div>
         </div>
     </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
