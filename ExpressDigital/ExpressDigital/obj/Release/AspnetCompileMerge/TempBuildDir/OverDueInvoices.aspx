<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OverDueInvoices.aspx.cs" Inherits="ExpressDigital.OverDueInvoices" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <%--   <script src="Scripts/jquery-ui.js"></script>
            <link href="Scripts/jquery-ui.css" rel="stylesheet" />--%>
            <script type="text/javascript" language="javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                function EndRequestHandler(sender, args) {
                    if (args.get_error() != undefined) {
                        //  **alert(args.get_error().message.substr(args.get_error().name.length + 2));
                        args.set_errorHandled(true);
                    }
                }
            </script>
            <script type="text/javascript">
                function pageLoad() {
                    applyDatePicker();

                }
                function applyDatePicker() {


                    $("#ContentPlaceHolder1_txtSearchROMODateFrom").datepicker({
                        showOn: 'button',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        buttonImage: 'Content/Images/Calender.png',
                        dateFormat: 'dd/mm/yy'
                    });

                    $("#ContentPlaceHolder1_txtSearchROMODateTo").datepicker({
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
            <div class="col-md-10 col-md-offset-1">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div>
                    <h4>&nbsp;&nbsp;&nbsp;&nbsp; Over Due Invoices
                    </h4>
                </div>

            </div>
            <div class="col-md-10 col-md-offset-1">
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 5px !important">
                        <div class="col-md-2">
                            Company 
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Status
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control">
                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Settled</asp:ListItem>
                                <asp:ListItem Value="2">Un-Settled</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 5px !important">
                        <div class="col-md-2">
                            City
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCity" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Master Agency
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlmasteragency" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlmasteragency_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 5px !important">
                        <div class="col-md-2">
                            Agency
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlAgency" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Client
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlClient" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">
                        <div class="col-md-2">
                            Date From
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateFrom" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="padding-top: 2px;">
                            Date To
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateTo" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group" style="display: none">
                    <div class="col-md-12" style="margin-top: 3px !important">
                        <div class="col-md-2">
                            Governament
                        </div>
                        <div class="col-md-1">
                            <asp:DropDownList ID="ddlisGovt" runat="server" CssClass="form-control">
                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">

                        <div class="col-md-1 col-md-offset-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" Style="width: 75px !important" ValidationGroup="s" OnClick="btnSearch_Click" />
                        </div>
                        <%--<div class="col-md-1" style="margin-left: 20px !important">
                            <asp:Button ID="btnExecute" runat="server" Text="Genereate" CssClass="btn btn-danger" Style="width: 90px !important; margin-left: -25px" ValidationGroup="s" OnClick="btnExecute_Click" />
                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" ConfirmText="Are you sure to execute records ?"
                                TargetControlID="btnExecute" runat="server" />
                        </div>--%>

                        <div class="col-md-1" style="margin-left: 50px !important">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" Style="width: 75px !important; margin-left: -40px" ValidationGroup="s" OnClick="btnCancel_Click" />

                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-10 col-md-offset-1">

                <div class="col-md-12" style="margin-top: 4px !important; height: 600px">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%"></rsweb:ReportViewer>
                     
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
