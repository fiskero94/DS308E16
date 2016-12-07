<%@ Page Title="Udbakke" Language="C#" MasterPageFile="~/newMessage.master" AutoEventWireup="true" CodeBehind="udbakke.aspx.cs" Inherits="StudyPlatform.Udbakke" %>
<%@ MasterType virtualpath="~/newMessage.master" %>
<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">
    <asp:Table ID="SentMessagesTable" runat="server" CssClass="table table-striped table-hover table-bordered">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell runat="server" CssClass="col-sm-5">Titel</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="col-sm-4">Modtagere</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="col-sm-2">Dato</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="col-sm-1"></asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <asp:Panel ID="AlertPanel" runat="server"></asp:Panel>
</asp:Content>
