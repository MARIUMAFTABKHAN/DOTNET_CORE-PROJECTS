<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgencyBrandView.aspx.cs" Inherits="AMR.AgencyBrandView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
         <script type="text/javascript">
             function searchBrands() {
                 var query = $('#<%= txtBrand.ClientID %>').val();

                 if (query.length > 0) {
                     $.ajax({
                         type: "POST",
                         url: "AgencyBrandView.aspx/SearchBrands", // Update with your page's path
                         data: JSON.stringify({ searchText: query }),
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         success: function (response) {
                             var results = response.d;
                             $('#brandResults').empty();
                             if (results.length > 0) {
                                 $.each(results, function (i, item) {
                                     $('#brandResults').append('<div onclick="selectBrand(\'' + item.Id + '\', \'' + item.Brand_Name + '\')">' + item.Brand_Name + '</div>');
                                 });
                                 $('#brandResults').show();
                             } else {
                                 $('#brandResults').hide();
                             }
                         }
                     });
                 } else {
                     $('#brandResults').hide();
                 }
             }

             function selectBrand(id, name) {
                 $('#<%= txtBrand.ClientID %>').val(name);
                 $('#<%= hiddenBrandId.ClientID %>').val(id);

                 console.log('Hidden Client ID:', $('#<%= hiddenBrandId.ClientID %>').val());

                 $('#brandResults').hide();
             }
             function populatefetchbrand(clientId) {
                 $.ajax({
                     type: "POST",
                     url: "AgencyBrandView.aspx/PopulateBrandDetails",
                     data: JSON.stringify({ clientId: clientId }),
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (response) {
                         console.log(response);
                         $('#<%= txtBrand.ClientID %>').val(response.d.BrandName);
                        $('#<%= hiddenBrandId.ClientID %>').val(clientId);


                     },
                    error: function (error) {
                        console.log("Error fetching client city:", error);
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
                <h4>AGENCY BRAND</h4>
                <br />

                <br />
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Agency
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlagency" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlagency_SelectedIndexChanged" Style="font-size: small;"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Brand
                        </div>
                        <div class="col-md-3">
                            <%--<asp:DropDownList ID="ddlbrand" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlbrand_SelectedIndexChanged" Style="font-size: small;"></asp:DropDownList>--%>
                            <asp:TextBox ID="txtBrand" runat="server" onkeyup="searchBrands()" CssClass="form-control" />
                            <div id="brandResults" style="display:none;"></div>
                            <asp:HiddenField ID="hiddenBrandId" runat="server" EnableViewState="true" />
                        </div>
                    </div>
                </div>
                
                 <div class="form-group">
                    <div class="row"> 
                        <div class="col-md-1">
                            Agency Executive
                        </div>
                        <div class="col-md-3">
                       <asp:DropDownList ID="ddlagencyexe" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlagencyexe_SelectedIndexChanged" Style="font-size: small;"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <%--grid start--%>
                <div class="form-group">
                    <div class="div-md-12">
                        <div class="col-md-3 col-md-offset-2">
                           <asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success" Text="Update" OnClick="btnupdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                    <div class="div-md-12" style="margin-top: 50px;">
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="Agency_Name" HeaderText="Agency Name">
                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="Brand_Name" HeaderText="Brand Name">
                                   
                                </asp:BoundField>
                                <asp:BoundField DataField="Executive_Name" HeaderText="Executive Name">
                                  
                                </asp:BoundField>

                                <asp:TemplateField>
                                    <HeaderStyle Width="10px" />
                                    <ItemStyle Width="10px" />
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
                                <%--<asp:TemplateField>
                                    <HeaderTemplate>
                                        Delete
                                    </HeaderTemplate>
                                    <HeaderStyle Width="10px" />
                                    <ItemStyle Width="10px" />
                                    <ItemTemplate>
                                        <center>
                                            <asp:ImageButton runat="server" ID="DelButton" ImageUrl="~/Content/images/Trash-Cancel.ico"
                                                CommandName="Delete" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteButton_Click" ValidationGroup="a" />
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
