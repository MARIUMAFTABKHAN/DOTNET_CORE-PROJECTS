<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MainCategoryRates.aspx.cs" Inherits="AMR.MainCategoryRates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
         <script type="text/javascript">
             function RemoveRecord(val) {
                 bootbox.confirm("Are you sure to delete ?", function (result) {
                     var link = "MainCategoryRates.aspx";
                     if (result) {
                         $.ajax({
                             type: "POST",
                             url: "MainCategoryRates.aspx/OnSubmit",
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
                <h4>Main Category Rates</h4>
                <br />
                <div class="form-group">
                    <div class="div-md-12"> 
                        <div class="col-md-1">
                            ID
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtid" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtid" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Category Title
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtcat" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtcat" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>

                   </div>
                </div>
              <%--  <br />--%>
                <div class="form-group">
                    <div class="div-md-12"> 
                        <%--<div class="col-md-2">
                            Category Title
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtcat" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtcat" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>--%>
                   </div>
                </div>
               <%-- <br />--%>
                <div class="form-group">
                    <div class="div-md-12"> 
                        <div class="col-md-1">
                            Status
                        </div>
                        <div class="col-md-1">
                            <asp:CheckBox ID="chstatus" runat="server" />
                        </div>
                        <div class="col-md-1">
                            AMR
                        </div>
                        <div class="col-md-1">
                            <asp:CheckBox ID="chamr" runat="server" />
                        </div>
                   </div>
                </div>
                <br />
                <div class="form-group">
                    <div class="div-md-12"> 
                        <%--<div class="col-md-2">
                            AMR
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chamr" runat="server" />
                        </div>--%>
                   </div>
                </div>
                <br />
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
                                <asp:BoundField DataField="ID" HeaderText="ID"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="Category" HeaderText="Category"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Status"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="AMR" HeaderText="AMR"><HeaderStyle Width="15%" /></asp:BoundField>

                                <asp:TemplateField>
                                    <HeaderStyle Width="10px" />
                                    <ItemStyle Width="10px" />
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
                                    <HeaderStyle Width="10px" />
                                    <ItemStyle Width="10px" />
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
