<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PublicationWiseSummary.aspx.cs" Inherits="AMR.PublicationWiseSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
<ContentTemplate>


    <script type="text/javascript">
        
        $(document).ready(function () {
            console.log("jQuery is loaded");
            if ($.fn.modal) {
                console.log("Bootstrap modal is loaded");
            } else {
                console.log("Bootstrap modal is not loaded");
            }
        });
        function ShowPopup() {
            console.log('ShowPopup called');
            $('#clientDetailsModal').modal('show');
        }

        function searchClients() {
            var query = $('#<%= txtClient.ClientID %>').val();
            if (query.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "PublicationWiseSummary.aspx/SearchClients",
                    data: JSON.stringify({ searchText: query }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var results = response.d;
                        $('#clientResults').empty();
                        if (results.length > 0) {
                            $.each(results, function (i, item) {
                                $('#clientResults').append('<div onclick="selectClient(\'' + item.Id + '\', \'' + item.Client_Name + '\')">' + item.Client_Name + '</div>');
                            });
                            $('#clientResults').show();
                        } else {
                            $('#clientResults').hide();
                        }
                    }
                });
            } else {
                $('#clientResults').hide();
            }
        }

        function selectClient(id, name) {
            $('#<%= txtClient.ClientID %>').val(name);
            $('#<%= hiddenClientId.ClientID %>').val(id);

            console.log('Hidden Client ID:', $('#<%= hiddenClientId.ClientID %>').val());
            $('#clientResults').hide();


        }

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
            
            setupDropdown('<%= txtSelectedSubCategories.ClientID %>', '<%= chksubcat.ClientID %>');

            

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
        .modal-dialog {
            margin: auto;
            top: 50%;
            transform: translateY(-50%);
        }

        .modal-content {
            width: 100vw; /* Adjust width as needed */
            max-width: 1000px; /* Adjust max-width as needed */
        }
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
                <td align="left" style="font-weight: bold">&nbsp;<h4>Publication Wise Summary</h4>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="center">&nbsp;<table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right" style="width: 75px">
                            <strong>From Date:
                            </strong>&nbsp;
                        </td>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                         <input id="txtfromdate" type="date" class="form-control" runat="server" style="font-size: small;" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="width: 75px">
                            <strong>To Date: </strong>&nbsp;
                        </td>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <input id="txttodate" type="date" class="form-control" runat="server" style="font-size: small;" />
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
                            <strong>City:&nbsp;</strong></td>
                        <td align="left">
                            <asp:DropDownList ID="ddlcity" runat="server"  CssClass="form-control" style="font-size: small;">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlcity"
                                ErrorMessage="Please select a City."
                                ForeColor="Red"
                                Display="Dynamic"
                                InitialValue="">
                            </asp:RequiredFieldValidator>   --%>                                            
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
                                OnSelectedIndexChanged="chkMainCat_SelectedIndexChanged" AutoPostBack="true" />
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
                                        CssClass="checkbox-list-style subcat-checkbox-list" RepeatColumns="1" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr style="height: 15px">
                        <td colspan="2" style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                            <strong>Client:&nbsp;</strong></td>
                        <td align="left">
                            <asp:TextBox ID="txtClient" runat="server" onkeyup="searchClients()" CssClass="form-control" />
                            <div id="clientResults" style="
                                    display:none;
                                    max-height: 500px;
                                    overflow-y: auto;
                                    border: 1px solid #ccc;
                                    border-radius: 4px;
                                    background-color: lightblue;
                                    z-index: 1000;
                                    position: absolute;
                                    width: 50%;
                                    box-shadow: 0 2px 6px rgba(0,0,0,0.2);"></div>
                            <asp:HiddenField ID="hiddenClientId" runat="server" EnableViewState="true" />
                             
                        </td>
                    </tr>

                    <tr style="height: 15px">
                        <td colspan="2" style="height: 20px"></td>
                    </tr>

                    <tr>
                        <td align="right" style="width: 150px">
                            <strong>Supp:&nbsp;</strong></td>
                        <td align="left">
                            <div class="dropdown-checkbox-wrapper">
                                <asp:CheckBox ID="chsup" runat="server" Checked="true"/>
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
            <div class="form-group" style="width: 100%;">
                <div class="div-md-12" style="width: 100%;">
                </div>
                <div class="div-md-12" style="margin-top: 50px; width: 100%;">
                    <asp:GridView ID="gvfirst" PageSize="100" runat="server" CssClass="EU_DataTable" 
                        AutoGenerateColumns="false" Width="100%" EnableViewState="true" ShowFooter="true" EmptyDataText="No data found.">
                        <FooterStyle CssClass="footer-bold" />
                        <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <%--new work--%>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Panel ID="clientDetailsPanel" runat="server" Visible="false">
                                        <asp:GridView ID="gvClientDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"></asp:GridView>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <%--new work end--%>

                    </asp:GridView>
                </div>

               
            </div>

        </div>
    </div>
    <asp:HiddenField ID="hfClientId" runat="server" />
    <asp:HiddenField ID="hfRowHeaderText" runat="server" />
    <asp:HiddenField ID="hfTypeId" runat="server" />

     <%--<div class="form-horizontal">
        <div class="col-md-10 col-md-offset-1">
            <div id="clientDetailsModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title" id="clientDetailsModalLabel">Client Details</h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="gvClientDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" EnableViewState="true" ></asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
         </div>
     </div>--%>
<%--grid end--%>
        </ContentTemplate>
        <%--<Triggers>
        <asp:AsyncPostBackTrigger ControlID="gvClientDetails" />
    </Triggers>--%>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexport" />
        </Triggers>
</asp:UpdatePanel>
</asp:Content>
