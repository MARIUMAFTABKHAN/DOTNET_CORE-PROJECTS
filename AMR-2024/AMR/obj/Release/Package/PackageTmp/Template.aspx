<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Template.aspx.cs" Inherits="AMR.Template" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
         <script type="text/javascript">
            
         </script>
        <style>
    .large-textarea {
        width: 100%; /* Full width of the parent container */
        min-height: 100px; /* Set a minimum height */
    }
</style>
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
                <h4>TEMPLATE MINUTES OF MEETING</h4>
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
                        <div class="col-md-2">
                            Minutes of Meeting  
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtmin" runat="server" CssClass="form-control large-textarea" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtmin" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                   </div>
                </div>
                <br />
                <div class="form-group">
                    <div class="div-md-12"> 
                        <div class="col-md-1">
                            Extra Info
                        </div>
                        <div class="col-md-1">
                            <asp:CheckBox ID="chinfo" runat="server" />
                        </div>
                        <div class="col-md-1">
                            Status
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox ID="chstatus" runat="server" />
                        </div>
                   </div>
                </div>
                <br />
                <div class="form-group">
                  <div class="div-md-12"> 
                      <%--<div class="col-md-2">
                          Status
                      </div>
                      <div class="col-md-4">
                          <asp:CheckBox ID="chstatus" runat="server" />
                      </div>--%>
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
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="ID" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <%--<asp:BoundField DataField="ID" HeaderText="ID"><HeaderStyle Width="25%" /></asp:BoundField>--%>
                                <asp:BoundField DataField="Minutes_Of_Meeting" HeaderText="Minutes of Meeting"><HeaderStyle Width="25%" /></asp:BoundField>
                                <asp:BoundField DataField="Temp_Status" HeaderText="Status"><HeaderStyle Width="25%" /></asp:BoundField>
                                <asp:BoundField DataField="Extra_Info" HeaderText="Extra Info"><HeaderStyle Width="25%" /></asp:BoundField>
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
