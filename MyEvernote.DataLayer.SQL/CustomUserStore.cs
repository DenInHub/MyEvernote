using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MyEvernote.Model;
using System;
using System.Data.SqlClient;

namespace MyEvernote.DataLayer.SQL
{
    public class CustomUserStore<T> : IUserStore<T> where T :  ApplicationUser, new()
    {
        T _user = new T();
        readonly string _ConnectionString = @"Data Source=localhost;Initial Catalog=MyEvernote;Integrated Security=True";
        // да , костылек((
        #region 
        object obj = new object();
        public CustomUserStore(CustomUserManager man)
        {
            obj = man;
        }
        // да , костылек((
        // да , костылек((
        // да , костылек((
        #endregion  
        public Task CreateAsync(T user)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    //Log.Instance.Info("ура!!");
                    user.Id_ = Guid.NewGuid();
                    command.CommandText = "insert into Users(Id,Name,Password) values(@id,@name,@password)";
                    command.Parameters.AddWithValue("@id", user.Id_);
                    command.Parameters.AddWithValue("@name", user.UserName);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.ExecuteNonQuery();
                }
            }
            return Task.FromResult<T>(user);
        }

        public Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public  Task<T> FindByNameAsync(string userName)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from Users where Name = @name";
                    command.Parameters.AddWithValue("@name", userName);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return  Task.Run(() => { return _user; });

                        _user.Id_ = reader.GetGuid(reader.GetOrdinal("Id"));
                        _user.Id = _user.Id_.ToString();
                        _user.UserName = reader.GetString(reader.GetOrdinal("Name"));
                        _user.Password = reader.GetString(reader.GetOrdinal("Password"));
                        return  Task.Run(() => { return _user; } );
                    }

                }
            }
            
        }

        public Task UpdateAsync(T user)
        {
            throw new NotImplementedException();
        }
    }


}
