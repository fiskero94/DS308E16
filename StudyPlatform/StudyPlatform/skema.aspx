﻿<%@ Page Title="Skema" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeBehind="skema.aspx.cs" Inherits="StudyPlatform.Skema" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType virtualpath="~/layout.master" %>



<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <asp:PlaceHolder runat="server">
        <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
    </asp:PlaceHolder>
    

    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
            -moz-opacity: 0.7;
        }
        .bold-token {
             font-weight: bold;
        }
    </style>
    
    
    

</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">

    <asp:ScriptManager runat="server" EnableScriptGlobalization="True">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle"/>
        </Scripts>
    </asp:ScriptManager>
    
    



    <%-- Popup content destination --%>
    <asp:Panel ID="Panel1" runat="server"></asp:Panel>

    <%-- Week shifters --%>
    <div class="container" style="margin-top: -80px;">
        <div class="form-group col-sm-offset-4">
            <asp:HyperLink runat="server" ID="WeekBack" CssClass="btn btn-default" BackColor="transparent" BorderStyle="None">
                <i class="fa fa-arrow-circle-o-left fa-2x"></i>
            </asp:HyperLink>
            <input type="week" runat="server" id="datepickerinut"/>
            <input type="submit" value="OK"/>
            <asp:HyperLink runat="server" ID="WeekForward" CssClass="btn btn-default" BackColor="transparent" BorderStyle="None">
                <i class="fa fa-arrow-circle-o-right fa-2x"></i>
            </asp:HyperLink>
        </div>
    </div>


    <%-- Table headers --%>
    <asp:Table runat="server" ID="scheduleTable" CssClass="table table-bordered">

        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell runat="server" CssClass="col-sm-1"></asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center" ID="tableHeaderCellMonday"></asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center" ID="tableHeaderCellTuesday"></asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center" ID="tableHeaderCellWednesday"></asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center" ID="tableHeaderCellThursday"></asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center" ID="tableHeaderCellFriday"></asp:TableHeaderCell>
        </asp:TableHeaderRow>


        <%-- Table rows --%>
        <asp:TableRow runat="server" ID="tableRow1" CssClass="table">
            <asp:TableCell runat="server">8.10</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow2" CssClass="table">
            <asp:TableCell runat="server">8.55</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow3" CssClass="table">
            <asp:TableCell runat="server"></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow4" CssClass="table">
            <asp:TableCell runat="server">9.05</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow5" CssClass="table">
            <asp:TableCell runat="server">9.50</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow6" CssClass="table">
            <asp:TableCell runat="server"></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow7" CssClass="table">
            <asp:TableCell runat="server">10:00</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow8" CssClass="table">
            <asp:TableCell runat="server">10:45</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow9" CssClass="table">
            <asp:TableCell runat="server"></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow10" CssClass="table">
            <asp:TableCell runat="server">10:55</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow11" CssClass="table">
            <asp:TableCell runat="server">11:40</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow12" CssClass="table">
            <asp:TableCell runat="server"></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow13" CssClass="table">
            <asp:TableCell runat="server">12:05</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow14" CssClass="table">
            <asp:TableCell runat="server">12:50</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow15" CssClass="table">
            <asp:TableCell runat="server"></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow16" CssClass="table">
            <asp:TableCell runat="server">13:00</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow17" CssClass="table">
            <asp:TableCell runat="server">13:45</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow18" CssClass="table">
            <asp:TableCell runat="server"></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow19" CssClass="table">
            <asp:TableCell runat="server">13:55</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow20" CssClass="table">
            <asp:TableCell runat="server">14:40</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow21" CssClass="table">
            <asp:TableCell runat="server"></asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow22" CssClass="table">
            <asp:TableCell runat="server">14:50</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="tableRow23" CssClass="table">
            <asp:TableCell runat="server">15:35</asp:TableCell>
        </asp:TableRow>

    </asp:Table>

</asp:Content>