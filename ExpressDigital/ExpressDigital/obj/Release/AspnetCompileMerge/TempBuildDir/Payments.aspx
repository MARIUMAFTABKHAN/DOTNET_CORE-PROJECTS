<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="ExpressDigital.Payments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>


            <style type="text/css">
                .row {
                    margin-bottom: 5px !important;
                }
            </style>

            <div class="row col-md-10 col-md-offset-1">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-12">
                    <div class="col-md-8">
                        <asp:Panel ID="pnlmain" runat="server" GroupingText="CRV Settlement Details">
                            <div class="row">
                                <div class="col-md-2">
                                    Company:
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    City:
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    Group:
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlMasterGroup" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlMasterGroup_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    Agency:
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlAgency" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    Client:
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlClient" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    Payment Type:
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlType" AutoPostBack="True" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    CRV:
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlCRV" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCRV_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    CRV:
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="cbLoadCN" AutoPostBack="true" OnCheckedChanged="cbLoadCN_CheckedChanged" runat="server" Text="Load Cr Notes" />
                                </div>

                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlCN" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-md-4" style="font-weight: bold">
                        <asp:Panel ID="Panel1" runat="server" GroupingText="CRV Information">
                            <div class="row">
                                <div class="col-md-6">
                                    Crv Amount:
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblCRVAmount" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    Tax Amount:
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblWHTax" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    GST:
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblGST" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    Total Amount:
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    Consumed Amount:
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblCRVConsumedAmount" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    Rremaining Amount
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblCRVRemainingAmount" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    Status:
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblCRVStatus" runat="server"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-2">
                        <asp:Button ID="btnSaveCRV" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                    </div>
                </div>

                <div class="col-md-12">
                    <asp:GridView ID="gvInvoices" runat="server" PageSize="25" CssClass="EU_DataTable" AutoGenerateColumns="false" OnRowDataBound="gvInvoices_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="InvoiceID" HeaderText="Invoice" />

                            <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                            <asp:BoundField DataField="CreationDate" HeaderText="Creation Date" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Company
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblcompany" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Agency" HeaderText="Agency" />
                            <asp:BoundField DataField="client" HeaderText="Client" />
                            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                            <asp:BoundField DataField="PaidAmount" HeaderText="Paid Amount" />
                            <asp:BoundField DataField="RemainingAmount" HeaderText="Balance" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Select
                                </HeaderTemplate>
                                <HeaderStyle Width="12%" />
                                <ItemStyle Width="12%" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdcompany" runat="server" Value='<%#Eval("Companyid")%>' />
                                    <asp:CheckBox ID="chkMultipleInvoices" runat="server"></asp:CheckBox>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdclientid" runat="server" Value='<%#Eval("ClientId") %>' />
                                    <asp:Label ID="lblIsDN" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"IsDN")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
