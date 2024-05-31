using System.ComponentModel.DataAnnotations;

namespace OneMusic.WebUI.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler birbiri ile uyumlu değil")]
        public string ConfirmPassword { get; set; }


    }
}