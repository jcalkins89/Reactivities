using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        // Method that builds and runs our application
        {
            var host =  CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()){
                var services = scope.ServiceProvider;
                // Everytime the application is started, check to see if there's a database
                // if there's not, create one based on migrations
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Oh snap! Looks like an error hath occured during migration my programmer lord!");
                }
            }

            host.Run();
        }
        // Creates a new instance of our Website builder class
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
