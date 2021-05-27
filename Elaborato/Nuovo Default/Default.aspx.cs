using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspAdventureLibrary;
using System.Drawing;
using System.Threading;
using Elaborato;

namespace AspAdventure
{
    public partial class Default : System.Web.UI.Page
    {
        static string NomePCDB = "DESKTOP-CDHTOA2";

        bool FileChosen
        {
            get
            {
                return (bool)Session["FileChosen"];
            }

            set
            {
                Session["FileChosen"] = value;
            }
        }
        bool OnDialogue
        {
            get
            {
                return (bool)Session["OnDialogue"];
            }

            set
            {
                Session["OnDialogue"] = value;
            }
        }
        bool OnDeal
        {
            get
            {
                return (bool)Session["OnDeal"];
            }

            set
            {
                Session["OnDeal"] = value;
            }
        }
        bool OnItemDescriptionDisplay
        {
            get
            {
                return (bool)Session["OnItemDescriptionDisplay"];
            }

            set
            {
                Session["OnItemDescriptionDisplay"] = value;
            }
        }
        bool OnFight
        {
            get
            {
                return (bool)Session["OnFight"];
            }

            set
            {
                Session["OnFight"] = value;
            }
        }
        int OnThrow
        {
            get
            {
                return (int)Session["OnThrow"];
            }

            set
            {
                Session["OnThrow"] = value;
            }
        }
        int OnEquip
        {
            get
            {
                return (int)Session["OnEquip"];
            }

            set
            {
                Session["OnEquip"] = value;
            }
        }
        int OnUse
        {
            get
            {
                return (int)Session["OnUse"];
            }

            set
            {
                Session["OnUse"] = value;
            }
        }
        int DialogueID
        {
            get
            {
                return (int)Session["DialogueID"];
            }

            set
            {
                Session["DialogueID"] = value;
            }
        }
        int DealerDialogueID
        {
            get
            {
                return (int)Session["DealerDialogueID"];
            }

            set
            {
                Session["DealerDialogueID"] = value;
            }
        }
        string ClientID
        {
            get
            {
                return Session["ClientID"] as string;
            }

            set
            {
                Session["ClientID"] = value;
            }
        }
        string FileName
        {
            get
            {
                return Session["FileName"] as string;
            }

            set
            {
                Session["FileName"] = value;
            }
        }
        string FightState
        {
            get
            {
                return Session["FightState"] as string;
            }

            set
            {
                Session["FightState"] = value;
            }
        }
        string FightResult
        {
            get
            {
                return Session["FightResult"] as string;
            }

            set
            {
                Session["FightResult"] = value;
            }
        }
        List<object> RestoreGame
        {
            get
            {
                return Session["RestoreGame"] as List<object>;
            }

            set
            {
                Session["RestoreGame"] = value;
            }
        }
        EnemyKeyNPC Enemy
        {
            get
            {
                return Session["EnemyKeyNPC"] as EnemyKeyNPC;
            }

            set
            {
                Session["EnemyKeyNPC"] = value;
            }
        }
        Dealer Dealer
        {
            get
            {
                return Session["Dealer"] as Dealer;
            }

            set
            {
                Session["Dealer"] = value;
            }
        }
        Game Game
        {
            get
            {
                return Session["Game"] as Game;
            }

            set
            {
                Session["Game"] = value;
            }
        }
        Dialogue Dialogue
        {
            get
            {
                return Session["Dialogue"] as Dialogue;
            }

            set
            {
                Session["Dialogue"] = value;
            }
        }
        List<ImageButton> DynamicNpcs
        {
            get
            {
                return Session["DynamicNpcs"] as List<ImageButton>;
            }

            set
            {
                Session["DynamicNpcs"] = value;
            }
        }
        List<ImageButton> DynamicItems
        {
            get
            {
                return Session["DynamicItems"] as List<ImageButton>;
            }

            set
            {
                Session["DynamicItems"] = value;
            }
        }
        List<Button> DynamicFighButtons
        {
            get
            {
                return Session["DynamicFighButtons"] as List<Button>;
            }

            set
            {
                Session["DynamicFighButtons"] = value;
            }
        }
        ListBox DynamicDialoguesList
        {
            get
            {
                return Session["DynamicDialoguesButtons"] as ListBox;
            }

            set
            {
                Session["DynamicDialoguesButtons"] = value;
            }
        }
        List<ItemTuple> PersonalShop
        {
            get
            {
                return Session["PersonalShop"] as List<ItemTuple>;
            }

            set
            {
                Session["PersonalShop"] = value;
            }
        }
        /*
        static bool FileChosen;
        static bool OnDialogue;
        static bool OnDealCopy;
        static bool OnItemDescriptionDisplay;
        static bool OnFight;
        static bool OnDeal;
        static int OnThrow;
        static int OnEquip;
        static int OnUse;
        static int DialogueID;
        static int DealerDialogueID;
        static string ClientID;
        static string FileName;
        static string FightState;
        static string FightResult;
        static EnemyKeyNPC Enemy;
        static Dealer Dealer;
        static Game Game;
        static Dialogue Dialogue;
        static List<ImageButton> DynamicNpcs;
        static List<ImageButton> DynamicItems;
        static List<Button> DynamicFighButtons;
        static ListBox DynamicDialoguesList;
        static List<ItemTuple> PersonalShop;
        */

        //Sessions
        //Game: Represent the game itself
        //OnDialogue: Define if the player is in a dialogue state
        //Dialogue: Rapresent the dialogue with an NPC
        //ClientID: Is the name of the npc that triggered a dialogue

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (!IsPostBack)
            {
                //Create the lstbox that contains answers
                FileName = "AspAdventure";
                ListBox listBox = new ListBox();
                listBox.ID = "lstDialoguesAnswers";
                listBox.Attributes["runat"] = "server";
                listBox.Attributes["style"] = "position:absolute; top: 620pxpx; left: -10px; height: 285px; width: 630px; font-size:30px; font-family: Calibri.ttf";
                listBox.Style.Add("z-index", "5");
                lstDialogue.SelectedIndexChanged += lstDialogue_SelectedIndexChanged;
                lstDialogue.Attributes["style"] = "position:absolute; top: 620px; left: -10px; height: 285px; width: 630px; font-size:30px; font-family: Calibri.ttf;";
                DynamicDialoguesList = listBox;
                DynamicDialoguesList.SelectedIndexChanged += lstDialogue_SelectedIndexChanged;
                DynamicDialoguesList.AutoPostBack = true;

                //inizialize Sessions
                DynamicNpcs = new List<ImageButton>();
                DynamicItems = new List<ImageButton>();
                DynamicFighButtons = new List<Button>();
                OnItemDescriptionDisplay = false;
                lstDialogue.Visible = false;
                OnDialogue = false;
                FileChosen = false;
                OnThrow = 0;
                OnEquip = 0;
                OnUse = 0;
                OnDeal = false;
                OnFight = false;
                FightResult = "";
                DialogueID = -1;
                DealerDialogueID = -1;

                /*Button btnRisposta1 = new Button();
                btnRisposta1.ID = "btnRisposta1";
                btnRisposta1.Attributes["runat"] = "server";
                btnRisposta1.Text = "Risposta 1";
                btnRisposta1.Attributes["style"] = "position:absolute; top: 630px; left: -2px; height: 142.5px; width: 315px;";
                btnRisposta1.Style.Add("z-index", "5");
                btnRisposta1.Attributes["Font-Size"] = "40px";
                DynamicDialoguesButtons.Add(btnRisposta1);
                Button btnRisposta2 = new Button();
                btnRisposta2.ID = "btnRisposta2";
                btnRisposta2.Attributes["runat"] = "server";
                btnRisposta2.Text = "Risposta 2";
                btnRisposta2.Attributes["style"] = "position:absolute; top: 630px; left: 313px; height: 142.5px; width: 315px; z-index:5;";
                btnRisposta2.Attributes["Font-Size"] = "40px";
                DynamicDialoguesButtons.Add(btnRisposta2);
                Button btnRisposta3 = new Button();
                btnRisposta3.ID = "btnRisposta3";
                btnRisposta3.Attributes["runat"] = "server";
                btnRisposta3.Text = "Risposta 3";
                btnRisposta3.Attributes["style"] = "position:absolute; top: 773px; left: -2px; height: 142.5px; width: 315px; z-index:5;";
                btnRisposta3.Attributes["Font-Size"] = "40px";
                DynamicDialoguesButtons.Add(btnRisposta3);
                Button btnRisposta4 = new Button();
                btnRisposta4.ID = "btnRisposta4";
                btnRisposta4.Attributes["runat"] = "server";
                btnRisposta4.Text = "Risposta 4";
                btnRisposta4.Attributes["style"] = "position:absolute; top: 773px; left: 313px; height: 142.5px; width: 315px; z-index:5;";
                btnRisposta4.Attributes["Font-Size"] = "80px";
                DynamicDialoguesButtons.Add(btnRisposta4);*/

                LoadTest();
            }
            else
            {
                try//when the user click too fast
                {
                    foreach (ImageButton b in DynamicItems)
                    {
                        for(int i=0;i<Game.Items.Count;i++)
                        {
                            if(Game.Items[i].Name == b.ClientID)
                            {
                                if (Game.Items[i].GetType() == typeof(Item))
                                    b.Click += ItemClick;
                                else if (Game.Items[i].GetType() == typeof(Container))
                                    b.Click += ContainerClick;
                                else if (Game.Items[i].GetType() == typeof(Portal))
                                    b.Click += PortalClick;
                                else if (Game.Items[i].GetType() == typeof(CurrencyItem))
                                    b.Click += CurrencyItemClick;
                                else if (Game.Items[i].GetType() == typeof(Weapon))
                                    b.Click += ItemClick;
                                else if (Game.Items[i].GetType() == typeof(Wearable))
                                    b.Click += ItemClick;
                                break;
                            }
                        }
                        plhItems.Controls.Add(b);
                    }

                    foreach (ImageButton b in DynamicNpcs)
                    {
                        b.Click += NpcClick;
                        plhNpcs.Controls.Add(b);
                    }

                    if(OnFight)
                    {
                        foreach(Button b in DynamicFighButtons)
                        {
                            if(b.Text=="Fight")
                            {
                                b.Click += btnFight_Click;
                                plhFight.Controls.Add(b);
                                continue;
                            }
                            if (b.Text == "Spells")
                            {
                                b.Click += btnSpells_Click;
                                plhSpells.Controls.Add(b);
                                continue;
                            }
                            if (b.Text == "Inventory")
                            {
                                b.Click += btnInventory_Click;
                                plhInventory.Controls.Add(b);
                                continue;
                            }
                            if (b.Text == "Run Away")
                            {
                                b.Click += btnRun_Click;
                                plhRun.Controls.Add(b);
                                continue;
                            }
                        }
                        txtDescription.Text = "";
                    }

                }
                catch
                {

                }

            }

            Title = FileName;

            if (OnEquip > 0)
                OnEquip--;
            else
                btnEquip.BackColor = Color.Empty;

            if (OnThrow > 0)
                OnThrow--;
            else
                btnThrow.BackColor = Color.Empty;

            //if (FileChosen)
            //{
            //    PanelChoseFile.Visible = false;
            //    PanelBackground.Visible = true;
            //    PanelButtons.Visible = true;
            //    PanelGUI.Visible = true;
            //    PanelDialogue.Visible = true;
            //    PanelDataButtons.Visible = true;
            //}
            //else
            //{
            //    PanelChoseFile.Visible = true;
            //    PanelBackground.Visible = false;
            //    PanelButtons.Visible = false;
            //    PanelGUI.Visible = false;
            //    PanelDialogue.Visible = true;
            //    PanelDataButtons.Visible = false;
            //}
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //if(FileChosen)
              UpdateMap(Game.Map.Zones[Game.Map.PlayerPos]);

            if (OnEquip==1)
            {
                btnEquip.BackColor = Color.Empty;
                OnEquip = 0;
            }
            if (OnThrow == 1)
            {
                btnThrow.BackColor = Color.Empty;
                OnThrow = 0;
            }

            if(OnFight)
                PanelFight.Visible = true;

        }

        //Movement Arrows
        protected void btnUpArrow_Click(object sender, ImageClickEventArgs e)
        {
            if (CheckLockedZone(Game.Map.Zones[Game.Map.Zones[Game.Map.PlayerPos].North]))
                return;
            Game.Map.Moving("up");
        }
        protected void btnRightArrow_Click(object sender, ImageClickEventArgs e)
        {
            if (CheckLockedZone(Game.Map.Zones[Game.Map.Zones[Game.Map.PlayerPos].Est]))
                return;
            Game.Map.Moving("right");
        }
        protected void btnDownArrow_Click(object sender, ImageClickEventArgs e)
        {
            if (CheckLockedZone(Game.Map.Zones[Game.Map.Zones[Game.Map.PlayerPos].South]))
                return;
            Game.Map.Moving("down");
        }
        protected void btnLeftArrow_Click(object sender, ImageClickEventArgs e)
        {
            if (CheckLockedZone(Game.Map.Zones[Game.Map.Zones[Game.Map.PlayerPos].West]))
                return;
            Game.Map.Moving("left");
        }

        //Action Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            /*XMLManager xMLManager = new XMLManager($"{Request.PhysicalApplicationPath}\\Game.xml", Server);
            xMLManager.Encode(Game);

            Response.ContentType = "text/xml";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Game.xml");
            Response.TransmitFile($"{Request.PhysicalApplicationPath}\\Game.xml");
            Response.End();*/
        }
        protected void btnUse_Click(object sender, EventArgs e)
        {

        }
        protected void btnTalkTo_Click(object sender, EventArgs e)
        {

        }
        protected void btnThrow_Click(object sender, EventArgs e)
        {
            if(OnThrow>0)
            {
                btnThrow.BackColor = Color.White;
                OnThrow = 0;
            }
            else
            {
                btnThrow.BackColor = Color.LightGreen;
                OnThrow = 2;
            }           
        }
        protected void btnEquip_Click(object sender, EventArgs e)
        {
            if (OnEquip > 0)
            {
                btnEquip.BackColor = Color.White;
                OnEquip = 0;
            }
            else
            {
                btnEquip.BackColor = Color.LightGreen;
                OnEquip = 2;
            }
        }
        protected void btnInspect_Click(object sender, EventArgs e)
        {
            //This just show the description of the zone, and all the code is on UpdateZone so there is nothing here just a call to the PostBack
        }
        protected void btnFight_Click(object sender, EventArgs e)
        {
            FightState = "Fight";
            lstFight.Items.Clear();

            if (Game.Player.Stats.AttackSpeed >= Enemy.Stats.AttackSpeed)
            {
                Enemy.Stats.HP -= (Game.Player.Stats.Attack);
                string str;
                str = $"Hai inflitto {Game.Player.Stats.Attack} danni\n\n";
                if (!(Game.Player.Weapon is null))
                {
                    Enemy.Stats.HP -= Game.Player.Weapon.AttackDamage;
                    str = $"Hai inflitto {Game.Player.Stats.Attack + Game.Player.Weapon.AttackDamage} danni\n\n";
                }
                FightResult += str;

                if (Enemy.Stats.HP < 0)
                    Enemy.Stats.HP = 0;

                if (Enemy.Stats.HP == 0)
                    return;

                EnemyAttack();
            }
            else
            {
                EnemyAttack();

                Enemy.Stats.HP -= (Game.Player.Stats.Attack);
                string str;
                str = $"Hai inflitto {Game.Player.Stats.Attack} danni\n\n";
                if (!(Game.Player.Weapon is null))
                {
                    Enemy.Stats.HP -= Game.Player.Weapon.AttackDamage;
                    str = $"Hai inflitto {Game.Player.Stats.Attack + Game.Player.Weapon.AttackDamage} danni\n\n";
                }
                FightResult += str;

                if (Enemy.Stats.HP < 0)
                    Enemy.Stats.HP = 0;
            }

            lstDialogue.ID = "lstFight";
        }
        protected void btnSpells_Click(object sender, EventArgs e)
        {
            FightState = "Spells";
            lstFight.Items.Clear();
            foreach (ItemTuple i in Game.Player.Items)
                if(i.Item.GetType() == typeof(Spell))
                    lstFight.Items.Add($"({((Spell)i.Item).ManaCost}){((Spell)i.Item).Name}");

            if (lstFight.Items.Count == 0)
                lstFight.Items.Add("Vuoto");
        }
        protected void btnInventory_Click(object sender, EventArgs e)
        {
            FightState = "Inventory";

            lstFight.Items.Clear();
            foreach (ItemTuple x in Game.Player.Items)
                if (x.Item.GetType() == typeof(Consumables))
                    lstFight.Items.Add(x.Quantity + "|" + x.Item.Name);

            if (lstFight.Items.Count==0)
                lstFight.Items.Add("Vuoto");
        }
        protected void btnRun_Click(object sender, EventArgs e)
        {
            lstFight.ID = "Run";
            lstFight.Items.Clear();
            Random rnd = new Random();
            if (rnd.Next(0, 101) > Enemy.EscapePerc)
            {
                OnFight = false;
                txtDescription.Text = "Sei scappato con successo!";
                OnItemDescriptionDisplay = true; 
                plhFight.Visible = false;
                plhSpells.Visible = false;
                plhInventory.Visible = false;
                plhRun.Visible = false;
                lstFight.Visible = false;
                OnFight = false;
                lstDialogue.ID = "lstFight";
            }
            else
            {
                FightResult += $"Non Sei riuscito a scappare\n\n";
                EnemyAttack();
            }
        }

        //Btn's Methods
        protected void BtnStartGame_Click(object sender, EventArgs e)
        {
        //    Thread.Sleep(300);

        //    XMLManager xMLManager = new XMLManager(Server);

        //    /*

        //        Mostra img

        //        FileUploadGame.SaveAs(Request.PhysicalApplicationPath + "\\BackgroundImg.bmp");
        //        System.Drawing.Image img = System.Drawing.Image.FromFile(Request.PhysicalApplicationPath + "\\bottle.png");
        //        MemoryStream ms = new MemoryStream();
        //        img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        //        byte[] bites = ms.ToArray();
        //        string base64String = Convert.ToBase64String(bites, 0, bites.Length);
        //        ImageBackground.ImageUrl = "data:image/jpg;base64," + base64String;
        //    */

        //    //ImageBackground.ImageUrl = "data:image;base64," + Convert.ToBase64String(roba);
        //    //byte[] img = (System.Drawing.Image)FileUploadGame.PostedFile


        //    /*FileUploadGame.SaveAs(Request.PhysicalApplicationPath + "\\Background.bmp");
        //    ImageBackground.ImageUrl = Request.PhysicalApplicationPath + "Background.bmp";
        //    ImageBackground.DataBind();
        //    */

        //    //Load game file uploaded by user
        //    if (!FileUploadGame.HasFile)
        //    {
        //        Response.Write("<script>alert('You must first select a file!');</script>");
        //        return;
        //    }
        //    else if (FileUploadGame.FileName.Split('.')[FileUploadGame.FileName.Split('.').Length - 1] != "xml")
        //    {
        //        Response.Write("<script>alert('The file format is wrong!');</script>");
        //        return;
        //    }

        //    try
        //    {
        //        String str = Server.MapPath("~/Game.xml");
        //        FileUploadGame.SaveAs(Server.MapPath("~/Game.xml"));
        //        Game = xMLManager.Decode(System.Web.HttpContext.Current.Server.MapPath("~/Game.xml"));
        //    }
        //    catch(Exception ex)
        //    {
        //        Response.Write("<script>alert('Error: Corrupted file!');</script>");
        //        return;
        //    }

        //    //turn true the variable that define if the file is chosen
        //    FileChosen = true;

        //    //Set up panels for the game
        //    PanelBackground.Visible = true;
        //    PanelButtons.Visible = true;
        //    PanelGUI.Visible = true;
        //    PanelDataButtons.Visible = true;
        //    PanelChoseFile.Visible = false;

        //    if(Game.Player is null)
        //    {
        //        Game.Player = new Player();
        //    }

        //    //Set up the player things
        //    if (Game.Player.Stats is null)
        //    {
        //        lblPlayerHp.Text = "Hp: ?";
        //        lblPlayerMana.Text = "Mana: ?";
        //        lblPlayerAttack.Text = "Attack: ?";
        //        lblPlayerAttackSpeed.Text = "Attack Speed: ?";
        //        lblPlayerElusiveness.Text = "Elusiveness: ?";
        //        lblPlayerAffinity.Text = "Affinity: ?";
        //        lblPlayerIntelligence.Text = "Intelligence: ?";
        //    }
        //    else
        //    {
        //        //Player stats
        //        lblPlayerStats.Text = $"Player Stats({Game.Player.Money}฿)";
        //        lblPlayerLevel.Text = $"Lvl: {Game.Player.Level} ({Game.Player.Exp}/{Game.Player.LevelCap}xp)";
        //        lblPlayerHp.Text = $"Hp:{Game.Player.Stats.HP}/{Game.Player.Stats.MaxHP}";
        //        lblPlayerMana.Text = $"Mana:{Game.Player.Stats.Mana}/{Game.Player.Stats.MaxMana}";
        //        lblPlayerAttack.Text = "Attack: " + Game.Player.Stats.Attack;
        //        lblPlayerAttackSpeed.Text = "Attack Speed: " + Game.Player.Stats.AttackSpeed;
        //        lblPlayerElusiveness.Text = "Elusiveness: " + Game.Player.Stats.Elusiveness;
        //        lblPlayerAffinity.Text = "Affinity: " + Game.Player.Stats.Affinity;
        //        lblPlayerIntelligence.Text = "Intelligence: " + Game.Player.Stats.Intelligence;
        //    }

        //    if(!(Game.Player.Items is null))
        //    {
        //        //player inventory
        //        lstInventory.DataSource = Game.Player.Items;
        //        lstInventory.DataBind();
        //    }

        //    FileName = FileUploadGame.FileName;

        //    /* Altri test */
        }
        protected void NpcClick(object sender, ImageClickEventArgs e)
        {
            ClientID = ((ImageButton)sender).ID;

            for(int i=0;i< Game.Map.Zones[Game.Map.PlayerPos].Peoples.Count;i++)
            {
                if (Game.Map.Zones[Game.Map.PlayerPos].Peoples[i].Name == ClientID)
                {
                    if (Game.Map.Zones[Game.Map.PlayerPos].Peoples[i].GetType() == typeof(EnemyKeyNPC))
                    {
                        //set up a point of restore

                        //XMLManager xMLManager = new XMLManager(HttpContext.Current.Server.MapPath(@"~/Restore.xml"), Server);
                        //xMLManager.Encode(Game);

                        /*RestoreGame = new List<object>();
                        RestoreGame.Add(FileChosen);
                        RestoreGame.Add(OnDialogue);
                        RestoreGame.Add(OnDeal);
                        RestoreGame.Add(OnItemDescriptionDisplay);
                        RestoreGame.Add(OnFight);
                        RestoreGame.Add(OnThrow);
                        RestoreGame.Add(OnEquip);
                        RestoreGame.Add(OnUse);
                        RestoreGame.Add(DialogueID);
                        RestoreGame.Add(DealerDialogueID);
                        RestoreGame.Add(ClientID);
                        RestoreGame.Add(FileName);
                        RestoreGame.Add(FightState);
                        RestoreGame.Add(FightResult);
                        RestoreGame.Add(Enemy);
                        RestoreGame.Add(Dealer);
                        RestoreGame.Add(Game);
                        RestoreGame.Add(Dialogue);
                        RestoreGame.Add(DynamicNpcs);
                        RestoreGame.Add(DynamicItems);
                        RestoreGame.Add(DynamicFighButtons);
                        RestoreGame.Add(DynamicDialoguesList);
                        RestoreGame.Add(PersonalShop);*/

                        /*FileChosenCopy = FileChosen;
                        OnDialogueCopy = OnDialogue;
                        OnDealCopy = OnDeal;
                        OnItemDescriptionDisplayCopy = OnItemDescriptionDisplay;
                        OnFightCopy = OnFight;
                        OnThrowCopy = OnThrow;
                        OnEquipCopy = OnEquip;
                        OnUseCopy = OnUse;
                        DialogueIDCopy = DialogueID;
                        DealerDialogueIDCopy = DealerDialogueID;
                        ClientIDCopy = ClientID;
                        FileNameCopy = FileName;
                        FightStateCopy = FightState;
                        FightResultCopy = FightResult;
                        EnemyCopy = Enemy;
                        DealerCopy = Dealer;
                        GameCopy = Game;
                        DialogueCopy = Dialogue;
                        DynamicNpcsCopy = DynamicNpcs;
                        DynamicItemsCopy = DynamicItems;
                        DynamicFighButtonsCopy = DynamicFighButtons;
                        DynamicDialoguesListCopy = DynamicDialoguesList;
                        PersonalShopCopy = PersonalShop;*/

                        Enemy = (EnemyKeyNPC)Game.Map.Zones[Game.Map.PlayerPos].Peoples[i];
                        Enemy.Stats.HP = Enemy.Stats.MaxHP;
                        Enemy.Stats.Mana = Enemy.Stats.MaxMana;
                        OnFight = true;
                        plhFight.Visible = true;
                        plhSpells.Visible = true;
                        plhInventory.Visible = true;
                        plhRun.Visible = true;
                        lstFight.Visible = true;
                        PanelFight.Visible = true;

                        System.Web.UI.WebControls.Image imgEnemy = new System.Web.UI.WebControls.Image();
                        imgEnemy.ID = "imgEnemy";
                        imgEnemy.Attributes["runat"] = "server";
                        imgEnemy.Attributes["style"] = $"position:absolute; top: 330px; left: 530px; height:300px; width:auto;";
                        string base64String = "";
                        if(Enemy.FightSprite is null)
                            base64String =  ImageToBase64(LocalFileToImage(Server.MapPath(Enemy.OverWorldSprite)));
                        else
                            base64String = ImageToBase64(LocalFileToImage(Server.MapPath(Enemy.FightSprite)));
                        imgEnemy.ImageUrl = "data:image/gif;base64," + base64String;
                        plhEnemy1.Controls.Add(imgEnemy);
                        txtDescription.Text = $"{Enemy.Name}:\nHp:{Enemy.Stats.HP}/{Enemy.Stats.MaxHP}\nMana:{Enemy.Stats.Mana}/{Enemy.Stats.MaxMana}";
                        OnItemDescriptionDisplay = true;
                        return;
                    }
                    else if (Game.Map.Zones[Game.Map.PlayerPos].Peoples[i].GetType() == typeof(Dealer))
                    {
                        Dealer = (Dealer)Game.Map.Zones[Game.Map.PlayerPos].Peoples[i];
                        OnDeal = true;
                        return;
                    }
                }
            }

            foreach (NPC x in Game.Map.Zones[Game.Map.PlayerPos].Peoples)
                if (x.Name == ClientID)
                {
                    Dialogue d = x.Dialogue;
                    Dialogue = d;
                    DialogueID = d.ActualSentenceID;
                    break;
                }
            OnDialogue = true;
            lstDialogue.Visible = true;
        }
        protected void ItemClick(object sender, ImageClickEventArgs e)
        {
            ClientID = ((ImageButton)sender).ID;
            for (int i = 0; i < Game.Map.Zones[Game.Map.PlayerPos].Items.Count; i++)
            {
                if (Game.Map.Zones[Game.Map.PlayerPos].Items[i].Name == ClientID)
                {
                    AddItem(Game.Map.Zones[Game.Map.PlayerPos].Items[i]);

                    List<Item> newItemList = new List<Item>();
                    foreach (Item x in Game.Map.Zones[Game.Map.PlayerPos].Items)
                    {
                        if (x.Name == ClientID)
                            continue;
                        newItemList.Add(x);
                    }
                    Game.Map.Zones[Game.Map.PlayerPos].Items = newItemList;
                    break;
                }
            }
        }
        protected void PortalClick(object sender, ImageClickEventArgs e)
        {
            ClientID = ((ImageButton)sender).ID;

            for (int i = 0; i < Game.Map.Zones[Game.Map.PlayerPos].Items.Count; i++)
            {
                if (Game.Map.Zones[Game.Map.PlayerPos].Items[i].Name == ClientID)
                {
                    foreach(ItemTuple x in ((Portal)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).ItemRequest)
                    {
                        if(Game.Player.Items.Find(y => y.Item.Name == x.Item.Name) is null)
                        {
                            txtDescription.Text = "Non hai gli oggetti necessari per accedere!";
                            OnItemDescriptionDisplay = true;
                            return;
                        }
                    }
                    if(((Portal)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).RemoveAfetrEntrance)
                    {
                        foreach(ItemTuple o in ((Portal)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).ItemRequest)
                            for (int k = 0; k < Game.Player.Items.Count; k++)
                            {
                                if (Game.Player.Items[k].Item.Name == o.Item.Name)
                                    LeftItem(o.Item.Name);
                            }
                    }
                    Game.Map.PlayerPos = Game.Map.Zones.FindIndex(a=> a.ID == ((Portal)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).ZonePointer);
                    break;
                }
            }
        }
        protected void ContainerClick(object sender, ImageClickEventArgs e)
        {
            ClientID = ((ImageButton)sender).ID;

            for (int i = 0; i < Game.Map.Zones[Game.Map.PlayerPos].Items.Count; i++)
            {
                if (Game.Map.Zones[Game.Map.PlayerPos].Items[i].Name == ClientID)
                {
                    foreach (ItemTuple x in ((Container)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).ItemRequest)
                    {
                        if (Game.Player.Items.Find(y => y.Item.Name == x.Item.Name) is null)
                        {
                            txtDescription.Text = "Non hai gli oggetti necessari per accedere!";
                            OnItemDescriptionDisplay = true;
                            return;
                        }
                    }

                    if (((Container)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).RemoveAfterUnlock)
                    {
                        foreach (ItemTuple o in ((Container)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).ItemRequest)
                            for (int k = 0; k < Game.Player.Items.Count; k++)
                            {
                                if (Game.Player.Items[k].Item.Name == o.Item.Name)
                                    LeftItem(o.Item.Name);
                            }
                    }

                    foreach(ItemTuple x in ((Container)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).ItemDrop)
                    {
                        AddItem(x.Item);
                    }

                    ((Container)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).ItemDrop.Clear();

                    if (((Container)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).AlreadyOpened)
                    {
                        List<Item> lst = new List<Item>();
                        foreach(Item o in Game.Map.Zones[Game.Map.PlayerPos].Items)
                        {
                            if(!(o.Name == ((Container)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).Name))
                                 lst.Add(o);
                        }
                        Game.Map.Zones[Game.Map.PlayerPos].Items = lst;
                    }

                    break;
                }
            }
        }
        protected void CurrencyItemClick(object sender, ImageClickEventArgs e)
        {
            ClientID = ((ImageButton)sender).ID;

            for (int i = 0; i < Game.Map.Zones[Game.Map.PlayerPos].Items.Count; i++)
            {
                if (Game.Map.Zones[Game.Map.PlayerPos].Items[i].Name == ClientID)
                {
                    Game.Player.Money += ((CurrencyItem)Game.Map.Zones[Game.Map.PlayerPos].Items[i]).SellValue;

                    List <Item> lst = new List<Item>();

                    for (int k = 0; k < Game.Map.Zones[Game.Map.PlayerPos].Items.Count; k++)
                    {
                        if (k == i)
                            continue;
                        lst.Add(Game.Map.Zones[Game.Map.PlayerPos].Items[k]);
                    }

                    Game.Map.Zones[Game.Map.PlayerPos].Items = lst;                   

                    break;
                }
            }
        }

        //Events
        protected void lstInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(OnEquip>0)
            {
                if(!Equip(lstInventory.SelectedIndex))
                {
                    txtDescription.Text = "Non è possibile equipaggiare " + Game.Player.Items[lstInventory.SelectedIndex].Item.Name;
                    OnEquip = 3;
                    OnItemDescriptionDisplay = true;
                }

                return;
            }
            else if(OnThrow>0)
            {
                if(Game.Player.Items[lstInventory.SelectedIndex].Item.IsKey)
                {
                    txtDescription.Text = "Non puoi gettare un'oggetto chiave!";
                    OnThrow = 1;
                    OnItemDescriptionDisplay = true;
                    return;
                }

                if (Game.Player.Items[lstInventory.SelectedIndex].Equipped)
                {
                    txtDescription.Text = "Non puoi gettare un'oggetto equipaggiato!";
                    OnThrow = 1;
                    OnItemDescriptionDisplay = true;
                    return;
                }

                LeftItem(Game.Player.Items[lstInventory.SelectedIndex].Item.Name);

                return;
            }

            string desc = Game.Player.Items[lstInventory.SelectedIndex].Item.Description;
            if (desc == "" || desc == null)
                return;
            if(Game.Player.Items[lstInventory.SelectedIndex].Item.IsKey)
                txtDescription.Text = $"({Game.Player.Items[lstInventory.SelectedIndex].Item.SellValue}฿){Game.Player.Items[lstInventory.SelectedIndex].Item.Name}* \n\n" + desc;
            else
                txtDescription.Text = $"({Game.Player.Items[lstInventory.SelectedIndex].Item.SellValue}฿){Game.Player.Items[lstInventory.SelectedIndex].Item.Name} \n\n" + desc;
            OnItemDescriptionDisplay = true;
        }
        protected void lstDialogue_SelectedIndexChanged(object sender, EventArgs e)
        {
             if(OnDeal)
            {
                if((Dealer.Dialogue.ActualSentenceID==1 || Dealer.Dialogue.ActualSentenceID == 2) && lstDialogue.SelectedIndex==0)
                {
                    lstDialogue.Items.Clear();
                    Dealer.Dialogue.ActualSentenceID = 0;
                }               
            }
            else if(OnDialogue)
            {
                if (DialogueID == Dialogue.Spiching[DialogueID].Answers[lstDialogue.SelectedIndex].Key)
                {
                    lstInventory.Enabled = true;
                    Dialogue = null;
                    lstDialogue.Visible = false;
                    OnDialogue = false;
                }
                else
                    DialogueID = Dialogue.Spiching[DialogueID].Answers[lstDialogue.SelectedIndex].Key;
            }
        }
        protected void lstFight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(FightState == "Inventory")
            {
                for(int i=0;i<Game.Player.Items.Count;i++)
                {
                    if($"{Game.Player.Items[i].Quantity}|{Game.Player.Items[i].Item.Name}"==lstFight.Items[lstFight.SelectedIndex].ToString())
                    {
                        Game.Player.Stats.HP += ((Consumables)Game.Player.Items[i].Item).Heal;
                        Game.Player.Stats.Mana += ((Consumables)Game.Player.Items[i].Item).Mana;

                        if (((Consumables)Game.Player.Items[i].Item).Heal > 0)
                            FightResult += $"Ti sei curato di {((Consumables)Game.Player.Items[i].Item).Heal} HP\n\n";

                        if (((Consumables)Game.Player.Items[i].Item).Mana > 0)
                            FightResult += $"Hai rigenerato {((Consumables)Game.Player.Items[i].Item).Mana} di Mana\n\n";

                        if (Game.Player.Stats.HP > Game.Player.Stats.MaxHP)
                            Game.Player.Stats.HP = Game.Player.Stats.MaxHP;

                        if (Game.Player.Stats.Mana > Game.Player.Stats.MaxMana)
                            Game.Player.Stats.Mana = Game.Player.Stats.MaxMana;

                        Game.Player.Stats.HP -= Enemy.Stats.Attack;
                        FightResult += $"Hai subito {Game.Player.Stats.Attack} danni\n\n";
                        if (!(Enemy.Weapon is null))
                        {
                            Game.Player.Stats.HP -= Enemy.Weapon.AttackDamage;
                            FightResult += $"Hai subito {Game.Player.Stats.Attack + Game.Player.Weapon.AttackDamage} danni\n\n";
                        }

                        LeftItem(lstFight.Items[lstFight.SelectedIndex].ToString().Split('|')[1]);

                        lstFight.Items.Clear();
                        foreach(ItemTuple x in Game.Player.Items)
                            if(x.Item.GetType() == typeof(Consumables))
                              lstFight.Items.Add(x.Quantity+"|"+x.Item.Name);

                        lblPlayerMana.Text = $"Mana:{Game.Player.Stats.Mana}/{Game.Player.Stats.MaxMana}";
                        lblPlayerHp.Text = $"HP:{Game.Player.Stats.HP}/{Game.Player.Stats.MaxHP}";
                        return;
                    }
                }
            }
            else if(FightState == "Spells")
            {
                for (int i = 0; i < Game.Player.Items.Count; i++)
                {
                    if(Game.Player.Items[i].Item.GetType() == typeof(Spell))
                    {
                        if ($"({((Spell)(Game.Player.Items[i].Item)).ManaCost}){((Spell)(Game.Player.Items[i].Item)).Name}" == lstFight.Items[lstFight.SelectedIndex].ToString())
                        {
                            if (Game.Player.Stats.Mana < ((Spell)(Game.Player.Items[i].Item)).ManaCost)
                            {
                                FightResult = "Non hai abbastanza mana!";
                                OnItemDescriptionDisplay = true;
                                break;
                            }

                            if (Game.Player.Stats.AttackSpeed >= Enemy.Stats.AttackSpeed)
                            {
                                Game.Player.Stats.HP += ((Spell)(Game.Player.Items[i].Item)).Healing;

                                Game.Player.Stats.Mana -= ((Spell)(Game.Player.Items[i].Item)).ManaCost;

                                Enemy.Stats.HP -= ((Spell)(Game.Player.Items[i].Item)).MagicPower;

                                if (((Spell)(Game.Player.Items[i].Item)).Healing > 0)
                                    FightResult += $"Ti sei curato  di {((Spell)(Game.Player.Items[i].Item)).Healing} HP utilizzando {((Spell)(Game.Player.Items[i].Item)).Name}\n\n";

                                if (((Spell)(Game.Player.Items[i].Item)).MagicPower > 0)
                                    FightResult += $"Hai inflitto {((Spell)(Game.Player.Items[i].Item)).MagicPower} danni utilizzando {((Spell)(Game.Player.Items[i].Item)).Name}\n\n";

                                if (Game.Player.Stats.HP > Game.Player.Stats.MaxHP)
                                    Game.Player.Stats.HP = Game.Player.Stats.MaxHP;

                                if (Game.Player.Stats.Mana > Game.Player.Stats.MaxMana)
                                    Game.Player.Stats.Mana = Game.Player.Stats.MaxMana;

                                if (Enemy.Stats.HP < 0)
                                    Enemy.Stats.HP = 0;

                                if (Enemy.Stats.HP == 0)
                                    return;

                                EnemyAttack();
                            }
                            else
                            {
                                EnemyAttack();

                                if (Game.Player.Stats.HP <= 0)
                                    return;

                                Game.Player.Stats.HP += ((Spell)(Game.Player.Items[i].Item)).Healing;

                                Game.Player.Stats.Mana -= ((Spell)(Game.Player.Items[i].Item)).ManaCost;

                                Enemy.Stats.HP -= ((Spell)(Game.Player.Items[i].Item)).MagicPower;

                                if (((Spell)(Game.Player.Items[i].Item)).Healing > 0)
                                    FightResult += $"Ti sei curato  di {((Spell)(Game.Player.Items[i].Item)).Healing} HP utilizzando {((Spell)(Game.Player.Items[i].Item)).Name}\n\n";

                                if (((Spell)(Game.Player.Items[i].Item)).MagicPower > 0)
                                    FightResult += $"Hai inflitto {((Spell)(Game.Player.Items[i].Item)).MagicPower} danni utilizzando {((Spell)(Game.Player.Items[i].Item)).Name}\n\n";

                                if (Game.Player.Stats.HP > Game.Player.Stats.MaxHP)
                                    Game.Player.Stats.HP = Game.Player.Stats.MaxHP;

                                if (Game.Player.Stats.Mana > Game.Player.Stats.MaxMana)
                                    Game.Player.Stats.Mana = Game.Player.Stats.MaxMana;

                                if (Enemy.Stats.HP < 0)
                                    Enemy.Stats.HP = 0;
                            }

                            List<ItemTuple> lst = Game.Player.Items;
                            lstFight.Items.Clear();
                            foreach (ItemTuple x in lst)
                                if (x.Item.GetType() == typeof(Spell))
                                    lstFight.Items.Add($"({((Spell)x.Item).ManaCost}){x.Item.Name}");

                            lblPlayerMana.Text = $"Mana:{Game.Player.Stats.Mana}/{Game.Player.Stats.MaxMana}";

                            return;
                        }
                    }
                    
                }
            }
        }

        //Usefull Methods
        private void LoadTest()
        {
            /*XMLManager xMLManager = new XMLManager(Server);
            Game = xMLManager.Decode(System.Web.HttpContext.Current.Server.MapPath("~/Game.xml"));*/

            Database database = new Database();
            Game = database.Load(Session["Username"].ToString(), (int)Session["PlayerID"]);

            /*Tests
            Game = new Game(new List<Item>(), new List<NPC>(), null, new Player());
            Game.Player.Money = 100;
            Game.Player.Stats.HP = 20;
            Game.Player.Stats.MaxHP = 20;
            Game.Player.Stats.Mana = 15;
            Game.Player.Stats.MaxMana = 15;
            Game.Player.Stats.Attack = 4;
            Game.Player.Stats.AttackSpeed = 3;
            Game.Player.Stats.Elusiveness = 4;
            Game.Player.Stats.Affinity = 1;
            Game.Player.Stats.Intelligence = 6;

            Game.Map = new Map("Palazzo");
            Zone z0 = new Zone("Piano terra", 0);
            Zone z1 = new Zone("Piano 1", 1);
            Zone z2 = new Zone("Piano 2", 2);
            Zone z3 = new Zone("Piano 1-1", 3);
            Zone z4 = new Zone("Piano 1-2", 4);
            Zone z5 = new Zone("Stanza Segreta", 5);
            z0.North = 1;
            z1.North = 2;
            z1.South = 0;
            z1.Est = 4;
            z1.West = 3;
            z2.South = 1;
            z3.Est = 1;
            z4.West = 1;
            z0.StoryDescription = "Cera una volta una persona.....quella persona ERI TU, WOOOOOOW Plottwist";
            z1.StoryDescription = "Idk, proseguendo vedi uno strano tipo";
            z2.StoryDescription = "ehehe, ed è così che incontrasti il baaaka(l'egirl)";
            z4.StoryDescription = "Idk, suppongo lui abbia qualcosa di interessante da venderti";
            z3.StoryDescription = "OWO Ecco una waifu!";
            z0.BackGround = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/Sfondo1.png"));
            z1.BackGround = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/Sfondo3.png"));
            z2.BackGround = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/iochemangio.gif"));
            z3.BackGround = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/Sfondo2.png"));
            z4.BackGround = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/Sfondo4.png"));
            z0.Description = "Una foresta OWO";
            z1.Description = "Una foresta OWO, sfocata";
            z2.Description = "Un Ezze";
            z3.Description = "Un tizio a caso";
            z4.Description = "Cosa ti aspettavi scusa?";
            z2.ItemNeeded.Add(new Item("Coin",true));

            NPC Okarin = new NPC("Rintaro Okabe", "A MAD SCIENTIST!", System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/Okarin2.png")), null, null);
            Okarin.DialogueSprite = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/Enemy2Dialogue.png"));
            Okarin.Position = new ElementPosition();
            Okarin.Position.X = 300;
            Okarin.Position.Y = 350;
            Okarin.Position.Scale = 600;
            Okarin.Dialogue = new Dialogue("I am a mad scientist!");
            Okarin.Dialogue.AddSentence("Kuristina!");
            Okarin.Dialogue.AddSentence("Bruh! Prendi i soldi");
            Okarin.Dialogue.Spiching[0].Answers.Add(new KeyValuePair<int, string>(0, "Ok"));
            Okarin.Dialogue.Spiching[0].Answers.Add(new KeyValuePair<int, string>(1, "Kurisu Makise?"));
            Okarin.Dialogue.Spiching[0].Answers.Add(new KeyValuePair<int, string>(2, "Bruh come rompo il lucchetto?"));
            Okarin.Dialogue.Spiching[1].Answers.Add(new KeyValuePair<int, string>(1, "Oook"));
            Okarin.Dialogue.Spiching[2].Answers.Add(new KeyValuePair<int, string>(2, "Oook"));
            z1.Peoples.Add(Okarin);

            EnemyKeyNPC Kurisu = new EnemyKeyNPC("Kurisu Makise", "Pff!", System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/Kurisu.png")), null, null);
            Kurisu.DialogueSprite = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/Kurisu.png"));
            Kurisu.FightSprite = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/Kurisu.png"));
            Kurisu.Position = new ElementPosition();
            Kurisu.Position.X = 500;
            Kurisu.Position.Y = 350;
            Kurisu.Position.Scale = 600;
            Kurisu.Stats.HP = 20;
            Kurisu.Stats.MaxHP = 20;
            Kurisu.Stats.Mana = 15;
            Kurisu.Stats.MaxMana = 15;
            Kurisu.Stats.Attack = 4;
            Kurisu.Stats.AttackSpeed = 3;
            Kurisu.Stats.Elusiveness = 4;
            Kurisu.Stats.Affinity = 1;
            Kurisu.Stats.Intelligence = 6;
            Kurisu.Dialogue = new Dialogue("Mmm");
            Kurisu.Dialogue.Spiching[0].Answers.Add(new KeyValuePair<int, string>(-1, "Mmmm"));
            Kurisu.Experience = 15;
            Kurisu.Drop.Items.Add(new ItemTuple(new Consumables(0, "Pozione", 10, 0), 5));
            Kurisu.Spells.Add(new Spell(0, "Magia che fa danno", 0, 5, 5));
            Kurisu.Spells.Add(new Spell(0, "Magia che cura", 5, 0, 5));
            Kurisu.Spells.Add(new Spell(0, "Magia mista", 5, 5, 10));
            z3.Peoples.Add(Kurisu);

            Dealer asdf = new Dealer(0, "adsf");
            Dealer SuperHacker = new Dealer("Itaru Hasida", "Pff!", System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/Hitaru.png")), null, null);
            SuperHacker.DialogueSprite = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/Hitaru.png"));
            SuperHacker.Position = new ElementPosition();
            SuperHacker.Position.X = 200;
            SuperHacker.Position.Y = 350;
            SuperHacker.Position.Scale = 600;
            SuperHacker.Shop.Add(new ItemTuple(new Item("Spada di Legno", 10, true), 3));
            SuperHacker.Shop.Add(new ItemTuple(new Item("Pozza", 5, true), 2));
            SuperHacker.Shop.Add(new ItemTuple(new Item("Porro", 2, true), 5));
            z4.Peoples.Add(SuperHacker);

            Item coin = new Item("Coin",false);
            coin.ItemSprite = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/coin.png"));
            coin.Position.X = 900;
            coin.Position.Y = 350;
            coin.Position.Scale = 600;
            Game.Items.Add(coin);

            Portal p = new Portal(0,"Portale");
            p.ZonePointer = 5;
            p.ItemSprite = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/NetherPortal.png"));
            z4.Items.Add(p);
            p.Position.X = 500;
            p.Position.Y = 350;
            p.Position.Scale = 100;
            Game.Items.Add(p);

            Portal p2 = new Portal(0, "Portale");
            p2.ZonePointer = 4;
            p2.ItemSprite = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/NetherPortal.png"));
            z5.Items.Add(p2);
            p2.Position.X = 500;
            p2.Position.Y = 350;
            p2.Position.Scale = 100;
            Game.Items.Add(p2);

            CurrencyItem sacco = new CurrencyItem(0,"sacchetto di monete");
            sacco.ItemSprite = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Immagini/sacchetto.png"));
            sacco.Position.X = 2000;
            sacco.Position.Y = 350;
            sacco.Position.Scale = 100;
            sacco.SellValue = 100;
            Game.Items.Add(sacco);
            z2.Items.Add(sacco);

            z3.Items.Add(coin);
            z4.Items.Add(coin);
            z2.Items.Add(coin);
            Game.Map.AddZone(z0);
            Game.Map.AddZone(z1);
            Game.Map.AddZone(z2);
            Game.Map.AddZone(z3);
            Game.Map.AddZone(z4);
            Game.Map.AddZone(z5);
            Game.Map.PlayerPos = 0;
            Game.Player.Items.Add(new ItemTuple(new Weapon("Spada di Legno","Una stupida spada di legno",true), 1));
            Game.Player.Items.Add(new ItemTuple(new Weapon("Spada del fato mistico del fuoco blu ancestrale","A big sord", false), 1));
            Game.Player.Items.Add(new ItemTuple(new Weapon("Calibrum","The long weapon", false), 1));
            Game.Player.Items.Add(new ItemTuple(new Weapon("Crescendum", "The weapon of the thickness", false), 1));
            Game.Player.Items.Add(new ItemTuple(new Item("Porro","Bruh this is a fuking Porro", 20, false), 1));
            Game.Player.Items.Add(new ItemTuple(new Wearable("Sandali", "Roba senza stile a quanto pare", false), 1));
            Game.Player.Items.Add(new ItemTuple(new Wearable("Tabi", true), 1));
            Game.Player.Items.Add(new ItemTuple(new Weapon("Stormrazor", true), 1));
            Game.Player.Items.Add(new ItemTuple(new Consumables(0, "Pozione",10,0), 5));
            Game.Player.Items.Add(new ItemTuple(new Consumables(0, "Pozione del mana",0,10), 5));
            Game.Player.Items.Add(new ItemTuple(new Spell(0, "Palla di fuoco", 0, 5, 5),1));
            Game.Player.Items.Add(new ItemTuple(new Spell(0, "Guarigione", 10, 0, 10),1));
            Game.Player.Items.Add(new ItemTuple(new Spell(0, "Soffio del drago della Romania", 0, 10, 10),1));
            Game.Player.Money = 100;
            lstInventory.DataSource = Game.Player.Items;
            lstInventory.DataBind();*/

            //NewImageButton("asdf", z0.BackGround, 500, 500);

            /*
            ImageBackground.ImageUrl = "data:image/bmp;base64," + ImageToBase64(LocalFileToImage(@"C:\Users\utente\Pictures\LolChampWtf.png"));
            ImageBackground.ImageUrl = "data:image/bmp;base64," + ImageToBase64(LocalFileToImage(@"C:\Users\utente\Pictures\XD.png"));
            */
        }
        private void UpdateMap(Zone zone)
        {
            lstDialogue.Visible = false;
            lstFight.Visible = false;

            //set Zone Name
            lblZoneName.Text = zone.Name;

            //Update inventory list
            if (Game.Player != null)
                lstInventory.DataSource = Game.Player.Items;

            lstInventory.DataBind();

            //Add zone description if the page is not refreshing for an item description
            if (!OnItemDescriptionDisplay)
            {
                if(Game.Map.Zones[Game.Map.PlayerPos].FirstTime)
                {
                    txtDescription.Text = zone.StoryDescription;
                    Game.Map.Zones[Game.Map.PlayerPos].FirstTime = false;
                    if (txtDescription.Text == "")
                        txtDescription.Text = zone.Description;
                }
                else
                {
                    if (FightResult != "")
                    {
                        txtDescription.Text = FightResult;
                        FightResult = "";
                    }
                    else
                        txtDescription.Text = zone.Description;
                }

            }
            else
                OnItemDescriptionDisplay = false;

            //Manage movement arrows
            if (OnDialogue)
            {
                lstDialogue.Visible = true;

                btnUpArrow.Visible = false;
                btnRightArrow.Visible = false;
                btnDownArrow.Visible = false;
                btnLeftArrow.Visible = false;

                //foreach(NPC npc in zone.Peoples)
                //{
                //    if(npc.Name == ClientID)
                //    {
                //        Dialogue = npc.Dialogue;
                //        break;
                //    }
                //}

                DynamicDialoguesList.Items.Clear();
                lstDialogue.Items.Clear();

                foreach (var x in Dialogue.Spiching[DialogueID].Answers)
                {
                    lstDialogue.Items.Add(x.Value);
                }

                if (Dialogue.Spiching[DialogueID].ItemGive is null)
                {
                    txtDescription.Text = (ClientID + ":\n\n" + "Non ho altro da darti!");
                }    
                else if(Dialogue.Spiching[DialogueID].ItemGive.Count>0)
                {
                    string itemGot = "Hai ottenuto:\n";
                    foreach(ItemTuple x in Dialogue.Spiching[DialogueID].ItemGive)
                    {
                        for (int i = 0; i < x.Quantity; i++)
                            AddItem(x.Item);
                        itemGot += x.Quantity + " " + x.Item.Name + "\n";
                    }
                    Dialogue.Spiching[DialogueID].ItemGive = null;
                    txtDescription.Text = (ClientID + ":\n\n" + Dialogue.Spiching[DialogueID].Phrase + "\n\n" + itemGot);
                }
                else
                {
                    txtDescription.Text = (ClientID + ":\n\n" + Dialogue.Spiching[DialogueID].Phrase);
                }
            }
            else if(OnDeal)
            {
                try
                {
                    lstDialogue.Visible = true;

                    btnUpArrow.Visible = false;
                    btnRightArrow.Visible = false;
                    btnDownArrow.Visible = false;
                    btnLeftArrow.Visible = false;

                    if (lstDialogue.Items.Count == 0)
                    {
                        foreach (var x in Dealer.Dialogue.Spiching[Dealer.Dialogue.ActualSentenceID].Answers)
                        {
                            lstDialogue.Items.Add(x.Value);
                        }
                    }
                    else
                    {
                        if (!(lstDialogue.SelectedIndex == -1 && lstInventory.SelectedIndex == -1))
                        {
                            if (Dealer.Dialogue.ActualSentenceID == 0)
                            {
                                if (lstDialogue.SelectedIndex == 0)
                                {
                                    Dealer.Dialogue.ActualSentenceID = 1;
                                    lstDialogue.Items.Clear();
                                    lstDialogue.Items.Add("Indietro");
                                    foreach (var x in Dealer.Shop)
                                    {
                                        lstDialogue.Items.Add(x.Item.SellValue + "฿ | " + x);
                                    }
                                }
                                else if (lstDialogue.SelectedIndex == 1)
                                {
                                    Dealer.Dialogue.ActualSentenceID = 2;
                                    lstDialogue.Items.Clear();
                                    lstDialogue.Items.Add("Indietro");
                                    PersonalShop = new List<ItemTuple>();
                                    foreach (var x in Game.Player.Items)
                                    {
                                        if (x.Item.IsKey)
                                            continue;
                                        lstDialogue.Items.Add(x.Item.SellValue + "฿ | " + x);
                                        PersonalShop.Add(x);
                                    }
                                }
                                else
                                {
                                    lstDialogue.Items.Clear();
                                    lstInventory.Enabled = true;
                                    Dialogue = null;
                                    lstDialogue.Visible = false;
                                    OnDeal = false;
                                }
                            }
                            else if (Dealer.Dialogue.ActualSentenceID == 1)
                            {
                                if (Game.Player.Money < Dealer.Shop[lstDialogue.SelectedIndex - 1].Item.SellValue)
                                {
                                    txtDescription.Text = "Non hai abbastanza soldi per comprare questo item";
                                    OnItemDescriptionDisplay = true;
                                }
                                else
                                {
                                    Game.Player.Money -= Dealer.Shop[lstDialogue.SelectedIndex - 1].Item.SellValue;
                                    AddItem(Dealer.Shop[lstDialogue.SelectedIndex - 1].Item);
                                    if (--Dealer.Shop[lstDialogue.SelectedIndex - 1].Quantity == 0)
                                    {
                                        List<ItemTuple> lst = new List<ItemTuple>();
                                        for (int i = 0; i < Dealer.Shop.Count; i++)
                                        {
                                            if (i == lstDialogue.SelectedIndex - 1)
                                                continue;
                                            lst.Add(Dealer.Shop[i]);
                                        }
                                        Dealer.Shop = lst;
                                    }

                                    lstDialogue.Items.Clear();
                                    lstDialogue.Items.Add("Indietro");
                                    foreach (var x in Dealer.Shop)
                                    {
                                        lstDialogue.Items.Add(x.Item.SellValue + "฿ | " + x);
                                    }
                                }
                            }
                            else if (Dealer.Dialogue.ActualSentenceID == 2)
                            {
                                Game.Player.Money += PersonalShop[lstDialogue.SelectedIndex - 1].Item.SellValue;

                                for (int i = 0; i < Game.Player.Items.Count; i++)
                                {
                                    if (" " + Game.Player.Items[i].Item.Name == lstDialogue.Items[lstDialogue.SelectedIndex].Text.Split('|')[2])
                                    {
                                        if (--Game.Player.Items[i].Quantity == 0)
                                        {
                                            List<ItemTuple> lst = new List<ItemTuple>();

                                            for (int k = 0; k < Game.Player.Items.Count; k++)
                                            {
                                                if (Game.Player.Items[k].Item.IsKey)
                                                {
                                                    lst.Add(Game.Player.Items[k]);
                                                    continue;
                                                }

                                                if (Game.Player.Items[k].Quantity == 0)
                                                    continue;

                                                PersonalShop.Add(Game.Player.Items[k]);
                                                lst.Add(Game.Player.Items[k]);
                                            }

                                            Game.Player.Items = lst;
                                        }
                                        break;
                                    }
                                }

                                lstDialogue.Items.Clear();
                                lstDialogue.Items.Add("Indietro");
                                PersonalShop = new List<ItemTuple>();
                                foreach (var x in Game.Player.Items)
                                {
                                    if (x.Item.IsKey)
                                        continue;
                                    lstDialogue.Items.Add(x.Item.SellValue + "฿ | " + x);
                                    PersonalShop.Add(x);
                                }
                            }
                            lblZoneName.Focus();
                        }

                    }

                    lstInventory.DataSource = Game.Player.Items;
                    lstInventory.DataBind();

                    /*if (Dialogue.Spiching[DialogueID].ItemGive is null)
                    {
                        txtDescription.Text = (ClientID + ":\n\n" + "Non ho altro da darti!");
                    }
                    else if (Dialogue.Spiching[DialogueID].ItemGive.Count > 0)
                    {
                        string itemGot = "Hai ottenuto:\n";
                        foreach (ItemTuple x in Dialogue.Spiching[DialogueID].ItemGive)
                        {
                            for (int i = 0; i < x.Quantity; i++)
                                AddItem(x.Item);
                            itemGot += x.Quantity + " " + x.Item + "\n";
                        }
                        Dialogue.Spiching[DialogueID].ItemGive = null;
                        txtDescription.Text = (ClientID + ":\n\n" + Dialogue.Spiching[DialogueID].Phrase + "\n\n" + itemGot);
                    }
                    else
                    {
                        txtDescription.Text = (ClientID + ":\n\n" + Dialogue.Spiching[DialogueID].Phrase);
                    }*/
                }
                catch (Exception ex)
                {

                }

                if(!OnDeal)
                {
                    if (zone.Est == zone.ID)
                        btnRightArrow.Visible = false;
                    else
                    {
                        btnRightArrow.Visible = true;

                        if (CheckLockedZone(Game.Map.Zones[zone.Est]))
                            btnRightArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/locket.png")));
                        else
                            btnRightArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/RightArrow.png")));
                    }

                    if (zone.West == zone.ID)
                        btnLeftArrow.Visible = false;
                    else
                    {
                        btnLeftArrow.Visible = true;

                        if (CheckLockedZone(Game.Map.Zones[zone.West]))
                            btnLeftArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/locket.png")));
                        else
                            btnLeftArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/LeftArrow.png")));
                    }

                    if (zone.North == zone.ID)
                        btnUpArrow.Visible = false;
                    else
                    {
                        btnUpArrow.Visible = true;

                        if (CheckLockedZone(Game.Map.Zones[zone.North]))
                            btnUpArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/locket.png")));
                        else
                            btnUpArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/UpArrow.png")));
                    }

                    if (zone.South == zone.ID)
                        btnDownArrow.Visible = false;
                    else
                    {
                        btnDownArrow.Visible = true;

                        if (CheckLockedZone(Game.Map.Zones[zone.South]))
                            btnDownArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/locket.png")));
                        else
                            btnDownArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/DownArrow.png")));
                    }
                }


            }
            else if(OnFight)
            {
                lstFight.Visible = true;

                btnUpArrow.Visible = false;
                btnRightArrow.Visible = false;
                btnDownArrow.Visible = false;
                btnLeftArrow.Visible = false;

                //Create the fight GUI
                DynamicFighButtons.Clear();

                Button btn = new Button();
                btn.ID = "btnFight";
                btn.Attributes["runat"] = "server";
                btn.Text = "Fight";
                btn.Attributes["style"] = "position:absolute; top: 620px; left: -10px; height: 142.5px; width: 315px; font-size:50px; font-family:Calibri;";
                btn.Click += btnFight_Click;

                Button btn1 = new Button();
                btn1.ID = "btnSpells";
                btn1.Attributes["runat"] = "server";
                btn1.Text = "Spells";
                btn1.Attributes["style"] = "position:absolute; top: 620px; left: 305px; height: 142.5px; width: 315px; font-size:50px; font-family:Calibri;";
                btn1.Click += btnSpells_Click;

                Button btn2 = new Button();
                btn2.ID = "btnInventory";
                btn2.Attributes["runat"] = "server";
                btn2.Text = "Inventory";
                btn2.Attributes["style"] = "position:absolute; top: 762.5px; left: -10px; height: 142.2px; width: 315px; font-size:50px; font-family:Calibri;";
                btn2.Click += btnInventory_Click;

                Button btn3 = new Button();
                btn3.ID = "btnRun";
                btn3.Attributes["runat"] = "server";
                btn3.Text = "Run Away";
                btn3.Attributes["style"] = "position:absolute; top: 762.5px; left: 305px; height: 142.2px; width: 315px; font-size:50px; font-family:Calibri;";
                btn3.Click += btnRun_Click;

                plhFight.Controls.Add(btn);
                plhSpells.Controls.Add(btn1);
                plhInventory.Controls.Add(btn2);
                plhRun.Controls.Add(btn3);

                DynamicFighButtons.Add(btn);
                DynamicFighButtons.Add(btn1);
                DynamicFighButtons.Add(btn2);
                DynamicFighButtons.Add(btn3);

                System.Web.UI.WebControls.Image imgEnemy = new System.Web.UI.WebControls.Image();
                imgEnemy.ID = "imgEnemy";
                imgEnemy.Attributes["runat"] = "server";
                imgEnemy.Attributes["style"] = $"position:absolute; top: 330px; left: 530px; height:300px; width:auto;";
                string base64String = "";
                if (Enemy.FightSprite is null)
                    base64String = ImageToBase64(LocalFileToImage(Enemy.OverWorldSprite));
                else
                    base64String = ImageToBase64(LocalFileToImage(Enemy.FightSprite));
                imgEnemy.ImageUrl = "data:image/gif;base64," + base64String;
                plhEnemy1.Controls.Add(imgEnemy);
                if(txtDescription.Text != "Non sei riuscito a scappare!")
                    txtDescription.Text = $"{Enemy.Name}:\nHp:{Enemy.Stats.HP}/{Enemy.Stats.MaxHP}\nMana:{Enemy.Stats.Mana}/{Enemy.Stats.MaxMana}\n\n{FightResult}";
                FightResult = "";
                OnItemDescriptionDisplay = true;

                if(Game.Player.Stats.HP<=0)
                {
                    imgEnemy.Visible = false;

                    XMLManager xMLManager = new XMLManager(Server);
                    String str = HttpContext.Current.Server.MapPath(@"~/Restore.xml");
                    FileUploadGame.SaveAs(HttpContext.Current.Server.MapPath(@"~/Restore.xml"));
                    Game = xMLManager.Decode(HttpContext.Current.Server.MapPath(@"~/Restore.xml"));
                    OnFight = false;
                    FightState = "lstFight";
                    Enemy = null;
                    OnItemDescriptionDisplay = false;
                    plhFight.Visible = false;
                    plhInventory.Visible = false;
                    plhSpells.Visible = false;
                    plhRun.Visible = false;

                    foreach (NPC npc in Game.Map.Zones[Game.Map.PlayerPos].Peoples)
                    {
                        string base64 = ImageToBase64(LocalFileToImage(Server.MapPath(npc.OverWorldSprite)));
                        System.Web.UI.WebControls.ImageButton imageButton = new System.Web.UI.WebControls.ImageButton();
                        plhNpcs.Controls.Add(imageButton);
                        imageButton.ImageUrl = "data:image/gif;base64," + base64;
                        imageButton.Click += NpcClick;
                        imageButton.ID = npc.Name;
                        imageButton.Attributes["style"] = String.Format("z-index:3; position:inherit; top: {0}px; left: {1}px; height:{2}%; width:auto;", npc.Position.Y*1.87, npc.Position.X*2.1, npc.Position.Scale*16);
                        DynamicNpcs.Add(imageButton);
                    }

                    foreach (Item item in Game.Map.Zones[Game.Map.PlayerPos].Items)
                    {
                        string base64 = ImageToBase64(LocalFileToImage(Server.MapPath(item.ItemSprite)));
                        System.Web.UI.WebControls.ImageButton imageButton = new System.Web.UI.WebControls.ImageButton();
                        plhItems.Controls.Add(imageButton);
                        imageButton.ImageUrl = "data:image/gif;base64," + base64;
                        if (item.GetType() == typeof(Portal))
                            imageButton.Click += PortalClick;
                        else if (item.GetType() == typeof(Container))
                            imageButton.Click += ContainerClick;
                        else
                            imageButton.Click += ItemClick;
                        imageButton.ID = item.Name;
                        imageButton.Attributes["style"] = String.Format("z-index:3; position:inherit; top: {0}px; left: {1}px; height:{2}%; width:auto;", item.Position.Y*1.87, item.Position.X*2.1, item.Position.Scale*16);
                        DynamicItems.Add(imageButton);
                    }


                    /*FileChosen = (bool)RestoreGame[0];
                    OnDialogue = Convert.ToBoolean(RestoreGame[1]);
                    OnDealCopy = Convert.ToBoolean(RestoreGame[2]);
                    OnItemDescriptionDisplay = Convert.ToBoolean(RestoreGame[3]);
                    OnFight = Convert.ToBoolean(RestoreGame[4]);
                    OnThrow = Convert.ToInt32(RestoreGame[5]);
                    OnEquip = Convert.ToInt32(RestoreGame[6]);
                    OnUse = Convert.ToInt32(RestoreGame[7]);
                    DialogueID = Convert.ToInt32(RestoreGame[8]);
                    DealerDialogueID = Convert.ToInt32(RestoreGame[9]);
                    ClientID = (string)RestoreGame[10];
                    FileName = (string)RestoreGame[11];
                    FightState = (string)RestoreGame[12];
                    FightResult = (string)RestoreGame[13];
                    Enemy = (EnemyKeyNPC)((object)RestoreGame[14]);
                    Dealer = (Dealer)RestoreGame[15];
                    Game = (Game)RestoreGame[16];
                    Dialogue = (Dialogue)RestoreGame[17];
                    DynamicNpcs = (List<ImageButton>)RestoreGame[18];
                    DynamicItems = (List<ImageButton>)RestoreGame[19];
                    DynamicFighButtons = (List<Button>)RestoreGame[20];
                    DynamicDialoguesList = (ListBox)RestoreGame[21];
                    PersonalShop = (List<ItemTuple>)RestoreGame[22];
                    */
                    UpdateMap(Game.Map.Zones[Game.Map.PlayerPos]);
                    return;
                }

                if(Enemy.Stats.HP <= 0)
                {
                    imgEnemy.Visible = false;
                    plhFight.Visible = false;
                    plhSpells.Visible = false;
                    plhInventory.Visible = false;
                    plhRun.Visible = false;
                    lstFight.Visible = false;

                    List<ItemTuple> lstItem = Enemy.Drop.Get();
                    string itemsDropped = "";
                    foreach (ItemTuple x in lstItem)
                    {
                        itemsDropped += "- " + x.Item.Name + "\n";
                        for (int i = 0; i < x.Quantity; i++)
                            AddItem(x.Item);
                    }
                    txtDescription.Text = $"{Game.Map.Zones[Game.Map.PlayerPos].Description}\n\nIl nemico ha lasciato:\n- {Enemy.Money}฿\n{itemsDropped}";

                    OnItemDescriptionDisplay = true;

                    Game.Player.Exp += Enemy.Experience;

                    Game.Player.Money += Enemy.Money;

                    OnFight = false;

                    List<NPC> lst = new List<NPC>();
                    foreach (NPC nPC in Game.Map.Zones[Game.Map.PlayerPos].Peoples)
                    {
                        if (nPC.Name != Enemy.Name)
                            lst.Add(nPC);
                    }
                    Game.Map.Zones[Game.Map.PlayerPos].Peoples = lst;

                    lstFight.Items.Clear();

                    UpdateMap(Game.Map.Zones[Game.Map.PlayerPos]);
                }
            }
            else
            {
                if (zone.Est == zone.ID)
                    btnRightArrow.Visible = false;
                else
                {
                    btnRightArrow.Visible = true;

                    if (CheckLockedZone(Game.Map.Zones[zone.Est]))
                        btnRightArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/locket.png")));
                    else
                        btnRightArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/RightArrow.png")));
                }

                if (zone.West == zone.ID)
                    btnLeftArrow.Visible = false;
                else
                {
                    btnLeftArrow.Visible = true;

                    if (CheckLockedZone(Game.Map.Zones[zone.West]))
                        btnLeftArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/locket.png")));
                    else
                        btnLeftArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/LeftArrow.png")));
                }

                if (zone.North == zone.ID)
                    btnUpArrow.Visible = false;
                else
                {
                    btnUpArrow.Visible = true;

                    if (CheckLockedZone(Game.Map.Zones[zone.North]))
                        btnUpArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/locket.png")));
                    else
                        btnUpArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/UpArrow.png")));
                }

                if (zone.South == zone.ID)
                    btnDownArrow.Visible = false;
                else
                {
                    btnDownArrow.Visible = true;                    

                    if (CheckLockedZone(Game.Map.Zones[zone.South]))
                        btnDownArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/locket.png")));
                    else
                        btnDownArrow.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Images/DownArrow.png")));
                }
            }

            //Load background image
            if (zone.BackGround is null)
            {
                Response.Write("<script>alert('The background image could not be loaded')</script>");
            }
            else
            {
                string base64String = ImageToBase64(LocalFileToImage(Server.MapPath(zone.BackGround)));
                ImageBackground.ImageUrl = "data:image/gif;base64," + base64String;
            }

            //Load Minimap Image
            if (zone.Minimap is null || zone.Minimap == "")
            {
                imageMinimap.ImageUrl = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBxIREBMSEhMVEhISFxcTGBETEBUVEhcSFRcXFxYVExYYHSggGBolGxUTITMiJSktLi4uFx8zODMsNygtLisBCgoKDg0NFw8PFS0ZFRkrKy0tKy0rLS0rLSsrLSsrKysrKysrKystLS0rKy0tLS0rLSstKysrLS0tLS03LTc3K//AABEIAOEA4QMBIgACEQEDEQH/xAAbAAEAAwEBAQEAAAAAAAAAAAAABAUGAwECB//EADgQAAIBAQQGCQMDBAMBAAAAAAABAgMEBREhEjFBUWFxIjJSgZGhscHRBhPwQnKSM2Ki4YKy8RX/xAAWAQEBAQAAAAAAAAAAAAAAAAAAAQL/xAAWEQEBAQAAAAAAAAAAAAAAAAAAARH/2gAMAwEAAhEDEQA/AP3EAAAAAAAAAAAeN4ayvtN80oanpv8AtzXjqAsQZytf1R9WMY/5P29CDVt1WWupLkpYLwRcTWvlNLW0ubwPn78e1H+SMU2MBhra/ej2l/JHsakXqafJoxIQw1uQYulapx6s5LDYpPDw1E2jfdWOvCXNYPxQw1pwVVmv2nLrJwf8o+K+Cyp1IyWMWpLemmvIivsAAAAAAAAAAAAAAAAAAADlaK8acXKTwS/MFvYHUq7dfUIZQ6cv8Vze3uKq8r0lVxiujDdtf7vj1K8uJqRarbOp15YrsrKPh8kcAqAB7GLbwSxe5AeAnUbprS/Tgt8ml5ayTG4J7ZRXiyKqAXD+n59uPgzlUuOqtWjLk8H5gVgO1eyVIdaLXHDLxRxKgdKNaUHjGTi+HutpzAF9Yr91KqsP71q71sLmE00mninmmtRiCTYrbOk8YvLbF9V/D4kxdbAEWw26FVYxya1xetfK4koigAAAAAAAAAAAHK0V404uUngl+YLiB82y1RpR0pdy2t7kZS2WuVWWlLujsS4fJ7bbXKrPSl3LYluI5UAAVAA0d0XUoYTmsZ7F2f8AZFQrvuWUsJVOjHs/qfPcXtns0KawjFL172dgRQAAAAB40V1tuenPOPQlvSy70WQAxtrsk6bwkuT2PkzgbS02eNSLjJYp/mKMpb7G6U9F5p5p718lRGABUfdKq4tSi2mtqNRdd4qqsHlNa4+64ehlD6p1HFqUXg1qYVtwQ7ttyqwx1SXWjue9cCYZUAAAAAAAAMtfFv8Auywi+hHVxfa/Pctb+tmhDQXWn5R2/HiZosSgAKgAfVKDlJRWttLxAt7gsOL+5LUso89/caA52ekoRUVqSwOhloAOdesoRcpakB7WrRgsZNJb2V1W/aS1KUuOGC8yittrlVlpS7o7EiOXE1oo3/DbGS8GTrLbqdTqyz3PJ+DMeexk08U8Gtq1jDW4BWXNeP3Foy68fNbyzIoRLysiq02v1LOL4ksAYdrA8LC/KGhWbWqXS79vn6leVAAFR3sVqdKaku9b1uNfQqqcVKOaksUYkuPp62YS+29Us48JbV3+xFaEAEUAAA8bwzPSuv20aFFrbPo9z1+WPiBn7fafuVJT2PJftWr57yOAaZAAALK4KWlWT7Kb9vcrS5+muvPkvUitAACKFB9R2rNU1s6T57EXzeBjLVW05yl2nj3bPIsSuQAKgAAO9graFSMtzWPJ5M2Rh4LNc0bdEqx6ACKpPqaGUJcWvf2KE0P1L/Th+72ZnixKAAqB7GTTTWTTxT4rNHgA2VitCqU4zW1eDWTXjidyi+m7R1qb/evJP2L0zWgAADOfUdXGpGPZXnL/AMRozHXhU0qtR/3NdyyXkixKjgAqAAAEy6rX9qom+q8nye0hgDcRkmsVmntPTJWO8qlLJPGPZeru3Eqd/wBRrKMU9+bJi6sb8tahTcV1p5d21mYPutVlNuUni3tZ8AAAVAA7WSzSqSUY69+xLewJVyWXTqJ/ph0nz2L83GpOFjssaUFGPe973ncy0AHjYFF9S1M4R5v2+SkJN42n7lSUtmpcl+Y95GKgACoAACTdlXQrQfHDueXubAwzNrZ6mlCMu0k/FEqx0ABFfM5YJvcsfAxDZtbR1Jcn6GKRYlAAVAAAAAAAAAAAADrZqEqklGKxb8lvYHtls0qktGKz8kt7NVYbHGlHRjr2va2LBY40o4LN7ZbWySZaAAAKe/rdor7cdcutwju7yRet4qksFnN6luW9mXnJttt4t5tveVHgAKgAAAAAGruWeNCHDFeDaMoai4f6Eecv+zJVixABFc6/UlyfoYpG2qxxi1vTXkYnAsSgAKgAAAAAAAAAAPUjVXVYVShn15a37FRcFm0qmk9UM/8Ak9RpSVYAAihAvS8VSWCzm9S3cWL0vBUo4LOb1LdxZl6k3JuTeLebZUKk3Jtt4t62z5AKgAAAAAAAAam4f6Eecv8AszLGquOGFCPHF+LbJVieACKGLtdPRqTjqwk0uWOXlgbQzH1BS0a2PaSfesn6IsSq0AFQAAAAAAAAAAGn+n6WFHHtNv2XoWRFupYUaf7SUZaDja7QqcHN7Nm97Edik+pauUIb8ZPu1eoFLXrOcnKTxb/MEcwDTIAAAAAAAAAADNlYqejThHVhFZccMzJ2Ojp1Ix2NrHlt8sTZkqwABFCq+orPpU1LbB/4yyfngWp8VaalFxeqSafJ5AYkHS0UnCUovXF4fD8MGczTIAAAAAAAAAANbc88aMOCw8CYZi57x+03GXUlnye80VK0QksYyTXBoy06mXv+ppVmuylH39y6t15wpp4NSlsinjnx3GWqTcm2823i3xLEr5ABUAAAAAAAAAABb/Tlnxm57IrBfuf+sfE0REuqzfbpRi9b6T5v4yXcSyVoABAAAFJ9RWTVVS1ZS5bH7d5Qm3nBNNNYp5NPcZG8LI6U3F6tcXvj8liVGABUAAAAAAAAASbDYpVZYRyS1yepF5SuOklnpSfPDwwIrNA0VouGDXQbi+LxXyUNooSpycZLBr8xQHMAFQAAAAAAAALG47J9yppNdGGf/LYvcgUqblJRisW3gka+w2VUoKK73ve1kVIABFAAAAAAi3jY1VhovJrNS3P4JQAxNWm4ycZLBrJo+DV3pdyqrFZTWp+z4ehlqlNxbjJYNa0aR8gAIAAAAfdHrR5r1A11gsyp01Fa9b4y2kgAy0FXf9mUqen+qGfc9a9y0I15f0an7X6AY8AGmQAAAAAANBc91aOFSoulsju4vj6BXW5bu+2tOXXktXZW7mWgBlQAAAAAAAAAACHeF3xqrPKS1S2rg96JgAxtrsk6UsJLk9j5HA21ajGa0ZJST2MobdcclnT6S7L6y5PaVFOD2UWng001sawfgeFQAAGvu21KrTT2rJriSjGWW0ypy0ovDhsfNFxS+oFh0oPH+1przJirsqfqC1JQ+2utLXwin/oj2i/3hhCOHGT9kU1So5NuTxb2sD5ABUAAAPqlTcmoxTbexE+xXPUqYOXQjva6Xcvk0FkscKSwiub2vmyKh3ZdKp9KeEp+UeXHiWgBFAAAAAAAAAAAAAAAAAABwtNkhUWE4p8dTXJrMqbTcG2nLun8r4L0DRj6131Ya4PmlivIjJm5OdWhCXWipc0mXUxigaud00X+jDk2vQ+FctHsv+cvkaYy4NT/APFo9l/zl8nsLnor9OPOTfqxpjKnajZZz6sW1vwy8dRrKVjpx6sIrDbgsfE7jTGds1wzfXaityzl8LzLeyXbTp5xji+1LN927uJYGqAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/9k=";
            }
            else
            {
                string base64String = ImageToBase64(LocalFileToImage(Server.MapPath(zone.Minimap)));
                imageMinimap.ImageUrl = "data:image/gif;base64," + base64String;
            }

            //Set up for the Npcs and Items Load
            plhItems.Controls.Clear();
            plhNpcs.Controls.Clear();
            DynamicItems.Clear();
            DynamicNpcs.Clear();

            if(OnDeal || OnDialogue)
            {
                foreach (var x in Game.Map.Zones[Game.Map.PlayerPos].Peoples)
                    if (x.Name == ClientID && !(x.DialogueSprite is null))
                    {
                        string base64String = ImageToBase64(LocalFileToImage(Server.MapPath(x.OverWorldSprite)));
                        System.Web.UI.WebControls.Image imgDialogue = new System.Web.UI.WebControls.ImageButton();
                        imgDialogue.ImageUrl = "data:image/gif;base64," + base64String;
                        imgDialogue.ID = x.Name+"Dialogue";
                        imgDialogue.Attributes["style"] = String.Format("z-index:3; position:inherit; top: {0}px; left: {1}px; height:{2}%; width:auto;", x.Position.Y * 1.87, x.Position.X * 2.1, x.Position.Scale * 16);
                        plhDialogueSprite.Controls.Add(imgDialogue);
                        break;
                    }
            }
            else if(OnFight)
            {
                string base64String = ImageToBase64(LocalFileToImage(Server.MapPath(Enemy.DialogueSprite)));
                System.Web.UI.WebControls.Image imgDialogue = new System.Web.UI.WebControls.ImageButton();
                imgDialogue.ImageUrl = "data:image/gif;base64," + base64String;
                imgDialogue.ID = Enemy.Name;
                imgDialogue.Attributes["style"] = String.Format("z-index:3; position:inherit; top: {0}px; left: {1}px; height:{2}%; width:auto;", Enemy.Position.Y * 1.87, Enemy.Position.X * 2.1, Enemy.Position.Scale * 16);
                plhEnemy1.Controls.Add(imgDialogue);                
            }
            else
            {
                //Load Npcs
                if (!(zone.Peoples is null))
                {
                    foreach (NPC npc in zone.Peoples)
                    {
                        NewImageButtonNpc(npc);
                    }
                }

                //Load Items
                if (!(zone.Items is null))
                {
                    foreach (Item item in zone.Items)
                    {
                        NewImageButtonItem(item);
                    }
                }
            }

            //refresh player's stats
            lblPlayerStats.Text = $"Player Stats({Game.Player.Money}฿)";
            lblPlayerLevel.Text = $"Lvl: {Game.Player.Level} ({Game.Player.Exp}/{Game.Player.LevelCap}xp)";
            lblPlayerHp.Text = $"Hp:{Game.Player.Stats.HP}/{Game.Player.Stats.MaxHP}";
            lblPlayerMana.Text = $"Mana:{Game.Player.Stats.Mana}/{Game.Player.Stats.MaxMana}";
            lblPlayerAttack.Text = "Attack: " + Game.Player.Stats.Attack;
            lblPlayerAttackSpeed.Text = "Attack Speed: " + Game.Player.Stats.AttackSpeed;
            lblPlayerElusiveness.Text = "Elusiveness: " + Game.Player.Stats.Elusiveness;
            lblPlayerAffinity.Text = "Affinity: " + Game.Player.Stats.Affinity;
            lblPlayerIntelligence.Text = "Intelligence: " + Game.Player.Stats.Intelligence;


            /*foreach (Button b in DynamicDialoguesButtons)
            {
                PanelDialogueButtons2.Controls.Add(b);
            }*/

        }
        private string ImageToBase64(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] bites = ms.ToArray();
            string base64String = Convert.ToBase64String(bites, 0, bites.Length);
            return base64String;
        }
        private string ByteToBase64(byte[] imgByte)
        {
            System.Drawing.Image img;
            using (MemoryStream ms = new MemoryStream(imgByte))
            {
                img = System.Drawing.Image.FromStream(ms);
                ms.Close();
            }
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                byte[] bites = ms.ToArray();
                return Convert.ToBase64String(bites, 0, bites.Length);
            }
        }
        private System.Drawing.Image LocalFileToImage(string directory)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(directory);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return img;
            }
            catch (Exception ex)
            {
                Response.Write(String.Format("<script>alert('{0}')</script>", ex));
            }
            return null;
        }
        private void NewImageButtonNpc(NPC npc)
        {
            string base64String = ImageToBase64(LocalFileToImage(Server.MapPath(npc.OverWorldSprite)));
            System.Web.UI.WebControls.ImageButton imageButton = new System.Web.UI.WebControls.ImageButton();
            plhNpcs.Controls.Add(imageButton);
            imageButton.ImageUrl = "data:image/gif;base64," + base64String;
            imageButton.Click += NpcClick;
            imageButton.ID = npc.Name;
            imageButton.Attributes["style"] = String.Format("z-index:3; position:inherit; top: {0}px; left: {1}px; height:{2}%; width:auto;", npc.Position.Y*1.87, npc.Position.X*2.1,npc.Position.Scale*16);
            DynamicNpcs.Add(imageButton);
        }
        private void NewImageButtonItem(Item item)
        {
            string base64String = ImageToBase64(LocalFileToImage(Server.MapPath(item.ItemSprite)));
            System.Web.UI.WebControls.ImageButton imageButton = new System.Web.UI.WebControls.ImageButton();
            plhItems.Controls.Add(imageButton);
            imageButton.ImageUrl = "data:image/gif;base64," + base64String;

            string str = item.GetType().ToString();

            if(item.GetType() == typeof(Item))
                imageButton.Click += ItemClick;
            else if (item.GetType() == typeof(Container))
                imageButton.Click += ContainerClick;
            else if (item.GetType() == typeof(Portal))
                imageButton.Click += PortalClick;
            else if (item.GetType() == typeof(CurrencyItem))
                imageButton.Click += CurrencyItemClick;
            else if (item.GetType() == typeof(Weapon))
                imageButton.Click += ItemClick;
            else if (item.GetType() == typeof(Wearable))
                imageButton.Click += ItemClick;
            imageButton.ID = item.Name;
            imageButton.Attributes["style"] = String.Format("z-index:3; position:inherit; top: {0}px; left: {1}px; height:{2}%; width:auto;", item.Position.Y*1.87, item.Position.X*2.1, item.Position.Scale*16);
            DynamicItems.Add(imageButton);
        }        
        private void AddItem(Item item)
        {
            bool alreadyInInv = false;
            for(int i=0;i< Game.Player.Items.Count;i++)
            {
                if (item.Name == Game.Player.Items[i].Item.Name)
                {
                    Game.Player.Items[i].Quantity++;
                    alreadyInInv = true;
                }
            }

            if(!alreadyInInv)
                Game.Player.Items.Add(new ItemTuple(item, 1));

            lstInventory.DataSource = Game.Player.Items;
            lstInventory.DataBind();
        }
        private void LeftItem(string item)
        {
            for(int i=0;i<Game.Player.Items.Count;i++)
            {
                if(Game.Player.Items[i].Item.Name == item)
                {
                    if(--Game.Player.Items[i].Quantity==0)
                    {
                        List<ItemTuple> lst = new List<ItemTuple>();
                        foreach (ItemTuple x in Game.Player.Items)
                            if (x.Item != Game.Player.Items[i].Item)
                                lst.Add(x);

                        Game.Player.Items = lst;
                        return;
                    }
                }
            }
        }
        private bool CheckLockedZone(Zone z)
        {
            foreach (Item x in z.ItemNeeded)
            {
                bool itemGot = false;
                foreach (ItemTuple y in Game.Player.Items)
                    if (x.Name == y.Item.Name)
                        itemGot = true;
                if (!itemGot)
                    return true;
            }
            return false;
        }
        private bool Equip(int itemInventoryPos)//true equipped false not equipable
        {
            OnEquip = 0;
            OnThrow = 0;
            btnEquip.BackColor = Color.Empty;
            btnThrow.BackColor = Color.Empty;

            if (Game.Player.Items[itemInventoryPos].Item.GetType() == typeof(Wearable))
            {
                if (((Wearable)Game.Player.Items[itemInventoryPos].Item).Parts == Wearable.Part.Helmet)
                {
                    if(Game.Player.Items[itemInventoryPos].Item == Game.Player.Helmet)
                    {
                        Game.Player.Items[itemInventoryPos].Equipped = false;
                        Game.Player.Helmet = null;
                    }
                    else if (Game.Player.Helmet == null)
                    {
                        Game.Player.Helmet = (Wearable)Game.Player.Items[itemInventoryPos].Item;
                        Game.Player.Items[itemInventoryPos].Equipped = true;
                    }
                    else
                    {
                        for (int i = 0; i < Game.Player.Items.Count; i++)
                        {
                            string str = Game.Player.Items[i].Item.GetType().ToString();
                            if (Game.Player.Items[i].Item.Name == Game.Player.Helmet.Name)
                            {
                                Game.Player.Items[i].Equipped = false;
                                Game.Player.Items[itemInventoryPos].Equipped = true;
                                Game.Player.Helmet = (Wearable)Game.Player.Items[itemInventoryPos].Item;
                            }
                        }
                    }

                }
                if (((Wearable)Game.Player.Items[itemInventoryPos].Item).Parts == Wearable.Part.Chestplate)
                {
                    if (Game.Player.Items[itemInventoryPos].Item == Game.Player.Chestplate)
                    {
                        Game.Player.Items[itemInventoryPos].Equipped = false;
                        Game.Player.Chestplate = null;
                    }
                    else if (Game.Player.Chestplate == null)
                    {
                        Game.Player.Chestplate = (Wearable)Game.Player.Items[itemInventoryPos].Item;
                        Game.Player.Items[itemInventoryPos].Equipped = true;
                    }
                    else
                    {
                        for (int i = 0; i < Game.Player.Items.Count; i++)
                        {
                            if (Game.Player.Items[i].Item.Name == Game.Player.Chestplate.Name)
                            {
                                Game.Player.Items[i].Equipped = false;
                                Game.Player.Items[itemInventoryPos].Equipped = true;
                                Game.Player.Chestplate = (Wearable)Game.Player.Items[itemInventoryPos].Item;
                            }
                        }
                    }
                }
                if (((Wearable)Game.Player.Items[itemInventoryPos].Item).Parts == Wearable.Part.Leggins)
                {
                    if (Game.Player.Items[itemInventoryPos].Item == Game.Player.Leggins)
                    {
                        Game.Player.Items[itemInventoryPos].Equipped = false;
                        Game.Player.Leggins = null;
                    }
                    else if (Game.Player.Leggins == null)
                    {
                        Game.Player.Leggins = (Wearable)Game.Player.Items[itemInventoryPos].Item;
                        Game.Player.Items[itemInventoryPos].Equipped = true;
                    }
                    else
                    {
                        for (int i = 0; i < Game.Player.Items.Count; i++)
                        {
                            if (Game.Player.Items[i].Item.Name == Game.Player.Leggins.Name)
                            {
                                Game.Player.Items[i].Equipped = false;
                                Game.Player.Items[itemInventoryPos].Equipped = true;
                                Game.Player.Leggins = (Wearable)Game.Player.Items[itemInventoryPos].Item;
                            }
                        }
                    }
                }
                if (((Wearable)Game.Player.Items[itemInventoryPos].Item).Parts == Wearable.Part.Boots)
                {
                    if (Game.Player.Items[itemInventoryPos].Item == Game.Player.Boots)
                    {
                        Game.Player.Items[itemInventoryPos].Equipped = false;
                        Game.Player.Boots = null;
                    }
                    else if (Game.Player.Boots == null)
                    {
                        Game.Player.Boots = (Wearable)Game.Player.Items[itemInventoryPos].Item;
                        Game.Player.Items[itemInventoryPos].Equipped = true;
                    }
                    else
                    {
                        for (int i = 0; i < Game.Player.Items.Count; i++)
                        {
                            if (Game.Player.Items[i].Item.Name == Game.Player.Boots.Name)
                            {
                                Game.Player.Items[i].Equipped = false;
                                Game.Player.Items[itemInventoryPos].Equipped = true;
                                Game.Player.Boots = (Wearable)Game.Player.Items[itemInventoryPos].Item;
                            }
                        }
                    }
                }
            }
            else if (Game.Player.Items[itemInventoryPos].Item.GetType() == typeof(Weapon))
            {
                if (Game.Player.Items[itemInventoryPos].Item == Game.Player.Weapon)
                {
                    Game.Player.Items[itemInventoryPos].Equipped = false;
                    Game.Player.Weapon = null;
                }
                else if (Game.Player.Weapon == null)
                {
                    Game.Player.Weapon = (Weapon)Game.Player.Items[itemInventoryPos].Item;
                    Game.Player.Items[itemInventoryPos].Equipped = true;
                }
                else
                {
                    for (int i = 0; i < Game.Player.Items.Count; i++)
                    {
                        if (Game.Player.Items[i].Item.Name == Game.Player.Weapon.Name)
                        {
                            Game.Player.Items[i].Equipped = false;
                            Game.Player.Items[itemInventoryPos].Equipped = true;
                            Game.Player.Weapon = (Weapon)Game.Player.Items[itemInventoryPos].Item;
                        }
                    }
                }
            }
            else
                return false;

            lstInventory.Items.Clear();
            lstInventory.DataSource = Game.Player.Items;
            lstInventory.DataBind();
            return true;
        }
        private void EnemyAttack()
        {
            Random rnd = new Random();
            int rand = rnd.Next(0, Enemy.Spells.Count+1);

            try
            {
                if (Enemy.Spells[rand].ManaCost > Enemy.Stats.Mana)
                    throw new Exception();

                Enemy.Stats.Mana -= Enemy.Spells[rand].ManaCost;

                int magicResist = 0;
                if (!(Game.Player.Helmet is null))
                    magicResist += Game.Player.Helmet.MagicResist;
                if (!(Game.Player.Chestplate is null))
                    magicResist += Game.Player.Chestplate.MagicResist;
                if (!(Game.Player.Leggins is null))
                    magicResist += Game.Player.Leggins.MagicResist;
                if (!(Game.Player.Boots is null))
                    magicResist += Game.Player.Boots.MagicResist;

                if (magicResist > Enemy.Spells[rand].MagicPower)
                    magicResist = Enemy.Spells[rand].MagicPower;

                Game.Player.Stats.HP -= (Enemy.Spells[rand].MagicPower - magicResist);
                Enemy.Stats.HP += Enemy.Spells[rand].Healing;

                if (Enemy.Spells[rand].MagicPower > 0 && Enemy.Spells[rand].Healing > 0)
                    FightResult += $"Il nemico ha usato {Enemy.Spells[rand].Name} hai subito {Enemy.Spells[rand].MagicPower - magicResist} danni e si è curato di {Enemy.Spells[rand].Healing} HP\n\n";
                else if (Enemy.Spells[rand].MagicPower > 0)
                    FightResult += $"Il nemico ha usato {Enemy.Spells[rand].Name} e hai subito {Enemy.Spells[rand].MagicPower - magicResist} danni\n\n";
                else if (Enemy.Spells[rand].Healing > 0)
                    FightResult += $"Il nemico ha usato {Enemy.Spells[rand].Name} e si è curato di {Enemy.Spells[rand].Healing} HP\n\n";

                if (Enemy.Stats.HP > Enemy.Stats.MaxHP)
                    Enemy.Stats.HP = Enemy.Stats.MaxHP;
            }
            catch(Exception ex)
            {
                int armor=0;
                if (!(Game.Player.Helmet is null))
                    armor += Game.Player.Helmet.Armor;
                if (!(Game.Player.Chestplate is null))
                    armor += Game.Player.Chestplate.Armor;
                if (!(Game.Player.Leggins is null))
                    armor += Game.Player.Leggins.Armor;
                if (!(Game.Player.Boots is null))
                    armor += Game.Player.Boots.Armor;

                if (!(Enemy.Weapon is null))
                {
                    if (armor > Enemy.Stats.Attack+Enemy.Weapon.AttackDamage)
                        armor = Enemy.Stats.Attack + Enemy.Weapon.AttackDamage;

                    Game.Player.Stats.HP -= Enemy.Stats.Attack + Enemy.Weapon.AttackDamage - armor;

                    FightResult += $"Hai subito {Enemy.Stats.Attack + Enemy.Weapon.AttackDamage - armor} danni\n\n";
                }
                else
                {
                    if (armor > Enemy.Stats.Attack)
                        armor = Enemy.Stats.Attack;

                    Game.Player.Stats.HP -= (Enemy.Stats.Attack - armor);

                    FightResult += $"Hai subito {Enemy.Stats.Attack - armor} danni\n\n";
                }
            }
        }
        private void ShowAlert(Page page, string msg, string url = null)
        {
            page.Response.Write("<script>alert('" +
                msg.Replace("\r", "").Replace("\n", "\\n").Replace("'", "\\'") +
                "');");
            if (url != null)
                page.Response.Write("document.location.href='" + url + "';");
            page.Response.Write("</script>");
        }
        #region
        /*protected void lstInventory_DataBinding(object sender, EventArgs e)
        {
            if(lstInventory.Items.Count>0)
            {
                string str = lstInventory.Items[0].ToString();
                string itemName="", strQuantity="";
                int quantity;
                bool isName=false;
                for(int i=str.Length-1;i>0;i--)
                {
                    if (isName)
                    {
                        itemName += str[i];
                        continue;
                    }
                    if (str[i] == ',')
                        isName = true;
                    strQuantity += str[i];
                }



                quantity = int.Parse(strQuantity);

            }*/
        #endregion Metodi test
    }
}