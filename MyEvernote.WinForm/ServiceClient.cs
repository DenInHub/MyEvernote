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
    public class ServiceClient
    {
        public HttpClient client;

        public ServiceClient(string connectionstring)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(connectionstring);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public List<User> GetAllUsers()
        {
            var user = client.GetAsync("users/all").Result.Content.ReadAsAsync<List<User>>().Result;
            return user;
        }
        public List<Note> GetNotesOfUser(Guid id)
        {
            var notes = client.GetAsync($"notes/{id}").Result.Content.ReadAsAsync<List<Note>>().Result;
            return notes;
        }
        
        public User CreateUser(User user)
        {
            return client.PostAsJsonAsync($"users", user).Result.Content.ReadAsAsync<User>().Result; 
        }
        public List<Category> GetCategories()
        {
            return client.GetAsync("Categories").Result.Content.ReadAsAsync<List<Category>>().Result;
        }

    }
}
