using System;
using System.Collections.Generic;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http;
using MyEvernote.DataLayer;
using MyEvernote.DataLayer.SQL;
using MyEvernote.Model;
using MyEvernote.Logger;
using Microsoft.AspNet.Identity.Owin;


namespace MyEvernote.Api.Controllers
{
    /// <summary>
	/// Управление пользователями
	/// </summary>
    public class UsersController : ApiController
    {
        private const string ConnectionString = @"Data Source=localhost;Initial Catalog=MyEvernote;Integrated Security=True";
        private readonly IUser _usersRepository;

        private CustomUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<CustomUserManager>();
            }
        }

        public UsersController()
        {
            _usersRepository = new UsersRepository(ConnectionString);
        }





        /// <summary>
        /// Получить пользователя по имени
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("api/users/{UserName}")]
        public ApplicationUser FindUser(string UserName)
        {
            Log.Instance.Info("Запрос пользователя с  именем {0}", UserName);
            return UserManager.FindByNameAsync(UserName).Result;
        }




        [Authorize]
        [HttpGet]
        [Route("api/users/all")]
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            Log.Instance.Info("Запрос всех пользователей");
            var  Users = _usersRepository.GetAll();

            return Users;
        }

        



        [HttpPost]
        [AllowAnonymous]
        [Route("api/register")]
        public Task<ApplicationUser> Post([FromBody] ApplicationUser user)
        {
            Log.Instance.Info("Создание пользователя с именем: {0} ", user.UserName);
            //проверка на наличие в базе юзера с таким же именем; если есть - вернуть null

            if (UserManager.FindByNameAsync(user.UserName).Result.UserName != null)
                return Task.FromResult<ApplicationUser>(null);

            //хэш пароля
            user.Password = UserManager.PasswordHasher.HashPassword(user.Password);
            // интерфейс от Identity на создание юзера не подразумевает воврат созданного юзера. поэтому сначал создаем, ...
            UserManager.CreateUser(user);
            //... потом ищем-возвращаем.
            return UserManager.FindByNameAsync(user.UserName);
        }




        [Authorize]
        [HttpDelete]
        [Route("api/users/{id}")]
        public void Delete(Guid id)
        {
            Log.Instance.Info("Удаление пользователя  ID {0}", id);
            _usersRepository.Delete(id);
        }
    }
}
