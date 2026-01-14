<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InvoiceUpDate.aspx.cs" Inherits="ExpressDigital.InvoiceUpDate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>

            <script type="text/javascript" language="javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                function EndRequestHandler(sender, args) {
                    if (args.get_error() != undefined) {
                        //  **alert(args.get_error().message.substr(args.get_error().name.length + 2));
                        args.set_errorHandled(true);
                    }
                }
            </script>
            <script src="Scripts/jquery-ui.js"></script>
            <link href="Scripts/jquery-ui.css" rel="stylesheet" />

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

                    Totalsize = $('.chk').size();


                    // updateSelectionLabel();
                }

                var Totalsize
                function unCheckSelectAll(selectCheckbox) {

                    Totalsize = $('.chk').size();
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
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div>
                    <h4>&nbsp;&nbsp;&nbsp;&nbsp; Cancel Invoices
                    </h4>
                </div>
                <div class="col-md-2 col-md-offset-2" style="display: none">
                    <asp:CheckBox class="chk" ID="chkDollar" runat="server" Text="Dollar Invoice" />

                </div>
            </div>
            <div class="col-md-10 col-md-offset-1">

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
                            Vr.Created
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlInvoiceStatus" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlInvoiceStatus_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                <asp:ListItem Value="2">Yes</asp:ListItem>
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
                            Invoice ID
                        </div>
                        <div class="col-md-3">
                            <asp:HiddenField ID="hdID" runat="server"></asp:HiddenField>
                            <asp:TextBox ID="txtSearchReleaseOrder" MaxLength="9" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                ControlToValidate="txtSearchReleaseOrder" ForeColor="Red" runat="server" ErrorMessage="*"
                                ValidationExpression="\d+" Display="Dynamic" ValidationGroup="s"></asp:RegularExpressionValidator>
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSearchROMODateFrom" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-2" style="padding-top: 2px;">
                            Date To
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateTo" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchROMODateTo" ErrorMessage="*"></asp:RequiredFieldValidator>
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
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" ConfirmText="Are you sure to execute records ?"
                                TargetControlID="btnExecute" runat="server" />
                        </div>

                        <div class="col-md-1" style="margin-left: 20px !important">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" Style="width: 75px !important; margin-left: -40px" ValidationGroup="s" OnClick="btnCancel_Click" />

                        </div>--%>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-12" style="margin-top: 3px !important">
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="InvoiceID" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField DataField="ReleaseOrderNumber" HeaderText="Release Order">
                                    <HeaderStyle Width="12%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="VrNumber" HeaderText="Vr. Number">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="InvoiceID" HeaderText="Invoice ID">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                    <HeaderTemplate>
                                        Inv-Reference
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Width="100%" ID="txtInvReference" Text='<%#Eval("invoiceReference")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Compaign" HeaderText="Compaign">
                                    <HeaderStyle Width="25%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="INV_Gross" HeaderText="Gross">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INV_AGC" DataFormatString="{0:N2}" HeaderText="AG Commssion">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="INV_Discount" DataFormatString="{0:N2}" HeaderText="Discount">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INV_GST" DataFormatString="{0:N2}" HeaderText="G.S.Tax">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INV_Net" DataFormatString="{0:N2}" HeaderText="Net Amount">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <%--  <asp:BoundField DataField="INV_Net" HeaderText="Net Receiable">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INV_Net" HeaderText="Net Receiable PKR">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>--%>

                                <asp:BoundField DataField="IsCancelled" HeaderText="Vr.Created">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UserName" HeaderText="Created By">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CancelledOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Created On">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>


                                <asp:TemplateField>
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle Width="7%" />
                                    <HeaderTemplate>
                                        Cancel
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnremove" OnClick="btnremove_Click" ImageUrl="~/Content/Images/Trash-Cancel.ico" runat="server" />
                                        <ajaxToolkit:ConfirmButtonExtender ConfirmText="Are you sure to Cancel this Invoice ?" TargetControlID="btnremove" ID="ConfirmButtonExtender2" runat="server"></ajaxToolkit:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
