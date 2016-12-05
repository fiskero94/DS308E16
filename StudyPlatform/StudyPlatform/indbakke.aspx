<%@ Page Title="Indbakke" Language="C#" MasterPageFile="~/newMessage.master" AutoEventWireup="true" CodeBehind="indbakke.aspx.cs" Inherits="StudyPlatform.Indbakke" %>
<%@ MasterType virtualpath="~/newMessage.master" %>
<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">
    <asp:Table ID="RecievedMessagesTable" runat="server" CssClass="table table-striped table-hover table-bordered">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell runat="server">Titel</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Sender</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Dato</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server"></asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server"></asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <asp:Panel ID="AlertPanel" runat="server"></asp:Panel>
</asp:Content>
