using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;

namespace Elaborato
{
    public static class Helper
    {
        public static void SendMail(string destinatario, string oggetto, string contenuto)
        {
            SmtpClient Client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "aspadventurerecovery@gmail.com",
                    Password = "aspadventure"
                }
            };


            MailAddress FromMail = new MailAddress("aspadventurerecovery@gmail.com", "ASP Adventure");
            MailAddress ToMail = new MailAddress(destinatario, "Someone");
            MailMessage Message = new MailMessage()
            {
                From = FromMail,
                Subject = oggetto,
                Body = contenuto
            };
            Message.To.Add(ToMail);
            Client.Send(Message);
        }

        //From Toni Greco
        public static string HashPassword(string pwd)
        {
            byte[] salt = new byte[32];
            var csp = new RNGCryptoServiceProvider();
            csp.GetBytes(salt);
            Rfc2898DeriveBytes hashObj = new Rfc2898DeriveBytes(pwd, salt, 10000);
            byte[] hash = hashObj.GetBytes(32);
            // Mette insieme salt e hash.
            byte[] crypts = new byte[64];
            Array.Copy(salt, 0, crypts, 0, 32);
            Array.Copy(hash, 0, crypts, 32, 32);
            return Convert.ToBase64String(crypts);
        }

        //From Toni Greco
        public static bool Authenticate(string user, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection($"Data Source=(local);Initial Catalog=ASPAdventure; Integrated Security = True;"))
                {
                    // Recupera le credenziali dal DB.
                    conn.Open();

                    SqlCommand test = new SqlCommand("SELECT * FROM Users WHERE Email = @Email OR Username = @Username", conn);
                    test.Parameters.AddWithValue("@Email", user);
                    test.Parameters.AddWithValue("@Username", user);

                    SqlDataReader reader = test.ExecuteReader();
                    reader.Read();
                    var pwd = reader["Password"];
                    if (pwd != null)
                    {
                        // Ricava un vettore di byte dal codice hash ed ...
                        byte[] hashBytesDB = Convert.FromBase64String(pwd.ToString());
                        // ... estrapola il salt.
                        byte[] saltDB = new byte[32];
                        Array.Copy(hashBytesDB, 0, saltDB, 0, 32);
                        // Usando il salt salvato nel DB calcola l'hash della password
                        // inserita dall'utente e ne ricava il vettore di byte.
                        var pbkdf2User = new Rfc2898DeriveBytes(password, saltDB, 10000);
                        byte[] hashDBUser = pbkdf2User.GetBytes(32);
                        // Compara i due vettori.
                        for (int i = 0; i < 32; i++)
                            if (hashBytesDB[i + 32] != hashDBUser[i])
                                return false;
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}