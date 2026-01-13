<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CDSN.Login" %>
<%@ OutputCache VaryByParam="*" Duration="60" VaryByCustom="isMobileDevice" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta name="viewport" content="width=device-width" />
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

        .header {
            /*background-image:url("~/Content/Images/headerBG.png");*/
        }
        /*.btn-info {
            background-color :#4682B4 !important; 
            color:white;
        }
        .btn-info:hover
        {
            background-color :#bed8eb !important;
            color:white;
        }*/

        .btn-info, .btn-info:focus, .btn-info:active {
            background-color: #4682B4 !important;
            color: white !important;
            border: none;
        }

        .btn-info:hover {
            background-color: #bed8eb !important;
            color: white !important;
        }
    </style>

</head>
<body >

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="container">
            <div class="row col-md-12" style="margin-top :90px">
                <div class="col-md-6 col-md-offset-3">
                    <div style="text-align: center">
                      
                        <asp:Image ImageUrl="~/Content/Images/Logo.png" CssClass="img-fluid" runat="server" />
                    </div>
                    <div class="col-sm-12 text-black"  style="margin-top :30px">

                       <%-- <div class="px-5 ms-xl-4">
                            <i class="fas fa-crow fa-2x me-3 pt-5 mt-xl-4" style="color: #709085;"></i>
                            <span class="h1 fw-bold mb-0">Logo</span>
                        </div>

                        <div class="d-flex align-items-center h-custom-2 px-5 ms-xl-4 mt-5 pt-5 pt-xl-0 mt-xl-n5">



                            <h3 class="fw-normal mb-3 pb-3" style="letter-spacing: 1px;">Log in</h3>--%>
                            <asp:Label ID="lbl" runat="server"></asp:Label>
                            <div class="form-outline mb-4">
                                <asp:TextBox ID="txtUserID" runat="server" placeholder="Input User ID" aria-label="Username" class="form-control form-control-lg">
                                </asp:TextBox>
                                <label class="form-label" for="Username" style="margin-left: 0px;"></label>
                                <div class="form-notch">
                                    <div class="form-notch-leading" style="width: 9px;"></div>
                                    <div class="form-notch-middle" style="width: 88.8px;"></div>
                                    <div class="form-notch-trailing"></div>
                                </div>
                            </div>

                            <div class="form-outline mb-4">
                                <asp:TextBox ID="txtUserPassword" TextMode="Password" runat="server" placeholder="Input Password" aria-label="Password " class="form-control form-control-lg">
                                
                                </asp:TextBox>
                                <label class="form-label" for="Password" style="margin-left: 0px;"></label>
                                <div class="form-notch">
                                    <div class="form-notch-leading" style="width: 9px;"></div>
                                    <div class="form-notch-middle" style="width: 64px;"></div>
                                    <div class="form-notch-trailing"></div>
                                </div>
                            </div>

                            <div class="pt-1 mb-4">
                                <asp:Button runat="server" ID="btnLogin" class="btn btn-info btn-lg btn-block" OnClick="btnLogin_Click" Text="Login"></asp:Button>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
       

    </form>


</body>
</html>
