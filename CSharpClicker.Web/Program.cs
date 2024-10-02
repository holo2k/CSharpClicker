using CSharpClicker.Web.Domain;

using CSharpClicker.Web.Infrastructure.DataAccessLayer;
using CSharpClicker.Web.Initializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web;

    public class Program
    {
        public static void Main(string[] args)
        {
            

            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddScoped<AppDbContext>();
            ConfigureService(builder.Services);
            
            var app = builder.Build();
            app.MapGet("/", () => "Hello World!");
            app.MapHealthChecks("health-check");
            app.Run();
        }

        private static void ConfigureService(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            DbContextInitializer.InitializeDbContext(services);
        }
    }

