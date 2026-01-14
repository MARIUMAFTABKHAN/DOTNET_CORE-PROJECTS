<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AgingReport.aspx.cs" Inherits="ExpressDigital.AgingReport " %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="up" runat="server">--%>
        <ContentTemplate>
          <script type="text/javascript" language="javascript">
                void showPleaseWait()
                {
                    document.getElementById('PleaseWait').style.display = 'block';
              }

              function pageLoad() {
                  applyDatePicker();

              }

              function applyDatePicker() {

                  $("#ContentPlaceHolder1_txttilldate").datepicker({
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
                            </div>
                        </div>
                        <h4>Aging Report
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
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                Till Date:
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txttilldate" runat="server" CssClass="form-control" Style="width: 72%; display: inline;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                
                    <br />

                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-4">
                                <%--<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="btn btn-sm btn-success" OnClick="btnSearch_Click"/>--%>
                                <asp:Button ID="btnShowData" runat="server"
                                    BorderStyle="Double" BorderWidth="2px" Font-Bold="True" Height="35px"
                                   OnClientClick="showPleaseWait()" Text="Show Data" Width="101px" OnClick="btnShowData_Click" CssClass="btn btn-sm btn-success" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
