using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyEvernote.DataLayer;
using MyEvernote.DataLayer.SQL;
using MyEvernote.Model;
using MyEvernote.Logger;
using System.Web.Script.Serialization;

namespace MyEvernote.Api.Controllers
{
    /// <summary>
	/// Управление пользователями
	/// </summary>
    public class UsersController : ApiController
    {
        private const string ConnectionString = @"Data Source=localhost;Initial Catalog=MyEvernote;Integrated Security=True";
        private readonly IUser _usersRepository;

        public UsersController()
        {
            _usersRepository = new UsersRepository(ConnectionString);
        }

        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/users/{id}")]
        public User Get(Guid id)
        {
            Log.Instance.Info("Запрос пользователя с  ID {0}", id);
            var UserForReturn = _usersRepository.Get(id);
            if (UserForReturn == null)
                {
                try
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                catch (HttpResponseException ex)
                {
                    
                }
            }
            return UserForReturn;
        }

        [HttpGet]
        [Route("api/users/all")]
        public List<User> GetAllUsers()
        {
            Log.Instance.Info("Запрос всех пользователей");
            var UsersForReturn = _usersRepository.GetAll();
            return UsersForReturn;
        }

        [HttpPost]
        [Route("api/users")]
        public User Post([FromBody] User user)
        {
            Log.Instance.Info("Создание пользователя с именем: {0} и ID {1}", user.Name, user.Id);
            return _usersRepository.Create(user);
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public void Delete(Guid id)
        {
            Log.Instance.Info("Удаление пользователя  ID {0}", id);
            _usersRepository.Delete(id);
        }


    }
}
