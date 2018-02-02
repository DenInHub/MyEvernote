using System;
using MyEvernote.Model;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MyEvernote.DataLayer.SQL
{
    public class CategoriesRepository : ICategories
    {
        readonly string _ConnectionString;
        public CategoriesRepository(string ConnectionString) //
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
                    command.CommandText = "select * from Category where Id = @id";
                    command.Parameters.AddWithValue("@id", Id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"категория с Id {Id} не найдена");

                        var category = new Category();
                        category.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                        category.Name = reader.GetString(reader.GetOrdinal("Category"));
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

        public IEnumerable<Category> GetCategories()
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from Category ";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return  new Category
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Category"))
                            };
                        }
                    }
                }
            }
        }
    }
}
