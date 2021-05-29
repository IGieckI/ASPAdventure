using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Elaborato
{
    public partial class RecoverPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConferma_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                lblMsg.Text = "Non puoi lasciare la password vuota!";
                return;
            }

            if (txtPassword.Text != txtPassword2.Text)
            {
                lblMsg.Text = "Le due password devono corrispondere";
                return;
            }

            SqlConnection cnn = new SqlConnection($"Data Source=(local);Initial Catalog=ASPAdventure; Integrated Security = True;");
            cnn.Open();
            SqlCommand command = new SqlCommand($"UPDATE Users SET Password = '{Helper.HashPassword(txtPassword.Text)}' WHERE Email = '{Session["mail"].ToString()}'", cnn);
            command.ExecuteNonQuery();
            cnn.Close();

            Response.Redirect("~/Response.aspx");
        }
    }
}