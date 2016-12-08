<%@ Page Title="Skema" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeBehind="skema.aspx.cs" Inherits="StudyPlatform.Skema" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType virtualpath="~/layout.master" %>






<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <asp:PlaceHolder runat="server">
        <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
    </asp:PlaceHolder>
    
    <script src="Scripts/coolb.js"></script>

    <style type="text/css">   
        .modalBackground 
        {
            background-color: black;
            opacity: 0.8;        
        }
        .modalPopup 
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;    
        }

        .overlay 
        {
            background: pink;
            display: none;
            position: absolute; 
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            opacity: 0.5;         
        }




    </style>
    
   
</asp:Content>


<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">

    <asp:ScriptManager runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle"/>
        </Scripts>
        

    </asp:ScriptManager>


<div class="overlay">

            <button id="bada">Submit</button>
    </div>
    


    <asp:PlaceHolder runat="server" ID="modalPlaceHolder">
        <div>

            <asp:Panel ID="Panel1" runat="server">

<%--                <asp:Button ID="Button1" runat="server" Text="CreateModal" OnClick="Button_Click" CssClass="right"/>--%>

<%--                <asp:Button runat="server" ID="08:10Monday"/>
                <asp:Button runat="server" ID="08:10Tuesday"/>
                <asp:Button runat="server" ID="08:10Wednesday"/>
                <asp:Button runat="server" ID="08:10Thursday"/>
                <asp:Button runat="server" ID="08:10Friday"/>

                <asp:Button runat="server" ID="09:05Monday"/>
                <asp:Button runat="server" ID="09:05Tuesday"/>
                <asp:Button runat="server" ID="09:05Wednesday"/>
                <asp:Button runat="server" ID="09:05Thursday"/>
                <asp:Button runat="server" ID="09:05Friday"/>

                <asp:Button runat="server" ID="10:00Monday"/>
                <asp:Button runat="server" ID="10:00Tuesday"/>
                <asp:Button runat="server" ID="10:00Wednesday"/>
                <asp:Button runat="server" ID="10:00Thursday"/>
                <asp:Button runat="server" ID="10:00Friday"/>

                <asp:Button runat="server" ID="10:55Monday"/>
                <asp:Button runat="server" ID="10:55Tuesday"/>
                <asp:Button runat="server" ID="10:55Wednesday"/>
                <asp:Button runat="server" ID="10:55Thursday"/>
                <asp:Button runat="server" ID="10:55Friday"/>

                <asp:Button runat="server" ID="12:05Monday"/>
                <asp:Button runat="server" ID="12:05Tuesday"/>
                <asp:Button runat="server" ID="12:05Wednesday"/>
                <asp:Button runat="server" ID="12:05Thursday"/>
                <asp:Button runat="server" ID="12:05Friday"/>

                <asp:Button runat="server" ID="13:00Monday"/>
                <asp:Button runat="server" ID="13:00Tuesday"/>
                <asp:Button runat="server" ID="13:00Wednesday"/>
                <asp:Button runat="server" ID="13:00Thursday"/>
                <asp:Button runat="server" ID="13:00Friday"/>

                <asp:Button runat="server" ID="13:55Monday"/>
                <asp:Button runat="server" ID="13:55Tuesday"/>
                <asp:Button runat="server" ID="13:55Wednesday"/>
                <asp:Button runat="server" ID="13:55Thursday"/>
                <asp:Button runat="server" ID="13:55Friday"/>

                <asp:Button runat="server" ID="14:50Monday"/>
                <asp:Button runat="server" ID="14:50Tuesday"/>
                <asp:Button runat="server" ID="14:50Wednesday"/>
                <asp:Button runat="server" ID="14:50Thursday"/>
                <asp:Button runat="server" ID="14:50Friday"/>--%>
               
            </asp:Panel>
            


            
            
    
    
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
