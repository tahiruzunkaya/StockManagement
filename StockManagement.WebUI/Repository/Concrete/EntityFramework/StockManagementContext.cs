using Microsoft.EntityFrameworkCore;
using StockManagement.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Repository.Concrete.EntityFramework
{
  public class StockManagementContext : DbContext
  {
    public StockManagementContext(DbContextOptions<StockManagementContext> options)
      : base(options)
    {

    }

    public DbSet<Bolum> Bolumler { get; set; }
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Stok> Stok { get; set; }
    public DbSet<Stoklar> Stoklar { get; set; }
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<UrunKategori> UrunKategoriler { get; set; }
    public DbSet<Zimmet> Zimmetler { get; set; }
    public DbSet<Atik> Atiklar { get; set; }

  }
}
