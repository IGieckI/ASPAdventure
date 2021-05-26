<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewCharacter.aspx.cs" Inherits="Elaborato.NewCharacter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="NewCharacter.css">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="New Character"></asp:Label>
        </div>
        <asp:Label ID="Label2" runat="server" Text="Character Name: "></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Description"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    </form>
</body>
</html>
