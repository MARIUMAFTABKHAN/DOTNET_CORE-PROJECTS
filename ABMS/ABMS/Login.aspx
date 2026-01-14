<%@ Page Title="" Language="C#" MasterPageFile="~/login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ABMS.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">

        body
        {
            padding-top:2px;
        }
    </style>
    <div class="row">
        <hr />
            <div class="col-md-4  col-md-offset-4">
                <div class="panel panel-default">
                    <div class="panel-heading" >
                        <strong class="">Login</strong>

                    </div>
                    <div class="panel-body">
                        <div style="margin-bottom: 0px; top: 0px; left: 0px;" class="input-group">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </div>
                        <div style="margin-bottom: 25px; top: 0px; left: 0px;" class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>                            
                            <asp:TextBox  id="loginusername"  CssClass ="form-control" placeholder="User Name" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="loginusername" Display="Dynamic"
                                 runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>

                        <div style="margin-bottom: 25px" class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                            <asp:TextBox  id="loginpassword" TextMode="Password" CssClass ="form-control" placeholder="password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="loginpassword" Display="Dynamic"
                                 runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-9">
                                <div class="checkbox">
                                    <label class="">
                                        <input type="checkbox" class="">Remember me</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group last">
                            <div class="col-sm-offset-3 col-sm-9">
                                <asp:Button ID="btnSubmit" Text="Sign in" runat="server" class="btn btn-success btn-sm" OnClick="btnSubmit_Click"  />
                                <button type="reset" class="btn btn-default btn-sm">Reset</button>
                            </div>
                        </div>

                    </div>                   
                </div>
            </div>
        </div>
</asp:Content>
