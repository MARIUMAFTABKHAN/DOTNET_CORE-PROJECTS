<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="frmGLPostings.aspx.cs" Inherits="ExpressDigital.frmGLPostings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<script src="Scripts/jquery-ui.js"></script>
    <link href="Scripts/jquery-ui.css" rel="stylesheet" />--%>

    <script type="text/javascript">          
        function pageLoad() {
            //  $('input[type=file]').bootstrapFileInput();
            applyDatePicker();

        }
        function applyDatePicker() {
            $("#ContentPlaceHolder1_txtDate").datepicker({
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

            $("#ContentPlaceHolder1_txtVrDate").datepicker({
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
        .btn {
            height: 35px;
            width: 120px;
        }

        .row {
            margin-bottom: 5px;
        }

        .ui-datepicker-trigger {
            margin-top: 5px;
        }

        .checkboxes label {
            display: block;
            float: right;
            padding-right: 10px;
            white-space: nowrap;
            width: 250px;
            margin-top: 7px;
            margin-left: 10px;
        }

        .checkboxes input {
            vertical-align: middle;
        }

        .checkboxes label span {
            vertical-align: middle;
        }
    </style>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-10 col-md-offset-1">
                    <div class="row">
                        <div class="col-md-12" style="background-color: #CCC; margin-bottom: 5px">
                            <div class="col-md-4" style="background-color: #CCC">
                                <h4>Voucher No:&nbsp;</h4>
                            </div>
                            <div class="col-md-2" style="background-color: #CCC">
                                <h3>
                                    <asp:Label ID="lblvoucherNo" runat="server"></asp:Label></h3>
                            </div>
                            <div class="col-md-4" style="background-color: #CCC">
                                <h4 style="text-align: center">Journal Voucher Posting
                                </h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>From Date :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Style="width: 70%; display: inline; float: left">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                        ForeColor="Red" ControlToValidate="txtDate" Display="Dynamic"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>To Date :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Style="width: 70%; display: inline; float: left">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                        ForeColor="Red" ControlToValidate="txtToDate" Display="Dynamic"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>Channel :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlchannel" runat="server" CssClass="form-control" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlchannel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>Cost Center :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlcostCenter" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>Company :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlGroupCompany" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlGroupCompany_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>Department :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>Station :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlStations" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>Invoice Type :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlInvoiceType" runat="server" CssClass="form-control" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlInvoiceType_SelectedIndexChanged">
                                        <asp:ListItem Selected="True">Invoice</asp:ListItem>
                                        <asp:ListItem>Debit</asp:ListItem>
                                        <asp:ListItem>Credit</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>Fiscal Year :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlFiscalyear" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>Vr.Date :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtVrDate" runat="server" CssClass="form-control" Style="width: 70%; display: inline; float: left">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                        ForeColor="Red" ControlToValidate="txtVrDate" Display="Dynamic"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>Nature of Account:</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlRevenueAccount" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>AG Commission Acc :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlAgencyComm" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>Sales Tax Account:</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSalesTaxAccount" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2" style="padding-top: 8px;">
                                    <strong>Remarks :</strong>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        Height="50" Rows="4" MaxLength="50">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                        ForeColor="Red" ControlToValidate="txtRemarks" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divReasonNote" runat="server">
                        <div class="col-md-10 col-md-offset-1" style="background-color: #CCC">
                            <h4>Adjustment Reasons
                            </h4>
                        </div>
                        <div class="col-md-10 col-md-offset-1">
                            <asp:RadioButtonList ID="chkReasonNote" CssClass="checkboxes" CellPadding="10" runat="server"
                                RepeatColumns="3" RepeatDirection="Horizontal" BorderStyle="Solid">
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row" style="text-align: center">
                        <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-md-10 col-md-offset-2">
                            <div class="col-md-2">
                                <asp:UpdateProgress ID="pg" AssociatedUpdatePanelID="up" runat="server">
                                    <ProgressTemplate>
                                        <img src="Content/Images/loading6.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <div class="col-md-10 col-md-offset-2">
                                <div class="col-md-2" style="padding-left: 0px; margin-left: 10px">
                                    <asp:Button ID="btnPrint" ValidationGroup="A" Text="Print " Width="100%" runat="server"
                                        CssClass="btn btn-success" OnClick="btnPrint_Click1" />
                                </div>
                                <div class="col-md-2" style="padding-left: 0px; margin-left: 10px">
                                    <asp:Button ID="btnshow" Text="Get Data"
                                        Width="100%" runat="server"  CssClass="btn btn-success" OnClick="btnshow_Click" />
                                </div>
                                <div class="col-md-2" style="padding-left: 0px; margin-left: 10px;">
                                    <asp:Button ID="btnGenerateVouher" Width="100%" Text="Generate JV" runat="server"
                                        CssClass="btn btn-success" OnClick="btnGenerateVouher_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="divUpdateAccountHead" style="padding-left: 17px; padding-right: 17px;">
                        <div class="col-md-10 col-md-offset-1" style="background-color: #CCC; padding-top: 12px;">
                            <div class="col-md-4" style="padding-left: 0px; margin-left: -5px; padding-top: 5px">
                                <b>
                                    <asp:Label ID="lblGroupAgency" runat="server" Text=""></asp:Label>
                                </b>
                            </div>
                            <div class="col-md-4" style="padding-left: 0px; margin-left: -5px">
                                <asp:HiddenField ID="hdheadaccount" runat="server" />
                                <asp:TextBox ID="txtHeadAccount" runat="server" CssClass="form-control"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ValidationGroup="AG" ControlToValidate="txtHeadAccount"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px; margin-left: -5px">
                                <asp:Button ID="btnupdateaccounthead" ValidationGroup="AG" Text="Up-Date" runat="server"
                                    CssClass="btn success" OnClick="btnupdateaccounthead_Click1" />
                            </div>
                            <div class="col-md-2" style="padding-left: 0px; margin-left: -5px">
                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn danger" OnClick="btnCancel_Click1" />
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="divinvoice">
                        <div class="col-md-10 col-md-offset-1">
                            <asp:GridView ID="gv" runat="server" CssClass="EU_DataTable" AutoGenerateColumns="false"
                                AllowPaging="false" OnRowDataBound="gv_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Description
                                        </HeaderTemplate>
                                        <HeaderStyle Width="40%" />
                                        <ItemTemplate>
                                            <headerstyle width="15%" />
                                            <asp:Label ID="lblGroupAgency" runat="server" Text='<%#Eval("GroupAgency") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Debit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <headerstyle width="15%" />
                                            <asp:Label ID="lblDebit" runat="server" Text='<%#Eval("Debit") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Credit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <headerstyle width="15%" />
                                            <asp:Label ID="lblCredit" runat="server" Text='<%#Eval("Credit") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <headerstyle width="15%" />
                                            <asp:Label ID="lblAccountHead" runat="server" Text='<%#Eval("AccountHead") %>'></asp:Label>
                                            <asp:HiddenField ID="hdheadaccount" runat="server" Value='<%#Eval("AccountHead") %>' />
                                            <asp:HiddenField ID="hdcompanyid" runat="server" Value='<%#Eval("CompnayID") %>' />
                                            <asp:HiddenField ID="hdGroupagencyID" runat="server" Value='<%#Eval("GroupagencyID") %>' />
                                            <asp:HiddenField ID="hdInvoiceID" runat="server" Value='<%#Eval("InvoiceID") %>' />
                                            <asp:HiddenField ID="hdInvoiceStatus" runat="server" Value='<%#Eval("InvoiceStatus") %>' />

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="5%" />
                                        <HeaderTemplate>
                                            Edit
                                        </HeaderTemplate>
                                        <ItemStyle Width="5%" />
                                        <ItemTemplate>
                                            <asp:Button Style="text-align: center; width: 100%; height: 22px" runat="server" Text="Edit" ID="btnAdd" CssClass="btn-danger" OnClick="btnAdd_Click" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrint" />
            <asp:PostBackTrigger ControlID="btnshow" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
