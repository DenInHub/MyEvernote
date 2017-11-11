using System;
using MyEvernote.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataLayer
{
    public interface INote
    {
        Note Create(Note note);
        List<Note> GetNotes(Guid id);
        Note GetNote(Guid NoteId);
        void Change(Note note);
        void Delete(Guid id);
        void Share(Guid NoteId , Guid UserId);
        
    }
    
}
