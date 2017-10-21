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
    public class CategoriesController : ApiController
    {
        private const string ConnectionString = @"Data Source=DESKTOP-IC679A3;Initial Catalog=MyEvernote;Integrated Security=True";
        private readonly ICategories _categoriesRepository;

        public CategoriesController()
        {
            _categoriesRepository = new CategoriesRepository(ConnectionString);
        }

        [HttpGet]
        [Route("api/Categories/{id}")]
        public Category Get(Guid id)
        {
            return _categoriesRepository.Get(id);
        }

        [HttpPost]
        [Route("api/Categories")]
        public Category Createe(Category category)
        {
            return _categoriesRepository.Create(category);
        }

        [HttpDelete]
        [Route("api/Categories/{id}")]
        public void Delete(Guid id)
        {
            _categoriesRepository.Delete(id);
        }
    }
}
