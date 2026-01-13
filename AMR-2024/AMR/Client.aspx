<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="AMR.Client" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
             <script type="text/javascript">
                 
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
                    <h4>CLIENT</h4>
                    <br />
                    <div class="form-group">
                        <div class="row"> 
                            <div class="col-md-1">
                                Client Name
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtname" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtname" ForeColor="Red" Display="Dynamic"
                                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1">
                                Client Abbreviation
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtabb" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row"> 
                            <div class="col-md-1">
                                Category
                            </div>
                            <div class="col-md-3">
                                  <asp:DropDownList ID="ddlcat" runat="server" CssClass="form-control" Style="font-size: small;" AutoPostBack="true" OnSelectedIndexChanged="ddlcat_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1">
                                Client Sector
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlsec" runat="server" CssClass="form-control" Style="font-size: small;" AutoPostBack="true" OnSelectedIndexChanged="ddlsec_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row"> 
                            <div class="col-md-1">
                                Address Line 1
                            </div>
                            <div class="col-md-3">
                                 <asp:TextBox ID="txtadd1" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1">
                                Address Line 2
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtadd2" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row"> 
                            <div class="col-md-1">
                                Address Line 3
                            </div>
                            <div class="col-md-3">
                                 <asp:TextBox ID="txtadd3" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1">
                                City
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtcity" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtcity" ForeColor="Red" Display="Dynamic"
                                    ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                  <div class="form-group">
                        <div class="row"> 
                            <div class="col-md-1">
                                Telephone Nos
                            </div>
                            <div class="col-md-3">
                                 <asp:TextBox ID="txttel" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1">
                                Fax Nos
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtfax" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                          <div class="row"> 
                              <div class="col-md-1">
                                  Email Address
                              </div>
                              <div class="col-md-3">
                                   <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                              </div>
                              <div class="col-md-1"></div>
                              <div class="col-md-1">
                                    Edition
                              </div>
                              <div class="col-md-3">
                                    <asp:DropDownList ID="ddledition" runat="server" CssClass="form-control" Style="font-size: small;" AutoPostBack="true" OnSelectedIndexChanged="ddledition_SelectedIndexChanged"></asp:DropDownList>
                              </div>
                          </div>
                      </div>

                     <div class="form-group">
                        <div class="row"> 
                            <div class="col-md-1">
                                Client Type
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlclienttype" runat="server" CssClass="form-control" Style="font-size: small;">
                                <asp:ListItem Text="Select Client type" Value=""/>
                                <asp:ListItem Text="Normal" Value="N" />
                                <asp:ListItem Text="Group" Value="G" />
                                <asp:ListItem Text="Barter" Value="B" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1">
                                Status
                            </div>
                            <div class="col-md-3">
                                <asp:CheckBox ID="chstatus" runat="server" />
                            </div>
                            
                        </div>
                    </div>

                     <div class="form-group">
                        <div class="row"> 
                            <div class="col-md-1">
                                 AMR
                            </div>
                            <div class="col-md-3">
                                <asp:CheckBox ID="chamr" runat="server" />
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1">
                                Contact Report
                            </div>
                            <div class="col-md-3">
                                 <asp:CheckBox ID="chreport" runat="server" />
                            </div>
                        </div>
                    </div>

                     <%--grid start--%>
                     <div class="form-group">
                        <div class="div-md-12">
                            <div class="col-md-3 col-md-offset-2">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                         <br />
                         <div class="class="div-md-12" style="margin-top: 50px;"">
                             <div class="col-md-1">
                                 Client Name
                             </div>
                             <div class="col-md-3">
                                 <asp:TextBox ID="txtclientfilter" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                                 <asp:RequiredFieldValidator ControlToValidate="txtclientfilter" ForeColor="Red" Display="Dynamic"
                                     ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                             </div>

                             <div class="col-md-1">
                                 <asp:Button ID="btnfilter" runat="server" ValidationGroup="c" CssClass="btn btn-success" Text="Search" OnClick="btnfilter_Click" />
                             </div>
                         </div>
                         <br />
                         <br />
                        <div class="div-md-12" style="margin-top: 50px;">
                            <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="Id" AutoGenerateColumns="false"
                                AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="Client_Name" HeaderText="Client">
                                        <HeaderStyle Width="25%" /></asp:BoundField>
                                    <asp:BoundField DataField="Category_Title" HeaderText="Category">
                                        <HeaderStyle Width="15%" HorizontalAlign="Center"/>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="clientSectorName" HeaderText="Client Sector">
                                        <HeaderStyle Width="15%" HorizontalAlign="Center"/>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Address_Line_4"  HeaderText="City">
                                        <HeaderStyle Width="15%" HorizontalAlign="Center"/>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GroupCompName"  HeaderText="Edition">
                                        <HeaderStyle Width="15%" HorizontalAlign="Center"/>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ClientType" HeaderText="Client Type">
                                        <HeaderStyle Width="15%" HorizontalAlign="Center"/>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StatusDes" HeaderText="Status">
                                        <HeaderStyle Width="15%" HorizontalAlign="Center"/>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
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
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="40px" />
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
