using System;
using System.Web;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Model;
using Newtonsoft.Json.Linq;


namespace MyEvernote.WinForm
{
    public  class ServiceClient
    {
        public static readonly HttpClient client;

        static ServiceClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:36932/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainForm.Token);
        }
        // Категории
        public static Category CreateCategory(Category Category)
        {
            return client.PostAsJsonAsync("api/Categories/", Category).Result.Content.ReadAsAsync<Category>().Result;
        }
        public async static Task<IEnumerable<Category>> GetCategories()
        {
            return client.GetAsync("api/Categories").Result.Content.ReadAsAsync<IEnumerable<Category>>().Result;
        }

        // Заметки
        public async static Task ShareNote(Guid NoteId, Guid UserId)
        {
            await client.PostAsJsonAsync($"api/notes/share/{NoteId}/{UserId}", string.Empty); 
        }           // пошарить

        public async static Task<List<Note>> GetNotesOfUser(Guid id)
        {
            return client.GetAsync($"api/notes/{id}").Result.Content.ReadAsAsync<List<Note>>().Result;
        }                       // заметки юзера
        
        public async static Task CreateNote(Note Note)
        {
            await client.PostAsJsonAsync($"api/notes", Note);
        }                         // создать

        public async static Task ChangeNote(Note Note)
        {
            await client.PostAsJsonAsync($"api/notes/{Note.Id}", Note);
        }                         // изменить

        public async static void CancelShare(Guid NoteId)
        {
            await client.DeleteAsync($"api/notes/share/{NoteId}");
        }                      // отменить шарить

        public async static void DeleteNote(Guid NoteId)
        {
            await client.DeleteAsync($"api/notes/{NoteId}");
        }                       // удалить
        public  static Note GetNote(Guid NoteId)
        {
            return client.GetAsync($"api/note/{NoteId}").Result.Content.ReadAsAsync<Note>().Result;
        }

        // Юзеры
        public async static Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            //var user = client.GetAsync("users/all").Result.Content.ReadAsAsync<List<ApplicationUser>>().Result;
            var users = client.GetAsync("api/users/all").Result.Content.ReadAsAsync<IEnumerable<ApplicationUser>>().Result;
            return users;
        }

        public static ApplicationUser CreateUser(ApplicationUser user)
        {
            return client.PostAsJsonAsync($"api/users", user).Result.Content.ReadAsAsync<ApplicationUser>().Result; 
        }

        public static async Task<ApplicationUser> Register(ApplicationUser user)
        {
            //зарегать
            return  await client.PostAsJsonAsync("api/register", user).Result.Content.ReadAsAsync<ApplicationUser>();
        }

        /// <summary>
        /// Возвращает "true" если токен установлен и "false" если токен не установлен
        /// </summary> 
        public static async Task<bool> GetToken(ApplicationUser user )
        {
            //str = string.Empty;
            //запрос токена
            HttpResponseMessage response =  client.PostAsync("Token",
                new StringContent(string.Format("grant_type=password&username={0}&password={1}",
                HttpUtility.UrlEncode(user.UserName),
                HttpUtility.UrlEncode(user.Password)),
                Encoding.UTF8,
                "application/x-www-form-urlencoded")).Result;

            //ответ API
            string ResultJSON = response.Content.ReadAsStringAsync().Result;

            //десериализация
            var ParseResultJSON = JObject.Parse(ResultJSON);

            if (ParseResultJSON["access_token"] != null)
            {
                MainForm.Token = ParseResultJSON["access_token"].ToString();
                return true;
            }
            else
            {
                MainForm.info = ParseResultJSON["error_description"]?.ToString();
                return false;
            }
        }

        /// <summary>
        /// Ищет пользователя в базе по имени.
        /// return:
        ///     ApplicationUser
        ///         or
        ///     null
        /// </summary>
        public static ApplicationUser FindUser(ApplicationUser User)
        {
            return client.GetAsync($"api/users/{User.UserName}").Result.Content.ReadAsAsync<ApplicationUser>().Result;
        }
    }
}
