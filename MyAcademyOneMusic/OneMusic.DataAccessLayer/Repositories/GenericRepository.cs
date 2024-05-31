using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class // Generic Repository, IGenericDal Interface'inden Miras alır zaten implement edildiğinde IGenericDal içerisindeki tüm metotlar buraya geliyor. 
    {
        private readonly OneMusicContext _context;  //contect'imizi burada çağırıyoruz. _context üzerine gelip ctrl + . ile "generate constructor'ı" seçerek constructor'u oluşturuyoruz.

        public GenericRepository(OneMusicContext context) //oluşturulan constructor
        {
            _context = context; // constructor yapısı : GenericRepository sınıfı ile işlem yapacağımız zaman bu sınıfı başka bir yerde çağırdığımız anda "OneMusicContext" ile hemen bir nesne (context) örnekler ve bu nesneyi de hemen "_context = context" ile eşitler
                                // biz MVC yapısında sınıfımızı OneMusicContext context = new OneMusicContext() olarak new'lrdik. fakat bir sınıfı new'lemek o sınıfa bağımlılık demek oluyor.
                                // biz ise sınıfa bağımlı olmak yerine o sınıftan bir parametre türetip (context) o parametreye de field'i atıyoruz ( _context ).
                                // Ayrıca zaten Interface'ler de new'lenemez. biz zaten controllerlarımız içerisinde de işlemlerimizi yaparken sınıflarımızı new'leyerek yapmayacapız.
                                // hep interface'leri çağıracağız.
                                // Sınıftan nesne türetmeyeceğiz. İnterface'ten bir field (_context) türeticez ve o field sayesinde işlemlerimizi yapıcaz.
                                // Böylelikle hiç bir sınıfa bağımlılığımız olmayacak.
        }

        public void Create(T entity)  // Burası Generic bir yapı olduğu için burda herhangi bir sınıf çağırmak yerine bir T nesnesi türetip sınıfları temsil edecek şekilde onu kullanırız.
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var value = _context.Set<T>().Find(id); //önce id'ye göre bir değer getirip ardından Remove metodu ile sileriz.
            _context.Set<T>().Remove(value); // burda da value null değer dönebilir ama biz butona tıklayarak id bazlı getircemiz için sıkıntı oluşturmayacak
            _context.SaveChanges(); // değişiklikleri kaydetmeli
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);  // burda null değer dönebilir ama biz butona tıklayarak id bazlı getircemiz için sıkıntı oluşturmayacak
        }

        public List<T> GetList()
        {
            return _context.Set<T>().ToList();  // T entitylerimize karşılık gelir, Set<T> metodu ise hangi tablo olduğunu belirtmek için kullanılır, yani T den gelen parametre
        }

        public void Update(T entity)
        {
            _context.Update(entity); //mvc de update metodu yoktu burda böyle bir kolaylık var
            _context.SaveChanges();

        }
    }
}

//metotların imzasını interfacede yaptık içerisini ise (yapacağı işlemler) repository de doldurucaz.
