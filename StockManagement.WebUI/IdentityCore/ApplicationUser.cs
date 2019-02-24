using Microsoft.AspNetCore.Identity;
using StockManagement.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.IdentityCore
{
  public class ApplicationUser : IdentityUser
  {
    public int CalisanId { get; set; }
    public Calisan Calisan { get; set; }

  }
}
