using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Entity
{
    public class Atik
    {
    public int AtikId { get; set; }
    public DateTime AtikTarih { get; set; }

    public int StoklarId { get; set; }
    public Stoklar Stoklar { get; set; }
  }
}
