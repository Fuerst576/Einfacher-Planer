<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PLTaskPlannerWeb.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">

    <title>Login</title>

    <link rel="stylesheet" href="Active.css" />

</head>

<body class="login-body">

<form id="form1" runat="server">

    <h1 class="login-h1">Login</h1>

    <div class="login-container">

        <asp:TextBox ID="txtUsername" runat="server" 
            CssClass="input" placeholder="Benutzername">
        </asp:TextBox>

        <asp:TextBox ID="txtPassword" runat="server"
            TextMode="Password"
            CssClass="input" placeholder="Passwort">
        </asp:TextBox>

        <asp:Button ID="btnLogin"
            runat="server"
            Text="Einloggen"
            CssClass="button"
            OnClick="btnLogin_Click" />

                        

        <asp:Label ID="lblMessage"
            runat="server"
            CssClass="message">
        </asp:Label>

    </div>

    <div class="login-link">

        <asp:Label ID="lblLink"
            runat="server" text="Noch kein Konto?"
            CssClass="login-label">
        </asp:Label>

        <a href="Registration.aspx">Registrieren</a>

        </div>

</form>

</body>
</html>