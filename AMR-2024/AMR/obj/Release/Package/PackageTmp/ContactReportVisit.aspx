<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactReportVisit.aspx.cs" Inherits="AMR.ContactReportVisit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <%--grid start--%>
            <div class="form-horizontal">
            <div class="col-md-10 col-md-offset-1">
                <div class="form-group">
                    <div class="div-md-12 text-center">
                        <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <h4>CONTACT REPORT NEXT VISIT </h4>
                <br />
                 
                <div class="form-group">

                    <div class="div-md-12" style="margin-top: 50px;">
                        <asp:GridView ID="gv" runat="server" PageSize="25"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            OnPageIndexChanging="gv_PageIndexChanging"
                            CssClass="EU_DataTable">

                            <Columns>
                                <asp:BoundField DataField="VisitPartyName" HeaderText="Client / Agency" />
                                <asp:BoundField DataField="Brand_Name" HeaderText="Brand Name" />
                                <asp:BoundField DataField="Main_Category" HeaderText="Main Category" />
                                <asp:BoundField DataField="Sub_Category" HeaderText="Sub Category" />
                                
                                
                                <asp:BoundField DataField="Visit_Date" HeaderText="Visit Date" DataFormatString="{0:dd-MM-yyyy}" />
                                <asp:BoundField DataField="Followup_Visit_Date" HeaderText="Next Follow Up Date" DataFormatString="{0:dd-MM-yyyy}" />
                                

                               
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>

                </div>
                </div>
        <%--grid end--%>

    </asp:Content>