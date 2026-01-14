<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="StationManagement.aspx.cs" Inherits="ExpressDigital.StationManagement " %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <script type="text/javascript">

                function RemoveRecord(val) {
                    bootbox.confirm("Are you sure to delete ?", function (result) {
                        var link = "StationManagement.aspx";
                        if (result) {

                            $.ajax({
                                type: "POST",
                                url: "StationManagement.aspx/OnSubmit",
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
                        <div class="div-md-12 text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <h4>City Management
                    </h4>
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                Region
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                State
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlStates" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlStates_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                City
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtStation" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtStation" ForeColor="Red" Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-2">
                                Active
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="ChkIsActive" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-3 col-md-offset-2">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                        <div class="div-md-12" style="margin-top: 50px;">
                            <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                                AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="CityName" HeaderText="Station">
                                        <HeaderStyle Width="90%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IsActive" HeaderText="Active"></asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
                                        <HeaderTemplate>
                                            Edit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <center>
                                                <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/News-Edit.ico' ValidationGroup="a" OnClick="EditButton_Click" />
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
                                                <asp:ImageButton runat="server" ID="DelButton" ImageUrl='~/Content/images/Trash-Cancel.ico' ValidationGroup="a" OnClick="DelButton_Click" OnClientClick='<%# "RemoveRecord(" +Eval("ID") + " );" %>' />
                                            </center>
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
