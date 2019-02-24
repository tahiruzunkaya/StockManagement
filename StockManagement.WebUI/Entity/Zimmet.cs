using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Entity
{
    public class Zimmet
    {
    public int ZimmetId { get; set; }
    public DateTime ZimmetTarih { get; set; }

    public int CalisanId { get; set; }
    public Calisan Calisan { get; set; }

    public int StoklarId { get; set; }
    public Stoklar Stoklar { get; set; }
  }
}
