using System;
using MyEvernote.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataLayer
{
    public interface IUser
    {
        User Create(User name);
        void Delete(Guid id);
        User Get(Guid id);
        List<User> GetAll();
    }
}

