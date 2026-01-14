<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactReportView.aspx.cs" Inherits="ProdContact.ContactReportView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script async defer
     src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD12KWCfJdVVjJx37hedrwFKlNPS2DoxxQ">
    </script>

    <script>
        var lat = 0;
        var lng = 0;
        var map;
        var marker;

        $(document).on("click", ".btn-view", function () {

            $("#m_main").text($(this).data("main"));
            $("#m_sub").text($(this).data("sub"));
            $("#m_brand").text($(this).data("brand"));
            $("#m_party").text($(this).data("party"));
            $("#m_visit").text($(this).data("visitdate"));
            $("#m_followup").text($(this).data("followup"));
            $("#m_minutes").text($(this).data("minutes"));
            $("#m_user").text($(this).data("user"));

            lat = parseFloat($(this).data("lat"));
            lng = parseFloat($(this).data("lng"));

            // 🔥 RESET previous marker position
            if (marker) {
                marker.setMap(null);
                marker = null;
            }

            map = null;                 // <-- THIS WAS MISSING
            $("#map").empty();          // <-- CRITICAL


            $("#viewModal").modal("show");
            

        });


        function showLocation() {

            if (!lat || !lng) {
                alert("Location not available");
                return;
            }

            var location = { lat: lat, lng: lng };

            // 🔥 ALWAYS CREATE A NEW MAP INSTANCE
            map = new google.maps.Map(document.getElementById("map"), {
                zoom: 15,
                center: location
            });

            marker = new google.maps.Marker({
                position: location,
                map: map
            });
        }



    </script>


            <%--grid start--%>
            <div class="form-horizontal">
            <div class="col-md-10 col-md-offset-1">
                <div class="form-group">
                    <div class="div-md-12 text-center">
                        <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <h4>CONTACT REPORT VIEW</h4>
                <br />
                <asp:Button ID="btnexport" CssClass="btn btn-success" runat="server" OnClick="btnexport_Click" Text="Export to Excel" />
                <br /><br />
                <div class="form-group"></div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Visit Date From
                        </div>
                        <div class="col-md-2">
                            <input id="txtfromdate" type="date" class="form-control" runat="server" style="font-size: small;" />
                        </div>
                        <div class="col-md-1">
                            To
                        </div>
                        <div class="col-md-2">
                            <input id="txttodate" type="date" class="form-control" runat="server" style="font-size: small;" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Visiting Employee
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtemp" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnsearch" CssClass="btn btn-success" runat="server" Text="Search" OnClick="btnsearch_Click" />
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        
                    </div>
                </div>
                
                <div class="form-group">
                    <div class="div-md-12" style="margin-top: 50px;">
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" ShowFooter="true">
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField DataField="Main_Category" HeaderText="Main Category" />
                                <asp:BoundField DataField="Sub_Category" HeaderText="Sub Category" />
                                <asp:BoundField DataField="Brand_Name" HeaderText="Brand Name" />
                                <%--<asp:BoundField DataField="Visit_To" HeaderText="Visit To" />--%>
                                <asp:BoundField DataField="VisitPartyName" HeaderText="Client/Agency" />
                                <asp:BoundField DataField="Visit_Date" HeaderText="Visit Date" DataFormatString="{0:dd-MM-yyyy}" />
                                <asp:BoundField DataField="NextFollowUp_Date" HeaderText="Next Follow Up Date" DataFormatString="{0:dd-MM-yyyy}" />
                                <asp:BoundField DataField="User_Name" HeaderText="Visiting Employee" />

                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hypEdit" runat="server" 
                                            NavigateUrl='<%# "ContactReport.aspx?Id=" + Eval("ID") + "&Mode=Edit" %>' 
                                                Style="display:block;width:100%;text-align:center;">
                                            <asp:Image
                                                    ID="imgEdit"
                                                    runat="server"
                                                    ImageUrl="~/Content/images/News-Edit.ico"
                                                    Width="14px"
                                                    Height="14px"
                                                    ToolTip="Edit" />
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <a href="javascript:void(0);"
                                           class="btn-view"
                                           data-main="<%# Eval("Main_Category") %>"
                                           data-sub="<%# Eval("Sub_Category") %>"
                                           data-brand="<%# Eval("Brand_Name") %>"
                                           data-party="<%# Eval("VisitPartyName") %>"
                                           data-visitdate="<%# Eval("Visit_Date","{0:dd-MM-yyyy}") %>"
                                           data-followup="<%# Eval("NextFollowUp_Date","{0:dd-MM-yyyy}") %>"
                                           data-minutes="<%# Eval("minutes_of_meetings") %>"
                                           data-user="<%# Eval("User_Name") %>"
                                           data-lat="<%# Eval("Latitude") %>"
                                           data-lng="<%# Eval("Longitude") %>">
                                            <img src='<%# ResolveUrl("~/Content/images/view.png") %>'
                                            width="14" height="14" />

                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                        <div class="modal fade" id="viewModal" tabindex="-1">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">

      <div class="modal-header">
        <h4 class="modal-title">Contact Report Details</h4>
        <button type="button" class="close" data-dismiss="modal">
            <span>&times;</span>
        </button>

      </div>

      <div class="modal-body">
        <table class="table table-bordered">
          <tr><th>Main Category</th><td id="m_main"></td></tr>
          <tr><th>Sub Category</th><td id="m_sub"></td></tr>
          <tr><th>Brand</th><td id="m_brand"></td></tr>
          <tr><th>Client / Agency</th><td id="m_party"></td></tr>
          <tr><th>Visit Date</th><td id="m_visit"></td></tr>
          <tr><th>Next Follow-up</th><td id="m_followup"></td></tr>
          <tr><th>Minutes</th><td id="m_minutes"></td></tr>
          <tr><th>Visiting Employee</th><td id="m_user"></td></tr>
        </table>

<button type="button" class="btn btn-primary" onclick="showLocation()">
    📍 Show Location
</button>




        <div id="map" style="width:100%;height:350px;margin-top:15px;"></div>
      </div>

    </div>
  </div>
</div>

                    </div>
                </div>

                </div>
                </div>
        <%--grid end--%>

    </asp:Content>