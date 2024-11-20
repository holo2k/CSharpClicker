using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.Infrastructure.DataAccessLayer;
using CSharpClicker.Web.Infrastructure.Implementations;
using CSharpClicker.Web.Initializers;

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
        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();
        app.MapDefaultControllerRoute();
        app.MapHealthChecks("health-check");

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddSwaggerGen();
        services.AddSession();
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddAuthentication()
            .AddCookie(o => o.LoginPath = "/auth/login");
        services.AddAuthorization();
        services.AddControllersWithViews();

        services.AddScoped<ICurrentUserAccessor, CurrentUserAccsessor>();
        services.AddScoped<IAppDbContext, AppDbContext>();

        IdentityInitializer.AddIdentity(services);
        DbContextInitializer.AddAppDbContext(services);
    }
}