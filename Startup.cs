using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models;
using PhuKienDienThoai.Services;
using Microsoft.EntityFrameworkCore;

namespace PhuKienDienThoai
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = false;
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            });
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc()
                    .AddSessionStateTempDataProvider();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{Controller=Home}/{Action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // try
            // {
            //     var roleseeder = new PhuKienDienThoai.Configurations.RoleSeeder(serviceProvider);
            //     roleseeder.SeedAsync().Wait();

            // }
            // catch (System.Exception ex)
            // {

            //     System.Console.WriteLine(ex.ToString());
            // }

        }
    }
}
