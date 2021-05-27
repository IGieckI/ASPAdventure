<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Response.aspx.cs" Inherits="Elaborato.RegisterOk" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Response.css">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="icon" runat="server" ImageUrl="Images/check.png" style="width:200px; left:43%; top:23%; position:absolute;" />
            <asp:Label ID="lblResponse" runat="server" Text="Registrazione avvenuta con successo!" class="active" style="left:41%; top:50%; position:absolute;"></asp:Label>
            <asp:Button ID="btnOk" type="submit"  runat="server" Text="Torna al menù principale" style="left:38%; top:55%; position:absolute;" OnClick="btnExit_Click"/>
        </div>
    </form>
</body>
</html>
