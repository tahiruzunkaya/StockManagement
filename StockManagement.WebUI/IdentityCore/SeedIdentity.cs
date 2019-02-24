using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.IdentityCore
{
  public static class SeedIdentity
  {
    public static async Task CreateIdentityUsers(IServiceProvider serviceProvider,IConfiguration configuration)
    {
      var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
      var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

      var username = configuration["Data:AdminUser:username"];
      var password= configuration["Data:AdminUser:password"];
      var role= configuration["Data:AdminUser:role"];

      var role1 = "bolumy";
      var role2 = "satis";

      if (await roleManager.FindByNameAsync(role1) == null)
      {
        await roleManager.CreateAsync(new IdentityRole(role1));
      }
      if (await roleManager.FindByNameAsync(role2) == null)
      {
        await roleManager.CreateAsync(new IdentityRole(role2));
      }

      if (await userManager.FindByNameAsync(username) == null)
      {
        if(await roleManager.FindByNameAsync(role) == null)
        {
          await roleManager.CreateAsync(new IdentityRole(role));
        }

        ApplicationUser user = new ApplicationUser()
        {
          UserName = username,
          CalisanId = 1
        };

        IdentityResult result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
          await userManager.AddToRoleAsync(user,role);
        }
      }
    }
  }
}
