using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using MyEvernote.Model;
using System.Windows.Forms;

namespace MyEvernote.WinForm
{
    static class ServiceUser
    {
        public static string GetNameCategory(Guid idCategory)
        {
            var Category = MainForm.serviceClient.client.GetAsync($"Categories/{idCategory}").Result.Content.ReadAsAsync<Category>().Result;
            return Category.Name;
        }
        public static string ShowNoteInfo(this Note note)
        {
            List<string> owners = new List<string>();
            for (int i = 0; i < note?.Shared?.Count; i++)
            {
                owners.Add(Variable.Users.First(x => x.Id == note.Shared[i]).Name);
            }
            var str = string.Format("{0,-25}\t{1,-25}\n{2,-25}\t{3,-25}\n{4,-25}\t{5,-25}\n{6,-25}\t{7,-25}\n{8,-25}\t{9,-25}\n{10,-30}\n{11,-25}\n",
                "Имя заметки:", note.Title,
                "Автор заметки:", Variable.Users.Single(x=>x.Id == note.Creator).Name,
                "Категория заметки:",GetNameCategory(note.Category),
                "Дата создания:", note.Created,
                "Дата изменения:", note.Changed,
                "Владельцы:",string.Join("\n" , owners)
                );
            return str;
        }
        public static void ShowNoteInfoBox(this Note note)
        {
            if (note != null)
                MessageBox.Show(note.ShowNoteInfo(),"Note info",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public static void RefreshWindow(this Note note)
        {

        }

    }
}
