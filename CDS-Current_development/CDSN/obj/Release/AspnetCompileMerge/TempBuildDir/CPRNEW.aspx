<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="CPRNEW.aspx.cs" Inherits="CDSN.CPRNEW" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="dcb" %>

<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .ListBoxCssClass {
            color:#4d758b; background-color:#c8c8c8; font-weight:300; width:100%; height:90%;
        }
        .lbls {
            color:#FFF; background-color:#4d758b; font-weight:500; height:25px; font-size:large; width:100%; text-align:center;
        }
        .map { position:absolute; overflow:hidden; width:71% !important; height:340px !important; margin:5px; }
        #subgurim_GMap3 { width:100% !important; height:340px !important; float:left; display:block; }
        .style3 { text-align:right; }
        .structural { position:absolute; left:-9999px; }
        .drp { z-index:900000 !important; }
        .lstborder { border:none !important; overflow:hidden !important; overflow-y:hidden !important; overflow-x:hidden !important; padding-left:14px; }
        .lblborder { border:1px solid #B8B4B4; margin-top:2px; margin-bottom:2px; margin-left:10px; text-align:center; }
        .panelheader .lbl { color:#fff; font-weight:bold; }
        .Button { /* keep your legacy button class if used */ }
    </style>

    <script type="text/javascript">
        function hideme() {
            $find('BehaveMap').hide();
            return false;
        }
        function showPleaseWait() {
            var el = document.getElementById('PleaseWait');
            if (el) el.style.display = 'block';
        }
        function AlertMessaged(x, e) {
            if (x.status == 0) { ShowMessage("You are offline!!\n Please Check Your Network"); }
            else if (x.status == 404) { ShowMessage("Requested URL not found."); }
            else if (x.status == 500) { ShowMessage("Internal Server Error."); }
            else if (e == 'parsererror') { ShowMessage("Error.\nParsing JSON Request failed"); }
            else if (e == 'timeout') { ShowMessage("Request Time out"); }
            else { ShowMessage("Unknown Error.\n'" + x.responseText); }
        }
        function showalert(Id) {
            document.getElementById("<%= lb.ClientID %>").options.length = 0;
            var CellData = '{"SubAreaID": "' + Id + '"}';
            $.ajax({
                type: "POST",
                url: "GetOperator.asmx/GetOperatorList",
                data: CellData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) { SetData(response.d); SetLabelData(response.d); },
                error: function (x, e) { AlertMessaged(x, e); }
            });
        }
        function SetLabelData(result) {
            var dtData = [];
            $.each(result, function () { dtData.push([this.OperatorName, this.SubAreaName]); });
            if (dtData.length > 0) { SetLabel(dtData[0]); }
        }
        function SetData(result) {
            var dtData = [];
            $.each(result, function () { dtData.push([this.OperatorName]); });
            splitRecord(dtData);
            SetLabel(dtData[0]);
        }
        function SetLabel(str) {
            $("#<%= lbl.ClientID %>").text(str[1]);
        }
        function splitRecord(str) {
            var lBox = $('select[id$=lb]');
            $("#lb").empty();
            var listItems = [];
            for (var i = 0; i < str.length; i++) {
                listItems.push('<option value="' + str[i] + '">' + str[i] + '</option>');
            }
            $(lBox).append(listItems.join(''));
        }


        // Called by Google when the API is loaded AND by server after UpdatePanel postbacks
        window.initMap = function () {
            var mapEl = document.getElementById('map');
            if (!mapEl) return;

            var center = (window.__mapCenter) ? window.__mapCenter : { lat: 24.8607, lng: 67.0011 };
            var map = new google.maps.Map(mapEl, { center: center, zoom: 12 });

            (window.__markers || []).forEach(function (m) {
                var marker = new google.maps.Marker({
                    position: { lat: m.lat, lng: m.lng },
                    map: map,
                    icon: m.icon || null
                });

                if (m.html) {
                    var iw = new google.maps.InfoWindow({ content: m.html });
                    marker.addListener('click', function () { iw.open(map, marker); });
                }
            });
        };

        // Re-initialize after UpdatePanel async postbacks (so map refreshes on BtnShow_Click etc.)
        if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                if (window.google && window.google.maps && typeof window.initMap === 'function') {
                    window.initMap();
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="MainContent" runat="Server">
    <!-- If your master already has a ScriptManager/ToolkitScriptManager, remove the next line -->
 


    <asp:UpdatePanel ID="updRoot" runat="server">
        <ContentTemplate>
            <table style="width:100%">
                <tr>
                    <td>
                        <asp:Panel CssClass="panelheader" ID="pnlRecordHeader" runat="server" Width="100%">
                            <div style="float:left; width:95%; height:20px;">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblRecordHeader" runat="server" CssClass="lbl" Text="Set Parameters" />
                                <div class="helptext" id="PleaseWait" style="display:none; text-align:right; color:#fff; vertical-align:top;">
                                    <table id="MyTable" style="background-color:red;" align="center">
                                        <tr>
                                            <td style="width:250px" align="center"><b><span style="color:#fff">Please Wait...</span></b></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div style="float:right; width:4%; height:20px; padding-top:5px;"></div>
                        </asp:Panel>

                        <asp:Panel ID="pnlRecords" runat="server" CssClass="CollapsePanelBody" Width="870">
                            <asp:UpdatePanel ID="updFilters" runat="server">
                                <ContentTemplate>
                                    <table style="width:100%">
                                        <tr>
                                            <td style="width:120px" class="style3">
                                                <asp:Label ID="Label4" runat="server" Text="Channel Type :"></asp:Label>
                                            </td>
                                            <td style="width:230px">
                                                <asp:DropDownList ID="ddlChannelType" runat="server" Width="180px"
                                                    CssClass="Button" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlChannelType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="style3" style="width:120px">
                                                <asp:Label ID="Label5" runat="server" Text="Country :"></asp:Label>
                                            </td>
                                            <td style="width:230px">
                                                <asp:DropDownList ID="ddlCountry" runat="server" Width="180px" CssClass="Button"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="style3" style="width:120px">
                                                <asp:Label ID="Label6" runat="server" Text="Region :"></asp:Label>
                                            </td>
                                            <td style="width:230px">
                                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="Button" Width="180px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="style3" style="width:120px">
                                                <asp:Label ID="Label7" runat="server" Text="Territory :"></asp:Label>
                                            </td>
                                            <td style="width:230px">
                                                <div style="width:200px; float:left; z-index:0!important">
                                                    <dcb:DropDownCheckBoxes ID="ddlTerritory" runat="server" AddJQueryReference="False"
                                                        OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged"
                                                        Style="top:0; left:0; height:7px; width:180px; margin-top:38px; z-index:1000"
                                                        UseButtons="true" UseSelectAllNode="True">
                                                        <Style2 DropDownBoxBoxHeight="75" DropDownBoxBoxWidth="180" SelectBoxWidth="179" />
                                                    </dcb:DropDownCheckBoxes>
                                                </div>
                                                <div style="width:30px; float:left">
                                                    <asp:ImageButton ID="imgterritory" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Plus+.GIF"
                                                        Width="16px" />
                                                </div>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>

                                        <tr id="trTerritory" runat="server">
                                            <td class="style3" style="width:120px">&nbsp;</td>
                                            <td style="width:230px">
                                                <asp:TextBox ID="lblTerritory" runat="server" BackColor="#CCCCCC" BorderStyle="None"
                                                    ReadOnly="true" Rows="2" TextMode="MultiLine" Width="674px"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td class="style3" style="width:120px">
                                                <asp:Label ID="Label8" runat="server" Text="District :"></asp:Label>
                                            </td>
                                            <td style="width:230px">
                                                <div style="width:200px; float:left;">
                                                    <dcb:DropDownCheckBoxes ID="ddlDivision" runat="server" AddJQueryReference="False"
                                                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                                        Style="top:0; left:0; height:7px; width:180px; margin-top:38px; z-index:2001!important"
                                                        UseButtons="true" UseSelectAllNode="True">
                                                        <Style2 DropDownBoxBoxHeight="75" DropDownBoxBoxWidth="180" SelectBoxWidth="179" />
                                                    </dcb:DropDownCheckBoxes>
                                                </div>
                                                <div style="width:30px; float:left">
                                                    <asp:ImageButton ID="ImgDisticts" runat="server" Height="16px" ImageAlign="Middle"
                                                        ImageUrl="~/Images/Plus+.GIF" Width="16px"  />
                                                </div>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>

                                        <tr id="trDistrict" runat="server">
                                            <td class="style3" style="width:120px">&nbsp;</td>
                                            <td style="width:230px">
                                                <asp:TextBox ID="lblDistrict" runat="server" BackColor="#CCCCCC" BorderStyle="None"
                                                    ReadOnly="true" Rows="2" TextMode="MultiLine" Width="674px"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td class="style3" style="width:120px">
                                                <asp:Label ID="Label9" runat="server" Text="City  :"></asp:Label>
                                            </td>
                                            <td style="width:230px">
                                                <div style="width:200px; float:left;">
                                                    <dcb:DropDownCheckBoxes ID="ddlCity" runat="server" AddJQueryReference="False"
                                                        OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"
                                                        Style="top:46px; left:-6px; height:7px; width:180px; margin-top:38px; z-index:1999!important"
                                                        UseButtons="true" UseSelectAllNode="True">
                                                        <Style2 DropDownBoxBoxHeight="75" DropDownBoxBoxWidth="180" SelectBoxWidth="179" />
                                                    </dcb:DropDownCheckBoxes>
                                                </div>
                                                <div style="width:30px; float:left">
                                                    <asp:ImageButton ID="ImgCity" runat="server" Height="16px" ImageAlign="Middle"
                                                        ImageUrl="~/Images/Plus+.GIF" Width="16px"  />
                                                </div>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>

                                        <tr id="trCity" runat="server">
                                            <td class="style3" style="width:120px">&nbsp;</td>
                                            <td colspan="2" style="width:230px">
                                                <asp:TextBox ID="lblCity" runat="server" BackColor="#CCCCCC" BorderStyle="None" ReadOnly="true"
                                                    Rows="2" TextMode="MultiLine" Width="674px"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td></td>
                                            <td>
                                                <div style="width:800px">
                                                    <div style="width:153px; display:block; float:left; margin:2px 2px 2px -3px">
                                                        City:
                                                        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div style="width:153px; display:block; float:left; margin:2px">
                                                        Operator:
                                                        <asp:TextBox ID="txtOperator" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div style="width:153px; display:block; float:left; margin:2px">
                                                        Properitor:
                                                        <asp:TextBox ID="txtDistrict" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div style="width:153px; display:block; float:left; margin:2px">
                                                        Area:
                                                        <asp:TextBox ID="txtArea" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div style="width:30px; display:block; float:left; margin:2px; padding-top:8px">
                                                        <asp:ImageButton ID="btnSearch" runat="server" Height="32px" ImageAlign="Middle"
                                                            ImageUrl="~/Images/Search.png" Width="32px"
                                                            OnClick="btnSearch_Click" OnClientClick="showPleaseWait()" />
                                                    </div>
                                                    <div style="width:30px; display:block; float:left; margin:2px; padding-top:11px">
                                                        <asp:ImageButton ID="btnClear" runat="server" Height="24px" ImageAlign="Middle"
                                                            ImageUrl="~/Images/ClearTxt.png" Width="24px"
                                                             OnClientClick="showPleaseWait()" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="style3" style="width:120px">&nbsp;</td>
                                            <td colspan="2" style="width:230px; padding-left:2px">
                                                <asp:Button ID="BtnShow" runat="server" CssClass="Button" Text="Show Summary" Width="100px"
                                                    OnClick="BtnShow_Click" OnClientClick="showPleaseWait()" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td>
                        <table style="width:100%" border="0" cellspacing="2" cellpadding="0" align="center">
                            <tr>
                                <td align="left" style="width:100%">
                                    <asp:Panel CssClass="panelheader" ID="Panel1" runat="server" Width="100%">
                                        <div style="float:left; width:95%; height:20px;">
                                            &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" CssClass="lbl" Text="(Channel statistic)" />
                                        </div>
                                        <div style="float:right; width:4%; height:20px; padding-top:5px;"></div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlRecord_" runat="server" CssClass="CollapsePanelBody" Width="100%">
                                        <asp:GridView ID="gvChannelView" runat="server" Width="100%"
                                            DataKeyNames="OperatorId"
                        OnRowCreated="gvChannelView_RowCreated"
                        OnRowDataBound="gvChannelView_RowDataBound"
                        >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Info">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Info" ImageUrl="~/Images/info.gif" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Area">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="BtnMap" runat="server" ImageUrl="~/Images/Chart2-Edit.ico" CommandName="Google" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity">
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:ImageButton ID="imgbtn" runat="server" ImageUrl="~/Images/Mail-reply.ico"
                                                                CausesValidation="false" />
                                                        </center>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="District">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Bind("District") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdterritoryid" runat="server" Value='<%# Bind("TerritoryID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="City">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Operator">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlView" runat="server" Text='<%# Eval("Operator") %>'
                                                            NavigateUrl='<%# "~/HeadendHistory.aspx?Opid=" + Eval("OperatorId") + "&DistrictID=" + Eval("DivisionId") + "&CityID=" + Eval("CityId") %>'>
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="200px" />
                                                    <ItemStyle Width="200px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#4D758B" ForeColor="white" HorizontalAlign="Center" Height="25px" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <!-- Activity Modal -->
            <asp:Button ID="btnShowPopup" runat="server" Style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                TargetControlID="btnShowPopup" PopupControlID="pnlpopup" CancelControlID="btnCancelP"
                BackgroundCssClass="modalBackground" />

            <asp:Panel ID="pnlpopup" runat="server" BackColor="white" Width="850px" Style="display:none">
                <table style="border:2px solid #B8B4B4; padding:10px; width:100%; background-color:#FCF8F8" cellpadding="0" cellspacing="0">
                    <tr style="background-color:#cccccc; height:25px">
                        <td colspan="2">
                            <asp:Label ID="lblHeadEndName" Font-Bold="True" Font-Size="Medium" runat="server" />
                            <div style="display:none">
                                <asp:Label ID="lblresult" runat="server" />
                                <asp:HiddenField ID="hdop" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr style="background-color:#4d758b; color:White">
                        <td colspan="2" style="height:30px; font-weight:bold; font-size:larger" align="center">Activity</td>
                    </tr>
                    <tr><td style="width:100px"></td><td>&nbsp;</td></tr>
                    <tr>
                        <td align="right" width="100px">Reply:</td>
                        <td><asp:TextBox ID="txtReply" runat="server" Rows="5" TextMode="MultiLine" Width="650px" MaxLength="2000"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="height:400px; width:800px; margin:0 auto; overflow:scroll">
                                <asp:GridView ID="gvContact" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CssClass="Button"
                                    PageSize="80"
                                    RowStyle-BackColor="White" RowStyle-ForeColor="#333333" Width="100%">
                                    <HeaderStyle BackColor="#4D758B" ForeColor="white" Height="25px" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                        <asp:BoundField DataField="MessageDate" HeaderText="Date" />
                                        <asp:BoundField DataField="MessageTime" HeaderText="Time" />
                                        <asp:BoundField DataField="TerritoryName" HeaderText="Territory" />
                                        <asp:BoundField DataField="OperatorName" HeaderText="Operator" />
                                        <asp:BoundField DataField="Messagetxt" HeaderText="Message" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdisreponded" runat="server" Value='<%# Bind("IsResponded") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="Send" CssClass="Button"  />
                            <asp:Button ID="btnCancelP" runat="server" Text="Cancel" CssClass="Button" />
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                </table>
            </asp:Panel>

            <!-- Operator Info Modal -->
            <asp:Button ID="btnOperatorInfo" runat="server" Style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                TargetControlID="btnOperatorInfo" PopupControlID="pnlOPpopup"
                BackgroundCssClass="modalBackground" />
            <asp:Panel ID="pnlOPpopup" runat="server" BackColor="white" Width="750px" Style="height:auto">
                <div style="width:120px; float:right; display:block; margin-left:600px">
                    <asp:ImageButton ID="ImageButton4" runat="server" AlternateText="Close" CausesValidation="false"
                        ImageUrl="~/Images/cross.png" Style="margin-top:10px; margin-left:120px" />
                </div>
                <div style="border:5px solid #B8B4B4; padding:10px; width:100%; background-color:#FCF8F8; height:230px;">
                    <div style="width:50%; float:left; display:block; height:auto; overflow:hidden">
                        <table style="width:100%">
                            <tr style="background-color:#4d758b; color:White; width:100%; height:25px;"><td colspan="2"><h3>Operator Info.</h3></td></tr>
                            <tr><td style="width:115px">Operator:</td><td style="width:290px"><asp:Label ID="lblOName" runat="server" /></td></tr>
                            <tr><td>Address:</td><td><asp:Label ID="lblOAddress" runat="server" /></td></tr>
                            <tr><td>Cell No:</td><td><asp:Label ID="lblOContact" runat="server" Width="290px" /></td></tr>
                            <tr><td>Lic.Category :</td><td><asp:Label ID="lblOLicCategory" runat="server" Width="100px" /></td></tr>
                            <tr><td>Review Date:</td><td><asp:Label ID="lblOReviewDate" runat="server" Width="100px" /></td></tr>
                            <tr><td>Lic.Status:</td><td><asp:Label ID="lblstatus" runat="server" /></td></tr>
                        </table>
                    </div>
                    <div style="width:50%; float:left; display:block; height:auto; overflow:hidden">
                        <table style="width:100%">
                            <tr style="background-color:#4d758b; color:White; width:100%; height:25px;"><td colspan="2"><h3>Properitor Info.</h3></td></tr>
                            <tr><td style="width:115px">Properitor:</td><td style="width:290px"><asp:Label ID="lblPName" runat="server" /></td></tr>
                            <tr><td>C.NIC:</td><td><asp:Label ID="lblPCNIC" runat="server" /></td></tr>
                            <tr><td>Email:</td><td><asp:Label ID="lblPEmail" runat="server" Width="290px" /></td></tr>
                            <tr><td>Contact:</td><td><asp:Label ID="lblPContact" runat="server" Width="100px" /></td></tr>
                            <tr><td>cell No.:</td><td><asp:Label ID="lblPCellNO" runat="server" Width="100px" /></td></tr>
                            <tr><td>Address:</td><td><asp:Label ID="lblPAddress" runat="server" /></td></tr>
                            <tr><td>City:</td><td><asp:Label ID="lblPCity" runat="server" /></td></tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <!-- Map Modal -->
            <asp:Button ID="BtnMap" runat="server" Style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server"
                TargetControlID="BtnMap" BehaviorID="BehaveMap" PopupControlID="pnlMap"
                BackgroundCssClass="modalBackground" />
            <asp:Panel CssClass="pnlMap" ID="pnlMap" runat="server" BackColor="white" Width="80%" Style="height:auto; margin:0 auto;">
                <div style="border:5px solid #B8B4B4; padding:10px; background-color:#FCF8F8; height:350px">
                    <div style="width:100%; height:350px; display:block; float:left; border:1px solid #B8B4B4; z-index:0; background-color:#FCF8F8; overflow:hidden;">
                        <div style="margin-left:95%; position:absolute; margin-top:-40px; z-index:100">
                            <img alt="" src="Images/mapClose1.png" style="margin-top:10px; margin-left:20px" onclick="hideme()" />
                        </div>
                        <div id="map" class="map"></div>
                        <div runat="server" style="width:25%; height:338px; margin:5px; display:block; float:right; z-index:1001; border:1px solid #B8B4B4; background-color:#EEEEEE;">
                            <asp:Label ID="lbl" runat="server" Width="100%" CssClass="lbls"></asp:Label>
                            <asp:ListBox ID="lb" runat="server" CssClass="ListBoxCssClass"></asp:ListBox>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
