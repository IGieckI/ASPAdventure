<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Elaborato.aspx.cs" Inherits="AspAdventure.Register" %>

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
                    <input type="text" id="username" name="login" placeholder="Username">
                    <h3>Email</h3>
                    <input type="text" id="email" name="login" placeholder="Email">
                    <h3>Password</h3>
                    <input type="password" id="password" name="login" placeholder="Password">
                    <asp:Button ID="btnRegister" type="submit" runat="server" Text="Register" OnClick="btnRegister_Click" />
                </div>
        </div>
    </form>
</body>
</html>
