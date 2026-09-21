using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTaskPlanner
{
    public class User
    {
        public int UserID { get; private set; }
        public string Username { get; set; } = string.Empty;

        internal User(int userId, string username)
        {
            this.UserID = userId;
            this.Username = username;
        }

        public List<Task> GetTasks()
        {
            List<Task> tasks = new List<Task>();

            if (UserID == 0) return tasks;

            try
            {
                using (SqlConnection connection = Starter.getConnection())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TaskID, Title, Description, DueDate, IsCompleted, UserID, CategoryID FROM Tasks WHERE UserID = @userId", connection);
                    cmd.Parameters.Add(new SqlParameter("@userId", UserID));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new Task(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.IsDBNull(2) ? "" : reader.GetString(2),
                                reader.GetDateTime(3),
                                reader.GetBoolean(4),
                                reader.GetInt32(5),
                                reader.GetInt32(6)
                            ));
                        }
                    }
                }
            }
            catch (SqlException)
            {

            }
            return tasks;
        }

        public List<Task> GetTasksByCategory(int categoryId)
        {
            List<Task> filteredTasks = new List<Task>();

            if (UserID == 0 || categoryId == 0)
            {
                return filteredTasks;
            }

            try
            {
                using (SqlConnection connection = Starter.getConnection())
                {
                    connection.Open();
                    string sql = "SELECT TaskID, Title, Description, DueDate, IsCompleted, UserID, CategoryID " +
                                 "FROM Tasks WHERE UserID = @userId AND CategoryID = @catId";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", this.UserID);
                        cmd.Parameters.AddWithValue("@catId", categoryId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                filteredTasks.Add(new Task(
                                    reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    reader.GetDateTime(3),
                                    reader.GetBoolean(4),
                                    reader.GetInt32(5),
                                    reader.GetInt32(6)
                                ));
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {

            }
            return filteredTasks;
        }

        public bool CreateTask(string title, string description, DateTime dueDate, int categoryId)
        {
            if (UserID == 0 || categoryId == 0 || string.IsNullOrWhiteSpace(title))
            {
                return false;
            }

            try
            {
                using (SqlConnection connection = Starter.getConnection())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Tasks (Title, Description, DueDate, IsCompleted, UserID, CategoryID) " +
                        "VALUES (@title, @desc, @date, 0, @userId, @catId)", connection);

                    cmd.Parameters.Add(new SqlParameter("@title", title));
                    cmd.Parameters.Add(new SqlParameter("@desc", (object)description ?? DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@date", dueDate));
                    cmd.Parameters.Add(new SqlParameter("@userId", UserID));
                    cmd.Parameters.Add(new SqlParameter("@catId", categoryId));

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException)
            {
            }
            return false;
        }
    }
}
