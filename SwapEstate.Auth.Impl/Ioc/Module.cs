using Microsoft.Extensions.DependencyInjection;
using SwapEstate.Auth.Abstract;

namespace SwapEstate.Auth.Impl.Ioc
{
    public static class Module
    {
        public static IServiceCollection WithAuthModule(this IServiceCollection services)
        {
            return services.AddSingleton<IJwtTokenFactory, JwtTokenFactory>();
        }
    }
}