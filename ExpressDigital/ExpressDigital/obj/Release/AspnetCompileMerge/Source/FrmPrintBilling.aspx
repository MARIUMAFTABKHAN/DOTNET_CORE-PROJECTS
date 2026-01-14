<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FrmPrintBilling.aspx.cs" Inherits="ExpressDigital.FrmPrintBilling" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <%--  <script src="Scripts/jquery-ui.js"></script>
            <link href="Scripts/jquery-ui.css" rel="stylesheet" />--%>

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
            <div class="col-md-10 col-md-offset-1">
                <div class="div-md-12 text-center">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div>
                    <h4>&nbsp;&nbsp;&nbsp;&nbsp; Print Ledger
                    </h4>
                </div>
            </div>
            <div class="col-md-10 col-md-offset-1">

                <div class="form-group">
                    <div class="col-md-12" style="margin-bottom: 3px !important">
                        <div class="col-md-2">
                            Company
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Group Agency
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlMasteragency" AutoPostBack="true" OnSelectedIndexChanged="ddlMasteragency_SelectedIndexChanged" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 0px !important">
                         <div class="col-md-2">
                            Agency
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlAgency" AutoPostBack="true"  OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                           Client
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlclient"  runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged">
                            </asp:DropDownList>
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
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">
                        <div class="col-md-1" style="margin-left: 20px !important">
                            <asp:Button ID="btnExecute" runat="server" Text="Genereate" CssClass="btn btn-danger" Style="width: 90px !important; margin-left: -25px" ValidationGroup="s" OnClick="btnExecute_Click" />
                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" ConfirmText="Are you sure to execute records ?"
                                TargetControlID="btnExecute" runat="server" />
                        </div>


                    </div>
                    <div class="col-md-12" style="height: 600px">

                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%"></rsweb:ReportViewer>


                    </div>
                </div>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
