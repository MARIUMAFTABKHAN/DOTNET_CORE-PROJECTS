<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GenerateInvoice.aspx.cs" Inherits="ExpressDigital.GenerateInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .row {
            margin-top: 5px !important;
        }
    </style>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <%--  <script src="Scripts/jquery-ui.js"></script>
            <link href="Scripts/jquery-ui.css" rel="stylesheet" />--%>

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
                        buttonImage: 'Content/Images/Calender.png'
                        ,
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

                    $("#ContentPlaceHolder1_txtinvoiceDate").datepicker({
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
                    <h4>&nbsp;&nbsp;Generate Invoices
                    </h4>
                </div>
            </div>
            <div class="col-md-10 col-md-offset-1">
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-4">
                            Invoice Date
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtinvoiceDate" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="txtinvoiceDate" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date Required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Invoice Status
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlInvoiceStatus" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlInvoiceStatus_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Billed</asp:ListItem>
                                <asp:ListItem Value="2">Un-Billed</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Company
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Portal
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlSearchPortal" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            IRO No.
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtIRO" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            External RO
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtRefNumber" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Release Order No
                        </div>

                        <div class="col-md-8">
                            <asp:HiddenField ID="hdID" runat="server"></asp:HiddenField>
                            <asp:TextBox ID="txtSearchReleaseOrder" MaxLength="12" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Date From
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtSearchROMODateFrom" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSearchROMODateFrom" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4" style="padding-top: 2px;">
                            Date To
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtSearchROMODateTo" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSearchROMODateTo" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-4">
                            Dollar Invoice
                        </div>
                        <div class="col-md-8">
                            <asp:CheckBox class="chk" ID="chkDollar" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4" style="height: 200px;">
                            Agency
                        </div>
                        <div class="col-md-8" style="height: 200px; overflow: auto">
                            <asp:CheckBoxList ID="chkagency" class="chk" runat="server" Style="height: 170px; overflow: auto; margin-top: 10px; margin-bottom: 10px; overflow: hidden">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Client
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtClient" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Campaign
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtcampaign" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>


            <div class="form-group">
                <div class="col-md-10 col-md-offset-1" style="margin-top: 10px !important">

                    <div class="col-md-1 col-md-offset-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" Style="width: 75px !important" ValidationGroup="ss" OnClick="btnSearch_Click" />
                    </div>
                    <div class="col-md-1" style="margin-left: 20px !important">
                        <asp:Button ID="btnExecute" runat="server" Text="Genereate" CssClass="btn btn-danger" Style="width: 90px !important; margin-left: -25px" ValidationGroup="s" OnClick="btnExecute_Click" />
                        <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" ConfirmText="Are you sure to execute records ?"
                            TargetControlID="btnExecute" runat="server" />
                    </div>

                    <div class="col-md-1" style="margin-left: 20px !important">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" Style="width: 75px !important; margin-left: -40px" ValidationGroup="s" OnClick="btnCancel_Click" />

                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="col-md-10 col-md-offset-1" style="margin-top: 3px !important">
                    <asp:GridView ID="gv" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                        AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
                        <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:BoundField DataField="ReleaseOrderNumber" HeaderText="ReleaseOrder">
                                <HeaderStyle Width="10%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ROMPDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="RO Date">
                                <HeaderStyle Width="7%" />
                            </asp:BoundField>

                            <asp:TemplateField>
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" />
                                <HeaderTemplate>
                                    Inv-Reference
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox Width="100%" ID="txtInvReference" Text='<%#Eval("invoiceReference")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AgencyName" HeaderText="Agency">
                                <HeaderStyle Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Client" HeaderText="Client">
                                <HeaderStyle Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Compaign" HeaderText="Campaign">
                                <HeaderStyle Width="25%" />
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
                            <%--  <asp:BoundField DataField="INV_Net" HeaderText="Net Receiable">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INV_Net" HeaderText="Net Receiable PKR">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>--%>

                            <asp:BoundField DataField="IsBilled" HeaderText="IsBilled">
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


                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
