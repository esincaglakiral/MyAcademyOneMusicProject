using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        // CRUD => Create, Read, Update ve Delete
        List<T> TGetList(); //başlarına T ekleriz ki IGenericDal'dan(DataAccessLayer katmanındaki) bunu ayırmak için
        T TGetById(int id);

        void TCreate(T entity);
        void TUpdate(T entity);
        void TDelete(int id);
    }
}