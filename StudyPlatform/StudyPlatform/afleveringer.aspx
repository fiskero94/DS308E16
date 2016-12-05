<%@ Page Title="Afleveringer" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeBehind="afleveringer.aspx.cs" Inherits="StudyPlatform.Afleveringer" %>
<%@ MasterType virtualpath="~/layout.master" %>
<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">
    <asp:Table ID="AssignmentDescriptionsTable" runat="server" CssClass="table table-striped table-hover table-bordered">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell runat="server">Beskrivelse</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Dokumenter</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Deadline</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Aflever</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
</asp:Content>