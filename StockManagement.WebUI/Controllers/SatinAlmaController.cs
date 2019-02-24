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
  [Authorize(Roles ="admin,satis")]
  public class SatinAlmaController : Controller
  {
    private IUnitOfWork uow;
    public SatinAlmaController(IUnitOfWork _uow)
    {
      uow = _uow;
    }

    public IActionResult Index()
    {
      IEnumerable<SelectListItem> items = uow.Urunler.GetAll().Include(i => i.Stok).Where(i => i.Stok.Adet > 0).Select(c => new SelectListItem
      {
        
        Value = c.UrunId.ToString(),
        Text = c.UrunAdi + " " + c.Aciklamasi

      });
      ViewBag.Urunler = items;

      ViewBag.Kategoriler = new SelectList(uow.UrunKategoriler.GetAll(),"UrunKategoriId","KategoriAdi");
      return View();
    }

    [HttpPost]
    public IActionResult SatinAlVar(int UrunId,int Adet)
    {
      if (ModelState.IsValid)
      {
        for (int i = 0; i < Adet; i++)
        {
          Stoklar s = new Stoklar();

          s.UrunId = UrunId;
          uow.Stoklar.Add(s);
          uow.SaveChanges();
        }
        
        Stok entity = uow.Stoks.GetAll().Where(i => i.UrunId == UrunId).FirstOrDefault();

        entity.Adet += Adet;
        uow.Stoks.Edit(entity);
        uow.SaveChanges();
        return RedirectToAction("Index");
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult SatinAlYok(Urun entity,int Adet)
    {
      if (ModelState.IsValid)
      {
        uow.Urunler.Add(entity);
        uow.SaveChanges();
        for (int i = 0; i < Adet; i++)
        {
          Stoklar sa = new Stoklar();

          sa.UrunId = entity.UrunId;
          uow.Stoklar.Add(sa);
          uow.SaveChanges();
        }

        Stok s = new Stok();
        s.Adet = Adet;
        s.UrunId = entity.UrunId;
        uow.Stoks.Add(s);
        uow.SaveChanges();

        return RedirectToAction("Index");

      }
        return View(entity);
    }
  }
}
