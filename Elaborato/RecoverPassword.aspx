﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs" Inherits="Elaborato.RecoverPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="RecoverPassword.css">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                <asp:Label ID="lblMsg" runat="server" Text="Inserisci la nuova password" style="position:absolute; left:42%; top:10%; width:300px;"></asp:Label>
                <br />
                <asp:TextBox ID="txtPassword" runat="server" name="login" type="password" placeholder="Password" style="position:absolute; left:40%; top:15%; width:300px;"></asp:TextBox>
            <br />
                <asp:TextBox ID="txtPassword2" runat="server" name="login" type="password" placeholder="Repete Password" style="position:absolute; left:40%; top:25%; width:300px;"></asp:TextBox>
            <br />
                <asp:Button ID="btnConferma" type="submit" runat="server" Text="Conferma" OnClick="btnConferma_Click" style="position:absolute; left:42%; top:35%;" />
        </div>
    </form>
</body>
</html>
