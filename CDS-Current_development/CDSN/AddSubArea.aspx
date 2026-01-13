<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AddSubArea.aspx.cs" Inherits="CDSN.AddSubArea" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

            <script type="text/javascript">
                function pageLoad() {
                     SetDDL();
                }
                $(document).ready(function () {
                     SetDDL();
                });
                function SetDDL() {
                    $('#MainContent_ddlDistrict').SumoSelect({

                        placeholder: 'Select District', includeSelectAllOption: true, okCancelInMulti: true, search: true, searchText: 'Select District', selectAll: true
                    });
                }
            </script>
        <%--<asp:UpdatePanel ID="RR" runat="server">--%>
            <ContentTemplate>
            <style type="text/css">
                .row {
                    margin-bottom: 5px !important;   
                }
            </style>
            <div class="col-md-10 col-md-offset-1">
                <div class="row" style="text-align:center">
                    <h3>Sub Area</h3>
                </div>
                <div class="row text-center">
                    <asp:Label ID="lblMsg" Text="" runat="server"></asp:Label>
                    <asp:Label ID="lblException" Text="" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Segment:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlSeqment" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvtxtUser" runat="server" ControlToValidate="ddlSeqment"
                        Display="None" ErrorMessage="Segment is Required." Font-Size="X-Small" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Territory:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlTerritory" OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged"
                            runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        Area:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlArea" AutoPostBack="true" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        District
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlDistrict" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                            runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        Sub Area:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtSubAreaName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="Sub area is required" Display="Dynamic"
                            ControlToValidate="txtSubAreaName"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        City:
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlCity" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"
                            runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        Subscribers:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtSubcribers" Text="0" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="row" style="display: none">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <asp:TextBox ID="txtlats" Text="0,0" runat="server"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-1 col-md-offset-2">
                            <asp:Button ID="btnSave" Style="min-width: 100%" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-info" />
                        </div>
                        <div class="col-md-1">
                            <asp:Button CausesValidation="false" ID="btnCancel" Style="min-width: 100%" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                CssClass="btn btn-danger" />
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <asp:HiddenField ID="SubAreaId" runat="server"></asp:HiddenField>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblGrid" runat="server"></asp:Label>
                            <asp:GridView ID="gvRecords" runat="server" DataKeyNames="SubAreaId"
                                AutoGenerateColumns="False" CssClass="EU_DataTable" Width="100%" OnPageIndexChanging="gvRecords_PageIndexChanging">
                                <HeaderStyle BackColor="#4D758B" ForeColor="white" HorizontalAlign="Center" Height="25px" />
                                <Columns>
                                    <asp:BoundField DataField="SubAreaId" HeaderText="S.Area Id">
                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubAreaName" HeaderText="Sub Area Name">
                                        <ItemStyle HorizontalAlign="Left" Width="80%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LatLng" HeaderText="Lat/Lng" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" Width="0" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Segment" HeaderText="Segment">
                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="CENTER" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" ImageUrl="~/Content/Images/News-Edit.ico" runat="server" 
                                                OnClick="btnEdit_Click" ValidationGroup="a"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                             <asp:ImageButton ID="btnDelete" ImageUrl="~/Content/Images/Trash-Cancel.ico" runat="server"
                                                 ValidationGroup="a" OnClick="btnDelete_Click"  CommandArgument='<%#Eval("SubAreaId")%>'/>
                                         </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
        </ContentTemplate>

  <%--  </asp:UpdatePanel>--%>
</asp:Content>
