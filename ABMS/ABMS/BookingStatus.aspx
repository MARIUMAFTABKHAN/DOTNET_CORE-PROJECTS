<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookingStatus.aspx.cs" Inherits="ABMS.BookingStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <link href="Content/MenuRed.css" rel="stylesheet" />
            <style type="text/css">
                .more_info {
                    border-bottom: 1px dotted;
                    position: relative;
                }

                    .more_info .title {
                        top: 20px;
                        background: silver;
                        padding: 4px;
                        left: 0;
                        white-space: nowrap;
                        margin: 0px auto;
                        color: red;
                    }

                .alert-div {
                    width: 500px;
                    display: none;
                    background-color: #ff9999;
                    text-align: center;
                    border-radius: 5px;
                    margin-bottom: 20px;
                    padding: 20px;
                }

                #modalContainer {
                    background-color: rgba(0, 0, 0, 0.3);
                    position: absolute;
                    width: 100%;
                    height: 100%;
                    top: 0px;
                    left: 0px;
                    z-index: 10000;
                    /*background-image: url(tp.png);  required by MSIE to prevent actions on lower z-index elements */
                }

                #alertBox {
                    position: relative;
                    width: 300px;
                    min-height: 100px;
                    margin-top: 50px;
                    border: 1px solid #666;
                    background-color: #fff;
                    background-repeat: no-repeat;
                    background-position: 20px 30px;
                }

                #modalContainer > #alertBox {
                    position: fixed;
                }

                #alertBox h1 {
                    margin: 0;
                    font: bold 0.9em verdana,arial;
                    background-color: #8e1421;
                    color: #FFF;
                    border-bottom: 1px solid #000;
                    padding: 2px 0 2px 5px;
                }

                #alertBox p {
                    font: 700 verdana,arial;
                    height: 100px;
                    padding-left: 5px;
                    margin-left: 20px;
                    padding-top: 20px;
                    margin-right: 20px;
                    color: #8e1421;
                    width: auto;
                }

                #alertBox #closeBtn {
                    display: block;
                    position: relative;
                    margin: 5px auto;
                    padding: 7px;
                    border: 0 none;
                    width: 70px;
                    font: 0.7em verdana,arial;
                    text-transform: uppercase;
                    text-align: center;
                    color: #FFF;
                    background-color: #8e1421;
                    border-radius: 3px;
                    text-decoration: none;
                }

                /* unrelated styles */

                #mContainer {
                    position: relative;
                    width: 600px;
                    margin: auto;
                    padding: 5px;
                    border-top: 2px solid #000;
                    border-bottom: 2px solid #000;
                    font: 0.7em verdana,arial;
                }

                h1, h2 {
                    margin: 0;
                    padding: 4px;
                    font: bold 1.5em verdana;
                    border-bottom: 1px solid #000;
                }

                code {
                    font-size: 1.2em;
                    color: #069;
                }

                #credits {
                    position: relative;
                    margin: 25px auto 0px auto;
                    width: 350px;
                    font: 0.7em verdana;
                    border-top: 1px solid #000;
                    border-bottom: 1px solid #000;
                    height: 90px;
                    padding-top: 4px;
                }

                    #credits img {
                        float: left;
                        margin: 5px 10px 5px 0px;
                        border: 1px solid #000000;
                        width: 80px;
                        height: 79px;
                    }

                .important {
                    background-color: #F5FCC8;
                    padding: 2px;
                }

                code span {
                    color: green;
                }

                .dd_chk_drop {
                    height: 250px !important;
                }
                .row{
                    margin-bottom :5px;
                }
            </style>
            <script type="text/javascript">
                $(document).ready(function () {
                    //$('#divmessage').hide();
                    //$('#divmessage').click(function () {
                    //    $('#divmessage').fadeOut( "slow" );;
                    //});

                    //$('#MainContent_dynDivCode0').click(function () {
                    //    $('#divmessage').fadeIn( "slow" );
                    //});
                    //$('#MainContent_dynDivCode1').click(function () {
                    //    $('#divmessage').fadeIn( "slow" );
                    //});



                });
                function pageLoad() {
                    applyDatePicker();
                }
            </script>
            <script type="text/javascript">
                //Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
                var ALERT_TITLE = "Stations !";
                var ALERT_BUTTON_TEXT = "Ok";
                var str;
                if (document.getElementById) {
                    window.alert = function (txt) {

                        createCustomAlert(txt);
                    }
                }



                function checkCheck(pl, pl2, pl3) {
                    //alert(pl);                    
                    ALERT_TITLE = pl2;
                    alert(pl + "<br />" + pl3);
                }
                function createCustomAlert(txt) {
                    //var partsOfStr = txt.split(':')
                    // alert(txt);
                    //alert(str[0]);
                    //alert(str[1]);

                    d = document;

                    if (d.getElementById("modalContainer")) return;

                    mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
                    mObj.id = "modalContainer";
                    mObj.style.height = "120px";// d.documentElement.scrollHeight + "px";

                    alertObj = mObj.appendChild(d.createElement("div"));
                    alertObj.id = "alertBox";
                    if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
                    alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";

                    alertObj.style.visiblity = "visible";
                    alertObj.style.width = "auto";
                    alertObj.style.minWidth = "175px";
                    alertObj.style.height = "120";

                    //alertObj.style.marginLeft = "-100px";
                    h1 = alertObj.appendChild(d.createElement("h1"));
                    h1.appendChild(d.createTextNode(ALERT_TITLE));

                    msg = alertObj.appendChild(d.createElement("p"));

                    // lblObj = alertObj.appendChild(d.createComment("p"));
                    //lblObj.innerHTML.appendChild(d.createTextNode("Munir"))
                    //msg.appendChild(d.createTextNode(txt));
                    msg.innerHTML = txt;

                    btn = alertObj.appendChild(d.createElement("a"));
                    btn.id = "closeBtn";
                    btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
                    btn.href = "#";
                    btn.focus();
                    btn.onclick = function () { removeCustomAlert(); return false; }

                    alertObj.style.display = "block";

                }

                function removeCustomAlert() {
                    document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainer"));
                }
                function ful() {
                    alert('Alert this pages');
                }



                function applyDatePicker() {


                    $("#MainContent_dtInsertionDate").datepicker({
                        showOn: 'button',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        buttonImage: 'Content/images/Calender.png',
                        dateFormat: 'dd-mm-yy'
                    });
                }

            </script>
            <script type="text/javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
                });
            </script>
            <div class="col-md-12">


                <div class="form-group" style="height: 22px; margin-top: 3px">
                    <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 25px; padding-top: 2px">
                        <b>Booking Status
                        </b>
                    </div>
                </div>
            </div>
            <div class="col-md-12">

                <div class="row">
                    <div class="col-md-3" style="margin-bottom :3px;">
                        <asp:DropDownList ID="ddlPublication" runat="server" CssClass="form-control" Width="100%" Style="max-width: 100% !important" AutoPostBack="True" OnSelectedIndexChanged="ddlPublication_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3" style="margin-bottom :3px;">
                        <asp:DropDownList ID="ddlPage" runat="server" CssClass="form-control" Width="100%" Style="max-width: 100% !important" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="row">
                        <div class="col-md-3" style="margin-bottom :3px;">
                            <div class="input-group" style="margin-left:15px; margin-right :15px">
                                <asp:TextBox ID="dtInsertionDate" ReadOnly="false" placeholder="Insertion Date" runat="server" CssClass="form-control  input-small" Style="width: 83%; max-width: 83%; display: initial" AutoPostBack="false"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:Button ID="btnSubmit" Text="View" runat="server" class="btn btn-success btn-sm" OnClick="btnSubmit_Click" />
                                </span>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="col-md-12" style="height: auto">
                <div class="form-group" style="background-color: #FFF; padding: 5px">
                    <asp:RadioButtonList ID="RdoStations" runat="server" AutoPostBack="true" RepeatColumns="6" RepeatDirection="Horizontal" Width="100%" OnSelectedIndexChanged="RdoStations_SelectedIndexChanged">
                    </asp:RadioButtonList>
                </div>

            </div>

            <%--<div class="alert alert-success" id="divmessage" style="margin-left:15px; margin-right:15px;" >
                <asp:Label runat="server" ID="lblAgency">                        
                </asp:Label>
                <br />
                <asp:Label runat="server" ID="lblClinet">                        
                <br />
                </asp:Label>
                <asp:Label runat="server" ID="lblConfirm">                        
                </asp:Label>

                <asp:Label runat="server" ID="lblTentive" style="color:red">                        
                </asp:Label>
            </div>--%>


            <div class="col-md-12" style="height: auto; display: block inline-block">
                <div class="form-group">
                    <div style="width: 100%; float: left; height: auto">
                        <div style="background-color: #cccccc !important; width: 100%; max-width: 100%; color: #c9223d">
                            <asp:Label ID="lblConfirmed" runat="server" Style="font-size: medium; font-weight: bold" Text="Confirmed">

                            </asp:Label>
                        </div>
                        <div id="DivConfirmed" runat="server" style="background-color: #f5f5f5 !important; width: 100%; max-width: 100%; display: inline-block">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="height: auto; display: block inline-block">
                <div style="width: 100%; float: left; height: auto">
                    <div style="background-color: #cccccc !important; width: 100%; max-width: 100%; color: #c9223d">
                        <asp:Label ID="lbltentative" runat="server" Style="font-size: medium; font-weight: bold" Text="Tentative">

                        </asp:Label>
                    </div>
                    <div id="Divtentative" runat="server" style="background-color: #f5f5f5 !important; width: 100%; max-width: 100%; display: inline-block">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
