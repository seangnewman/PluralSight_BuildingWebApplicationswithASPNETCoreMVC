
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Identity;

namespace BethanysPieShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
         }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Registering services through Dependency Injection container


            // Registering services for EF
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BethanysPieShopConnection")));
            //Register identity services
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            
            
            // Register the service with it's interface
            // With add scoped an instance is created at each call and remains until it is out of scope
            //services.AddScoped<IPieRepository, MockPieRepository>();
            //services.AddScoped<ICategoryRepository, MockCategoryRepository>();
            services.AddScoped<IPieRepository, PieRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            //When user comes to site, the Shopping cart is now associated with the user
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            // Return information regarding the HttpContext and session
            services.AddHttpContextAccessor();
             services.AddSession();



            //Adding support for MVC 
            services.AddControllersWithViews();

            //Indentity requires Razor Pages
            services.AddRazorPages();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Middleware components are defined here
            // Requests are processed sequentially


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Redirect http requests to https
            app.UseHttpsRedirection();
            // Serve static files  
            app.UseStaticFiles();
            // Middleware required to support retrieving session information
            // Must be called before UseRouting()
            app.UseSession();

            // UseRouting and UseEndpoints middleware enable convention based built-in routing
            app.UseRouting();
            // Middleware for .NET Authentication
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
                // Add endpoints for Identity Login/Logout
                endpoints.MapRazorPages();

            });
        }
    }
}
