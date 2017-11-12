using System;
using MyEvernote.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataLayer
{
    public interface ICategories
    {
        Category Create(Category category); //
        Category Get(Guid Id);
        List<Category> GetCategories();
        void Delete(Guid Id);
    }
}
