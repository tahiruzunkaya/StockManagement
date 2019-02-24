using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Entity
{
  public class UrunKategori
  {
    public int UrunKategoriId { get; set; }
    public string KategoriAdi { get; set; }

    public List<Urun> Urunler { get; set; }
  }
}
