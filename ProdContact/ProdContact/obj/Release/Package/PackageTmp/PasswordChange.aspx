<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordChange.aspx.cs" Inherits="AMR.PasswordChange" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Express News Channel</title>
    <link href="Content/MasterStyle.css" rel="stylesheet" type="text/css" />
    <link href="Content/master.css" rel="stylesheet" type="text/css" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <style type="text/css">
        .ddllist {
            height: 40px;
            width: 350px;
            margin-bottom: 25px;
            font-size: medium;
            color: #1a344d;
        }

        .txt {
            width: 300px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>

        <div class="row">
            <div class="loginheader">
                <asp:Image ImageUrl="~/Content/Images/POLogin4.png" runat="server" />
            </div>
        </div>
        <div class="logininfo">

            <div style="color: red; margin-top: 5px">
                <asp:Label ID="lblMessage" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            </div>
            
            <div class="username" style="margin-top: 30px">
                <div class="userpwdico">
                </div>
                <asp:TextBox ID="txtUserPassword" runat="server" CssClass="txt" placeholder="Existing Password" TextMode="Password"></asp:TextBox>
            </div>
            <div class="username" style="margin-top: 30px">
                <div class="userpwdico">
                </div>
                <asp:TextBox ID="txtnewUserPassword" runat="server" CssClass="txt" placeholder="New Password" TextMode="Password"></asp:TextBox>
            </div>
             <div class="username" style="margin-top: 30px">
                 <div class="userpwdico">
                 </div>
                 <asp:TextBox ID="txtrenewUserPassword" runat="server" CssClass="txt" placeholder="Re Enter New Password" TextMode="Password"></asp:TextBox>
             </div>
            <div class="form-group">
                <br />
                <div class="row">
                    <div class="col-md-2 col-md-offset-2">
                        <asp:Button Text="Save" runat="server" CssClass="login" ID="btnsave" OnClick="btnSave_Click" Style="width: 75px !important"/>
                    </div>
                    <div class="col-md-2" style="margin-left: 50px !important">
                        <asp:Button Text="Cancel" runat="server" CssClass="login" ID="btncancel" OnClick="btncancel_Click" Style="width: 95px !important;"/>
                    </div>
                </div>
            </div>

            <div class="loginfooter" style="margin-top: 10px">
            </div>
        </div>
    </form>
</body>
</html>
