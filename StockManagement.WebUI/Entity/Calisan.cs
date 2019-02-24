using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Entity
{
  public class Calisan
  {
    public int CalisanId { get; set; }
    [Required]
    public string CalisanAdi { get; set; }
    [Required]
    public string CalisanSoyadi { get; set; }
    public Boolean IsYetkili { get; set; }

    public int BolumId { get; set; }
    public Bolum Bolum { get; set; }
  }
}
