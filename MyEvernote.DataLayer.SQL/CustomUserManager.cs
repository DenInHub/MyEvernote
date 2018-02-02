using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MyEvernote.Model;

namespace MyEvernote.DataLayer.SQL
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {
        IUserStore<ApplicationUser> _customUserStore;
        public CustomUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
            this.PasswordHasher = new PasswordHasher();
            _customUserStore = store;
        }

        public static CustomUserManager Create(IdentityFactoryOptions<CustomUserManager> options, IOwinContext context)
        {
            var manager = new CustomUserManager(new CustomUserStore<ApplicationUser>(context.Get<CustomUserManager>()));

            // Configure validation logic for usernames
            //
            //
            // Configure validation logic for usernames


            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public override Task<ApplicationUser> FindAsync(string userName, string password)
        {
            //1. дость из базы хешированный пароль
            var UserFromDB = _customUserStore.FindByNameAsync(userName).Result;
            if (UserFromDB == null)
            {
                return Task.FromResult<ApplicationUser>(null);
            }
            //2. сопоставить
            PasswordVerificationResult result = PasswordHasher.VerifyHashedPassword(UserFromDB.Password, password);
            if (result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded)//
            {
                return Task.Run(() => { return UserFromDB; });
            }
            else
            {
                return Task.FromResult<ApplicationUser>(null);
            }
        }

        public Task CreateUser(ApplicationUser user )
        {
            //создание юзера
            return _customUserStore.CreateAsync(user);
        }


        public override  Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return  _customUserStore.FindByNameAsync(userName);
        }
    }
}
