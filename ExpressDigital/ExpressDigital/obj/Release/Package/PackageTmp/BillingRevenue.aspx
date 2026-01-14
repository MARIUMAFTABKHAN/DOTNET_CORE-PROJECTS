<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BillingRevenue.aspx.cs" Inherits="ExpressDigital.BillingRevenue" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
    </script>
    <style type="text/css">
        .chk label {
            margin-left: 10px !important;
        }
    </style>
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

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
                <div style="width: 95% !important; margin: 0px auto">
                    <div class="form-group">
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group text-left">
                        <div class="col-md-12">
                            <h4>Billing Revenue
                            </h4>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-1">
                                Company 
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-1">
                                Fiscal Year
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                     <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-1">
                                Quarters
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlQuater" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="-- All --" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Q1 (July 1 – September 30)" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Q2 (October 1 – December 31)" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Q3 (January 1 – March 31)" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Q4 (April 1 – June 30)" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" Text="Show Report" CssClass="btn btn-success" Style="width: 100%; min-width: 100%" ValidationGroup="FillReport" OnClick="btnSearch_Click" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnexport" runat="server" Text="Export To Excel" CssClass="btn btn-success" Style="width: 100%; min-width: 100%" ValidationGroup="FillReport" OnClick="btnexport_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-10">
                    <div class="col-md-12" style="height: 100%; width: 100%;">
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>
                    </div>
                </div>
        </ContentTemplate>
          <Triggers>
         <asp:PostBackTrigger ControlID="btnExport" />
          </Triggers>
    </asp:UpdatePanel>
</asp:Content>
