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
        ApplicationUser Create(ApplicationUser name);
        void Delete(Guid id);
        ApplicationUser Get(Guid id);
        IEnumerable<ApplicationUser> GetAll();
    }
}

