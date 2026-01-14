<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FrmDownloadFiles.aspx.cs" Inherits="ExpressDigital.FrmDownloadFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
            <style type="text/css">
                .row {
                    margin-bottom: 5px;
                }
            </style>

            <script type="text/javascript">

                //function Downloaddoc(val) {
                //    $.ajax({
                //        url: 'DocDownload.ashx',
                //        type: 'POST',
                //        data: 'id=' + val,
                //        success: function (data) {
                //            //console.log(data);
                //            // alert("Success :" + data);
                //        },
                //        error: function (errorText) {
                //            alert("Wwoops something went wrong !");
                //        }
                //    });
                //}

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
                    <div style="display: none">
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
                            <a href="~/SrcFile/eScanner.msi">Download Files</a>
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
                        <div class="col-md-12">
                            <asp:GridView ID="gv" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false">
                                                               
                                <Columns>
                                    <asp:BoundField DataField="RONumber" HeaderText="RO Number">                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DocType" HeaderText="Doc Type">                                        
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                           <HeaderStyle Width="40%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AppCode" HeaderText="Source">
                                           <HeaderStyle Width="40%" />
                                    </asp:BoundField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            View
                                        </HeaderTemplate>
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ValidationGroup="a" runat="server" ID="ViewButton" ImageUrl="~/Content/Images/View_16x16.png" OnClick="ViewButton_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Download
                                        </HeaderTemplate>
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ValidationGroup="a" runat="server" ID="DelButton" ImageUrl="~/Content/Images/download.png" OnClick="DelButton_Click" />
                                            <%--<asp:ImageButton ValidationGroup="a" runat="server" ID="DelButton" ImageUrl="~/Content/Images/download.png" OnClientClick='<%# "Downloaddoc(" +Eval("ID") + " );" %>' />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <%--   <div class="row">
                        <div class="col-md-6">
                            <asp:Button ID="btnScan" OnClientClick="Downloaddoc();" Style="width: 200px !important" runat="server" Text="Start Scanning" CssClass="btn btn-success" />
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-12" style="height: 550px; width: 100%; border: 1px solid gray; padding: 5px">
                            <asp:Image ID="imgPic" runat="server" Width="100%" Height="100%" />
                        </div>
                    </div>
                </div>

                <div class="col-md-10 col-md-offset-1">
                </div>
            </div>
      
</asp:Content>
