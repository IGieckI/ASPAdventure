<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Elaborato.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Login.css">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <div class="wrapper fadeInDown">
                <div id="formContent">
                    <h2 class="active"><a href="#">Sign In </a></h2>
                    <h2 class="underlineHover"><a href="Register.aspx">Sign Up </a></h2>

                    <div class="fadeIn first">
                        <img src="Images/GameIcon.png" id="icon" />
                    </div>

                    <asp:TextBox ID="username" runat="server" type="text" class="fadeIn second" name="login" placeholder="Username"></asp:TextBox>
                    <asp:TextBox ID="password" runat="server" type="password" class="fadeIn third" name="login" placeholder="Password"></asp:TextBox>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <br />
                            <asp:Label ID="lblErrore" runat="server" Text="Dati inseriti non corretti!" ForeColor="#CC3300"></asp:Label>
                            <br />
                            <asp:Button ID="btnLogIn" type="submit" class="fadeIn fourth" runat="server" Text="Log In" OnClick="btnLogIn_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div id="formFooter">
                        <a class="underlineHover" href="Recover.aspx">Forgot Password?</a>
                    </div>
                    <asp:HyperLink ID="hlDefault" runat="server" NavigateUrl="~/Default.aspx"></asp:HyperLink>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
