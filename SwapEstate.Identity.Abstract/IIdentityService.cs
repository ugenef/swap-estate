using System.Threading.Tasks;
using SevenSem.Identity.Abstract.Model;

namespace SevenSem.Identity.Abstract
{
    public interface IIdentityService
    {
        Task CreateAsync(Creds creds, Role[] roles);
        Task<LoginResult> SingInAsync(Creds creds);
    }
}