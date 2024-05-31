using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class // T bir sınıfı temsil ediyor burda
    {
        // CRUD => Create, Read, Update ve Delete
        List<T> GetList(); //List metodu olduğu için başına List<> gelir
        T GetById(int id);

        void Create(T entity); //dönüş tipi olmayan metotlara ise void gelir
        void Update(T entity);
        void Delete(int id);
    }
}
