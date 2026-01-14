<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CRVInProcess.aspx.cs" Inherits="ExpressDigital.CRVInProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <script type="text/javascript"> 
                function pageLoad() {
                    applyDatePicker();
                }
                function applyDatePicker() {

                    $("#ContentPlaceHolder1_CRVClearDatePicker").datepicker({
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
                <div class="row col-md-12">
                    <h3>Provide Payment Details
                    </h3>
                </div>
                <div class="row col-md-12">
                    <asp:Label ID="lblMsg" Visible="false" runat="server" ForeColor="Maroon"></asp:Label>
                </div>
                <div class="row col-md-12">
                    <div class="col-md-2">
                        Cheque #
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtChqNo" required runat="server" CssClass="form-control" Width="120px"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        Clear Date :
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="CRVClearDatePicker" required Style="width: 120px; display: inline" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnSearch" CssClass="btn btn-success" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div class="row col-md-12" style="margin-top: 10px !important">
                    <div class="col-md-2 col-md-offset-2">
                        <asp:Button ID="btnClear" Style="width: 100% !important" CssClass="btn btn-info" runat="server" OnClick="btnClear_Click" Text="Clear" />
                    </div>

                    <div class="col-md-2">
                        <asp:Button ID="btnClearCheque" Style="min-width: 100% !important" CssClass="btn btn-success" Enabled="false" OnClientClick="return confirm('Are you sure you want to clear this item?');" runat="server" Text="Mark as Clear" OnClick="btnClearCheque_Click" />

                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnBounceCheque" Style="min-width: 100%" CssClass="btn btn-danger" Enabled="false" OnClientClick="return confirm('Are you sure you want to bounce this item?');" runat="server" Text="Mark as Bounce" OnClick="btnBounceCheque_Click" />
                    </div>
                </div>
                <div class="row col-md-12 EU_TableScroll" style="margin-top: 10px !important">
                    <asp:GridView Font-Size="X-Small" ID="gvCRV" runat="server" Width="100%" AutoGenerateColumns="False"
                        CssClass="EU_DataTable" OnRowCommand="gvCRV_RowCommand" OnRowDataBound="gvCRV_RowDataBound"
                        BorderWidth="1px" ShowFooter="True">
                        <Columns>
                            <asp:BoundField HeaderText="CRVId" DataField="CRVId">
                                <ItemStyle Width="0px" CssClass="hide" />
                                <HeaderStyle Width="0px" CssClass="hide" />
                                <ControlStyle CssClass="hide" />
                                <FooterStyle CssClass="hide" />
                            </asp:BoundField>
                            <asp:TemplateField Visible="false" HeaderText="Select">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" onclick="Check_Click(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "CRV.aspx?CRVId=" + Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVId"))+"&CRVDetailId="+Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVDetailId")) %>'
                                        Text="Edit" Visible='<%# isReversed(Convert.ToString(Eval("CRVStatus")),Convert.ToString(Eval("CRVId"))) %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="Settlement">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkSettlement" runat="server" CausesValidation="false" Text="Settlement"
                                        CommandName="SettlementPlan" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CRVId") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CRVNo" HeaderText="CRV #">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CRVDate" HeaderText="CRV Date" DataFormatString="{0:d-M-yy}"
                                HtmlEncode="False">
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
                                    <asp:HiddenField runat="server" ID="hdCRVId" Value='<%# Eval("CRVId") %>' />
                                    <asp:HiddenField runat="server" ID="hdClientId" Value='<%# Eval("ClientId") %>' />
                                    <asp:HiddenField runat="server" ID="hdAgencyId" Value='<%# Eval("AgencyId") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="GSTNO" HeaderText="GST #">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PaymentNumber" HeaderText="Cheque #">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ChequeDate" DataFormatString="{0:d-M-yy}" HeaderText="Chq. Date"
                                HtmlEncode="False">
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
                                    <asp:Label Visible="true" ForeColor='<%# CRVRemainingStatusColor(Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVDetailId")),Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVId"))) %>'
                                        ID="Label1" runat="server" Text=' <%# GetUnitCRVRemAmount(decimal.Parse(Eval("RemainingAmount").ToString())).ToString("N0")%>'></asp:Label>
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
                            <asp:BoundField DataField="ADN" HeaderText="ADN #">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Challan Rcvd.">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox Visible="false" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged"
                                        ID="CheckBox1" runat="server" />
                                    <asp:Label ForeColor='<%# ChallanStatusColor(Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVId"))) %>'
                                        runat="server" Text='<%# ChallanStatus(Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVId"))) %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="Reversal">
                                <ItemTemplate>
                                    <asp:Button ID="btnReverseCRV" runat="server" CausesValidation="false" Text="Reverse"
                                        OnClientClick='<%# "javascript:return ConfirmReversal(" + GetCRVNo(Convert.ToString(Eval("CRVNo"))) + ");"  %>'
                                        CommandName="ReverseCRV" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CRVId") %>'
                                        Visible='<%# isCleared(Convert.ToString(Eval("CRVStatus")),Convert.ToString(Eval("CRVId"))) %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
