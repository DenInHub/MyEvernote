using System;
using MyEvernote.Model;
using System.Collections.Generic;


namespace MyEvernote.DataLayer
{
    public interface ICategories
    {
        Category Create(Category category); //
        Category Get(Guid Id);
        IEnumerable<Category> GetCategories();
        void Delete(Guid Id);
    }
}
