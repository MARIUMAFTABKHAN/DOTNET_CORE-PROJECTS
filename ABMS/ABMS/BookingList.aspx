<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookingList.aspx.cs" Inherits="ABMS.BookingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">


        function applyDatePicker() {

            $("#MainContent_dtInsertionDate").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                changeYear: true,
                buttonImage: 'Content/images/Calender.png',
                dateFormat: 'dd-mm-yy'
            });



        }
        function dosearch() {
            document.getElementById('PleaseSearch').style.display = 'block';
        }

        function pageLoad() {

            applyDatePicker();
        }
    </script>
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>


            <div class="row" style="padding-top: 2px">
                <div class="col-md-12">
                    <div class="col-md-12" style="margin-bottom: 2px; background-color: #5f021f; color: #FFF; height: 25px; padding-top: 2px">
                        <b>Booking Register
                        </b>
                    </div>
                </div>

                <div class="col-md-12">
                    <center>
                   <asp:Label ID="lblMessage" runat="server" ForeColor="#c4223d"></asp:Label></center>
                </div>

                <div class="col-md-12">
                    <div class="helptext" id="PleaseSearch" style="display: none; text-align: right; color: White; vertical-align: top;">
                        <table id="MySearch" style="text-align: center">
                            <tr>
                                <td style="width: 100%; text-align: center">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Content/Images/spinner.gif" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="col-md-12">

                    <div class="row">
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlPublication" runat="server" CssClass="form-control" Width="100%" Style="max-width: 100% !important">
                            </asp:DropDownList>
                        </div>
                        <div class="row" style="margin-top: 2px !important">
                            <div class="col-md-3">
                                <div class="input-group" style="margin-left: 15px; margin-right: 15px">
                                    <asp:TextBox ID="dtInsertionDate" ReadOnly="false" placeholder="Insertion Date" runat="server" CssClass="form-control  input-small" Style="width: 83%; max-width: 83%; display: initial" AutoPostBack="false"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSubmit" Text="View" runat="server" class="btn btn-success btn-sm" OnClick="btnSubmit_Click" />
                                    </span>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-md-12">
                    <div class="row" id="divlist" runat="server">
                        <div class="col-md-12">
                            <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
