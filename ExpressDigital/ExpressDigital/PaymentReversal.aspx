<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PaymentReversal.aspx.cs" Inherits="ExpressDigital.PaymentReversal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">

        // --------Highlighting-----
        function Highlight(row) {

            row.className = 'hightlighrow';

        }




        function UnHighlight(row) {

            row.className = 'GridItems';

        }
    </script>

    <style  type="text/css">

        .row{
            margin-bottom:5px !important;
        }
    </style>
    <div class="col-md-10 col-md-offset-1" style="margin-top:10px">
         <div class="row">
             <h5>
                 Payment Reversal
             </h5>
         </div>
        <div class="row">
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-4">
                        Company:
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlCompay" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        City:
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlCity" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        View:
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlCriteria" runat="server" CssClass="form-control" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCriteria_SelectedIndexChanged" ValidationGroup="CriteriaValidation">
                            <asp:ListItem Value="select">------ Select ------</asp:ListItem>
                            <asp:ListItem Value="CRV" Text="CRV" />
                            <asp:ListItem Value="CN" Text="Credit Note" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-4">
                        Agecy:
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlAgency" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        Client:
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlClient" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        List:
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlSelectionList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectionList_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>




            <div class="col-md-4">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="right" style="font-weight: bold; width: 180px">CRV No:&nbsp;</td>
                        <td align="right" style="width: 140px; text-align: left">
                            <asp:Label ID="lblCRVNo" runat="server" Font-Bold="True" Style="text-align: right"></asp:Label></td>
                    </tr>

                    <tr>
                        <td align="right" style="font-weight: bold; width: 180px; height: 17px;">Total 
                                CRV Amount:&nbsp;
                        </td>
                        <td align="right" style="text-align: left; width: 140px; height: 17px;">
                            <asp:Label ID="lblCRVAmount" Style="text-align: right;" Font-Bold="True" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="font-weight: bold; width: 180px">Tax Amount:&nbsp;</td>
                        <td align="right" style="text-align: left">
                            <asp:Label ID="lblWHTax" Style="text-align: right;" runat="server" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="right" style="font-weight: bold; width: 180px">GST:&nbsp;</td>
                        <td align="right" style="text-align: left">
                            <asp:Label ID="lblGST" Style="text-align: right;" runat="server" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="right" style="font-weight: bold; width: 180px">Amount:&nbsp; 
                        </td>
                        <td align="left" style="height: 20px">
                            <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="right" style="font-weight: bold; width: 180px">Consumed Amount:&nbsp; 
                        </td>
                        <td align="left">
                            <asp:Label ID="lblCRVConsumedAmount" Font-Bold="True" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="font-weight: bold; width: 180px">Remaining Amount:&nbsp; 
                        </td>
                        <td align="left">
                            <asp:Label ID="lblCRVRemainingAmount" Font-Bold="True" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" style="font-weight: bold; width: 180px; height: 17px;">CN No:&nbsp;
                        </td>
                        <td align="right" style="width: 140px; text-align: right; height: 17px;">
                            <asp:Label ID="lblCNNo" runat="server" Font-Bold="True" Style="text-align: right"></asp:Label></td>
                    </tr>

                    <tr>
                        <td align="right" style="font-weight: bold; width: 180px">CN Amount:&nbsp;
                        </td>
                        <td align="right" style="text-align: right; width: 140px;">
                            <asp:Label ID="lblCNAmount" Style="text-align: right;" Font-Bold="True" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="font-weight: bold; width: 180px">Consumed Amount:&nbsp; 
                        </td>
                        <td align="left">
                            <asp:Label ID="lblCNConAmount" Font-Bold="True" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="font-weight: bold; width: 180px">Remaining Amount:&nbsp; 
                        </td>
                        <td align="left">
                            <asp:Label ID="lblCNRemAmount" Font-Bold="True" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>

        </div>

        <div class="row">
            <div class="col-md-col-2 col-md-offset-2">
                <asp:Button ID="btnView" runat="server" CssClass="btn btn-success" Text="View" OnClick="btnView_Click" ValidationGroup="CriteriaValidation" />

            </div>
        </div>
        <div class="row">
            <table id="tblGrd" visible="false" runat="server" border="1" cellpadding="0" cellspacing="0" width="100%" class="maintable2">
                <tr>
                    <td width="100%">
                        <asp:Panel ID="pnlInvoice" runat="server" GroupingText="Invoices/DN" Font-Bold="true" Width="80%" Height="100%" HorizontalAlign="Left">
                            <table width="100%" border="0">
                                <tr align="center">
                                    <td align="center">
                                        <asp:Label ID="lblRecMsg" runat="server" Visible="false" ForeColor="red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvInvoices" runat="server" Width="100%" AutoGenerateColumns="False"
                                            AllowPaging="True" CssClass="EU_DataTable"
                                            OnPageIndexChanging="gvInvoices_PageIndexChanging" Font-Bold="False"
                                            OnRowDataBound="gvInvoices_RowDataBound">

                                            <Columns>

                                                <asp:BoundField DataField="Mode" HeaderText="Invoice No/Debit Note">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="AgencyName" HeaderText="Agency Name">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="ClientName" HeaderText="Client Name">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="AmountPaid" HeaderText="Paid Amount" DataFormatString="{0:#,##0;(#,##0);0}" HtmlEncode="False">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>

                                                <asp:TemplateField HeaderText="Select for Reversal">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMultipleInvoices" runat="server" Height="15px" Width="15px" />
                                                        <asp:Label ID="lblCRVId" runat="server" Visible="false" Text='<%# Bind("Id")%>'></asp:Label>
                                                        <asp:Label ID="lblIsDN" runat="server" Visible="false" Text='<%# Bind("IsDN")%>'></asp:Label>
                                                        <asp:Label ID="lblIsCN" runat="server" Visible="false" Text='<%# Bind("IsCN")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                            </Columns>
                                            <FooterStyle CssClass="GridFooter" />
                                            <RowStyle CssClass="GridItems" />
                                            <PagerStyle CssClass="GridFooter" />
                                            <HeaderStyle CssClass="GridHeader" />

                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            &nbsp;
 <table id="tblSave" visible="false" runat="server" border="1" cellpadding="0" cellspacing="0" width="100%" class="maintable2">
     <tr>
         <td width="100%">
             <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="btnSave_Click" />
         </td>
     </tr>
 </table>
        </div>
    </div>
</asp:Content>
