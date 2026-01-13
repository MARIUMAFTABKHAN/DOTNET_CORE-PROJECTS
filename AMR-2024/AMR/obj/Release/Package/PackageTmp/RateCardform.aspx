<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RateCardform.aspx.cs" Inherits="AMR.RateCardform" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12">

        <div class="col-md-10 col-md-offset-1" style="margin-top: 5px !important">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: lightblue;">
                    <h5>Rate Card</h5>
                </div>
                <br />
                <div class="panel-body" style="padding-bottom: 9px !important">
                    <div class="col-md-12">
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-1">
                                Publication
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlpub" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPublicationGroup" runat="server"
                                    ControlToValidate="ddlpub"
                                    ErrorMessage="Please select a publication."
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    InitialValue="">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-2"></div>
                            <div class="col-md-1">
                                Main Caregory
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlmaincat" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlmaincat_SelectedIndexChanged" Style="font-size: small;"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="ddlmaincat"
                                    ErrorMessage="Please select a Main Category."
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    InitialValue="">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdneffDate" runat="server" />
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-1">
                                Effective from
                            </div>
                            <div class="col-md-3">
                                <input id="txteffdate" type="date" class="form-control" runat="server" style="font-size: small;" />
                            </div>
                            <div class="col-md-2"></div>
                            <div class="col-md-1">
                                Supplement
                            </div>
                            <div class="col-md-3">
                                <asp:CheckBox ID="chsup" runat="server" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
            <div class="col-md-10 col-md-offset-1" style="margin-top: 5px !important">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: lightblue;">
                        <h5>Detail</h5>
                    </div>
                    <br />
                    <div class="panel-body" style="padding-bottom: 9px !important">
                        <div class="col-md-12">
                            <div class="col-md-12 text-center">
                                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-1">
                                    Sub Category
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlsubcat" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="ddlsubcat"
                                        ErrorMessage="Please select a Sub Category."
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        InitialValue="">
                                    </asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-1">
                                    City Edition
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlcity" runat="server" CssClass="form-control"  Style="font-size: small;"></asp:DropDownList>
                                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="ddlcity"
                                        ErrorMessage="Please select a City Edition."
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        InitialValue="">
                                    </asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Full
                                    <asp:TextBox ID="txtfull" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Half
                                    <asp:TextBox ID="txthalf" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Day
                                    <asp:DropDownList ID="ddlday" runat="server" CssClass="form-control" Style="font-size: small;">
                                    <asp:ListItem Text="Select Day" Value=""/>
                                    <asp:ListItem Text="Monday"/>
                                    <asp:ListItem Text="Tuesday"/>
                                    <asp:ListItem Text="Wednesday"/>
                                    <asp:ListItem Text="Thursday"/>
                                    <asp:ListItem Text="Friday"/>
                                    <asp:ListItem Text="Saturday"/>
                                    <asp:ListItem Text="Sunday"/>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Full
                                    <asp:TextBox ID="txtdayfull" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Half
                                    <asp:TextBox ID="txtdayhalf" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                
                            </div>
                        </div>
                        <br />

                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary"
                                        OnClick="btnAdd_Click" CausesValidation="false" />
                                </div>
                             </div>
                        </div>

                        <asp:HiddenField ID="hfEditIndex1" runat="server" Value="-1" />
                        <asp:HiddenField ID="hfEditIndex2" runat="server" Value="-1" />
                        <asp:GridView ID="gv" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                            <Columns>
                                 <asp:TemplateField HeaderText="Subcategory ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsubcatId" runat="server" Text='<%# Eval("Id") %>' ></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Subcategory">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsubcatName" runat="server" Text='<%# Eval("Category_Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="City ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcityId" runat="server" Text='<%# Eval("GroupComp_Id") %>' ></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>--%>
                                 <asp:TemplateField HeaderText="City">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcityName" runat="server" Text='<%# Eval("Abreviation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Full">
                                    <ItemTemplate>
                                        <asp:Label ID="lblfull" runat="server" Text='<%# Eval("Full") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Half">
                                    <ItemTemplate>
                                        <asp:Label ID="lblhalf" runat="server" Text='<%# Eval("Half") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday" runat="server" Text='<%# Eval("Day") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Full">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldayfull" runat="server" Text='<%# Eval("DayFull") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Half">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldayhalf" runat="server" Text='<%# Eval("DayHalf") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField>
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
                                </asp:TemplateField>--%>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Delete
                                    </HeaderTemplate>
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                        <center>
                                            <asp:ImageButton runat="server" ID="DelButton" ImageUrl="~/Content/images/Trash-Cancel.ico"
                                                CommandName="Delete" CommandArgument='<%# Eval("Id") %>'  ValidationGroup="a" OnClick="DeleteButton_Click"/>
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>

            <div class="col-md-10 col-md-offset-1" style="margin-top: 10px !important; margin: 0 auto;">
                <div class="col-md-3 col-md-offset-2">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-dark" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
            <div class="col-md-10 col-md-offset-1" style="margin-top: 10px !important; margin: 0 auto;"></div>
    </div>
</asp:Content>
