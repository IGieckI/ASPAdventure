using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Elaborato
{
    public partial class Recover : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConferma_Click(object sender, EventArgs e)
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

            if(Session["OTP"] is null)
            {
                Session["mail"] = txtEmail.Text;
                SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE Email = '{txtEmail.Text}';", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Session["OTP"] = SendMail(reader["Email"].ToString());
                    lblMsg.Text = "Inserisci il codice OTP";
                    txtEmail.Text = "";
                    return;
                }
                lblErrore.Text = "Email inserita non corretta";
            }
            else
            {

                if(txtEmail.Text == Session["OTP"].ToString())
                    Response.Redirect("~/RecoverPassword.aspx");
            }
            
            
        }

        private int SendMail(string email)
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

            Random rnd = new Random();
            int otp = rnd.Next(10000, 1000000);

            try
            {
                Helper.SendMail(email, "Recupero credenziali ASPAdventure", $"Codice OTP: {otp}");
                Session["Messaggio pagina conferma"] = "Mail inviata";

            }
            catch
            {
                Session["Messaggio pagina conferma"] = "Qualcosa è andato storto";
            }

            return otp;
        }
    }
}