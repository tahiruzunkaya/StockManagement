using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Entity
{
  public class Urun
  {
    public int UrunId { get; set; }
    [Required]
    [MaxLength(50)]
    public string UrunAdi { get; set; }
    [Required]
    public DateTime SatinAlmaTarihi { get; set; }
    [Required]
    public double BirimFiyat { get; set; }
    [Required]
    [MaxLength(50)]
    public string ParcaTipi { get; set; }
    [Required]
    [MaxLength(50)]
    public string Firma { get; set; }

    public string Aciklamasi { get; set; }

    public int UrunKategoriId { get; set; }
    public UrunKategori UrunKategori { get; set; }

    public Stok Stok { get; set; }
  }
}
