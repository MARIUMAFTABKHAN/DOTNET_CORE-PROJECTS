<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AddRegion.aspx.cs" Inherits="CDSN.AddRegion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    function setBodyContentHeight() {
        //Setting height of body to maintain position of drag panel
        document.body.style.height = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight) + "px";
    }
</script>
    <style type="text/css">
        .row {
            margin-bottom: 5px !important;
        }
    </style>
    <%--<asp:UpdatePanel ID="up" runat="server">--%>
        <ContentTemplate>
            <div class="col-md-10 col-md-offset-1">
                <div class="row" style="text-align:center">
                    <h3>Region</h3>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                        <asp:Label ID="lblException" runat="server" Visible="False"></asp:Label>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-md-2">
                        Country:
                    </div>
                    <div class="col-md-4">
                      <asp:DropDownList ID="ddlcountry" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Region Name:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtregion" runat="server" CssClass="form-control">
                        </asp:TextBox>

                        <asp:RequiredFieldValidator ID="rfvtxtUser" runat="server" ControlToValidate="txtregion"
                            Display="Dynamic" ErrorMessage="Region Name is Required." Font-Size="X-Small" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        Active:
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chkActive" runat="server" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Style="min-width: 100%" CssClass="btn btn-info" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger" Style="min-width: 100%" />
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-4 col-md-offset-2">
                        <asp:Label ID="lblGrid" runat="server" CssClass="lbl"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvRecords" runat="server" AllowSorting="true" DataKeyNames="RegionId,CountryId"
                            AutoGenerateColumns="False" CssClass="EU_DataTable" Width="100%" OnPageIndexChanging="gvRecords_PageIndexChanging">
                            <HeaderStyle BackColor="#4D758B" ForeColor="white" HorizontalAlign="Center" Height="25px" />
                            <Columns>
                                <%--<asp:BoundField DataField="RegionId" HeaderText="Region Id" SortExpression="CountryId">
                                    <ItemStyle HorizontalAlign="Left" Width="70" />
                                </asp:BoundField>
                               --%>
                                <asp:BoundField DataField="RegionName" HeaderText="Region Name" SortExpression="CountryName">
                                    <ItemStyle HorizontalAlign="Left" Width="81%" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="CountryName" HeaderText="Country Name" SortExpression="CountryName">
                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Active" SortExpression="CountryName">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="CENTER" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" ImageUrl="~/Content/Images/News-Edit.ico" runat="server" OnClick="btnEdit_Click" ValidationGroup="a"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="CENTER" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" ImageUrl="~/Content/Images/Trash-Cancel.ico" runat="server"
                                             ValidationGroup="a" OnClick="btnDelete_Click"  CommandArgument='<%#Eval("RegionId")%>'/>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>

        </ContentTemplate>
    <%--</asp:UpdatePanel>--%>


</asp:Content>
