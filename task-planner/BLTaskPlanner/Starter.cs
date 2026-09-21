using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace BLTaskPlanner
{
    public static class Starter
    {
        public static SqlConnection getConnection()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lukas\Downloads\task-planner (1)\task-planner\DBTaskPlanner\DBTaskPlanner.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        public static User Login(string username, string password)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    connection.Open();
                    string sql = "SELECT UserID, Username FROM Users WHERE Username = @user AND Password = @pass";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@user", username));
                        string hashedPassword = HashPassword(password);
                        cmd.Parameters.Add(new SqlParameter("@pass", hashedPassword));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User(
                                    reader.GetInt32(0),
                                    reader.GetString(1)
                                );
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {

            }
            return null;
        }


        public static bool Register(string username, string password)
        {
            try
            {
                using (SqlConnection connection = getConnection())
                {
                    connection.Open();
                    string sql = "INSERT INTO Users (Username, Password) VALUES (@user, @pass)";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@user", username));
                        string hashedPassword = HashPassword(password);
                        cmd.Parameters.Add(new SqlParameter("@pass", hashedPassword));

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Datenbankfehler: " + ex.Message);
            }
            return false;
        }
    }
}
