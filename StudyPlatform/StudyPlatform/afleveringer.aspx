<%@ Page Title="Afleveringer" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeBehind="afleveringer.aspx.cs" Inherits="StudyPlatform.Afleveringer" %>
<%@ MasterType virtualpath="~/layout.master" %>
<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server" enctype="multipart/form-data">
    <asp:Panel ID="AlertPanel" runat="server"></asp:Panel>
    <asp:Table ID="AssignmentDescriptionsTable" runat="server" CssClass="table table-striped table-hover table-bordered">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell runat="server" CssClass="col-sm-9">Title</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="col-sm-1">Deadline</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="col-sm-2">Aflever</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="col-sm-1">Karakter</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <asp:Panel ID="SubmitPanel" runat="server" CssClass="card card-block">
        <h4>
            Aflever til
            <small class="text-muted">
                <asp:Label ID="SubmitAssignmentDescriptionTitle" runat="server"></asp:Label>
            </small>
        </h4>
        <p>Kommentar</p>
        <asp:TextBox ID="SubmitCommentTextBox" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        <br/>
        <asp:FileUpload id="FileUploadControl" runat="server" AllowMultiple="True"/>
        <hr/>
        <asp:Label ID="SubmitResponseLabel" runat="server" CssClass="text-danger"></asp:Label>
        <div class="form-group pull-right">
            <a href="afleveringer.aspx" class="btn btn-default">Annuller</a>
            <asp:Button ID="SubmitAssignmentButton" runat="server" Text="Aflever" CssClass="btn btn-primary" OnClick="SubmitAssignmentButton_OnClick"/>
        </div>
    </asp:Panel>
    <asp:Panel ID="SuccessPanel" runat="server" >
        <h3>
            Du har afleveret
            <small class="text-muted">
                <asp:Label ID="SuccessAssignmentDescription" runat="server"></asp:Label>
            </small>
        </h3>
        <div class="container-fluid">
            <h4>Kommetar</h4>
            <p>
                <asp:Label ID="SuccessComment" runat="server"></asp:Label>
            </p>
        </div>
        <div class="container-fluid">
            <h4>Dokumenter</h4>
            <p></p><asp:Label ID="SuccessDocuments" runat="server"></asp:Label>
        </div>
        <br/>
        <div class="form-group">
            <a href="afleveringer.aspx" class="btn btn-default">Tilbage til afleveringer</a>
        </div>
    </asp:Panel>
</asp:Content>