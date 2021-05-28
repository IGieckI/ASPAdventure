using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Elaborato
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrore.Visible = false;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cnn;

                cnn = new SqlConnection($"Data Source=(local);Initial Catalog=ASPAdventure;User ID=sa;Password=burbero2020");
                try
                {
                    cnn.Open();
                    cnn.Close();
                }
                catch
                {
                    cnn = new SqlConnection($"Data Source=(local);Initial Catalog=ASPAdventure; Integrated Security = True;");
                }

                cnn.Open();

                if (!email.Text.Contains('@'))
                    throw new Exception("Inserisci una mail valida!");

                if (EmailExist(email.Text, cnn))
                    throw new Exception("Questa mail è già stata usata");

                if(password.Text != confermaPassword.Text)
                    throw new Exception("Le due password non corrispondono!");

                SqlCommand command = new SqlCommand(@"INSERT INTO Users VALUES(@username,@password,@mail,0)", cnn);
                command.Parameters.AddWithValue("@username", username.Text);
                command.Parameters.AddWithValue("@mail", email.Text);
                command.Parameters.AddWithValue("@password", Helper.HashPassword(password.Text));
                command.ExecuteNonQuery();
                cnn.Close();

                Helper.SendMail(email.Text, "Registrazione ASPAdventure", $"Ciao {username.Text}, la tua registrazione ad ASPAdventure è avvenuta con successo, buon divertimento!");
                Response.Redirect("~/Response.aspx");
            }
            catch (Exception ex)
            {
                lblErrore.Text = ex.Message;
                lblErrore.Visible = true;
            }
        }

        private bool EmailExist(string email, SqlConnection cnn)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE Email = '{email}';",cnn);
            command.Parameters.AddWithValue("@mail", email);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    return true;
                return false;
            }                
        }
    }
}