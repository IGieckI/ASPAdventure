<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AspAdventure.Default" Debug="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Nome Gioco</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />

        <asp:UpdatePanel ID="PanelChoseFile" runat="server" style="width:100%; z-index:10; position:absolute;" Visible="false">
            <ContentTemplate>
                <asp:FileUpload ID="FileUploadGame" runat="server" style="position:absolute; top:100px; left: 325px;" />
                <asp:Button ID="BtnStartGame" runat="server" Text="Start Game" style="position:absolute; top:150px; left: 325px;" OnClick="BtnStartGame_Click" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnStartGame" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="PanelDialogue" runat="server" style="width:100%; position:absolute; z-index:4;">
            <ContentTemplate>
                <asp:ListBox ID="lstDialogue" runat="server" AutoPostBack="True" style="z-index:4;" OnSelectedIndexChanged="lstDialogue_SelectedIndexChanged"></asp:ListBox>              
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="PanelBackground" runat="server" style="width:100%; height:50px; z-index:1; position:absolute; top:0px; left:0px;">
                <ContentTemplate>
                    <asp:PlaceHolder ID="plhNpcs" runat="server" ></asp:PlaceHolder>
                    <asp:PlaceHolder ID="plhItems" runat="server" ></asp:PlaceHolder>
                    <asp:PlaceHolder ID="plhDialogueSprite" runat="server"></asp:PlaceHolder>  
                    <asp:Image ID="ImageBackground"     runat="server" ImageUrl="https://www.chimerarevo.com/wp-content/uploads/2020/06/Sfondi-4K-PC.jpg" height="630" width="1260"/><!--26:9(730) -> 16:9 -->
                    <asp:ImageButton ID="btnUpArrow"    runat="server" style="position:absolute; top: 0px;   left: 593px; background-color: transparent; width: 70px; z-index:4;" BorderStyle="None"  OnClick="btnUpArrow_Click" />
                    <asp:ImageButton ID="btnDownArrow"  runat="server" style="position:absolute; top: 555px; left: 593px; background-color: transparent; width: 70px; z-index:4;" BorderStyle="None"  OnClick="btnDownArrow_Click"/>
                    <asp:ImageButton ID="btnLeftArrow"  runat="server" style="position:absolute; top: 263px; left: 0px;  background-color: transparent; height: 70px; z-index:4;" BorderStyle="None" OnClick="btnLeftArrow_Click"/>
                    <asp:ImageButton ID="btnRightArrow" runat="server" style="position:absolute; top: 263px; left: 1190px; background-color: transparent; height: 70px; z-index:4;" BorderStyle="None" OnClick="btnRightArrow_Click"/>  
                    <asp:Label ID="lblZoneName" runat="server" style="position:absolute;  left: 10px;" Font-Names="Calibri" ForeColor="#DFDFDF" Font-Size="20" Font-Bold="True"></asp:Label>
                </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="PanelButtons" runat="server" Visible="true" style="width:100%; z-index:2; position:absolute;">
            <ContentTemplate>
                <asp:Button ID="btnInspect" runat="server" Text="Inspect" style="position:absolute; top: 620px; left: -10px; height: 95px; width: 315px;" Font-Size="40px" OnClick="btnInspect_Click"/>
                <asp:Button ID="btnExit"    runat="server" Text="Exit"    style="position:absolute; top: 810px; left: -10px; height: 95px; width: 315px;" Font-Size="40px" OnClick="btnExit_Click"/>
                <asp:Button ID="btnEquip"   runat="server" Text="Equip"   style="position:absolute; top: 715px; left: -10px; height: 95px; width: 315px;" Font-Size="40px" OnClick="btnEquip_Click"/>
                <asp:Button ID="btnUse"     runat="server" Text="Use"     style="position:absolute; top: 620px; left: 305px; height: 95px; width: 315px;" Font-Size="40px" OnClick="btnUse_Click"/>
                <asp:Button ID="btnThrow"   runat="server" Text="Throw"   style="position:absolute; top: 715px; left: 305px; height: 95px; width: 315px;" Font-Size="40px" OnClick="btnThrow_Click"/>
                <asp:Button ID="btnSave"    runat="server" Text="Save"    style="position:absolute; top: 810px; left: 305px; height: 95px; width: 315px;" Font-Size="40px" OnClick="btnSave_Click" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="PanelGUI" runat="server" style="width:100%; z-index:2; position:absolute;">
            <ContentTemplate>
                <asp:ListBox ID="lstInventory" runat="server"                                 style="position:absolute; top: 620px; left: 620px; height: 285px; width: 632px;" Font-Size="30px"  Font-Names="Calibri" OnSelectedIndexChanged="lstInventory_SelectedIndexChanged" AutoPostBack ="true" Visible="True"></asp:ListBox>
                <asp:TextBox ID="txtDescription" runat="server"                               style="position:absolute; top:337px; left: 1253px; height: 560px; width: 662px; resize:none;" TextMode="MultiLine" ReadOnly="True" Font-Size="30px"></asp:TextBox>
                <asp:Image   ID="imageMinimap" runat="server"                                 style="position:absolute; top:-10px; left: 1252px; height: 337px; width: 337px;" />
                <asp:Label   ID="lblPlayerStats" runat="server" Text="Player Stats"           style="position:absolute; top:-10px; left: 1588px;" Font-Names="Calibri" ForeColor="Black" Font-Size="40px"></asp:Label>
                <asp:Label   ID="lblPlayerLevel" runat="server" Text="Lvl: 0 (0/0xp)"         style="position:absolute; top:40px; left: 1588px;" Font-Names="Calibri" ForeColor="Black" Font-Size="27px"></asp:Label>
                <asp:Label   ID="lblPlayerHp" runat="server" Text="Hp: 0"                     style="position:absolute; top:75px; left: 1588px;" Font-Names="Calibri" ForeColor="Black" Font-Size="27px"></asp:Label>
                <asp:Label   ID="lblPlayerMana" runat="server" Text="Mana: 0"                 style="position:absolute; top:110px; left: 1588px;" Font-Names="Calibri" ForeColor="Black" Font-Size="27px"></asp:Label>
                <asp:Label   ID="lblPlayerAttack" runat="server" Text="Attack: 0"             style="position:absolute; top:145px; left: 1588px;" Font-Names="Calibri" ForeColor="Black" Font-Size="27px"></asp:Label>
                <asp:Label   ID="lblPlayerAttackSpeed" runat="server" Text="Attack Speed: 0"  style="position:absolute; top:180px; left: 1588px;" Font-Names="Calibri" ForeColor="Black" Font-Size="27px"></asp:Label>
                <asp:Label   ID="lblPlayerElusiveness" runat="server" Text="Elusiveness: 0"   style="position:absolute; top:215px; left: 1588px;" Font-Names="Calibri" ForeColor="Black" Font-Size="27px"></asp:Label>
                <asp:Label   ID="lblPlayerAffinity" runat="server" Text="Affinity: 0"         style="position:absolute; top:250px; left: 1588px;" Font-Names="Calibri" ForeColor="Black" Font-Size="27px"></asp:Label>
                <asp:Label   ID="lblPlayerIntelligence" runat="server" Text="Intelligence: 0" style="position:absolute; top:285px; left: 1588px;" Font-Names="Calibri" ForeColor="Black" Font-Size="27px"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="PanelFight" runat="server" style="width:100%; position:absolute; z-index:5;">
            <ContentTemplate>
                <asp:PlaceHolder ID="plhEnemy1" runat="server"></asp:PlaceHolder>
                <asp:PlaceHolder ID="plhEnemy2" runat="server"></asp:PlaceHolder>
                <asp:PlaceHolder ID="plhEnemy3" runat="server"></asp:PlaceHolder>
                <asp:PlaceHolder ID="plhEnemy4" runat="server"></asp:PlaceHolder>
                <asp:PlaceHolder ID="plhEnemy5" runat="server"></asp:PlaceHolder>
                <asp:PlaceHolder ID="plhFight" runat="server"></asp:PlaceHolder>
                <asp:PlaceHolder ID="plhSpells" runat="server"></asp:PlaceHolder>
                <asp:PlaceHolder ID="plhInventory" runat="server"></asp:PlaceHolder>
                <asp:PlaceHolder ID="plhRun" runat="server"></asp:PlaceHolder>
                <asp:ListBox ID="lstFight" Visible="false" runat="server" style="position:absolute; top: 620px; left: 620px; height: 285px; width: 632px;" Font-Size="30px"  Font-Names="Calibri" OnSelectedIndexChanged="lstFight_SelectedIndexChanged" AutoPostBack ="true"></asp:ListBox>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="PanelDataButtons" runat="server" style="width:100%; z-index:3; position:absolute; visibility:hidden">
            <ContentTemplate>
                 <asp:Button ID="Save"   runat="server" Text="SAVE" style="position:absolute; top:-10px; left: 722px; height: 63px; width: 35px; font-size: 10px;" />
                 <asp:Button ID="Load"   runat="server" Text="LOAD" style="position:absolute; top:53px; left: 722px; height: 63px; width: 35px; font-size: 10px;" />
            </ContentTemplate>            
        </asp:UpdatePanel>
    </form>
</body>
</html>
