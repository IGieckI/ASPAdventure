using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspAdventureLibrary;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Elaborato
{
    public class Database
    {
        List<Item> itemsBase = new List<Item>();
        List<Item> items = new List<Item>();
        List<NPC> npcBase = new List<NPC>();
        List<NPC> npc = new List<NPC>();
        List<Dialogue> dialogues = new List<Dialogue>();
        List<Sentence> sentences = new List<Sentence>();
        Map map = new Map();
        List<Zone> zones = new List<Zone>();
        Player player;
        public Game Load(string username, int playerID)
        {
            SqlConnection conn;

            conn = new SqlConnection($"Data Source=(local);Initial Catalog=ASPAdventure;User ID=sa;Password=burbero2020");
            try
            {
                conn.Open();
                conn.Close();
            }
            catch
            {
                conn = new SqlConnection($"Data Source=(local);Initial Catalog=ASPAdventure; Integrated Security = True;");
            }
            conn.Open();

            SqlCommand command;
            SqlDataReader reader;

                //Istanzio Map
                //command = new SqlCommand($"SELECT * FROM Map WHERE ID = {playerID};", conn);
                //reader = command.ExecuteReader();
                //reader.Read();
                //map = new Map(reader[1].ToString());
                //map.PlayerPos = (int)reader[2];
                //map.ID = (int)reader[0];
                //reader.Close();

                //Scarico le zone e le inserisco in map
                command = new SqlCommand($"SELECT * FROM Zone;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    map.Zones.Add(new Zone((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), (int)reader[4], (int)reader[5], (int)reader[6], (int)reader[7], reader[8].ToString()));
                }
                reader.Close();

                //Scarico tutti gli items
                command = new SqlCommand($"SELECT * FROM Item;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader[0].ToString());
                    string name = reader[1].ToString();
                    string sprite = reader[2].ToString();
                    bool isKey = (bool)reader[3];
                    int sellValue = int.Parse(reader[4].ToString());

                    string str = reader[5].ToString();

                    if (reader[5] == DBNull.Value)
                    {
                        itemsBase.Add(new Item(id, name, sprite, isKey, sellValue));
                    }
                    else if ((int)reader[5] == 0)
                    {
                        itemsBase.Add(new Portal(id, name, sprite, isKey, sellValue));

                    }
                    else if ((int)reader[5] == 1)
                    {
                        itemsBase.Add(new Consumables(id, name, sprite, isKey, sellValue));
                    }
                    else if ((int)reader[5] == 2)
                    {
                        itemsBase.Add(new Wearable(id, name, sprite, isKey, sellValue));
                    }
                    else if ((int)reader[5] == 3)
                    {
                        itemsBase.Add(new Spell(id, name, sprite, isKey, sellValue));
                    }
                    else if ((int)reader[5] == 4)
                    {
                        itemsBase.Add(new Weapon(id, name, sprite, isKey, sellValue));
                    }
                    else if ((int)reader[5] == 5)
                    {
                        itemsBase.Add(new CurrencyItem(id, name, sprite, isKey, sellValue));
                    }
                    else if ((int)reader[5] == 6)
                    {
                        itemsBase.Add(new Container(id, name, sprite, isKey, sellValue));
                    }
                }
                reader.Close();

                //Imposto le proprietà nei consumables
                command = new SqlCommand($"SELECT * FROM Consumables;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ((Consumables)itemsBase.Find(a => a.ID == (int)reader[0])).Heal = (int)reader[1];
                    ((Consumables)itemsBase.Find(a => a.ID == (int)reader[0])).Mana = (int)reader[2];
                }
                reader.Close();

                //Imposto le proprietà nei Container
                command = new SqlCommand($"SELECT * FROM Container;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ((Container)itemsBase.Find(a => a.ID == (int)reader[0])).RemoveAfterUnlock = (bool)reader[1];
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM ContainerGive;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if(((Container)itemsBase.Find(a => a.ID == (int)reader[0])).ItemDrop is null)
                        ((Container)itemsBase.Find(a => a.ID == (int)reader[0])).ItemDrop = new List<ItemTuple>();

                    ((Container)itemsBase.Find(a => a.ID == (int)reader[0])).ItemDrop.Add(new ItemTuple(itemsBase.Find(b=>b.ID==(int)reader[1]),(int)reader[2]));
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM ContainerRequirement;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (((Container)itemsBase.Find(a => a.ID == (int)reader[0])).ItemRequest is null)
                        ((Container)itemsBase.Find(a => a.ID == (int)reader[0])).ItemRequest = new List<ItemTuple>();

                    ((Container)itemsBase.Find(a => a.ID == (int)reader[0])).ItemRequest.Add(new ItemTuple(itemsBase.Find(b => b.ID == (int)reader[1]), (int)reader[2]));
                }
                reader.Close();

                //Imposto le proprietà di Spell
                command = new SqlCommand($"SELECT * FROM Spell;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ((Spell)itemsBase.Find(a => a.ID == (int)reader[0])).Healing = (int)reader[1];
                    ((Spell)itemsBase.Find(a => a.ID == (int)reader[0])).MagicPower = (int)reader[2];
                    ((Spell)itemsBase.Find(a => a.ID == (int)reader[0])).ManaCost = (int)reader[3];
                }
                reader.Close();

                //Imposto le proprietà nei Portal
                command = new SqlCommand($"SELECT * FROM Portal;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ((Portal)itemsBase.Find(a => a.ID == (int)reader[0])).RemoveAfetrEntrance = (bool)reader[1];
                    ((Portal)itemsBase.Find(a => a.ID == (int)reader[0])).ZonePointer = (int)reader[2];
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM PortalRequest;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (((Portal)itemsBase.Find(a => a.ID == (int)reader[0])).ItemRequest is null)
                        ((Portal)itemsBase.Find(a => a.ID == (int)reader[0])).ItemRequest = new List<ItemTuple>();

                        ((Portal)itemsBase.Find(a => a.ID == (int)reader[0])).ItemRequest.Add(new ItemTuple(itemsBase.Find(b => b.ID == (int)reader[1]), (int)reader[2]));
                }
                reader.Close();

                //Imposto le proprietà nei Weapon
                command = new SqlCommand($"SELECT * FROM Weapon;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ((Weapon)itemsBase.Find(a => a.ID == (int)reader[0])).AttackDamage = (int)reader[1];
                    ((Weapon)itemsBase.Find(a => a.ID == (int)reader[0])).Crit = (int)reader[2];
                    ((Weapon)itemsBase.Find(a => a.ID == (int)reader[0])).MagicPower = (int)reader[3];
                    ((Weapon)itemsBase.Find(a => a.ID == (int)reader[0])).Weight = (int)reader[4];
                }
                reader.Close();

                //Imposto le proprietà nei Wearable
                command = new SqlCommand($"SELECT * FROM Wearables;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                     string str = reader[0].ToString();
                    ((Wearable)itemsBase.Find(a => a.ID == (int)reader[0])).Armor = (int)reader[1];
                    ((Wearable)itemsBase.Find(a => a.ID == (int)reader[0])).MagicResist = (int)reader[2];
                    ((Wearable)itemsBase.Find(a => a.ID == (int)reader[0])).ManaRegen = (int)reader[3];
                    ((Wearable)itemsBase.Find(a => a.ID == (int)reader[0])).Weight = (int)reader[4];
                    switch ((int)reader[5])
                    {
                        case 0:
                            ((Wearable)itemsBase.Find(a => a.ID == (int)reader[0])).Parts = Wearable.Part.Helmet;
                            break;
                        case 1:
                            ((Wearable)itemsBase.Find(a => a.ID == (int)reader[0])).Parts = Wearable.Part.Chestplate;
                            break;
                        case 2:
                            ((Wearable)itemsBase.Find(a => a.ID == (int)reader[0])).Parts = Wearable.Part.Leggins;
                            break;
                        case 3:
                            ((Wearable)itemsBase.Find(a => a.ID == (int)reader[0])).Parts = Wearable.Part.Boots;
                            break;
                    }
                }
                reader.Close();

                //Aggiungo i dialoghi
                command = new SqlCommand($"SELECT * FROM Dialogue;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dialogues.Add(new Dialogue(int.Parse(reader[0].ToString()), int.Parse(reader[0].ToString())));
                }
                reader.Close();

                //Scarico le sentences
                command = new SqlCommand($"SELECT * FROM Sentence;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                     sentences.Add(new Sentence(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()), reader[2].ToString()));
                }
                reader.Close();

                //Inserisco gli item giusti nelle sentences
                command = new SqlCommand($"SELECT * FROM ItemInSentence;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                     if (sentences.Find(a => a.ID == int.Parse(reader[0].ToString())).ItemGive is null)
                         sentences.Find(a => a.ID == int.Parse(reader[0].ToString())).ItemGive = new List<ItemTuple>();

                    sentences.Find(a => a.ID == int.Parse(reader[0].ToString())).ItemGive.Add(new ItemTuple(itemsBase.Find(b => b.ID == int.Parse(reader[1].ToString())), (int)reader[2]));
                }
                reader.Close();

                //Scarico e inserisco le answers
                command = new SqlCommand($"SELECT * FROM Answer;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (sentences.Find(a => a.ID == (int)reader[3]).Answers is null)
                        sentences.Find(a => a.ID == (int)reader[3]).Answers = new List<KeyValuePair<int, string>>();
                    sentences.Find(a => a.ID == (int)reader[3]).Answers.Add(new KeyValuePair<int, string>((int)reader[2], reader[1].ToString()));
                }
                reader.Close();

                //Inserisco le sentences nei dialogues
                for (int i = 0; i < sentences.Count(); i++)
                {
                    if (dialogues.Find(a => a.DialogueID == sentences[i].DialogueID).Spiching is null)
                        dialogues.Find(a => a.DialogueID == sentences[i].DialogueID).Spiching = new List<Sentence>();
                    dialogues.Find(a => a.DialogueID == sentences[i].DialogueID).Spiching.Add(sentences[i]);
                }

                //Scarico gli npcs
                command = new SqlCommand($"SELECT * FROM NPC;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["NpcType"] == DBNull.Value)
                    {
                    if (reader[4] == DBNull.Value)
                        npcBase.Add(new NPC(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), -1));
                    else
                        npcBase.Add(new NPC(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[4].ToString())));
                }
                    else if (int.Parse(reader["NpcType"].ToString()) == 0)
                    {
                    if (reader[4] == DBNull.Value)
                        npcBase.Add(new EnemyKeyNPC(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), -1));
                    else
                        npcBase.Add(new EnemyKeyNPC(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[4].ToString())));
                }
                    else if (int.Parse(reader["NpcType"].ToString()) == 1)
                    {
                        if(reader[4] == DBNull.Value)
                            npcBase.Add(new Dealer(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), -1));
                        else
                            npcBase.Add(new Dealer(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[4].ToString())));
                    }
                }
                reader.Close();

                //Inserisco i dati nei dealer
                command = new SqlCommand($"SELECT * FROM DealerInstantiation WHERE PlayerID = {playerID};", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ((Dealer)npcBase.Find(a => a.ID == (int)reader[1])).Url = reader[0].ToString();
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM DealerInventory;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ((Dealer)npcBase.Find(a => a.Url == reader[0].ToString())).Shop.Add(new ItemTuple(itemsBase.Find(b => b.ID == (int)reader[1]), (int)reader[2]));
                }
              reader.Close();

                //Imposto i dati degli enemy
                command = new SqlCommand($"SELECT * FROM Enemy;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if(reader[1] == DBNull.Value)
                        ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Helmet = null;
                    else
                        ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Helmet = (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[1].ToString()));

                    if (reader[2] == DBNull.Value)
                        ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Chestplate = null;
                    else
                        ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Chestplate = (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[2].ToString()));

                    if (reader[3] == DBNull.Value)
                        ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Leggins = null;
                    else
                        ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Leggins = (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[3].ToString()));

                    if (reader[4] == DBNull.Value)
                        ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Boots = null;
                    else
                        ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Boots = (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[4].ToString()));

                    if (reader[5] == DBNull.Value)
                        ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Weapon = null;
                    else
                        ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Weapon = (Weapon)itemsBase.Find(a => a.ID == int.Parse(reader[5].ToString()));

                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Experience = (int)reader[6];
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).EscapePerc = (int)reader[7];
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Money = (int)reader[8];
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Stats.HP = (int)reader[9];
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Stats.MaxHP = (int)reader[10];
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Stats.Mana = (int)reader[11];
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Stats.MaxMana = (int)reader[12];
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Stats.Attack = (int)reader[13];
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Stats.AttackSpeed = (int)reader[14];
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Stats.Elusiveness = (int)reader[15];
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM EnemyDrop;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Drop is null)
                        ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Drop = new Drop();
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Drop.Items.Add(new ItemTuple(itemsBase.Find(b => b.ID == (int)reader[1]), (int)reader[2]));
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM EnemySpells;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Spells.Add((Spell)itemsBase.Find(b=>b.ID==(int)reader[1]));
                }
                reader.Close();

                //Scarico i dati specifici degli npc locali e li aggiungo alla mappa
                command = new SqlCommand($"SELECT * FROM NpcInstantiation WHERE PlayerID = {playerID};", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples.Add(npcBase.Find(a => a.ID == (int)reader[1]));

                    if (map.Zones.Find(a => a.ID == (int)reader[6]).Peoples.Find(b => b.ID == (int)reader[1]).Position is null)
                        map.Zones.Find(a => a.ID == (int)reader[6]).Peoples.Find(b => b.ID == (int)reader[1]).Position = new ElementPosition();

                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples.Find(b => b.ID == (int)reader[1]).Position.X = (int)reader[2];
                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples.Find(b => b.ID == (int)reader[1]).Position.Y = (int)reader[3];
                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples.Find(b => b.ID == (int)reader[1]).Position.Scale = (int)reader[4];
                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples.Find(b => b.ID == (int)reader[1]).AlreadySpoken = (bool)reader[5];
                //map.Zones.Find(a => a.ID == (int)reader[6]).Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].Dialogue = dialogues.Find(a => a.DialogueID == map.Zones[(int)reader[6]].Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].DialogueID);
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM ItemInstantiation WHERE Player = {playerID};", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    map.Zones.Find(a => a.ID == (int)reader[6]).Items.Add(itemsBase.Find(b => b.ID == (int)reader[4]));
                    map.Zones.Find(a => a.ID == (int)reader[6]).Items[map.Zones.Find(a => a.ID == (int)reader[6]).Items.Count - 1].Position.X = (int)reader[1];
                    map.Zones.Find(a => a.ID == (int)reader[6]).Items[map.Zones.Find(a => a.ID == (int)reader[6]).Items.Count - 1].Position.Y = (int)reader[2];
                    map.Zones.Find(a => a.ID == (int)reader[6]).Items[map.Zones.Find(a => a.ID == (int)reader[6]).Items.Count - 1].Position.Scale = (int)reader[3];
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM DealerInstantiation WHERE PlayerID = {playerID};", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Add(npcBase.Find(a => a.ID == (int)reader[1]));

                    if (map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Find(b => b.ID == (int)reader[1]).Position is null)
                        map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Find(b => b.ID == (int)reader[1]).Position = new ElementPosition();

                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Find(b => b.ID == (int)reader[1]).Position.X = (int)reader[2];
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Find(b => b.ID == (int)reader[1]).Position.Y = (int)reader[3];
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Find(b => b.ID == (int)reader[1]).Position.Scale = (int)reader[4];                    
                }
            reader.Close();

            command = new SqlCommand($"SELECT * FROM EnemyNPCInstantiation WHERE PlayerID = {playerID};", conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Add(npcBase.Find(a => a.ID == (int)reader[1]));

                if (map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Find(b => b.ID == (int)reader[1]).Position is null)
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Find(b => b.ID == (int)reader[1]).Position = new ElementPosition();

                map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Find(b => b.ID == (int)reader[1]).Position.X = (int)reader[2];
                map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Find(b => b.ID == (int)reader[1]).Position.Y = (int)reader[3];
                map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Find(b => b.ID == (int)reader[1]).Position.Scale = (int)reader[4];
            }
            reader.Close();

            //Create the player
            command = new SqlCommand($"SELECT * FROM Player WHERE Username = '{username}' AND ID = {playerID};", conn);
            reader = command.ExecuteReader();
            reader.Read();
            player = new Player(int.Parse(reader[0].ToString()), reader[2].ToString(), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[18].ToString()));
            if(!(reader[3] == DBNull.Value)) 
                player.Helmet=(Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[3].ToString()));

            if (!(reader[4] == DBNull.Value))
                player.Chestplate = (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[4].ToString()));

            if (!(reader[5] == DBNull.Value))
                player.Leggins = (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[5].ToString()));

            if (!(reader[6] == DBNull.Value))
                player.Boots = (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[6].ToString()));

            if (!(reader[7] == DBNull.Value))
                player.Weapon = (Weapon)itemsBase.Find(a => a.ID == int.Parse(reader[7].ToString()));

            if (reader["PlayerPos"] == DBNull.Value)
                map.PlayerPos = 1;
            else
                map.PlayerPos = (int)reader["PlayerPos"];
            reader.Close();

            return new Game(itemsBase, npcBase, map, player);
        }


        public void Save(Game game, int username, int playerID)
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                conn.Open();

                //Inserisco il player
                SqlCommand command = new SqlCommand($"UPDATE Player SET Helmet = @Helmet WHERE ID = {playerID}");
                if (game.Player.Helmet is null)
                    command.Parameters.AddWithValue("@Helmet", null);
                else
                    command.Parameters.AddWithValue("@Helmet", game.Player.Helmet.ID);

                command = new SqlCommand($"UPDATE Player SET Chestplate = @Chestplate WHERE ID = {playerID}");
                if (game.Player.Chestplate is null)
                    command.Parameters.AddWithValue("@Chestplate", null);
                else
                    command.Parameters.AddWithValue("@Chestplate", game.Player.Chestplate.ID);

                command = new SqlCommand($"UPDATE Player SET Leggins = @Leggins WHERE ID = {playerID}");
                if (game.Player.Leggins is null)
                    command.Parameters.AddWithValue("@Leggins", null);
                else
                    command.Parameters.AddWithValue("@Leggins", game.Player.Leggins.ID);

                command = new SqlCommand($"UPDATE Player SET Boots = @Boots WHERE ID = {playerID}");
                if (game.Player.Boots is null)
                    command.Parameters.AddWithValue("@Boots", null);
                else
                    command.Parameters.AddWithValue("@Boots", game.Player.Boots.ID);
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Weapon = @Weapon WHERE ID = {playerID}");
                if (game.Player.Weapon is null)
                    command.Parameters.AddWithValue("@Weapon", null);
                else
                    command.Parameters.AddWithValue("@Weapon", game.Player.Weapon.ID);
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Level = {game.Player.Level} WHERE ID = {playerID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Exp = {game.Player.Exp} WHERE ID = {playerID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Money = {game.Player.Money} WHERE ID = {playerID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET HP = {game.Player.Stats.HP} WHERE ID = {playerID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET MaxHP = {game.Player.Stats.MaxHP} WHERE ID = {playerID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Mana = {game.Player.Stats.Mana} WHERE ID = {playerID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET MaxMana = {game.Player.Stats.MaxMana} WHERE ID = {playerID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Attack = {game.Player.Stats.Attack} WHERE ID = {playerID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET AttackSpeed = {game.Player.Stats.AttackSpeed} WHERE ID = {playerID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Elusiveness = {game.Player.Stats.Elusiveness} WHERE ID = {playerID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Intelligence = {game.Player.Stats.Intelligence} WHERE ID = {playerID}");
                command.ExecuteNonQuery();


                command = new SqlCommand($"DELETE FROM PlayerItem WHERE ID = {playerID}");
                command.ExecuteNonQuery();

                for(int i=0;i<game.Player.Items.Count;i++)
                {
                    if (game.Player.Items[i].Item.GetType() == typeof(Spell))
                        continue;
                    command = new SqlCommand($"INSERT INTO PlayerItem VALUES({playerID},{game.Player.Items[i].Item.ID},{game.Player.Items[i].Quantity})");
                    command.ExecuteNonQuery();
                }

                for (int i = 0; i < game.Player.Items.Count; i++)
                {
                    if (game.Player.Items[i].Item.GetType() == typeof(Spell))
                    {
                        command = new SqlCommand($"INSERT INTO PlayerSpell VALUES({playerID},{game.Player.Items[i].Item.ID})");
                        command.ExecuteNonQuery();
                    }
                }

                //Update Map
                command = new SqlCommand($"UPDATE Map SET PlayerPos = {game.Map.PlayerPos} WHERE ID = {game.Map.ID}");
                command.ExecuteNonQuery();

                //Update Zones
                foreach(Zone z in game.Map.Zones)
                {
                    command = new SqlCommand($"DELETE FROM ItemInstantiation WHERE Zone = {z.ID}");
                    command.ExecuteNonQuery();

                    foreach (Item item in z.Items)
                    {
                        command = new SqlCommand($"INSERT INTO ItemInstantiation(PositionX,PositionY,Scale,Item,Player,Zone) VALUES({item.Position.X},{item.Position.X},{item.Position.Scale},{item.ID},{playerID},{z.ID})");
                        command.ExecuteNonQuery();                                              
                    }

                    command = new SqlCommand($"DELETE FROM NpcInstantiation WHERE Zone = {z.ID}");
                    command.ExecuteNonQuery();

                    foreach (NPC npc in z.Peoples)
                    {
                        command = new SqlCommand($"INSERT INTO ItemInstantiation(NPCID,PositionX,PositionY,Scale,AlreadySpoken,Zone,PlayerID) VALUES({npc.ID},{npc.Position.X},{npc.Position.X},{npc.Position.Scale},{npc.AlreadySpoken},{z.ID},{playerID})");
                        command.ExecuteNonQuery();
                    }
                }
            }        
        }

        public static int GetLastIndex(SqlConnection conn,string tabella)
        {
            conn.InfoMessage += Conn_InfoMessage;
            SqlCommand command = new SqlCommand($"DBCC CHECKIDENT('{tabella}', NORESEED)", conn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
            return index;

        }

        static int index = 0;
        private static void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            string s = e.Message;
            if (s.Contains("DBCC")) return;
            string idx = "";
            bool go = true;
            for (int i = 65; i < s.Length && go; i++)
            {
                if (s[i] == '\'')
                    go = false;
                else if (s[i] == 'N')
                {

                    idx = "NULL";
                    go = false;
                }
                else
                    idx += s[i];

            }

            if (idx == "NULL")
            {
                index = 0;
            }
            else
                try
                {

                    index = int.Parse(idx);
                }
                catch (Exception)
                {
                    index = 0;
                }
        }

        public void NewCharacter(string username, string name, string description)
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                //Aggiungo il nuovo character
                SqlCommand command = new SqlCommand("INSERT INTO Player(Helmet,Chestplate,Leggins,Boots,Weapon,Level,Exp,Money,Hp,MaxHp,Mana,MaxMana,Attack,AttackSpeed,Elusiveness,Intelligence) VALUES(SELECT Helmet,Chestplate,Leggins,Boots,Weapon,Level,Exp,Money,Hp,MaxHp,Mana,MaxMana,Attack,AttackSpeed,Elusiveness,Intelligence FROM Player WHERE ID = 0;);", conn);
                command.ExecuteNonQuery();
                int lastIndex = GetLastIndex(conn,"Player");
                command = new SqlCommand($"UPDATE Player SET Username = {username} WHERE ID = {lastIndex};",conn);
                command.ExecuteNonQuery();
                command = new SqlCommand($"UPDATE Player SET Name = {name} WHERE ID = {lastIndex};", conn);
                command.ExecuteNonQuery();
                command = new SqlCommand($"UPDATE Player SET Description = {description} WHERE ID = {lastIndex};", conn);
                command.ExecuteNonQuery();
                //Immetto gli item nell'inventario del player(inizio game)
                command = new SqlCommand("SELECT * FROM PlayerItem WHERE WHERE Player = 0;", conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SqlCommand command2 = new SqlCommand($"INSERT INTO PlayerItem VALUES({lastIndex},{(int)reader[1]},{(int)reader[2]});", conn);
                    command2.ExecuteNonQuery();
                }

                //Immetto gli oggetti nelle stanze
                command = new SqlCommand("SELECT * FROM ItemInstantiation WHERE WHERE Player = 0;", conn);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    SqlCommand command2 = new SqlCommand($"INSERT INTO ItemInstantiation(PositionX,PositionY,Scale,Item,Player,Zone) VALUES({(int)reader[1]},{(int)reader[2]},{(int)reader[3]},{(int)reader[4]},{lastIndex},{(int)reader[6]});", conn);
                    command2.ExecuteNonQuery();
                }

                command = new SqlCommand("SELECT * FROM ContainerIstance WHERE WHERE Player = 0;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SqlCommand command2 = new SqlCommand($"INSERT INTO ContainerIstance(AlreadyOpen,ItemIstID,Player) VALUES({reader[1]},{(int)reader[2]},{lastIndex});", conn);
                    command2.ExecuteNonQuery();
                }

                //Immetto gli npc nelle stanze
                command = new SqlCommand("SELECT * FROM NpcInstantiation WHERE WHERE Player = 0;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SqlCommand command2 = new SqlCommand($"INSERT INTO NpcInstantiation(NPCID,PositionX,PositionY,Scale,AlreadySpoken,Zone,PlayerID) VALUES({(int)reader[1]},{(int)reader[2]},{(int)reader[3]},{(int)reader[4]},{reader[5]},{(int)reader[6]},{lastIndex});", conn);
                    command2.ExecuteNonQuery();
                }
                                
                command = new SqlCommand("SELECT * FROM EnemyNPCInstantiation WHERE WHERE Player = 0;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SqlCommand command2 = new SqlCommand($"INSERT INTO EnemyNPCInstantiation(Enemy,PositionX,PositionY,Scale,Zone,PlayerID) VALUES({(int)reader[1]},{(int)reader[2]},{(int)reader[3]},{(int)reader[4]},{(int)reader[5]},{lastIndex});", conn);
                    command2.ExecuteNonQuery();
                }

                command = new SqlCommand("SELECT * FROM DealerInstantiation WHERE WHERE Player = 0;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SqlCommand command2 = new SqlCommand($"INSERT INTO DealerInstantiation(NPCID,PositionX,PositionY,Scale,Zone,PlayerID) VALUES({(int)reader[1]},{(int)reader[2]},{(int)reader[3]},{(int)reader[4]},{(int)reader[5]},{lastIndex});", conn);
                    command2.ExecuteNonQuery();

                    command2 = new SqlCommand($"SELECT * FROM DealerInventory WHERE WHERE Player = 0 AND DealerID = {(int)reader[1]};", conn);
                    SqlDataReader reader2 = command.ExecuteReader();
                    while (reader2.Read())
                    {
                        SqlCommand command3 = new SqlCommand($"INSERT INTO DealerInventory(DealerID,ID,Amount) VALUES({GetLastIndex(conn,"DealerInventory")},{(int)reader[1]},{(int)reader[2]});", conn);
                        command3.ExecuteNonQuery();
                    }
                }
            }
        }

        #region Codice Vecchio
        //static Item GetIDItem(int ID)
        //{
        //    foreach (Item i in itemsBase)
        //        if (i.ID == ID)
        //            return i;
        //    return null;
        //}

        //static Item GetIDnpc(int ID)
        //{
        //    foreach (Item i in npcBase)
        //        if (i.ID == ID)
        //            return i;
        //    return null;
        //}

        //public static Player GetPlayer(int id)
        //{
        //    using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
        //    {
        //        conn.Open();
        //        SqlCommand command = new SqlCommand($"SELECT * FROM Player WHERE ID = {id};", conn);
        //        SqlDataReader reader = command.ExecuteReader();
        //        reader.Read();
        //        return (new Player(int.Parse(reader[0].ToString()), reader[2].ToString(), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[18].ToString()));
        //    }
        //}

        //public static Zone GetZone(int id)
        //{
        //    using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
        //    {
        //        conn.Open();
        //        SqlCommand command = new SqlCommand($"SELECT * FROM Zone WHERE ID = {id};", conn);
        //        SqlDataReader reader = command.ExecuteReader();
        //        reader.Read();
        //        return (new Zone(int.Parse(reader[0].ToString()), reader[2].ToString(), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[18].ToString()));
        //    }
        //}

        //public static Weapon GetWeapon(int id)
        //{
        //    using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
        //    {
        //        conn.Open();
        //        SqlCommand command = new SqlCommand($"SELECT * FROM Weapon WHERE ID = {id};", conn);
        //        SqlDataReader reader = command.ExecuteReader();
        //        string name, sprite;
        //        int attackDamage, criticalChance, magicalPower, weight, sellValue;
        //        bool isKey;
        //        reader.Read();
        //        attackDamage = int.Parse(reader[1].ToString());
        //        criticalChance = int.Parse(reader[2].ToString());
        //        magicalPower = int.Parse(reader[3].ToString());
        //        weight = int.Parse(reader[4].ToString());
        //        reader.Close();

        //        command = new SqlCommand($"SELECT * FROM Item WHERE ID = {id};", conn);
        //        reader = command.ExecuteReader();
        //        reader.Read();
        //        name = reader[1].ToString();
        //        sprite = reader[2].ToString();
        //        if (int.Parse(reader[3].ToString()) == 1)
        //            isKey = true;
        //        else
        //            isKey = false;
        //        sellValue = int.Parse(reader[4].ToString());
        //        reader.Close();

        //        return (new Weapon(name, sprite, isKey, sellValue, attackDamage, criticalChance, magicalPower, weight));
        //    }
        //}

        //public static List<npc> GetnpcS(int id)
        //{
        //    using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
        //    {
        //        conn.Open();
        //        SqlCommand command = new SqlCommand($"SELECT * FROM Player WHERE ID = {id};", conn);
        //        SqlDataReader reader = command.ExecuteReader();
        //        List<npc> npcs = new List<npc>();
        //        while (reader.Read())
        //        {
        //            npcs.Add(new npc(reader[]));
        //        }
        //        return (new Player(int.Parse(reader[0].ToString()), reader[2].ToString(), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[18].ToString()));
        //    }
        //}

        //public static List<Item> GetZoneitems(int id)
        //{
        //    using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
        //    {
        //        conn.Open();
        //        List<ItemTuple> items = new List<Item>();
        //        SqlCommand command = new SqlCommand($"SELECT * FROM ItemInstatiation WHERE Zone = {id};", conn);
        //        SqlDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            int pk = int.Parse(reader[0].ToString());
        //            int posx = int.Parse(reader[1].ToString());
        //            int posy = int.Parse(reader[2].ToString());
        //            int scale = int.Parse(reader[3].ToString());
        //            int amount = int.Parse(reader[4].ToString());
        //            int itemID = int.Parse(reader[5].ToString());
        //            int player = int.Parse(reader[6].ToString());
        //            int Enemy = int.Parse(reader[7].ToString());
        //            int Dealer = int.Parse(reader[8].ToString());
        //            int Zone = int.Parse(reader[9].ToString());

        //            items.Add(new ItemTuple(GetItem(new Item(pk, posx, posy, scale, ), amount))


        //        }

        //        string name, sprite;
        //        int attackDamage, criticalChance, magicalPower, weight, sellValue;
        //        bool isKey;
        //        reader.Read();
        //        attackDamage = int.Parse(reader[1].ToString());
        //        criticalChance = int.Parse(reader[2].ToString());
        //        magicalPower = int.Parse(reader[3].ToString());
        //        weight = int.Parse(reader[4].ToString());
        //        reader.Close();

        //        command = new SqlCommand($"SELECT * FROM Item WHERE ID = {id};", conn);
        //        reader = command.ExecuteReader();
        //        reader.Read();
        //        name = reader[1].ToString();
        //        sprite = reader[2].ToString();
        //        if (int.Parse(reader[3].ToString()) == 1)
        //            isKey = true;
        //        else
        //            isKey = false;
        //        sellValue = int.Parse(reader[4].ToString());
        //        reader.Close();
        //    }
        //}

        //public static List<Dialogue> Getdialogues(int id)
        //{
        //    using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
        //    {
        //        conn.Open();
        //        SqlCommand command = new SqlCommand($"SELECT * FROM Player WHERE ID = {id};", conn);
        //        SqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {

        //        }
        //        return (new Player(int.Parse(reader[0].ToString()), reader[2].ToString(), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[18].ToString()));
        //    }
        //}

        //public static Map GetMap(int id)
        //{
        //    using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
        //    {
        //        conn.Open();
        //        SqlCommand command = new SqlCommand($"SELECT * FROM Map WHERE ID = {id};", conn);
        //        SqlDataReader reader = command.ExecuteReader();
        //        reader.Read();
        //        int id = int.Parse(reader[0].ToString());
        //        string name = reader[1].ToString();
        //        int position = reader[1].ToString();
        //        reader.Close();
        //        command = new SqlCommand($"SELECT * FROM Zone WHERE ID = {id};", conn);
        //        Map map = new Map(int.Parse())
        //        return map;
        //    }
        //}
        #endregion
}
}
