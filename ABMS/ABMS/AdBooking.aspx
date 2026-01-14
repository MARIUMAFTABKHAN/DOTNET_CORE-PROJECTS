<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdBooking.aspx.cs" Inherits="ABMS.AdBooking" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/MenuGray.css" rel="stylesheet" />
    <script type="text/javascript">
        function deleteConfirm() {
            var result = confirm('Are you sure to Cancel this booking  ?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#MainContent_divclient').hide();
        });


        function applyDatePicker() {

            $("#MainContent_dtInsertionDate").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/images/Calender.png',
                dateFormat: 'dd-mm-yy'
            });

            $("#MainContent_txtAsOn").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/images/Calender.png',
                dateFormat: 'dd-mm-yy'
            });

        }


    </script>
    <style type="text/css">
        body {
            background-color: #f5f5f5;
        }

        .modalBackground {
            background-color: #c4223d;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        div.dd_chk_select {
            height: 32px;
        }

            div.dd_chk_select div#caption {
                padding-top: 7px;
                height: 32px;
            }

        div.label {
            display: none;
        }

        div.dd_chk_drop {
            top: 32px !important;
            height: 215px !important;
        }

            div.dd_chk_drop div {
                height: 20px;
                margin-top: 10px;
            }

                div.dd_chk_drop div span {
                    margin-top: -11px;
                }

                    div.dd_chk_drop div span input {
                        display: block;
                        float: left;
                    }

            div.dd_chk_drop label {
                padding-top: 12px;
            }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 1px;
            border-style: solid;
            border-color: #c4223d;
            margin-left: 14px;
            margin-right: 14px;
        }

        .PopupHeader {
            background-color: #dfdfdf;
            border-width: 1px;
            border-style: solid;
            border-color: #c4223d;
            margin-bottom: 3px;
        }

        .Controls {
            margin-top: 0%;
            float: right;
        }

        #gv tr {
            height: 15px;
        }

        .Maxwidth {
            max-width: 100%;
        }

        .Centerselect {
            text-align: center;
        }

        input {
            height: 30px !important;
        }

        select {
            height: 32px !important;
        }

        .btn-xs {
            height: 20px !important;
        }

        .btn-xm {
            height: 30px !important;
        }

        .s-small {
            font-size: smaller;
            padding-left: 1px;
            padding-right: 1px;
        }

        th {
            background-color: #e3e2e3;
        }
      
    </style>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {

        });
    </script>

    <script language="javascript" type="text/javascript">
        function pageLoad() {

            applyDatePicker();
            $(function () {
                //$("#MainContent_btnSearchclient").click(function () {
                //    alert("AAA");
                //    $('#MainContent_divclient').hide();
                //} );

                //$("#btn").click(function () {

                //    $('#MainContent_divclient').show();
                //});
                //$("#btncancelsave").click(function () {

                //    $('#MainContent_divclient').hide();
                //});

                //$("#MainContent_btnClose").click(function () {
                //    $('#MainContent_txtClient').val("");
                //    $('#MainContent_hdclientid').val("");

                //    $('#MainContent_btnCancel').click();

                //});
                //$("#MainContent_btnCloseAgency").click(function () {
                //    $('#MainContent_txtAgency').val("");
                //    $('#MainContent_hdagencyid').val("");
                //    $('#MainContent_btnCancel').click();
                //});
                //$('#MainContent_dtInsertionDate').on('input', function () {
                //    alert("AA");
                //});

            });
            function dateSelectionChanged(sender, args) {
                selectedDate = sender.get_selectedDate();
                alert(selectedDate);
                /* replace this next line with your JS code to get the Sunday date */
                //sundayDate = getSundayDateUsingYourAlgorithm(selectedDate); 
                /* this sets the date on both the calendar and textbox */
                //sender.set_SelectedDate(sundayDate); 
            }
        }

        function myFunction(val) {

            if (val == "show") {
                $("#RowSearch").show();
                return false;
            }
            else {
                $("#RowSearch").hide();
                return false;
            }
        }
        function showPleaseWait() {
            document.getElementById('PleaseWait').style.display = 'block';
        }
        function doclientsearch() {
            document.getElementById('PleaseWaitSClient').style.display = 'block';
        }
        function dosearchagency() {
            document.getElementById('PleaseWaitAgency').style.display = 'block';
        }
        function dosearch() {
            document.getElementById('PleaseSearch').style.display = 'block';
        }


    </script>

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>

            <div class="row" style="padding-top: 2px">
                <div class="col-md-12">
                    <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 25px; padding-top: 2px">
                        <b>Ad Booking 
                        </b>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 2px">
                <div class="col-md-12">
                    <center>
                   <asp:Label ID="lblMessage" runat="server" ForeColor="#c4223d"></asp:Label></center>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="helptext" id="PleaseSearch" style="display: none; text-align: right; color: White; vertical-align: top;">
                        <table id="MySearch" style="text-align: center">
                            <tr>
                                <td style="width: 100%; text-align: center">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Content/Images/spinner.gif" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div style="width: 7%; float: left; margin-top: -4px; color: #c4223d;">
                        <asp:CheckBox ID="chkConfirm" runat="server" />
                    </div>
                    <div style="width: 50%; float: left; margin-left: 5px; padding-top: 3px">
                        <span>IsConfirmed Ad ? </span>
                    </div>
                    <div style="width: 5%; float: left; margin-top: 0px; display: none; color: #c4223d;">
                        <asp:Button ID="btnCancelAdd" runat="server" Text="Cancel" CssClass="btn btn-danger btn-xs" OnClientClick="return deleteConfirm();" OnClick="btnCancelAdd_Click" />
                    </div>
                    <div style="width: 70px; float: right">
                        <asp:Button ID="btnSearchAds" Text="Search" Width="70px" runat="server" OnClientClick="dosearch()"  CssClass="btn btn-success btn-sm" OnClick="btnSearchAds_Click" />
                    </div>
                </div>
            </div>
            <div class="row" id="RowSearch" runat="server">
                <div class="col-md-12">
                    <div class="table-responsive" style="height: auto">
                        <asp:GridView ID="gvAd" runat="server" DataKeyNames="ID" CssClass="table table-hover table-bordered table-condensed" GridLines="None"
                            Font-Size="X-Small" AutoGenerateColumns="false" EmptyDataText="No data in the data source." OnRowDataBound="gvAd_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" />
                                <asp:BoundField DataField="Publication" HeaderText="Pub." />
                                <asp:BoundField DataField="Base" HeaderText="Sta" />
                                <asp:BoundField DataField="Size" HeaderText="Size" />
                                <asp:BoundField DataField="Client" HeaderText="Client" ControlStyle-Width="30%" />
                                <asp:TemplateField>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle Width="5%" />
                                    <HeaderTemplate>
                                        Select
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdConfirm" Value='<%# Eval("IsConfirm")%>' runat="server" />
                                        <asp:Button ID="btnSelectAds" runat="server" Text="View" CssClass="btn btn-success btn-xs" OnClick="btnSelectAds_Click" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="cursor-pointer" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-sm-12" style="margin-bottom: -10px !important">

                            <div style="float: left; width: 50%">
                                <asp:TextBox ID="dtInsertionDate" ReadOnly="false" placeholder="Insertion Date" runat="server" CssClass="form-control  input-small" Style="width: 70%; max-width: 70%; display: initial !important" AutoPostBack="false" OnTextChanged="dtInsertionDate_TextChanged1"></asp:TextBox>
                                <%-- <asp:CalendarExtender ID="txtCalander" runat="server" CssClass="cal_Theme1" TargetControlID="dtInsertionDate" Format="dd-MM-yyyy" BehaviorID="_content_txtCalander" />--%>
                            </div>
                            <div style="float: left; width: 0%; height: 2px">
                            </div>
                            <div style="float: left; width: 14%">
                                <asp:Button ID="btnChart" runat="server" Text="View" CssClass="btn btn-success" Style="width: 100%; max-width: 100%; display: none" OnClick="btnChart_Click" />
                            </div>
                            <div style="float: left; width: 0%; height: 2px">
                            </div>
                            <div style="float: left; width: 50%">
                                <div style="float: left; width: 100%">
                                    <asp:DropDownList ID="ddlBaseStation" runat="server" CssClass="form-control" Width="100%">
                                    </asp:DropDownList>
                                </div>

                            </div>

                        </div>
                    </div>
                    <div class="row" style="margin-top: 2px">

                        <div class="col-sm-12">
                            <asp:DropDownList ID="ddlPublication" runat="server" CssClass="form-control" Width="100%" Style="max-width: 100% !important" AutoPostBack="True" OnSelectedIndexChanged="ddlPublication_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 2px; display: none">
                        <div class="col-sm-12">

                            <div style="float: left; width: 50%">
                            </div>
                            <div style="float: left; width: 50%; display: none">
                                <asp:DropDownList ID="ddlAdStation" runat="server" CssClass="form-control" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2px; display: none">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="ddlBookingExecutive" runat="server" CssClass="form-control" Width="100%">
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="row" style="margin-top: 2px">
                        <div class="col-sm-12">
                            <div style="float: left; width: 100%">

                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2px">
                        <div class="col-sm-12">
                            <div style="float: left; width: 65%">
                                <asp:TextBox ID="txtClient" placeholder="Client" CssClass="form-control Maxwidth" ReadOnly="true" Enabled="true" Style="width: 100%; max-width: 100%" runat="server"></asp:TextBox>


                                <asp:HiddenField ID="hdclientid" runat="server" />
                            </div>
                            <div style="float: left; width: 35%">
                                <span class="input-group-btn">
                                    <asp:Button runat="server" ID="btnAddNewClient" class="btn btn-default" Text="Add" Width="100%" OnClick="btnAddNewClient_Click"></asp:Button>
                                </span>
                                <span class="input-group-btn">
                                    <asp:Button runat="server" ID="btnShowClient" class="btn btn-default" Text="Find" Width="100%" OnClick="btnShowClient_Click"></asp:Button>
                                </span>
                            </div>

                        </div>
                    </div>
                    <div id="divNewclient" runat="server" class="row" style="height: auto  !important; margin-top: 2px">

                        <asp:Panel ID="PanelNew" runat="server" CssClass="modalPopup" align="center">
                            <div class="HellowWorldPopup" style="padding: 10px">
                                <div class="PopupHeader" id="PopupHeaderNew">
                                    Clients
                                </div>
                                <div class="PopupBody">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblclientmessage" ForeColor="Red" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                Client Name:
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtClientName" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                Main Category:
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlMainCateogry" CssClass="form-control" Width="100%" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                Sub Category:
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlSubCategory" CssClass="form-control" Width="100%" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                Address:
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtLine1" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3 col-md-offset-2">
                                                <asp:TextBox ID="txtLine2" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3 col-md-offset-2">
                                                <asp:TextBox ID="txtLine3" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3 col-md-offset-2">
                                                <asp:DropDownList ID="ddlClientCity" Width="100%" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4 col-md-offset-3">
                                                <div class="input-group" style="margin-top: 30px;">
                                                    <div class="col-md-4" style="float: left">
                                                        <asp:Button ID="btnSaveClient" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSaveClient_Click" Style="margin-left: 10px" />
                                                    </div>
                                                    <div class="col-md-4" style="float: right">
                                                        <asp:Button ID="btnCloseNew" runat="server" Text="Close" OnClick="btnCloseNewClient_Click" CssClass="btn btn-danger" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </asp:Panel>

                    </div>
                    <div id="divSearchClient" runat="server" class="row">
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center">
                            <div class="HellowWorldPopup" style="padding: 10px">
                                <div class="PopupHeader" id="PopupHeader">
                                    <div style="width: 100%">
                                        Client
                                    </div>
                                    <div class="helptext" id="PleaseWaitSClient" style="display: none; text-align: right; color: White; vertical-align: top;">
                                        <table id="MySTable" style="text-align: center">
                                            <tr>
                                                <td style="width: 100%; text-align: center">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Content/Images/spinner.gif" />
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                                <div class="PopupBody">
                                    <div>
                                        <asp:Label ID="lblClientNotFound" ForeColor="Red" runat="server"></asp:Label>
                                    </div>
                                    <div class="input-group">

                                        <asp:TextBox ID="txtSearchClient" placeholder="Min.4 lettter provide" CssClass="form-control" Style="float: right" runat="server"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button runat="server" ID="btnSearchclient" class="btn btn-default" Text="Search!" OnClientClick="doclientsearch()" OnClick="btnSearchClient_Click"></asp:Button>
                                        </span>
                                        <span class="input-group-btn">
                                            <asp:Button runat="server" ID="btnXClient" OnClick="btnXClient_Click" class="btn btn-default" Text="X"></asp:Button>
                                        </span>
                                    </div>
                                    <div class="row" style="padding-top: 15px;">
                                        <div class="col-md-12">
                                            <div class="table-responsive" style="height: 95% !important">
                                                <asp:GridView ID="gv" runat="server" DataKeyNames="ID" CssClass="table table-hover table-bordered table-condensed" GridLines="None"
                                                    AllowPaging="True" PageSize="7" OnPageIndexChanging="gv_PageIndexChanging" AutoGenerateColumns="false" Font-Size="Smaller">
                                                    <Columns>
                                                        <asp:BoundField DataField="Client" HeaderText="Client" ControlStyle-Width="90%" />
                                                        <asp:TemplateField>
                                                            <HeaderStyle Width="5%" />
                                                            <ItemStyle Width="5%" />
                                                            <HeaderTemplate>
                                                                Select
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="btn-xs" OnClick="btnSelectClient_Click" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass="cursor-pointer" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="row" style="margin-top: 2px">
                        <div class="col-sm-12">
                            <div style="float: left; width: 65%">
                                <asp:TextBox ID="txtAgency" placeholder="Agency" ReadOnly="true" CssClass="form-control Maxwidth" Enabled="true" Style="width: 100%; max-width: 100%" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdagencyid" runat="server" />
                            </div>
                            <div style="float: left; width: 35%;">
                                <span class="input-group-btn">
                                    <asp:Button runat="server" ID="btnShowAgency" OnClick="btnShowAgency_Click" class="btn btn-default" Text="Find" Width="100%"></asp:Button>
                                </span>
                            </div>

                        </div>
                    </div>
                    <div id="divSearchAgency" runat="server" class="row">
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center">
                            <div class="HellowWorldPopup" style="padding: 10px">
                                <div class="PopupHeader" id="PopupHeader2">
                                    <div style="width: 100%">
                                        Agency
                                    </div>
                                    <div class="helptext" id="PleaseWaitAgency" style="display: none; text-align: right; color: White; vertical-align: top;">
                                        <table id="MyAGTable" style="text-align: center">
                                            <tr>
                                                <td style="width: 100%; text-align: center">
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Content/Images/spinner.gif" />
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="PopupBody">
                                    <div>
                                        <asp:Label ID="lblAgencyFound" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="input-group">

                                        <asp:TextBox ID="txtSearchAgency" placeholder="Atleast 4 letter provide" CssClass="form-control" Style="float: right" runat="server"></asp:TextBox>
                                        <span class="input-group-btn">

                                            <asp:Button runat="server" ID="btnSearchAgency" placeholder="Min.4 lettter provide" class="btn btn-default" Text="Search!"
                                                OnClientClick="dosearchagency()" OnClick="btnSearchAgency_Click"></asp:Button>
                                        </span>
                                        <span class="input-group-btn">
                                            <asp:Button runat="server" ID="btnXAgencyClose" OnClick="btnXAgencyClose_Click" class="btn btn-default" Text="X"></asp:Button>
                                        </span>
                                    </div>
                                    <div class="row" style="padding-top: 15px;">
                                        <div class="col-md-12">
                                            <div class="table-responsive" style="height: 95% !important">
                                                <asp:GridView ID="gvAgency" runat="server" DataKeyNames="ID" CssClass="table table-hover table-bordered table-condensed" GridLines="None"
                                                    AllowPaging="True" PageSize="7" OnPageIndexChanging="gvAgency_PageIndexChanging" AutoGenerateColumns="false" Font-Size="Smaller">
                                                    <Columns>
                                                        <asp:BoundField DataField="Agency" HeaderText="Agency" ControlStyle-Width="90%" />
                                                        <asp:TemplateField>
                                                            <HeaderStyle Width="5%" />
                                                            <ItemStyle Width="5%" />
                                                            <HeaderTemplate>
                                                                Select
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnSelectAgency" runat="server" Text="Select" CssClass="btn-xs" OnClick="btnSelectAgency_Click" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass="cursor-pointer" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </asp:Panel>
                    </div>
                    <div class="row" style="margin-top: 2px">
                        <div class="col-sm-12">
                            <div style="float: left; width: 70%">
                                <asp:TextBox ID="txtCaption" runat="server" CssClass="form-control" Style="width: 100%; max-width: 100%" placeholder="Caption">
                                </asp:TextBox>
                            </div>
                            <div style="float: left; width: 30%">
                                <asp:TextBox ID="txtRO" runat="server" CssClass="form-control" Style="width: 100%; max-width: 100%" placeholder="RO #">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2px" runat="server" id="RowAsOn">
                        <div class="col-sm-12">
                            <div style="float: left; width: 50%">
                                <asp:TextBox ID="TextBox1" placeholder="Client" Text="hello" CssClass="form-control Maxwidth" ReadOnly="true" Style="width: 100%; max-width: 100%; display: none" runat="server"></asp:TextBox>

                            </div>
                            <div style="float: right; width: 50%">
                                <div style="width: 100%; max-width: 100%">
                                    <div style="width: 45%; float: left; max-width: 100%; text-align: right; padding-top: 5px">
                                        <span style="padding-top: 6px; max-width: 100%; text-align: right;">Ason Date :</span>
                                    </div>
                                    <div style="width: 55%; float: left; max-width: 100%">
                                        <asp:TextBox ID="txtAsOn" CssClass="form-control" Width="100%" Style="max-width: 90%; display: initial" runat="server"></asp:TextBox>
                                        <%--                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtAsOn" Format="dd-MM-yyyy" BehaviorID="_content_txtCalander" />--%>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2px">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Style="max-width: 100%; width: 100%" MaxLength="255" placeholder="Remarks"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2px">
                        <div class="col-sm-12">
                            <div style="float: left; width: 22%">
                                <asp:DropDownList ID="ddlCM" runat="server" CssClass="form-control s-small"></asp:DropDownList>
                            </div>
                            <div style="float: left; width: 20%">
                                <asp:DropDownList ID="ddlCOL" runat="server" CssClass="form-control s-small"></asp:DropDownList>
                            </div>
                            <div style="float: left; width: 28%">
                                <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control s-small"></asp:DropDownList>
                            </div>
                            <div style="float: left; width: 30%">
                                <asp:DropDownList ID="ddlMaterial" runat="server" CssClass="form-control s-small" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-12">
                    <div style="float: left; width: 40%">
                        <span>Select Station</span>
                    </div>
                    <%-- <div style="float: left; width: 30%">
                        <span>Page</span>
                    </div>
                    <div style="float: left; width: 30%">
                        <span>Position</span>
                    </div>--%>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div style="float: left; width: 100%; margin-bottom: 2px!important">

                        <asp:DropDownCheckBoxes ID="ddlStations" runat="server" AddJQueryReference="False"
                            Style="z-index: 1000" CssClass="form-control s-small"
                            UseButtons="true"
                            UseSelectAllNode="true">
                            <Style2 DropDownBoxBoxHeight="150" DropDownBoxBoxWidth="100%" SelectBoxWidth="100%" />

                        </asp:DropDownCheckBoxes>

                    </div>
                </div>
                <div class="col-md-12">
                    <div style="float: left; width: 40%">
                        <asp:DropDownList ID="ddlPage" runat="server" CssClass="form-control s-small">
                        </asp:DropDownList>
                    </div>
                    <div style="float: left; width: 40%">
                        <asp:DropDownList ID="ddlPosition" runat="server" CssClass="form-control s-small">
                        </asp:DropDownList>
                    </div>
                    <div style="float: left; width: 20%">
                        <span class="input-group-btn">
                            <asp:Button ID="btnSelectPage" runat="server" Text="Select" CssClass="btn btn-danger btn-sm" Style="width: 95%; margin-top: 1px; margin-left: 4px;" OnClick="btnSelectPage_Click" />
                        </span>

                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 2px;">
                <div class="col-md-12">
                    <div class="table-responsive" style="height: 95% !important">
                        <asp:GridView ID="gvPosition" runat="server" DataKeyNames="ID" CssClass="table table-hover table-bordered table-condensed" GridLines="None"
                            AutoGenerateColumns="false" Font-Size="X-Small" OnRowDeleting="gvPosition_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="Station" HeaderText="Station" ControlStyle-Width="25%" />
                                <asp:BoundField DataField="Page" HeaderText="Page" ControlStyle-Width="25%" />
                                <asp:BoundField DataField="Position" HeaderText="Position" ControlStyle-Width="25%" />
                                <asp:TemplateField>
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle Width="25%" />
                                    <HeaderTemplate>
                                        Remove
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdstationid" Value='<%#Eval("StationID" ) %>' runat="server" />
                                        <asp:HiddenField ID="hdpageid" Value='<%#Eval("PageID" ) %>' runat="server" />
                                        <asp:HiddenField ID="hdpositionid" Value='<%#Eval("PositionID" ) %>' runat="server" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Remove" CssClass="btn btn-danger btn-xs" OnClientClick="showPleaseWait()" OnClick="btnDelete_Click" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="cursor-pointer" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="helptext" id="PleaseWait" style="display: none; text-align: right; color: White; vertical-align: top;">
                    <table id="MyTable" style="text-align: center">
                        <tr>
                            <td style="width: 100%; text-align: center; padding-left: 15px">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Content/Images/spinner.gif" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3" style="float: left">
                    <asp:Button ID="btnSave" OnClientClick="showPleaseWait()" CssClass="btn btn-success btn-sm" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" OnClientClick="showPleaseWait()" CssClass="btn btn-default btn-sm" runat="server" Text="Reset" Width="100px" OnClick="btnCancel_Click" />
                </div>



            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
