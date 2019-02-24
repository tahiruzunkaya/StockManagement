using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Entity
{
  public class Stok
  {
    public int StokId { get; set; }
    public int Adet { get; set; }

    public int UrunId { get; set; }
    public Urun Urun { get; set; }
  }
}
