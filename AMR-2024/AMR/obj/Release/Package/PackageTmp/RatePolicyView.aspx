<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RatePolicyView.aspx.cs" Inherits="AMR.RatePolicyView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:UpdatePanel ID="up" runat="server">--%>
        <ContentTemplate>
            <style>
                    .click {
        background-color: darkgray;
        color: black;          
        border: none;          
        padding: 10px 20px;    
        font-size: 14px;       
        cursor: pointer;       
    }

    .click:hover {
        background-color: gray;
        color: black;  
    }
            </style>
            
            <div class="form-horizontal">
                <div class="col-md-10 col-md-offset-1">
                    <div class="form-group">
                        <div class="div-md-12 text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <h4>RATE POLICY</h4>
                    <div class="form-group">
                        <div class="div-md-12">
                            <asp:Button ID="btnview" runat="server" Text="ADD a new Record" CssClass="click" OnClick="btnview_Click" CausesValidation="false" />
                        </div>
                  </div>
                    <%--grid start--%>
                    <div class="form-group" style="width: 100%;">
                        <div class="div-md-12" style="width: 100%;">
                        </div>
                        <div class="div-md-12" style="margin-top: 50px; width: 100%;">
                            <asp:GridView ID="gv" PageSize="100" runat="server" CssClass="EU_DataTable" DataKeyNames="RatePolicyId" AutoGenerateColumns="false"
                                AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" Width="100%">
                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="PublicationName" HeaderText="Publication">
                                        <HeaderStyle Width="40%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CategoryTitle" HeaderText="Main Category">
                                        <HeaderStyle Width="40%" />
                                    </asp:BoundField>
                                    

                                    <%--<asp:TemplateField>
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
                                            Edit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:HyperLink ID="hypEdit" runat="server" Text = "Edit" NavigateUrl = '<%# "RatePolicyform.aspx?RatePolicyId=" + Convert.ToString(DataBinder.Eval(Container.DataItem,"RatePolicyId")) + "&Mode=Edit" %>' ></asp:HyperLink>
                                        </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <%--grid end--%>
                </div>
            </div>
        </ContentTemplate>
   <%-- </asp:UpdatePanel>--%>
</asp:Content>
