using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.Infrastructure.DataAccessLayer;
using CSharpClicker.Web.Infrastructure.Implementations;
using CSharpClicker.Web.Initializers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;

namespace CSharpClicker.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        DbContextInitializer.InitializeDbContext(appDbContext);

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.Use(async (context, next) =>
        {
            Console.WriteLine($"Request: {context.Request.Path}");
            await next();
        });
        app.MapControllers();
        app.MapDefaultControllerRoute();
        app.MapHealthChecks("health-check");
        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddControllersWithViews();
        services.AddAuthentication();
        services.AddAuthorization();

        services.AddScoped<ICurrentUserAccessor, CurrentUserAccsessor>();
        services.AddScoped<IAppDbContext, AppDbContext>();

        IdentityInitializer.AddIdentity(services);
        DbContextInitializer.AddAppDbContext(services);
    }
}