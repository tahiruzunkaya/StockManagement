using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockManagement.WebUI.Entity;
using StockManagement.WebUI.Repository.Abstract;

namespace StockManagement.WebUI.Controllers
{
  [Authorize(Roles ="admin")]
  public class StokController : Controller
  {
    private IUnitOfWork uow;
    public StokController(IUnitOfWork _uow)
    {
      uow = _uow;
    }


    public IActionResult Index()
    {

      ViewBag.Calisanlar = uow.Calisanlar.GetAll().ToList();



      List<Stoklar> stoklar = new List<Stoklar>();
      stoklar.Clear();
      stoklar = uow.Stoklar.GetAll().Include(i => i.Urun).ToList();

      List<Zimmet> zimmetler = new List<Zimmet>();
      zimmetler.Clear();

      List<Atik> atiklar = new List<Atik>();
      atiklar.Clear();
      zimmetler = uow.Zimmetler.GetAll().ToList();
      atiklar = uow.Atiklar.GetAll().ToList();

      foreach (var item in stoklar.ToList())
      {
        foreach (var item1 in zimmetler.ToList())
        {
          if (item1.StoklarId == item.StoklarId)
          {
            stoklar.Remove(item);
          }
        }
        foreach (var item1 in atiklar.ToList())
        {
          if (item1.StoklarId == item.StoklarId)
          {
            stoklar.Remove(item);
          }

        }
      }

      ViewBag.Stoklar = stoklar;
      ViewBag.Zimmetler = uow.Zimmetler.GetAll().ToList();
      return View();
    }

    [HttpPost]
    public IActionResult Zimmetle(int CalisanId,int StoklarId)
    {

      
      if (ModelState.IsValid)
      {
        Zimmet entity = new Zimmet();
        entity.CalisanId = CalisanId;
        entity.StoklarId = StoklarId;
        entity.ZimmetTarih = DateTime.Now;

        uow.Zimmetler.Add(entity);
        uow.SaveChanges();
        return RedirectToAction("Index");
      }
      return RedirectToAction("Index");


    }
  }
}
