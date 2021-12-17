using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace VerstaTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
    //Todo
    /*На стороне клиенте обновлять время исходя из его часового пояса*/
}
