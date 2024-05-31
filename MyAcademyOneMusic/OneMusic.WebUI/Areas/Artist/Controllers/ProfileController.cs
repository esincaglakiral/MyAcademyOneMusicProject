using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Areas.Artist.Models;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]  //area içerisindeki artist controller
    [Authorize(Roles = "Artist")] //sadece rolü artist olan kişiler bu controller'a erişebilecek
    [Route("[area]/[controller]/[action]/{id?}")]  // burası bir area içerisinde olduğu için area içerisindeki controllerlara bu route 'ı belirtiriz
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = new ArtistEditViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Surname = user.Surname,
                ImageUrl = user.ImageUrl,
                UserName = user.UserName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ArtistEditViewModel model)
        {
            ModelState.Clear();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (model.ImageFile != null)  //değer varsa
            {
                var resource = Directory.GetCurrentDirectory(); //şuanki projenin bulunduğu dosy yolunu bul

                var extension = Path.GetExtension(model.ImageFile.FileName).ToLower(); //Dosya isminin uzatsını alır (jpeg, png)

                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                {
                    // Desteklenmeyen dosya uzantısı hatası
                    ModelState.AddModelError("ImageFile", "Sadece JPG dosyaları kabul edilir.");
                    // Gerekirse, işlemi sonlandırabilirsiniz.
                    return View(model);
                }
                var imageName = Guid.NewGuid() + extension; //evrensel eşsiz bir id değeri atar
                var saveLocation = resource + "/wwwroot/images/" + imageName; // kaydedeceğimiz yol
                var stream = new FileStream(saveLocation, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);
                user.ImageUrl = "/images/" + imageName; 
            }

    

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.UserName;
            user.Email = model.Email;

            var result = await _userManager.CheckPasswordAsync(user, model.OldPassword);
            if (result == true)
            {
                if (model.NewPassword != null && model.ConfirmPassword == model.NewPassword)  //değerler tutuyorsa şifre değiştircek
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {
                        foreach (var item in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                            return View();
                        }
                    }
                }


                var updateResult =  await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            ModelState.AddModelError("", "Mevcut Şifreniz Hatalı");
            return View();

        }
    }
}
