using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLTaskPlanner;

namespace PLTaskPlannerWeb
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                lblMessage.Text = "Bitte alle Felder ausfüllen.";
                return;
            }

            try
            {
                bool success = Starter.Register(username, password);

                if (success)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    lblMessage.Text = "Registrierung fehlgeschlagen.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}