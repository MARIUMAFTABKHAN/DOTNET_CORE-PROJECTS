<%@ Page Title="" Language="C#" Async="true"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactReport.aspx.cs" Inherits="AMR.ContactReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:UpdatePanel ID="up" runat="server">--%>
    <ContentTemplate>
        

        <script type="text/javascript" language="javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                if (args.get_error() != undefined) {
                    //  **alert(args.get_error().message.substr(args.get_error().name.length + 2));
                    args.set_errorHandled(true);
                }
            }

            $(document).ready(function () {

                applyTimePicker();

                // Event: radio selection toggle
                $('input[name="optionsRadios"]').change(function () {
                    const selected = $('input[name="optionsRadios"]:checked').val();

                    if (selected === 'Agency') {
                        $("#labelClientAgency").text("Agency");

                        $("#<%= txtClient.ClientID %>").hide();
                        $("#clientResults").hide();

                        $("#<%= txtAgency.ClientID %>").show();
                        $("#agencyResults").show();
                    } else {
                        $("#labelClientAgency").text("Client");

                        $("#<%= txtAgency.ClientID %>").hide();
                        $("#agencyResults").hide();

                        $("#<%= txtClient.ClientID %>").show();
                        $("#clientResults").show();
                    }
                });

                // Generic autocomplete setup
                function setupAutocomplete(inputId, resultBoxId, hiddenFieldId, apiProxyUrl, displayField) {
                    $("#" + inputId).on("input", function () {
                        var query = $(this).val();

                        if (query.length < 2) {
                            $("#" + resultBoxId).hide();
                            return;
                        }

                        $.ajax({
                            url: apiProxyUrl + encodeURIComponent(query),
                            method: "GET",
                            dataType: "json",
                            success: function (data) {
                                var resultHtml = "";
                                data.forEach(function (item) {
                                    resultHtml += "<div class='client-option' data-id='" + item.id + "' style='padding: 5px; cursor: pointer;'>" + item[displayField] + "</div>";
                                });

                                $("#" + resultBoxId).html(resultHtml).show();
                            },
                            error: function () {
                                $("#" + resultBoxId).html("<div style='padding:5px;color:red;'>Error fetching results</div>").show();
                            }
                        });
                    });

                    $(document).on("click", "#" + resultBoxId + " .client-option", function () {
                        const selectedName = $(this).text();
                        const selectedId = $(this).data("id");

                        $("#" + inputId).val(selectedName);
                        $("#" + hiddenFieldId).val(selectedId);
                        $("#" + resultBoxId).hide();
                    });
                }


                // Initialize both autocomplete fields
                setupAutocomplete("<%= txtClient.ClientID %>", "clientResults", "<%= hiddenClientId.ClientID %>", "SearchClientProxy.ashx?name=", "clientName");
                setupAutocomplete("<%= txtAgency.ClientID %>", "agencyResults", "<%= hiddenAgencyId.ClientID %>", "SearchAgencyProxy.ashx?name=", "agencyName");


                // Click anywhere else closes both boxes
                $(document).on("click", function (e) {
                    if (!$(e.target).closest("#<%= txtClient.ClientID %>, #clientResults, #<%= txtAgency.ClientID %>, #agencyResults").length) {
                        $("#clientResults, #agencyResults").hide();
                    }
                });

                // Trigger once on load to apply visibility state if pre-filled
                $('input[name="optionsRadios"]:checked').trigger('change');

                setupAutocomplete("<%= txtBrand.ClientID %>", "brandResults", "<%= hiddenBrandId.ClientID %>", "SearchBrandProxy.ashx?name=", "brandName");


            });
        </script>
        <script src="Scripts/jquery-ui.js"></script>
        <link href="Scripts/jquery-ui.css" rel="stylesheet" />
                
        <!-- Flatpickr CSS -->
        <link href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" rel="stylesheet" />

        <!-- Flatpickr JS -->
        <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>

        <script type="text/javascript">
            function pageLoad() {
                applyDatePicker();
                applyTimePicker();
            }
            function applyDatePicker() {


                $("#MainContent_txtbirthdate").datepicker({
                    showOn: 'button',
                    buttonImageOnly: true,
                    changeMonth: true,
                    changeYear: true,
                    buttonImage: 'Content/Images/Calender.png',
                    dateFormat: 'dd/mm/yy'
                });


            }
            function applyTimePicker() {
                flatpickr("#<%= txtmvisisttime.ClientID %>", {
                enableTime: true,
                noCalendar: true,
                dateFormat: "h:i K", // e.g. 10:15 AM
                time_24hr: false
                });

                flatpickr("#<%= txtfvisisttime.ClientID %>", {
                    enableTime: true,
                    noCalendar: true,
                    dateFormat: "h:i K",
                    time_24hr: false
                });
            }


        </script>
        <style>
    .styled-textarea {
        width: 100%;
        height: 150px;
        padding: 10px;
        border: 1px solid #ccc;
        resize: none;
        font-size: 14px;
        color: #333;
        overflow-y: auto;
    }

    .styled-textarea::placeholder {
        color: #888;
        font-style: italic;
    }
    .autocomplete-box {
    display: none;
    max-height: 150px;
    overflow-y: auto;
    border: 1px solid #ccc;
    border-radius: 4px;
    background-color: white;
    z-index: 1000;
    position: absolute;
    width: 100%;
    box-shadow: 0 2px 6px rgba(0,0,0,0.2);
}
</style>

        <script>
            document.getElementById('<%= btnSave.ClientID %>').addEventListener("click", async function (e) {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(function (position) {
                    document.getElementById('<%= hdnLatitude.ClientID %>').value = position.coords.latitude;
                    document.getElementById('<%= hdnLongitude.ClientID %>').value = position.coords.longitude;
                    }, function () {
                        alert("Geolocation not available or denied.");
                    });
                } else {
                    alert("Geolocation is not supported by this browser.");
                }
            });
        </script>

        <div class="col-md-12">
            <asp:HiddenField ID="hdnLatitude" runat="server" />
            <asp:HiddenField ID="hdnLongitude" runat="server" />


            <div class="col-md-10 col-md-offset-1" style="margin-top: 5px !important">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: lightblue;">Contact Report</div>
                    <div class="panel-body" style="padding-bottom: 9px !important">

                        <div class="col-md-12">
                            <div class="col-md-12 text-center">
                                <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-1">
                                        <label>Visit To</label>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" type="radio" name="optionsRadios" id="Agency" value="Agency">
                                        <label class="form-check-label" for="Agency">Agency &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                        <input class="form-check-input" type="radio" name="optionsRadios" id="Client" value="Client">
                                        <label class="form-check-label" for="Client">Client&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <label>Main Category</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlmaincat" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlmaincat_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <label>Sub Category</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlsubcat" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-1">
                                    <label id="labelClientAgency">Client</label>
                                </div>
                                <div class="col-md-5">
                                    <!-- Client TextBox -->
                                    <asp:TextBox ID="txtClient" runat="server" CssClass="form-control" autocomplete="off" />
                                    <div id="clientResults" class="autocomplete-box"></div>

                                    <!-- Agency TextBox (initially hidden) -->
                                    <asp:TextBox ID="txtAgency" runat="server" CssClass="form-control" autocomplete="off" Style="display: none;" />
                                    <div id="agencyResults" class="autocomplete-box" style="display: none;"></div>

                                    <asp:HiddenField ID="hiddenClientId" runat="server" EnableViewState="true" />
                                    <asp:HiddenField ID="hiddenAgencyId" runat="server" EnableViewState="true" />

                                </div>
                                <div class="col-md-1">
                                    <label>Brand</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control" autocomplete="off" />
                                    <div id="brandResults" class="autocomplete-box"></div>
                                    <asp:HiddenField ID="hiddenBrandId" runat="server" />

                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>

            <div class="col-md-10 col-md-offset-1" style="margin-top: 5px !important">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: lightblue;">Meeting</div>
                    <div class="panel-body" style="padding-bottom: 9px !important">

                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-1">
                                    <label for="txtmvisitdate">Visit Date</label>
                                </div>
                                <div class="col-md-3">
                                   <input id="txtmvisitdate" type="date" class="form-control" runat="server" style="font-size: small;" />
                                </div>
                                <div class="col-md-1">
                                    <label for="txtmvisisttime">Visit Time</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtmvisisttime" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtmvisisttime" ForeColor="Red" Display="Dynamic"
                                        ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-check mb-2">
                                    <asp:CheckBox ID="chprvisit" runat="server" CssClass="form-check-input" />
                                    <label class="form-check-label" for="chprvisit">P.R Visit</label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-1">
                                    <label for="txtMinutesOfMeeting">Minutes of Meeting</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtMinutesOfMeeting"
                                        runat="server"
                                        TextMode="MultiLine"
                                        CssClass="styled-textarea"
                                        placeholder="Enter meeting notes..."></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtMinutesOfMeeting" ForeColor="Red" Display="Dynamic"
                                        ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chnew" runat="server" CssClass="form-check-input" />
                                    <label class="form-check-label" for="chnew"> New Executive</label>
                                </div>
                                <div class="col-md-1">
                                    <label for="txtnewexename">Exe. Name</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtnewexename" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="txtdesgn">Designation</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtdesgn" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="txtprname">Person Name</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtprname" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-md-10 col-md-offset-1" style="margin-top: 5px !important">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: lightblue;">Follow Up</div>
                    <div class="panel-body" style="padding-bottom: 9px !important">

                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-1">
                                    <label>Visit To</label>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" type="radio" name="optionsRadios2" id="Agency2" value="Agency">
                                        <label class="form-check-label" for="Agency2">Agency &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                        <input class="form-check-input" type="radio" name="optionsRadios2" id="Client2" value="Client">
                                        <label class="form-check-label" for="Client2">Client&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                    </div>
                                </div>
                                <div class="col-md-3"></div>
                                <div class="col-md-1">
                                    <label>Mode</label>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" type="radio" name="optionsRadios3" id="Visit" value="Visit">
                                        <label class="form-check-label" for="Visit">Visit</label>
                                        <input class="form-check-input" type="radio" name="optionsRadios3" id="Call" value="Call">
                                        <label class="form-check-label" for="Call">Call</label>
                                        <input class="form-check-input" type="radio" name="optionsRadios3" id="NoFollowUp" value="NoFollowUp">
                                        <label class="form-check-label" for="NoFollowUp">No Follow Up</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-1">
                                    <label for="txtfvisitdate">Visit Date</label>
                                </div>
                                <div class="col-md-3">
                                   <input id="txtfvisitdate" type="date" class="form-control" runat="server" style="font-size: small;" />
                                </div>
                                <div class="col-md-3"></div>
                                <div class="col-md-1">
                                    <label for="txtfvisisttime">Visit Time</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtfvisisttime" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtfvisisttime" ForeColor="Red" Display="Dynamic"
                                        ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="div-md-12">
                    <div class="col-md-2 col-md-offset-2">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-dark" Text="Cancel"  OnClick="btnCancel_Click" />
                    </div>

                    <br />

                </div>
            </div>

        </div>
    </ContentTemplate>
<%--</asp:UpdatePanel>--%>
</asp:Content>
