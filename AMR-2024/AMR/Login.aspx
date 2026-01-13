<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AMR.Login" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Login</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <style>
      body {
        background-color:lightgrey;
      }

    .login-container {
        height: 100vh;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .login-panel {
        padding: 30px;
        border-radius: 20px;
        border-color:black;
        background-color: 	#C0C0C0;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        width: 100%;
        max-width: 400px;
        text-align: center;
    }

    .login-logo {
        width: 100px;
        margin-bottom: 20px;
    }

    .btn-darkgray {
        background-color: gray ;
        color: 	black;
    }

    .btn-darkgray:hover {
        background-color: 	#e1f1fd;
        color:black ;
    }
    </style>

</head>
<body>
    <form id="form1" runat="server">
         <div class="container login-container">
     <div class="login-panel">
         <img src="Content/Images/Logo.png" alt="Express Media Group" class="login-logo" />
         <div class="form-group mb-3">
            <asp:Label ID="lblMessage" ForeColor="Red" runat="server" CssClass="form-label"></asp:Label> 
         </div>
         <div class="form-group mb-3"  style="text-align:left" >
             <asp:Label ID="lblUsername" runat="server" Text="User Name" CssClass="form-label"></asp:Label>
             <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" />
             <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUserID"
                 ErrorMessage="txtUserID is required" CssClass="text-danger" Display="Dynamic" />
         </div>
         <div class="form-group mb-3"  style="text-align:left">
             <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="form-label"  style="text-align:left"></asp:Label>
             <asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" CssClass="form-control" />
             <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtUserPassword"
                 ErrorMessage="Password is required" CssClass="text-danger" Display="Dynamic" />
         </div>
         <br />
           <div class="form-group mb-3"  style="text-align:left">
                     <%--<asp:Label ID="lLocation" runat="server" Text="Location" CssClass="form-label"  style="text-align:left"></asp:Label>
                     <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"  ></asp:DropDownList>--%>
           </div>
         

         <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-darkgray w-100" OnClick="btnLogin_Click" />
 </div>
 </div>
    </form>
</body>
</html>
