using System;
using MyEvernote.Model;
using System.Data.SqlClient;

namespace MyEvernote.DataLayer.SQL
{
    public class CategoriesRepository : ICategories
    {
        readonly string _ConnectionString;
        public CategoriesRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }
        public Category Create(Category category)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    category.Id = Guid.NewGuid();
                    command.CommandText = "insert into Category(Id, Category) values(@id, @category)";
                    command.Parameters.AddWithValue("@id", category.Id);
                    command.Parameters.AddWithValue("@category", category.Name);
                    command.ExecuteNonQuery();
                    return category;
                }
            }
        }
        public Category Get(Guid Id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select Id from Category where Id = @id";
                    command.Parameters.AddWithValue("@id", Id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"категория с Id {Id} не найдена");

                        var category = new Category();
                        category.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                        return category;
                    }
                    
                    
                }
            }
        }
        public void Delete(Guid Id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "delete from Category where Id=@id";
                    command.Parameters.AddWithValue("@id", Id);
                    command.ExecuteNonQuery();
                }

            }
        }
    }
}
