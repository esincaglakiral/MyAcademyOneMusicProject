using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.DataAccessLayer.Repositories;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Concrete
{
    public class EfAboutDal : GenericRepository<About>, IAboutDal  //IAboutDal içerisinde farklı bir metot daha yazdırmak istersem yani sadece bu entitye özgü farklı bir metot yazmak istiyorsam (orneğin count metodu) IAboutDal a yazabilirim haliyle miras alması için buraya da implement ederiz
    {
        //constructor : EfAboutDal üstüne gelip Generate constructor'ı dahil ediyoruz.
        public EfAboutDal(OneMusicContext context) : base(context)  //base: EfAboutDal sınıfı nerden miras almışşsa (GenericRepository) bir üst sınıftaki parametreye karşılık geliyor
        {
        }
    }
}

//Entityler(classlar: about,song ...) içerisinde prop dışında başka birşey yazmamamız daha sağlıklıdır solid prensiplerine göre