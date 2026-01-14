<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FrmUploadFiles.aspx.cs" Inherits="ExpressDigital.FrmUploadFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .row {
            margin-bottom: 5px;
        }
    </style>

    <script type="text/javascript">
        //function Downloaddoc()
        //{            
        //     $.ajax({
        //                    url: 'DocDownload.ashx',
        //                    type: 'POST',
        //                    data: 'id='+ 110000007,
        //                    success: function (data) {
        //                        console.log(data);
        //                        alert("Success :" + data);
        //                    },
        //                    error: function (errorText) {
        //                        alert("Wwoops something went wrong !");
        //                    }
        //                });
        //}

        var selDiv = "";
        var storedFiles = [];

        $(document).ready(function () {
            selDiv = $("#selectedFiles");
            $("body").on("click", ".selFile", editFiles);
        });

        var start = function () {
            var i = 0;

            var wsImpl = window.WebSocket || window.MozWebSocket;

            window.ws = new wsImpl('ws://localhost:8182');
            ws.onmessage = function (e) {
               
                if (typeof e.data === "string") {
                    //IF Received Data is String
                }
                else if (e.data instanceof ArrayBuffer) {
                    //IF Received Data is ArrayBuffer
                }
                else if (e.data instanceof Blob) {

                    i++;

                    var f = e.data;

                    f.name = "File" + i;

                    storedFiles.push(f);

                    var reader = new FileReader();

                    reader.onload = function (e) {
                        var html = "<div class=\"col-sm-12 text-center\" style=\"border: 1px solid black; margin-left: 2px;\"><img height=\"600px\" width=\"400px\" src=\"" + e.target.result + "\" data-file='" + f.name + "' class='selFile' title='Click to remove'><br/>" + i + "</div>";
                        var b64 = reader.result.replace(/^data:.+;base64,/, '');
                        var arrayBuffer = e.target.result;
                       // alert( arrayBuffer);
                        var msg = {
                            userid:$("#ContentPlaceHolder1_lbluserid").html(),
                            roid: $("#ContentPlaceHolder1_ddlROList").val(),
                            ronumber: $("#ContentPlaceHolder1_ddlROList").text(),
                            doctypeid: $("#ContentPlaceHolder1_ddlvrType").val(),                           
                            ext: ".jpg",
                            port: "1100",
                            remarks:$("#ContentPlaceHolder1_txtRemarks").val(),
                            datacontent: b64                            
                        };
                        var jsonData = JSON.stringify(msg);

                        $.ajax({
                         //   url: 'SaveDocHandler.ashx',
                            url: 'ScanHandler.ashx',                            
                            type: 'POST',
                            data: jsonData,
                            success: function (data) {
                                console.log(data);
                                alert("Success :" + data);
                            },
                            error: function (errorText) {
                                alert("Wwoops something went wrong !");
                            }
                        });
                        selDiv.append(html);
                    }
                    reader.readAsDataURL(f);
                }
            };
            ws.onopen = function () {
                //Do whatever u want when connected succesfully
            };
            ws.onclose = function () {
                $('.dalert').modal('show');
            };
        }
        window.onload = start;

        function scanImage() {
            //alert($("#ContentPlaceHolder1_txtRemarks").val().length);
           //  alert(msg.ronumber);
           
            if ($("#ContentPlaceHolder1_txtRemarks").val().length  == 0) {
            
                alert("Please provide complete inofrmation");
                return;
            }
            else {
               //    alert("Sending");
                //ws.send(JSON.stringify(msg));
                ws.send("1100");
            }
        }

        function editFiles(e) {
            var file = $(this).data("file");
            for (var i = 0; i < storedFiles.length; i++) {
                if (storedFiles[i].name === file) {
                    $('.scandetail').modal('show');
                    var c = document.getElementById("myCanvas");
                    var ctx = c.getContext("2d");
                    var img = new Image();
                    img.src = window.URL.createObjectURL(storedFiles[i]);
                    img.onload = function () {
                        c.width = img.width;
                        c.height = img.height;
                        ctx.drawImage(img, 0, 0, img.width, img.height);
                    }
                    break;

                }
            }
        }

    </script>
    <style type="text/css">
        .row {
            margin-bottom: 5px !important;
        }
    </style>
    <div class="col-md-10 col-md-offset-1">
        <div class="col-md-12" style="text-align: center">
            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="col-md-12">
            <div style="display:none">
                <asp:Label ID="lbluserid" runat="server">

                </asp:Label>
            </div>
            <div class="modal fade scandetail" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div style="max-height: 800px; overflow: scroll;">
                            <canvas id="myCanvas"></canvas>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="modal fade dalert" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            Connection Failed           
                        </div>
                        <div class="modal-body">
                            No Scan app application found in your machine please download,install and open first then refresh the browser.
                            <a href="../SrcFile/eScanner-V-1.0.0.0.msi">Download Files</a>
                        </div>
                    </div>
                </div>
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
                    <asp:DropDownList ID="ddlROList" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>

            </div>
            <div class="row">
                <div class="col-md-4">
                    Remarks
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="6" Height="150px"
                        runat="server" CssClass="form-control">

                    </asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="btnScan" OnClientClick="scanImage();" Style="width: 200px !important" runat="server" Text="Start Scanning" CssClass="btn btn-success" />
                </div>
            </div>
            <div class="row">
                <div id="selectedFiles" class="col-md-12" style="height: 600px; width: 100%" />
            </div>
        </div>
      <%--  <div class="col-md-12">
            <asp:Button ID="Button1" Style="width: 200px !important" runat="server" Text="Download" CssClass="btn btn-success" OnClick="vtb_Click"  OnClientClick="Downloaddoc()" />
        </div>--%>
        <%-- <div class="col-md-12">
             <asp:Image ID="imgPic" runat="server" Width="400" Height="600" />
         </div>--%>
        

    </div>
</asp:Content>
