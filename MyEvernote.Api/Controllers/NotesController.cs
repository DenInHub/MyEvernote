using System;
using System.Collections.Generic;
using System.Web.Http;
using MyEvernote.DataLayer;
using MyEvernote.DataLayer.SQL;
using MyEvernote.Model;
using MyEvernote.Logger;
using System.Web.Http.Filters;



namespace MyEvernote.Api.Controllers
{
    //[MyExceptionFilter]
    public class NotesController : ApiController
    {
        private const string ConnectionString = @"Data Source=DESKTOP-IC679A3;Initial Catalog=MyEvernote;Integrated Security=True";
        private readonly INote _notesRepository;

        public NotesController()
        {
            _notesRepository = new NotesRepository(ConnectionString);
        }


        [HttpGet]
        [Route("api/note/{NoteId}")]
        public Note GetNote(Guid NoteId)
        {
            Log.Instance.Info("запрос заметки  с ID {0}", NoteId);
            return _notesRepository.GetNote(NoteId);
        }

        [HttpGet]
        [Route("api/notes/{id}")]
        public List<Note> Get(Guid id)
        {
            Log.Instance.Info("Запрос заметок пользователя с  ID {0}", id);
            return _notesRepository.GetNotes(id);
        }

        [HttpPost]
        [Route("api/notes")]
        public Note Post(Note note)
        {
            Log.Instance.Info("Создание заметки с {0}  ID {1}",note.Title, note.Id );
            return _notesRepository.Create(note);
        }

        [HttpPost]
        [Route("api/notes/{id}")]
        public void Change(Note note)
        {
            Log.Instance.Info("Изменение заметки с {0}  ID {1}", note.Title, note.Id);
            _notesRepository.Change(note);
        }

        [HttpDelete]
        [Route("api/notes/{id}")]
        public void Delete(Guid id)
        {
            Log.Instance.Info("Удаление заметки с  ID {0}", id);
            _notesRepository.Delete(id);
        }

        [HttpDelete]
        [Route("api/notes/share/{NoteId}")]
        public void CancelShare(Guid NoteId)
        {
            _notesRepository.CancelShare(NoteId);
        }

        [HttpPost]
        [Route("api/notes/share/{NoteId}/{UserId}")]
        public void Share(Guid NoteId,Guid UserId)
        {
            Log.Instance.Info($"Расшарить заметкку {NoteId} пользователю {UserId}");
            _notesRepository.Share(NoteId, UserId);
        }
    }
}
