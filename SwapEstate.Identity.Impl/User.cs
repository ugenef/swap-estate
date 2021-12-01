using Microsoft.AspNetCore.Identity;

namespace SwapEstate.Identity.Impl
{
    internal class User : IdentityUser
    {
        public string Login { get; set; }
    }
}