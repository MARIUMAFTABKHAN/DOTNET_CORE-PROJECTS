<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="AMR.WebForm2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Express News Channel</title>
    <link href="Content/MasterStyle.css" rel="stylesheet" type="text/css" />
    <link href="Content/master.css" rel="stylesheet" type="text/css" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

  <style type="text/css">
        body, html {
            height: 100%;
            margin: 0;
            font-family: Arial, sans-serif;
        }

        /* Full-height background image */
        .background {
             background-color: rgba(0, 0, 0, 0.5); 
            background-image: url('~/Content/Images/contact_image.png'); /* Replace with your background image */
            
            background-size: cover;
            background-position: center;
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: flex-end; /* Align to the right */
             align-items: center; 
        }

        /* Centering the login info div on the right */
        .logininfo {
            width: 350px;
            height:300px;
            background-color: rgba(255, 255, 255, 0.8); /* Slight transparency */
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .loginheader {
            text-align: left;
            margin-bottom: 20px;
        }

        .form-control {
            width: 100%;
            margin-bottom: 15px;
        }

        .login {
            background-color: gray;
            color: black;
            border: none;
            padding: 10px 20px;
            font-size: 14px;
            cursor: pointer;
            width: 100%;
        }

        .login:hover {
            background-color: darkgray;
        }

        .error-message {
            color: red;
            margin-bottom: 15px;
        }
    </style>

   

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>

        <div class="row">
    <div class="loginheader" style="background-color:gray;">
        <asp:Image ImageUrl="~/Content/Images/amr logo.png" runat="server" Height="75px" Width="149px" />
    </div>
</div>

        <div class="background">
            <div class="logininfo">
                
                <div class="error-message">
                    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </div>

                <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" placeholder="User Name" ></asp:TextBox>

                <asp:TextBox ID="txtUserPassword" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>

                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" AutoPostBack="True" placeholder="Location"></asp:DropDownList>

                <asp:Button Text="LOGIN" runat="server" CssClass="login" ID="btnLogin"  />
            </div>
        </div>
    </form>
</body>
</html>
