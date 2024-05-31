using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Validators
{
	public class CustomErrorDescriber : IdentityErrorDescriber  //Hata Mesajlarını Türkçeleştirme
	{
		public override IdentityError PasswordTooShort(int length) // minimum karakter sınırı belirleme
		{
			return new IdentityError
			{
				Description = "Şifre en az 6 karakterden oluşmalıdır."
			};
		}

		public override IdentityError PasswordRequiresDigit()  //rakam içermeli
		{
			return new IdentityError
			{
				Description = "Şifre en az bir rakam(1-2-3...) içermelidir."
			};
		}

		public override IdentityError PasswordRequiresLower() //küçük harf
		{
			return new IdentityError
			{
				Description = "Şifre en az bir küçük harf(a-z) içermelidir."
			};
		}

		public override IdentityError PasswordRequiresUpper() //büyük harf
		{
			return new IdentityError
			{
				Description = "Şifre en az bir büyük harf(A-Z) içermelidir."
			};
		}

		public override IdentityError PasswordRequiresNonAlphanumeric()  //özel karakter
		{
			return new IdentityError
			{
				Description = "Şifre en az bir özel karakter(*,-,_,+...) içermelidir."
			};
		}

		public override IdentityError InvalidUserName(string? userName)
		{
			return new IdentityError
			{
				Description = "Bu Kullanıcı Adını Kullanamazsınız"
			};
		}


        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Description = "Bu Kullanıcı AdıDaha Önce Alınmıştır"
            };
        }
    }
}