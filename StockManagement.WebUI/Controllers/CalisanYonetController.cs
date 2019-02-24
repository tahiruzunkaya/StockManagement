using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockManagement.WebUI.Entity;
using StockManagement.WebUI.IdentityCore;
using StockManagement.WebUI.Model;
using StockManagement.WebUI.Repository.Abstract;

namespace StockManagement.WebUI.Controllers
{
  [Authorize(Roles ="admin")]
  public class CalisanYonetController : Controller
  {


    private IUnitOfWork uow;
    private UserManager<ApplicationUser> userManager;

    public CalisanYonetController(IUnitOfWork _uow, UserManager<ApplicationUser> _userManager)
    {
      userManager = _userManager;
      uow = _uow;
    }
    public IActionResult Index()
    {
      ViewBag.Bolumler = new SelectList(uow.Bolumler.GetAll(), "BolumId", "BolumAdi");
      ViewBag.Calisanlar = uow.Calisanlar.GetAll().Include(i => i.Bolum).ToList();

      
      return View();
    }

    [HttpPost]
    public IActionResult AddCalisan(Calisan entity)
    {
      if (ModelState.IsValid)
      {
        uow.Calisanlar.Add(entity);
        uow.SaveChanges();

        return RedirectToAction("Index");
      }

      return View(entity);

    }

    [HttpPost]
    public async Task<IActionResult> YetkiEkle(string UserName, string Password, string role,int CalisanId)
    {

      if (ModelState.IsValid)
      {

        if (await userManager.FindByNameAsync(UserName) == null)
        {
          ApplicationUser user = new ApplicationUser()
          {
            UserName = UserName,
            CalisanId = CalisanId
          };
          IdentityResult result = await userManager.CreateAsync(user, Password);

          if (result.Succeeded)
          {
            await userManager.AddToRoleAsync(user, role);
            Calisan entity = uow.Calisanlar.Find(i => i.CalisanId == CalisanId).FirstOrDefault();
            entity.IsYetkili = true;
            uow.Calisanlar.Edit(entity);
            uow.SaveChanges();
            return RedirectToAction("Index");
          }
          else
          {
            ModelState.AddModelError("hata1", "Ekleme sırasında bir hata oluştu.");
          }
        }
        else
        {
          ModelState.AddModelError("hata2", "Böyle bir user bulunmaktadır.");
        }
      }

      return RedirectToAction("Index");
    }

   
  }
}
