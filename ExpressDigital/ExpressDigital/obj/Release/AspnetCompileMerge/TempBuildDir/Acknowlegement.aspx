<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Acknowlegement.aspx.cs" Inherits="ExpressDigital.Acknowlegement" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


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

                    $("#ContentPlaceHolder1_txtPrintDate").datepicker({
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

                .row {
                    margin-bottom: 5px !important;
                }
            </style>
            <div class="col-md-10 col-md-offset-1">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div>
                    <h4>&nbsp;&nbsp; Print Acknowledge
                    </h4>
                </div>

            </div>
            <div class="col-md-10 col-md-offset-1">
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-4">
                            Company
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control">
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
                            <asp:TextBox ID="txtSearchReleaseOrder" MaxLength="13" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Start Date
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchROMODateTo" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4" style="padding-top: 2px;">
                            Campaign
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtcampaign" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-4 col-md-offset-4">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" Style="width: 75px !important" ValidationGroup="s" OnClick="btnSearch_Click" />

                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" Style="width: 75px !important; margin-left: -40px" ValidationGroup="s" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-3">
                            Print Date
                        </div>
                        <div class="col-md-7">
                            <asp:CheckBox class="chk" ID="chkDollar" runat="server" Text="Dollar Invoice" Visible="false" />
                            <asp:TextBox ID="txtPrintDate" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3" style="height: 200px;">
                            Agency
                        </div>
                        <div class="col-md-7" style="height: 200px; overflow: auto">
                            <asp:CheckBoxList ID="chkagency" class="chk" runat="server" Style="height: 170px; overflow: auto; margin-top: 10px; margin-bottom: 10px; overflow: hidden">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            Client
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtClient" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            Combine
                        </div>
                        <div class="col-md-7">
                            <asp:CheckBox ID="chkAll" class="chk" Text="Combine" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-10 col-md-offset-1" style="margin-top: 3px !important; height: auto">
                <asp:GridView ID="gv" PageSize="50" runat="server" CssClass="EU_DataTable" DataKeyNames="ID, CompnayID,AgencyID, InvoiceID" AutoGenerateColumns="false"
                    AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:BoundField DataField="InvoiceID" HeaderText="Invoice ID">
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>

                        <asp:BoundField DataField="AgencyName" HeaderText="Agency">
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
                                <center>
                                    <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/Printer.png' ValidationGroup="a" OnClick="btnprint_Click" />
                                </center>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-10 col-md-offset-1">
                <rsweb:ReportViewer ID="ReportViewer1" Width="100%" runat="server"></rsweb:ReportViewer>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
