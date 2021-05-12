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
                <h2>Inserisci la tua email</h2>
                <input type="text" id="email" name="login">
                <asp:Button ID="btnConferma" type="submit" runat="server" Text="Conferma" OnClick="btnConferma_Click"/>
            </div>
        </div>
    </form>
</body>
</html>
