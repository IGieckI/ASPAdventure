using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspAdventureLibrary;
using System.Data.SqlClient;
using System.Linq;

namespace Elaborato
{
    public static class Database
    {
        public static Game Get(int PlayerID)
        {
            Game game;
            List<Item> itemsBase = new List<Item>();
            List<Item> items = new List<Item>();
            List<NPC> npcBase = new List<NPC>();
            List<NPC> npc = new List<NPC>();
            List<Dialogue> dialogues = new List<Dialogue>();
            List<Sentence> sentences = new List<Sentence>();
            Map map;
            List<Zone> zones = new List<Zone>();

            using (SqlConnection conn = new SqlConnection("Data Source = (local); Initial Catalog = ASPAdventure; Integrated Security=True;"))
            {
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
                command = new SqlCommand($"SELECT * FROM Answer;", conn);
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

                //Scarico Map
                command = new SqlCommand($"SELECT * FROM Map WHERE ID = {PlayerID};", conn);
                reader = command.ExecuteReader();
                reader.Read();
                map = new Map(reader[1].ToString());
                reader.Close();

                //Scarico le zone e le inserisco in map
                command = new SqlCommand($"SELECT * FROM Zone WHERE ID = {PlayerID};", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    map.Zones.Add(new Zone((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), (int)reader[4], (int)reader[5], (int)reader[6], (int)reader[7], reader[8].ToString())); ;
                }
                reader.Close();

                //Scarico gli npcs
                command = new SqlCommand($"SELECT * FROM npc;", conn);
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

                //Scarico i dati specifici degli npc locali e li aggiungo alla mappa
                command = new SqlCommand($"SELECT * FROM npc_Instantiation;", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    map.Zones[(int)reader[6]].Peoples.Add(npcBase.Find(a => a.ID == (int)reader[1]));
                    map.Zones[(int)reader[6]].Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].ID = int.Parse(reader[0].ToString());
                    map.Zones[(int)reader[6]].Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].Position.X = int.Parse(reader[2].ToString());
                    map.Zones[(int)reader[6]].Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].Position.Y = int.Parse(reader[3].ToString());
                    map.Zones[(int)reader[6]].Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].Position.Scale = int.Parse(reader[4].ToString());


                    map.Zones[(int)reader[6]].Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].AlreadySpoken = (int)reader[5] == 0 ? false : true;
                    map.Zones[(int)reader[6]].Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].Dialogue = dialogues.Find(a => a.DialogueID == map.Zones[(int)reader[6]].Peoples[map.Zones[(int)reader[6]].Peoples.Count() - 1].DialogueID);
                }
                reader.Close();

                return null;

                /*if (itemsBase[i].GetType() == typeof(Portal))
                {

                }
                else if (itemsBase[i].GetType() == typeof(Consumables))
                {

                }
                else if (itemsBase[i].GetType() == typeof(Wearable))
                {

                }
                else if (itemsBase[i].GetType() == typeof(Spell))
                {

                }
                if (itemsBase[i].GetType() == typeof(Weapon))
                {

                }
                if (itemsBase[i].GetType() == typeof(CurrencyItem))
                {

                }
                if (itemsBase[i].GetType() == typeof(Container))
                {

                }
                reader.Close();*/
            }
        }

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
    }
}
