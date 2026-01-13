<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RatePolicyform.aspx.cs" Inherits="AMR.RatePolicyform" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function togglesubcat() {
            var ddlno = document.getElementById('<%= ddlsubcat.ClientID %>');

            if (ddlno.disabled) {
                ddlno.disabled = false;
            } else {
                ddlno.disabled = true;
            }
        }
    </script>

    <div class="col-md-12">

        <div class="col-md-4 col-md-offset-1" style="margin-top: 5px !important">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: lightblue;">
                    <h5>Rate Policy</h5>
                </div>
                <br />
                <div class="panel-body" style="padding-bottom: 9px !important">
                    <div class="col-md-12">
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            Publication
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlpub" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPublicationGroup" runat="server"
                                ControlToValidate="ddlpub"
                                ErrorMessage="Please select a publication."
                                ForeColor="Red"
                                Display="Dynamic"
                                InitialValue="">
                                </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Main Caregory
                        </div>
                        <div class="col-md-6">
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
                    <div class="row">
                        <div class="col-md-4">
                            Sub Category
                                &nbsp;&nbsp;<asp:CheckBox ID="chsubcat" runat="server" onclick="togglesubcat()" />
                        </div>
                        <div class="col-md-6">

                            <asp:DropDownList ID="ddlsubcat" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Page No.
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlno" runat="server" CssClass="compact-dropdown" Style="font-size: small; width: 90px; height: 30px;"></asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp; Back Page&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chbackpage" runat="server" />
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            Applicable on Stations(Min.)
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtminno" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Percentage (%)
                        </div>
                        <div class="col-md-2">
                            <asp:CheckBox ID="chper" runat="server" />
                        </div>
                        <div class="col-md-3">
                            Base Station
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox ID="chbase" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Amount
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtamt" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Percentage(%) With Size
                        </div>
                        <div class="col-md-1">
                            <asp:CheckBox ID="chpersize" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Rate%
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtsize" runat="server" CssClass="col-md-9" Style="height: 30px;"></asp:TextBox>
                            &nbsp;&nbsp;<asp:DropDownList ID="ddlop" runat="server" CssClass="compact-dropdown" Style="font-size: small; width: 50px; height: 30px;"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Combine(any Page)
                        </div>
                        <div class="col-md-2">
                            <asp:CheckBox ID="chcomb" runat="server" />
                        </div>
                        <div class="col-md-3">
                            Size Exception
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox ID="chexcep" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Supplement
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox ID="chsup" runat="server" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="col-md-4 col-md-offset-1" style="margin-top: 5px !important">
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

                            <div class="col-md-3">
                                City Edition
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="ddlcity" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
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
                    
                    <asp:GridView ID="gv" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                        <Columns>
                             <asp:TemplateField HeaderText="City ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcityId" runat="server" Text='<%# Eval("GroupComp_Id") %>' ></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                            <asp:TemplateField HeaderText="City">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcityName" runat="server" Text='<%# Eval("Abreviation") %>'></asp:Label>
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
                                                CommandName="Delete" CommandArgument='<%# Eval("GroupComp_Id") %>'  ValidationGroup="a" OnClick="DeleteButton_Click"/>
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
