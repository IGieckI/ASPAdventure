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
        public static List<ItemTuple> ItemsBase {get;set;}
        public static List<ItemTuple> Items {get;set;}
        
        public static void UpdateList()
        {
            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
                ItemsBase = new List<ItemTuple>();
                
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Item;", conn);
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    int pk = int.Parse(reader[0].ToString());
                    string name = reader[1].ToString();
                    string sprite = reader[2].ToString();
                    bool isKey;
                    int.Parse(reader[3].ToString()) == 0 ? isKey = false; : isKey = true;
                    int sellValue = int.Parse(reader[4].ToString());
                    int itemType = int.Parse(reader[5].ToString());
                    
                    if(itemType is null)
                    {
                        ItemsBase.Add(new ItemTuple(new Item(pk, name, sprite, isKey, sellValue),amount));
                    }
                    else if(itemType == 0)
                    {
                        ItemsBase.Add(new ItemTuple(new Portal(pk, name, sprite, isKey, sellValue),amount));
                        
                    }
                    else if(itemType == 1)
                    {
                        ItemsBase.Add(new ItemTuple(new Consumables(pk, name, sprite, isKey, sellValue),amount));
                    }
                    else if(itemType == 2)
                    {
                        ItemsBase.Add(new ItemTuple(new Wearable(pk, name, sprite, isKey, sellValue),amount));
                    }
                    else if(itemType == 3)
                    {
                        ItemsBase.Add(new ItemTuple(new Spell(pk, name, sprite, isKey, sellValue),amount));
                    }
                    else if(itemType == 4)
                    {
                        ItemsBase.Add(new ItemTuple(new Weapon(pk, name, sprite, isKey, sellValue),amount));
                    }
                    else if(itemType == 5)
                    {
                        ItemsBase.Add(new ItemTuple(new CurrencyItem(pk, name, sprite, isKey, sellValue),amount));
                    }else if(itemType == 6)
                    {
                        ItemsBase.Add(new ItemTuple(new Container(pk, name, sprite, isKey, sellValue),amount));
                    }
                }
                reader.Close();
                
                //Aggiungo i microdati
                
                //while(read) non for, devi farlo per ogni elemento in instantiation
                
                for(int i=0;i<ItemsBase.Count;i++)
                {
                    if(ItemsBase[i].GetType() == typeof(Portal))
                    {
                        command = new SqlCommand($"SELECT * FROM ItemInstantiation WHERE ;", conn);   
                        I
                    }
                    else if(ItemsBase[i].GetType() == typeof(Consumables))
                    {
                        
                    }
                    else if(ItemsBase[i].GetType() == typeof(Wearable))
                    {
                        
                    }
                    else if(ItemsBase[i].GetType() == typeof(Spell))
                    {
                        
                    }
                    if(ItemsBase[i].GetType() == typeof(Weapon))
                    {
                        
                    }
                    if(ItemsBase[i].GetType() == typeof(CurrencyItem))
                    {
                        
                    }
                    if(ItemsBase[i].GetType() == typeof(Container))
                    {
                        
                    }    
                }
                reader.Close();
            }
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