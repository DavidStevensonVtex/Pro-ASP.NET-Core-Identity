using IdentityApp.Models;
using IdentityApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("IdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityDbContextConnection' not found.");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<ProductDbContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration["ConnectionStrings:AppDataConnection"]);
                //opts.EnableSensitiveDataLogging(true);
            });

            builder.Services.AddDbContext<IdentityDbContext>(opts =>
            {
                opts.UseSqlServer(
                    builder.Configuration["ConnectionStrings:IdentityConnection"],
                    opts => opts.MigrationsAssembly("IdentityApp"));
            });

            builder.Services.AddScoped<IEmailSender, ConsoleEmailSender>();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(
                opts =>
                {
                    opts.Password.RequiredLength = 8;
                    opts.Password.RequireDigit = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.SignIn.RequireConfirmedAccount = true;         // default: false
                })
                .AddEntityFrameworkStores<IdentityDbContext>();

            builder.Services.AddAuthentication()
                .AddFacebook(opts =>
                {
                    opts.AppId = builder.Configuration["Facebook:AppId"];
                    opts.AppSecret = builder.Configuration["Facebook:AppSecret"];
                })
                .AddGoogle(opts =>
                {
                    opts.ClientId = builder.Configuration["Google:ClientId"];
                    opts.ClientSecret = builder.Configuration["Google:ClientSecret"];
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //    endpoints.MapRazorPages();
            //});

            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();
        }
    }
}