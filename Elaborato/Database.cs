using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspAdventureLibrary;
using System.Data.SqlClient;

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
        Map map;
        List<Zone> zones = new List<Zone>();
        Player player;
        public Game Load(int username, int characterID)
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                conn.Open();

                //Scarico Map
                SqlCommand command = new SqlCommand($"SELECT * FROM Map WHERE ID = {characterID};", conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                map = new Map(reader[1].ToString());
                map.PlayerPos = (int)reader[2];
                map.ID = (int)reader[0];
                reader.Close();

                //Scarico le zone e le inserisco in map
                command = new SqlCommand($"SELECT * FROM Zone WHERE ID = {characterID};", conn);
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
                    bool isKey;
                    isKey = (int.Parse(reader[3].ToString()) == 0) ? false : true;
                    int sellValue = int.Parse(reader[4].ToString());
                    int itemType = int.Parse(reader[5].ToString());

                    if (reader[5] is null)
                    {
                        itemsBase.Add(new Item(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 0)
                    {
                        itemsBase.Add(new Portal(id, name, sprite, isKey, sellValue));

                    }
                    else if (itemType == 1)
                    {
                        itemsBase.Add(new Consumables(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 2)
                    {
                        itemsBase.Add(new Wearable(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 3)
                    {
                        itemsBase.Add(new Spell(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 4)
                    {
                        itemsBase.Add(new Weapon(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 5)
                    {
                        itemsBase.Add(new CurrencyItem(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 6)
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
                    ((Container)itemsBase.Find(a => a.ID == (int)reader[0])).RemoveAfeterUnlock = (bool)reader[1];
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM ContainerGive;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ((Container)itemsBase.Find(a => a.ID == (int)reader[0])).ItemDrop.Add(new ItemTuple(itemsBase.Find(b=>b.ID==(int)reader[1]),(int)reader[2]));
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM ContainerRequirement;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
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
                    ((Portal)itemsBase.Find(a => a.ID == (int)reader[0])).RemoveAfetrEntrance = (int)reader[1] == 1 ? true : false;
                    ((Portal)itemsBase.Find(a => a.ID == (int)reader[0])).ZonePointer = (int)reader[2];
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM PortalRequest;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
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
                command = new SqlCommand($"SELECT * FROM Weapon;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
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
                    for (int i = 0; i < dialogues.Count(); i++)
                    {
                        sentences.Add(new Sentence(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()), reader[2].ToString()));
                    }
                }
                reader.Close();

                //Inserisco gli item giusti nelle sentences
                command = new SqlCommand($"SELECT * FROM ItemInSentence;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sentences.Find(a => a.ID == int.Parse(reader[0].ToString())).ItemGive.Add(new ItemTuple(itemsBase.Find(b => b.ID == int.Parse(reader[1].ToString())), (int)reader[2]));
                }
                reader.Close();

                //Scarico e inserisco le answers
                command = new SqlCommand($"SELECT * FROM Answer WHERE ;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sentences.Find(a => a.ID == (int)reader[3]).Answers.Add(new KeyValuePair<int, string>((int)reader[2], reader[1].ToString()));
                }
                reader.Close();

                //Inserisco le sentences nei dialogues
                for (int i = 0; i < sentences.Count(); i++)
                {
                    dialogues.Find(a => a.DialogueID == sentences[i].DialogueID).Spiching.Add(sentences[i]);
                }

                //Scarico gli npcs
                command = new SqlCommand($"SELECT * FROM NPC;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[4] is null)
                    {
                        npcBase.Add(new NPC(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[4].ToString())));
                    }
                    else if (int.Parse(reader[4].ToString()) == 0)
                    {
                        npcBase.Add(new EnemyKeyNPC(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[4].ToString())));
                    }
                    else if (int.Parse(reader[4].ToString()) == 0)
                    {
                        npcBase.Add(new Dealer(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[4].ToString())));
                    }
                }
                reader.Close();

                //Inserisco i dati nei dealer
                command = new SqlCommand($"SELECT * FROM DealerInventory;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ((Dealer)npcBase.Find(a => a.ID == (int)reader[0])).Shop.Add(new ItemTuple(itemsBase.Find(b => b.ID == (int)reader[1]), (int)reader[2]));
                }
                reader.Close();

                //Imposto i dati degli enemy
                command = new SqlCommand($"SELECT * FROM Enemy;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Helmet = (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[1].ToString()));
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Chestplate = (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[2].ToString()));
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Leggins = (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[3].ToString()));
                    ((EnemyKeyNPC)npcBase.Find(a => a.ID == (int)reader[0])).Boots = (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[4].ToString()));
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
                command = new SqlCommand($"SELECT * FROM NpcInstantiation WHERE PlayerID = {characterID};", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples.Add(npcBase.Find(a => a.ID == (int)reader[1]));
                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].ID = int.Parse(reader[0].ToString());
                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].Position.X = int.Parse(reader[2].ToString());
                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].Position.Y = int.Parse(reader[3].ToString());
                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].Position.Scale = int.Parse(reader[4].ToString());
                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].AlreadySpoken = (int)reader[5] == 0 ? false : true;
                    map.Zones.Find(a => a.ID == (int)reader[6]).Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].Dialogue = dialogues.Find(a => a.DialogueID == map.Zones[(int)reader[6]].Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].DialogueID);
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM ItemInstantiation WHERE Player = {characterID};", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    map.Zones.Find(a => a.ID == (int)reader[6]).Items.Add(itemsBase.Find(b => b.ID == (int)reader[4]));
                    map.Zones.Find(a => a.ID == (int)reader[6]).Items[map.Zones.Find(a => a.ID == (int)reader[6]).Items.Count - 1].Position.X = (int)reader[1];
                    map.Zones.Find(a => a.ID == (int)reader[6]).Items[map.Zones.Find(a => a.ID == (int)reader[6]).Items.Count - 1].Position.Y = (int)reader[2];
                    map.Zones.Find(a => a.ID == (int)reader[6]).Items[map.Zones.Find(a => a.ID == (int)reader[6]).Items.Count - 1].Position.Scale = (int)reader[3];
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM DealerInstantiation WHERE PlayerID = {characterID};", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Add(npcBase.Find(a => a.ID == (int)reader[1]));
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples[map.Zones[(int)reader[5]].Peoples.Count() - 1].ID = int.Parse(reader[0].ToString());
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples[map.Zones[(int)reader[5]].Peoples.Count() - 1].Position.X = int.Parse(reader[2].ToString());
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples[map.Zones[(int)reader[5]].Peoples.Count() - 1].Position.Y = int.Parse(reader[3].ToString());
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples[map.Zones[(int)reader[5]].Peoples.Count() - 1].Position.Scale = int.Parse(reader[4].ToString());
                }
                reader.Close();

                command = new SqlCommand($"SELECT * FROM EnemyNPCInstantiation WHERE PlayerID = {characterID};", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples.Add(npcBase.Find(a => a.ID == (int)reader[1]));
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples[map.Zones[(int)reader[5]].Peoples.Count() - 1].ID = int.Parse(reader[0].ToString());
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples[map.Zones[(int)reader[5]].Peoples.Count() - 1].Position.X = int.Parse(reader[2].ToString());
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples[map.Zones[(int)reader[5]].Peoples.Count() - 1].Position.Y = int.Parse(reader[3].ToString());
                    map.Zones.Find(a => a.ID == (int)reader[5]).Peoples[map.Zones[(int)reader[5]].Peoples.Count() - 1].Position.Scale = int.Parse(reader[4].ToString());
                }
                reader.Close();

                //Create the player
                command = new SqlCommand($"SELECT * FROM Player WHERE Username = {username} AND ID = {characterID};", conn);
                reader = command.ExecuteReader();
                reader.Read();
                player = new Player(int.Parse(reader[0].ToString()), reader[2].ToString(), (Wearable)itemsBase.Find(a=>a.ID== int.Parse(reader[3].ToString())), (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[4].ToString())), (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[5].ToString())), (Wearable)itemsBase.Find(a => a.ID == int.Parse(reader[6].ToString())), (Weapon)itemsBase.Find(a => a.ID == int.Parse(reader[3].ToString())), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[18].ToString()));
                reader.Close();

                return new Game(itemsBase, npcBase, map, player);
            }
        }
        public void Save(Game game, int username, int characterID)
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                conn.Open();

                //Inserisco il player
                SqlCommand command = new SqlCommand($"UPDATE Player SET Helmet = @Helmet WHERE ID = {characterID}");
                if (game.Player.Helmet is null)
                    command.Parameters.AddWithValue("@Helmet", null);
                else
                    command.Parameters.AddWithValue("@Helmet", game.Player.Helmet.ID);

                command = new SqlCommand($"UPDATE Player SET Chestplate = @Chestplate WHERE ID = {characterID}");
                if (game.Player.Chestplate is null)
                    command.Parameters.AddWithValue("@Chestplate", null);
                else
                    command.Parameters.AddWithValue("@Chestplate", game.Player.Chestplate.ID);

                command = new SqlCommand($"UPDATE Player SET Leggins = @Leggins WHERE ID = {characterID}");
                if (game.Player.Leggins is null)
                    command.Parameters.AddWithValue("@Leggins", null);
                else
                    command.Parameters.AddWithValue("@Leggins", game.Player.Leggins.ID);

                command = new SqlCommand($"UPDATE Player SET Boots = @Boots WHERE ID = {characterID}");
                if (game.Player.Boots is null)
                    command.Parameters.AddWithValue("@Boots", null);
                else
                    command.Parameters.AddWithValue("@Boots", game.Player.Boots.ID);
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Weapon = @Weapon WHERE ID = {characterID}");
                if (game.Player.Weapon is null)
                    command.Parameters.AddWithValue("@Weapon", null);
                else
                    command.Parameters.AddWithValue("@Weapon", game.Player.Weapon.ID);
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Level = {game.Player.Level} WHERE ID = {characterID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Exp = {game.Player.Exp} WHERE ID = {characterID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Money = {game.Player.Money} WHERE ID = {characterID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET HP = {game.Player.Stats.HP} WHERE ID = {characterID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET MaxHP = {game.Player.Stats.MaxHP} WHERE ID = {characterID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Mana = {game.Player.Stats.Mana} WHERE ID = {characterID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET MaxMana = {game.Player.Stats.MaxMana} WHERE ID = {characterID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Attack = {game.Player.Stats.Attack} WHERE ID = {characterID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET AttackSpeed = {game.Player.Stats.AttackSpeed} WHERE ID = {characterID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Elusiveness = {game.Player.Stats.Elusiveness} WHERE ID = {characterID}");
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Player SET Intelligence = {game.Player.Stats.Intelligence} WHERE ID = {characterID}");
                command.ExecuteNonQuery();


                command = new SqlCommand($"DELETE FROM PlayerItem WHERE ID = {characterID}");
                command.ExecuteNonQuery();

                for(int i=0;i<game.Player.Items.Count;i++)
                {
                    if (game.Player.Items[i].Item.GetType() == typeof(Spell))
                        continue;
                    command = new SqlCommand($"INSERT INTO PlayerItem VALUES({characterID},{game.Player.Items[i].Item.ID},{game.Player.Items[i].Quantity})");
                    command.ExecuteNonQuery();
                }

                for (int i = 0; i < game.Player.Items.Count; i++)
                {
                    if (game.Player.Items[i].Item.GetType() == typeof(Spell))
                    {
                        command = new SqlCommand($"INSERT INTO PlayerSpell VALUES({characterID},{game.Player.Items[i].Item.ID})");
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
                        command = new SqlCommand($"INSERT INTO ItemInstantiation(PositionX,PositionY,Scale,Item,Player,Zone) VALUES({item.Position.X},{item.Position.X},{item.Position.Scale},{item.ID},{characterID},{z.ID})");
                        command.ExecuteNonQuery();
                    }

                    foreach (NPC npc in z.Peoples)
                    {
                        command = new SqlCommand($"INSERT INTO ItemInstantiation(NPCID,PositionX,PositionY,Scale,AlreadySpoken,Zone,PlayerID) VALUES({npc.ID},{npc.Position.X},{npc.Position.X},{npc.Position.Scale},{npc.AlreadySpoken},{z.ID},{characterID})");
                        command.ExecuteNonQuery();
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
