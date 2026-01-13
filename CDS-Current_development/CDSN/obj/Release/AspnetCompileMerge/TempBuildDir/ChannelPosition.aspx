<%@ Page Title="" Language="C#" EnableViewState="true" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ChannelPosition.aspx.cs" Inherits="CDSN.ChannelPosition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function UnHighlight(txt) {
            var grid = $("#sortable");
            var inputs = grid.find("input");
            for (var i = 0; i < inputs.length; i++) {

                for (var j = 0; j < inputs.length; j++) {
                    inputs[i].style.backgroundColor = "white";
                    inputs[j].style.backgroundColor = "white";
                }
            }
            var OptrId = $('#<%=ddlOperator.ClientID %> option:selected').val()
            var UserId = $('#<%= hduid.ClientID%>').val();
            var isValid = true;
            var grid = $("#sortable");
            var inputs = grid.find("input");
            var elements = grid.find('.clsChild');
            var cpos = 0;
            var optrid = 0;
            var divchid = 0;
            var ppos = 0;
            var posid = 0;
            for (var i = 0; i < inputs.length; i++) {

                for (var j = 0; j < inputs.length; j++) {


                    if (inputs[i] != inputs[j] && (inputs[i].value != "0" || inputs[j].value != "0")) {

                        if (inputs[i] != inputs[j] && (inputs[i].value != "" || inputs[j].value != "") && inputs[i].value == inputs[j].value) {
                            inputs[i].style.backgroundColor = "red";
                            inputs[j].style.backgroundColor = "red";

                            if (isValid) {

                                isValid = false;

                            }

                        }

                    }
                }

            }

        }



        function checkduplidate() {

           
            var OptrId = $('#<%=ddlOperator.ClientID %> option:selected').val()
            var UserId = $('#<%= hduid.ClientID%>').val();
            var isValid = true;
            var grid = $("#sortable");
            var inputs = grid.find("input");
            var elements = grid.find('.clsChild');          
            var posid = 0;
            for (var i = 0; i < inputs.length; i++) {

                for (var j = 0; j < inputs.length; j++) {


                    if (inputs[i] != inputs[j] && (inputs[i].value != "0" || inputs[j].value != "0")) {

                        if (inputs[i] != inputs[j] && (inputs[i].value != "" || inputs[j].value != "") && inputs[i].value == inputs[j].value) {
                            inputs[i].style.backgroundColor = "red";
                            inputs[j].style.backgroundColor = "red";

                            if (isValid) {

                                isValid = false;

                            }

                        }

                    }
                }

            }
            if (isValid) {



                var Cpos = 0;
                var Ppos = 0;
                var CPInut = 0;
                var values = [];
                for (var i = 0; i < inputs.length; i++) {
                    CPInut = inputs[i].value;

                    const myArray = elements[i].innerHTML.split(":");
                    Channelid = myArray[0];
                    posid = myArray[1];
                    Ppos = myArray[2]

                    values.push({
                        OptrId: OptrId,
                        UserId: UserId,
                        ChannelId: Channelid,
                        Cpos: CPInut,
                        Ppos: Ppos,
                        Totalchannels: inputs.length,
                        PosId: posid
                    });

                }
               // alert(values.length);
                //var strProp = "{strProp:" + JSON.stringify(values) + "}";
                $('#loadingmessage').show();
                $.ajax({
                    type: "POST",
                    url: "GetCPR.asmx/GetJSON",

                    //url: "API/JSONData",
                    data: JSON.stringify({ data: values }),
                    //data:  strProp , //JSON.stringify(input),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        //alert(r.d);
                        $('#loadingmessage').hide();
                    },
                    failure: function (r) {
                        alert(r.d);
                    }
                });
            }
        }


    </script>
    <style type="text/css">
        #divchid {
            display: none;
        }

        #sortable {
            list-style-type: none;
            margin: 0;
            padding: 0;
            width: 100%;
        }

            #sortable li {
                margin: 3px 3px 3px 0;
                padding: 1px;
                float: left;
                width: 19%;
                height: 45px;
                font-size: 1em;
                text-align: center;
                background-color: #bed8eb;
            }

        .divtitle {
            color: #f0071d;
        }


        .txt {
            width: 50px !important;
        }

        .divcolor {
            color: #bed8eb;
            background-color: #4682B4;
            text-align: center;
            font-weight: bold;
            width: 96% !important;
        }
    </style>
    <asp:UpdatePanel ud="up" runat="server">
        <ContentTemplate>

            <div class="row">
                <asp:HiddenField ID="hduid" runat="server" />
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Channel Position</div>
                        <div class="panel-body">
                            <div class="col-md-12">

                                <div class="row">
                                    <strong style="color: red">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                            <div class="col-md-12">

                                <div class="row" style="margin-bottom: 5px">
                                    <div class="col-md-2 col-md-offset-1">
                                        Territory :
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlTerritory" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        City :
                                    </div>
                                    <div class="col-md-3 ">
                                        <asp:DropDownList ID="ddlcity" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlcity_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row" style="margin-bottom: 5px">
                                    <div class="col-md-2  col-md-offset-1">
                                        District :
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlDistrict" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        Operator :
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlOperator" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOperator_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row" style="margin-bottom: 5px">
                                    <asp:HiddenField ID="hdch" runat="server" />
                                    <div class="col-md-3 col-md-offset-3">
                                        <input type="button" id="btnupdate" onclick="checkduplidate();" value="Update Positions" style="width: 100% !important; min-width: 100%!important" class="btn btn-info" />
                                    </div>
                                    <div class="col-md-3">
                                        <div id='loadingmessage' style='display: none; text-align: center'>
                                            <img src='Content/Images/ajax-loader.gif' />
                                            <span style="font-size:medium;color:green"> Job in Progress.... </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-bottom: 5px; margin-left: 25px">
                                    <asp:PlaceHolder ID="mph" runat="server"></asp:PlaceHolder>
                                </div>


                            </div>
                        </div>


                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
