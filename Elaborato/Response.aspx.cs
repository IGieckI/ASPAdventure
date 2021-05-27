using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Elaborato
{
    public partial class RegisterOk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!(Session["Messaggio pagina conferma"] is null))
                if (Session["Messaggio pagina conferma"].ToString() == "Qualcosa è andato storto")
                {
                    lblResponse.Text = "Qualcosa è andato storto";
                    icon.ImageUrl = "Immagini/Error.png";
                }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}