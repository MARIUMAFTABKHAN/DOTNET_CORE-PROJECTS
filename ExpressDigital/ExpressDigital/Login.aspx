<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ExpressDigital.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Express Digital</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/MasterStyle.css" rel="stylesheet" />
    <style type="text/css">
        .username {
            /*background-image: url(images/logininfotxt.png) ;     background-repeat:no-repeat;*/
            height: 42px;
            width: 350px;
            /*margin-top: 20px;*/
            border-style: solid;
            border-color: #685642;
            border-width: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="header">
            <div class="leftHeader">
            </div>
            <div class="rightHeader">
            </div>
        </div>
        <div class="loginlogo" style="margin: 0px auto; margin-top: 70px;">
            <img src="Content/Images/digitalLogoAamir.png" alt="logo" title="Express Digital" />
        </div>

        <div class="logininfo">
            <div style="color: red; margin-top: 20px;">
                <asp:Label ID="lblMessage" ForeColor="Red" Visible="false" runat="server" Text=""></asp:Label>
            </div>
            <div class="username">
                <div class="userico">
                </div>
                <asp:TextBox ID="txtUserID" Text="" runat="server" required CssClass="txt" placeholder="User Name"></asp:TextBox>
            </div>
            <div class="username" style="margin-top: 30px">
                <div class="userpwdico">
                </div>
                <asp:TextBox ID="txtUserPassword" Text="" runat="server" required CssClass="txt" placeholder="Password" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <br />
                <asp:Button Text="Login" runat="server" CssClass="login" ID="btnLogin" OnClick="btnLogin_Click" />
            </div>
            <div class="loginfooter" style="margin-top: 10px">
            </div>
        </div>
    </form>
</body>
</html>
