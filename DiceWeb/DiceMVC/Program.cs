using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiceMVC.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DiceMVC.Application.ViewModels.Game;
using DiceMVC.Application.ViewModels.Player;
using FluentValidation;
using static DiceMVC.Application.ViewModels.Game.GetPlayerCountVm;
using DiceMVC.Application;
using System.Configuration;

namespace DiceMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //  CreateHostBuilder(args).Build().Run();
            var builder = WebApplication.CreateBuilder(args);

           //Add services to the container
           builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<WorkContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<WorkContext>();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();
            builder.Services.AddControllersWithViews();//.AddFluentValidation(fv => fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false);
            builder.Services.AddRazorPages();
            builder.Services.AddTransient<IValidator<NewPlayerVm>, NewPlayerValidator>();
            builder.Services.AddTransient<IValidator<GetPlayerCountVm>, GetPlayerCountValidator>();

            builder.Services.Configure<IdentityOptions>(option =>   //register options
            {
                option.Password.RequireDigit = true;                //require number in password
                option.Password.RequireLowercase = true;            //require lowercase in password
                option.Password.RequireUppercase = true;            //require uppercase in password
                option.Password.RequiredLength = 8;                 //require min length of password - 8

                option.SignIn.RequireConfirmedEmail = false;        //email confirmation disabled
                option.User.RequireUniqueEmail = true;              //require unique email

            });

            //builder.Services.AddAuthentication().AddGoogle(option =>
            //{
            //    option.ClientId = builder.Configuration["Authentication:Google:ClientId:"];
            //    option.ClientSecret = builder.Configuration["Authentication:Google:Secret:"];
            //});
            var app = builder.Build();
            //Configure the HTTP request pipline
            if (!app.Environment.IsDevelopment()) 
            {
                app.UseExceptionHandler("Home/Error");
                // The default HSTS value is 30 days. Yo may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); 
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //          webBuilder.UseStartup<Startup>();
        //        });
    }
}
