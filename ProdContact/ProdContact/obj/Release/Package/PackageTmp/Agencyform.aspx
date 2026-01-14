<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Agencyform.aspx.cs" Inherits="AMR.Agencyform" %>
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
                <h4>AGENCY</h4>
                <br />
                <div class="form-group">
                    <div class="row"> 
                        <div class="col-md-1">
                            Agency Name
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtname" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Agency Type
                        </div>
                        <div class="col-md-3">
                             <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" Style="font-size: small;">
                                 <asp:ListItem Text="Select Agency type" Value=""/>
                                 <asp:ListItem Text="Accredited" Value="A" />
                                 <asp:ListItem Text="Non-Accredited" Value="N" />
                             </asp:DropDownList>
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
                            Address Line 4
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtadd4" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                           
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
                             Credit Days
                         </div>
                         <div class="col-md-3">
                             <asp:TextBox ID="txtdays" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                             
                         </div>
                    </div>
                </div>

                <div class="form-group">
                      <div class="row"> 
                          <div class="col-md-1">
                              Credit Limit
                          </div>
                          <div class="col-md-3">
                               <asp:TextBox ID="txtlimit" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                               
                          </div>
                          <div class="col-md-1"></div>
                          <div class="col-md-1">
                            Agency Group
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlagency" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlagency_SelectedIndexChanged" Style="font-size: small;" ></asp:DropDownList>
                        </div>
                      </div>
                  </div>

                 <div class="form-group">
                    <div class="row"> 
                        <div class="col-md-1">
                            Edition Responsible
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddledition" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddledition_SelectedIndexChanged" Style="font-size: small;" ></asp:DropDownList>
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
                            Agency
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtclientfilter" runat="server" CssClass="form-control" Style="font-size: small;"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtclientfilter" ForeColor="Red" Display="Dynamic"
                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-1">
                            <asp:Button ID="btnfilter" runat="server" ValidationGroup="c" CssClass="btn btn-success" Text="Search" OnClick="btnfilter_Click"/>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="div-md-12" style="margin-top: 50px;">
                        <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable" DataKeyNames="Id" AutoGenerateColumns="false"
                            AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                             GridLines="Both" CellPadding="0" CellSpacing="0">
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField DataField="Agency_Name" HeaderText="Agency">
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Accredited_Status" HeaderText="Agency Type">
                                    <HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GroupComp_Name" HeaderText="Edition"><HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="_Status" HeaderText="Status"><HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="_AMR" HeaderText="AMR"><HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                     <ItemStyle HorizontalAlign="Center" />
                                 </asp:BoundField>
                                <asp:BoundField DataField="_cExport" HeaderText="Contact Report"><HeaderStyle Width="10%" HorizontalAlign="Center"/>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="EditButton" ImageUrl='~/Content/images/News-Edit.ico'
                                            ValidationGroup="a" Width="14px" Height="14px"
                                            Style="display:block;margin:auto;" OnClick="EditButton_Click"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="DelButton" ImageUrl="~/Content/images/Trash-Cancel.ico"
                                                  CommandName="Delete" CommandArgument='<%# Eval("Id") %>' 
                                                Width="14px" Height="14px" Style="display:block;margin:auto;"
                                                OnClick="DeleteButton_Click" ValidationGroup="a"/>
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
