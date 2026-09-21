using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTaskPlanner
{
    public class Category
    {
        public int CategoryID { get; private set; }
        public string CategoryName { get; set; } = string.Empty;

        internal Category(int categoryId, string categoryName) {
            this.CategoryID = categoryId;
            this.CategoryName = categoryName;
        }

        public static List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            try
            {
                using (SqlConnection connection = Starter.getConnection())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Categories", connection);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category(
                                reader.GetInt32(0),
                                reader.GetString(1)
                            ));
                        }
                    }
                }
            }
            catch (SqlException)
            {

            }

            return categories;
        }

        public override string ToString()
        {
            return this.CategoryName;
        }
    }
}
