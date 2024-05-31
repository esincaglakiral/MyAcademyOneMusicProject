using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Context
{
    public class OneMusicContext : IdentityDbContext<AppUser,AppRole,int> // IdentityDbContext'ten miras alcak. o sırada  Microsoft.AspNetCore.Identity.EntityFrameworkCore'u dahil etti.
    {
        //DbContext 'ten miras alması demek OneMusicContext'tin DbContext içerisindeki metotlara erişebilmesi içindir

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  // protected deme sebebimiz hem bulunduğu sınıf içerisinden, o sınıftan miras alan (alt sınıflar) sınıflardan erişilebilir.
        {
            optionsBuilder.UseSqlServer("server=CAGLA\\SQLEXPRESS;database=OneMusicDb;integrated security=true;trustServerCertificate=true"); //burada bağlantı adresimi gerçiyorum.
        }

        // artık DbSet'lerimi oluşturcam:
        public DbSet<About> Abouts { get; set; }  //About bizim class(entity) adımızken, Abouts ise sql tarafına yansıyan tablomuzun adıdır.
        public DbSet<Album> Albums { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Singer> Singers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }

    }
}
