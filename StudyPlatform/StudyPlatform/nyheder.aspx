<%@ Page Title="Nyheder" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeBehind="nyheder.aspx.cs" Inherits="StudyPlatform.Nyheder" %>
<%@ MasterType virtualpath="~/layout.master" %>
<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">
    <div class="form-group">
        <asp:Button ID="NewNewsButton" runat="server" Text="Ny nyhed" CssClass="btn btn-default"/>
    </div>
    <div class="collapse" id="newnews">
        <div class="card card-block">
            <div class="form-group">
                <asp:TextBox ID="TitleTextBox" runat="server" CssClass="form-control" placeholder="Titel"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="TextTextBox" runat="server" CssClass="form-control" placeholder="Tekst" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="CreateNewsButton" runat="server" Text="Offentliggør nyheden" CssClass="btn btn-success" OnClick="CreateNewsButton_OnClick"/>
                <asp:Label ID="ResponseLabel" runat="server" CssClass="text-danger"></asp:Label>
            </div>
        </div>
    </div>
    <asp:Table ID="NewsTable" runat="server" CssClass="table table-striped table-hover table-bordered">
        <asp:TableHeaderRow ID="NewsTableHeaderRow" runat="server">
            <asp:TableHeaderCell runat="server">Titel</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Forfatter</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Dato</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
</asp:Content>