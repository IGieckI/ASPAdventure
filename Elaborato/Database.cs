using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspAdventureLibrary;
using System.Data.SqlClient;

namespace Elaborato
{
    public static class Database
    {
        public static List<Item> ItemsBase { get; set; }
        public static List<Item> Items { get; set; }

        public static List<NPC> NPCBase { get; set; }
        public static List<NPC> NPC { get; set; }

        public static List<Dialogue> Dialogues { get; set; }
        public static List<Sentence> Sentences { get; set; }

        public static void UpdateList()
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                ItemsBase = new List<Item>();
                Items = new List<Item>();
                NPCBase = new List<NPC>();
                NPC = new List<NPC>();

                conn.Open();

                //Scarico tutti gli items
                SqlCommand command = new SqlCommand($"SELECT * FROM Item;", conn);
                SqlDataReader reader = command.ExecuteReader();
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
                        ItemsBase.Add(new Item(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 0)
                    {
                        ItemsBase.Add(new Portal(id, name, sprite, isKey, sellValue));

                    }
                    else if (itemType == 1)
                    {
                        ItemsBase.Add(new Consumables(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 2)
                    {
                        ItemsBase.Add(new Wearable(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 3)
                    {
                        ItemsBase.Add(new Spell(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 4)
                    {
                        ItemsBase.Add(new Weapon(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 5)
                    {
                        ItemsBase.Add(new CurrencyItem(id, name, sprite, isKey, sellValue));
                    }
                    else if (itemType == 6)
                    {
                        ItemsBase.Add(new Container(id, name, sprite, isKey, sellValue));
                    }
                }
                reader.Close();

                //Aggiungo i dialoghi
                command = new SqlCommand($"SELECT * FROM Dialogue;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Dialogues.Add(new Dialogue(int.Parse(reader[0].ToString()), int.Parse(reader[0].ToString())));
                }
                reader.Close();

                //Scarico le sentences
                command = new SqlCommand($"SELECT * FROM Sentence;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < Dialogues.Count(); i++)
                    {
                        Sentences.Add(new Sentence(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()), reader[2].ToString()));
                    }
                }
                reader.Close();

                //Inserisco gli item giusti nelle sentences
                command = new SqlCommand($"SELECT * FROM ItemInSentence;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < Sentences.Count(); i++)
                    {
                        if(Sentences[i].ID == int.Parse(reader[0].ToString()))
                        {

                            break;
                        }
                    }
                }
                reader.Close();


                command = new SqlCommand($"SELECT * FROM NPC;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if(reader[4] is null)
                    {
                        NPCBase.Add(new NPC(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[4].ToString())));
                    }
                    else if(int.Parse(reader[4].ToString()) == 0)
                    {
                        NPCBase.Add(new EnemyKeyNPC(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[4].ToString())));
                    }
                    else if (int.Parse(reader[4].ToString()) == 0)
                    {
                        NPCBase.Add(new Dealer(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[4].ToString())));
                    }
                }
                reader.Close();


                command = new SqlCommand($"SELECT * FROM NPC_Instantiation;", conn);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    for (int i = 0; i < NPCBase.Count(); i++)
                    {
                        if(NPCBase[i].ID==int.Parse(reader[1].ToString()))
                        {
                            NPC.Add(NPCBase[i]);
                            NPC[NPC.Count() - 1].ID = int.Parse(reader[1].ToString());
                            NPC[NPC.Count() - 1].Position.X = int.Parse(reader[2].ToString());
                            NPC[NPC.Count() - 1].Position.Y = int.Parse(reader[3].ToString());
                            NPC[NPC.Count() - 1].Position.Scale = int.Parse(reader[4].ToString());
                            
                            //Aggiungi dialogues

                            break;
                        }
                    }
                }
                reader.Close();

                //Aggiungo i microdati

                command = new SqlCommand($"SELECT * FROM Item_Instantiation;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for(int i=0;i<ItemsBase.Count;i++)
                    {
                        if(ItemsBase[i].ID == int.Parse(reader[1].ToString()))
                        {
                            Items.Add(GetIDItem(int.Parse(reader[5].ToString())));
                            Items.Add(new ItemTuple(ItemsBase[i],int.Parse(reader[4].ToString())));
                            Items[Items.Count - 1].ID = int.Parse(reader[0].ToString());
                            Items[Items.Count - 1].Position.X = int.Parse(reader[1].ToString());
                            Items[Items.Count - 1].Position.Y = int.Parse(reader[2].ToString());
                            Items[Items.Count - 1].Position.Scale = int.Parse(reader[3].ToString());
                            break;
                        }
                    }                    
                }

                if (ItemsBase[i].GetType() == typeof(Portal))
                {

                }
                else if (ItemsBase[i].GetType() == typeof(Consumables))
                {

                }
                else if (ItemsBase[i].GetType() == typeof(Wearable))
                {

                }
                else if (ItemsBase[i].GetType() == typeof(Spell))
                {

                }
                if (ItemsBase[i].GetType() == typeof(Weapon))
                {

                }
                if (ItemsBase[i].GetType() == typeof(CurrencyItem))
                {

                }
                if (ItemsBase[i].GetType() == typeof(Container))
                {

                }
                reader.Close();
            }
         }

        static Item GetIDItem(int ID)
        {
            foreach(Item i in ItemsBase)
                if (i.ID == ID)
                    return i;
            return null;
        }

        static Item GetIDNPC(int ID)
        {
            foreach (Item i in NPCBase)
                if (i.ID == ID)
                    return i;
            return null;
        }

        public static Player GetPlayer(int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Player WHERE ID = {id};", conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                return (new Player(int.Parse(reader[0].ToString()), reader[2].ToString(), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[18].ToString()));
            }
        }

        public static Zone GetZone(int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Zone WHERE ID = {id};", conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                return (new Zone(int.Parse(reader[0].ToString()), reader[2].ToString(), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[18].ToString()));
            }
        }

        public static Weapon GetWeapon(int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Weapon WHERE ID = {id};",conn);             
                SqlDataReader reader = command.ExecuteReader();
                string name, sprite;
                int attackDamage, criticalChance, magicalPower, weight, sellValue;
                bool isKey;
                reader.Read();
                attackDamage = int.Parse(reader[1].ToString());
                criticalChance = int.Parse(reader[2].ToString());
                magicalPower = int.Parse(reader[3].ToString());
                weight = int.Parse(reader[4].ToString());
                reader.Close();

                command = new SqlCommand($"SELECT * FROM Item WHERE ID = {id};", conn);
                reader = command.ExecuteReader();
                reader.Read();
                name = reader[1].ToString();
                sprite = reader[2].ToString();
                if (int.Parse(reader[3].ToString()) == 1)
                    isKey = true;
                else
                    isKey = false;
                sellValue = int.Parse(reader[4].ToString());
                reader.Close();

                return (new Weapon(name,sprite,isKey,sellValue,attackDamage,criticalChance,magicalPower,weight));
            }
        }

        public static List<NPC> GetNPCS(int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Player WHERE ID = {id};", conn);
                SqlDataReader reader = command.ExecuteReader();
                List<NPC> npcs = new List<NPC>();
                while(reader.Read())
                {
                    npcs.Add(new NPC(reader[]));
                }
                return (new Player(int.Parse(reader[0].ToString()), reader[2].ToString(), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[18].ToString()));
            }
        }

        public static List<Item> GetZoneItems(int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                conn.Open();
                List<ItemTuple> items = new List<Item>();
                SqlCommand command = new SqlCommand($"SELECT * FROM ItemInstatiation WHERE Zone = {id};",conn);             
                SqlDataReader reader = command.ExecuteReader();
                
                while(reader.Read())
                {
                    int pk = int.Parse(reader[0].ToString());
                    int posx = int.Parse(reader[1].ToString());
                    int posy = int.Parse(reader[2].ToString());
                    int scale  = int.Parse(reader[3].ToString());
                    int amount = int.Parse(reader[4].ToString());
                    int itemID = int.Parse(reader[5].ToString());                                                            
                    int player = int.Parse(reader[6].ToString());
                    int Enemy = int.Parse(reader[7].ToString());
                    int Dealer = int.Parse(reader[8].ToString());
                    int Zone = int.Parse(reader[9].ToString());
                    
                    items.Add(new ItemTuple(GetItem(new Item(pk, posx, posy, scale, ), amount))
                    
                }
                
                string name, sprite;
                int attackDamage, criticalChance, magicalPower, weight, sellValue;
                bool isKey;
                reader.Read();
                attackDamage = int.Parse(reader[1].ToString());
                criticalChance = int.Parse(reader[2].ToString());
                magicalPower = int.Parse(reader[3].ToString());
                weight = int.Parse(reader[4].ToString());
                reader.Close();

                command = new SqlCommand($"SELECT * FROM Item WHERE ID = {id};", conn);
                reader = command.ExecuteReader();
                reader.Read();
                name = reader[1].ToString();
                sprite = reader[2].ToString();
                if (int.Parse(reader[3].ToString()) == 1)
                    isKey = true;
                else
                    isKey = false;
                sellValue = int.Parse(reader[4].ToString());
                reader.Close();
            }
        }

        public static List<Dialogue> GetDialogues(int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Player WHERE ID = {id};", conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                }
                return (new Player(int.Parse(reader[0].ToString()), reader[2].ToString(), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[18].ToString()));
            }
        }

        public static Map GetMap(int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Map WHERE ID = {id};", conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int id = int.Parse(reader[0].ToString());
                string name = reader[1].ToString();
                int position = reader[1].ToString();
                reader.Close();
                command = new SqlCommand($"SELECT * FROM Zone WHERE ID = {id};", conn);
                Map map = new Map(int.Parse())
                return map;
            }
        }
    }
}