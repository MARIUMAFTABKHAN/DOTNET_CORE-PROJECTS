<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PrintInvoices.aspx.cs" Inherits="ExpressDigital.PrintInvoices" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%--   <script src="Scripts/jquery-ui.js"></script>
            <link href="Scripts/jquery-ui.css" rel="stylesheet" />--%>
            <script type="text/javascript" language="javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                function EndRequestHandler(sender, args) {
                    if (args.get_error() != undefined) {
                        //  **alert(args.get_error().message.substr(args.get_error().name.length + 2));
                        args.set_errorHandled(true);
                    }
                }
            </script>
            <script type="text/javascript">
                function pageLoad() {
                    applyDatePicker();

                }
                function applyDatePicker() {

                    $("#ContentPlaceHolder1_txtSearchROMODateFrom").datepicker({
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

                }
            </script>

            <style type="text/css">
                .chk label {
                    margin-left: 10px !important;
                }
            </style>
            <div class="col-md-10 col-md-offset-1">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div>
                    <h4>&nbsp;&nbsp;&nbsp;&nbsp; View All CRV
                    </h4>
                </div>

            </div>
            <div class="col-md-10 col-md-offset-1">
                 <div class="form-group">
                    <div class="col-md-12" style="margin-top: 5px !important">
                        <div class="col-md-2">
                            Company 
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2" style="display:none">
                            Status
                        </div>
                        <div class="col-md-2">
                           <%-- <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control">                              
                            </asp:DropDownList>--%>
                        </div>
                    </div>
                </div>


                 <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">
                         <div class="col-md-2">
                            City
                        </div>
                         <div class="col-md-3">
                            <asp:DropDownList ID="ddlCity" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

                       <div class="col-md-2">
                            Date From
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateFrom" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtSearchROMODateFrom" ForeColor="Red" runat="server" ErrorMessage="Required: From Date"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
               
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 5px !important">
                         <div class="col-md-2">
                            Master Agency
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlmasteragency" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlmasteragency_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                        </div>
                          
                        <div class="col-md-2" style="padding-top: 2px;">
                            Date To
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateTo" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="Required: To Date" ForeColor="Red" ControlToValidate="txtSearchROMODateTo"></asp:RequiredFieldValidator>
                        </div>
                     
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 5px !important">
                          <div class="col-md-2">
                            Agency
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlAgency" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                        </div>
                      
                       <div class="col-md-2">
                            Invoice ID
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtinvoiceid"  runat="server"  CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                               ControlToValidate="txtinvoiceid" runat="server" 
                                   ErrorMessage="Only Numbers allowed"
                                         ValidationExpression="\d+" Display="Dynamic"  >
                                </asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                 <div class="form-group">
                    <div class="col-md-12" style="margin-top: 5px !important">
                         
                          
                        <div class="col-md-2">
                            Client
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlClients" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        
                        <div class="col-md-2">
                            Status
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                    
                     
                    </div>
                </div>
                    
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">

                        <div class="col-md-1 col-md-offset-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" Style="width: 75px !important" ValidationGroup="s" OnClick="btnSearch_Click" />
                        </div>                     

                        <div class="col-md-1" style="margin-left: 10px !important">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" Style="width: 75px !important; margin-left: -40px" ValidationGroup="s" OnClick="btnCancel_Click" />

                        </div>

                        <div class="col-md-1" style="margin-left:10px !important;">
                            <asp:Button ID="btnexport" runat="server" Text="Export To Excel" CssClass="btn btn-success" Style="width: 120px !important" ValidationGroup="s" OnClick="btnexport_Click" />
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-12">

                <div class="col-md-12" style="margin-top: 4px !important; height: 600px">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%"></rsweb:ReportViewer>
                     
                </div>
            </div>

        </ContentTemplate>
                 <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
         </Triggers>
    </asp:UpdatePanel>
</asp:Content>
