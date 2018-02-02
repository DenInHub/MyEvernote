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

namespace MyEvernote.Api.Controllers
{
    public class CategoriesController : ApiController
    {
        private const string ConnectionString = @"Data Source=DESKTOP-IC679A3;Initial Catalog=MyEvernote;Integrated Security=True"; //
        private readonly ICategories _categoriesRepository;

        public CategoriesController()
        {
            _categoriesRepository = new CategoriesRepository(ConnectionString);
        }

        [Authorize]
        [HttpGet]
        [Route("api/Categories/{id}")]
        public Category Get(Guid id)
        {
            Log.Instance.Info("Запрос категории заметок  c ID {0}", id);
            return _categoriesRepository.Get(id);
        }
        [HttpGet]
        [Route("api/Categories")]
        public IEnumerable<Category> Get()
        {
            return _categoriesRepository.GetCategories();
        }

        [Authorize]
        [HttpPost]
        [Route("api/Categories")]
        public Category Create(Category category)
        {
            Log.Instance.Info("Создание категории заметок  {0} c ID {1}", category.Name , category.Id);
            //Log.Instance.Error
            return _categoriesRepository.Create(category);
        }

        [Authorize]
        [HttpDelete]
        [Route("api/Categories/{id}")]
        public void Delete(Guid id)
        {
            Log.Instance.Info("Удаление категории заметок  c ID {0}", id);
            _categoriesRepository.Delete(id);
        }
    }
}
