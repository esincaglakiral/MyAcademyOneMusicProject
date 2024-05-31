using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Abstract
{
    public  interface IAboutDal : IGenericDal<About>  // T yerine About geliyor yani GenericRepositorydeki (IGenericDal'dan miras alıyordu) tüm metotlarda T yerine About sınıfı geçiyormuş gibi işlem yapacak, diğer classlar içinde durum böyle. Miras almanın önemi burda belirtilmiştir.
    {
        //tek tek her bi interfacete ekleme silme güncelleme işlemleri için metot yazmak yerine;
        //bir generic interface oluşturup onda tanımlayacağız ve tüm ınterface'ler IGenericDal interface'inden miras alacaktır.
        //böylelikle her interface generic interface içerisindeki metotlara erişim sağlayacak
    }
}
