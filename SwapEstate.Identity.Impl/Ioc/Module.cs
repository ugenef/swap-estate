using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SevenSem.Identity.Abstract;
using SwapEstate.Identity.Impl.Dal;

namespace SwapEstate.Identity.Impl.Ioc
{
    public static class Module
    {
        public static IServiceCollection WithIdentityModule(this IServiceCollection services, string connString)
        {
            return services
                .AddSingleton<IIdentityService, IdentityService>()
                .AddDbContext<UserContext>(o=>
                    o.UseNpgsql(connString)
                        .UseSnakeCaseNamingConvention())
                .AddIdentity<IdentityUser, IdentityRole>(o=>
                {
                    o.Password.RequireDigit = false;
                    o.Password.RequiredLength = 1;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<UserContext>()
                .Services;
        }
    }
}