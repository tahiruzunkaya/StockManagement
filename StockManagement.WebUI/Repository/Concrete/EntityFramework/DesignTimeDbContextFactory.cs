using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Repository.Concrete.EntityFramework
{
  public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StockManagementContext>
  {
    public StockManagementContext CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var builder = new DbContextOptionsBuilder<StockManagementContext>();
      var connectionstring = configuration.GetConnectionString("DefaultConnection");
      builder.UseSqlServer(connectionstring);
      return new StockManagementContext(builder.Options);
    }
  }
}
