using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Concrete
{
    public class AboutManager : IAboutService  // AboutManager ==> IAboutService interface'inden miras alıyor.
                                               // IAboutService üzerine gelip "İmplement İnterface diyoruz ve metotlarımızı implement ediyoruz.
                                               // IAboutService ise IGenericService'ten miraz alıyordu haliyle IGenericService içindeki metotlar geliyor.
    {
        private readonly IAboutDal _aboutDal; // IAboutDal interface'inden bir _aboutDal field'i örnekledik.
                                              // Sınıftan bir nesne örneği türetebilirdik (new'leme) ama interfaceten bir nesne örneği türetemedğimiz için newlenemezdi.
                                              // ( _ eklememizin sebebi this. ile belirtmeyelim diye)

        public AboutManager(IAboutDal aboutDal) // constructor
        {
            _aboutDal = aboutDal; //biz sınıfa bağımlı olmak yerine o sınıftan bir parametre türetip (aboutDal) o parametreye de field'i atıyoruz ( _aboutDal ).
        }

        public void TCreate(About entity) //TCreate ise Business katmanında IGenericService 'ten gelen metot
        {
            _aboutDal.Create(entity);  // DataAccessten gelen Create metodu
        }

        public void TDelete(int id) //TDelete ise Business katmanında IGenericService 'ten gelen metot
        {
            _aboutDal.Delete(id); // DataAccessten gelen Delete metodu
        }

        public About TGetById(int id) // GetList ve GetById metotları dönüş tipi beklediği için "return" kullanılır
        {
            return _aboutDal.GetById(id);
        }

        public List<About> TGetList()
        {
            return _aboutDal.GetList();
        }

        public void TUpdate(About entity)
        {
            _aboutDal.Update(entity);
        }
    }
}