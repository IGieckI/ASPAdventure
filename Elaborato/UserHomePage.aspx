<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserHomePage.aspx.cs" Inherits="Elaborato.UserHomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="UserHomePage.css">
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
            <img src="Immagini/GameTitle.png" id="icon" style="position:absolute; top: 0px; left: 0px;" />
            <asp:GridView ID="grdCharacters" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" style="position:absolute; top: 100px; left: 14px; width: 1340px;" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanging="grdCharacters_SelectedIndexChanging">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField FooterText="Nome" HeaderText="Nome" DataField="Name" />
                    <asp:BoundField FooterText="Livello" HeaderText="Livello" DataField="Level" />
                    <asp:BoundField FooterText="Soldi" HeaderText="Soldi" DataField="Money" />
                    <asp:BoundField FooterText="Descrizione" HeaderText="Description" DataField="Description" />
                </Columns>
                        <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
            <asp:Label ID="lblErrore" runat="server" Text="" ForeColor="#CC3300"></asp:Label>
        </div>
        <asp:Button ID="btnExit" type="submit"  runat="server" Text="Esci" style="position:absolute; top:-50; left: 1500px;" OnClick="btnExit_Click" />
    </form>
</body>
</html>
