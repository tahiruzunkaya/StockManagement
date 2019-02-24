using StockManagement.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.WebUI.Repository.Concrete.EntityFramework
{
  public class EfUnitOfWork : IUnitOfWork
  {
    private readonly StockManagementContext dbContext;

    public EfUnitOfWork(StockManagementContext _dbContext)
    {
      dbContext = _dbContext ?? throw new ArgumentNullException("Db Context can not be null");
    }

    private IBolumRepository _bolumler;
    private ICalisanRepository _calisanlar;
    private IStoklarRepository _stoklar;
    private IStokRepository _stoks;
    private IUrunRepository _urunler;
    private IZimmetRepository _zimmetler;
    private IAtikRepository _atiklar;
    private IUrunKategoriRepository _urunKategoriler;
    public IBolumRepository Bolumler
    {
      get
      {
        return _bolumler ?? (_bolumler = new EfBolumRepository(dbContext));
      }
    }

    public ICalisanRepository Calisanlar
    {
      get
      {
        return _calisanlar ?? (_calisanlar = new EfCalisanRepository(dbContext));
      }
    }
    public IStoklarRepository Stoklar
    {
      get
      {
        return _stoklar ?? (_stoklar = new EfStoklarRepository(dbContext));
      }
    }
    public IStokRepository Stoks
    {
      get
      {
        return _stoks ?? (_stoks = new EfStokRepository(dbContext));
      }
    }
    public IUrunRepository Urunler
    {
      get
      {
        return _urunler ?? (_urunler = new EfUrunRepository(dbContext));
      }
    }
    public IZimmetRepository Zimmetler
    {
      get
      {
        return _zimmetler ?? (_zimmetler = new EfZimmetRepository(dbContext));
      }
    }

    public IUrunKategoriRepository UrunKategoriler
    {
      get
      {
        return _urunKategoriler ?? (_urunKategoriler = new EfUrunKategoriRepository(dbContext));
      }
    }

    public IAtikRepository Atiklar
    {
      get
      {
        return _atiklar ?? (_atiklar = new EfAtikRepository(dbContext));
      }
    }
    public void Dispose()
    {
      dbContext.Dispose();
    }

    public int SaveChanges()
    {
      try
      {
        return dbContext.SaveChanges();
      }
      catch (Exception)
      {

        throw;
      }
    }
  }
}
