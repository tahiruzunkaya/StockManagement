using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Repository.Abstract
{
  public interface IUnitOfWork : IDisposable
  {
    IBolumRepository Bolumler { get; }
    ICalisanRepository Calisanlar { get; }
    IStoklarRepository Stoklar { get; }
    IUrunKategoriRepository UrunKategoriler { get; }
    IStokRepository Stoks { get; }
    IUrunRepository Urunler { get; }
    IZimmetRepository Zimmetler { get; }
    IAtikRepository Atiklar { get; }
   


    int SaveChanges();
  }
}
