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
  [Authorize(Roles ="admin")]
  public class AtikController : Controller
  {
    private IUnitOfWork uow;
    public AtikController(IUnitOfWork _uow)
    {
      uow = _uow;
    }
    public IActionResult Index()
    {
      ViewBag.Atiklar = uow.Atiklar.GetAll().Include(i => i.Stoklar).ThenInclude(i => i.Urun).ToList();
      return View();
    }
  }
}
