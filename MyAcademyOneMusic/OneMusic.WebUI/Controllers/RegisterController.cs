using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Models;

namespace OneMusic.WebUI.Controllers
{
	[AllowAnonymous]  //aouthorize'dan muaf

	public class RegisterController : Controller
    {
        public readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(RegisterViewModel model)
        {
            AppUser user = new AppUser
            {
                Email = model.Email,
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.SurName
            };

            if(model.Password == model.ConfirmPassword)
            {
				var result = await _userManager.CreateAsync(user, model.Password);
                //AppUser türünde bir parametre bekliyor, o nedenle direk modeli vermedik parametre olarak, AppUserdaki parametreyi Model’deki Password e atadık
                //Ayrıca await keyword’ü de önemlidir.

                if (result.Succeeded) // sonuç başarılı olursa
				{
                    await _userManager.AddToRoleAsync(user, "Visitor");

					return RedirectToAction("Index", "Login"); //Logine yönlendir
                }
				foreach (var item in result.Errors)  //Hata mesajı döndürcek
				{
					ModelState.AddModelError("", item.Description);
				}
			}

            return View();
        }
    }
}