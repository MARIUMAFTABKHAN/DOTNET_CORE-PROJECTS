<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CashReceivedVoucher.aspx.cs" Inherits="ExpressDigital.CashReceivedVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Scripts/jquery-ui.js"></script>
    <link href="Scripts/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">


        function pageLoad() {
            //  $('input[type=file]').bootstrapFileInput();
            applyDatePicker();

        }

        function applyDatePicker() {

            $("#ContentPlaceHolder1_dtCRVDate").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/Calender.png',
                dateFormat: 'dd/mm/yy'
            });
            $("#ContentPlaceHolder1_dtChequeDate").datepicker({
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
            $("#ContentPlaceHolder1_txtSearchROMODateFrom").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/Calender.png',
                dateFormat: 'dd/mm/yy'
            });
            $("#ContentPlaceHolder1_txtClearDate").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/Images/Calender.png',
                dateFormat: 'dd/mm/yy'
            });
            $("#ContentPlaceHolder1_txtChallanDate").datepicker({
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
        .CheckboxList label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: bold;
            margin-left: 5px;
            margin-top: 1px;
        }

            .CheckboxList input {
                float: left;
                clear: both;
            }
    </style>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="col-md-10 col-md-offset-1">
                    <cc1:Accordion ID="Accordion1" Width="100%" SelectedIndex="1" RequireOpenedPane="true" runat="server" CssClass="accordion" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                        <Panes>
                            <cc1:AccordionPane runat="server" ID="AccordionPane1">
                                <Header>Search Cheque Receive Voucher  </Header>
                                <Content>
                                    <div class="col-md-12">

                                        <div class="form-group">
                                            <div class="col-md-2">
                                                Date From
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtSearchROMODateFrom" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>

                                            <div class="col-md-1 col-md-offset-1" style="padding-top: 2px;">
                                                Date To
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtSearchROMODateTo" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-2">
                                                Group
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlSearchGroup" OnSelectedIndexChanged="ddlSearchGroup_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2 col-md-1">
                                                Agency
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlSearchAgency" OnSelectedIndexChanged="ddlSearchAgency_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" ValidationGroup="s" OnClick="btnSearch_Click" />
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-2" style="display: none">
                                                Client
                                            </div>
                                            <div class="col-md-3" style="display: none">
                                                <asp:DropDownList ID="ddlSearchClient" OnSelectedIndexChanged="ddlSearchClient_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>


                                        </div>
                                        <div class="form-group">
                                            <asp:GridView ID="gv" PageSize="50" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                                                AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
                                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>#</HeaderTemplate>
                                                        <ItemTemplate><%#Container.DataItemIndex + 1 %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CRVDate" HeaderText="Date">
                                                        <HeaderStyle Width="7%" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="CRVNO" HeaderText="CRV-No">
                                                        <HeaderStyle Width="12%" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="CRVAmount" HeaderText="Amount">
                                                        <HeaderStyle Width="5%" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Bank" HeaderText="Bank">
                                                        <HeaderStyle Width="18%" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Branch" HeaderText="Branch">
                                                        <HeaderStyle Width="18%" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Status" HeaderText="CRV Status">
                                                        <HeaderStyle Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PaymnetType" HeaderText="Payment Type">
                                                        <HeaderStyle Width="10%" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="PaymentTypeStatus" HeaderText="Payment Status">
                                                        <HeaderStyle Width="10%" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle Width="40px" />
                                                        <HeaderTemplate>
                                                            Edit
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <center>
                                                                <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/News-Edit.ico'
                                                                    ValidationGroup="a" OnClick="EditButton_Click" />
                                                            </center>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle Width="40px" />
                                                        <HeaderTemplate>
                                                            View
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <center>
                                                                <asp:ImageButton runat="server" ID="ViewButton" ImageUrl='~/Content/images/application_view_tile.png'
                                                                    ValidationGroup="a" OnClick="ViewButton_Click" />
                                                            </center>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </Content>
                            </cc1:AccordionPane>
                        </Panes>
                        <Panes>
                            <cc1:AccordionPane runat="server" ID="Pane1">
                                <Header>Cheque Received Voucher  </Header>
                                <Content>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="col-md-12 text-center">
                                                <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Company 
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    CRV Status
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlCRVStatus" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCRVStatus_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    CRV Date
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="dtCRVDate" Style="width: 35% !important; display: initial !important" runat="server" CssClass="form-control">

                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ForeColor="Red" Display="Dynamic" ControlToValidate="dtCRVDate" ID="RequiredFieldValidator9" runat="server" ErrorMessage="Date Required"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    Payment Type
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Amount
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control">

                                                    </asp:TextBox>
                                                    <asp:RangeValidator ID="RangeValidator2" ControlToValidate="txtAmount" runat="server" ErrorMessage="Only Number" ForeColor="Red" Display="Dynamic" MaximumValue="999999999" MinimumValue="0"></asp:RangeValidator>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="txtAmount"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlcurrency" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    Mode 
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>



                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Income Tax
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtWithhoding" runat="server" CssClass="form-control">

                                                    </asp:TextBox>
                                                    <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtWithhoding" runat="server" ErrorMessage="Only Number" ForeColor="Red" Display="Dynamic" MaximumValue="99999999" MinimumValue="0"></asp:RangeValidator>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="txtWithhoding"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGST" runat="server" placeholder="GST Tax" CssClass="form-control">
                                                    </asp:TextBox>
                                                    <asp:RangeValidator ID="RangeValidator3" ControlToValidate="txtGST" runat="server" ErrorMessage="Only Number" ForeColor="Red" Display="Dynamic" MaximumValue="9" MinimumValue="0"></asp:RangeValidator>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="txtGST"></asp:RequiredFieldValidator>

                                                </div>

                                                <div class="col-md-2">
                                                    Cheque #
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtChequeNo" runat="server" CssClass="form-control" Style="width: 75% !important">

                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="txtChequeNo"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Bank
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtBank" runat="server" CssClass="form-control">

                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="txtBank"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="col-md-2">
                                                    Cheque Date
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="dtChequeDate" Style="width: 75% !important; display: initial" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Date Required" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="dtChequeDate"></asp:RequiredFieldValidator>
                                                </div>


                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Branch
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="txtBranch"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    Challan Date
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtChallanDate" Style="width: 75% !important; display: initial" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Date Required" ForeColor="Red"
                                                        Display="Dynamic" ControlToValidate="dtChequeDate"></asp:RequiredFieldValidator>
                                                </div>

                                            </div>
                                        </div>


                                        <div class="form-group" id="divClearDate" runat="server">
                                            <div class="col-md-12">

                                                <div class="col-md-3">
                                                    Cheque Clear Date
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtClearDate" Style="width: 75% !important; margin-left: 5px !important; display: initial" runat="server" CssClass="form-control">
                                                    </asp:TextBox>

                                                </div>



                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Group Agency
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlGroupAgency" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlGroupAgency_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator1" Display="Dynamic" ControlToValidate="ddlGroupAgency" OnServerValidate="ddlValidation_server"
                                                        runat="server" ForeColor="Red" ErrorMessage="*"></asp:CustomValidator>
                                                </div>
                                                <div class="col-md-2">
                                                    Challan Rec.
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:CheckBox ID="chkChallanReceived" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Agency
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlAgency" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator2" Display="Dynamic" ControlToValidate="ddlAgency" OnServerValidate="ddlValidation_server"
                                                        runat="server" ForeColor="Red" ErrorMessage="*"></asp:CustomValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    Client
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlClient" CssClass="form-control"  AutoPostBack="false" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-1 col-md-col-md-offset-2">
                                                    <asp:Button ID="btnSave" OnClientClick="this.disabled='true';" UseSubmitBehavior="false" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />&nbsp;                    
                                                </div>
                                                <div class="col-md-1" style="margin-left: 20px !important">
                                                    <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />

                                                </div>
                                            </div>
                                        </div>
                                </Content>

                            </cc1:AccordionPane>
                        </Panes>
                    </cc1:Accordion>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
