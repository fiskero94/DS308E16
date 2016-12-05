<%@ Page Title="Brugere" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeBehind="brugere.aspx.cs" Inherits="StudyPlatform.Brugere" %>
<%@ MasterType virtualpath="~/layout.master" %>
<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">
    <asp:Table ID="NewTable" runat="server" CssClass="table">
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" CssClass="col-sm-3">
                <asp:TextBox ID="NameTextBox" runat="server" CssClass="form-control" placeholder="Navn"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="col-sm-3">
                <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-control" placeholder="Brugernavn"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="col-sm-3">
                <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="form-control" placeholder="Adgangskode"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="col-sm-2">
                <asp:DropDownList ID="TypeDropDownList" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Kursist" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Lærer"></asp:ListItem>
                    <asp:ListItem Text="Sekretær"></asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="col-sm-1">
                <asp:Button ID="NewButton" runat="server" Text="Ny" CssClass="btn btn-success" OnClick="NewButton_OnClick"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Table ID="UsersTable" runat="server" CssClass="table table-striped table-hover">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell runat="server">ID</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Name</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Type</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server"></asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
</asp:Content>
