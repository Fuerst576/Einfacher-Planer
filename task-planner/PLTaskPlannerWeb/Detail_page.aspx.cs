using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLTaskPlanner;

namespace PLTaskPlannerWeb
{
    public partial class taskdetails : Page
    {
        private User CurrentUser
        {
            get
            {
                return Session["User"] as User;
            }
        }

        private int TaskID
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["TaskID"], out id);
                return id;
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadCategories();
                LoadTask();
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

        private void LoadTask()
        {
            List<Task> tasks = CurrentUser.GetTasks();

            Task task = tasks.FirstOrDefault(
                t => t.TaskID == TaskID
            );

            txtTitle.Text = task.Title;

            txtDescription.Text = task.Description;

            txtDueDate.Text =
                task.DueDate.ToString("yyyy-MM-dd");

            chkCompleted.Checked =
                task.IsCompleted;

            ddlCategory.SelectedValue =
                task.CategoryID.ToString();
        }

        protected void btnSave_Click(
            object sender,
            EventArgs e)
        {
            string title =
                txtTitle.Text.Trim();

            string description =
                txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                lblMessage.Text =
                    "Bitte einen Titel eingeben.";

                return;
            }

            DateTime dueDate;

            if (!DateTime.TryParse(
                txtDueDate.Text,
                out dueDate))
            {
                lblMessage.Text =
                    "Bitte ein gültiges Datum eingeben.";

                return;
            }

            int categoryId;

            if (!int.TryParse(
                ddlCategory.SelectedValue,
                out categoryId))
            {
                lblMessage.Text =
                    "Bitte eine Kategorie auswählen.";

                return;
            }

            List<Task> tasks = CurrentUser.GetTasks();

            Task task = tasks.FirstOrDefault(
                t => t.TaskID == TaskID
            );

            task.Title = title;
            task.Description = description;
            task.DueDate = dueDate;
            task.IsCompleted = chkCompleted.Checked;
            task.CategoryID = categoryId;

            if (task.Update())
            {
                Response.Redirect("Home_page.aspx");
            }
            else
            {
                lblMessage.Text =
                    "Die Aufgabe konnte nicht geändert werden.";
            }
        }
    }
}

