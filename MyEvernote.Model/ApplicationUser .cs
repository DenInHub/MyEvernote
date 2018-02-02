
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System;

//using Microsoft.AspNet.Identity.EntityFramework;
//using System.Data.Entity;
//using System.ComponentModel.DataAnnotations;
//using System;

namespace MyEvernote.Model
{

    public class ApplicationUser : IUser
    {
        public string Id            { get; set; }
        public string UserName      { get; set; }
        public string Password      { get; set; }
        public Guid Id_             { get; set; } // введен ID  пользователя типа гуид , т.к возникают проблеиы при переносе в скуль
         
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
