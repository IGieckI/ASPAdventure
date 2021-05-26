using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspAdventureLibrary;

namespace Elaborato
{
    public partial class UserHomePage : System.Web.UI.Page
    {
        List<Player> Characters
        {
            get
            {
                return (List<Player>)Session["Characters"];
            }

            set
            {
                Session["Characters"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Characters = new List<Player>();
            }

            lblErrore.Visible = false;

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

                string s = Session["Username"].ToString();

                SqlCommand command = new SqlCommand($"SELECT * FROM Player WHERE Username = '{Session["Username"]}';", cnn);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //Player p = new Player(int.Parse(reader[0].ToString()), reader[2].ToString(), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), int.Parse(reader[8].ToString()), int.Parse(reader[9].ToString()), int.Parse(reader[10].ToString()), int.Parse(reader[11].ToString()), int.Parse(reader[12].ToString()), int.Parse(reader[13].ToString()), int.Parse(reader[14].ToString()), int.Parse(reader[15].ToString()), int.Parse(reader[16].ToString()), int.Parse(reader[17].ToString()), int.Parse(reader[17].ToString()));
                    Player p = new Player((int)reader[0], reader[2].ToString(), (int)reader[8], (int)reader[10], reader[19].ToString());
                    Characters.Add(p);
                }
                grdCharacters.DataSource = Characters;
                grdCharacters.DataBind();

            }
            catch (Exception ex)
            {
                lblErrore.Text = ex.Message;
                lblErrore.Visible = true;
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void grdCharacters_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Session["PlayerID"] = Characters[e.NewSelectedIndex].ID;
            Response.Redirect("~/Default.aspx");
        }
    }
}