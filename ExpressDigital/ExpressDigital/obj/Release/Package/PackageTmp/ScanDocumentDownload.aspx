<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ScanDocumentDownload.aspx.cs" Inherits="ExpressDigital.ScanDocumentDownload " %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="up" runat="server">--%>
        <ContentTemplate>
          
            <div class="form-horizontal">
                <div class="col-md-10 col-md-offset-1">
                        <div class="form-group">
                            <div class="div-md-12 text-center">
                                <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                        <h4>Scan Document Download
                        </h4>
                        <div class="form-group">
                            <div class="div-md-12">
                                 <div style="display: none">
                                    <asp:Label ID="lbluserid" runat="server"></asp:Label>
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
                     </div>
                    <br />
                    <div class="form-group">
                        <div class="div-md-12">
                           <div class="modal fade dalert" role="dialog">
                                <div class="modal-dialog modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            Connection Failed
                                        </div>
                                        <div class="modal-body">
                                            No Scan app application found in your machine please download,install and open first
                                            then refresh the browser. <a href="~/SrcFile/eScanner.msi">Download Files</a>
                                        </div>
                                    </div>
                                </div>
                          </div>
                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="row">
                                <div class="col-md-4">
                                    Document Type:
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="form-control" >
                                        <asp:ListItem Selected="True" Value="RO">Release Order</asp:ListItem>
                                        <asp:ListItem Value="ACLetter">Acknowledgment letter</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4" style="margin-top: 5px">
                                    Enter NO:
                                    <%--<asp:Label ID="lblTextToShow" runat="server" Text="Label"></asp:Label>--%>
                                </div>
                                <div class="col-md-5" style="margin-top: 5px">
                                    <asp:TextBox ID="txtPOSearch" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-1" style="margin-top: 5px">
                                    <asp:Button ID="btnsearch" runat="server" Text="Go" CssClass="btn btn-sm btn-success" OnClick="btnsearch_Click"/>
                                </div>
                             </div>
                        </div>
                            <br />
                            <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gv" runat="server" CssClass="EU_DataTable" DataKeyNames="ID, RONumber"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="RONumber" HeaderText="RO Number"></asp:BoundField>
                                <asp:BoundField DataField="DocumentType" HeaderText="Document Type"></asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                    <HeaderStyle Width="30%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AppCode" HeaderText="Source">
                                    <HeaderStyle Width="30%" />
                                </asp:BoundField>
                                
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Download
                                    </HeaderTemplate>
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ValidationGroup="a" Visible="false" runat="server" ID="DelButton"
                                            ImageUrl="~/Content/Images/download.png" OnClick="DelButton_Click" />
                                        <asp:HyperLink ID="hypViews" runat="server" ImageUrl="~/Content/Images/download.png" NavigateUrl='<%# MakeDownloadFileURL(Convert.ToString(DataBinder.Eval(Container.DataItem,"RoNumber")))%>'
                                            Text="View"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                    <br />
                    

                </div>
            </div>
                    </div>
                </div>
        </ContentTemplate>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
