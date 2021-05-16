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

        public static List<Item> GetItems(int id)
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
                SqlCommand command = new SqlCommand($"SELECT * FROM Zone WHERE ID = {id};", conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                return (new Zone(int.Parse(reader[0].ToString()), reader[2].ToString(), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[18].ToString()));
            }
        }
    }
}