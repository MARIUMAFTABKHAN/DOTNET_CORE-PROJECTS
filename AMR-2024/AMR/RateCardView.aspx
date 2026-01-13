<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RateCardView.aspx.cs" Inherits="AMR.RateCardView" %>
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
     .center-align {
        text-align: center;
    }
            </style>
            <%--<asp:UpdateProgress ID="pu" runat="server">
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
                    <h4>RATE CARD</h4>
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
                            <asp:GridView ID="gv" PageSize="100" runat="server" CssClass="EU_DataTable" DataKeyNames="RateCardId" AutoGenerateColumns="false"
                                AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" Width="100%">
                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="PublicationName" HeaderText="Publication">
                                        <HeaderStyle Width="25%" CssClass="center-align"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CategoryTitle" HeaderText="Main Category">
                                        <HeaderStyle Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EffectiveFrom" HeaderText="EffectiveFrom Date">
                                        <HeaderStyle Width="25%" />
                                    </asp:BoundField>
                                    
                                     <asp:TemplateField>
                                         <HeaderTemplate>
                                            Edit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:HyperLink ID="hypEdit" runat="server" Text = "Edit" NavigateUrl = '<%# "RateCardform.aspx?RateCardId=" + Convert.ToString(DataBinder.Eval(Container.DataItem,"RateCardId")) + "&Mode=Edit" %>' ></asp:HyperLink>
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
