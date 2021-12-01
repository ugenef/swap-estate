using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SevenSem.Rest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(o => o.ListenAnyIP(80));
                    webBuilder.UseStartup<Startup>();
                });
    }
}