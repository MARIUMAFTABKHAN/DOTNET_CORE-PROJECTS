<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FrmRptLedger.aspx.cs" Inherits="ExpressDigital.FrmRptLedger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
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
                    <h4> Print Ledger
                    </h4>
                </div>
            </div>
            <div class="col-md-10 col-md-offset-1">
                <div class="row">
                    <div class="col-md-12" style="margin-bottom: 3px !important">
                        <div class="col-md-2">
                            Company
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-top: 0px !important">
                        <div class="col-md-2">
                            Master Agency
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlMasteragency" AutoPostBack="true" OnSelectedIndexChanged="ddlMasteragency_SelectedIndexChanged" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            Date From
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateFrom" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12" style="margin-top: 3px !important">
                        <div class="col-md-2">
                            Branch Agency
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlAgency" AutoPostBack="true" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2" style="padding-top: 2px;">
                            Date To
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateTo" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-top: 3px !important">
                       
                       <div class="col-md-2 col-md-offset-2" >
                             <asp:Button ID="btnexport" runat="server" Text="Export To Excel" CssClass="btn btn-success" Style="width: 120px !important;" ValidationGroup="s" OnClick="btnexport_Click"/>
                        </div>

                        <div class="col-md-2" style="margin-left:380px !important;">
                            <asp:Button ID="btnExecute" runat="server" Text="Genereate" CssClass="btn btn-danger" Style="width: 90px !important;" ValidationGroup="s" OnClick="btnExecute_Click" />

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-16" style="margin-top: 4px !important; height: 600px">
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%"></rsweb:ReportViewer>
                    </div>
                </div>

            </div>
        </ContentTemplate>
         <Triggers>
        <asp:PostBackTrigger ControlID="btnexport" />
         </Triggers>
    </asp:UpdatePanel>
</asp:Content>
