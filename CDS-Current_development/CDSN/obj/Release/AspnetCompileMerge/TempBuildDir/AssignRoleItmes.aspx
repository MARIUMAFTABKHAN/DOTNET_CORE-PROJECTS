<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AssignRoleItmes.aspx.cs" Inherits="CDSN.AssignRoleItmes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <asp:UpdatePanel ID="up" runat="server">

        <ContentTemplate>
            <script type="text/javascript">

                var list = [];

                var gridViewId = '#<%= gv.ClientID %>';
                function checkAll(selectAllCheckbox) {

                    $('td :checkbox', gridViewId).prop("checked", selectAllCheckbox.checked);

                    Totalsize = $('.chk').length;
                }

                var Totalsize
                function unCheckSelectAll(selectCheckbox) {

                    Totalsize = $('.chk').length;
                    list = [];
                    if (selectCheckbox.checked) {
                        list = [];
                        $('#MainContent_gv input:checked').each(function () {
                            list.push(this.name);
                        });
                        Totalchk = list.length;
                    }
                    else {
                        list = [];
                        $('#MainContent_gv input:checked').each(function () {
                            list.push(this.name);

                        });
                        Totalchk = list.length - 1;
                    }

                    if (Totalsize == Totalchk) {

                        $('th :checkbox', gridViewId).prop("checked", true);
                    }
                    else {
                        $('th :checkbox', gridViewId).prop("checked", false);
                    }
                }

            </script>
            <div class="form-horizontal">
                <div class="col-md-10 col-md-offset-1">
                    <div class="form-group">
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <h4>Form Assignment
                    </h4>

                    <div class="form-group">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Role
                            </div>
                            <div class="col-md-4">

                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12" style="margin-top: 50px;">
                            <asp:GridView ID="gv" runat="server" CssClass="EU_DataTable" DataKeyNames="ItemID"
                                AutoGenerateColumns="false">
                                <PagerStyle CssClass="" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="ItemName" HeaderText="Menu Name">
                                        <ItemStyle Width="90%"></ItemStyle>

                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Active?">

                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" CssClass="myCheckbox" onclick="checkAll(this);" runat="server" />
                                        </HeaderTemplate>
                                        <HeaderStyle BackColor="#287db4" />
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>

                                            <asp:CheckBox ID="chkRow" CssClass="chk" onclick="unCheckSelectAll(this);" Checked='<%# Eval("IsActive") %>' runat="server" />

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
