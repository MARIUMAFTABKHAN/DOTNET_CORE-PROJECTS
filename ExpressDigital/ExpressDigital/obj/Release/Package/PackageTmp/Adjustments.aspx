<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Adjustments.aspx.cs" Inherits="ExpressDigital.Adjustments" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <style type="text/css">
                .btn-danger {
                    width: 100px !important;
                    height: 26px !important;
                }

                .btn-success {
                    width: 100px !important;
                    height: 26px !important;
                }
            </style>
            <%--        <script src="Scripts/jquery-ui.js"></script>
            <link href="Scripts/jquery-ui.css" rel="stylesheet" />--%>
            <script type="text/javascript">

                function pageLoad() {
                    applyDatePicker();
                }
                function applyDatePicker() {
                    $("#ContentPlaceHolder1_txtdate").datepicker({
                        showOn: 'button',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        buttonImage: 'Content/Images/Calender.png',
                        dateFormat: 'dd/mm/yy'
                    });  
                }
            </script>
            <div class="col-md-12">
                <div class="form-group">
                    <div class="col-md-10 col-md-offset-1">
                        <div class="col-md-4 col-md-offset-4">
                            <%--    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up">
                            <ProgressTemplate>
                                <center>  
                                    <img src="Content/Images/loading6.gif" alt="Loading .........." />
                                </center>
                            </ProgressTemplate>

                        </asp:UpdateProgress>--%>
                        </div>
                    </div>
                </div>
                <div class="col-md-10 col-md-offset-1" style="margin-top: 5px !important">

                    <div class="panel panel-default">
                        <div class="panel-heading">Debit Credit Notes</div>
                        <div class="panel-body" style="padding-bottom: 9px !important">


                            <div class="col-md-12">
                                <div class="col-md-12 text-center">
                                    <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="col-md-12" style="margin-top: 1px !important">
                                            <div class="col-md-4">
                                                Company
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList ID="ddlcompany" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged"  AutoPostBack="true" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12" style="margin-top: 1px !important">
                                            <div class="col-md-4">
                                                Date
                                            </div>
                                            <div class="col-md-7">
                                                    <asp:TextBox ID="txtdate" Style="width: 90%; display: inline" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Release Order Date Required"
                                                ForeColor="Red" Display="Dynamic" ControlToValidate="txtdate"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12" style="margin-top: 1px !important">
                                            <div class="col-md-4">
                                                Group
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList ID="ddlSearchGroup" OnSelectedIndexChanged="ddlSearchGroup_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">

                                        <div class="col-md-12" style="margin-top: 1px !important">
                                            <div class="col-md-4">
                                                Agency
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList ID="ddlSearchAgency"  OnSelectedIndexChanged="ddlSearchAgency_SelectedIndexChanged"  AutoPostBack="true" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12" style="margin-top: 3px !important">
                                            <div class="col-md-4">
                                                Client
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList ID="ddlSearchClient" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchClient_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>


                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12" style="margin-top: 3px !important">
                                            <div class="col-md-4">
                                                Debit/Credit
                                            </div>
                                            <div class="col-md-6 text-left" style="width: 268px !important">
                                                <asp:RadioButtonList ID="rblDebitCreditNote" runat="server" RepeatDirection="Vertical"
                                                    Visible="true" AutoPostBack="True" OnSelectedIndexChanged="rblDebitCreditNote_SelectedIndexChanged"
                                                    ValidationGroup="Invoice">
                                                    <asp:ListItem Text="Debit Note" Value="true"></asp:ListItem>
                                                    <asp:ListItem Text="Credit Note" Value="false"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12" style="margin-top: 3px !important">
                                            <div class="col-md-2 col-md-offset-2">
                                                <asp:CheckBox ID="ChkLumpSump" runat="server" Text="Lump Sum"  />
                                            </div>
                                            <div class="col-md-2 col-md-offset-1" >
                                                <asp:CheckBox ID="ChkLoad" runat="server" GroupName="DRCR" Text="Load" AutoPostBack="True" OnCheckedChanged="ChkLoad_CheckedChanged" />
                                            </div>

                                            

                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-6">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <asp:Button ID="btnInvoices" Text="Get Invoices" CssClass="btn-danger" runat="server" Style="margin-left: -18px;" OnClick="btnInvoices_Click" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnViewInvoices" Text="View Invoices" CssClass="btn-danger" runat="server" Style="margin-left: 28px;" OnClick="btnViewInvoices_Click" />
                                        </div>




                                        
                                           <%-- <div class="col-md-1">
                                                Date:
                                            </div>
                                            <div class="col-md-4">
                                               <asp:TextBox ID="TextBox1" ValidationGroup="S" Text="0" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>--%>
                                        
                                        <div class="col-md-4" style="display:none">
                                            <asp:Button ID="btnPrintInvoice" Text="Print Invoices" CssClass="btn-success" runat="server" Style="margin-left: 34px;" OnClick="btnPrintInvoice_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-left: -28px !important; margin-top: 10px !important">
                                        <div class="col-md-4">
                                            <asp:ListBox ID="lstInvoices" runat="server" Height="150px" Width="120px" AutoPostBack="false" SelectionMode="Single" OnSelectedIndexChanged="lstInvoices_SelectedIndexChanged"></asp:ListBox>
                                        </div>
                                        <div class="col-md-1" style="margin-left: 10px !important">
                                            <asp:ListBox ID="lstCredit" runat="server" Height="150px" Width="120px" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-10 col-md-offset-1" style="margin-top: -10px !important">
                    <div class="panel panel-default">
                        <div class="panel-heading">Apply Lump Sum</div>
                        <div class="panel-body" style="padding-bottom: 9px !important">
                            <div class="form-group">
                                <div class="col-md-12">

                                    <div class="col-md-2" style="padding-top: 6px !important">
                                        Lump Sum Premium: 
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtLumSumPremium" ValidationGroup="S" Text="0" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="S" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtLumSumPremium"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ValidationGroup="S" Display="Dynamic" ID="RegularExpressionValidator3" ForeColor="Red" ValidationExpression="^\d{1,9}\.\d{1,2}$"
                                            ControlToValidate="txtLumSumPremium" runat="server" ErrorMessage="*"></asp:RegularExpressionValidator>

                                    </div>
                                    <div class="col-md-1">
                                        <asp:CheckBox ID="chkPremium" runat="server" Text="" />
                                    </div>
                                    <div class="col-md-2" style="padding-top: 6px !important">
                                        Remarks
                                    </div>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtReason" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" style="margin-top: 3px !important">
                                    <div class="col-md-2">
                                        Sales Tax
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkSalesTax" runat="server" />
                                    </div>
                                    <div class="col-md-2">
                                        Agency Commission
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkAGCommission" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" style="margin-top: 3px !important">
                                    <div class="col-md-2" style="padding-top: 6px !important">
                                        Other Charges
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtOtherDiscount" Text="0" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtOtherDiscount"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" ForeColor="Red"
                                            ValidationExpression="\d+"
                                            ControlToValidate="txtOtherDiscount" runat="server" ErrorMessage="*"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-2" style="padding-top: 6px !important">
                                        Reason
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlReason" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button Text="Apply Lump Sum" runat="server" ID="btnLumpsum" Style="width: 140px !important" CssClass="btn-danger" OnClick="btnLumpsum_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                  
                </div>
                <div class="col-md-10 col-md-offset-1" style="margin-top: -10px !important">
                    <div class="panel panel-default">
                        <div class="panel-heading">Calculations</div>
                        <div class="panel-body" style="padding-bottom: 9px !important">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <asp:Panel ID="pnlOrig" GroupingText="" runat="server">
                                            <div class="col-md-12" style="color: red; background-color: #CCC">
                                                Orginal Invoice
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Gross Amount
                                                </div>
                                                <div class="col-md-4" style="float: right">
                                                    <asp:Label ID="lblGross" Style="float: right" DataFormatString="{0:0.00}" runat="server" Text="0.00"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    AC
                                                </div>
                                                <div class="col-md-4" style="float: right">
                                                    <asp:Label ID="lblAC" Style="float: right" runat="server" DataFormatString="{0:0.00}" Text="0.00"></asp:Label>
                                                    <%--<asp:Label ID="lblAC"  Text="0.00" runat="server"> </asp:Label>                                                --%>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Net Before GST
                                                </div>
                                                <div class="col-md-4" style="float: right">
                                                    <asp:Label ID="lblNetBeforeGST" Style="float: right" DataFormatString="{0:0.00}" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    GST
                                                </div>
                                                <div class="col-md-4" style="float: right">
                                                    <asp:Label ID="lblGST" Style="float: right" DataFormatString="{0:0.00}" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Gross With GST
                                                </div>
                                                <div class="col-md-4" style="float: right">
                                                    <asp:Label ID="lblGrossWithGST" Style="float: right" DataFormatString="{0:0.00}" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Other Charges
                                                </div>
                                                <div class="col-md-4" style="float: right">
                                                    <asp:Label ID="lblOtherCharges" Style="float: right" DataFormatString="{0:0.00}" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Net Receiable
                                                </div>
                                                <div class="col-md-4" style="float: right">
                                                    <asp:Label ID="lblNetReceiable" Style="float: right" DataFormatString="{0:0.00}" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Panel ID="pnlCRDR" runat="server">
                                            <div class="col-md-12" style="color: red; background-color: #CCC">
                                                CR/DR Total Ajustment
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Gross Amount
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGrossCRDR" DataFormatString="{0:0.00}" Text="0.00" Style="float: right" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    AC
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblACCRDR" Text="0.00" Style="float:  right"  DataFormatString="{0:0.00}" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Net Before GST
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblNetBeforeGSTCRDR" DataFormatString="{0:0.00}"  Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    GST
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGSTCRDR" Style="float: right" DataFormatString="{0:0.00}" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Gross With GST
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGrowwWithGSTCRDR" DataFormatString="{0:0.00}" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Other Charges
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblOTHERCRDR"  DataFormatString="{0:0.00}" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Net Receiable
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblNETRECDRCR" Style="float: right" DataFormatString="{0:0.00}" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                    <%--      <div class="col-md-3">
                                        <asp:Panel ID="pnlAdjustedd" runat="server">
                                            <div class="col-md-12" style="color: red; background-color: #CCC">
                                                Adjusted Invoice
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Gross Amount
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGrossAd" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    AC
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblACAd" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Net Before GST
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblNETBeforeGSTAd" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    GST
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGSTAd" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Gross With GST
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGrossWithGSTAd" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Other Charges
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblOtherChargesAd" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Net Receiable
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblNetReceiableAd" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>--%>
                                    <div class="col-md-4">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <div class="col-md-12" style="color: red; background-color: #CCC">
                                                Changed Invoice
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Gross Amount
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGrossCH" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    AC
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblACCH" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Net Before GST
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblNetBeforeGSTCH" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    GST
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGSTCH" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Gross With GST
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblGrossWithGSTCH" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Other Charges
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblOtherChargesCH" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    Net Receiable
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="lblNetReceiableCH" Style="float: right" Text="0.00" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-10 col-md-offset-1">
                    <asp:Button ID="btnApplySuccess" ValidationGroup="s" CssClass="btn btn-success" btn="btnApplySuccess" Style="width: 150px !important" Text="Apply Changes" runat="server" OnClick="btnApplySuccess_Click" />
                </div>
            </div>
        

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
