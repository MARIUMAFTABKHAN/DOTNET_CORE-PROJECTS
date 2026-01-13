<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExMneuControls.ascx.cs" Inherits="AMR.ExMneuControls" %>

<%--<style type="text/css">
    .offcanvas-header {
        background-color:#4b4b4b   !important;
    }
    #offcanvasNavbar {
        background-color: #d3d3d3 !important;
        float: right !important;
        margin-right: 50px !important
    }

    .navbar-toggler-icon {
        background-image: url("data:image/svg+xml;charset=utf8,%3Csvg viewBox='0 0 32 32' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath stroke='rgba(255,255,255)' stroke-width='2' stroke-linecap='round' stroke-miterlimit='10' d='M4 8h24M4 16h24M4 24h24'/%3E%3C/svg%3E");*/
    }

    .img-congainter .navbar-toggler {
        position: relative;
        margin-left: 30px !important;
        transform: translate(-50%, -50%);
        -ms-transform: translate(-50%, -50%);
        color: white;
        font-size: 16px;
        padding: 12px 24px;
        border: none;
        cursor: pointer;
        border-radius: 5px;
        float: right !important;
        margin-top: -34px;
    }

    .header {
        min-height: 90px;
        width: 100%;
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: 0 20px;
        background: linear-gradient(to left, #4b4b4b 0%, #d3d3d3 100%);
        border: 2px solid darkred;
        box-sizing: border-box;
    }

    .logo {
        height: 60px;
        margin-right: 20px;
    }

    .header-text {
        text-align: center;
        flex-grow: 1;
        color: white;
        font-family: 'Segoe UI', sans-serif;
    }

    .main-title {
        font-size: 22px;
        font-weight: bold;
    }

    .sub-title {
        font-size: 12px;
        opacity: 0.9;
    }

    /* Toggler button on the right inside header */
    .header-toggler {
        margin-right: 20px;
        width: 30px;
        height: 30px;
        padding: 0;
        background-color:white;
    }

        .header-toggler .navbar-toggler-icon {
            width: 100%;
            height: 100%;
        }

    .sub-bar {
        height: 25px;
        background-color: #d3d3d3; /* light grey */
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: 0 20px;
        font-size: 12px;
        margin: 0;
        box-sizing: border-box;
    }

    .sub-label {
        color: #333;
        font-weight: 500;
    }

    .logout-link {
        text-decoration: none;
        color: #333;
        font-weight: 500;
    }
</style>--%>

<style type="text/css">
    .header {
        min-height: 90px;
        width: 100%;
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: 0 20px;
        background: linear-gradient(to left, #4b4b4b 0%, #d3d3d3 100%);
        border: 2px solid darkred;
        box-sizing: border-box;
    }

    .logo {
        height: 60px;
        margin-right: 20px;
    }

    .header-text {
        text-align: center;
        flex-grow: 1;
        color: white;
        font-family: 'Segoe UI', sans-serif;
    }

    .main-title {
        font-size: 22px;
        font-weight: bold;
    }

    .sub-title {
        font-size: 12px;
        opacity: 0.9;
    }

    .header-menu {
        position: relative;
        float: right;
        margin-right: 20px;
    }

    .dropdown-menu {
        min-width: 180px;
    }

    .dropdown-submenu {
        position: relative;
    }

    .dropdown-submenu>.dropdown-menu {
        top: 0;
        left: -100%;
        margin-top: -6px;
        margin-left: -1px;
    }

    .dropdown-submenu:hover>.dropdown-menu {
        display: block;
    }

    .sub-bar {
        height: 25px;
        background-color: #d3d3d3;
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: 0 20px;
        font-size: 12px;
        margin: 0;
        box-sizing: border-box;
    }

    .sub-label {
        color: #333;
        font-weight: 500;
    }

    .logout-link {
        text-decoration: none;
        color: #333;
        font-weight: 500;
    }
</style>


<asp:PlaceHolder ID="PlaceHolderMenu" runat="server"></asp:PlaceHolder>
