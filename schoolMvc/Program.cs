using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using schoolMvc.BLL.Interfaces;
using schoolMvc.BLL.Repositories;
using schoolMvc.DAL.Data;
using schoolMvc.DAL.Models;
using schoolMvc.PL.Helper;

namespace schoolMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            //builder.Services.ConfigureApplicationCookie(config =>
            //{
            //    config.LoginPath = "/Account/SignIn";

            //});

            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

            
            builder.Services.AddControllersWithViews();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
    }
}