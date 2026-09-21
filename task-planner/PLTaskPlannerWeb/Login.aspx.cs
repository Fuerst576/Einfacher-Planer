using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLTaskPlanner;

namespace PLTaskPlannerWeb
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                lblMessage.Text =
                    "Bitte Benutzername und Passwort eingeben.";

                return;
            }

            User user = Starter.Login(username, password);

            if (user != null)
            {
                Session["User"] = user;

                Response.Redirect("Home_page.aspx");
            }
            else
            {
                lblMessage.Text =
                    "Benutzername oder Passwort ist falsch.";
            }
        }
    }
}