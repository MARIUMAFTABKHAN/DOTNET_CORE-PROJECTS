<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdMonitoring.aspx.cs" Inherits="ABMS.AdMonitoring" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<style type="text/css">
        body {
            background-color: #f5f5f5;
        }

      

        .ajax__myTab .ajax__tab_header {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: #222222;
            border-left: solid 1px #666666;
            border-bottom: thin 1px #666666;
            height: 30px;
        }

        .ajax__myTab .ajax__tab_outer {
            padding-right: 4px;
            height: 30px;
            background-color: #fff;
            margin-right: 1px;
            border-right: solid 1px #666666;
            border-top: solid 1px #666666;
        }

        .ajax__myTab .ajax__tab_inner {
            padding-left: 4px;
            background-color: #fff;
            border: none;
        }

        .ajax__myTab .ajax__tab_tab {
            height: 20px;
            padding: 4px;
            margin: 0;
        }

        .ajax__myTab .ajax__tab_hover .ajax__tab_outer {
            background-color: #f5f5f5;
        }

        .ajax__myTab .ajax__tab_hover .ajax__tab_inner {
            background-color: #f5f5f5;
        }

        .ajax__myTab .ajax__tab_hover .ajax__tab_tab {
            background-color: #f5f5f5;
            color: #000000;
            cursor: pointer;
        }

        .ajax__myTab .ajax__tab_active .ajax__tab_outer {
            background-color: #f5f5f5;
            border-left: solid 1px #999999;
        }

        .ajax__myTab .ajax__tab_active .ajax__tab_inner {
            background-color: #f5f5f5;
        }

        .ajax__myTab .ajax__tab_active .ajax__tab_tab {
            background-color: #f5f5f5;
            color: #000000;
            cursor: inherit;
        }

        .ajax__myTab .ajax__tab_body {
            border: 1px solid #666666;
            padding: 6px;
            background-color: #ffffff;
        }
    </style>--%>
    <script type="text/javascript">
        //Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {

        //});


    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--<asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__myTab" Width="100%">
                <asp:TabPanel runat="server" HeaderText="Personal Information" ID="TabPanel1">
                    <HeaderTemplate>
                        Billing Register
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="row">--%>
                                <asp:Button runat="server" ID="btnSearchClient" class="btn btn-default" Text="Search!" OnClick="btnSearchClient_Click"></asp:Button>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                       <%-- </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- <div class="row">
        <div class="col-md-12" style="height: auto">

            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__myTab" Width="100%">
                <asp:TabPanel runat="server" HeaderText="Personal Information" ID="TabPanel1">
                    <HeaderTemplate>
                        Billing Register
                    </HeaderTemplate>
                    <ContentTemplate>

                        <div class="row">

                            <div class="col-sm-2">

                                <asp:TextBox ID="dtInsertionDate" placeholder="Insertion Date" runat="server" CssClass="form-control  input-small" Style="width: 100%"></asp:TextBox>
                                <asp:CalendarExtender ID="txtCalander" runat="server" CssClass="cal_Theme1" TargetControlID="dtInsertionDate" Format="dd/MM/yyyy" BehaviorID="_content_txtCalander" />

                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlPublication" runat="server" CssClass="form-control" Width="100%">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="row" style="margin-top: 2px">

                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlBaseStation" runat="server" CssClass="form-control" Width="100%">
                                </asp:DropDownList>

                            </div>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlAdStation" runat="server" CssClass="form-control" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 2px">

                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlBookingExecutive" runat="server" CssClass="form-control" Width="100%">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="row" style="margin-top: 2px">

                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" Width="100%">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="row" style="margin-top: 2px">

                            <div class="col-sm-4">

                                <div class="input-group">

                                    <asp:HiddenField ID="hdclientid" runat="server" />
                                    <span class="input-group-btn">
                                        <asp:Button runat="server" ID="btnSearchClient" class="btn btn-default" Text="Search!" OnClick="btnSearchClient_Click"></asp:Button>
                                    </span>
                                </div>
                                <!-- /input-group -->
                            </div>
                        </div>
                        <div class="row" style="margin-top: 2px">

                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlAgency" runat="server" CssClass="form-control" Width="100%">
                                </asp:DropDownList>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Academic Information">
                    <HeaderTemplate>
                        Academic Information
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="col-md-12" id="Main" runat="server">
                        </div>
                        this is academic information panel
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>

        </div>

    </div>--%>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
