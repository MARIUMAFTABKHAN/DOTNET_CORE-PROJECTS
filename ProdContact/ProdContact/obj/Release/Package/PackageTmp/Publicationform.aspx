<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Publicationform.aspx.cs" Inherits="AMR.Publicationform" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
         <script type="text/javascript">
                 //function confirmDelete(button) {
                 //    return confirm("Are you sure you want to delete this record?");
                 //}
             

         </script>
        <asp:UpdateProgress ID="pu" runat="server">
            <ProgressTemplate>
                <div class="dialog-background">
                    <div class="dialog-loading-wrapper">
                        <img src="Content/Images/loading6.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <div class="form-horizontal">
            <div class="col-md-10 col-md-offset-1">
                <div class="form-group">
                    <div class="div-md-12 text-center">
                        <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <h4>PUBLICATION</h4>
                <br />
                <%--<div class="form-group">
                    <div class="div-md-12"> 
                        <div class="col-md-2">
                            ID
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtid" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtid" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                   </div>
                </div>--%>
                <br />
                <div class="form-group">
                    <div class="div-md-12"> 
                        <div class="col-md-1">
                            Pub Abbreviation
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtabb" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtabb" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Publication Name
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtname" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                   </div>
                </div>
             <%--   <br />--%>
                <div class="form-group">
                    <div class="div-md-12"> 
                        <%--<div class="col-md-1">
                            Publication Name
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtname" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>--%>
                   </div>
                </div>
                <%--<br />--%>
                <div class="form-group">
                    <div class="div-md-12"> 
                        <div class="col-md-1">
                            In-House Publication
                        </div>
                        <div class="col-md-1">
                            <asp:CheckBox ID="chhouse" runat="server" />
                        </div>
                        <div class="col-md-1">
                            AMR Comparision
                        </div>
                        <div class="col-md-1">
                            <asp:CheckBox ID="chcom" runat="server" />
                        </div>
                         <div class="col-md-1">
                             Status
                         </div>
                         <div class="col-md-1">
                             <asp:CheckBox ID="chstatus" runat="server" />
                         </div>
                   </div>
                </div>
                <br />
                
                 <%--grid start--%>
                 <div class="form-group">
                    <div class="div-md-12">
                        <div class="col-md-3 col-md-offset-2">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                    <div class="div-md-12" style="margin-top: 50px;">
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="Id" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField DataField="Publication_Name" HeaderText="Publication Name"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="Pub_Abreviation" HeaderText="Pub Abbreviation"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="In_House_Publication" HeaderText="In-House"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="AMR_Comparision" HeaderText="AMR"><HeaderStyle Width="15%" /></asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Status"><HeaderStyle Width="15%" /></asp:BoundField>

                                <asp:TemplateField>
                                    <HeaderStyle Width="10px" />
                                    <ItemStyle Width="10px" />
                                    <HeaderTemplate>
                                        Edit
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <center>
                                            <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/News-Edit.ico'
                                                ValidationGroup="a" OnClick="EditButton_Click"/>
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Delete
                                    </HeaderTemplate>
                                    <HeaderStyle Width="10px" />
                                    <ItemStyle Width="10px" />
                                    <ItemTemplate>
                                        <center>
                                              <asp:ImageButton runat="server" ID="DelButton" ImageUrl="~/Content/images/Trash-Cancel.ico"
                                                CommandName="Delete" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteButton_Click" ValidationGroup="a"/>
                                           <%-- <asp:ImageButton runat="server" ID="DelButton" ImageUrl="~/Content/images/Trash-Cancel.ico"
    CommandName="Delete" CommandArgument='<%# Eval("Id") %>' 
    OnClientClick="return confirmDelete(this);" />--%>
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
            </div>

         <%--grid end--%>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
