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

                SqlDataReader OutPutSelectAll;
                SqlCommand command;
                String sql;
                SqlDataAdapter adapter = new SqlDataAdapter();
                cnn.Open();
                if (username.Text.Contains('@'))
                    sql = "SELECT Email,Password FROM dbo.Users";
                else
                    sql = "SELECT Username,Password FROM dbo.Users";
                command = new SqlCommand(sql, cnn);
                OutPutSelectAll = command.ExecuteReader();
                bool found = false;

                while (OutPutSelectAll.Read() && found == false)
                {
                    if (username.Text == OutPutSelectAll[0].ToString() && password.Text == OutPutSelectAll[1].ToString())
                    {
                        OutPutSelectAll.Close();
                        Session["Username"] = username.Text;
                        Response.Redirect("~/UserHomePage.aspx");
                    }

                }
                OutPutSelectAll.Close();
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