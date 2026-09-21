<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="PLTaskPlannerWeb.Register" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Registrieren</title>
    <link rel="stylesheet" href="Active.css" />
</head>

<body class="login-body">

<form id="form1" runat="server">

    <h1 class="login-h1">Registrieren</h1>

    <div class="login-container">

        <asp:TextBox ID="txtUsername" runat="server" 
            CssClass="input" placeholder="Benutzername">
        </asp:TextBox>

        <asp:TextBox ID="txtPassword" runat="server"
            TextMode="Password"
            CssClass="input" placeholder="Passwort">
        </asp:TextBox>

        <asp:Button ID="btnRegister"
            runat="server"
            Text="Registrieren"
            CssClass="button"
            OnClick="btnRegister_Click" />

        <asp:Label ID="lblMessage"
            runat="server"
            CssClass="message">
        </asp:Label>

    </div>

    <div class="login-link">
        <asp:Label ID="lblLink"
            runat="server"
            text="Bereits registriert?"
            CssClass="login-label">
        </asp:Label>
        
        <a href="Login.aspx">Zum Login</a>
    </div>

</form>

</body>
</html>