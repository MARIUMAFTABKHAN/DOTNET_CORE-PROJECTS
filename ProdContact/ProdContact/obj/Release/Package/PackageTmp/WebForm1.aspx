<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="EPOMS.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
         <script type="text/javascript">
             function RemoveRecord(val) {
                 bootbox.confirm("Are you sure to delete ?", function (result) {
                     var link = "Brand.aspx";
                     if (result) {
                         $.ajax({
                             type: "POST",
                             url: "Brand.aspx/OnSubmit",
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
                <h4>Brands</h4>
                <br />
                <div class="form-group">
                    <div class="row"> 
                        <div class="col-md-2">
                            ID
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtid" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtid" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-2">
                            Brand Name
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtname" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row"> 
                        <div class="col-md-2">
                            Company / Client
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlclient" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Client Executive
                        </div>
                        <div class="col-md-3">
                              <asp:DropDownList ID="ddlexe" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row"> 
                        <div class="col-md-2">
                            Media Buying House
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlmedia" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Agency
                        </div>
                        <div class="col-md-3">
                             <asp:TextBox ID="txtagency" runat="server" CssClass="form-control"></asp:TextBox>
                             <asp:RequiredFieldValidator ControlToValidate="txtagency" ForeColor="Red" Display="Dynamic"
                             ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row"> 
                        <div class="col-md-2">
                            Agency Executive
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtagencyexe" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtagencyexe" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-2">
                            Status
                        </div>
                        <div class="col-md-3">
                             <asp:CheckBox ID="chstatus" runat="server" />
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
                                <asp:BoundField DataField="BrandName" HeaderText="Brand Name"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="Company/Client" HeaderText="Company/Client"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="ClientExecutive" HeaderText="Client Executive"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="MediaBuyingHouse" HeaderText="Media Buying House"></asp:BoundField>
                                <asp:BoundField DataField="Agency"  HeaderText="Agency"></asp:BoundField>
                                <asp:BoundField DataField="AgencyExecutive" HeaderText="Agency Executive"></asp:BoundField>
                                 <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
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
