<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AgencyManagement.aspx.cs" Inherits="ExpressDigital.AgencyManagement" %>

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

                #ContentPlaceHolder1_chkagency label{
                    margin-left:5px !important;
                }
            </style>
            <script src="Scripts/FileInput.js"></script>
            <script src="Scripts/jquery-ui.js"></script>
            <link href="Scripts/jquery-ui.css" rel="stylesheet" />
            <script type="text/javascript">

                function RemoveRecord(val) {

                    bootbox.confirm("Are you sure to delete ?", function (result) {
                        var link = "AgencyManagement.aspx";
                        if (result) {

                            $.ajax({
                                type: "POST",
                                url: "AgencyManagement.aspx/OnSubmit",
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
                function pageLoad() {
                    //  $('input[type=file]').bootstrapFileInput();
                    applyDatePicker();

                }


                function applyDatePicker() {

                    $("#ContentPlaceHolder1_txtCommissionDate").datepicker({
                        showOn: 'button',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        buttonImage: 'Content/Images/Calender.png',
                        dateFormat: 'dd/mm/yy'
                    });
                }
            </script>
            <div class="form-horizontal">
                <div class="col-md-10 col-md-offset-1">

                    <div class="form-group">
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <h4>Agency 
                        </h4>
                    </div>


                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    Group Name
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlAgency" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlAgency_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-1" style="color: red; display: none">
                                    <asp:Label ID="lblClientAgency" runat="server"></asp:Label>
                                </div>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    Agency
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtAgency" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtAgency" ForeColor="Red" Display="Dynamic"
                                        ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
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
                                    Designation
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    Commission Date
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCommissionDate" Style="width: 50%; display: inline" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>

                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    Phone
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>

                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    Cell
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCell" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-12">

                                <div class="col-md-2">
                                    Fax
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtFax" runat="server" CssClass="form-control"></asp:TextBox>

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
                                    Address
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" Height="65"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtAddress" ForeColor="Red" Display="Dynamic"
                                        ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    City
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    Active
                                </div>
                                <div class="col-md-3">
                                    <asp:CheckBox ID="chkActive" runat="server" OnCheckedChanged="chkActive_CheckedChanged"></asp:CheckBox>
                                </div>
                            </div>

                        </div>
                        <div class="form-group" style="display: none">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    Suspended
                                </div>
                                <div class="col-md-1">
                                    <asp:CheckBox ID="RdoSuspend" runat="server" Text="" AutoPostBack="false" Enabled="false" />

                                </div>

                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    Remarks
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" Height="65"></asp:TextBox>

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
                                    <asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="Find Client" aria-label="Find Agency" aria-describedby="basic-addon2"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:Button ID="btnSearch" ValidationGroup="na" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 50px;">
                                <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID,GroupID" AutoGenerateColumns="false"
                                    AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
                                    <PagerStyle  HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <Columns>


                                        <asp:BoundField DataField="AgencyName" HeaderText="Agency">
                                            <HeaderStyle Width="30%" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="GroupName" HeaderText="Parent Agency">
                                            <HeaderStyle Width="30%" />
                                        </asp:BoundField>                                         
                                        <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person">
                                            <HeaderStyle Width="25%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CellNumber" HeaderText="CellNumber">
                                            <HeaderStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CityName" HeaderText="City">
                                            <HeaderStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AgencyClinet" HeaderText="Staus"></asp:BoundField>
                                        <%-- <asp:BoundField DataField="Suspended" HeaderText="Suspended"></asp:BoundField>--%>
                                        <asp:BoundField DataField="Active" HeaderText="Active"></asp:BoundField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderStyle Width="40px" />
                                            <ItemStyle Width="40px" />
                                            <HeaderTemplate>
                                                Suspend/Restore
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <center>      
                                    <asp:HiddenField  runat="server" ID="hdsuspend" value='<%#Eval("Suspended") %>'></asp:HiddenField>                                                                                                             
                                    <asp:ImageButton  runat="server" ID="ImgSuspendButton" ImageUrl='~/Content/images/suspend.png' 
                                        ValidationGroup="a" OnClick="ImgSuspendButton_Click"  /></center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="true">
                                            <HeaderStyle Width="40px" />
                                            <ItemStyle Width="40px" />
                                            <HeaderTemplate>
                                                Client
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <center>                                     
                                    <asp:ImageButton runat="server" ID="ChildAgencyButton" ImageUrl='~/Content/images/ChildAgency.png' tooltip="Add Client"
                                        ValidationGroup="a" OnClick="ChildAgencyButton_Click"  /></center>
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
                                    <asp:ImageButton runat="server" ID="DelButton" ImageUrl='~/Content/images/Trash-Cancel.ico'  tooltop="Delete Record"                                                                                                           
                                        ValidationGroup="a"   OnClientClick='<%# "RemoveRecord(" +Eval("ID") + " );" %>'/></center>
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
