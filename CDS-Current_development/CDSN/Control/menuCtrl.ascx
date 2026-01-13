<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuCtrl.ascx.cs" Inherits="CDSN.Controls.menuCtrl" %>
<style type="text/css">
   
   #menu
        {
            display: inline-block;
            min-width: 100%;
            list-style: none;           
            width:100%;
            padding-left:0px;          
            
        }
        #menu li
        {
            float: left;
            position: relative;
            list-style: none;
            width:150px;
            background-color:#3f372a;
            color:#fff496;
            height:35px !important;
            margin-left :5px !important;
           
           
        }
        #menu > li:hover > ul
        {
            display: block;           
            width:150px;
            z-index:9999;
             
           
            
        }
        #menu > li > ul
        {
            display: none;
            position: absolute;
            padding-left:0px;
            width:150px;
        }
        #menu li a
        {
            display: block;
            padding: 5px 10px;
            text-decoration: none;           
            white-space: nowrap;
            width:150px;
            color:#fff496;
            padding-top: 5px;
          
        }
        #menu li a:hover
        {
            color: #fff496;
            width:150px;
            border-left: 5px solid #fff496 !important;
            background-color:#c5a977 !important;
            border-bottom: 1px solid #fff496 !important;
        }

    .MenuBar a
    {
        color: #FFF !important;
        height: 27px;
        font-size: 10px !important;
        font-weight: 600;
        
    }
    .DynamicMenuItem
    {
        width: 140px;
        background-color: #93b8cb;
        padding: 2px;
        height: 27px;
        border-bottom-style: double;
        border-bottom-width: 1px;
        border-bottom-color: #4d758b;
        
    }
    .DynamicHover
    {
        width: 140px;
        background-color: #b4b4b4;
        padding: 2px;
        border-bottom-style: double;
        border-bottom-width: 1px;
        border-bottom-color: #4d758b;
        height: 27px;
        
    }
    .StaticMenuItem
    {
        width: 140px;
        background-color: #4d758b;
        color: White;
        height: 27px;
        border-top-style: double;
        border-top-width: 1px;
        border-top-color: #4d758b;
        text-align: center;
        
    }
    .staticHover
    {
        width: 140px;
        color: White;
        height: 27px;
        border-top-style: double;
        border-top-width: 1px;
        border-top-color: #FFF !important;
        border-bottom-style: double;
        border-bottom-width: 1px;
        border-bottom-color: #FFF !important;
    }
    .DynamicStyle
    {
        z-index:10001;
    }
</style>
<asp:PlaceHolder ID="PHMenu" runat="server">
    <div id ="menu" style="height:37px;background-color: #3f372a;"  runat="server">       
    </div>
</asp:PlaceHolder>