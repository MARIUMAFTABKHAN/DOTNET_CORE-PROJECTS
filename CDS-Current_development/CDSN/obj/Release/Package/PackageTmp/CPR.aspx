<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="CPR.aspx.cs" Inherits="CDSN.CPR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <style type="text/css">
        .ListBoxCssClass {
            color: #4d758b;
            background-color: #c8c8c8;
            font-weight: 300;
            width: 100%;
            height: 90%;
        }

        .lbls {
            color: #FFF;
            background-color: #4d758b;
            font-weight: 500;
            height: 25px;
            font-size: large;
            width: 100%;
            text-align: center;
        }

        .highlite {
            background-color: Gray;
        }

        .EU_DataTable tr:nth-child(2n+2) {
            background-color: #fff;
        }

        /* ensure modal body & map have real height */
    #mapModal .modal-body{ height:70vh; }
    #map{ width:100%; height:100%; }
    </style>

        <!-- Load Google Maps once at render-time using your Web.config key -->
<script
  src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD12KWCfJdVVjJx37hedrwFKlNPS2DoxxQ&callback=googleMapsReady"
  async defer></script>


<script type="text/javascript" language="javascript">

        function showPleaseWait() {
            // alert("hello");
            document.getElementById('PleaseWait').style.display = 'block';

        }
        function AlertMessaged(x, e) {

            if (x.status == 0) {
                ShowMessage("You are offline!!\n Please Check Your Network");
            }
            else if (x.status == 404) {
                ShowMessage("Requested URL not found.");
            }
            else if (x.status == 500) {
                ShowMessage("Internal Server Error.");
            }
            else if (e == 'parsererror') {
                ShowMessage("Error.\nParsing JSON Request failed");
            }
            else if (e == 'timeout') {
                ShowMessage(" Request Time out");
            } else {
                ShowMessage("Unknow Error.\n'" + x.responseText);
            }
        }

        function pageLoad() {
            SetDDL();
        }


        $(document).ready(function () {
            SetDDL();

        });


        function SetDDL() {

            $('#MainContent_ddlTerritory').SumoSelect({
                placeholder: 'Select Territory', includeSelectAllOption: true, okCancelInMulti: true, search: true, searchText: 'Select Country', selectAll: true
            });

            $('#MainContent_ddlDivision').SumoSelect({ okCancelInMulti: true, search: true, searchText: 'Enter here.', selectAll: true });

            $('#MainContent_ddlCity').SumoSelect({ okCancelInMulti: true, search: true, searchText: 'Enter here.', selectAll: true });

            $('#MainContent_ddlCountry').SumoSelect({

                placeholder: 'Select Country', includeSelectAllOption: true, okCancelInMulti: true, search: true, searchText: 'Select Country', selectAll: true
            });

        }


        function hideme() {
            //$find('ModalPopupExtender1').hide();
            $find('BehaveMap').hide();
            return false;
        }

        function SetLabelData(result) {

            dtData = [];
            $.each(result, function () {

                dtData.push([
                    this.OperatorName,
                    this.SubAreaName
                ]);
            });
            if (dtData.length > 0) {
                SetLabel(dtData[0]);
            }
        }
        function SetData(result) {

            dtData = [];
            $.each(result, function () {
                dtData.push([
                    this.OperatorName
                ]);
            });

            splitRecord(dtData);
            SetLabel(dtData[0]);
        }

        function splitRecord(str) {
            var lBox = $('select[id$=lb]');
            $("#lb").empty()
            var listItems = [];
            for (var i = 0; i < str.length; i++) {

                listItems.push('<option value="' +
                    str[i] + '">' + str[i]
                    + '</option>');
            }
            $(lBox).append(listItems.join(''));
        }

    function openInfoModal(operatorId) {
        $.ajax({
            url: 'CPRService.asmx/GetOperatorProprietorInfo',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ operatorId: operatorId }),
            success: function (r) {
                var d = r.d;
                var html = `
                <div class="row">
                    <div class="col-md-6">
                        <h5><b>Operator Info</b></h5>
                        <p><b>Operator:</b> ${d.OperatorName}</p>
                        <p><b>Address:</b> ${d.Address}</p>
                        <p><b>Cell No:</b> ${d.Cell}</p>
                        <p><b>LandLine:</b> ${d.LandLine}</p>
                        <p><b>Lic.Category:</b> ${d.LicCategory}</p>
                        <p><b>Review Date:</b> ${d.LicReviewDate}</p>
                        <p><b>Status:</b> ${d.LicStatus}</p>
                    </div>
                    <div class="col-md-6">
                        <h5><b>Proprietor Info</b></h5>
                        <p><b>Name:</b> ${d.ProprietorName}</p>
                        <p><b>CNIC:</b> ${d.CNIC}</p>
                        <p><b>Email:</b> ${d.Email}</p>
                        <p><b>Contact:</b> ${d.ContactNo}</p>
                        <p><b>Cell:</b> ${d.CellNo}</p>
                        <p><b>City:</b> ${d.CityName}</p>
                        <p><b>Address:</b> ${d.PropAddress}</p>
                    </div>
                </div>`;
                $('#infoContent').html(html);
                $('#infoModal').modal('show');
            },
            error: function (xhr) {
                console.error("Error fetching info", xhr.responseText);
            }
        });
    }

</script>

  

<script>
    var gReady = false, gMap = null, gMarkers = [], gQueue = [];

    function googleMapsReady() {
        console.log("Google Maps API ready");
        gReady = true;
        while (gQueue.length) (gQueue.shift())();
    }
    window.googleMapsReady = googleMapsReady;

    function whenMapReady(fn) {
        if (gReady && google && google.maps) fn();
        else gQueue.push(fn);
    }

    function openAreaMap(operatorId) {
        console.log('openAreaMap', operatorId);

        // Bind event first
        $('#mapModal').off('shown.bs.modal').on('shown.bs.modal', function () {
            whenMapReady(function () {
                drawOperatorMap(operatorId);
                if (gMap) google.maps.event.trigger(gMap, 'resize');
            });
        });

        // Now show modal
        $('#mapModal').modal('show');
    }


    function drawOperatorMap(operatorId) {
        console.log('Requesting GetOperatorMap for', operatorId);

        if (!gMap) {
            gMap = new google.maps.Map(document.getElementById('map'), {
                zoom: 12,
                center: { lat: 30, lng: 70 }
            });
        }

        gMarkers.forEach(m => m.setMap(null));
        gMarkers = [];

        //$.ajax({
        //    url: 'CPR.aspx/GetOperatorMap', // ✅ call real method
        //    type: 'POST',
        //    contentType: 'application/json; charset=utf-8',
        //    dataType: 'json',
        //    xhrFields: { withCredentials: true }, // ✅ send cookies/session
        //    data: JSON.stringify({ operatorId: operatorId }),

        //    success: function (r) {
        //        console.log('Points:', r.d);
        //        drawMarkers(r.d);
        //    },
        //    error: function (xhr) {
        //        console.error('GetOperatorMap failed', xhr.responseText);
        //    }
        //});

        $.ajax({
            url: 'CPRService.asmx/GetOperatorMap', // ✅ Call ASMX instead of CPR.aspx
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            xhrFields: { withCredentials: true }, // ✅ Send cookies/session
            data: JSON.stringify({ operatorId: operatorId }),
            success: function (r) {
                console.log('Points:', r.d);
                drawMarkers(r.d);
            },
            error: function (xhr) {
                console.error('GetOperatorMap failed', xhr.responseText);
            }
        });




    }

    function drawMarkers(points) {
        var bounds = new google.maps.LatLngBounds();
        var markers = [];

        points.forEach(function (p) {
            var pos = { lat: p.Lat, lng: p.Lng };
            var iconUrl = p.IconImage || null;
            if (iconUrl && !iconUrl.toLowerCase().startsWith('/content/images/')) {
                iconUrl = '/Content/Images/' + iconUrl.replace(/^\/+/, '');
            }

            var marker = new google.maps.Marker({
                position: pos,
                icon: iconUrl,
                map: gMap
            });

            if (p.InfoWindowText) {
                var info = new google.maps.InfoWindow({ content: p.InfoWindowText });
                marker.addListener('click', function () { info.open(gMap, marker); });
            }

            markers.push(marker);
            bounds.extend(pos);
        });

        

        if (points.length) {
            gMap.fitBounds(bounds);
        }
        
    }

    // Called when user clicks subarea link in InfoWindow
    function showalert(subAreaId) {
        $.ajax({
            url: 'CPRService.asmx/GetOperatorsBySubArea',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ subAreaId: subAreaId }),
            success: function (r) {
                var ops = r.d;
                var html = "";

                if (ops.length > 0) {
                    html += "<h5>Operators in " + ops[0].SubAreaName + "</h5>";
                    html += "<ul>";
                    ops.forEach(function (op) {
                        html += "<li>" + op.OperatorName + " (" + op.Subscribers + " subs)</li>";
                    });
                    html += "</ul>";
                } else {
                    html = "<p>No operators found for this subarea.</p>";
                }

                $('#operatorsModal .modal-body').html(html);
                $('#operatorsModal').modal('show');
            },
            error: function (xhr) {
                console.error("Error fetching operators", xhr.responseText);
            }
        });
    }



</script>

    <div class="col-md-12">


        <div class="helptext" id="PleaseWait" style="display: none; text-align: right; color: White; vertical-align: top;">
            <table id="MyTable" style="background-color: red" align="center">
                <tr>
                    <td style="width: 250px" align="center">
                        <b style="color: #fff">Please Wait...</b>
                    </td>
                </tr>
            </table>
        </div>

    </div>

    <div class="container">

        <div class="row" style="margin-top: 0px;">
            <div class="col-md-12">
                <h4>&nbsp;&nbsp;&nbsp;&nbsp;Channel Position Report
                </h4>
            </div>
        </div>



        <div class="row" style="margin-top: 5px">
            <div class="col-md-12">
                <div class="col-md-2">
                    Channel Type
                </div>
                <div class="col-md-3">


                    <asp:DropDownList class="form-control" ID="ddlChannelType" runat="server"
                        OnSelectedIndexChanged="ddlChannelType_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    County
                </div>
                <div class="col-md-3">
                    <asp:DropDownList class="form-control" ID="ddlCountry" runat="server"
                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>

                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 5px">
            <div class="col-md-12">
                <div class="col-md-2">
                    Region
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Territory
                </div>
                <div class="col-md-3">
                    <asp:ListBox Multiple="Multiple" SelectionMode="Multiple" AutoPostBack="true" runat="server" ID="ddlTerritory" CssClass="form-control" OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged"></asp:ListBox>

                </div>


            </div>
        </div>
        <div class="row" style="margin-top: 5px">
            <div class="col-md-12">

                <div class="col-md-2">
                    Division
                </div>
                <div class="col-md-3">
                    <asp:ListBox Multiple="Multiple" runat="server" SelectionMode="Multiple" ID="ddlDivision" CssClass="form-contrtol" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:ListBox>


                </div>
                <div class="col-md-2">
                    City
                </div>
                <div class="col-md-3">
                    <asp:ListBox Multiple="Multiple" SelectionMode="Multiple" runat="server" ID="ddlCity" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:ListBox>

                </div>
            </div>

        </div>

        <div class="row" style="margin-top: 5px">
            <div class="col-md-12">

                <div class="col-md-3 col-md-offset-2">
                    <asp:Button ID="BtnShow" runat="server" CssClass="btn btn-info" Style="min-width: 100%; width: 100% !important" OnClick="BtnShow_Click"
                        OnClientClick="showPleaseWait()" Text="View" />
                </div>

            </div>
        </div>
        <div class="row" style="margin-top: 5px">
            <div class="col-md-12">
                <div class="col-md-3 col-md-offset-2">
                    <asp:TextBox runat="server" placeholder="Enter City" ID="txtCity" class="form-control" />
                </div>

                <div class="col-md-3 col-md-offset-2">
                    <asp:TextBox runat="server" placeholder="Enter Operator" ID="txtOperator" class="form-control" />
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 5px">
            <div class="col-md-12">
                <div class="col-md-3 col-md-offset-2">
                    <asp:TextBox runat="server" placeholder="Ente District " ID="txtDistrict" class="form-control" />
                </div>

                <div class="col-md-3 col-md-offset-2">
                    <asp:Button ID="btnSearch" Style="min-width: 100% !important; width: 100%!important" OnClick="btnSearch_Click" OnClientClick="showPleaseWait()" runat="server" Text="Search" class="btn btn-danger"></asp:Button>
                </div>

            </div>
        </div>
        <div class="col-md-12">
            <asp:GridView ID="gvChannelView" DataKeyNames="OperatorId,CityId,DivisionId,TerritoryId"
                OnClientClick="showPleaseWait()" CssClass="EU_DataTable" runat="server" AutoGenerateColumns="True"
                OnRowCommand="gvChannelView_RowCommand"
                OnDataBound="gvChannelView_DataBound">
                <Columns>

                    <asp:TemplateField HeaderText="Info">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkInfo" runat="server"
                                CommandName="Info"
                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                ToolTip="Info">
                                            <span class="glyphicon glyphicon-info-sign"></span>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="36px" />
                        <HeaderStyle HorizontalAlign="Center" Width="36px" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Area">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkArea" runat="server"
                                CommandName="Area"
                                CommandArgument='<%# Eval("OperatorId") %>'
                                ToolTip="Area">
                                             <span class="glyphicon glyphicon-map-marker"></span>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <%--<asp:TemplateField HeaderText="Activity">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkActivity" runat="server"
                                CommandName="Activity"
                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                ToolTip="Activity">
                                            <span class="glyphicon glyphicon-flash"></span>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="36px" />
                        <HeaderStyle HorizontalAlign="Center" Width="36px" />
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>

        <!-- Bootstrap modal to host the map -->
        <div class="modal fade" id="mapModal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-xl" role="document" style="width: 95%">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Operator Area Map</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body" style="height: 70vh">
                        <div id="map" style="width: 100%; height: 100%"></div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="operatorsModal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<h5 class="modal-title">Operators in Subarea</h5>--%>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span>&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <!-- Operator list will be injected here -->
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="infoModal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Operator & Proprietor Info</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span>&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="infoContent">
                            <!-- Operator info will be injected here -->
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <asp:HiddenField ID="hfGoogleMapsKey" runat="server" />
        <asp:HiddenField ID="hfOperatorId" runat="server" />



    </div>
       
</asp:Content>
