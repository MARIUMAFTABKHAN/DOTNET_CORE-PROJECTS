<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CDSN.Home" %>
<%@ MasterType VirtualPath="~/Layout.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">

        function ShowMessage(message) {

            if (message == "Saved") {

                jAlert(message, 'CDS Alerts');
            }
            else if (message == "Updated") {

                jAlert(message, 'CDS Alerts');
            }
            else if (message == "Already") {
                jAlert(message, 'CDS Alerts');

            }
            else if (message == "REFERENCE") {
                jAlert(message, 'CDS Alerts');
            }
            else {
                jAlert(message, 'CDS Alerts');
            }
        }
     </script>

    <div style="width: 100%; height: 100%">
        <asp:GridView ID="gvRecords" runat="server" AllowPaging="true" AllowSorting="false"
            DataKeyNames="Id" AutoGenerateColumns="False" CssClass="EU_DataTable" PageSize="20" Width="100%"
            OnPageIndexChanging="gvRecords_PageIndexChanging">         
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false">
                    <ItemStyle HorizontalAlign="Left" Width="50" />
                </asp:BoundField>
                <asp:BoundField DataField="EventDate" HeaderText="Date" SortExpression="EventDate">
                    <ItemStyle HorizontalAlign="Left" Width="70" />
                </asp:BoundField>
                <asp:BoundField DataField="EventTime" HeaderText="Time" SortExpression="EventTime">
                    <ItemStyle HorizontalAlign="Left" Width="70" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="Assign by" SortExpression="UserName">
                    <ItemStyle HorizontalAlign="Left" Width="200" />
                </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Head End" SortExpression="Name">
                    <ItemStyle HorizontalAlign="Left" Width="200" />
                </asp:BoundField>
                <asp:BoundField DataField="Task" HeaderText="Task" SortExpression="Task">
                    <ItemStyle HorizontalAlign="Left" Width="600" />
                </asp:BoundField>
                <asp:BoundField DataField="EventStatus" HeaderText="Status" SortExpression="EventStatus"
                    Visible="true">
                    <ItemStyle HorizontalAlign="Left" Width="70" />
                </asp:BoundField>
                <asp:BoundField DataField="CreatedOn" HeaderText="Created" SortExpression="CreatedOn"
                    Visible="true">
                    <ItemStyle HorizontalAlign="Left" Width="70" />
                </asp:BoundField>
               <%-- <asp:TemplateField HeaderText="Edit" Visible="false">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="CENTER" />
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" ImageUrl="~/Images/News-Edit.ico" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id")%>'
                            CausesValidation="false" CommandName="ShowRecord" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Reply">
                    <ItemTemplate>
                     <Center>  <asp:ImageButton ID="imgbtn" ImageUrl="~/Content/images/Mail-reply.ico" runat="server" CausesValidation="false"
                            OnClick="imgbtn_Click" /></Center> 
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />    
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="btnCancelP" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="white" Width="770px" Style="display: none">
        <table width="100%" style="border: Solid 2px #B8B4B4; width: 100%; background-color: #FCF8F8"
            cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblHeadEndName" Font-Bold="True" Font-Size="Medium" runat="server" />
                    <div style="display: none">
                        <asp:Label ID="lblresult" runat="server" />
                    </div>
                </td>
            </tr>
            <tr style="background-color: #CCC">
                <td colspan="2" style="height: 30px; font-weight: bold; font-size: larger" align="center">
                    Message Details
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 100px">
                    Task :
                </td>
                <td style="width: 500px; background-color: #F0F8FF">
                    <asp:Label ID="lblTaskDetail" runat="server" BackColor="#F0F8FF" Font-Bold="True"
                        Font-Size="Smaller" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td>
                    <asp:GridView ID="gvEventDetails" runat="server" AllowPaging="True" AllowSorting="false"
                        DataKeyNames="EventId" AutoGenerateColumns="False" CssClass="EU_DataTable" PageSize="20"
                        Width="100%" OnRowDataBound="gvEventDetails_RowDataBound">
                       
                        <Columns>
                            <asp:BoundField DataField="AssignedBy" HeaderText="Message By" SortExpression="AssignedBy"
                                Visible="true">
                                <ItemStyle HorizontalAlign="Left" Width="100" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MessageDetails" HeaderText="Message">
                                <ItemStyle HorizontalAlign="Left" Width="500" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right" width="100px">
                    Reply:
                </td>
                <td>
                    <asp:TextBox ID="txtReply" runat="server" Rows="5" TextMode="MultiLine" Width="650px"
                        MaxLength="2000"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Send" OnClick="btnUpdate_Click"
                        CssClass="Button" />
                    <asp:Button ID="BtnCancelP" runat="server" Text="Cancel" CssClass="Button" OnClick="btnCancelP_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
