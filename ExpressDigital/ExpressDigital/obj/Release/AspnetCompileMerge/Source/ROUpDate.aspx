<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ROUpDate.aspx.cs" Inherits="ExpressDigital.ROUpDate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>

            <%--   <script src="Scripts/jquery-ui.js"></script>
            <link href="Scripts/jquery-ui.css" rel="stylesheet" />--%>
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
                    <h4>&nbsp;&nbsp;&nbsp;&nbsp; Release Orders
                    </h4>
                </div>

            </div>
            <div class="col-md-10 col-md-offset-1">
                <div class="row">
                    <div class="col-md-12" style="margin-top: 5px !important">
                        <div class="col-md-2">
                            Company 
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-bottom: 3px !important">
                        <div class="col-md-2">
                            Release Order No
                        </div>
                        <div class="col-md-3">
                            <asp:HiddenField ID="hdID" runat="server"></asp:HiddenField>
                            <asp:TextBox ID="txtSearchReleaseOrder" MaxLength="12" runat="server" CssClass="form-control"></asp:TextBox>
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                ControlToValidate="txtSearchReleaseOrder" ForeColor="Red" runat="server" ErrorMessage="*"
                                ValidationExpression="\d+" Display="Dynamic" ValidationGroup="s"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="col-md-2 col-md-offset-2" style="display: none">
                            <asp:CheckBox class="chk" ID="chkDollar" runat="server" Text="Dollar Invoice" />

                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            IRO No.
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtIRO" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            External RO
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtRefNumber" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-top: 5px !important">
                        <div class="col-md-2">
                            Agency
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlAgency" runat="server" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Client
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlClient" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-top: 3px !important">
                        <div class="col-md-2">
                            Campaign
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtcampaign" runat="server" CssClass="form-control" Style="min-width: 100%"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-top: 3px !important">
                        <div class="col-md-2">
                            Date From
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateFrom" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchROMODateFrom" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-2" style="padding-top: 2px;">
                            Date To
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSearchROMODateTo" Style="width: 75%; display: initial" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSearchROMODateTo" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-top: 3px !important">

                        <div class="col-md-1 col-md-offset-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" Style="width: 75px !important" ValidationGroup="s" OnClick="btnSearch_Click" />
                        </div>
                        <%--<div class="col-md-1" style="margin-left: 20px !important">
                            <asp:Button ID="btnExecute" runat="server" Text="Genereate" CssClass="btn btn-danger" Style="width: 90px !important; margin-left: -25px" ValidationGroup="s" OnClick="btnExecute_Click" />
                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" ConfirmText="Are you sure to execute records ?"
                                TargetControlID="btnExecute" runat="server" />
                        </div>--%>

                        <div class="col-md-1" style="margin-left: 50px !important">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" Style="width: 75px !important; margin-left: -40px" ValidationGroup="s" OnClick="btnCancel_Click" />

                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-10 col-md-offset-1">
                <div class="row">

                    <div class="col-md-12" style="margin-top: 3px !important">
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID, CompnayID" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">

                            <Columns>
                                <asp:TemplateField HeaderText="Release Order">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRO" runat="server" Style="width: 100% !important; min-width: 100% !important" Text='<%#Eval("ReleaseOrderNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RO Date">
                                    <HeaderStyle Width="12%" />
                                    <ItemStyle Width="12%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRODate" runat="server" Style="width: 100% !important; min-height: 100% !important" Text='<%# Convert.ToDateTime(Eval("ROMPDate")).ToString("dd-MM-yyyy")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Compaign">
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle Width="25%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblcomapin" runat="server" Style="width: 100% !important; min-width: 100% !important" Text='<%#Eval("Compaign")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="INV_Gross" DataFormatString="{0:0.00}" HeaderText="Gross">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INV_AGC" DataFormatString="{0:0.00}" HeaderText="AG Commssion">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="INV_Discount" DataFormatString="{0:0.00}" HeaderText="Discount">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INV_GST" DataFormatString="{0:0.00}" HeaderText="G.S.Tax">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="INV_Net" DataFormatString="{0:0.00}" HeaderText="Net Amount">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="IsBilled" HeaderText="Billed">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IsCancelled" HeaderText="Cancel">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemStyle Width="40px" />
                                    <HeaderStyle Width="40px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ImageUrl="~/Content/Images/Trash-Cancel.ico" ID="btnDelete" Width="16px" Height="16px" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" TargetControlID="btnDelete" ConfirmText="Are you sure to Cancel this Release Order ?" runat="server"></ajaxToolkit:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="40px" />
                                    <HeaderStyle Width="40px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ImageUrl="~/Content/Images/News-Edit.ico" ID="btnEdit" Width="16px" Height="16px" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                                        <ajaxToolkit:ConfirmButtonExtender ConfirmText="Are you sure to Edit this Release Order ?" TargetControlID="btnEdit" ID="ConfirmButtonExtender2" runat="server"></ajaxToolkit:ConfirmButtonExtender>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <pagerstyle horizontalalign="Center" verticalalign="Middle" />
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
