using Microsoft.Extensions.DependencyInjection;
using SwapEstate.Chats.Abstract;
using SwapEstate.Chats.Infra;
using SwapEstate.Chats.Infra.Ef;
using SwapEstate.Chats.Service;

namespace SwapEstate.Chats.Ioc
{
    public static class Module
    {
        public static IServiceCollection WithChatsModule(this IServiceCollection services, string connString)
        {
            services.AddSingleton<IMessagesService, MessagesService>();
            services.AddSingleton<IMessagesRepository, MessagesRepository>();
            services.AddSingleton<IContextFactory>(new ContextFactory(connString));
            return services;
        }
    }
}