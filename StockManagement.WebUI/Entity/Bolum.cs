using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Entity
{
    public class Bolum
    {
    public int BolumId { get; set; }
    public string BolumAdi { get; set; }



    public List<Calisan> Calisanlar { get; set; }
  }
}
