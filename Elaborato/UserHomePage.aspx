<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserHomePage.aspx.cs" Inherits="Elaborato.UserHomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #icon {
            height: 402px;
            width: 727px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img src="Immagini/GameTitle.png" id="icon" style="position:absolute; top: -100px;" />
            <asp:GridView ID="grdCharacters" runat="server">
                        </asp:GridView>
            <asp:Label ID="lblErrore" runat="server" Text="" ForeColor="#CC3300"></asp:Label>
        </div>
    </form>
</body>
</html>
