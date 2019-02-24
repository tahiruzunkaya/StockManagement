using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Entity
{
    public class Stoklar
    {
    public int StoklarId { get; set; }

    [Required]
    public int UrunId { get; set; }
    public Urun Urun { get; set; }
  }
}
