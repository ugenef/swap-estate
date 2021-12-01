using System.Threading.Tasks;
using SevenSem.Identity.Abstract;
using SevenSem.Identity.Abstract.Model;

namespace SevenSem.Rest
{
    public class MockIdentity : IIdentityService
    {
        public Task CreateAsync(Creds creds, Role[] roles)
        {
            return Task.CompletedTask;
        }

        public Task<LoginResult> SingInAsync(Creds creds)
        {
            return Task.FromResult(new LoginResult{Roles = new [] {Role.Landlord}});
        }
    }
}