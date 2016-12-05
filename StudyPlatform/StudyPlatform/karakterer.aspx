<%@ Page Title="Karakterer" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeBehind="karakterer.aspx.cs" Inherits="StudyPlatform.Karakterer" %>
<%@ MasterType virtualpath="~/layout.master" %>
<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">
    <div class="container col-sm-6">
        <asp:Table ID="GradesTable" runat="server" CssClass="table table-striped table-hover table-bordered">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" CssClass="col-sm-8">Kursus</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="col-sm-4">Karakter</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
    <div class="container col-sm-6">
        <asp:Table ID="CommentsTable" runat="server" CssClass="table table-striped table-hover table-bordered">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" CssClass="col-sm-4">Kursus</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="col-sm-8">Kommentar</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>