using CSharpClicker.Web.Infrastructure.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.Initializers
{
    public class DbContextInitializer
    {
        public static void InitializeDbContext(IServiceCollection services)
        {
            

            services
               .AddDbContext<AppDbContext>(options =>
               options.UseSqlite($"Data Source = {GetPathToDbFile()}"));

            using var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();

            string GetPathToDbFile()
            {
                var applicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CSharpClicker");
                if (!Directory.Exists(applicationFolder))
                {
                    Directory.CreateDirectory(applicationFolder);
                }
                var pathToDbFile = Path.Combine(applicationFolder, "CSharpClicker.db");
                return pathToDbFile;
            }
        }
    }
}
