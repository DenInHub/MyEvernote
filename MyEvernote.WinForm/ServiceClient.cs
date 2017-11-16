using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Model;

namespace MyEvernote.WinForm
{
    public  class ServiceClient
    {
        public static readonly HttpClient client;

        static ServiceClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:36932/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // Категории
        public static Category CreateCategory(Category Category)
        {
            return client.PostAsJsonAsync("Categories/", Category).Result.Content.ReadAsAsync<Category>().Result;
        }
        public static List<Category> GetCategories()
        {
            return client.GetAsync("Categories").Result.Content.ReadAsAsync<List<Category>>().Result;
        }

        // Заметки
        public async static void ShareNote(Guid NoteId, Guid UserId)
        {
            await client.PostAsJsonAsync($"notes/share/{NoteId}/{UserId}", string.Empty); 
        }           // пошарить

        public static List<Note> GetNotesOfUser(Guid id)
        {
            var notes = client.GetAsync($"notes/{id}").Result.Content.ReadAsAsync<List<Note>>().Result;
            return notes;
        }                       // заметки юзера
        
        public async static Task CreateNote(Note Note)
        {
            await client.PostAsJsonAsync($"notes", Note);
        }                         // создать

        public async static Task ChangeNote(Note Note)
        {
            await client.PostAsJsonAsync($"notes/{Note.Id}", Note);
        }                         // изменить

        public async static void CancelShare(Guid NoteId)
        {
            await client.DeleteAsync($"notes/share/{NoteId}");
        }                      // отменить шарить

        public async static void DeleteNote(Guid NoteId)
        {
            await client.DeleteAsync($"notes/{NoteId}");
        }                       // удалить
        public  static Note GetNote(Guid NoteId)
        {
            return  client.GetAsync($"note/{NoteId}").Result.Content.ReadAsAsync<Note>().Result;
        }

        // Юзеры
        public static  List<User> GetAllUsers()
        {
            var user = client.GetAsync("users/all").Result.Content.ReadAsAsync<List<User>>().Result;
            return user;
        }

        public static User CreateUser(User user)
        {
            return client.PostAsJsonAsync($"users", user).Result.Content.ReadAsAsync<User>().Result; 
        }
    }
}
