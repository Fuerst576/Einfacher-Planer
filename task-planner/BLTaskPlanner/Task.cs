using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BLTaskPlanner
{
    public class Task
    {
        public int TaskID { get; internal set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int UserID { get; private set; }
        public int CategoryID { get; set; }

        internal Task(int taskId, string title, string description, DateTime dueDate, bool isCompleted, int userId, int categoryId)
        {
            this.TaskID = taskId;
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            this.IsCompleted = isCompleted;
            this.UserID = userId;
            this.CategoryID = categoryId;
        }

        public bool Update()
        {
            if (TaskID == 0 || UserID == 0 || string.IsNullOrWhiteSpace(Title)) return false;

            try
            {
                using (SqlConnection connection = Starter.getConnection())
                {
                    connection.Open();
                    string sql = "UPDATE Tasks " +
                                 "SET Title = @title, Description = @desc, DueDate = @date, " +
                                 "    IsCompleted = @done, CategoryID = @catId " +
                                 "WHERE TaskID = @taskId AND UserID = @userId";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@title", Title);
                        cmd.Parameters.AddWithValue("@desc", Description ?? "");
                        cmd.Parameters.AddWithValue("@date", DueDate);
                        cmd.Parameters.AddWithValue("@done", IsCompleted);
                        cmd.Parameters.AddWithValue("@catId", CategoryID);
                        cmd.Parameters.AddWithValue("@taskId", TaskID);
                        cmd.Parameters.AddWithValue("@userId", UserID);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool Delete()
        {
            if (TaskID == 0 || UserID == 0)
            {
                return false;
            }

            try
            {
                using (SqlConnection connection = Starter.getConnection())
                {
                    connection.Open();
                    string sql = "DELETE FROM Tasks WHERE TaskID = @taskId AND UserID = @userId";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@taskId", TaskID);
                        cmd.Parameters.AddWithValue("@userId", UserID);

                        bool success = cmd.ExecuteNonQuery() > 0;

                        if (success)
                        {
                            TaskID = 0;
                        }

                        return success;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }
    }
}
