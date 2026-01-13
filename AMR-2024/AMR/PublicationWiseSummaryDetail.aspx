<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PublicationWiseSummaryDetail.aspx.cs" Inherits="AMR.PublicationWiseSummaryDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
<ContentTemplate>
   <style>
       .name-label {
    font-size: 18px;
    font-weight: bold;
    margin-bottom: 20px;
    display: block;
}

   </style>

                <asp:Label ID="lbldate" runat="server" Style="font-size:15px;font-weight:bold;display:block;"></asp:Label>

                <asp:Label ID="lblpub" runat="server" Style="font-size:15px;font-weight:bold;display:block;"></asp:Label>

        <br />

     
    <asp:Label ID="lblClientName" runat="server" CssClass="name-label"></asp:Label>
    <asp:Label ID="lblTypename" runat="server" CssClass="name-label"></asp:Label>
    
                    <div class="form-group">
                    <div class="div-md-12 text-center">
                        <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
   
      <%--grid start--%>
    <div class="form-horizontal">
        <div class="col-md-10 col-md-offset-1">
            <div class="form-group" style="width: 100%;">
                <div class="div-md-12" style="width: 100%;">
                </div>
                <div class="div-md-12" style="margin-top: 50px; width: 100%;">
                    <asp:GridView ID="gv" PageSize="100" runat="server" CssClass="EU_DataTable" AutoGenerateColumns="true" Width="100%" EnableViewState="false" OnRowDataBound="gv_RowDataBound">
             
                        <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />


                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>
    
    <div class="form-horizontal">
        <div class="col-md-10 col-md-offset-1">
            <div class="form-group" style="width: 100%;">
                <div class="div-md-12" style="width: 100%;">
                </div>
                <div class="div-md-12" style="margin-top: 50px; width: 100%;">
                    <asp:GridView ID="gventry" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="Id" AutoGenerateColumns="false">
                        <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <%--<asp:BoundField DataField="Id" HeaderText="ID" />--%>
                            <asp:BoundField DataField="Publication_Date" HeaderText="Publication Date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="City" HeaderText="City Edition" />
                            <asp:BoundField DataField="Pub" HeaderText="Publication" />
                            <asp:BoundField DataField="MainCategory" HeaderText="Main Category" />
                            <asp:BoundField DataField="SubCategory" HeaderText="Sub Category" />
                            <asp:BoundField DataField="Client" HeaderText="Client Company" />
                            <asp:BoundField DataField="CM" HeaderText="Size CM" />
                            <asp:BoundField DataField="COL" HeaderText="Column Size" />
                            <asp:BoundField DataField="RO" HeaderText="RO Number" />
                            <asp:BoundField DataField="Page" HeaderText="Page No" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink
                                        ID="hypEdit"
                                        runat="server"
                                        Text="Edit"
                                        NavigateUrl='<%# "AdEntry.aspx?Id=" + Convert.ToString(DataBinder.Eval(Container.DataItem,"Id")) + "&Mode=Edit" %>'
                                        Target="_blank">
                                    </asp:HyperLink>

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
                                            CommandName="Delete" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteButton_Click" ValidationGroup="a" />
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
