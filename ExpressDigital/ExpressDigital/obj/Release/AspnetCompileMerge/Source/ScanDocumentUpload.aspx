<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ScanDocumentUpload.aspx.cs" Inherits="ExpressDigital.ScanDocumentUpload " %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="up" runat="server">--%>
        <ContentTemplate>
            
            <script type="text/javascript">
                function pageLoad() {
                    applyDatePicker();

                }
                function applyDatePicker() {

                    $("#ContentPlaceHolder1_txtFromDate").datepicker({
                        showOn: 'button',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        buttonImage: 'Content/Images/Calender.png',
                        dateFormat: 'dd/mm/yy'
                    });

                    $("#ContentPlaceHolder1_txtToDate").datepicker({
                        showOn: 'button',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        buttonImage: 'Content/Images/Calender.png',
                        dateFormat: 'dd/mm/yy'
                    });

                }
            </script>
            <div class="form-horizontal">
                <div class="col-md-10 col-md-offset-1">
                    <div class="form-group">
                        <div class="div-md-12 text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                            <asp:HiddenField ID="hdRONumber" runat="server" />
                            <asp:HiddenField ID="hdReleaseOrderId" runat="server" />
                            <asp:HiddenField ID="hdPODetailID" runat="server" />
                            <asp:HiddenField ID="hdCreatedBy" runat="server" />
                        </div>
                    </div>
                    <h4>Scan Document Upload
                    </h4>
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                From:
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Style="width: 72%; display: inline;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                To:
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Style="width: 87%; display: inline;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                Document Type:
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlDocumentType" AutoPostBack="true"  runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="RO">Release Order</asp:ListItem>
                                    <asp:ListItem Value="ACLetter">Acknowledgment letter</asp:ListItem>
                                 </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                <asp:Label ID="lblTextToShow" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtPONumber" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="btn btn-sm btn-success" OnClick="btnSearch_Click"/>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                Select File:
                            </div>
                            <div class="col-md-10">
                                <asp:FileUpload ID="FileUpload1" Enabled="false" style="float: left;"  runat="server" AllowMultiple="true"/>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                Remarks:
                            </div>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtRemarks" runat="server" Style="height: 60px !important; max-width: 100%" Rows="3" TextMode="MultiLine" MaxLength="250" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-10">
                                <asp:Button ID="btnUpload" Enabled="false" runat="server" Text="Upload File" CssClass="btn btn-sm btn-success" OnClick="btnUpload_Click"/>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
