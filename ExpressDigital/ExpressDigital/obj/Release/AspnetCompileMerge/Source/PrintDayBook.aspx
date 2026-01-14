<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PrintDayBook.aspx.cs" Inherits="ExpressDigital.PrintDayBook" %>

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
                    <h4>&nbsp;&nbsp;&nbsp;&nbsp; Day Book
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
                            City
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlcity" AutoPostBack="true" OnSelectedIndexChanged="ddlcity_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSearchROMODateFrom" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-2" style="padding-top: 2px;">
                            Date To
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateTo" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchROMODateTo" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">

                        <div class="col-md-1 col-md-offset-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Show Report " CssClass="btn btn-success" Style="width: 50%; min-width: 50%" ValidationGroup="s" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-md-1" style="margin-left: 2px !important">
                            <asp:Button ID="btnexport" runat="server" Text="Export To Excel " CssClass="btn btn-success" Style="width: 120%; min-width: 120%" ValidationGroup="s" OnClick="btnexport_Click" />
                        </div>
                        <div class="col-md-2 col-md-offset-1">
                            Status
                        </div>
                        <div class="col-md-2" style="padding-top: 2px;">
                            <asp:DropDownList ID="ddlstatis" runat="server" CssClass="form-control">
                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Invoice</asp:ListItem>
                                <asp:ListItem Value="2">Release Order</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-10 col-md-offset-1">
                <div class="col-md-12" style="margin-top: 3px !important; height: 600px; width: 100%;">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>
                </div>
            </div>
        </ContentTemplate>
           <Triggers>
  <asp:PostBackTrigger ControlID="btnExport" />
   </Triggers>
    </asp:UpdatePanel>
</asp:Content>
