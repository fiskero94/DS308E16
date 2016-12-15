<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StudyPlatform.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="da">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta http-equiv="x-ua-compatible" content="ie=edge"/>
    <title>Login</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.5/css/bootstrap.min.css" integrity="sha384-AysaV+vQoT3kOAXZkl02PThvDr8HYKPZhNT5h/CXfBThSRXQ6jW5DO2ekP5ViFdi" crossorigin="anonymous">
    <link rel="stylesheet" href="~/Content/bootstrap-lumen.min.css"/>
</head>
<body>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js" integrity="sha384-3ceskX3iaEnIogmQchP8opvBy3Mi7Ce34nWjpBIwVTHfGYWQS9jwHDVRnpKKHJg7" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/tether/1.3.7/js/tether.min.js" integrity="sha384-XTs3FgkjiBgo8qjEjBk0tGmf3wPrWtA6coPfQDfFEY8AnYJwjalXCiosYRBIBZX8" crossorigin="anonymous"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.5/js/bootstrap.min.js" integrity="sha384-BLiI7JTZm+JWlgKa0M0kGRpJbF2J8q+qreVrKBC47e3K6BW78kGLrCkeRX6I9RoK" crossorigin="anonymous"></script>
<script src="https://use.fontawesome.com/d62dbf0888.js"></script>
<form id="LoginForm" runat="server">
    <div class="container">
        <div class="card card-block col-sm-8 offset-sm-2">
            <h4 class="display-4 text-center">Login</h4>
            <div class="form-group">
                <label class="control-label">Brugernavn</label>
                <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-user"></i></span>
                    <asp:TextBox runat="server" ID="UsernameTextBox" CssClass="form-control" placeholder="Brugernavn"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label">Adgangskode</label>
                <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                    <asp:TextBox runat="server" ID="PasswordTextBox" CssClass="form-control" placeholder="Adgangskode" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div class="form-group pull-right">
                <asp:Label runat="server" ID="ResponseLabel" CssClass="text-danger"></asp:Label>
                <asp:LinkButton runat="server" ID="LoginButton" CssClass="btn btn-primary" OnClick="LoginButton_OnClick">
                    <i class="fa fa-sign-in"></i> Log på
                </asp:LinkButton>
            </div>
        </div>
    </div>
</form>
</body>
</html>