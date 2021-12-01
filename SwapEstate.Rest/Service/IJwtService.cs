using SevenSem.Identity.Abstract;
using SevenSem.Identity.Abstract.Model;

namespace SevenSem.Rest.Service
{
    public interface IJwtService
    {
        string GetToken(Role[] roles);
    }
}