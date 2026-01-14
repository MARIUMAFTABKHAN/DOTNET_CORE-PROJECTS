<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BookReleaseOrder.aspx.cs" Inherits="ExpressDigital.BookReleaseOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .row {
            margin-bottom: 2px !important;
        }

        .EU_DataTable tr td {
            height: 35px !important;
        }

        .form-control {
            height: 28px !important;
        }

        .progressbar {
            width: 300px;
            height: 21px;
        }

        .progressbarlabel {
            width: 300px;
            height: 21px;
            position: absolute;
            text-align: center;
            font-size: small;
        }

        .ui-progressbar-value ui-corner-left ui-widget-header ui-progressbar-complete ui-corner-right {
            height: 20px !important;
            width: 250px !important;
        }

        .ui-widget-header {
            border: 1px solid #aaaaaa;
            color: white;
            background-color: forestgreen;
            font-weight: bold;
            height: 20px;
            width: 100% !important;
        }
    </style>
    <script src="Scripts/FileInput.js"></script>
    <%-- <script src="Content/jquery-ui.js"></script>
     <link href="Content/jquery-ui.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        //  $(function () {
        function SetScript() {
            $("#ContentPlaceHolder1_gv").find(".txtgross").on("blur", function () {

                var gross = $(this).val();
                var gst = parseFloat($(this).parent().next().find(".txtgstrate").val());
                var agc = parseFloat($(this).parent().next().next().find(".txtagcrate").val());
                var discount = parseFloat($(this).parent().next().next().next().next().next().find(".txtdiscount").val());
                var total = parseFloat(gross) - parseFloat(discount);//.toFixed(2);
                if (gst > 0)
                    var gstamount = parseFloat(((total * gst) / 100));//.toFixed(2);
                else
                    gstamount = 0;

                var agcamount = parseFloat(((total * agc) / 100));//.toFixed(2);

                if (isNaN(agcamount))
                    agcamount = 0;
                if (isNaN(gstamount))
                    gstamount = 0;
                var net = ((parseFloat(total) - parseFloat(gstamount)) + parseFloat(agcamount));
                $(this).parent().next().next().next().next().find(".txtagc").val(gstamount);
                $(this).parent().next().next().next().find(".txtgst").val(agcamount);
                $(this).parent().next().next().next().next().next().next().find(".txtnet").val(net);
            });// Discount


            $("#ContentPlaceHolder1_gv").find(".txtdiscount").on("blur", function () {

                var discount = $(this).val();;

                var txtagcrate = parseFloat($(this).parent().prev().prev().prev().find(".txtagcrate").val());
                var txtgstrate = parseFloat($(this).parent().prev().prev().prev().prev().find(".txtgstrate").val());
                var gross = parseFloat($(this).parent().prev().prev().prev().prev().prev().find(".txtgross").val());
                var total = (parseFloat(gross) - parseFloat(discount));//.toFixed(2);;;
                var gstamount = parseFloat(((total * txtgstrate) / 100));//.toFixed(2);
                var agcamount = parseFloat(((total * txtagcrate) / 100));//.toFixed(2);
                if (isNaN(agcamount))
                    agcamount = 0;
                if (isNaN(gstamount))
                    gstamount = 0;
                var net = ((parseFloat(total) + parseFloat(gstamount)) - parseFloat(agcamount));//.toFixed(2);
                $(this).parent().prev().prev().find(".txtgst").val(gstamount);
                $(this).parent().prev().find(".txtagc").val(agcamount);
                $(this).parent().next().find(".txtnet").val(net);

            });// GST Calcution below

            // gv usd
            $("#ContentPlaceHolder1_gv").find(".txtusd").on("blur", function () {

                var txtusd = parseFloat($(this).val()).toFixed(3);
                var txtcvr = parseFloat($(this).parent().next().find(".txtcr").val()).toFixed(4);;
                var total = parseFloat(txtusd) * parseFloat(txtcvr).toFixed(0);;
                $(this).parent().next().next().next().next().next().find(".txtgross").val(total);
            });

            // gv crv
            $("#ContentPlaceHolder1_gv").find(".txtcr").on("blur", function () {

                var txtcvr = parseFloat($(this).val()).toFixed(4);;
                var txtusd = parseFloat($(this).parent().prev().find(".txtusd").val()).toFixed(3);;

                var total = parseFloat(txtusd) * parseFloat(txtcvr).toFixed(0);;
                // alert(total);
                $(this).parent().next().next().next().next().find(".txtgross").val(total);
            });

            //// AGC input
            $("#ContentPlaceHolder1_gv").find(".txtagcrate").on("blur", function () {

                var txtagcrate = parseFloat($(this).val()).toFixed(2);;
                var txtgstrate = parseFloat($(this).parent().prev().find(".txtgstrate").val());
                var gross = parseFloat($(this).parent().prev().prev().find(".txtgross").val());
                var discount = parseFloat($(this).parent().next().next().next().find(".txtdiscount").val());
                var total = (parseFloat(gross) - parseFloat(discount)).toFixed(2);;;
                var gstamount = parseFloat(((total * txtgstrate) / 100));//.toFixed(2);
                var agcamount = parseFloat(((total * txtagcrate) / 100));//.toFixed(2);
                if (isNaN(agcamount))
                    agcamount = 0;
                if (isNaN(gstamount))
                    gstamount = 0;
                var net = ((parseFloat(total) + parseFloat(gstamount)) - parseFloat(agcamount));//.toFixed(2);
                $(this).parent().next().find(".txtgst").val(gstamount);
                $(this).parent().next().next().find(".txtagc").val(agcamount);
                $(this).parent().next().next().next().next().find(".txtnet").val(net);
            });


            $("#ContentPlaceHolder1_gv").find(".txtgstrate").on("blur", function () {

                var txtgstrate = parseFloat($(this).val()).toFixed(2);;
                //alert(txtgstrate);

                //var agc = entPlaceHolder1_lblagc").text(); //$(this).parent().next().next().find(".txtagc").text();
                var txtagcrate = parseFloat($(this).parent().next().find(".txtagcrate").val());

                // alert(txtgstrate);
                var gross = parseFloat($(this).parent().prev().find(".txtgross").val());//.toFixed(2);;
                //     alert(gross);
                var discount = parseFloat($(this).parent().next().next().next().next().find(".txtdiscount").val());//.toFixed(2);;

                var total = (parseFloat(gross) - parseFloat(discount));//.toFixed(2);;;
                //  alert('total:' + total);
                var gstamount = parseFloat(((total * txtgstrate) / 100)).toFixed(0);
                //    alert('gst' + gstamount);
                var agcamount = parseFloat(((total * txtagcrate) / 100)).toFixed(0);
                //   alert('agc' + agcamount);
                var net = parseFloat(((parseFloat(total) + parseFloat(gstamount)) - parseFloat(agcamount)));//.toFixed(2);
                //   alert('net' + net);

                $(this).parent().next().next().find(".txtgst").val(gstamount);
                $(this).parent().next().next().next().find(".txtagc").val(agcamount);

                $(this).parent().next().next().next().next().next().find(".txtnet").val(net);
            });
            // USD Conversion

            $("#ContentPlaceHolder1_txtusd_E").on("blur", function () {
                //  alert("AA");
                var usd = parseFloat($(this).val().toFixed(3));;;
                var cvr = parseFloat($("#ContentPlaceHolder1_txtcr_E").val()).toFixed(4);// $(this).parent().next().find(".txtgstratee").val();


                //   alert(gross)
                //   alert(gst)
                //   alert(agc)
                if (isNaN(cvr))
                    cvr = 0.00;
                var total = (parseFloat(cvr) * parseFloat(usd)).toFixed(0);;
                if (isNaN(total))
                    total = 0.00;//                               
                $("#ContentPlaceHolder1_txtgross_E").val(total);
            });//    

            //$("#ContentPlaceHolder1_txtusd_E").on("blur", function () {
            //    //  alert("AA");
            //    var usd = $(this).val();
            //    var cvr = $("#ContentPlaceHolder1_txtcr_E").val();// $(this).parent().next().find(".txtgstratee").val();


            //    //   alert(gross)
            //    //   alert(gst)
            //    //   alert(agc)
            //    if (isNaN(cvr))
            //        cvr = 0.00;
            //    var total = parseFloat(cvr) * parseFloat(usd).toFixed(0);;
            //    if (isNaN(total))
            //        total = 0.00;//                               
            //    $("#ContentPlaceHolder1_txtgross_E").val(total).toFixed(0);;
            //});//    
            //  CRV rate


            $("#ContentPlaceHolder1_txtcr_E").on("blur", function () {
                //   alert("AA");
                var usd = parseFloat($(this).val()).toFixed(3);;;
                var cvr = parseFloat($("#ContentPlaceHolder1_txtusd_E").val()).toFixed(4);// $(this).parent().next().find(".txtgstratee").val();


                //   alert(gross)
                //   alert(gst)
                //   alert(agc)
                if (isNaN(cvr))
                    cvr = 0.00;
                var total = (parseFloat(cvr) * parseFloat(usd)).toFixed(0);;
                //alert(total.toFixed(0));
                if (isNaN(total))
                    total = 0.00;//                               
                $("#ContentPlaceHolder1_txtgross_E").val(total);
            });//    

            //
            $("#ContentPlaceHolder1_txtgross_E").on("blur", function () {
                //  alert("AA");
                var gross = parseFloat($(this).val()).toFixed(0);;;
                var gst = parseFloat($("#ContentPlaceHolder1_txtgstrate_E").val());// $(this).parent().next().find(".txtgstratee").val();
                var agc = parseFloat($("#ContentPlaceHolder1_txtagcrate_E").val());// $(this).parent().next().next().find(".txtagcratee").val();
                var discount = parseFloat($("#ContentPlaceHolder1_discount_E").val());// $(this).parent().next().next().next().next().next().find(".txtdiscounte").val();
                //   alert(gross)
                //   alert(gst)
                //   alert(agc)
                if (isNaN(discount))
                    discount = 0.00;
                var total = (parseFloat(gross) - parseFloat(discount)).toFixed(0);;;
                if (isNaN(total))
                    total = 0.00;//
                var gstamount = parseFloat(((total * gst) / 100)).toFixed(0);
                var agcamount = parseFloat(((total * agc) / 100)).toFixed(0);
                if (isNaN(agcamount))
                    agcamount = 0.00;
                if (isNaN(gstamount))
                    gstamount = 0.00;
                var net = ((parseFloat(total) - parseFloat(agcamount)) + parseFloat(gstamount));//.toFixed(2);
                $("#ContentPlaceHolder1_txtgst_E").val(gstamount);
                $("#ContentPlaceHolder1_txtagc_E").val(agcamount);
                $("#ContentPlaceHolder1_txtnet_E").val(net);
            });//          

            $("#ContentPlaceHolder1_txtagcrate_E").on("blur", function () {
                var txtagcrate = parseFloat($(this).val());//.toFixed (2);
                var txtgstrate = parseFloat($("#ContentPlaceHolder1_txtgstrate_E").val());//   $(this).parent().prev().find(".txtgstrate").val();
                var gross = parseFloat($("#ContentPlaceHolder1_txtgross_E").val());// $(this).parent().prev().prev().find(".txtgross").val();
                var discount = parseFloat($("#ContentPlaceHolder1_txtdiscount_E").val());// $(this).parent().next().next().next().find(".txtdiscount").val();
                if (isNaN(discount))
                    discount = 0.00;
                var total = (parseFloat(gross) - parseFloat(discount)).toFixed(0);
                var gstamount = parseFloat(((total * txtgstrate) / 100)).toFixed(0);
                var agcamount = parseFloat(((total * txtagcrate) / 100)).toFixed(0);
                if (isNaN(agcamount))
                    agcamount = 0;
                if (isNaN(gstamount))
                    gstamount = 0;
                var net = ((parseFloat(total) - parseFloat(agcamount)) + parseFloat(gstamount)).toFixed(0);
                //  var net = ((parseFloat(total) + parseFloat(gstamount)) - parseFloat(agcamount)).toFixed(0);
                $("#ContentPlaceHolder1_txtgst_E").val(gstamount);// $(this).parent().next().find(".txtgst").val(gstamount);
                $("#ContentPlaceHolder1_txtagc_E").val(agcamount);///$(this).parent().next().next().find(".txtagc").val(agcamount);
                $("#ContentPlaceHolder1_txtnet_E").val(net);//$(this).parent().next().next().next().next().find(".txtnet").val(net);
            });
            $("#ContentPlaceHolder1_txtgstrate_E").on("blur", function () {

                var txtgstrate = parseFloat($(this).val());//.toFixed(2);;
                var txtagcrate = parseFloat($("#ContentPlaceHolder1_txtagcrate_E").val());// $(this).parent().next().find(".txtagcrate").val();
                var gross = parseFloat($("#ContentPlaceHolder1_txtgross_E").val());//$(this).parent().prev().find(".txtgross").val();
                var discount = parseFloat($("#ContentPlaceHolder1_txtdiscount_E").val());//$(this).parent().next().next().next().next().find(".txtdiscount").val();
                if (isNaN(discount))
                    discount = 0.00;
                var total = parseFloat((parseFloat(gross)) - parseFloat(discount)).toFixed(0);;;
                var gstamount = parseFloat(((total * txtgstrate) / 100)).toFixed(0);
                var agcamount = parseFloat(((total * txtagcrate) / 100)).toFixed(0);
                if (isNaN(agcamount))
                    agcamount = 0;
                if (isNaN(gstamount))
                    gstamount = 0;
                //var net = ((parseFloat(total) + parseFloat(gstamount)) - parseFloat(agcamount)).toFixed(0);

                var net = parseFloat(((parseFloat(total) - parseFloat(agcamount)) + parseFloat(gstamount))).toFixed(0);

                $("#ContentPlaceHolder1_txtgst_E").val(gstamount);//$(this).parent().next().next().find(".txtgst").val(gstamount);
                $("#ContentPlaceHolder1_txtagc_E").val(agcamount);// $(this).parent().next().next().next().find(".txtagc").val(agcamount);
                $("#ContentPlaceHolder1_txtnet_E").val(net);// $(this).parent().next().next().next().next().next().find(".txtnet").val(net);
            });
            $("#ContentPlaceHolder1_txtdiscount_E").on("blur", function () {

                var discount = parseFloat($(this).val()).toFixed(2);

                var txtagcrate = parseFloat($("#ContentPlaceHolder1_txtagcrate_E").val());
                var txtgstrate = parseFloat($("#ContentPlaceHolder1_txtgstrate_E").val());
                var gross = parseFloat($("#ContentPlaceHolder1_txtgross_E").val());
                var total = parseFloat((parseFloat(gross) - parseFloat(discount)));
                if (isNaN(total))
                    total = 0.00;
                var gstamount = parseFloat(((total * txtgstrate) / 100)).toFixed(0);
                var agcamount = parseFloat(((total * txtagcrate) / 100)).toFixed(0);
                if (isNaN(agcamount))
                    agcamount = 0;
                if (isNaN(gstamount))
                    gstamount = 0;
                //var net = ((parseFloat(total) + parseFloat(gstamount)) - parseFloat(agcamount)).toFixed(0);
                var net = ((parseFloat(total) - parseFloat(agcamount)) + parseFloat(gstamount)).toFixed(0);

                $("#ContentPlaceHolder1_txtgst_E").val(gstamount);
                $("#ContentPlaceHolder1_txtagc_E").val(agcamount)
                $("#ContentPlaceHolder1_txtnet_E").val(net);

            });// Disc

            //});
        }

        //GST Calcution below// GST Calcution below




        function MyFunction(val) {
            var url = "RptReleaseOrder.aspx?ID=" + val;
            window.open(url, '_blank');
        }
        function RemoveRecord(val) {

            bootbox.confirm("Are you sure to delete ?", function (result) {
                var link = "ReleaseOrder.aspx";
                if (result) {

                    $.ajax({
                        type: "POST",
                        url: "ReleaseOrder.aspx/OnSubmit",
                        data: JSON.stringify({ id: val }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            bootbox.alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown + "")

                        },
                        success: function (result) {
                            if (result.d == "Ok") {
                                //bootbox.alert("Record Deleted Successfully.....", function () {
                                document.location.href = link;
                                //});

                            }
                            else {
                                bootbox.alert(result.d, function () {
                                });

                            }
                        }
                    });
                }

                // ---- WCF Service call backs -------------------




            });
        }
        function pageLoad() {
            //  $('input[type=file]').bootstrapFileInput();


            applyDatePicker();
            SetInputBox();
            SetScript();
            $('input[type=file]').bootstrapFileInput();
            $('.file-inputs').bootstrapFileInput();
            SetButton();
        }


        function applyDatePicker() {
            $("#ContentPlaceHolder1_dtCamStart").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/Calender.png',
                dateFormat: 'dd/mm/yy'
            });
            $("#ContentPlaceHolder1_dtCamEnd").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/Calender.png',
                dateFormat: 'dd/mm/yy'
            });
            $("#ContentPlaceHolder1_txtROMPDate").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/Calender.png',
                dateFormat: 'dd/mm/yy'
            });
            $("#ContentPlaceHolder1_txtStartDate").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/Calender.png',
                dateFormat: 'dd/mm/yy'
            });
            $("#ContentPlaceHolder1_txtEndDate").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/Calender.png',
                dateFormat: 'dd/mm/yy'
            });

            $("#ContentPlaceHolder1_txtEmail").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/Calender.png',
                dateFormat: 'dd/mm/yy'
            });
            $("#ContentPlaceHolder1_txtSearchROMODateFrom").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/Calender.png',
                dateFormat: 'dd/mm/yy'
            });
             $("#ContentPlaceHolder1_txtReleaseOrderDate").datepicker({
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
            $(".dtfrom").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/cal20x20.png',
                dateFormat: 'dd/mm/yy'
            });
            $(".dtto").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/cal20x20.png',
                dateFormat: 'dd/mm/yy'
            });
        }
        function SetInputBox() {
            //var ival = $('input[type=file]');
            // ival.attr('disabled', true);
            $('#btnUploadFile').attr('disabled', true);
        }

        function checkValidFile(input) {
            $("#btnUploadFile").prop('disabled', false);
            if ($("#ContentPlaceHolder1_txtReleaseOrder").val().length == 12) {
                if (input.files && input.files[0]) {
                    var file = input.files[0];
                    var fileType = file["type"];
                    var validImageTypes = ["application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/pdf", "application/msword", "application/ms-word", "image/jpeg", "image/jpg", "image/png", "application/zip"];
                    if ($.inArray(fileType, validImageTypes) < 0) {
                        alert("Only Doc/Docx/Pdf Format Allowed....");
                        $("#btnUploadFile").prop('disabled', true);

                        return;
                    }
                }
            }
            else {
                alert("Please create Release Order Before Uploading Documents");
                $("#fileUpload").val(null);
                $("#btnUploadFile").prop('disabled', true);
                return;
            }
        }
        function showPleaseWait() {
            document.getElementById('PleaseWait').style.display = 'block';
        }
        function SetButton() {
            $('#btnUploadFile').on('click', function () {
                // alert("AAAAA");
                var xhr = new XMLHttpRequest();
                var d = new Date();
                var time = d.getHours() + "-" + d.getMinutes() + "-" + d.getSeconds();
                var guid = $("#ContentPlaceHolder1_txtReleaseOrder").val() + "-" + time
                var data = new FormData();
                var files = $("#fileUpload").get(0).files;
                // Add the uploaded image content to the form data collection
                if (files.length > 0) {
                    data.append("UploadedImage", files[0]);
                }
                //xhr.upload.addEventListener("progress", function (event) {
                //    if (event.lengthComputable) {
                //        var progress = (event.loaded * 100 / event.total).toFixed();
                //        $("#progressbar").progressbar("value", progress);
                //    }
                //}, false);
                $("#ContentPlaceHolder1_hdguid").val(guid + "." + files[0].name.split(".").pop());
                var uid = $("#ContentPlaceHolder1_hduid").val();
                var Roid = $("#ContentPlaceHolder1_txtReleaseOrder").val();
                alert(uid);
                // Make Ajax request with the contentType = false, and procesDate = false          
                //string url = "WebForm4.aspx?BeginDate="+BeginDate+"& EndDate="+EndDate;
                xhr.open("POST", "FileHandler.ashx?guid=" + guid + "&RoID="+ Roid + "&sessionid=" + uid );
                xhr.send(data);

                //$("#progressbar").progressbar({
                //    max: 100,
                //    change: function () {
                //        $("#progresslabel").text($("#progressbar").progressbar("value") + "%");
                //    },
                //    complete: function () {
                //        $("#progresslabel").text("File upload successful!");
                //    }
                //});
                //event.preventDefault();
            });
        }
        function disableenable(val) {
            alert(val);
        }
    </script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {

        });
    </script>
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdsave" Value="0" runat="server" />
            <div class="col-md-12" style="margin-top: 0px; padding: 0px">
                <div class="panel panel-default" style="padding: 0px; margin: 0px">
                    <div class="panel-heading text-center">Release Order</div>
                    <div class="panel-body" style="padding-bottom: 9px !important">
                        <div class="form-group">
                            <div class="col-md-12 text-center">
                                <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                                <asp:HiddenField ID="hduid" runat="server"  />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-6" style="padding-left: 44px !important; margin-bottom: 13px;">
                                <input id="btnUploadFile" type="button" value="Upload File" class="btn btn-info" onclick="showPleaseWait()" />
                                <input id="fileUpload" type="file" onchange="checkValidFile(this)" />

                            </div>
                            <div style="float: left; width: 95%; height: 20px;">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblRecordHeader" runat="server" CssClass="lbl" Text="Set Parameters" Visible="false" />
                                <div class="helptext" id="PleaseWait" style="display: none; text-align: right; color: White; vertical-align: top;">
                                    <table id="MyTable" style="" bgcolor="red" align="center">
                                        <tr>
                                            <td style="width: 250px" align="center">
                                                <b><font color="white">Please Wait...</font></b>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <%-- <div class="col-md-6 progressbar progressbar" id="progressbar">
                                <div id="progresslabel" class="progressbarlabel"></div>
                            </div>--%>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-8">
                                <div class="col-md-12">
                                     <div class="form-group row">
                                        <div class="col-md-2">
                                            Release Order Date:
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtReleaseOrderDate" Style="width: 75%; display: inline" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Release Order Date Required"
                                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtReleaseOrderDate"></asp:RequiredFieldValidator>
                                        </div>


                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            Company
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList Style="min-width: 100%" ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            R.O.# 
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtReleaseOrder" Style="min-width: 100% !important" Text="RO-" CssClass="form-control" runat="server"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        ForeColor="Red" Display="Dynamic" ControlToValidate="txtReleaseOrder"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFOCPaid" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="P">Paid</asp:ListItem>
                                                <asp:ListItem Value="F">FOC</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            IO Ref (External)
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtExternalID" runat="server" Text="" CssClass="form-control" Style="width: 100%;"> </asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            Master (A/C)
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList ID="ddlMasterGroup" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlMasterGroup_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                        <div class="col-md-2">
                                            IIO (Interal) 
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtInternalID" Style="width: 100%;" runat="server" CssClass="form-control"></asp:TextBox>


                                        </div>
                                    </div>
                                    <div class="form-group row" style="text-align: center">
                                        <asp:CustomValidator ID="CustomValidator7" Display="Dynamic" ControlToValidate="ddlMasterGroup" OnServerValidate="ddlcampaign_server"
                                            runat="server" ForeColor="Red" ErrorMessage="*"></asp:CustomValidator>
                                        <asp:RequiredFieldValidator ControlToValidate="txtExternalID" Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please provide External RO Number" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            Agency                                          
                                            <asp:ImageButton ID="imgResetagency" ValidationGroup="na" runat="server" ImageUrl="~/Content/Images/Restore.png" OnClick="imgResetagency_Click" />
                                        </div>

                                        <div class="col-md-5">
                                            <asp:DropDownList ID="ddlAgency" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="col-md-2">
                                            RO/MP Date
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtROMPDate" runat="server" Style="width: 75%; display: initial" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <%--  <div class="col-md-2">
                                            G.S.Tax
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblgst" runat="server"  CssClass="form-control"></asp:Label>
                                        </div>--%>
                                    </div>
                                    <div class="form-group row" style="text-align: center">
                                        <asp:CustomValidator ID="CustomValidator6" Display="Dynamic" ControlToValidate="ddlAgency" OnServerValidate="ddlcampaign_server"
                                            runat="server" ForeColor="Red" ErrorMessage="Please Select Agency"></asp:CustomValidator>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select RO Date"
                                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtROMPDate"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group row">

                                        <div class="col-md-2">
                                            Client
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList ID="ddlclient" OnServerValidate="ddlcampaign_server" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-md-2">
                                            A.G.C
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblagc" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group row" style="text-align: center">
                                        <asp:CustomValidator ID="CustomValidator1" Display="Dynamic" ControlToValidate="ddlclient" OnServerValidate="ddlcampaign_server"
                                            runat="server" ForeColor="Red" ErrorMessage="Please select client"></asp:CustomValidator>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            Sales Person
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList ID="ddlSalesPerson" runat="server" CssClass="form-control"></asp:DropDownList>


                                        </div>
                                        <div class="col-md-2">
                                            GST. Rate
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblgst" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <%--<asp:CustomValidator ID="CustomValidator3" Display="Dynamic" ControlToValidate="ddlSalesPerson" OnServerValidate="ddlcampaign_server"
                                    runat="server" ForeColor="Red" ErrorMessage="Please select Sales Person "></asp:CustomValidator>--%>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            Campaign
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="txtCampaign" MaxLength="250" Rows="5" Style="height: 60px !important; width: 100%; min-width: 100%;" TextMode="MultiLine" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-5">
                                            <div class="row">
                                                <div class="col-md-5">
                                                    Start Date
                                                </div>
                                                <div class="col-md-7" style="padding-left: 9px">
                                                    <asp:TextBox ID="dtCamStart" Style="width: 75%; display: inline" runat="server" CssClass="form-control"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="row" style="padding: 0px">
                                                <div class="col-md-5">
                                                    End Date
                                                </div>
                                                <div class="col-md-7" style="padding-left: 9px">
                                                    <asp:TextBox ID="dtCamEnd" Style="width: 75%; display: inline" runat="server" CssClass="form-control"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row" style="text-align: center">
                                        <center>                                     
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtCamEnd" ErrorMessage="Invalid Start Date Format | " ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtCamStart" ErrorMessage="Invalid End Date Format | "  ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>                       
                                            <asp:RequiredFieldValidator ID="req" runat="server" ControlToValidate="txtCampaign" ErrorMessage="Please provide compaign details" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                          </center>

                                    </div>

                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            Remarks
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="txtRemrks" MaxLength="250" Rows="5" Style="height: 60px !important;" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-2 col-md-offset-2">
                                            <asp:Button CssClass="btn btn-success" Text="Save" ID="btnSave" OnClick="btnSave_Click" runat="server"></asp:Button>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button CssClass="btn btn-info" ID="btnAddNewRec" OnClick="btnAddNewRec_Click"  CausesValidation="false" Text="Add New" runat="server"></asp:Button>
                                            <%--<asp:Button CssClass="btn btn-danger" ID="btncancel" OnClick="btncancel_Click" CausesValidation="false" Text="Cancel" runat="server"></asp:Button>--%>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4" style="height: 250px">
                                <asp:Panel ID="Panel1" GroupingText="Calculations" runat="server">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:Label ID="lblClient" ForeColor="Red" runat="server" CssClass=""></asp:Label>
                                            </div>
                                            <div class="col-md-8">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                Billed
                                            </div>
                                            <div class="col-md-1">
                                                :
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblBilled" runat="server" Text="Not Billed" ForeColor="Green"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                Amount(US$)
                                            </div>
                                            <div class="col-md-1">
                                                :
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblGrossAmount" runat="server" CssClass=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                Gross Amount
                                            </div>
                                            <div class="col-md-1">
                                                :
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblGrossAmountPkr" runat="server" CssClass=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                Discount
                                            </div>
                                            <div class="col-md-1">
                                                :
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblTotalDiscount" runat="server" CssClass=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                AG.Comm
                                            </div>
                                            <div class="col-md-1">
                                                :
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblTotalCommission" runat="server" CssClass=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                G.S.Tax
                                            </div>
                                            <div class="col-md-1">
                                                :
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblTotalGST" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                Net Amount
                                            </div>
                                            <div class="col-md-1">
                                                :
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label ID="lblNetAmount" runat="server" CssClass=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                Cancelled 
                                            </div>
                                            <div class="col-md-1">
                                                :
                                            </div>
                                            <div class="col-md-7">
                                                <asp:CheckBox ID="chkIsCancelled" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="col-md-12" style="padding-left: 0px; padding-right: 0px">
                            <div style="width: 11%; float: left; text-align: center;">
                                Portal
                            </div>
                            <div style="width: 0px; float: left; text-align: center; display: none; margin-left: 2px !important">
                                Date From
                            </div>
                            <div style="width: 0px; float: left; text-align: center; display: none; margin-left: 2px !important">
                                Date To
                            </div>
                            <div style="width: 9%; float: left; text-align: center !important; margin-left: 2px !important">
                                Currency
                            </div>
                            <div style="width: 5%; float: left; text-align: center; margin-left: 2px !important">
                                USD
                            </div>
                            <div style="width: 5%; float: left; text-align: center; margin-left: 2px !important">
                                CR
                            </div>
                            <div style="width: 5%; float: left; text-align: center; margin-left: 2px !important">
                                CPM
                            </div>
                            <div style="width: 6%; float: left; text-align: center; margin-left: 2px !important">
                                Impression
                            </div>
                            <div style="width: 6%; float: left; text-align: center; margin-left: 2px !important">
                                Delivered
                            </div>
                            <div style="width: 6%; float: left; text-align: center; margin-left: 2px !important">
                                Gross
                            </div>
                            <div style="width: 6%; float: left; text-align: center;">
                                GST %   
                            </div>
                            <div style="width: 6%; float: left; text-align: center; margin-left: -5px !important">
                                AGC %
                            </div>
                            <div style="width: 7%; float: left; text-align: center; margin-left: 2px !important">
                                G.S.T
                            </div>

                            <div style="width: 7%; float: left; text-align: center; margin-left: 2px !important">
                                A.G.C
                            </div>
                            <div style="width: 6%; float: left; text-align: center; margin-left: -12px !important">
                                Discount
                            </div>
                            <div style="width: 7%; float: left; text-align: center; margin-left: -5px !important">
                                Net
                            </div>
                        </div>

                        <div class="col-md-12" style="padding-left: 0px; padding-right: 0px">
                            <div style="width: 11%; float: left">
                                <asp:DropDownList class="form-control" ValidationGroup="gv" ID="ddlportal_E" Width="100%" runat="server"></asp:DropDownList>

                            </div>
                            <div style="width: 0px; float: left; margin-left: 2px !important; display: none">
                                <asp:TextBox ID="dtfrom_E" ValidationGroup="gv" Style="width: 0px !important; display: inline" class="form-control dtfrom" runat="server"></asp:TextBox>

                            </div>
                            <div style="width: 0px; float: left; margin-left: 2px !important; display: none">
                                <asp:TextBox ID="dtto_E" ValidationGroup="gv" Style="width: 0px !important; display: inline" class="form-control dtfrom" runat="server"></asp:TextBox>
                            </div>
                            <div style="width: 9%; float: left; margin-left: 2px !important">
                                <asp:DropDownList ID="ddlcurrency_E" ValidationGroup="gv" class="form-control" Width="100%" runat="server"></asp:DropDownList>
                            </div>
                            <div style="width: 5%; float: left; margin-left: 2px !important">
                                <asp:TextBox ID="txtusd_E" ValidationGroup="gv" Text="0.00" class="form-control" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div style="width: 5%; float: left; margin-left: 2px !important">
                                <asp:TextBox ID="txtcr_E" Text="0.00" ValidationGroup="gv" class="form-control txtcre" runat="server" Width="100%"></asp:TextBox>

                            </div>
                            <div style="width: 5%; float: left; margin-left: 2px !important">
                                <asp:TextBox ID="txtcpm_E" Text="0" ValidationGroup="gv" class="form-control" runat="server" Width="100%"></asp:TextBox>

                            </div>
                            <div style="width: 6%; float: left; margin-left: 2px !important">
                                <asp:TextBox ID="txtimpressions_E" ValidationGroup="gv" Text="0" class="form-control form-control" runat="server" Width="100%"></asp:TextBox>

                            </div>
                            <div style="width: 6%; float: left; margin-left: 2px !important">
                                <asp:TextBox ID="txtDeliveredimpressions_E" ValidationGroup="gv" Text="0" class="form-control form-control" runat="server" Width="100%"></asp:TextBox>

                            </div>


                            <div style="width: 7%; float: left; margin-left: 2px !important">
                                <asp:TextBox ID="txtgross_E" ValidationGroup="gv" Text="0" Style="width: 100% !important" class="txtgross form-control" runat="server"></asp:TextBox>


                            </div>
                            <div style="width: 5%; float: left; margin-left: 2px !important">
                                <asp:TextBox ID="txtgstrate_E" ValidationGroup="gv" Text="0" class="txtgstrate_e form-control" Style="width: 100% !important" runat="server"></asp:TextBox>

                            </div>
                            <div style="width: 5%; float: left; margin-left: 2px !important">
                                <asp:TextBox ID="txtagcrate_E" ValidationGroup="gv" Text="0" class="form-control txtagcratee" Style="width: 100% !important" runat="server"></asp:TextBox>

                            </div>

                            <div style="width: 7%; float: left; margin-left: 2px !important">
                                <asp:TextBox ID="txtgst_E" ValidationGroup="gv" Text="0" class="form-control txtagce" Style="width: 100% !important" runat="server"></asp:TextBox>
                            </div>
                            <div style="width: 7%; float: left; margin-left: 2px !important">
                                <asp:TextBox ID="txtagc_E" ValidationGroup="gv" Text="0" class="form-control txtagce" Style="width: 100% !important" runat="server"></asp:TextBox>
                            </div>

                            <div style="width: 6%; float: left; margin-left: 2px !important">
                                <asp:TextBox ValidationGroup="gv" Text="0" class="form-control txtdiscounte" Style="width: 100% !important" ID="txtdiscount_E" runat="server"></asp:TextBox>


                            </div>

                            <div style="width: 7%; float: left; margin-left: 2px !important">
                                <asp:TextBox Text="0" class="form-control txtnete" Style="width: 100% !important" ID="txtnet_E" runat="server"></asp:TextBox>
                            </div>

                            <div style="width: 5%; float: left; margin-left: 8px !important">
                                <asp:Button ID="btnAddNew" ValidationGroup="gv" runat="server" Style="width: 100% !important; height: 27px !important" CssClass="btn btn-sm btn-success" Text="Add" OnClick="btnAddNew_Click" />
                            </div>

                        </div>

                        <div class="col-md-12" style="padding-left: 0px; padding-right: 0px; text-align: center">

                            <asp:RegularExpressionValidator ValidationGroup="gv" ID="RegularExpressionValidator2" runat="server" ValidationExpression="^\d+(.\d{1,2})?$"
                                ErrorMessage="Invalid GST Rate Format" ForeColor="Red" Display="Dynamic" ControlToValidate="txtgstrate_E" />

                            <asp:RegularExpressionValidator ValidationGroup="gv" ID="RegularExpressionValidator3" runat="server" ValidationExpression="^\d+(.\d{1,2})?$"
                                ErrorMessage="Invalid AGC Rate Format" ForeColor="Red" Display="Dynamic" ControlToValidate="txtagcrate_E" />

                            <%--   <asp:RegularExpressionValidator ValidationGroup="gv" ID="RegularExpressionValidator4" runat="server" ValidationExpression="^\d+(.\d{1,3})?$"
                                ErrorMessage="Invalid CPM Format" ForeColor="Red" Display="Dynamic" ControlToValidate="txtCPM_E" />--%>

                            <asp:RegularExpressionValidator ValidationGroup="gv" ID="RegularExpressionValidator5" runat="server" ValidationExpression="^\d+(.\d{1,2})?$"
                                ErrorMessage="Invalid USD Amount Format" ForeColor="Red" Display="Dynamic" ControlToValidate="txtusd_E" />

                            <asp:RequiredFieldValidator ValidationGroup="gv" ControlToValidate="dtfrom_E" ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="From Date Required"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ValidationGroup="gv" ID="RequiredFieldValidator4" ControlToValidate="dtfrom_E" runat="server" Display="Dynamic" ErrorMessage="To Date Required"></asp:RequiredFieldValidator>

                            <asp:RangeValidator ID="RangeValidator1" ValidationGroup="gv" ControlToValidate="txtgross_E" ForeColor="Red" Display="Dynamic"
                                MinimumValue="1" MaximumValue="9999999.999" runat="server" ErrorMessage="Gross Amount Required"></asp:RangeValidator>
                            <asp:RegularExpressionValidator ValidationGroup="gv" ID="Regex2ee" runat="server" ValidationExpression="^\d+(.\d{1,3})?$"
                                ErrorMessage="Invalid Gross Amount Format" ForeColor="Red" Display="Dynamic" ControlToValidate="txtgross_E" />


                            <asp:RegularExpressionValidator ValidationGroup="gv" ID="RegularExpressionValidator1" runat="server" ValidationExpression="^\d+(.\d{1,2})?$"
                                ErrorMessage="invalid Discount Format" ForeColor="Red" Display="Dynamic" ControlToValidate="txtdiscount_E" />

                            <asp:RangeValidator ID="RangeValidator2" ValidationGroup="gv" ControlToValidate="txtnet_E" ForeColor="Red" Display="Dynamic"
                                MinimumValue="1" MaximumValue="9999999.99" runat="server" ErrorMessage="Net Amount Required"></asp:RangeValidator>
                             <asp:RequiredFieldValidator ValidationGroup="gv" ID="RequiredFieldValidator27" ControlToValidate="txtnet_E" runat="server" Display="Dynamic" ErrorMessage="To Date Required"></asp:RequiredFieldValidator>

                            <asp:Label ID="lblgvdetailmesages" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-12" style="padding-left: 0px; padding-right: 0px">
                            <asp:GridView ID="gv" runat="server" PageSize="25" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false" OnRowDataBound="gv_RowDataBound">
                                <Columns>                                      
                                    <asp:TemplateField>
                                        
                                        <HeaderTemplate>
                                            Portal
                                        </HeaderTemplate>
                                        <HeaderStyle Width="12%" />
                                        <ItemStyle Width="12%" />
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlportal" Width="100%" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                        <HeaderTemplate>
                                            Date From
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="dtfrom" CssClass="dtfrom" Text='<%# Eval("StartDate", "{0:dd/MM/yyyy}") %>' runat="server" Width="75%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                        <HeaderTemplate>
                                            Date To
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="dtto" CssClass="dtto" Text='<%# Eval("EndDate", "{0:dd/MM/yyyy}") %>' runat="server" Width="75%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Currency
                                        </HeaderTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlcurrency" Width="100%" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="6%" />
                                        <ItemStyle Width="6%" />
                                        <HeaderTemplate>
                                            USD $
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtusd" DataFormatString="{0:0.000}" class="txtusd" Text='<%# Eval("CurrencyValue") %>' runat="server" Width="100%"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="Regex112" runat="server" ValidationExpression="^\d+(.\d{1,3})?$"
                                                ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtusd" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="6%" />
                                        <ItemStyle Width="6%" />
                                        <HeaderTemplate>
                                            C.R
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtcr" DataFormatString="{0:0.0000}" CssClass="txtcr" Text='<%# Eval("ConversionRate") %>' runat="server" Width="100%"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="Regex12" runat="server" ValidationExpression="^\d+(.\d{1,4})?$"
                                                ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtcr" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderStyle Width="6%" />
                                        <ItemStyle Width="6%" />
                                        <HeaderTemplate>
                                            C.P.M
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtcpm" Text='<%# Eval("CPMRate") %>' runat="server" Width="100%"></asp:TextBox>
                                            <%-- <asp:RegularExpressionValidator ID="Regex000" runat="server" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtcpm" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderStyle Width="0%" />
                                        <ItemStyle Width="0%" />
                                        <HeaderTemplate>
                                            Weight
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty" Text='<%# Eval("Weight") %>' runat="server"></asp:TextBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="6%" />
                                        <ItemStyle Width="6%" />
                                        <HeaderTemplate>
                                            Imps.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtimpressions" Text='<%# Eval("Impressions") %>' runat="server" Width="100%"></asp:TextBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="6%" />
                                        <ItemStyle Width="6%" />
                                        <HeaderTemplate>
                                            Posted
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDeliveredimpressions" Text='<%# Eval("DeleiveredExpresssions") %>' runat="server" Width="100%"></asp:TextBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="7%" />
                                        <ItemStyle Width="7%" />
                                        <HeaderTemplate>
                                            Gross
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgross" Style="width: 100% !important" Text='<%# Eval("TotalBilled")  %>' class="txtgross" runat="server" Width="100%"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="Regex2" runat="server" ValidationExpression="^\d+(.\d{1,2})?$"
                                                ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtgross" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle Width="5%" />
                                        <HeaderTemplate>
                                            GST %
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgstrate" class="txtgstrate" DataFormatString="{0:0.00}" Style="width: 100% !important; text-align: right" Text='<%# Eval("GSTPercentage") %>' runat="server" Width="100%"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="Regex0" runat="server" ValidationExpression="^\d+(.\d{1,2})?$"
                                                ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtgstrate" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle Width="5%" />
                                        <HeaderTemplate>
                                            AGC %
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtagcrate" class="txtagcrate" DataFormatString="{0:0.00}" Style="width: 100% !important; text-align: right" Text='<%# Eval("AGCPercentage") %>' runat="server" Width="100%"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="Regex00" runat="server" ValidationExpression="^\d+(.\d{1,2})?$"
                                                ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtagcrate" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="7%" />
                                        <ItemStyle Width="7%" />
                                        <HeaderTemplate>
                                            G.S.T 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgst" Style="width: 100% !important" DataFormatString="{0:0.00}" Text='<%# Eval("GSTAmount")  %>' class="txtgst" runat="server" Width="100%"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="Regex3" runat="server" ValidationExpression="^\d+(.\d{1,2})?$"
                                                ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtgst" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="7%" />
                                        <ItemStyle Width="7%" />
                                        <HeaderTemplate>
                                            A.G.C 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtagc" Style="width: 100% !important; text-align: right" DataFormatString="{0:0.00}" class="txtagc" Text='<%# Eval("AGCommission") %>' runat="server" Width="100%"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="Regex4" runat="server" ValidationExpression="^\d+(.\d{1,2})?$"
                                                ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtagc" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle Width="5%" />
                                        <HeaderTemplate>
                                            Discount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtdiscount" Style="width: 100% !important; text-align: right" DataFormatString="{0:0.00}" Text='<%# Eval("Discount") %>' class="txtdiscount" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="Regex5" runat="server" ValidationExpression="^\d+(.\d{1,2})?$"
                                                ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtdiscount" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="7%" />
                                        <ItemStyle Width="7%" />
                                        <HeaderTemplate>
                                            txtNet
                                        </HeaderTemplate>
                                        <ItemTemplate>                                           

                                            <asp:TextBox Style="width: 100% !important; text-align: right" ID="txtnet"  Text='<%# Eval("NetAmount") %>' class="txtnet" runat="server"></asp:TextBox>

                                            <asp:HiddenField ID="hdID" Value='<%# Eval("ID") %>' runat="server" />
                                            <asp:HiddenField ID="hdPortalID" Value='<%# Eval("PortalId") %>' runat="server" />
                                            <asp:HiddenField ID="hdCurrencyID" Value='<%# Eval("CurrencyID") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="7%" />
                                        <ItemStyle Width="7%" />
                                        <HeaderTemplate>
                                            Remove
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnremove" OnClick="btnremove_Click" ImageUrl="~/Content/Images/Trash-Cancel.ico" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
