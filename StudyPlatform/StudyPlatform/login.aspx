<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StudyPlatform.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="da">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta http-equiv="x-ua-compatible" content="ie=edge"/>
    <title>Login</title>
    <link rel="stylesheet" href="~/Content/bootstrap.min.css"/>
    <link rel="stylesheet" href="~/Content/bootstrap-lumen.min.css"/>
</head>
<body>
<form id="LoginForm" runat="server">
    <div class="container-fluid">
        <div class="card card-block col-sm-4 offset-sm-4">
            <div class="form-group">
                <h4>Login</h4>
            </div>
            <div class="form-group">
                <asp:TextBox runat="server" ID="UsernameTextBox" CssClass="form-control" placeholder="Brugernavn"></asp:TextBox>
            </div>
            <div class="form-group">
                
                <asp:TextBox runat="server" ID="PasswordTextBox" CssClass="form-control" placeholder="Adgangskode" TextMode="Password"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button runat="server" ID="LoginButton" CssClass="btn btn-primary" Text="LOG PÅ" OnClick="LoginButton_OnClick"/>
                <asp:Label runat="server" ID="ResponseLabel" CssClass="text-danger"></asp:Label>
            </div>
        </div>
    </div>
</form>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js" integrity="sha384-3ceskX3iaEnIogmQchP8opvBy3Mi7Ce34nWjpBIwVTHfGYWQS9jwHDVRnpKKHJg7" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/tether/1.3.7/js/tether.min.js" integrity="sha384-XTs3FgkjiBgo8qjEjBk0tGmf3wPrWtA6coPfQDfFEY8AnYJwjalXCiosYRBIBZX8" crossorigin="anonymous"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.5/js/bootstrap.min.js" integrity="sha384-BLiI7JTZm+JWlgKa0M0kGRpJbF2J8q+qreVrKBC47e3K6BW78kGLrCkeRX6I9RoK" crossorigin="anonymous"></script>
</body>
</html>