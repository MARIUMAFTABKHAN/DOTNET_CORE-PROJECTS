<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Thankyou.aspx.cs" Inherits="ABMS.Thankyou" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        Job Portal
    </title>
    
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body >
    <form id="form1" runat="server">
        <script type="text/javascript">
            history.pushState(null, null, document.title);
            window.addEventListener('popstate', function () {
                history.pushState(null, null, document.title);
            });
        </script>
        <div class="form-horizontal">


            <div class="col-md-10 col-md-offset-1" style="padding-top: 20px !important">
                <img src="Content/Images/Thank-you.png" alt="Personal Information" class="img-responsive" />
            </div>
            <div class="col-md-10 col-md-offset-1" style="padding-left: 20px; padding-right: 20px; color: #510202; font-family:Exo; font-size:large">
                <br />
                <br />
                Thank you <b>
                    <asp:Label ID="lbltitle" runat="server"> </asp:Label>
                </b>for submitting your profile. All applications will be kept confidential.<br />

                <b>Your Application ID is
                <asp:Label ID="lblcliam" runat="server"></asp:Label>
                </b>
                <br />
                This ID is required for any future communication.<br />
                <br />

                We assure you that every application will be reviewed and, if the applicant is deemed to be a compelling candidate, he / she will be contacted.
            <br />
                <br />
                <br />
                <br />
                <br />
                <br />

            </div>
        </div>
    </form>
</body>
</html>
