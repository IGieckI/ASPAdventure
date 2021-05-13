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
                string connectionString;
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
                {
                    lblErrore.Text = "Inserisci una mail valida!";
                    lblErrore.Visible = false;
                }

                SqlCommand command = new SqlCommand(@"INSERT INTO Users VALUES(@username,@mail,@password)", cnn);
                command.Parameters.AddWithValue("@username", username.Text);
                command.Parameters.AddWithValue("@mail", email.Text);
                command.Parameters.AddWithValue("@password", password.Text);
                command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                lblErrore.Text = ex.Message;
                lblErrore.Visible = true;
            }
        }
    }
}