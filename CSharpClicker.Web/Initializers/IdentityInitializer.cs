﻿using CSharpClicker.Web.Domain;
using CSharpClicker.Web.Infrastructure.DataAccessLayer;
using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.Web.Initializers
{
    public static class IdentityInitializer
    {
        public static void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(o => o.Password.RequireNonAlphanumeric = false);

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/auth/login"; 
            });
        }
    }
}
