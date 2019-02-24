using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagement.WebUI.Entity;
using StockManagement.WebUI.IdentityCore;
using StockManagement.WebUI.Repository.Abstract;

namespace StockManagement.WebUI.Controllers
{
  [Authorize(Roles = "admin,bolumy")]
  public class ZimmetController : Controller
  {
    private IUnitOfWork uow;
    private UserManager<ApplicationUser> userManager;
    public ZimmetController(IUnitOfWork _uow, UserManager<ApplicationUser> _userManager)
    {
      userManager = _userManager;
      uow = _uow;
    }
    public async Task<IActionResult> Index(int BolumId=0)
    {
      var user = await userManager.GetUserAsync(User);
      var calisan = uow.Calisanlar.Find(i => i.CalisanId == user.CalisanId).FirstOrDefault();

      if(calisan.BolumId!=BolumId && BolumId != 0 && User.IsInRole("bolumy"))
      {

        return Redirect("/Zimmet?BolumId=" + calisan.BolumId);
      }
      if (User.IsInRole("bolumy") && BolumId==0)
      {

        return Redirect("/Zimmet?BolumId="+calisan.BolumId);
      }
      


      if (BolumId!=0)
      {
        ViewBag.Zimmetler = uow.Zimmetler.GetAll().Include(i => i.Calisan).ThenInclude(i => i.Bolum).Include(i => i.Stoklar).Where(i => i.Calisan.BolumId == BolumId).ToList();
        ViewBag.BolumId = BolumId;
      }
      else { 
      ViewBag.Zimmetler = uow.Zimmetler.GetAll().Include(i=>i.Calisan).ThenInclude(i=>i.Bolum).Include(i=>i.Stoklar).ToList();
      }
      ViewBag.Bolumler = uow.Bolumler.GetAll().ToList();

      return View();
    }

    
    [HttpPost]
    public IActionResult ZimmetSil(int ZimmetId)
    {

      if (ModelState.IsValid)
      {
        Zimmet entity = uow.Zimmetler.Find(i => i.ZimmetId == ZimmetId).FirstOrDefault(); 
        uow.Zimmetler.Delete(entity);
        uow.SaveChanges();
        return RedirectToAction("Index");
      }


      return RedirectToAction("Index");

    }

    [HttpPost]
    public IActionResult AtikGonder(int ZimmetId)
    {
      Zimmet entity = uow.Zimmetler.Find(i => i.ZimmetId == ZimmetId).FirstOrDefault();
      uow.Zimmetler.Delete(entity);
      uow.SaveChanges();


      Atik a = new Atik();

      a.AtikTarih = DateTime.Now;
      a.StoklarId = entity.StoklarId;

      uow.Atiklar.Add(a);
      uow.SaveChanges();
      return RedirectToAction("Index");
      
    }
  }
}
