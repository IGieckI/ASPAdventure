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
        public Weapon GetWeapon(int id)
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
    }
}