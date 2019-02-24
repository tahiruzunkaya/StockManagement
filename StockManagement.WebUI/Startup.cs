using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using StockManagement.WebUI.IdentityCore;
using StockManagement.WebUI.Repository.Abstract;
using StockManagement.WebUI.Repository.Concrete.EntityFramework;

namespace StockManagement.WebUI
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
      services.AddDbContext<StockManagementContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
      services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
      services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
        .AddDefaultTokenProviders();
      services.AddTransient<IUrunRepository, EfUrunRepository>();
      services.AddTransient<IStoklarRepository, EfStoklarRepository>();
      services.AddTransient<IBolumRepository, EfBolumRepository>();
      services.AddTransient<ICalisanRepository, EfCalisanRepository>();
      services.AddTransient<IStokRepository, EfStokRepository>();
      services.AddTransient<IZimmetRepository, EfZimmetRepository>();
      services.AddTransient<IUnitOfWork, EfUnitOfWork>();
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
      }
      app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
      app.UseStaticFiles();
      app.UseAuthentication();
      app.UseStaticFiles(new StaticFileOptions()
      {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
        RequestPath = new PathString("/vendor")
      });
      app.UseMvc(routes=> {
        routes.MapRoute(
          name:"default",
          template:"{controller=Home}/{action=Index}/{id?}"
          );
      });
      SeedData.EnsurePopulated(app);
      SeedIdentity.CreateIdentityUsers(app.ApplicationServices,Configuration).Wait();
    }
  }
}
