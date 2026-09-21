using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLTaskPlanner;

namespace PLTaskPlannerWeb
{
    public partial class tasklist : Page
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
                LoadTasks();
            }
        }

        private void LoadCategories()
        {
            

            ddlCategory.Items.Add(
                new ListItem("Alle", "0")
            );

            List<Category> categories =
                Category.GetAllCategories();

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

        private void LoadTasks()
        {
            int categoryId;

            int.TryParse(ddlCategory.SelectedValue, out categoryId);
            
            List<Task> tasks;

            if (categoryId == 0)
            {
                tasks = CurrentUser.GetTasks();
            }
            else
            {
                tasks =
                    CurrentUser.GetTasksByCategory(
                        categoryId
                    );
            }

            rptTasks.DataSource = tasks;
            rptTasks.DataBind();
        }

        protected void ddlCategory_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            LoadTasks();
        }

        protected void imgStatus_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton button = (ImageButton)sender;

            int taskId;

            int.TryParse(button.CommandArgument, out taskId);

            Task task = CurrentUser.GetTasks()
                .FirstOrDefault(t => t.TaskID == taskId);

            task.IsCompleted = !task.IsCompleted;

            if (task.Update())
            {
                LoadTasks();
            }
        }

        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton button = (ImageButton)sender;

            int taskId;

            int.TryParse(button.CommandArgument, out taskId);

            Task task = CurrentUser.GetTasks()
                .FirstOrDefault(t => t.TaskID == taskId);

            if (task.Delete())
            {
                LoadTasks();
            }
        }

        public string GetCategoryName(int categoryId)
        {
            Category category = Category.GetAllCategories()
                .FirstOrDefault(
                    c => c.CategoryID == categoryId
                );

            return category != null
                ? category.CategoryName
                : "";
        }


    }
}