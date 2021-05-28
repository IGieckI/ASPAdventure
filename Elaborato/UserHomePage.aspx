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
            <img src="Images/GameTitle.png" id="icon" style="position:absolute; top: -100px; left: -20px;" />
            <asp:GridView ID="grdCharacters" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" style="position:absolute; top: 100px; left: 14px; width: 1340px;" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanging="grdCharacters_SelectedIndexChanging">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField FooterText="Name" HeaderText="Name" DataField="Name" />
                    <asp:BoundField FooterText="Level" HeaderText="Level" DataField="Level" />
                    <asp:BoundField FooterText="Money" HeaderText="Money" DataField="Money" />
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

            <!-- <asp:TextBox ID="name" runat="server" type="text" name="login" placeholder="Character Name" style="position:absolute; top:10%; left: 83%; width:280px;"></asp:TextBox>
            <asp:TextBox ID="description" runat="server" type="text" name="login" placeholder="Description" style="position:absolute; top:15%; left: 83%; width:280px; height:120px;" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnNewCharacter" type="submit"  runat="server" Text="New Character" style="position:absolute; top:30%; left: 82%; width:280px;" OnClick="btnNewCharacter_Click"/> -->
        </div>
        <asp:Button ID="btnExit" type="submit"  runat="server" Text="Esci" style="position:absolute; top:90%; left: 88%;" OnClick="btnExit_Click" />
    </form>
</body>
</html>
