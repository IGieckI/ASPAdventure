<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Elaborato.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Register.css">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                <div id="formContent">
                    <h2 class="underlineHover"><a  href="Login.aspx">Sign In </a></h2>
                    <h2 class="active"><a  href="#">Sign Up </a></h2>
                    <h3 >Username</h3>
                    <asp:TextBox ID="username" runat="server" type="text" name="login" placeholder="Username"></asp:TextBox>
                    <h3>Email</h3>
                    <asp:TextBox ID="email" runat="server" type="text" name="login" placeholder="Email"></asp:TextBox>
                    <h3>Password</h3>
                    <asp:TextBox ID="password" runat="server" type="password" name="login" placeholder="Password"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Text="Dati inseriti non corretti!" ForeColor="#CC3300"></asp:Label>
                    <br />
                    <asp:Button ID="btnRegister" type="submit" runat="server" Text="Register" OnClick="btnRegister_Click" />
                </div>
        </div>
    </form>
</body>
</html>
