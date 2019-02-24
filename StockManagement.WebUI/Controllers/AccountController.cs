using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagement.WebUI.IdentityCore;
using StockManagement.WebUI.Model;

namespace StockManagement.WebUI.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private UserManager<ApplicationUser> userManager;
    private SignInManager<ApplicationUser> signInManager;

    public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
    {
      userManager = _userManager;
      signInManager = _signInManager;
    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl)
    {
      ViewBag.returnUrl = returnUrl;
      return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model,string returnUrl)
    {
      if (ModelState.IsValid)
      {
        var user = await userManager.FindByNameAsync(model.UserName);

        if (user != null)
        {
          await signInManager.SignOutAsync();
          var result = await signInManager.PasswordSignInAsync(user,model.Password,false,false);
          if (result.Succeeded)
          {
            return Redirect(returnUrl??"/");
          }
          else
          {
            ModelState.AddModelError(nameof(model.Password), "Hatalı Kullanıcı Adı yada Şifre");
          }
        }
        else
        {
          ModelState.AddModelError(nameof(model.UserName),"Hatalı Kullanıcı Adı yada Şifre");
        }
      }
      return View(model);
    }

    public async Task<IActionResult> Logout()
    {
      await signInManager.SignOutAsync();
      return RedirectToAction("Login");
    }


    public IActionResult AccessDenied()
    {

      return View();
    }
  }
}
