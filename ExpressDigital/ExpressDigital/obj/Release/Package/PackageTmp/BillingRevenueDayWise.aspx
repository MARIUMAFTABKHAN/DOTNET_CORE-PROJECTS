<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BillingRevenueDayWise.aspx.cs" Inherits="ExpressDigital.BillingRevenueDayWise" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                function EndRequestHandler(sender, args) {
                    if (args.get_error() != undefined) {
                        args.set_errorHandled(true);
                    }
                }
            </script>
            <script type="text/javascript">
                function pageLoad() {
                    applyDatePicker();

                }
                function applyDatePicker() {

                    $("#ContentPlaceHolder1_txtDateFrom").datepicker({
                        showOn: 'button',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        buttonImage: 'Content/Images/Calender.png',
                        dateFormat: 'dd/mm/yy'
                    });

                    $("#ContentPlaceHolder1_txtDateTo").datepicker({
                        showOn: 'button',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        buttonImage: 'Content/Images/Calender.png',
                        dateFormat: 'dd/mm/yy'
                    });

                }
            </script>
            <style type="text/css">
                .chk label {
                    margin-left: 10px !important;
                }
            </style>
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
                            <h4>Billing Revenue Company Wise
                            </h4>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-1">
                                From:
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtDateFrom" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtDateFrom" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-1">
                                To:
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtDateTo" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtDateTo" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <%--<div class="col-md-4">
                            </div>--%>
                            <div class="col-md-2" style="margin-left:150px;">
                                <asp:Button ID="btnSearch" runat="server" Text="Show Report" CssClass="btn btn-success" Style="width: 100%; min-width: 100%" ValidationGroup="FillReport" OnClick="btnSearch_Click" />
                            </div>
                            <div class="col-md-2" style="margin-left:150px;">
                                <asp:Button ID="btnexport" runat="server" Text="Export to Excel" CssClass="btn btn-success" Style="width: 100%; min-width: 100%" ValidationGroup="FillReport" OnClick="btnexport_Click" />
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
