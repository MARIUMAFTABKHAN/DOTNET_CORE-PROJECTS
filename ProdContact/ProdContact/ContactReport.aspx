<%@ Page Title="" Language="C#" Async="true"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactReport.aspx.cs" Inherits="ProdContact.ContactReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ContentTemplate>

        <!-- Flatpickr -->
        <link href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" rel="stylesheet" />
        <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>

        <script type="text/javascript">

            /*UTILITY FUNCTIONS */
            function scrollToActive(container, item) {
                const containerTop = container.scrollTop();
                const containerBottom = containerTop + container.outerHeight();
                const itemTop = item.position().top + containerTop;
                const itemBottom = itemTop + item.outerHeight();

                if (itemBottom > containerBottom) {
                    container.scrollTop(itemBottom - container.outerHeight());
                } else if (itemTop < containerTop) {
                    container.scrollTop(itemTop);
                }
            }

            /*AUTOCOMPLETE*/
            function setupAutocomplete(inputId, resultBoxId, hiddenFieldId, apiProxyUrl, displayField, idField) {

                let selectedIndex = -1;

                $("#" + inputId).on("input", function () {
                    const query = $(this).val();
                    selectedIndex = -1;

                    if (query.length < 2) {
                        $("#" + resultBoxId).hide();
                        return;
                    }

                    $.ajax({
                        url: apiProxyUrl + encodeURIComponent(query),
                        method: "GET",
                        dataType: "json",
                        success: function (data) {
                            let html = "";
                            data.forEach(item => {
                                html += `<div class="client-option" data-id="${item[idField]}">${item[displayField]}</div>`;
                            });
                            $("#" + resultBoxId).html(html).show();
                        },
                        error: function () {
                            $("#" + resultBoxId)
                                .html("<div style='padding:5px;color:red;'>Error fetching results</div>")
                                .show();
                        }
                    });
                });

                $("#" + inputId).on("keydown", function (e) {
                    const items = $("#" + resultBoxId + " .client-option");
                    const container = $("#" + resultBoxId);

                    if (!items.length) return;

                    if (e.key === "ArrowDown") {
                        e.preventDefault();
                        selectedIndex = (selectedIndex + 1) % items.length;
                    }
                    else if (e.key === "ArrowUp") {
                        e.preventDefault();
                        selectedIndex = (selectedIndex - 1 + items.length) % items.length;
                    }
                    else if (e.key === "Enter") {
                        e.preventDefault();
                        if (selectedIndex >= 0) items.eq(selectedIndex).click();
                        return;
                    }
                    else if (e.key === "Escape") {
                        container.hide();
                        return;
                    }

                    items.removeClass("active");

                    if (selectedIndex >= 0) {
                        const activeItem = items.eq(selectedIndex);
                        activeItem.addClass("active");
                        scrollToActive(container, activeItem);
                    }
                });

                $(document).on("click", "#" + resultBoxId + " .client-option", function () {
                    $("#" + inputId).val($(this).text());
                    $("#" + hiddenFieldId).val($(this).data("id"));
                    $("#" + resultBoxId).hide();
                });
            }

            /*RADIO TOGGLE (CLIENT / AGENCY)*/
            function setupVisitToToggle() {
                $('input[name="optionsRadios"]').change(function () {
                    const isAgency = $(this).val() === "Agency";

                    $("#labelClientAgency").text(isAgency ? "Agency" : "Client");

                    $("#<%= txtClient.ClientID %>").toggle(!isAgency);
                    $("#clientResults").toggle(!isAgency);

                    $("#<%= txtAgency.ClientID %>").toggle(isAgency);
                    $("#agencyResults").toggle(isAgency);
                });

                $('input[name="optionsRadios"]:checked').trigger("change");
            }

            /*DATE & TIME PICKERS*/
            function initPickers() {

                // ❌ REMOVE jQuery UI datepicker COMPLETELY

                flatpickr("#<%= txtmvisitdate.ClientID %>", {
                    dateFormat: "Y-m-d"
                });

                flatpickr("#<%= txtfvisitdate.ClientID %>", {
                    dateFormat: "Y-m-d"
                });

                flatpickr("#<%= txtmvisisttime.ClientID %>", {
                    enableTime: true,
                    noCalendar: true,
                    dateFormat: "h:i K"
                });

                flatpickr("#<%= txtfvisisttime.ClientID %>", {
                    enableTime: true,
                    noCalendar: true,
                    dateFormat: "h:i K"
                });
            }


            /*GEOLOCATION*/
            function setupGeolocation() {
                document.getElementById('<%= btnSave.ClientID %>').addEventListener("click", function () {
                    if (!navigator.geolocation) return;

                    navigator.geolocation.getCurrentPosition(
                        pos => {
                            $("#<%= hdnLatitude.ClientID %>").val(pos.coords.latitude);
                            $("#<%= hdnLongitude.ClientID %>").val(pos.coords.longitude);
                        }
                    );
                });
            }

            /*DOCUMENT READY (INIT)*/
            $(document).ready(function () {

                setupVisitToToggle();
                initPickers();              // ✅ FIXED
                setupGeolocation();

                setupAutocomplete("<%= txtClient.ClientID %>", "clientResults",
                    "<%= hiddenClientId.ClientID %>",
                    "SearchClientProxy.ashx?name=", "name", "clientId");

                setupAutocomplete("<%= txtAgency.ClientID %>", "agencyResults",
                    "<%= hiddenAgencyId.ClientID %>",
                    "SearchAgencyProxy.ashx?name=", "name", "agencyId");

                setupAutocomplete("<%= txtproject.ClientID %>", "projectResults",
                    "<%= hiddenProjectId.ClientID %>",
                    "SearchProjectProxy.ashx?name=", "projectName", "projectId");

                $(document).on("click", function (e) {
                    if (!$(e.target).closest(".autocomplete-box, input").length) {
                        $(".autocomplete-box").hide();
                    }
                });
            });


        </script>


        <style>
            .autocomplete-box {
                position: absolute;
                background: #fff;
                border: 1px solid #ccc;
                max-height: 150px;
                overflow-y: auto;
                z-index: 1000;
                width: 100%;
            }

            .client-option {
                padding: 6px;
                cursor: pointer;
            }

            .client-option.active {
                background-color: #007bff;
                color: #fff;
            }


        </style>

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
                                    <label>Project</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtproject" runat="server" CssClass="form-control" autocomplete="off" />
                                    <div id="projectResults" class="autocomplete-box"></div>
                                    <asp:HiddenField ID="hiddenProjectId" runat="server" />

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
</asp:Content>
