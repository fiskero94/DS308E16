<%@ Page Title="Skema" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeBehind="skema.aspx.cs" Inherits="StudyPlatform.Skema" %>
<%@ MasterType virtualpath="~/layout.master" %>






<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    
 <script type="text/javascript">
    function openModal() {
        $('#myModal').modal('show');
    }
</script>
    

</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="body" runat="server">
    


        <div class="container col-sm-6">

    <asp:Table runat="server" ID="scheduleTable" CssClass="table table-bordered">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell runat="server"></asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Mandag</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Tirsdag</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Onsdag</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Torsdag</asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">Fredag</asp:TableHeaderCell>  
    </asp:TableHeaderRow>
        <asp:TableRow runat="server" ID="tableRow1" CssClass="table">
            <asp:TableCell runat="server">8.10</asp:TableCell>


        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow2" CssClass="table">
            <asp:TableCell runat="server">8.55</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow3" CssClass="table">
            <asp:TableCell runat="server">Pause</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow4" CssClass="table">
            <asp:TableCell runat="server">9.05</asp:TableCell>
            
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow5" CssClass="table">
            <asp:TableCell runat="server">9.50</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow6" CssClass="table">
            <asp:TableCell runat="server">Pause</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow7" CssClass="table">
            <asp:TableCell runat="server">10:00</asp:TableCell>
            

        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow8" CssClass="table">
            <asp:TableCell runat="server">10:45</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow9" CssClass="table">
            <asp:TableCell runat="server">Pause</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow10" CssClass="table">
            <asp:TableCell runat="server">10:55</asp:TableCell>
            

        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow11" CssClass="table">
            <asp:TableCell runat="server">11:40</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow12" CssClass="table">
            <asp:TableCell runat="server">Pause</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow13" CssClass="table">
            <asp:TableCell runat="server">12:05</asp:TableCell>
            

        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow14" CssClass="table">
            <asp:TableCell runat="server">12:50</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow15" CssClass="table">
            <asp:TableCell runat="server">Pause</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow16" CssClass="table">
            <asp:TableCell runat="server">13:00</asp:TableCell>
            

        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow17" CssClass="table">
            <asp:TableCell runat="server">13:45</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow18" CssClass="table">
            <asp:TableCell runat="server">Pause</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow19" CssClass="table">
            <asp:TableCell runat="server">13:55</asp:TableCell>
            

        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow20" CssClass="table">
            <asp:TableCell runat="server">14:40</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow21" CssClass="table">
            <asp:TableCell runat="server">Pause</asp:TableCell>
        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow22" CssClass="table">
            <asp:TableCell runat="server">14:50</asp:TableCell>
            

        </asp:TableRow>
    
        <asp:TableRow runat="server" ID="tableRow23" CssClass="table">
            <asp:TableCell runat="server">15:35</asp:TableCell>
        </asp:TableRow>
    

    </asp:Table>
    </div>
    


    
    
    
    
    
    

    
    

    

</asp:Content>
