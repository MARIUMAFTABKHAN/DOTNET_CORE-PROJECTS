<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JobForms.aspx.cs" Inherits="ABMS.JobForms" %>

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

            $("#MainContent_txtDOB").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                yearRange: '-60:+0',
                buttonImage: 'Content/images/Calender.png',
                dateFormat: 'dd-mm-yy',
                maxDate: '0', 
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

        .mr label {
            margin-left: 10px !important;
        }

        .un label {
            margin-left: 10px !important;
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

        .row {
            margin-bottom: 3px !important;
        }

        input[type="radio"], input[type="checkbox"] {
            /* margin: 4px 0 0; */
            margin-top: -3px;
            line-height: normal;
        }

        .navbar-default {
            background-color:#FFF;
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


            });
            function dateSelectionChanged(sender, args) {
                selectedDate = sender.get_selectedDate();
                alert(selectedDate);

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
            <%--            <div class="row" style="padding-top: 2px; margin-left: 10px; margin-right: 20px;">
                <div class="col-md-12">
                    <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 25px; padding-top: 2px">
                        <b>Personal Information
                        </b>
                    </div>
                </div>
            </div>--%>

            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 25px; padding-top: 2px; height: 33px">
                        <b>
                            <asp:Label ID="lblpersonalinfo" runat="server" Text="Personal Information " Style="font-size: large; font-weight: bold; font-family: Exo"></asp:Label>
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
                    <div style="width: 50%; float: left">
                        <asp:TextBox ID="txtCanName" placeholder="Applicant Name" CssClass="form-control" Style="width: 100%; max-width: 100%;" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Name " Text=" " ControlToValidate="txtCanName" ForeColor="Red" Display="Dynamic" Font-Bold="true">

                        </asp:RequiredFieldValidator>
                    </div>
                    <div style="width: 50%; float: left">
                        <div style="width: 100%; float: left; margin-left: 5px; padding-top: 3px">

                            <div style="width: 10%; float: left">
                                <asp:RadioButton GroupName ="aa" runat="server" ID="RdoMarrid" Checked="true" CssClass="mr" />
                            </div>
                            <div style="width: 40%; float: left; margin-top: 2px;">
                                Married
                            </div>

                            <div style="width: 10%; float: left">
                                <asp:RadioButton GroupName ="aa" runat="server" ID="RdoUnMarrid" CssClass="un" />
                            </div>
                            <div style="width: 40%; float: left; margin-top: 2px;">
                                Single
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">

                    <asp:TextBox ID="txtAdddressDetails" placeholder="Address" CssClass="form-control Maxwidth" TextMode="MultiLine" Height="70" Rows="4" Style="width: 100%; max-width: 100%;" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Address" ControlToValidate="txtAdddressDetails" ForeColor="Red" Display="Dynamic" Font-Bold="true">

                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div style="width: 50%; float: left">
                        <asp:DropDownList ID="ddlCity" CssClass="form-control" Style="width: 100%; max-width: 100%;" runat="server"></asp:DropDownList>
                    </div>
                    <div style="width: 50%; float: left">
                        <asp:TextBox ID="txtOtherCity" placeholder="Other City not in list" CssClass="form-control" Style="width: 100%; max-width: 100%;" runat="server"></asp:TextBox>
                        <b>
                            <asp:Label ID="ErrorCity" runat="server" Style="color: red"> </asp:Label>
                        </b>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div style="width: 50%; float: left">
                        <asp:TextBox ID="txtEmail" placeholder="Email" CssClass="form-control" Style="width: 100%; max-width: 100%;" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                            Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" runat="server" ErrorMessage="Invalid Email Addresss"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Email Address" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" Font-Bold="true">

                        </asp:RequiredFieldValidator>
                    </div>
                    <div style="width: 50%; float: left">
                        <div style="width: 100%; float: left; margin-left: 5px; padding-top: 3px">

                            <div style="width: 10%; float: left">
                                <asp:RadioButton GroupName ="bb" runat="server" ID="RdoMale" Checked="true" CssClass="mr" />
                            </div>
                            <div  style="width: 40%; float: left; margin-top: 2px;">
                                Male
                            </div>

                            <div style="width: 10%; float: left">
                                <asp:RadioButton runat="server" GroupName ="bb" ID="RdoFemale" CssClass="un" />
                            </div>
                            <div style="width: 40%; float: left; margin-top: 2px;">
                                Female
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: -5px !important">
                <div class="col-md-12">
                    <div style="width: 50%; float: left">
                        <asp:TextBox ID="txtMobile" placeholder="Mobile 1" CssClass="form-control" Style="width: 100%; max-width: 100%;" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Mobile Number" ControlToValidate="txtMobile" ForeColor="Red" Display="Dynamic" Font-Bold="true">

                        </asp:RequiredFieldValidator>
                    </div>
                    <div style="width: 50%; float: left">
                        <asp:TextBox ID="txtAlandLine" placeholder="Mobile 2" CssClass="form-control" Style="width: 100%; max-width: 100%;" runat="server"></asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: -5px !important">
                <div class="col-md-12">
                    <div style="width: 100%; float: left">
                        <asp:TextBox ID="txtDOB" placeholder="Date Of Birth (DD/MM/YYYY)" CssClass="form-control" Style="width: 89%; display: inline; max-width: 100%;" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select Date of Birth" ControlToValidate="txtDOB" ForeColor="Red" Display="Dynamic" Font-Bold="true">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: -5px !important">
                <div class="col-md-12">
                    <div style="width: 100%; float: left">
                        <asp:TextBox ID="txtHobbies" TextMode="MultiLine" Rows="4" Height="70" placeholder="Hobbies (Optional)" CssClass="form-control" Style="width: 100%; max-width: 100%;" runat="server"></asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 25px; padding-top: 2px; height: 33px">
                        <b>
                            <asp:Label ID="lblappliedfor" runat="server" Text="Position Applied: " Style="font-size: large; font-weight: bold; font-family: Exo"></asp:Label>
                        </b>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div style="width: 50%; float: left">
                        <asp:DropDownList ID="ddlPositionApplied" CssClass="form-control" Style="width: 100%; max-width: 100%;" runat="server"></asp:DropDownList>
                        <b>
                            <asp:Label ID="ErrPosition" runat="server" Style="color: red"></asp:Label></b>

                    </div>
                    <div style="width: 50%; float: left">
                        <div style="width: 100%; float: left; margin-left: 5px; padding-top: 3px">

                            <div style="width: 10%; float: left">
                                <asp:RadioButton runat="server"  GroupName ="cc"  ID="RdoExp" Checked="true" CssClass="mr" />
                            </div>
                            <div style="width: 50%; float: left;  margin-top: 2px;">
                                Experience
                            </div>

                            <div style="width: 10%; float: left">
                                <asp:RadioButton runat="server" ID="RdoFresh" CssClass="un" GroupName ="cc" />
                            </div>
                            <div style="width: 30%; float: left; margin-top: 2px;">
                                Fresh
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div style="width: 50%; float: left">
                        <asp:DropDownList ID="ddlReadytoJoin" CssClass="form-control" Style="width: 100%; max-width: 100%;" runat="server" OnSelectedIndexChanged="ddlReadytoJoin_SelectedIndexChanged">
                            <asp:ListItem>Availability</asp:ListItem>
                            <asp:ListItem>Immediately</asp:ListItem>
                            <asp:ListItem>In 1 Week</asp:ListItem>
                            <asp:ListItem>In 2 Weeks</asp:ListItem>
                            <asp:ListItem>In 3 Weeks</asp:ListItem>
                            <asp:ListItem>In 4 Weeks</asp:ListItem>
                            <asp:ListItem>More then 4 weeks</asp:ListItem>
                        </asp:DropDownList>
                        <b>
                            <asp:Label ID="ErrReady" runat="server" Style="color: red"></asp:Label></b>
                    </div>
                    <div style="width: 50%; float: left">
                        <asp:DropDownList ID="ddlLocation" CssClass="form-control" Style="width: 100%; max-width: 100%;" runat="server"></asp:DropDownList>
                        <b>
                            <asp:Label ID="ErrLocation" runat="server" Style="color: red"></asp:Label></b>
                    </div>
                </div>
            </div>
            <div class="row" id="lan1" runat="server" >
                <div class="col-md-12">
                    <div style="width: 50%; float: left">
                        <asp:DropDownList ID="ddlLanges" CssClass="form-control" Style="width: 100%; max-width: 100%;" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="txtOthers" CssClass="form-control" runat="server" Style="min-width: 100%" AutoPostBack="True" OnTextChanged="txtOthers_TextChanged" Visible="false"></asp:TextBox>
                        <b>
                            <asp:Label ID="ErrOthers" runat="server" Style="color: red"></asp:Label></b>

                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 25px; padding-top: 2px; height: 33px">
                        <b>
                            <asp:Label ID="lblqualification" runat="server" Text="Acadamic Qualifications" Style="font-size: large; font-weight: bold; font-family: Exo"></asp:Label>
                        </b>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">

                    <div style="width: 35%; float: left; background-color: lightgray; text-align: center">
                        <b>Institution</b>
                    </div>
                    <div style="width: 35%; float: left; background-color: lightgray; text-align: center">
                        <b>Degree/Certificate</b>
                    </div>
                    <div style="width: 30%; float: left; background-color: lightgray; text-align: center">
                        <b>Passing Year</b>
                    </div>


                    <div style="margin-bottom: 5px; width: 35%; float: left">
                        <asp:TextBox ID="txtschool" placeholder="School" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="margin-bottom: 5px; width: 35%; float: left">
                        <asp:DropDownList ID="ddluni1" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:DropDownList>
                    </div>
                    <div style="margin-bottom: 5px; width: 30%; float: left">
                        <asp:TextBox ID="txtschoolyear" placeholder="Passing Year" runat="server" MaxLength="4" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>

                    <div style="margin-bottom: 5px; width: 35%; float: left">
                        <asp:TextBox ID="txtcollege" placeholder="College" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="margin-bottom: 5px; width: 35%; float: left">
                        <asp:DropDownList ID="ddluni2" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:DropDownList>
                    </div>
                    <div style="margin-bottom: 5px; width: 30%; float: left">
                        <asp:TextBox ID="txtcollegeyear" placeholder="Passing Year" runat="server" MaxLength="4" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="margin-bottom: 5px; width: 35%; float: left">
                        <asp:TextBox ID="txtuni1" placeholder="University" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="margin-bottom: 5px; width: 35%; float: left">
                        <asp:DropDownList ID="ddluni3" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:DropDownList>
                    </div>
                    <div style="margin-bottom: 5px; width: 30%; float: left">
                        <asp:TextBox ID="txtuniyear1" placeholder="Passing Year" runat="server" MaxLength="4" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="margin-bottom: 5px; width: 35%; float: left">
                        <asp:TextBox ID="txtuni2" placeholder="University" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="margin-bottom: 5px; width: 35%; float: left">
                        <asp:DropDownList ID="ddluni4" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:DropDownList>
                    </div>
                    <div style="margin-bottom: 5px; width: 30%; float: left">
                        <asp:TextBox ID="txtuniyear2" placeholder="Passing Year" runat="server" MaxLength="4" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>

                    <div style="margin-bottom: 5px; width: 35%; float: left">
                        <asp:TextBox ID="txtuni3" placeholder="University" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="margin-bottom: 5px; width: 35%; float: left">
                        <asp:DropDownList ID="ddluni5" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:DropDownList>
                    </div>
                    <div style="margin-bottom: 5px; width: 30%; float: left">
                        <asp:TextBox ID="txtuniyear3" placeholder="Passing Year" runat="server" MaxLength="4" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                </div>
            </div>

            </div>
            <div id="divtechinical" runat="server">
                <div class="row" style="padding-top: 5px">
                    <div class="col-md-12">
                        <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 25px; padding-top: 2px; height: 33px">
                            <b>
                                <asp:Label ID="lbltechinical" runat="server" Text="Technical" Style="font-size: large; font-weight: bold; font-family: Exo"></asp:Label>
                            </b>
                        </div>
                    </div>
                </div>

                <div class="row" style="padding-top: 5px">
                    <div class="col-md-12">
                        <div style="width: 34%; float: left; background-color: lightgray; text-align: center">
                            <b>Institution</b>
                        </div>
                        <div style="width: 33%; float: left; background-color: lightgray; text-align: center">
                            <b>Degree/Certificate</b>
                        </div>
                        <div style="width: 33%; float: left; background-color: lightgray; text-align: center">
                            <b>Passing Year</b>
                        </div>

                        <div style="width: 34%; float: left">
                            <asp:TextBox ID="txtins1" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 33%; float: left">
                            <asp:TextBox ID="txtdeg1" placeholder="Degree/Certificate" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 33%; float: left">
                            <asp:TextBox ID="txtpass1" placeholder="Passing Year" MaxLength="4" runat="server" Style="min-width: 100%" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div style="width: 34%; float: left">
                            <asp:TextBox ID="txtins2" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 33%; float: left">
                            <asp:TextBox ID="txtdeg2" placeholder="Degree/Certificate" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 33%; float: left">
                            <asp:TextBox ID="txtpass2" placeholder="Passing Year" MaxLength="4" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>

                        <div style="width: 34%; float: left">
                            <asp:TextBox ID="txtins3" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 33%; float: left">
                            <asp:TextBox ID="txtdeg3" placeholder="Degree/Certificate" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 33%; float: left">
                            <asp:TextBox ID="txtpass3" placeholder="Passing Year" MaxLength="4" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>

                        <div style="width: 34%; float: left">
                            <asp:TextBox ID="txtins4" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 33%; float: left">
                            <asp:TextBox ID="txtdeg4" placeholder="Degree/Certificate" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 33%; float: left">
                            <asp:TextBox ID="txtpass4" placeholder="Passing Year" MaxLength="4" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>

                        <div style="width: 34%; float: left">
                            <asp:TextBox ID="txtins5" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 33%; float: left">
                            <asp:TextBox ID="txtdeg5" placeholder="Degree/Certificate" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 33%; float: left">
                            <asp:TextBox ID="txtpass5" placeholder="Passing Year" MaxLength="4" runat="server" CssClass="form-control " Style="min-width: 100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divtraining" runat="server">
                <div class="row" style="padding-top: 5px">
                    <div class="col-md-12">
                        <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 25px; padding-top: 2px; height: 33px">
                            <b>
                                <asp:Label ID="lblworkshop" runat="server" Text="Workshop" Style="font-size: large; font-weight: bold; font-family: Exo"></asp:Label>
                            </b>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 5px">
                    <div class="col-md-12">

                        <div style="width: 50%; float: left; background-color: lightgray; text-align: center; ">
                            <b>Institution</b>
                        </div>
                        <div style="width: 26%; float: left; background-color: lightgray; text-align: center">
                            <b>Training Course</b>
                        </div>
                        <div style="width: 12%; float: left; background-color: lightgray; text-align: center">
                            <b>Passing Year</b>
                        </div>
                        <div style="width: 12%; float: left; background-color: lightgray; text-align: center">
                            <b>Duration</b>
                        </div>



                        <div style="width: 50%; float: left">
                            <asp:TextBox ID="txttrg1" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 26%; float: left">
                            <asp:TextBox ID="txttrgcourse1" placeholder="Title of Training Course" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 12%; float: left">
                            <asp:TextBox ID="txttrgyear1" placeholder="Passing Year" runat="server" MaxLength="4" Style="min-width: 100%" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div style="width: 12%; float: left">
                            <asp:TextBox ID="txttrgdur1" placeholder="Duration" runat="server" Style="min-width: 100%" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div style="width: 50%; float: left">
                            <asp:TextBox ID="txttrg2" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 26%; float: left">
                            <asp:TextBox ID="txttrgcourse2" placeholder="Title of Training Course" runat="server" Style="min-width: 100%" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div style="width: 12%; float: left">
                            <asp:TextBox ID="txttrgyear2" placeholder="Passing Year" runat="server" Style="min-width: 100%" MaxLength="4" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div style="width: 12%; float: left">
                            <asp:TextBox ID="txttrgdur2" placeholder="Duration" runat="server" Style="min-width: 100%" CssClass="form-control"></asp:TextBox>
                        </div>


                        <div style="width: 50%; float: left">
                            <asp:TextBox ID="txttrg3" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 26%; float: left">
                            <asp:TextBox ID="txttrgcourse3" placeholder="Title of Training Course" runat="server" Style="min-width: 100%" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div style="width: 12%; float: left">
                            <asp:TextBox ID="txttrgyear3" placeholder="Passing Year" runat="server" Style="min-width: 100%" MaxLength="4" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div style="width: 12%; float: left">
                            <asp:TextBox ID="txttrgdur3" placeholder="Duration" runat="server" Style="min-width: 100%" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div style="width: 50%; float: left">
                            <asp:TextBox ID="txttrg4" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 26%; float: left">
                            <asp:TextBox ID="txttrgcourse4" placeholder="Title of Training Course" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 12%; float: left">
                            <asp:TextBox ID="txttrgyear4" placeholder="Passing Year" runat="server" MaxLength="4" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div style="width: 12%; float: left">
                            <asp:TextBox ID="txttrgdur4" placeholder="Duration" runat="server" Style="min-width: 100%" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                </div>
            </div>


            <div class="row" style="padding-top: 5px; height:50px">
                <div class="col-md-12">
                    <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 50px; padding-top: 2px;">
                        <b>
                            <asp:Label ID="lblJobsExp" runat="server" Text="Experience (OPTIONAL for fresh candidates) Start from current employer" Style="font-size: large; font-weight: bold; font-family: Exo"></asp:Label>
                        </b>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">

                    <div style="width: 25%; float: left; background-color: lightgray; text-align: center">
                        <b>Institution</b>
                    </div>
                    <div style="width: 20%; float: left; background-color: lightgray; text-align: center">
                        <b>Position</b>
                    </div>
                    <div style="width: 20%; float: left; background-color: lightgray; text-align: center">
                        <b>Duration</b>
                    </div>
                    <div style="width: 35%; float: left; background-color: lightgray; text-align: center">
                        <b>Job Description</b>
                    </div>



                    <div style="width: 25%; float: left">
                        <asp:TextBox ID="txtexpcom1" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="width: 20%; float: left">
                        <asp:TextBox ID="txtexppos1" placeholder="Position" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div style="width: 20%; float: left">
                        <div style="width: 50%; float: left">
                            <asp:DropDownList ID="ddlexpcomMMS1" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlexpcomYYS1" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div style="width: 50%; float: left">
                            <asp:DropDownList ID="ddlexpcomMME1" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlexpcomYYE1" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div style="width: 35%; float: left">
                        <asp:TextBox ID="txtexpBrief1" placeholder="Brief job Description" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" Style="height: 60px !important;"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div style="width: 25%; float: left">
                        <asp:TextBox ID="txtexpcom2" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="width: 20%; float: left">
                        <asp:TextBox ID="txtexppos2" placeholder="Position" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div style="width: 20%; float: left">
                        <div style="width: 50%; float: left">
                            <asp:DropDownList ID="ddlexpcomMMS2" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlexpcomYYS2" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div style="width: 50%; float: left">
                            <asp:DropDownList ID="ddlexpcomMME2" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlexpcomYYE2" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div style="width: 35%; float: left">
                        <asp:TextBox ID="txtexpBrief2" placeholder="Brief job Description" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" Style="height: 60px !important;"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div style="width: 25%; float: left">
                        <asp:TextBox ID="txtexpcom3" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="width: 20%; float: left">
                        <asp:TextBox ID="txtexppos3" placeholder="Position" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div style="width: 20%; float: left">
                        <div style="width: 50%; float: left">
                            <asp:DropDownList ID="ddlexpcomMMS3" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlexpcomYYS3" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div style="width: 50%; float: left">
                            <asp:DropDownList ID="ddlexpcomMME3" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlexpcomYYE3" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div style="width: 35%; float: left">
                        <asp:TextBox ID="txtexpBrief3" placeholder="Brief job Description" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" Style="height: 60px !important;"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">

                    <div style="width: 25%; float: left">
                        <asp:TextBox ID="txtexpcom4" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="width: 20%; float: left">
                        <asp:TextBox ID="txtexppos4" placeholder="Position" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div style="width: 20%; float: left">
                        <div style="width: 50%; float: left">
                            <asp:DropDownList ID="ddlexpcomMMS4" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlexpcomYYS4" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div style="width: 50%; float: left">
                            <asp:DropDownList ID="ddlexpcomMME4" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlexpcomYYE4" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div style="width: 35%; float: left">
                        <asp:TextBox ID="txtexpBrief4" placeholder="Brief job Description" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" Style="height: 60px!important; min-width: 100%"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">

                    <div style="width: 25%; float: left">
                        <asp:TextBox ID="txtexpcom5" placeholder="Name of Institution" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                    </div>
                    <div style="width: 20%; float: left">
                        <asp:TextBox ID="txtexppos5" placeholder="Position" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div style="width: 20%; float: left">
                        <div style="width: 50%; float: left">
                            <asp:DropDownList ID="ddlexpcomMMS5" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlexpcomYYS5" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div style="width: 50%; float: left">
                            <asp:DropDownList ID="ddlexpcomMME5" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlexpcomYYE5" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div style="width: 35%; float: left">
                        <asp:TextBox ID="txtexpBrief5" placeholder="Brief job Description" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" Style="height: 60px !important;"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div id="divexpdetails" runat="server">
                <div class="row" style="padding-top: 5px">
                    <div class="col-md-12" style="height:50px !important">
                        <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 50px; padding-top: 2px;">
                            <b>
                                <asp:Label ID="lblwrokingexp" runat="server" Text="Do you have experience working in one or more of the following writing domains? (OPTIONAL)" Style="font-size: large; font-weight: bold; font-family: Exo"></asp:Label>
                            </b>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 5px">
                    <div class="col-md-12">
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="id1,id2" AutoGenerateColumns="false">
                            <Columns>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Select 
                                    </HeaderTemplate>
                                    <ItemStyle Width="3%" />
                                    <ItemTemplate>
                                        <div style="text-align: right !important">
                                            <asp:CheckBox ID="chk1" runat="server"></asp:CheckBox>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="47%" />
                                    <HeaderTemplate>
                                        Details
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDetail1" Text='<%# Eval("desc1") %>' runat="server"></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Select 
                                    </HeaderTemplate>
                                    <ItemStyle Width="3%" />
                                    <ItemTemplate>
                                        <div style="text-align: right !important">
                                            <asp:CheckBox ID="chk2" runat="server"></asp:CheckBox>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Details
                                    </HeaderTemplate>
                                    <HeaderStyle Width="47%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDetail2" Text='<%# Eval("desc2") %>' runat="server"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px" >
                <div class="col-md-12">
                    <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 25px; padding-top: 2px; height: 33px">
                        <b>
                            <asp:Label ID="lbllanguages" runat="server" Text="Languages" Style="font-size: large; font-weight: bold; font-family: Exo"></asp:Label>
                        </b>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">

                    <div style="width: 5%; float: left; background-color: lightgray; text-align: center; color: lightgray">
                        .
                    </div>
                    <div style="width: 50%; float: left; background-color: lightgray; text-align: center">
                        <b>Language</b>
                    </div>                   
                   
                    <div style="width: 15%; float: left; background-color: lightgray; text-align: center">
                        <b>Read</b>
                    </div>
                    <div style="width: 15%; float: left; background-color: lightgray; text-align: center">
                        <b>Write</b>
                    </div>
                    <div style="width: 15%; float: left; background-color: lightgray; text-align: center">
                        <b>Speak</b>
                    </div>


                    <div style="width: 5%; float: left">
                        <asp:CheckBox ID="chk1" runat="server" />
                    </div>
                    <div style="width: 50%; float: left">
                        Urdu
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlRead1" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlwrite1" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlspeak1" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>

                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div style="width: 5%; float: left">
                        <asp:CheckBox ID="chk2" runat="server" />
                    </div>
                    <div style="width: 50%; float: left">
                        English
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlRead2" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlwrite2" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlspeak2" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>

                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div style="width: 5%; float: left">
                        <asp:CheckBox ID="chk3" runat="server" />
                    </div>
                    <div style="width: 25%; float: left">
                        Other (Please specify)
                    </div>
                    <div style="width: 25%; float: left">
                        <asp:TextBox ID="txtoth3" Style="min-width: 100%" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlRead3" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlwrite3" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlspeak3" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>

                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div style="width: 5%; float: left">
                        <asp:CheckBox ID="chk4" runat="server" />
                    </div>
                    <div style="width: 25%; float: left">
                        Other (Please specify) 
                    </div>
                    <div style="width: 25%; float: left">
                        <asp:TextBox ID="txtoth4" Style="min-width: 100%" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlRead4" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlwrite4" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div style="width: 15%; float: left">
                        <asp:DropDownList ID="ddlspeak4" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 25px; padding-top: 2px; height: 33px">
                        <b>
                            <asp:Label ID="lbl" runat="server" Text="Brief Note (OPTIONAL)" Style="font-size: large; font-weight: bold; font-family: Exo"></asp:Label>
                        </b>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <asp:TextBox ID="txtBriefnote" CssClass="form-control" placeholder="Brief note about your self" runat="server" Rows="6" Height="60" TextMode="MultiLine" Style="height: 100px !important;">
                    </asp:TextBox>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div style="width: 100%">
                        <b><span style="color: red">
                            <asp:Label ID="ErrorCaptcha" runat="server"></asp:Label>
                        </span></b>
                    </div>
                    <div style="width: 60%; float: left">
                        <asp:Image ID="imgCaptcha" ImageUrl="~/Content/captcha/1BIMUC.jpg" runat="server"  class="img-responsive" />
                    </div>
                    <div style="width: 30%; float: left; margin-left: 8px;">
                        <asp:TextBox ID="txtcaptcha" CssClass="form-control" runat="server" OnTextChanged="txtcaptcha_TextChanged"></asp:TextBox>
                    </div>
                  
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div style="width: 60%; float: left">Please type the text in the box as shown in the picture </div>
                     <div style="width: 15%; float: left;margin-left: 8px;">
                        <asp:Button ID="btnResetCaptcha" CausesValidation="false" runat="server" Text="Reset Captcha" CssClass="btn btn-danger" OnClick="btnResetCaptcha_Click"  />
                    </div>
                </div>
            </div>
             <div class="row" style="padding-top: 5px">
                <div class="col-md-12">               
                     <div style="width: 25%; float: left">
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnsubmit_Click" />
                    </div>
                    <div style="width: 25%; float: left; margin-left:10px">
                        <asp:Button ID="btnReset" CausesValidation="false" runat="server" Text="Reset" CssClass="btn btn-default" OnClick="btnReset_Click" />
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
