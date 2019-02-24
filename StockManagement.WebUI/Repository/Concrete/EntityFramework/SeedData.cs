using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockManagement.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Repository.Concrete.EntityFramework
{
  public static class SeedData
  {
    public static void EnsurePopulated(IApplicationBuilder app)
    {
      
      var context = app.ApplicationServices.GetRequiredService<StockManagementContext>();
      context.Database.Migrate();
      if (!context.UrunKategoriler.Any())
      {
        var kategoriler = new[] {
          new UrunKategori(){KategoriAdi="Ekran Kartı"},
          new UrunKategori(){KategoriAdi="Ram"},
          new UrunKategori(){KategoriAdi="İşlemci"}
        };

        context.UrunKategoriler.AddRange(kategoriler);

        var urunler = new[] {
          new Urun(){UrunAdi="Nvidia",SatinAlmaTarihi=DateTime.Now,BirimFiyat=1,ParcaTipi="Toplama",Firma="Vatan",Aciklamasi="Nvidia geForce 1080ti",UrunKategori=kategoriler[0]}
        };

        context.Urunler.AddRange(urunler);

        var stok = new[] {
          new Stok(){ Adet=10,Urun=urunler[0]}
        };

        context.Stok.AddRange(stok);

        var stoklar = new[] {
          new Stoklar(){Urun=urunler[0]},
          new Stoklar(){Urun=urunler[0]},
          new Stoklar(){Urun=urunler[0]},
          new Stoklar(){Urun=urunler[0]},
          new Stoklar(){Urun=urunler[0]},
          new Stoklar(){Urun=urunler[0]},
          new Stoklar(){Urun=urunler[0]},
          new Stoklar(){Urun=urunler[0]},
          new Stoklar(){Urun=urunler[0]},
          new Stoklar(){Urun=urunler[0]}
        };
       
        context.Stoklar.AddRange(stoklar);

        context.SaveChanges();

      }
      
    }
  }
}
