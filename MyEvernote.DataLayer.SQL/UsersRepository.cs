using System;
using MyEvernote.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataLayer.SQL
{
    public class UsersRepository:IUser
    {
        readonly string _ConnectionString;
        public UsersRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }
        public User Create(User user)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    user.Id =  Guid.NewGuid();
                    command.CommandText = "insert into Users(Id,Name) values(@id,@name)";
                    command.Parameters.AddWithValue("@id", user.Id);
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.ExecuteNonQuery();
                }
            }
            return user;

        }

        public void Delete(Guid id)
        {
            using (SqlConnection connection =new SqlConnection(_ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "delete from Shared where UserId = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "delete from Users where Id = @idd";
                    command.Parameters.AddWithValue("@idd" , id);
                    command.ExecuteNonQuery();
                }

            }
        }
        /// <summary>
        /// exception: ArgumentException
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User Get(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select Id, Name from Users where Id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        //var idd = (string)reader["Id"];
                        var user = new User
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };
                        return user;
                    }
                        
                }
            }
          
        }
    }
}
