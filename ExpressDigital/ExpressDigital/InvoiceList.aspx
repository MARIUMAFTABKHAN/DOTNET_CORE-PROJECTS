<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InvoiceList.aspx.cs" Inherits="ExpressDigital.InvoiceList" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
               <script src="Scripts/jquery-ui.js"></script>
            <link href="Scripts/jquery-ui.css" rel="stylesheet" />
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
            <script type="text/javascript">

                var list = [];

                var gridViewId = '#<%= gv.ClientID %>';
                function checkAll(selectAllCheckbox) {
                    //get all checkboxes within item rows and select/deselect based on select all checked status
                    //:checkbox is jquery selector to get all checkboxes

                    $('td :checkbox', gridViewId).prop("checked", selectAllCheckbox.checked);

                    Totalsize = $('.chk'), length;


                    // updateSelectionLabel();
                }

                var Totalsize
                function unCheckSelectAll(selectCheckbox) {

                    Totalsize = $('.chk').length;
                    list = [];
                    if (selectCheckbox.checked) {
                        list = [];
                        $('#ContentPlaceHolder1_gv input:checked').each(function () {
                            list.push(this.name);
                        });
                        Totalchk = list.length;
                    }
                    else {
                        list = [];
                        $('#ContentPlaceHolder1_gv input:checked').each(function () {
                            list.push(this.name);

                        });
                        Totalchk = list.length - 1;
                    }

                    if (Totalsize == Totalchk) {

                        $('th :checkbox', gridViewId).prop("checked", true);
                    }
                    else {
                        $('th :checkbox', gridViewId).prop("checked", false);
                    }
                }

            </script>
            <style type="text/css">
                .chk label {
                    margin-left: 10px !important;
                }
            </style>
            <div class="col-md-10 col-md-offset-1">
                <div class="div-md-12 text-center">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div>
                    <h4>&nbsp;&nbsp;&nbsp;&nbsp; Print Invoices
                    </h4>
                </div>

            </div>
            <div class="col-md-10 col-md-offset-1">
                <div class="form-group">
                    <div class="col-md-12" style="margin-bottom: 3px !important">
                        <div class="col-md-2 col-md-offset-2">
                            <asp:CheckBox class="chk" ID="chkDollar" runat="server" Text="Dollar Invoice" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-bottom: 3px !important">
                        <div class="col-md-2">
                            Company
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Portal
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlSearchPortal" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            Invoice Status
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlInvoiceStatus" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlInvoiceStatus_SelectedIndexChanged">
                                <asp:ListItem Value="1">Billed</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            IRO No.
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtIRO" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">
                        <div class="col-md-2">
                            Release Order No
                        </div>
                        <div class="col-md-3">
                            <asp:HiddenField ID="hdID" runat="server"></asp:HiddenField>
                            <asp:TextBox ID="txtSearchReleaseOrder" MaxLength="12" runat="server" CssClass="form-control"></asp:TextBox>
                           
                        </div>
                        <div class="col-md-2">
                            External RO
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtRefNumber" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 0px !important">
                        <div class="col-md-2">
                            Agency
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtAgency" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Client
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtClient" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">
                        <div class="col-md-2">
                            Campaign
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtcampaign" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">
                        <div class="col-md-2">
                            Date From
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateFrom" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-2" style="padding-top: 2px;">
                            Date To
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateTo" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">

                        <div class="col-md-1 col-md-offset-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" Style="width: 75px !important" ValidationGroup="s" OnClick="btnSearch_Click" />
                        </div>
                        <%--<div class="col-md-1" style="margin-left: 20px !important">
                            <asp:Button ID="btnExecute" runat="server" Text="Genereate" CssClass="btn btn-danger" Style="width: 90px !important; margin-left: -25px" ValidationGroup="s" OnClick="btnExecute_Click" />
                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" ConfirmText="Are you sure to execute records ?"
                                TargetControlID="btnExecute" runat="server" />
                        </div>--%>

                        <div class="col-md-1" style="margin-left: 50px !important">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" Style="width: 75px !important; margin-left: -40px" ValidationGroup="s" OnClick="btnCancel_Click" />

                        </div>

                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID, CompnayID" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
                            <PagerStyle  HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField DataField="ReleaseOrderNumber" HeaderText="ReleaseOrder">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ROMPDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="RO Date">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Compaign" HeaderText="Compaign">
                                    <HeaderStyle Width="30%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="INV_Gross" HeaderText="Gross">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INV_AGC" HeaderText="AG Commssion">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="INV_Discount" HeaderText="Discount">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INV_GST" HeaderText="G.S.Tax">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INV_Net" HeaderText="Net Amount">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="InvoiceBuilds" HeaderText="Billed">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="InvoiceCancelled" HeaderText="Cancelled">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>


                                <asp:TemplateField>
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle Width="40px" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" CssClass="myCheckbox" onclick="checkAll(this);" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <center>      
                                                <asp:CheckBox id="chk" runat="server" CssClass="chk" onclick="unCheckSelectAll(this);">
                                                </asp:CheckBox> 
                                            </center>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    
                                         <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="btn btn-sm btn-success" OnClick="btnprint_Click1"  />
                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="form-group">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>


                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
