<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MissingReport.aspx.cs" Inherits="AMR.MissingReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <script type="text/javascript">
        Sys.Application.add_load(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_beginRequest(function () {
                $('#ajaxLoader').show();
            });

            prm.add_endRequest(function () {
                $('#ajaxLoader').hide();
            });

        });
    </script>
    <style>
    .loader-overlay {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: rgba(255,255,255,0.85);
        z-index: 99999;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }

    .spinner {
        width: 60px;
        height: 60px;
        border: 6px solid #ccc;
        border-top: 6px solid #28a745;
        border-radius: 50%;
        animation: spin 1s linear infinite;
    }

    .loader-text {
        margin-top: 15px;
        font-size: 16px;
        font-weight: bold;
        color: #333;
    }

    @keyframes spin {
        0% { transform: rotate(0deg); }
        100% { transform: rotate(360deg); }
    }

    .subcat-box {
    border: 1px solid #ddd;
    padding: 12px;
    border-radius: 6px;
    background-color: #fafafa;
    max-height: 300px;
    overflow-y: auto;
}

.subcat-list td {
    padding: 6px 12px;
    white-space: nowrap;
}

.subcat-list input[type="checkbox"] {
    margin-right: 6px;
}

    </style>
    <div id="ajaxLoader" style="display: none;">
        <div class="loader-overlay">
            <div class="spinner"></div>
            <div class="loader-text">Loading data, please wait...</div>
        </div>
    </div>
    <div class="col-md-10 col-md-offset-1">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" style="font-weight: bold">&nbsp;<h4>MISSING REPORT</h4>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="center">&nbsp;<table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right" style="width: 75px">
                            <strong>No. of Intervals:
                            </strong>&nbsp;
                        </td>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                         <asp:TextBox ID="txtintervals" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="width: 75px">
                            <strong>End Date: </strong>&nbsp;
                        </td>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <input id="txtenddate" type="date" class="form-control" runat="server" style="font-size: small;" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 15px">
                        <td colspan="2" style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                            <strong>Publication Group:&nbsp;</strong></td>
                        <td align="left">
                            <asp:DropDownList ID="ddlpubgroup" runat="server"  CssClass="form-control" style="font-size: small;">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPublicationGroup" runat="server"
                                ControlToValidate="ddlpubgroup"
                                ErrorMessage="Please select a publication group."
                                ForeColor="Red"
                                Display="Dynamic"
                                InitialValue="">
                            </asp:RequiredFieldValidator>                                               
                        </td>
                    </tr>
                    <tr style="height: 15px">
                        <td colspan="2" style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                            <strong>Main Category:&nbsp;</strong></td>
                        <td align="left">
                            <asp:DropDownList ID="ddlmaincat" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlmaincat_SelectedIndexChanged" Style="font-size: small;"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlmaincat"
                                ErrorMessage="Please select a Main Category."
                                ForeColor="Red"
                                Display="Dynamic"
                                InitialValue="">
                            </asp:RequiredFieldValidator>                                            
                        </td>
                    </tr>
                    <tr style="height: 15px">
                        <td colspan="2" style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                            <strong>Sub Category:&nbsp;</strong></td>
                        <td align="left">
                            <asp:CheckBoxList ID="chkSubCat" runat="server" CssClass="subcat-list" RepeatDirection="Horizontal" RepeatColumns="6">
                            </asp:CheckBoxList>
                            <asp:CustomValidator ID="cvSubCat" runat="server" 
                                ErrorMessage="Please select at least one Sub Category."
                                ForeColor="Red" Display="Dynamic">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr style="height: 15px">
                        <td colspan="2" style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                            <strong>City:&nbsp;</strong></td>
                        <td align="left">
                            <asp:DropDownList ID="ddlcity" runat="server"  CssClass="form-control" style="font-size: small;">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="ddlcity"
                                ErrorMessage="Please select a City."
                                ForeColor="Red"
                                Display="Dynamic"
                                InitialValue="">
                            </asp:RequiredFieldValidator>                                               
                        </td>
                    </tr>
                    <tr style="height: 15px">
                        <td colspan="2" style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td style="width: 75px"></td>
                        <td align="left">
                            <asp:Button ID="btnSearch" CssClass="btn btn-success" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnClear" CssClass="btn btn-success" runat="server" OnClick="btnClear_Click" Text="Clear" />
                            <asp:Button ID="btnexport" CssClass="btn btn-success" runat="server" OnClick="btnexport_Click" Text="Export to Excel" />
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
    <div class="col-md-10 col-md-offset-1" style="margin-top: 30px;">
    <asp:Label ID="lblReportHeading" runat="server" CssClass="h3 text-primary" />
</div>

    <%--grid start--%>
    <div class="form-horizontal">
        <div class="col-md-10 col-md-offset-1">
            <div class="form-group">
                <div class="div-md-12" >
                </div>
                <div class="div-md-12" style="margin-top: 50px; ">
                    <asp:GridView ID="gvfirst" PageSize="100" runat="server" CssClass="EU_DataTable" AutoGenerateColumns="false" EnableViewState="false" ShowFooter="true" EmptyDataText="No data found.">
                        
                        <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>

<%--grid end--%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexport" />
        </Triggers>
</asp:UpdatePanel>
</asp:Content>
