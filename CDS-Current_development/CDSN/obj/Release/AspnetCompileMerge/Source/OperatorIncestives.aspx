<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="OperatorIncestives.aspx.cs" Inherits="CDSN.OperatorIncestives" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function pageLoad() {

            //$("#Button1").unbind();
            $('#<%= txtwef.ClientID  %>').datepicker({
                showOn: 'button',
                buttonImage: "Content/images/calander.png",
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                maxDate: '+0m',                
                changeMonth: true,
                changeYear: true
            });

        }
    </script>
    <style type="text/css">
        .row {
            margin-bottom: 5px !important;
        }
    </style>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="col-md-10 col-md-offset-1">
                <div class="row" style="text-align: center">
                    <h3>Operator Incentive</h3>
                </div>
                <%--<asp:Panel ID="pnlRecord" runat="server" CssClass="CollapsePanelBody" Width="100%">--%>

                <div class="row">
                    <div class="col-md-12">
                        <div align="center" style="height: 19px">
                            <asp:Label ID="lblMsg" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblException" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Date:
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtwef" runat="server" Enabled="False" CssClass="form-control" Style="display: inline; width: 75%"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Territory:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlterritory" runat="server" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlterritory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        District:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlDistricts" runat="server" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlDistricts_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        City:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                       Operator:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlOp" runat="server" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlOp_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                       Incentive Type:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlIncentype" runat="server" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlIncentype_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Type Details:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlIncentiveDetailType" runat="server" CssClass="form-control" 
                            OnSelectedIndexChanged="ddlIncentiveDetailType_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Item Info.
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtInfo" CssClass="form-control" runat="server" Rows="4" Style="min-width: 100%; height: 50px" MaxLength="150"
                            TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-4 col-md-offset-2">
                        <div class="col-md-6">
                            <asp:Button ID="btnSave" Style="min-width: 100%" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-info btn-block" />
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnCancel" Style="min-width: 100%" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger btn-block" />
                        </div>
                    </div>

                    <asp:HiddenField ID="IncentiveId" runat="server"></asp:HiddenField>
                </div>
                <%--</asp:Panel>--%>

                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-4 col-md-offset-2">
                            <asp:Label ID="lblGrid" runat="server"  CssClass="lbl" ></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvRecords" runat="server" DataKeyNames="Id"
                                AutoGenerateColumns="False" CssClass="EU_DataTable"  Width="100%" OnPageIndexChanging="gvRecords_PageIndexChanging">
                                <HeaderStyle BackColor="#4D758B" ForeColor="white" HorizontalAlign="Center" Height="25px" />
                                <Columns>
                                    <%--<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id">
                                        <ItemStyle HorizontalAlign="Left" Width="120" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="ItemInfo" HeaderText="Item Detail">
                                        <ItemStyle HorizontalAlign="Left" Width="400" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OperatoName" HeaderText="Operator Name">
                                        <ItemStyle HorizontalAlign="Left" Width="350" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IncentiveTypename" HeaderText="Incentive Type">
                                        <ItemStyle HorizontalAlign="Left" Width="200" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IncentiveDetailTypename" HeaderText="Incentive Detail Type">
                                        <ItemStyle HorizontalAlign="Left" Width="200" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DateIncentive" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Date">
                                        <ItemStyle HorizontalAlign="Left" Width="200" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="status" HeaderText="Active">
                                        <ItemStyle HorizontalAlign="Left" Width="200" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="CENTER" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" ImageUrl="~/Content/Images/News-Edit.ico" runat="server"
                                                OnClick="btnEdit_Click" ValidationGroup="a" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" ImageUrl="~/Content/Images/Trash-Cancel.ico" runat="server"
                                                ValidationGroup="a" OnClick="btnDelete_Click" CommandArgument='<%#Eval("Id")%>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
