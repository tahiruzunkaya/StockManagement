using StockManagement.WebUI.Entity;
using StockManagement.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Repository.Concrete.EntityFramework
{
    public class EfUrunKategoriRepository:EfGenericRepository<UrunKategori>, IUrunKategoriRepository
    {
    public EfUrunKategoriRepository(StockManagementContext context)
     : base(context)
    {

    }
    public StockManagementContext StockContext
    {
      get { return context as StockManagementContext; }
    }
  }
}
