using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Elaborato
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrore.Visible = false;
        }

        protected void btnLogIn_Click(object sender, EventArgs e)
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

                if(Helper.Authenticate(username.Text,password.Text))
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Username = @Username OR Email = @Username", cnn);
                    command.Parameters.AddWithValue("@Username", username.Text);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Session["Username"] = reader[0].ToString();
                    reader.Close();
                    Response.Redirect("~/UserHomePage.aspx");
                }

                lblErrore.Text = "Dati inseriti non corretti!";
                lblErrore.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrore.Text = ex.Message;
                lblErrore.Visible = true;
            }
        }
    }
}