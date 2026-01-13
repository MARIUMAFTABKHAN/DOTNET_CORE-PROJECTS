<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master"
    AutoEventWireup="true" CodeBehind="CurrentActivity.aspx.cs" Inherits="CDSN.CurrentActivity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .panel-info2 {
            background-color: rgb(143,99,20);
            color: #ffd800;
        }
        .hideshow{
            display:none;
        }

        .panel-body {
            padding: 0px !important;
        }

        .lbl {
            color: #fff !important;
            text-align: center !important;
        }

        .row {
            margin-bottom: 5px !important;
        }

        .modal-body {
            background-color: #fef8e9;
        }

        .modal-footer {
            background-color: #fef8e9;
        }

        .divcolor {
            color: #f7f3c7;
            background-color: #bed8eb;
            text-align: center;
            font-weight: bold;
            width: 100% !important;
            height: 35px !important;
            padding-top: 5px !important;
        }

        .ui-widget-header {
            background-color: rgb(143,99,20);
            color: #FFF;
            background: none;
        }

        .ui-datepicker .ui-datepicker-title select {
            background-color: rgb(143,99,20);
            color: #fff;
        }

        .ui-datepicker table {
            background-color: #bed8eb;
            color: #fff;
        }

        .ui-widget.ui-widget-content {
            background-color: #bed8eb;
            border: 1px solid rgb(143,99,20);
        }
    </style>
    <script type="text/javascript">

        $(function () {


        })
        function msendtask() {
            var OptrID = $('#<%= ddlmoperator.ClientID%>').val();
            <%--var UserId = $('#<%= ddlmtaskuser.ClientID%>').val();--%>
            var sendto = $('#<%= ddlmSentto.ClientID%>').val();
            var task = $('#<%= txtmtask.ClientID%>').val();
            var taskdate = $('#<%= txtmwef.ClientID%>').val();
            var IsAdminContact = $('#<%= hdisadmin.ClientID%>').val();// IsAdminContact;
            var TerritoryID = $('#<%= hdmtid.ClientID%>').val();


            var values = {};
            values.Messagetxt = task;
            /*values.CreatedBy = UserId;*/
            values.TerritoryID = TerritoryID;
            values.OperatorID = OptrID;
            values.IsResponded = false;
            values.IsAdminContact = IsAdminContact;
            values.isViewed = false;
            values.sendto = sendto;


            $.ajax({

                type: "POST",
                url: "ActivityTasks.asmx/SendContent",
                data: JSON.stringify(values),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    alert(r.d);
                },
                failure: function (r) {
                    alert(r.d);
                }
            });
        }

        function mhidetask() {
            document.getElementById('PleaseWait').style.display = 'None';
            $('#modeltask').modal('hide');
        }
        function showAddressByamit() {
            var UserID = $('#<%=ddlmtaskuser.ClientID%>').val();

            getoperato(UserID);//  alert(Country_ID);
        }

        function ShowTask(OptrID, Territory, TerritoryID) {

            //alert("cpr");
            showPleaseWait(true);
            $('#<%=ddlmoperator.ClientID%>').val(OptrID).change();

            //$("#mydropdownlist").val("thevalue").change();
           // var UserID = $('#<%=ddlmtaskuser.ClientID%>').val();
            CheckDetails(OptrID, Territory, TerritoryID);

        }

        function CheckDetails(OptrID, Territory, TerritoryID) {

            showPleaseWait(true);

            var values = {};
            values.RecordID = OptrID;// RecordID;
            $.ajax({

                type: "POST",
                url: "ActivityTasks.asmx/MessageCounter",
                data: JSON.stringify(values),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                   <%-- var val = $('#<%= hdisadmin.ClientID%>').val();
                    alert(val);
                    if (val == 'True') {
                        $("#btnmchatclose").show();

                    }
                    else {
                        $("#btnmchatclose").hide();

                    }--%>
                    if (data.d == 0) {

                        $('#<%= lblmterritory.ClientID%>').text(Territory);
                        $('#<%= hdmtid.ClientID%>').val(TerritoryID);
                        document.getElementById('PleaseWait').style.display = 'None';
                        $('#modeltask').modal('show');

                    }
                    else {

                        $("#<%= hdoptrvalue.ClientID%>").val(OptrID);
                        $("#<%=btngo.ClientID%>").click();
                    }
                },
                failure: function (r) {
                    alert(r.d);
                }
            });
        }
        function ShowPanel(val, ID, OperatoName, messageterritory, messagedate, sentby, territoryid, operatorid, sentto, CreatedBy) {

            showPleaseWait(true);

            $("#<%= txtMessage.ClientID %>").val(val);
            $("#<%= txtrecordid.ClientID %>").val(ID);

            $("#<%= lblHeadEndName.ClientID %>").text(OperatoName);
            $("#<%= lblmessageterritory.ClientID %>").text(messageterritory);
            $("#<%= lblmessagedate.ClientID %>").text(messagedate);

            $("#<%= ddlActivitySentFrom.ClientID %>").val(sentto );
            $("#<%= ddlActivitySentto.ClientID %>").val(CreatedBy);

            $("#<%= lblterritoryid.ClientID %>").text(territoryid);
            $("#<%= lbloperatorid.ClientID %>").text(operatorid);

           <%-- var val = $('#<%= hdisadmin.ClientID%>').val();
            if (val != 'True') {
                $("#btnchatclose").hide();

            }
            else {
                $("#btnchatclose").show();

            }--%>

            $('#modelactivity').modal('show');
        }

        function showPleaseWait() {
            document.getElementById('PleaseWait').style.display = 'block';

        }
        function showPleaseWait(val) {

            if (val == true)
                document.getElementById('PleaseWait').style.display = 'block';
            else
                document.getElementById('PleaseWait').style.display = 'None';
        }
        function getoperato(UserID) {
            var values = {};
            values.UserID = UserID;
            $.ajax({

                type: "POST",
                url: "ActivityTasks.asmx/GetOperator",
                data: JSON.stringify(values),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('#<%=ddlmoperator.ClientID%>').empty();
                    var jsdata = JSON.parse(data.d);
                    $.each(jsdata, function (key, value) {
                        $('#<%=ddlmoperator.ClientID%>').append($("<option></option>").val(value.Id).html(value.Name));
                    });
                },
                failure: function (data) {
                    alert(data.d);
                }
            });
        }
        function pageLoad() {
            var $j = jQuery.noConflict();



            $('#<%= txtwef.ClientID  %>').datepicker({
                showOn: 'button',
                buttonImage: "Content/images/calander.png",
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                maxDate: '+0m',
                changeMonth: true,
                changeYear: true,
                buttonImageOnly: true

            }); $('#<%= txtwet.ClientID  %>').datepicker({
                showOn: 'button',
                buttonImage: "Content/images/calander.png",
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                maxDate: '+0m',
                changeMonth: true,
                changeYear: true,
                buttonImageOnly: true

            });

            $('#<%= txtmwef.ClientID  %>').datepicker({
                showOn: 'button',
                buttonImage: "Content/images/calander.png",
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                maxDate: '+0m',
                changeMonth: true,
                changeYear: true

            });

        }


        //  Activity Details

        function hideactivity() {

            document.getElementById('PleaseWait').style.display = 'None';
            var RecordID = $('#<%= txtrecordid.ClientID%>').val();
            var values = {};
            values.RecordID = RecordID;
            $.ajax({

                type: "POST",
                url: "GetCPR.asmx/ViewedMessage",
                data: JSON.stringify(values),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    showPleaseWait(false);
                    $("#<%= txtReply.ClientID %>").val("");
                    //alert(r.d);

                },
                failure: function (r) {
                    alert(r.d);
                }
            });

            $('#modelactivity').modal('hide');


        }
        function sendmessagereply() {
            var IsAdmin = $('#<%= hdisadmin.ClientID%>').val();
           var TerritoryId = $('#<%= lblterritoryid.ClientID%>').text();
           var Messagetxt = $("#<%= txtReply.ClientID %>").val();
           var OperatorId = $('#<%= lbloperatorid.ClientID%>').text();

    // ✅ Always get numeric IDs from dropdowns (because DataValueField="UserID")
    var CreatedBy = $('#<%= ddlActivitySentFrom.ClientID%>').val();
    var Sentto = $('#<%= ddlActivitySentto.ClientID%>').val();

    if (!CreatedBy || CreatedBy === "0") {
        alert("Please select a valid 'Sent From' user.");
        return;
    }
    if (!Sentto || Sentto === "0") {
        alert("Please select a valid 'Reply To' user.");
        return;
    }

    var values = {
        Messagetxt: Messagetxt,
        CreatedBy: CreatedBy,
        TerritoryID: TerritoryId,
        OperatorID: OperatorId,
        IsResponded: false,
        IsAdminContact: IsAdmin,
        isViewed: false,
        Sentto: Sentto
    };

    $.ajax({
        type: "POST",
        url: "GetCPR.asmx/SendContent",
        data: JSON.stringify(values),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            showPleaseWait(false);
            $("#<%= txtReply.ClientID %>").val("");
            alert(r.d);
        },
        error: function (xhr, status, error) {
            showPleaseWait(false);
            alert("Error sending reply: " + error);
            console.log(xhr.responseText);
        }
    });
        }


        function closeChats() {
            var RecordID = $('#<%= txtrecordid.ClientID%>').val();
            var values = {};
            values.RecordID = RecordID;
            values.Messagetxt = $('#<%= txtReply.ClientID%>').val()

            $.ajax({

                type: "POST",
                url: "GetCPR.asmx/CloseMessage",
                data: JSON.stringify(values),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {

                    showPleaseWait(false);
                    $("#<%= txtReply.ClientID %>").val("");
                    alert(r.d);

                },
                failure: function (r) {
                    alert(r.d);
                }
            });
        }
    </script>


    <asp:UpdatePanel ID="coninfo" runat="server">
        <ContentTemplate>
            <div class="panel-group">
                <div class="panel panel-success">
                    <div class="panel-heading" style="height: 40px !important;">
                        <div><b>Parameters</b></div>

                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="Panel2" runat="server" CssClass="CollapsePanelBody" Width="100%">
                            <div class="col-md-12" style="padding: 0px !important">
                                <div class="col-md-5" style="border: 1px solid #ffd800; height: 50px; padding-top: 5px !important">
                                    <div class="col-md-12" style="height: 45px">
                                        <div class="col-md-2">
                                            From
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtwef" runat="server" CssClass="form-control"
                                                Style="display: inline; width: 75%; min-width: 75%"></asp:TextBox>
                                        </div>

                                        <div class="col-md-1" style="margin-bottom: 5px">
                                            To
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtwet" runat="server" CssClass="form-control"
                                                Style="display: inline; width: 75%; min-width: 75%" OnTextChanged="txtwet_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnactivity" ValidationGroup="c" runat="server" CssClass="btn btn-info"
                                                Text="View" OnClientClick="showPleaseWait()" OnClick="btnactivity_Click" />
                                            <div style="display: none">
                                                <asp:Button ID="btngo" CausesValidation="false" runat="server" Text="go" OnClick="btngo_Click" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-7" style="border: 1px solid #ffd800; height: 50px">
                                    <div id="chkall" style="width: 90%; margin-top: 0px; padding-top: 8px; height: 40px"
                                        align="center">
                                        <div style="width: 67px; display: block; float: left; padding-top: 3px; margin-left: 0px">
                                            <asp:CheckBox runat="server" ID="checkAll" AutoPostBack="true" OnCheckedChanged="checkAll_CheckedChanged" Text="All" Width="80px" />
                                        </div>
                                        <div style="width: 90%; display: block; float: left; margin-top: 0px; padding-top: 4px; height: 40px;">
                                            <asp:CheckBoxList CssClass="dd" ID="chklist" OnSelectedIndexChanged="chklist_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="100%">
                                            </asp:CheckBoxList>
                                        </div>
                                        <div style="width: 60px; display: block; float: left; display: none">
                                            <asp:Button ID="BtnCheckbox" runat="server" Text="" CssClass="Button" Width="0px"
                                                OnClick="BtnCheckbox_Click" />


                                        </div>
                                        <div id='PleaseWait' style='display: none; text-align: center; width: 100px;'>
                                            <img src='Content/Images/ajax-loader.gif' />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="pnlactivity" runat="server">
                        <div class="panel panel-success">
                            <div class="panel-heading" style="height: 40px !important">
                                <div style="width: 70%; float: left">
                                    <div><b>Activity Details</b></div>
                                    <asp:Label ID="lblActivityDetailHeader" runat="server" CssClass="lbl" />
                                    <asp:Label ID="lblactivityid" runat="server"></asp:Label>
                                </div>

                            </div>

                            <div class="panel-body">
                                <asp:Panel ID="pnlActivityDetails" runat="server" CssClass="CollapsePanelBody" Width="100%">
                                    <div style="width: 100%; margin-top: 4px; border: 2px solid #b1b1b1;" align="center">
                                        <asp:GridView ID="gvActivityDetails" DataKeyNames="ID,TerritoryID,IsAdminContact,Sentto" runat="server"
                                            OnRowDataBound="gvActivityDetails_RowDataBound" AutoGenerateColumns="False" CssClass="EU_DataTable"
                                            PageSize="80" Width="100%">
                                            <HeaderStyle BackColor="#4D758B" ForeColor="white" Height="25px" HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="MessaUserNamegeTime">
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TerritoryID" HeaderText="Territory ID">
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SentoUser" HeaderText="Send To">
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UserName" HeaderText="Send By">
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MessageDate" HeaderText="Date">
                                                    <ItemStyle HorizontalAlign="Left" Width="80" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MessageTime" HeaderText="Time">
                                                    <ItemStyle HorizontalAlign="Left" Width="50" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TerritoryName" HeaderText="Territory">
                                                    <ItemStyle HorizontalAlign="Left" Width="70" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="OperatorName" HeaderText="Operator">
                                                    <ItemStyle HorizontalAlign="Left" Width="250" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Messagetxt" HeaderText="Message">
                                                    <ItemStyle HorizontalAlign="Left" Width="400" />
                                                </asp:BoundField>

                                                <asp:TemplateField HeaderText="Activity" >
                                                    <ItemTemplate>
                                                        
                                                        <asp:HiddenField id="hdSentto" runat="server" Value='<%# Bind("Sentto") %>' />
                                                        <asp:HiddenField id="hdadmin" runat="server" Value='<%# Bind("IsAdminContact") %>' />
                                                        <asp:HiddenField ID="hdActivitystatus" runat="server" />
                                                        <asp:HiddenField ID="hdOperatorID" runat="server" Value='<%# Bind("OperatorID") %>' />
                                                        <asp:HiddenField ID="hdTerritoryID" runat="server" Value='<%# Bind("TerritoryID") %>' />
                                                        <asp:Image ID="imgbtn" runat="server" onclick='<%# string.Format("return ShowPanel(\"{0}\", {1}, \"{2}\", \"{3}\", \"{4}\",\"{5}\", {6} , {7},{8},{9})", Eval("Messagetxt"), Eval("ID"), Eval("OperatorName"), Eval("MessageDate"), Eval("TerritoryName"),  Eval("UserName"), Eval("TerritoryID"),Eval("OperatorID"), Eval("Sentto"),Eval("CreatedBy")) %>' src="Content/Images/email.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading" style="height: 40px !important;">
                            <div><b>Current Activity</b></div>
                            <div style="width: 100%; float: left">
                                <asp:HiddenField ID="hduserid" runat="server" />
                                <asp:HiddenField ID="hdisadmin" runat="server" />
                                <asp:HiddenField ID="hdoptrvalue" runat="server" />
                            </div>
                        </div>


                        <div class="panel-body">
                            <asp:Panel ID="Panel1" runat="server" CssClass="CollapsePanelBody" Width="100%">

                                <div class="col-md-12" style="padding: 0px !important">

                                    <asp:HiddenField ID="hdactstatus" runat="server" />

                                    <asp:GridView ID="gvContact" CssClass="EU_DataTable" runat="server" AllowPaging="True" AllowSorting="True" DataKeyNames="OperatorID"
                                        AutoGenerateColumns="False" PageSize="80"
                                        Width="100%"
                                        OnPageIndexChanging="gvContact_PageIndexChanging" OnRowDataBound="gvContact_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="TerritoryName" HeaderText="Territory">
                                                <ItemStyle HorizontalAlign="Left" Width="120" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CityName" HeaderText="City">
                                                <ItemStyle HorizontalAlign="Left" Width="120" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="OperatorName" HeaderText="Operator">
                                                <ItemStyle HorizontalAlign="Left" Width="300" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="Address" HeaderText="Address">
                                                <ItemStyle HorizontalAlign="Left" Width="300" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Total" Visible="false">
                                                <HeaderStyle Width="40" />
                                                <ItemStyle Width="40" HorizontalAlign="Center" BackColor="Violet" ForeColor="White" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotal" runat="server" CssClass="lbl">
                                                    </asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View" Visible="false">
                                                <HeaderStyle Width="40" />
                                                <ItemStyle Width="40" HorizontalAlign="Center" BackColor="YellowGreen" ForeColor="Red" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblv" runat="server" CssClass="lbl">
                                                    </asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NV" Visible="false">
                                                <HeaderStyle Width="40" />
                                                <ItemStyle Width="40" HorizontalAlign="Center" BackColor="Red" ForeColor="White" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnv" runat="server" CssClass="lbl">
                                                    </asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RV" Visible="false">
                                                <HeaderStyle Width="40" />
                                                <ItemStyle Width="40" HorizontalAlign="Center" BackColor="Blue" ForeColor="White" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrv" runat="server" CssClass="lbl">
                                                    </asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Activity">
                                                <ItemTemplate>
                                                    <img id="imgtask" src="Content/Images/News-Edit.ico" alt="Add Task" onclick='<%# string.Format("return ShowTask({0},\"{1}\",{2})", Eval("OperatorID"),Eval("TerritoryName"),Eval("TerritoryID"))%>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>


                            </asp:Panel>
                        </div>
                    </div>


                </div>

                <!-- Button trigger modal -->


                <!-- Modal -->
                <div class="modal fade" id="modelactivity" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header" style="margin-bottom: 5px !important; padding-left: 20px !important">
                                <h5 class="modal-title" id="lblactivity">
                                    <div class="row">
                                        Operator:  
                                                <asp:Label ID="lblHeadEndName" Font-Bold="True" runat="server" />
                                        <button id="btnchatclose" style="display: none !important" type="button" onclick="closeChats()" class="btn btn-sm danger hideme">Chat Close</button>

                                    </div>
                                    <div class="row">
                                        Date:  
                                    <asp:Label ID="lblmessagedate" runat="server" Font-Bold="True" />
                                    </div>
                                    <div class="row">
                                        Territory:   
                                    <asp:Label ID="lblmessageterritory" runat="server" Font-Bold="True" />
                                    </div>
                                    <h5></h5>
                                    <div style="display: none">
                                        <asp:Label ID="lblresult" runat="server" />
                                    </div>
                            </div>
                            <div class="modal-body">
                                <div class="row" style="width: 100%; min-width: 100%; margin-bottom: 5px">
                                    <div style="width: 20%; min-width: 20%; float: left">
                                        <span style="text-align: right; padding-right: 20px; padding-left: 20px;">Sent From</span>
                                    </div>
                                    <div style="width: 80%; min-width: 80%; float: left">
                                         <asp:DropDownList ID="ddlActivitySentFrom" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                       <%-- <asp:Label ID="lblsentby" runat="server"
                                            CssClass="form-control"></asp:Label>--%>
                                    </div>
                                </div>
                                <div class="row" style="width: 100%; min-width: 100%; margin-bottom: 5px">
                                    <div style="width: 20%; min-width: 20%; float: left">
                                        <span style="text-align: right; padding-right: 20px; padding-left: 20px;">Reply To </span>
                                    </div>
                                    <div style="width: 80%; min-width: 80%; float: left">
                                        <asp:DropDownList ID="ddlActivitySentto" runat="server" CssClass="form-control">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="row" style="width: 100%; min-width: 100%; margin-bottom: 5px">
                                    <div style="width: 20%; min-width: 20%; float: left">
                                        <div style="display: none">
                                            <asp:TextBox ID="txtsentto" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtrecordid" Text='<%#Eval("ID") %>' ReadOnly="true" runat="server">
                                           
                                            </asp:TextBox>
                                            <asp:Label ID="lblterritoryid" Text='<%#Eval("TerritoryID") %>' ReadOnly="true" runat="server"></asp:Label>
                                            <asp:Label ID="lbloperatorid" Text='<%#Eval("OperatorID") %>' ReadOnly="true" runat="server">
                                      
                                            </asp:Label>


                                        </div>
                                        <span style="text-align: right; padding-right: 20px; padding-left: 20px;">Message </span>
                                    </div>
                                    <div style="width: 80%; min-width: 80%; float: left">
                                        <asp:TextBox ID="txtMessage" Text='<%#Eval("txtMessage") %>' CssClass="form-control" ReadOnly="true" runat="server" Rows="4" TextMode="MultiLine"
                                            Style="min-width: 100%; max-width: 100%"
                                            MaxLength="2000"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row" style="width: 100%; min-width: 100%">
                                    <div style="width: 20%; min-width: 20%; float: left">
                                        <span style="text-align: right; padding-right: 20px; padding-left: 20px;">Reply: </span>
                                    </div>
                                    <div style="width: 80%; min-width: 80%; float: left">
                                        <asp:TextBox ID="txtReply" runat="server" Rows="5" TextMode="MultiLine" Width="650px"
                                            MaxLength="2000" Style="min-width: 100%; max-width: 100%" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row" style="width: 100%; min-width: 100%; display: none">
                                    <div style="width: 80%; min-width: 80%; margin-left: 20%">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Send" CssClass="btn btn-info"
                                            OnClick="btnUpdate_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button id="myBtn" type="button" onclick="hideactivity()" class="btn btn-success">Close</button>
                                <button type="button" onclick="sendmessagereply()" class="btn btn-info">Save changes</button>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="modeltask" tabindex="-1" role="dialog" aria-labelledby="lbltask" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">

                                <h5 class="modal-title" id="lltask">
                                    <asp:Label ID="lblmterritory" Text="Create Task" Font-Bold="True" runat="server" />
                                </h5>
                                <div style="display: none">
                                    <asp:HiddenField ID="hdmactivityby" runat="server" />
                                    <asp:HiddenField ID="hdmtid" runat="server" />
                                    <asp:HiddenField ID="hdmopid" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-body" style="height: 260px">

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblmmessage" ForeColor="Red" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        Date :
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtmwef" CssClass="form-control" Style="width: 120px !important; min-width: 75%; display: inline !important;" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                 
                                   <div class="row">
                                    <div class="col-md-2">
                                        Send From:
                                    </div>                                    
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlmtaskuser" runat="server" CssClass="form-control">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        Send To :
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlmSentto" runat="server" Style="min-width: 100%;" CssClass="form-control mytask">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2">
                                        Head End :
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlmoperator" Style="min-width: 100%" CssClass="form-control" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlmOperator_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        Task :
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtmtask" TextMode="MultiLine" Rows="4" Style="height: 80px; min-width: 100%; width: 100%"
                                            MaxLength="1000" runat="server" ValidationGroup="dd"></asp:TextBox>
                                        <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Message required" ControlToValidate="txtmTask" Display="Dynamic"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                            </div>

                        </div>
                        <div class="modal-footer">
                            <button id="btntask" type="button" onclick="mhidetask()" class="btn btn-danger">Close</button>
                            <button id="btnmchatclose" type="button" style="display: none !important" class="btn btn-success hideme">Chat Cancel</button>
                            <button type="button" id="btnsaveactivty" onclick="msendtask();" class="btn btn-info">Send Task </button>

                        </div>
                    </div>
                </div>
        </ContentTemplate>




    </asp:UpdatePanel>
</asp:Content>
