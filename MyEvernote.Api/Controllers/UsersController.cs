using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyEvernote.DataLayer;
using MyEvernote.DataLayer.SQL;
using MyEvernote.Model;

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
            return _usersRepository.Get(id);
        }

        [HttpPost]
        [Route("api/users")]
        public User Post([FromBody] User user)
        {
            return _usersRepository.Create(user);
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public void Delete(Guid id)
        {
            _usersRepository.Delete(id);
        }


    }
}
