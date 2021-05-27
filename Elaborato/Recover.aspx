<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recover.aspx.cs" Inherits="Elaborato.Recover" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Recover.css">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="formContent">
                <br />
                <asp:Label ID="lblMsg" runat="server" Text="Inserisci la tua mail"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" type="text" name="login"></asp:TextBox>
                <asp:Label ID="lblErrore" runat="server" Text=""></asp:Label>
                <asp:Button ID="btnConferma" type="submit" runat="server" Text="Conferma" OnClick="btnConferma_Click"/>
            </div>
        </div>
    </form>
</body>
</html>
