<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Brandform.aspx.cs" Inherits="AMR.Brandform" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
         <script type="text/javascript">
             function searchClients() {
                 var query = $('#<%= txtClient.ClientID %>').val();
                 if (query.length > 0) {
                     $.ajax({
                         type: "POST",
                         url: "Brandform.aspx/SearchClients",
                         data: JSON.stringify({ searchText: query }),
                         contentType: "application/json; charset=utf-8",
                         dataType: "json", 
                         success: function (response) {
                             var results = response.d;
                             $('#clientResults').empty();
                             if (results.length > 0) {
                                 $.each(results, function (i, item) {
                                     $('#clientResults').append('<div onclick="selectClient(\'' + item.Id + '\', \'' + item.Client_Name + '\')">' + item.Client_Name + '</div>');
                                 });
                                 $('#clientResults').show();
                             } else {
                                 $('#clientResults').hide();
                             }
                         }
                     });
                 } else {
                     $('#clientResults').hide();
                 }
             }

             function selectClient(id, name) {
                 $('#<%= txtClient.ClientID %>').val(name);
                 $('#<%= hiddenClientId.ClientID %>').val(id);

                 console.log('Hidden Client ID:', $('#<%= hiddenClientId.ClientID %>').val());
                 $('#clientResults').hide();

    

             }

            function populateClient(clientId) {
                // Fetch and set the client name in txtClient
                $.ajax({
                    type: "POST",
                    url: "Brandform.aspx/PopulateClientDetails",
                    data: JSON.stringify({ clientId: clientId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(response);
                        $('#<%= txtClient.ClientID %>').val(response.d.ClientName);
                        $('#<%= hiddenClientId.ClientID %>').val(clientId);


                    },
                    error: function (error) {
                        console.log("Error fetching client city:", error);
                    }
                });


            }
         </script>
       <%-- <asp:UpdateProgress ID="pu" runat="server">
            <ProgressTemplate>
                <div class="dialog-background">
                    <div class="dialog-loading-wrapper">
                        <img src="Content/Images/loading6.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>

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
                        <div class="col-md-1">
                            Brand Name
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Client
                        </div>
                        <div class="col-md-3">
                            <%--<asp:DropDownList ID="ddlclient" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged"></asp:DropDownList>--%>
                            <asp:TextBox ID="txtClient" runat="server" onkeyup="searchClients()" CssClass="form-control" />
                            <div id="clientResults" style="display:none;"></div>
                            <asp:HiddenField ID="hiddenClientId" runat="server" EnableViewState="true" />
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row"> 
                        <div class="col-md-1">
                            Client Executive
                        </div>
                        <div class="col-md-3">
                              <asp:DropDownList ID="ddlexe" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Status
                        </div>
                        <div class="col-md-3">
                             <asp:CheckBox ID="chstatus" runat="server" />
                        </div>
                    </div>
                </div>

                <%--<div class="form-group">
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
                </div>--%>

                <%--<div class="form-group">
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
                </div>--%>
                <br />
                 <%--grid start--%>
                 <div class="form-group">
                    <div class="div-md-12">
                        <div class="col-md-3 col-md-offset-2">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                     <br />
                    <div class="class="div-md-12" style="margin-top: 50px;"">
                        <div class="col-md-1">
                            Brand Name
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtbrandfilter" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtbrandfilter" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-1">
                            <asp:Button ID="btnfilter" runat="server" ValidationGroup="c" CssClass="btn btn-success" Text="Search" OnClick="btnfilter_Click" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="div-md-12" style="margin-top: 50px;">
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField DataField="Brand_Name" HeaderText="Brand Name"></asp:BoundField>
                                <asp:BoundField DataField="Client_Name" HeaderText="Client Name"></asp:BoundField>
                                
                                 <asp:BoundField DataField="StatusDes" HeaderText="Status"></asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle Width="40px" />
                                    <HeaderTemplate>
                                        Edit
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <center>
                                            <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/News-Edit.ico'
                                                ValidationGroup="a"  OnClick="EditButton_Click"/>
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
                                            <asp:ImageButton runat="server" ID="DelButton" ImageUrl="~/Content/images/Trash-Cancel.ico"
                                            CommandName="Delete" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteButton_Click" ValidationGroup="a"/>
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
