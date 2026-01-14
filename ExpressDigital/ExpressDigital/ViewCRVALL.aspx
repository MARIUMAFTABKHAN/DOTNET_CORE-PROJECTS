<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ViewCRVALL.aspx.cs" Inherits="ExpressDigital.ViewCRVALL" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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

                .StickyHeader th {
                    position: sticky;
                    top: 0
                }
            </style>
            <div class="col-md-10 col-md-offset-1">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-12 text-center">
                    <h4>&nbsp;&nbsp;&nbsp;&nbsp; View All CRV
                    </h4>
                </div>
                <div class="col-md-12 ">
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
                        <div class="col-md-12" style="margin-top: 5px !important">
                            <div class="col-md-2">
                                Company 
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                Status
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12" style="margin-top: 5px !important">
                            <div class="col-md-2">
                                City
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlCity" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                Master Agency
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlmasteragency" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlmasteragency_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
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
                                Client
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlClient" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12" style="margin-top: 5px !important">
                            <div class="col-md-2">
                                CRV # 
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCrvNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Cheque No
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtChkNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">

                            <div class="col-md-1 col-md-offset-2">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" Style="width: 75px !important" ValidationGroup="s" OnClick="btnSearch_Click" />
                            </div>

                            <div class="col-md-1 col-md-offset-1">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" Style="width: 75px !important; margin-left: -40px" ValidationGroup="s" OnClick="btnCancel_Click" />

                            </div>
                            <div class="col-md-2 col-md-offset-2">
                                <asp:Label ID="lblcounter" runat="server"></asp:Label>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="col-md-12" style="margin-top: 3px !important">
                    <asp:UpdateProgress AssociatedUpdatePanelID="up" ID="pg" runat="server">
                        <ProgressTemplate>
                            <img id="imgpg" alt="Processing Data , Please waitt......" src="Content/Images/loading6.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>



            </div>
            <%--<div class="col-md-12" style="margin-top: 3px !important; padding: 20px !important; height: 600px !important; overflow: auto">--%>
            <div class="col-md-12">
                <asp:GridView Font-Size="XX-Small" CssClass="EU_DataTable" ID="gvCRV" DataKeyNames="CRVId" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCRV_RowCommand" OnRowDataBound="gvCRV_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.">
                            <HeaderStyle Width="1%" />
                            <ItemStyle Width="1%" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="CRVId" DataField="CRVId">
                            <ItemStyle Width="0px" CssClass="hide" />
                            <HeaderStyle Width="0px" CssClass="hide" />
                            <ControlStyle CssClass="hide" />
                            <FooterStyle CssClass="hide" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Edit">
                            <HeaderStyle Width="1%" />
                            <ItemStyle Width="1%" />
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "CashReceivedVoucher.aspx?CRVId=" + Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVId"))+"&CRVDetailId="+Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVDetailId")) %>' Text="Edit" Visible='<%# isReversed(Convert.ToString(Eval("Status")),Convert.ToString(Eval("CRVId"))) %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Settlement">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkSettlement" runat="server" CausesValidation="false" Text="Settlement" CommandName="SettlementPlan" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CRVId") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="1%" />
                            <ItemStyle Width="1%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                            <ItemTemplate>
                                <asp:Label ID="lbocmpany" Style="width: 200px !important; min-width: 200% !important" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Company_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CRVNo" HeaderText="CRV #">
                            <HeaderStyle Width="2%" />
                            <ItemStyle Width="2%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CRVDate" HeaderText="CRV Date" DataFormatString="{0:d-M-yy}" HtmlEncode="False">
                            <HeaderStyle Width="2%" Wrap="false" />
                            <ItemStyle Width="2%" />

                        </asp:BoundField>
                        <asp:BoundField DataField="Agency" HeaderText="Agency">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CityName" HeaderText="City Name" />
                        <asp:TemplateField HeaderText="Client">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Client") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="2%" />
                            <ItemStyle Width="2%" />
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Client") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GSTNO" HeaderText="GST #">
                            <HeaderStyle Width="1%" />
                            <ItemStyle Width="1%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PaymentNumber" HeaderText="Cheque #">
                            <HeaderStyle Width="2%" />
                            <ItemStyle Width="2%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ChequeDate" DataFormatString="{0:d-M-yy}" HeaderText="Chq. Date" HtmlEncode="False">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Amount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CRVAmount") %>' DataFormatString="{0:N2}"></asp:TextBox>
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
                        <asp:TemplateField HeaderText="Client Amt.">
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
                        <asp:TemplateField HeaderText="Rem. Amt.">
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
                        <asp:BoundField DataField="PaymentType" HeaderText="Type" />
                        <asp:BoundField DataField="Branch" HeaderText="Branch" Visible="False">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PaymentMode" HeaderText="Mode" Visible="False">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Bank" HeaderText="Bank">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IsShifted" HeaderText="Is Shifted">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ADN" HeaderText="ADN #">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Challan Rcvd.">
                            <ItemTemplate>
                                <asp:CheckBox Visible="false" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" ID="CheckBox1" runat="server" />
                                <asp:Label ForeColor='<%# ChallanStatusColor(Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVId"))) %>' runat="server" Text='<%# ChallanStatus(Convert.ToString(DataBinder.Eval(Container.DataItem,"CRVId"))) %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reversal">
                            <ItemTemplate>
                                <asp:Button ID="btnReverseCRV" runat="server" CausesValidation="false" Text="Reverse" OnClientClick='<%# "javascript:return ConfirmReversal(" + GetCRVNo(Convert.ToString(Eval("CRVNo"))) + ");"  %>' CommandName="ReverseCRV" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CRVId") %>' Visible='<%# isCleared(Convert.ToString(Eval("Status")),Convert.ToString(Eval("CRVId"))) %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="GridFooter" HorizontalAlign="Right" />
                    <RowStyle CssClass="GridItems" />
                    <PagerStyle CssClass="GridFooter" />
                    <HeaderStyle CssClass="GridHeader StickyHeader" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
