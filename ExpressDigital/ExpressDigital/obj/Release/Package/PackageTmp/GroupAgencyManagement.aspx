<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GroupAgencyManagement.aspx.cs" Inherits="ExpressDigital.GroupAgencyManagement" %>

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
            <script type="text/javascript">

                function RemoveRecord(val) {

                    bootbox.confirm("Are you sure to delete ?", function (result) {
                        var link = "GroupAgencyManagement.aspx";
                        if (result) {
                            $.ajax({
                                type: "POST",
                                url: "GroupAgencyManagement.aspx/OnSubmit",
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
                <div class="col-md-10 col-md-offset-1">
                    <div class="form-group">
                        <div class="row text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <h4>Group Agency 
                    </h4>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                Agency (Parent)
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlAgencyClient" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlAgencyClient_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="A">Agency</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                Agency Name
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtName" ForeColor="Red" Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-1">
                                Owner
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtOwner" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                Email
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                G.S.Tax #.
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="txtGST" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                N.T.N #.
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="txtntn" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                Address
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="2" Height="50" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtAddress" ForeColor="Red" Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                City
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                Currency 
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlCurrencymode" runat="server" CssClass="form-control" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                Phone
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                Cell
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtCell" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                Fax
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtFax" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" style="display: none">
                        <div class="row">
                            <div class="col-md-2">
                                Commission
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlComission" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlComission_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                Rate
                            </div>
                            <div class="col-md-2" style="margin-left: -2px !important">
                                <asp:TextBox ID="txtCommission" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                Credit Limit
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtCrLimit" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtCrLimit" ForeColor="Red" runat="server" ErrorMessage="*" ValidationExpression="\d+" Display="Dynamic"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                Status
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlLocalInternational" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True" Value="L">Local</asp:ListItem>
                                    <asp:ListItem Value="I">International</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1" style="display: none">
                                Suspended
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox ID="chkSuspended" Enabled="false" Text="" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                Remarks
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="2" Height="50" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                Active
                            </div>
                            <div class="col-md-1">
                                <asp:CheckBox ID="ChkIsActive" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-3 col-md-offset-2">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-3 col-md-offset-8">
                                <asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="Find Client" aria-label="Find Agency" aria-describedby="basic-addon2"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnSearch" ValidationGroup="na" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                                AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="GroupName" HeaderText="Group Name">
                                        <HeaderStyle Width="30%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OwenrName" HeaderText="Owner">
                                        <HeaderStyle Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GSTNumber" HeaderText="GST">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone">
                                        <HeaderStyle Width="11%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CellNumber" HeaderText="CellNumber">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CityName" HeaderText="City">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Active" HeaderText="Active"></asp:BoundField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
                                        <HeaderTemplate>
                                            Suspend
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <center>
                                                <asp:ImageButton runat="server" ID="ImgSuspendButton" ImageUrl='~/Content/images/suspend.png' ValidationGroup="a" OnClick="ImgSuspendButton_Click" />
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
                                        <HeaderTemplate>
                                            Agency
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <center>
                                                <asp:ImageButton runat="server" ID="ChildAgencyButton" ImageUrl='~/Content/images/ChildAgency.png' tooltip="Add Agency" ValidationGroup="a" OnClick="ChildAgencyButton_Click" />
                                            </center>
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
                                                <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/News-Edit.ico' tooltip="Edit Record" ValidationGroup="a" OnClick="EditButton_Click" />
                                            </center>
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
                                                <asp:HiddenField runat="server" ID="hdsuspend" value='<%#Eval("Suspended") %>'></asp:HiddenField>
                                                <asp:ImageButton runat="server" ID="DelButton" ImageUrl='~/Content/images/Trash-Cancel.ico' tooltop="Delete Record" ValidationGroup="a" OnClick="DelButton_Click" OnClientClick='<%# "RemoveRecord(" +Eval("ID") + " );" %>' />
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
