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
    public class NotesController : ApiController
    {
        private const string ConnectionString = @"Data Source=DESKTOP-IC679A3;Initial Catalog=MyEvernote;Integrated Security=True";
        private readonly INote _notesRepository;

        public NotesController()
        {
            _notesRepository = new NotesRepository(ConnectionString);
        }

        [HttpGet]
        [Route("api/notes/{id}")]
        public Note Get(Guid id)
        {
            return _notesRepository.Get(id);
        }

        [HttpPost]
        [Route("api/notes")]
        public Note Post(Note note)
        {
            return _notesRepository.Create(note);
        }

        [HttpDelete]
        [Route("api/notes/{id}")]
        public void Delete(Guid id)
        {
             _notesRepository.Delete(id);
        }
    }
}
