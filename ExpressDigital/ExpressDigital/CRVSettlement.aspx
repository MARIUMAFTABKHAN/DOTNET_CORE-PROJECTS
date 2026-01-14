<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CRVSettlement.aspx.cs" Inherits="ExpressDigital.CRVSettlement" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <style type="text/css">
                .form-control_New {
                    height: 22px;
                    border: 1px solid #CCC;
                    width: 100px;
                    padding-left: 2px;
                }

                .btn-success {
                    height: 26px !important;
                }
            </style>
            <script type="text/javascript">
                var Knockup = 0;
                //CalculateKnockup();
                // $(window).load(function () {
                //$("#<$= fbn.ClientID %>").change(function () {
                //    alert("fired");
                //});
                function cmtChanged(val) {

                 //   $('#ContentPlaceHolder1_btnSettlement').click();
                }
                ////Change price and grand total on changing qty
                //$('#ContentPlaceHolder1_gv .form-control_New').keydown(function () {
                //    var $tr = $(this).parents('tr');
                //    if ($tr.length > 0) {
                //        CalculateKnockup();
                //        //alert($(this).val());
                //    }

                //    //}
                //    //CalculateGrandTotal();
                //});
                // });
                //$(document).ready(function () {

                //    $('.form-control_New').change(function () {
                //       // alert("A");
                //       // CalculateKnockup();
                //    });

                //});               
                //$(document).ready(function () {
                //    {
                function CalculateKnockup() {
                    //var total = 0;
                    //$(".form-control_New").each(function () {
                    //    total += parseFloat($(this).val());
                    //    $('#ContentPlaceHolder1_lblSumofKnockplan').text(total);
                    //    //$(selectCheckbox).closest("tr").find($("[id*=txtKnockup]")).css("color", "black");
                    //    $(this).css("color", "black");
                    //});
                }

                //            $('#ContentPlaceHolder1_lblSumofKnockplan').text(total);
                //            //$(selectCheckbox).closest("tr").find($("[id*=txtKnockup]")).css("color", "black");

                //        }
                //    }
                //    });
                function unCheckSelectAll(selectCheckbox, val) {

                    //var total = 0;
                    //if (selectCheckbox.checked) {
                    //    $(selectCheckbox).closest("tr").find($("[id*=txtKnockup]")).val($(selectCheckbox).closest("tr").find("td:eq(6)").text());
                    //    CalculateKnockup();
                    //    total = $('#ContentPlaceHolder1_lblSumofKnockplan').text();
                    //    //alert(total);
                    //    var RemainingAmount = $('#ContentPlaceHolder1_lblRemaining').text();
                    //    var Balance = parseInt(RemainingAmount) - parseInt(total);

                    //    $('#ContentPlaceHolder1_lblBalanceAmount').text(Balance);

                    //    if (parseInt(Balance) > 0) {
                    //        $('#ContentPlaceHolder1_lblBalanceAmount').css("color", "green")
                    //    }
                    //    else {

                    //        $('#ContentPlaceHolder1_lblBalanceAmount').css("color", "red")
                    //    }

                    //    if (parseInt(total) > parseInt(RemainingAmount)) {
                    //        $(selectCheckbox).closest("tr").find($("[id*=txtKnockup]")).css("color", "red");
                    //    }
                    //}
                    //else {

                    //    $(selectCheckbox).closest("tr").find($("[id*=txtKnockup]")).val("0.00");
                    //    CalculateKnockup();
                    //    total = $('#ContentPlaceHolder1_lblSumofKnockplan').text();
                    //    var RemainingAmount = $('#ContentPlaceHolder1_lblRemaining').text();
                    //    $(selectCheckbox).closest("tr").find($("[id*=txtKnockup]")).css("color", "black");
                    //    var Balance = parseInt(RemainingAmount) - parseInt(total);
                    //    if (isNaN(parseInt(Balance))) {
                    //        $('#ContentPlaceHolder1_lblBalanceAmount').text(parseInt(RemainingAmount) - parseInt(total));
                    //    }
                    //    else {

                    //        $('#ContentPlaceHolder1_lblBalanceAmount').text(Balance);
                    //        if (parseInt(Balance) > 0) {
                    //            $('#ContentPlaceHolder1_lblBalanceAmount').css("color", "green")
                    //        }
                    //        else {
                    //            $('#ContentPlaceHolder1_lblBalanceAmount').css("color", "red")
                    //        }
                    //    }
                    //}
                }


            </script>
            <div class="col-md-10 col-md-offset-1" style="margin-bottom: -12px !important; margin-top: 5px !important">
                <asp:Button ID="btnSettlement" runat="server" Text="Settlement" OnClick="btnSettlement_Click" Style="display: none" />
                <div class="col-md-12" style="text-align: center; left: 0px;">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red">

                    </asp:Label>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">Settlement Plan</div>
                        <div class="panel-body" style="padding-bottom: 9px !important">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    Agency :
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="lblAgency" Text="" runat="server">
                                    </asp:Label>
                                </div>
                            </div>

                            <div class="form-group" style="margin-top: 2px !important">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-4">
                                        Client :
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblClient" Text="" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" style="margin-top: 2px !important">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-4">
                                        Payment Type :
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblPaymentType" Text="" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" style="margin-top: 2px !important">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-4">
                                        Payment Mode :
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblPaymentMode" Text="" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-4">
                                        CRV Nuumber :
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblCRVNumber" Text="" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-4">
                                        Status :
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblStatus" Text="" Style="color: green" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-10">
                                        <asp:Label ID="Label1" Text="" Style="color: green" runat="server">
                                        </asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn-success" Text="Update" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">CRV Information</div>
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    CRV Amount :
                                </div>
                                <div class="col-md-7">
                                    <asp:Label ID="lblCRVAmount" Text="" runat="server">
                                    </asp:Label>
                                </div>
                            </div>

                            <div class="form-group" style="margin-top: 2px !important">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-5">
                                        Tax Amount :
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Label ID="lblTaxAmount" Text="" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" style="margin-top: 2px !important">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-5">
                                        G.S.Tax :
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Label ID="lblGST" Text="" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-5">
                                        Total Amount :
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Label ID="lblTotalAmount" Text="" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-5">
                                        Consumed Amount :
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Label ID="lblCounsumedAmount" Text="" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-5">
                                        Remaining Amount :
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Label ID="lblRemaining" Text="" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-12" style="margin-top: 2px !important">
                                    <div class="col-md-5">
                                        Sum of Knock Of Plan:
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblSumofKnockplan" Text="0.00" Style="color: red" runat="server">
                                        </asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblBalanceAmount" Text="0.00" Style="color: red" runat="server">
                                        </asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-10 col-md-offset-1" style="padding-left: 29px !important; padding-right: 29px !important">
                <asp:GridView ID="gv"  runat="server" CssClass="EU_DataTable" DataKeyNames="InvoiceID" 
                    AutoGenerateColumns="false"
                    AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
                    <PagerStyle  HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No">
                            <HeaderStyle Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CreationDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="8%" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Agency" HeaderText="Agency">
                            <HeaderStyle Width="23%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Client" HeaderText="Client">
                            <HeaderStyle Width="18%" />
                        </asp:BoundField>
                        <%--   <asp:BoundField DataField="Suspended" HeaderText="Suspended">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>--%>




                        <asp:BoundField DataField="BalanceAmount" HeaderText="Net Receiable">
                            <HeaderStyle Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RecivedAmount" HeaderText="Paid Amount">
                            <HeaderStyle Width="8%" />
                        </asp:BoundField>

<%--                        <asp:BoundField DataField="BalanceAmount" HeaderText="Balance Amount">
                            <HeaderStyle Width="8%" />
                        </asp:BoundField>--%>
                             <asp:TemplateField HeaderText="Select for Settlement">
                            <ItemTemplate>
                                <asp:TextBox ID="txtKnockup" DataFormatString="{0:0.00}" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"BalanceAmount")%>' />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select for Settlement">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMultipleInvoices" OnCheckedChanged="chkMultipleInvoices_CheckedChanged" AutoPostBack="true" runat="server" Height="15px" Width="15px" ToolTip='<%# DataBinder.Eval(Container.DataItem,"ClientId")%>' />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Knock of Plan">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelected" OnCheckedChanged="chkSelected_CheckedChanged" AutoPostBack="true" runat="server" Enabled="false" Height="15px" Width="15px" ToolTip='<%# DataBinder.Eval(Container.DataItem,"InvoiceId")%>' Checked='<%# DataBinder.Eval(Container.DataItem,"IsChecked")%>' />
                                <asp:HiddenField ID ="hdchecked" Value= '<%# Eval("IsChecked")  %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText ="ISDN">
                            <ItemTemplate >
                                <asp:Label ID="lblIsDN" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"IsDN")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       
                     

                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gv" EventName="PageIndexChanging" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
