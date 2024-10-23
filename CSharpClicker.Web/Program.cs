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

            using var scope = app.Services.CreateScope();
            using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            DbContextInitializer.InitializeDbContext(appDbContext);
            //app.MapControllers();

            app.UseMvc();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();
            
            app.MapGet("/", () => "Hello World!");
            app.MapHealthChecks("health-check");
           
            app.Run();
        }

        private static void ConfigureService(IServiceCollection services)
        {
            services.AddHealthChecks();
            
            services.AddSwaggerGen();
            services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddMvcCore(o=>o.EnableEndpointRouting = false)
                    .AddApiExplorer();
            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            //    .AddEntityFrameworkStores<AppDbContext>()
            //    .AddDefaultTokenProviders();
            IdentityInitializer.AddIdentity(services); 
            DbContextInitializer.AddAppDbContext(services);
        }
    }

