using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLTaskPlanner;

namespace PLTaskPlannerWeb
{
    public partial class newtask : Page
    {
        private User CurrentUser
        {
            get
            {
                return Session["User"] as User;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            

            List<Category> categories = Category.GetAllCategories();

            foreach (Category category in categories)
            {
                ddlCategory.Items.Add(
                    new ListItem(
                        category.CategoryName,
                        category.CategoryID.ToString()
                    )
                );
            }

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;

            string description =
                txtDescription.Text;

            if (string.IsNullOrWhiteSpace(title))
            {
                lblMessage.Text =
                    "Bitte einen Titel eingeben.";

                return;
            }

            if (string.IsNullOrWhiteSpace(txtDueDate.Text))
            {
                lblMessage.Text =
                    "Bitte ein Fälligkeitsdatum auswählen.";

                return;
            }

            DateTime dueDate;

            if (!DateTime.TryParse(txtDueDate.Text, out dueDate))
            {
                lblMessage.Text =
                    "Das Datum ist ungültig.";

                return;
            }

            int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);

            bool success =
                CurrentUser.CreateTask(
                    title,
                    description,
                    dueDate,
                    categoryId
                );

            if (success)
            {
                Response.Redirect("Home_page.aspx");
            }
            else
            {
                lblMessage.Text =
                    "Die Aufgabe konnte nicht erstellt werden.";
            }
        }
    }
}