<%@ Page Title="" EnableEventValidation="true" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FrmUploadDocuments.aspx.cs" Inherits="ExpressDigital.FrmUploadDocuments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .row {
            margin-bottom: 5px;
        }
    </style>
    <style type="text/css">
        .EU_DataTable tr td {
            height: 35px !important;
        }

        .form-control {
            height: 28px !important;
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
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {

        });
    </script>
    <script type="text/javascript">

        function pageLoad() {
            SetInputBox();
            $('input[type=file]').bootstrapFileInput();
            $('.file-inputs').bootstrapFileInput();
            // SetButton();
        }
        function Downloaddoc(val) {
            alert(val);
          //  var myData = new FormData();
           // myData.append('id', val);
             var myData = { };
            $.ajax({
                url: 'DocDownload.ashx?id='+ val,
                type: 'POST',
                data: myData,
                success: function (result) {
                   
                    alert("Success : OK" );
                },
                error: function (errorText) {
                    alert("Wwoops something went wrong !");
                }
            });
        }


        function SetInputBox() {
            $('#btnUploadFile').attr('disabled', true);
        }
        function ROCheck(source, args) {
            var ddlCountry = document.getElementById("<%=ddlROList.ClientID%>");
            var txtROSearch = ddlCountry.options[ddlCountry.selectedIndex].value;
            if (txtROSearch == "0") {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function checkValidFile(input) {
            $("#btnUploadFile").prop('disabled', false);
            var ddlCountry = document.getElementById("<%=ddlROList.ClientID%>");
            var txtROSearch = ddlCountry.options[ddlCountry.selectedIndex].value;
            if (txtROSearch != "0" || txtROSearch != "") {
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


        $("body").on("click", "#btnUploadFile", function () {

            alert("AAAA");
            msg = {
                userid: $("#ContentPlaceHolder1_lbluserid").html(),
                roid: $("#ContentPlaceHolder1_ddlROList").val(),
                ronumber: $("#ContentPlaceHolder1_ddlROList").text(),
                doctypeid: $("#ContentPlaceHolder1_ddlvrType").val(),
                ext: extension,
                port: "1100",
                remarks: $("#ContentPlaceHolder1_txtRemarks").val(),
                datacontent: ""//files
            };

            var input = $('#fileUpload')[0].files[0];//document.getElementById('fileUpload');
            var filename = document.getElementById('fileUpload').value;
            var extension = filename.replace(/^.*\./, '');
            formdata = new FormData();
            //..var _image = $('#myPhoto')[0].files[0];
            // var data.append("file", input[0]);
            //  var formdata;
            formdata.append('file', input);
            //formdata.append('msg', msg);
            //var msg;
            //var data = new FormData($('form')[0]);
            ////  var files = $("#fileUpload").get(0).files;    
            //alert("length" + input.length);
            //alert (input.filename); 

            //        data.append("file", input[0]);

            //    //var file = input.files[0];
            //     alert(input);
            //     alert(data);
            //var reader = new FileReader();
            //reader.onload = function (e) {
            //    var arrayBuffer = e.target.result;
            //    alert(arrayBuffer);


            //for (let i = 0; i < bytes.length; i++) {
            //    b64[i] =  file.charCodeAt(i);
            //    alert(b64);
            //}




            var jsonData = JSON.stringify(msg);

            var d = new Date();
            var time = d.getHours() + "-" + d.getMinutes() + "-" + d.getSeconds();
            // var guid = $("#ContentPlaceHolder1_txtROSearch").val() + "-" + time;
            $.ajax({
                url: "SaveDocHandler.ashx?id=" + jsonData,
                type: 'POST',
                // data: { jsonData: jsonData, locale: formdata },
                data: formdata,// jsonData, // new FormData($('form')[0]),
                cache: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (result) {
                    alert("Saved");
                    $("#fileProgress").hide();
                    $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                },
                xhr: function () {
                    var fileXhr = $.ajaxSettings.xhr();
                    if (fileXhr.upload) {
                        $("progress").show();
                        fileXhr.upload.addEventListener("progress", function (e) {
                            if (e.lengthComputable) {
                                $("#fileProgress").attr({
                                    value: e.loaded,
                                    max: e.total
                                });
                            }
                        }, false);
                    }
                    return fileXhr;
                }

            });

            //}

        });


    </script>
    <style type="text/css">
        .row {
            margin-bottom: 5px !important;
        }
    </style>
    <%--<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <div class="col-md-10 col-md-offset-1">
                <div class="col-md-12" style="text-align: center">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-12">
                    <div style="display: none">
                        <asp:Label ID="lbluserid" runat="server">

                        </asp:Label>
                    </div>

                </div>

                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-4">
                            Doc Type
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlvrType" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4" style="margin-top: 5px">
                            Enter RO/RO Reference
                        </div>
                        <div class="col-md-5" style="margin-top: 5px">
                            <asp:TextBox ID="txtROSearch" runat="server" CssClass="form-control">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                Display="None" ControlToValidate="txtROSearch" ErrorMessage="Enter RO Number"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-1" style="margin-top: 5px">
                            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="btn btn-sm btn-success" OnClick="btnSearch_Click" />
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Select RO
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlROList" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlROList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator1" Display="Dynamic" ForeColor="Red" runat="server" ErrorMessage="Please Select RO." ControlToValidate="ddlROList" ClientValidationFunction="ROCheck"></asp:CustomValidator>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Remarks
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="6"
                                runat="server" CssClass="form-control" Style="height: 70px !important">

                            </asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtRemarks" ForeColor="Red" ErrorMessage="Remarks Required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <asp:HiddenField ID="hdsave" Value="0" runat="server" />
                <div class="col-md-12" style="margin-top: 9px; padding: 0px">
                    <div class="panel panel-default" style="padding: 0px; margin: 0px">
                        <div class="panel-heading text-center">Upload Documents</div>
                        <div class="panel-body" style="padding-bottom: 9px !important">
                            <div class="form-group">
                                <div class="col-md-12 text-center">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6" style="padding-left: 44px !important; margin-bottom: 13px;">
                                    <input id="btnUploadFile" type="button" value="Upload File" class="btn btn-info" />
                                    <input name="fileUpload" id="fileUpload" type="file" onchange="checkValidFile(this)" />

                                </div>
                                <progress id="fileProgress" style="display: none"></progress>
                                <hr />
                                <span id="lblMessage" style="color: Green"></span>
                                <%--<div class="col-md-6 progressbar progressbar" id="progressbar">
                                    <div id="progresslabel" class="progressbarlabel"></div>
                                </div>--%>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:GridView ID="gv" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false" OnSelectedIndexChanged="gv_SelectedIndexChanged">

                                        <Columns>
                                            <asp:BoundField DataField="RONumber" HeaderText="RO Number"></asp:BoundField>
                                            <asp:BoundField DataField="DocType" HeaderText="Doc Type"></asp:BoundField>

                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                <HeaderStyle Width="40%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppCode" HeaderText="Source">
                                                <HeaderStyle Width="40%" />
                                            </asp:BoundField>


                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Download
                                                </HeaderTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle Width="40px" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ValidationGroup="a" runat="server" ID="DelButton" ImageUrl="~/Content/Images/download.png" OnClick="DelButton_Click" />
                                                   <%-- <asp:ImageButton ValidationGroup="a" runat="server" ID="DelButton" ImageUrl="~/Content/Images/download.png" OnClientClick='<%# "Downloaddoc(" +Eval("ID") + " );" %>' />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>
