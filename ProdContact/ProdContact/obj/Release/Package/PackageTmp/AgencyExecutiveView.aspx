<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgencyExecutiveView.aspx.cs" Inherits="AMR.AgencyExecutiveView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .click {
        background-color: darkgray;
        color: black;          
        border: none;          
        padding: 10px 20px;    
        font-size: 14px;       
        cursor: pointer;       
    }

    .click:hover {
        background-color: gray;
        color: black;  
    }
    </style>
    <asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
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
                <h4>AGENCY EXECUTIVE</h4>
               <div class="form-group">
                    <div class="div-md-12">
                        <asp:Button ID="btnview" runat="server" Text="Add new Record" CssClass="click" OnClick="btnview_Click" CausesValidation="false" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="class="div-md-12" style="margin-top: 50px;"">
                        <div class="col-md-1">
                            Agency
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtclientfilter" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtclientfilter" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-1">
                            Executive
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtexecname" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtexecname" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnfilter" runat="server" ValidationGroup="c" CssClass="btn btn-success" Text="Search" OnClick="btnfilter_Click"/>
                        </div>
                    </div>
                </div>
                
                <%--<br />--%>
                <%--grid start--%>
                <div class="form-group" style="width: 100%;">
                    <div class="div-md-12" style="width: 100%;">
                    </div>
                    <div class="div-md-12" style="margin-top: 50px; width: 100%;">
                        <asp:GridView ID="gv" PageSize="100" runat="server" CssClass="EU_DataTable" DataKeyNames="Id" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" Width="100%">
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField DataField="Agency_Name" HeaderText="Agency">
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Executive_Name" HeaderText="Executive">
                                    <HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation">
                                    <HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Department" HeaderText="Department">
                                    <HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation">
                                    <HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Exec1_UserName" HeaderText="Executive1 Name">
                                    <HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Exec2_UserName" HeaderText="Executive2 Name">
                                    <HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StatusDescription" HeaderText="Status">
                                    <HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <%--<asp:TemplateField>
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle Width="40px" />
                                    <HeaderTemplate>
                                        Edit
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <center>
                                            <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/News-Edit.ico'
                                                ValidationGroup="a" />
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                 <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                    <asp:HyperLink ID="hypEdit" runat="server" 
                                        NavigateUrl = '<%# "AgencyExecutiveform.aspx?Id=" + Convert.ToString(DataBinder.Eval(Container.DataItem,"Id")) + "&Mode=Edit" %>' 
                                        Style="display:block;width:100%;text-align:center;">
                                        <asp:Image
                                            ID="imgEdit"
                                            runat="server"
                                            ImageUrl="~/Content/images/News-Edit.ico"
                                            Width="14px"
                                            Height="14px"
                                            ToolTip="Edit" />
                                    </asp:HyperLink>
                                    
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delete">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                        <center>
                                            <asp:ImageButton runat="server" ID="DelButton" ImageUrl="~/Content/images/Trash-Cancel.ico"
                                                CommandName="Delete" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteButton_Click" ValidationGroup="a" />
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
