<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RevenueReport.aspx.cs" Inherits="AMR.RevenueReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <script type="text/javascript">
        // Setup dropdown utility
        function setupDropdown(textboxId, checklistId) {
            var textbox = document.getElementById(textboxId);
            var listContainer = textbox?.nextElementSibling;

            if (!textbox || !listContainer) return;

            textbox.onclick = function () {
                listContainer.style.display = listContainer.style.display === 'none' ? 'block' : 'none';
            };

            document.querySelectorAll('#' + checklistId + ' input[type=checkbox]').forEach(cb => {
                cb.addEventListener('change', () => {
                    const selected = Array.from(document.querySelectorAll('#' + checklistId + ' input[type=checkbox]:checked'))
                        .map(cb => cb.nextSibling.textContent.trim());
                    textbox.value = selected.join(', ');
                });
            });

            document.addEventListener('click', function (e) {
                if (!e.target.closest('.dropdown-checkbox-wrapper')) {
                    listContainer.style.display = 'none';
                }
            });
        }

        // Attach dropdown setup after each postback
        Sys.Application.add_load(function () {
            setupDropdown('<%= txtSelectedPublications.ClientID %>', '<%= chkPub.ClientID %>');
            setupDropdown('<%= txtSelectedSubCategories.ClientID %>', '<%= chksubcat.ClientID %>');

            // Publication filter
            $('#<%= txtSelectedPublications.ClientID %>').off('keyup').on('keyup', function () {
                var searchText = $(this).val().toLowerCase();
                $('#<%= chkPub.ClientID %> input[type=checkbox]').each(function () {
                    var text = $(this).parent().text().toLowerCase();
                    if (text.indexOf(searchText) !== -1) {
                        $(this).parent().show();
                    } else {
                        $(this).parent().hide();
                    }
                });
            });

            // Subcategory filter
            $('#<%= txtSelectedSubCategories.ClientID %>').off('keyup').on('keyup', function () {
                var searchText = $(this).val().toLowerCase();
                $('#<%= chksubcat.ClientID %> input[type=checkbox]').each(function () {
                    var text = $(this).parent().text().toLowerCase();
                    if (text.indexOf(searchText) !== -1) {
                        $(this).parent().show();
                    } else {
                        $(this).parent().hide();
                    }
                });
            });

        });

        // Loader on async postback
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            if (elem && elem.id === '<%= btnSearch.ClientID %>') {
                document.getElementById("loaderOverlay").style.display = "block";
            }
        });
        prm.add_endRequest(function () {
            document.getElementById("loaderOverlay").style.display = "none";
        });


        <%--$(document).ready(function () {
            $('#<%= txtSelectedPublications.ClientID %>').on('keyup', function () {
                var searchText = $(this).val().toLowerCase();
                $('#<%= chkPub.ClientID %> input[type=checkbox]').each(function () {
                    var label = $(this).next('label');
                    var text = label.text().toLowerCase();
                    if (text.indexOf(searchText) !== -1) {
                        label.show();
                        $(this).show();
                    } else {
                        label.hide();
                        $(this).hide();
                    }
                });
            });

            // For Sub Categories
            $('#<%= txtSelectedSubCategories.ClientID %>').on('keyup', function () {
                var searchText = $(this).val().toLowerCase();
                $('#<%= chksubcat.ClientID %> input[type=checkbox]').each(function () {
                    var label = $(this).next('label');
                    var text = label.text().toLowerCase();
                    if (text.indexOf(searchText) !== -1) {
                        label.show();
                        $(this).show();
                    } else {
                        label.hide();
                        $(this).hide();
                    }
                });
            });
        });--%>

        

    </script>


    <style>
    /*.checkbox-list-style {
        height: 250px;
        overflow-y: auto;
        padding: 10px;
        border: 1px solid #ccc;
        background-color: #f9f9f9;
        font-size: small;
    }

    .checkbox-list-style label {
        display: inline-block;
        margin-right: 15px;
        white-space:nowrap;
    }*/

    .checkbox-list-style-main {
        height: 50px;
        overflow-y: inherit;
        padding: 10px;
        border: 1px solid #ccc;
        background-color: #f9f9f9;
        font-size: small;
}

    .checkbox-list-style-main label {
        display: inline-block;
        margin-right: 15px;
        white-space:nowrap;
    }

    .dropdown-checkbox-wrapper {
        position: relative;
        width: 300px;
    }

    .dropdown-checkbox-textbox {
        width: 100%;
        padding: 6px;
        cursor: pointer;
    }

    .dropdown-checkbox-list {
        position: absolute;
        background-color: white;
        border: 1px solid #ccc;
        max-height: 200px;
        overflow-y: auto;
        width: 100%;
        z-index: 1000;
    }

    .checkbox-list-style input[type="checkbox"] {
        margin-right: 6px;
    }
    .spinner-border {
    display: inline-block;
    width: 3rem;
    height: 3rem;
    border: 0.25em solid currentColor;
    border-right-color: transparent;
    border-radius: 50%;
    animation: spinner-border 0.75s linear infinite;
}
@keyframes spinner-border {
    100% {
        transform: rotate(360deg);
    }
}



</style>

    <div id="loaderOverlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background: rgba(255,255,255,0.8); z-index: 2000;">
        <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%,-50%);">
            <div class="spinner-border text-success" role="status" style="width: 3rem; height: 3rem;">
                <span class="sr-only">Loading...</span>
            </div>
            <div style="text-align: center; margin-top: 10px;">Processing...</div>
        </div>
    </div>

    <div class="col-md-10 col-md-offset-1">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" style="font-weight: bold">&nbsp;<h4>REVENUE REPORT</h4>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="center">&nbsp;<table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right" style="width: 75px">
                            <strong>Start Date:&nbsp;</strong>
                        </td>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                         <input id="txtstartdate" type="date" class="form-control" runat="server" style="font-size: small;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="width: 75px">
                            <strong>End Date:&nbsp;</strong>
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
                            <strong>Publication:&nbsp;</strong></td>
                        <td align="left">
                            <div class="dropdown-checkbox-wrapper">
                                <asp:TextBox ID="txtSelectedPublications" runat="server" 
                                    CssClass="dropdown-checkbox-textbox" ReadOnly="false" />
                                <div class="dropdown-checkbox-list" style="display: none;">
                                    <asp:CheckBoxList ID="chkPub" runat="server" 
                                        CssClass="checkbox-list-style pub-checkbox-list" RepeatColumns="1" />
                                </div>
                            </div>

                        </td>
                    </tr>
                    <tr style="height: 15px">
                        <td colspan="2" style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                            <strong>Main Category:&nbsp;</strong></td>
                        <td align="left">
                            <asp:CheckBoxList ID="chkmaincat" runat="server" CssClass="checkbox-list-style" RepeatColumns="1"
                                OnSelectedIndexChanged="chkMainCat_SelectedIndexChanged" AutoPostBack="true"/>
                        </td>
                    </tr>

                    <tr style="height: 15px">
                        <td colspan="2" style="height: 20px"></td>
                    </tr>

                    <tr>
                        <td align="right" style="width: 150px">
                            <strong>Sub Category:&nbsp;</strong></td>
                        <td align="left">
                            <div class="dropdown-checkbox-wrapper">
                                <asp:TextBox ID="txtSelectedSubCategories" runat="server"
                                    CssClass="dropdown-checkbox-textbox" ReadOnly="false" />
                                <div class="dropdown-checkbox-list" style="display: none;">
                                    <asp:CheckBoxList ID="chksubcat" runat="server" 
                                        CssClass="checkbox-list-style subcat-checkbox-list" RepeatColumns="1"/>
                                </div>
                            </div>
                        </td>
                    </tr>

                    <tr style="height: 15px">
                        <td colspan="2" style="height: 20px"></td>
                    </tr>

                    <tr>
                        <td align="right" style="width: 150px">
                            <strong>With Supp:&nbsp;</strong></td>
                        <td align="left">
                            <div class="dropdown-checkbox-wrapper">
                                <asp:CheckBox ID="chsup" runat="server" />
                            </div>
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
    <%--grid start--%>
    <div class="form-horizontal">
        <div class="col-md-10 col-md-offset-1">
            <div class="form-group">
                <div class="div-md-12" >
                </div>
                <div class="div-md-12" style="margin-top: 50px; ">
                    <asp:GridView ID="gvfirst" PageSize="100" runat="server" CssClass="EU_DataTable" AutoGenerateColumns="false" 
                        EnableViewState="false" ShowFooter="true" OnDataBound="gvfirst_DataBound" EmptyDataText="No data found for the selected criteria.">
                        <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField DataField="Pub_Abreviation" HeaderText="PUB"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="Publication_Date" HeaderText="DATE" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" ><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="Client_Name" HeaderText="CLIENT NAME"></asp:BoundField>
                                <asp:BoundField DataField="Address_Line_4" HeaderText="CLIENT CITY"></asp:BoundField>
                                 <asp:BoundField DataField="MainCategory_Title" HeaderText="MAIN CATEGORY"></asp:BoundField>
                                <asp:BoundField DataField="SubCategory_Title" HeaderText="SUB CATEGORY"></asp:BoundField>
                                <%--<asp:BoundField DataField="CM" HeaderText="CM"></asp:BoundField>--%>
                                <asp:TemplateField HeaderText="CM">
                                    <ItemTemplate>
                                        <asp:Literal ID="CMControl" runat="server" Text='<%# Eval("CM") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalCM" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="City_Editions" HeaderText="EDITION"></asp:BoundField>
                                <asp:BoundField DataField="Colour_BW" HeaderText="COLOR"></asp:BoundField>
                                <asp:BoundField DataField="Page" HeaderText="PAGE"></asp:BoundField>
                                <%--<asp:BoundField DataField="RateAmount" HeaderText="AMOUNT"></asp:BoundField>--%>
                                <asp:TemplateField HeaderText="AMOUNT">
                                    <ItemTemplate>
                                        <asp:Literal ID="RateAmountControl" runat="server" Text='<%# Eval("RateAmount", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>


                            </Columns>

                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>

<%--grid end--%>
        </ContentTemplate>
      <Triggers>
        <asp:AsyncPostBackTrigger ControlID="chkmaincat" EventName="SelectedIndexChanged" />
          <asp:PostBackTrigger ControlID="btnexport" />

    </Triggers>
</asp:UpdatePanel>
</asp:Content>
