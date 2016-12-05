<%@ Page Title="Kurser" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeBehind="kurser.aspx.cs" Inherits="StudyPlatform.Kurser" %>
<%@ MasterType virtualpath="~/layout.master" %>
<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">
    <asp:Panel ID="ActiveCoursesPanel" runat="server" CssClass="container text-center col-sm-6"/>
    <asp:Panel ID="InactiveCoursesPanel" runat="server" CssClass="container text-center col-sm-6"/>
    <asp:Panel ID="CoursePanel" runat="server" CssClass="container-fluid"/>
</asp:Content>