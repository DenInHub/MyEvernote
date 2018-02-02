using System;
using MyEvernote.Model;
using System.Data.SqlClient;
using MyEvernote.Logger;
using System.Collections.Generic;

namespace MyEvernote.DataLayer.SQL
{
    public class UsersRepository:IUser
    {
        readonly string _ConnectionString;
        public UsersRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public ApplicationUser Create(ApplicationUser user)
        {

            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    //Log.Instance.Info("ура!!");
                    user.Id_ =  Guid.NewGuid();
                    command.CommandText = "insert into Users(Id,Name,Password) values(@id,@name,@password)";
                    command.Parameters.AddWithValue("@id", user.Id_);
                    command.Parameters.AddWithValue("@name", user.UserName);
                    command.Parameters.AddWithValue("@password", user.Password);
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
        public ApplicationUser Get(Guid id)
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
                        var user = new ApplicationUser
                        {
                            Id_ = reader.GetGuid(reader.GetOrdinal("Id")),
                            UserName = reader.GetString(reader.GetOrdinal("Name"))
                        };
                        return user;
                    }
                        
                }
            }
          
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                IEnumerable<ApplicationUser> Users; //= new IEnumerable<ApplicationUser>();
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select *from Users";

                    using (var reader = command.ExecuteReader())
                    {
                        /*if (!reader.Read())
                            return null;*/
                        while (reader.Read())
                        {
                            yield return new ApplicationUser
                            {
                                Id_ = reader.GetGuid(reader.GetOrdinal("Id")),
                                UserName = reader.GetString(reader.GetOrdinal("Name")),
                                Password = reader.GetString(reader.GetOrdinal("Password"))
                            };
                        }
                    }

                }
            }
        }
    }
}
