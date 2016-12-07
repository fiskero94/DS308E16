<%@ Page Title="Fravær" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeBehind="fravaer.aspx.cs" Inherits="StudyPlatform.Fravaer" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.Helpers" Assembly="System.Web.Helpers, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ MasterType virtualpath="~/layout.master" %>
<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">
     <asp:Table ID="AbsenceTable" runat="server" CssClass="table table-striped table-hover table-bordered text-center">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell runat="server" ColumnSpan="2" RowSpan="3" CssClass="text-center" Font-Size="larger" id="fag">Fag</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" ColumnSpan="4" CssClass="text-center">Almindeligt</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" ColumnSpan="4" CssClass="text-center">Skriftligt</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow runat="server" Font-Italic="True">
            <asp:TableHeaderCell runat="server" ColumnSpan="2" CssClass="text-center">Opgjort</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" ColumnSpan="2" CssClass="text-center">For Året</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" ColumnSpan="2" CssClass="text-center">Opgjort</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" ColumnSpan="2" CssClass="text-center">For Året</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow runat="server" Font-Italic="True">
            <asp:TableHeaderCell runat="server" CssClass="text-center">Procent</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Moduler</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Procent</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Moduler</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Procent</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Afleveringer</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Procent</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Afleveringer</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <h4>Graf over Almindeligt Fravær </h4>
     <asp:Image runat="server" ID="AbsenceGraph" Width="100%" Height="100%">
    </asp:Image>
</asp:Content>