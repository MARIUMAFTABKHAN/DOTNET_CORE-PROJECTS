<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClientManagement.aspx.cs" Inherits="ExpressDigital.ClientManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <style type="text/css">
                .form-control {
                    height: 30px;
                    padding-bottom: 1px;
                    padding-top: 1px;
                }
            </style>
            <%--     <script src="Scripts/FileInput.js"></script>
            <script src="Scripts/jquery-ui.js"></script>
            <link href="Scripts/jquery-ui.css" rel="stylesheet" /--%>>
            <script type="text/javascript">
                function pageLoad() {
                    //   $('input[type=file]').bootstrapFileInput();
                    applyDatePicker();

                }
                var data = new FormData();
              <%--  function previewFile() {
                    var file = document.querySelector('#<%=avatarUpload.ClientID %>').files[0];

                    var reader = new FileReader();
                    var data = new FormData();
                    data.append(file.name, file);
                    $.ajax({
                        url: "UploadRefeerence.ashx",
                        type: "POST",
                        data: data,
                        contentType: false,
                        processData: false,
                        success: function (result) {
                            //alert(result);
                        },
                        error: function (err) {
                            alert(err.statusText)
                        }
                    });
                }--%>
                function pageLoad() {
                    //    $('input[type=file]').bootstrapFileInput();

                }

                function RemoveRecord(val) {

                    bootbox.confirm("Are you sure to delete ?", function (result) {
                        var link = "ClientManagement.aspx";
                        if (result) {
                            $.ajax({
                                type: "POST",
                                url: "ClientManagement.aspx/OnSubmit",
                                data: JSON.stringify({ id: val }),
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    bootbox.alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown + "")

                                },
                                success: function (result) {
                                    if (result.d == "Ok") {
                                        //bootbox.alert("Record Deleted Successfully.....", function () {
                                        document.location.href = link;
                                        //});

                                    }
                                    else {
                                        bootbox.alert(result.d, function () {
                                        });

                                    }
                                }
                            });
                        }

                        // ---- WCF Service call backs -------------------




                    });
                }
            </script>
            <div class="form-horizontal">
                <div class="col-md-10 col-md-offset-1" style="margin-top: 50px">
                    <div class="form-group">
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <h4>&nbsp;&nbsp; Client Management 
                    </h4>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Group
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator1" Display="Dynamic" ControlToValidate="ddlGroup" OnServerValidate="ddlcampaign_server"
                                    runat="server" ForeColor="Red" ErrorMessage="*"></asp:CustomValidator>

                            </div>
                             <div class="col-md-2">
                                 <asp:CheckBox ID="chkLocalInternational" runat="server" AutoPostBack="false" Text="Local/Int." />
                             </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Agency
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlAgency" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged"></asp:DropDownList>

                                <asp:CustomValidator ID="CustomValidator7" Display="Dynamic" ControlToValidate="ddlAgency" OnServerValidate="ddlcampaign_server"
                                    runat="server" ForeColor="Red" ErrorMessage="*"></asp:CustomValidator>

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Client
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtClient" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtClient" ForeColor="Red"
                                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Contact Person
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Contact Number
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtContactnumber" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Email
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                G.S.Tax Province
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlGSTProvince" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGSTProvince_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                G.S.Tax #.
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtGST" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtGSTRate" Text="0.00" runat="server" CssClass="form-control"></asp:TextBox>

                                <%--<asp:TextBox ID="txtGSTRate" Text="0.00" MaxLength="5" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtGSTRate" Display="Dynamic" ForeColor="Red"
                                    ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" ForeColor="Red" ValidationExpression="^\d+\.\d{0,2}$" ControlToValidate="txtGSTRate" runat="server" ErrorMessage="*"></asp:RegularExpressionValidator>--%>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                NTN No.
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="TxtNTNNO" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                CNIC No.
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtCNIC" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Address
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtAddress" Rows="4" Height="60px" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtAddress" ForeColor="Red" Display="Dynamic"
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Country
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator2" Display="Dynamic" ControlToValidate="ddlCountry" OnServerValidate="ddlcampaign_server"
                                    runat="server" ForeColor="Red" ErrorMessage="*"></asp:CustomValidator>
                            </div>
                        </div>

                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                State
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                               <%-- <asp:CustomValidator ID="CustomValidator3" Display="Dynamic" ControlToValidate="ddlState" OnServerValidate="ddlcampaign_server"
                                    runat="server" ForeColor="Red" ErrorMessage="*"></asp:CustomValidator>--%>

                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                City
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator4" Display="Dynamic" ControlToValidate="ddlCity" OnServerValidate="ddlcampaign_server"
                                    runat="server" ForeColor="Red" ErrorMessage="*"></asp:CustomValidator>

                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Active
                            </div>
                            <div class="col-md-1">
                                <asp:CheckBox ID="ChkIsActive" runat="server" Checked="true" />
                            </div>
                            <div class="col-md-1" style="display: none">
                                Suspended
                            </div>
                            <div class="col-md-8">
                                <asp:CheckBox ID="IsSuspended" runat="server" Enabled="false" />
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                GST Exempted
                            </div>
                            <div class="col-md-1">
                                <asp:CheckBox ID="chkExempted" Checked="false" runat="server" />
                            </div>
                            <div class="col-md-1">
                                Is Govt
                            </div>
                            <div class="col-md-1">
                                <asp:CheckBox ID="IsGovt" runat="server" />
                            </div>
                             <div class="col-md-1">
                                Effective Station
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlGSTStation" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-3 col-md-offset-2">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />&nbsp;                    
                        <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3 col-md-offset-7">
                                <asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="Find Client" aria-label="Find Client" aria-describedby="basic-addon2"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnSearch" ValidationGroup="na" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    
                        <div class="col-md-10 col-md-offset-1" style="margin-top: 50px;">
                            <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                                AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
                                <PagerStyle  HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="Client1" HeaderText="Client">
                                        <HeaderStyle Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person">
                                        <HeaderStyle Width="20%" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="NTNNo" HeaderText="NTN No">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GSTNo" HeaderText="G.S.Tax No">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NICNo" HeaderText="NIC No.">
                                        <HeaderStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CityName" HeaderText="CityName">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>
                                    <%--   <asp:BoundField DataField="Suspended" HeaderText="Suspended">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="Active" HeaderText="Active">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>

                                    <asp:TemplateField Visible="false">
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
                                        <HeaderTemplate>
                                            Suspend/Restore
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <center>      
                                    <asp:HiddenField runat="server" ID="hdsuspend" value='<%#Eval("Suspended") %>'></asp:HiddenField>                                                                                                             
                                    <asp:ImageButton runat="server" ID="ImgSuspendButton" ImageUrl='~/Content/images/suspend.png' 
                                        ValidationGroup="a" OnClick="ImgSuspendButton_Click"  /></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
                                        <HeaderTemplate>
                                            Edit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <center>                                     
                                    <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/News-Edit.ico'
                                        ValidationGroup="a" OnClick="EditButton_Click"  /></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Delete
                                        </HeaderTemplate>
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
                                        <ItemTemplate>
                                            <center>
                                    <asp:ImageButton runat="server" ID="DelButton" ImageUrl='~/Content/images/Trash-Cancel.ico'
                                                                                                            
                                        ValidationGroup="a"  OnClick="DelButton_Click" OnClientClick='<%# "RemoveRecord(" +Eval("ID") + " );" %>'/></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
