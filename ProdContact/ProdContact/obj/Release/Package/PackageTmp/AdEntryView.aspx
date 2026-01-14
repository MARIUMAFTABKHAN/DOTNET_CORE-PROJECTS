<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdEntryView.aspx.cs" Inherits="AMR.AdEntryView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

        <style>
    .checkbox-scroll-wrapper {
        border: 1px solid #ccc;
        padding: 8px 10px;
        height: 100px; /* Set your desired height */
        overflow-y: auto;
        background-color: #fff;
        border-radius: 4px;
    }


</style>


        <script type="text/javascript">
            function searchClients() {
                var query = $('#<%= txtClient.ClientID %>').val();
                if (query.length > 0) {
                    $.ajax({
                        type: "POST",
                        url: "AdEntryView.aspx/SearchClients",
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
                <h4>ADVERTISMENT REPORT</h4>
                <br />

                <br />
                 <div class="form-group">
                     <div class="row">
                         <div class="col-md-1">
                             Insertion Date From
                         </div>
                         <div class="col-md-3">
                             <input id="txtfromdate" type="date" class="form-control" runat="server" style="font-size: small;" />
                         </div>
                         <div class="col-md-1"></div>
                         <div class="col-md-1">
                              To
                         </div>
                         <div class="col-md-3">
                             <input id="txttodate" type="date" class="form-control" runat="server" style="font-size: small;" />
                         </div>
                     </div>
                 </div>
                <br />
                <div class="form-group">
                     <div class="row">
                         <div class="col-md-1">
                             Publication Date To 
                         </div>
                         <div class="col-md-3">
                             <input id="txtpubdate" type="date" class="form-control" runat="server" style="font-size: small;" />
                         </div>
                         <div class="col-md-1"></div>
                         <div class="col-md-1">
                             From 
                         </div>
                         <div class="col-md-3">
                             <input id="txtpubdatefrom" type="date" class="form-control" runat="server" style="font-size: small;" />
                         </div>
                     </div>
               </div>
                <br />
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Main Cat
                        </div>
                        <div class="col-md-3">
                            <%--<asp:DropDownList ID="ddlmaincat" runat="server" CssClass="form-control" Style="font-size: small;" AutoPostBack="True" OnSelectedIndexChanged="ddlmaincat_SelectedIndexChanged1"></asp:DropDownList>--%>
                            <asp:CheckBox ID="chkselectallMain" runat="server" AutoPostBack="true" Text="Select All" OnCheckedChanged="chkselectallMain_CheckedChanged" />
                            <asp:CheckBoxList ID="chkmaincat" runat="server" CssClass="checkbox-list-style" RepeatColumns="1"
                                 OnSelectedIndexChanged="chkMainCat_SelectedIndexChanged" AutoPostBack="true"/>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Sub Cat
                        </div>
                        <div class="col-md-3">
                            <%--<asp:DropDownList ID="ddlsubcat" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>--%>
                            <div class="checkbox-scroll-wrapper">
                                <asp:CheckBox ID="chkselectallsub" runat="server" AutoPostBack="true" Text="Select All" OnCheckedChanged="chkselectallsub_CheckedChanged" />
                                    <asp:CheckBoxList ID="chksubcat" runat="server"
                                        RepeatColumns="2" RepeatDirection="Horizontal" />
                                
                            </div>
                        </div>
                    </div>
                </div>
                <br />
               <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            City Edition
                        </div>
                        <div class="col-md-3">
                            <div class="checkbox-scroll-wrapper">
                                <asp:CheckBox ID="chkSelectAllCity" runat="server" Text="Select All" AutoPostBack="true" OnCheckedChanged="chkSelectAllCity_CheckedChanged" />
                                <asp:CheckBoxList ID="chkCity" runat="server"  RepeatColumns="6" RepeatDirection="Horizontal" />
                            </div>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Publication
                        </div>
                        <div class="col-md-3">
                            <div class="checkbox-scroll-wrapper">
                                <asp:CheckBox ID="chkSelectAllPub" runat="server" AutoPostBack="true" Text="Select All" OnCheckedChanged="chkSelectAllPub_CheckedChanged" />
                                <asp:CheckBoxList ID="chkPub" runat="server"  RepeatColumns="5" RepeatDirection="Horizontal" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="form-group">
                     <div class="row">
                         <div class="col-md-1">
                             Client Company
                         </div>
                         <div class="col-md-3">
                             
                             <asp:TextBox ID="txtClient" runat="server" onkeyup="searchClients()" CssClass="form-control" />
                             <div id="clientResults" style="display:none;"></div>
                             <asp:HiddenField ID="hiddenClientId" runat="server" EnableViewState="true" />
                             
                         </div>
                         <div class="col-md-1"></div>
                         <div class="col-md-1">
                             RO Number
                         </div>
                         <div class="col-md-3">
                            <asp:TextBox ID="txtro" runat="server" CssClass="form-control"></asp:TextBox>
                         </div>
                     </div>
                 </div>
       
                <br />
                <div class="form-group">
                     <div class="row">
                         <div class="col-md-1">
                             Page No
                         </div>
                         <div class="col-md-3">
                             <asp:TextBox ID="txtpage" runat="server" CssClass="form-control"></asp:TextBox>
                         </div>
                     </div>
                 </div>

                <br />
                <%--grid start--%>
                <div class="form-group">
                    <div class="div-md-12">
                        <div class="col-md-3 col-md-offset-2">
                           <asp:Button ID="btnfilter" runat="server" CssClass="btn btn-success" Text="Search" OnClick="btnfilter_Click"/>
                           <%--<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success" Text="Update" OnClick="btnupdate_Click"/>--%>
                           <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                    <div class="div-md-12" style="margin-top: 50px;">
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" ShowFooter="true"
                            OnRowDataBound="gv_RowDataBound">
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                               <%--<asp:BoundField DataField="Id" HeaderText="ID" />--%>
                                <asp:BoundField DataField="Publication_Date" HeaderText="Publication Date"  DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:BoundField DataField="City" HeaderText="City Edition" />
                                <asp:BoundField DataField="Publication" HeaderText="Publication" />
                                <asp:BoundField DataField="MainCategory" HeaderText="Main Category" />
                                <asp:BoundField DataField="SubCategory" HeaderText="Sub Category" />
                                <asp:BoundField DataField="Client" HeaderText="Client Company" />
                                <asp:BoundField DataField="Size_CM" HeaderText="Size CM" />
                                <asp:BoundField DataField="Col_Size" HeaderText="Column Size" />
                                <%--<asp:BoundField DataField="CM" HeaderText="CM" />--%>

                                <asp:TemplateField HeaderText="CM">
                                    <ItemTemplate>
                                        <%# Eval("CM") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalCM" runat="server" Font-Bold="true" ForeColor="DarkGreen" />
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="RO" HeaderText="RO Number" />
                                <asp:BoundField DataField="Page" HeaderText="Page No" />
                                 <asp:TemplateField>
                                     <HeaderTemplate>
                                        Edit
                                    </HeaderTemplate>
                                    <HeaderStyle Width="40px" />
                                    
                                    <ItemTemplate>
                                    <asp:HyperLink ID="hypEdit" runat="server" Text = "Edit" NavigateUrl = '<%# "AdEntry.aspx?Id=" + Convert.ToString(DataBinder.Eval(Container.DataItem,"Id")) + "&Mode=Edit" %>' ></asp:HyperLink>
                                    </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
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
