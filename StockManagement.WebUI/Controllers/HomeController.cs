using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagement.WebUI.Repository.Abstract;

namespace StockManagement.WebUI.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
    private IUnitOfWork uow;
    public HomeController(IUnitOfWork _uow)
    {
      uow = _uow;
    }


    public IActionResult Index()
    {


      ViewBag.ZimmetSayisi = uow.Zimmetler.GetAll().Count();
      ViewBag.CalisanSayisi = uow.Calisanlar.GetAll().Count();
      ViewBag.AtikSayisi = uow.Atiklar.GetAll().Count();


      return View();
    }

  
  }
}
