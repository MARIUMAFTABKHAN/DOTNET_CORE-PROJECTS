<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CRVView.aspx.cs" Inherits="ExpressDigital.CRVView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <style type="text/css">
                tr {
                    margin-bottom: 10px !important;
                }
            </style>
            <script src="Scripts/jquery-ui.js"></script>
            <link href="Scripts/jquery-ui.css" rel="stylesheet" />

            <script type="text/javascript">
                function pageLoad() {
                    applyDatePicker();
                }
                function applyDatePicker() {

                    $("#ContentPlaceHolder1_FromDatePicker").datepicker({
                        showOn: 'button',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        buttonImage: 'Content/Images/Calender.png',
                        dateFormat: 'dd/mm/yy'
                    });

                    $("#ContentPlaceHolder1_ToDatePicker").datepicker({
                        showOn: 'button',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        buttonImage: 'Content/Images/Calender.png',
                        dateFormat: 'dd/mm/yy'
                    });
                }
            </script>

            <div class="col-md-10 col-md-offset-1">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="left" style="font-weight: bold">&nbsp;Cheque Receipt Voucher
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="center">&nbsp;<table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="right" style="width: 75px">
                                    <strong>For Date:
                                    </strong>&nbsp;
                                </td>
                                <td align="left">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td onmouseup="ButtonFrom_OnMouseUp()">
                                                <asp:TextBox ID="FromDatePicker" runat="server" CssClass="form-control" Style="width: 75%; display: inline-block"></asp:TextBox>

                                            </td>

                                            <td></td>

                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <strong>To Date: </strong>
                                </td>
                                <td align="left">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td onmouseup="ButtonTo_OnMouseUp()">
                                                <asp:TextBox ID="ToDatePicker" runat="server" CssClass="form-control" Style="width: 75%; display: inline-block"></asp:TextBox>

                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>

                            <%# GetWHTaxTotal().ToString("N0") %>
                            <tr>
                                <td align="right" style="width: 75px">
                                    <strong>City:</strong></td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlHOCity" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlHOCity_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 75px">
                                    <strong>Agency:&nbsp; </strong>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlAgency" runat="server" AutoPostBack="True" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged" Width="100%">
                                    </asp:DropDownList>
                                    <%--  <asp:CheckBox ID="chkAgencyByName" runat="server" AutoPostBack="True"
                                        Checked="True" OnCheckedChanged="chkAgencyByName_CheckedChanged"
                                        Text="Fill By Agency Master" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 75px">
                                    <strong>Client:&nbsp;</strong></td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="True" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlClient_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 75px">
                                    <strong>CRV List: &nbsp;</strong></td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCRV" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlCRV_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 75px">
                                    <strong>CRV #:&nbsp;&nbsp; </strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCRVNo" CssClass="form-control" Width="69%"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 75px">
                                    <strong>Cheque #: &nbsp; </strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtChqNo" runat="server" CssClass="form-control" Width="205px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 75px">
                                    <strong>Status:&nbsp;&nbsp; </strong>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server" Width="50%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="height: 15px">
                                <td colspan="2" style="height: 15px"></td>
                            </tr>
                            <tr>
                                <td style="width: 75px"></td>
                                <td align="left">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-success" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnClear" CssClass="btn btn-success" runat="server" OnClick="btnClear_Click" Text="Clear" />
                                    <asp:Button ID="btnViewReport" CssClass="btn btn-success" runat="server" Text="View Report" OnClick="btnViewReport_Click" />
                                    <asp:Button ID="btnExportReport" CssClass="btn btn-success" runat="server" Text="Export"
                                        OnClick="btnExportReport_Click" /></td>
                            </tr>

                        </table>
                        </td>

                    </tr>


                    <tr>
                        <td colspan="2" style="height: 15px">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Maroon"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-md-12" style="padding:20px !important" >
                <asp:GridView CssClass="EU_DataTable" Font-Size="X-Small" ID="gvCRV" DataKeyNames="CompanyId" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="gvCRV_RowCommand" OnRowDataBound="gvCRV_RowDataBound" BorderWidth="1px" ShowFooter="True">
                    <Columns>
                        <asp:BoundField HeaderText="CRVId" DataField="CRVId">
                            <ItemStyle Width="0px" CssClass="hide" />
                            <HeaderStyle Width="0px" CssClass="hide" />
                            <ControlStyle CssClass="hide" />
                            <FooterStyle CssClass="hide" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "CashReceivedVoucher.aspx?CRVId=" + Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVId"))+"&CRVDetailId="+Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVDetailId")) %>' Text="Edit" Visible='<%# isReversed(Convert.ToString(Eval("PaymentStatus")),Convert.ToString(Eval("CRVId"))) %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Settlement">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkSettlement" runat="server" CausesValidation="false" Text="Settlement" CommandName="SettlementPlan" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CRVId") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CRVNo" HeaderText="CRV #">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CRVDate" HeaderText="CRV Date" DataFormatString="{0:d-M-yy}" HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AgencyName" HeaderText="Agency">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AgencyCity" HeaderText="City Name" />
                        <asp:TemplateField HeaderText="Client">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Client") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Client") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%-- <asp:Literal Text ="Total" runat =server />--%>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GSTNO" HeaderText="GST #">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PaymentNumber" HeaderText="Cheque #">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ChequeDate" DataFormatString="{0:d-M-yy}" HeaderText="Chq. Date" HtmlEncode="False">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Amount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CRVAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label Visible="true" ID="Label2" runat="server" Text='<%# GetUnitCRVAmount(decimal.Parse(Eval("CRVAmount").ToString())).ToString("N0")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%# GetCRVAmountTotal().ToString("N0") %>
                            </FooterTemplate>
                            <FooterStyle Font-Size="XX-Small" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tax">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("WithHoldingTax") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle Width="60px" />
                            <ItemTemplate>
                                <asp:Label Visible="true" ID="Label3" runat="server" Text='<%# GetUnitWHTax(decimal.Parse(Eval("WithHoldingTax").ToString())).ToString("N0")%>'></asp:Label>

                            </ItemTemplate>
                            <FooterTemplate>
                                <%# GetWHTaxTotal().ToString("N0") %>
                            </FooterTemplate>
                            <FooterStyle Font-Size="XX-Small" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GST">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtGST" runat="server" Text='<%# Bind("GST") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle Width="60px" />
                            <ItemTemplate>
                                <asp:Label Visible="true" ID="lblGST" runat="server" Text='<%# GetUnitGST(decimal.Parse(Eval("GST").ToString())).ToString("N0")%>'></asp:Label>

                            </ItemTemplate>
                            <FooterTemplate>
                                <%# GetGSTTotal().ToString("N0") %>
                            </FooterTemplate>
                            <FooterStyle Font-Size="XX-Small" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Client Amount">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtShiftAmount" runat="server" Text='<%# Bind("CRVDetailAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle Wrap="False" />
                            <ItemTemplate>
                                <asp:Label Visible="true" ID="lblshiftcrv" runat="server" Text='<%# GetUnitClientCRV(decimal.Parse(Eval("CRVDetailAmount").ToString())).ToString("N0") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%# GetClientCRVTotal().ToString("N0") %>
                            </FooterTemplate>
                            <FooterStyle Font-Size="XX-Small" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rem. Amount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CRVRemainingAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle Wrap="False" />
                            <ItemTemplate>
                                <asp:Label Visible="true" ForeColor='<%# CRVRemainingStatusColor(Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVDetailId")),Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVId"))) %>' ID="Label1" runat="server" Text=' <%# GetUnitCRVRemAmount(decimal.Parse(Eval("RemainingAmount").ToString())).ToString("N0")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%# GetCRVRemTotal().ToString("N0") %>
                            </FooterTemplate>
                            <FooterStyle Font-Size="XX-Small" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="PaymnetType" HeaderText="Type" />
                        <asp:BoundField DataField="Branch" HeaderText="Branch" Visible="False">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PaymentMode" HeaderText="Mode" Visible="False">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Bank" HeaderText="Bank">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PaymentStatus" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <%--  <asp:BoundField DataField="IsShifted" HeaderText="Is Shifted">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ADN" HeaderText="ADN #">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>--%>


                        <asp:TemplateField HeaderText="Challan Rcvd.">
                            <EditItemTemplate>
                                <!--    <asp:CheckBox  AutoPostBack="true" OnCheckedChanged ="CheckBox1_CheckedChanged"  ID="CheckBox1" runat="server" /> -->
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox Visible="false" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" ID="CheckBox1" runat="server" />
                                <asp:Label ForeColor='<%# ChallanStatusColor(Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVId"))) %>' runat="server" Text='<%# ChallanStatus(Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVId"))) %>' />

                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reversal">
                            <ItemTemplate>
                                <asp:Button ID="btnReverseCRV" runat="server" CausesValidation="false" Text="Reverse" OnClientClick='<%# "javascript:return ConfirmReversal(" + GetCRVNo(Convert.ToString(Eval("CRVNo"))) + ");"  %>' CommandName="ReverseCRV" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CRVId") %>' Visible='<%# isCleared(Convert.ToString(Eval("PaymentStatus")),Convert.ToString(Eval("CRVId"))) %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle CssClass="GridFooter" HorizontalAlign="Right" />
                    <RowStyle CssClass="GridItems" />
                    <PagerStyle CssClass="GridFooter" />
                    <HeaderStyle CssClass="GridHeader" />
                </asp:GridView>
            </div>
            <table align="Center">

                <%# GetGSTTotal().ToString("N0") %>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
