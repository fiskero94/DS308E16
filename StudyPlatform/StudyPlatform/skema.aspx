<%@ Page Title="Skema" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeBehind="skema.aspx.cs" Inherits="StudyPlatform.Skema" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType virtualpath="~/layout.master" %>






<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <asp:PlaceHolder runat="server">
        <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
    </asp:PlaceHolder>
    
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">

    <asp:ScriptManager runat="server" EnableScriptGlobalization="True">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle"/>
        </Scripts>   
    </asp:ScriptManager>


    <asp:PlaceHolder runat="server" ID="modalPlaceHolder">
        <div>

            <asp:Panel ID="Panel1" runat="server">

            </asp:Panel>

            <div class="container col-sm-12" style="margin-top: -80px;">
                <div class="form-group col-sm-4 col-sm-offset-4">

                    <asp:LinkButton runat="server" ID="JumpWeekLeft" CssClass="btn btn-default" OnClick="JumpWeekLeft_OnClick">
                        <i class="fa fa-arrow-circle-o-left fa-2x"></i>
                    </asp:LinkButton>


                    <button class="btn btn-default disabled">
                        Uge <asp:Label runat="server" ID="CurrentWeekNumber" CssClass=""></asp:Label>
                    </button>
                    <asp:LinkButton runat="server" ID="JumpWeekRight" CssClass="btn btn-default" OnClick="JumpWeekRight_OnClick"><i class="fa fa-arrow-circle-o-right fa-2x"></i></asp:LinkButton>
                </div>
            </div>


            

    
    <asp:Table runat="server" ID="scheduleTable" CssClass="table table-bordered">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell runat="server" CssClass="col-sm-1"></asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Mandag</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Tirsdag</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Onsdag</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Torsdag</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server" CssClass="text-center">Fredag</asp:TableHeaderCell>  
    </asp:TableHeaderRow>
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

    
    

            </div>

    </asp:PlaceHolder>



    
    
    
    
    

</asp:Content>
