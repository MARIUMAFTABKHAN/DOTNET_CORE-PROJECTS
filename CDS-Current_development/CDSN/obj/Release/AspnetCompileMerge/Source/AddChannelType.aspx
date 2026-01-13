<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AddChannelType.aspx.cs" Inherits="CDSN.AddChannelType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>--%>
        <script type="text/javascript">
            function setBodyContentHeight() {
                //Setting height of body to maintain position of drag panel
                document.body.style.height = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight) + "px";
            }
        </script>
        <%--<script type="text/javascript">

            function RemoveRecord(val) {

                bootbox.confirm("Are you sure to delete ?", function (result) {
                    var link = "AddChannelType.aspx";
                    if (result) {
                        $.ajax({
                            type: "POST",
                            url: "AddChannelType.aspx/OnSubmit",
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
                });
            }
        </script>--%>
        <style type="text/css">
            .row {
                margin-bottom: 5px !important;
            }
        </style>
    
        
            <div class="col-md-10 col-md-offset-1">
                <div class="row" style="text-align: center">
                    
                        <h3>Channel Type Parameters</h3>
                            <%--<asp:HiddenField ID="AreaId" runat="server"></asp:HiddenField>--%>
                        
                    </div>
                
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblException" runat="server" Visible="False"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Type Name:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control">
                        </asp:TextBox>

                        <asp:RequiredFieldValidator ID="rfvtxtUser" runat="server" ControlToValidate="txtCountry"
                            Display="Dynamic" ErrorMessage="Country Name is Required." Font-Size="X-Small" />
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
                        <asp:GridView ID="gvRecords" runat="server" AllowPaging="false" AllowSorting="false" DataKeyNames="ID"
                            AutoGenerateColumns="False" CssClass="EU_DataTable" Width="100%" OnPageIndexChanging="gvRecords_PageIndexChanging">
                            <HeaderStyle BackColor="#4D758B" ForeColor="white" HorizontalAlign="Center" Height="25px" />
                            <Columns>
                                <%--<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
                                    <ItemStyle HorizontalAlign="Left" Width="70" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="ChannelType" HeaderText="Type" SortExpression="CountryName">
                                    <ItemStyle HorizontalAlign="Left" Width="90%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="CountryName">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="CENTER" />
                                    <HeaderTemplate>Edit</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" ImageUrl="~/Content/Images/News-Edit.ico" runat="server" ValidationGroup="a" OnClick="btnEdit_Click"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>Delete</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" ImageUrl="~/Content/Images/Trash-Cancel.ico" runat="server" ValidationGroup="a" 
                                            OnClick="btnDelete_Click" CommandName="Delete" CommandArgument='<%#Eval("ID")%>'/>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>

   <%--     </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

