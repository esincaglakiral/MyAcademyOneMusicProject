using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.BusinessLayer.Concrete;
using OneMusic.BusinessLayer.Validators;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Concrete;
using OneMusic.DataAccessLayer.Context;
using OneMusic.EntityLayer.Entities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<OneMusicContext>().AddErrorDescriber<CustomErrorDescriber>();

// Program�n katmanlar aras�nda tan�mlad���m�z metotlar� i�lemleri �al��t�rabilmesi i�in her bir katman nerden biras alm��sa onu burda mutlaka tan�mlamam�z gerekir.
// Unutmayal�mki proje WebUI katman�ndan �al���yordu ve program.cs de bu katman�n i�erisinde. 
builder.Services.AddScoped<IAboutDal, EfAboutDal>(); //DataAccessLayer: IAboutDal i�erisindeki metotlar EfAboutDal da yaz�lm��t�r. (registiration i�lemi)
builder.Services.AddScoped<IAboutService, AboutManager>(); // BusinessLayer: IAboutService i�erisindeki metotlar AboutManager da yaz�lm��t�r. (registiration i�lemi)

builder.Services.AddScoped<IAlbumDal, EfAlbumDal>();
builder.Services.AddScoped<IAlbumService, AlbumManager>();

builder.Services.AddScoped<IBannerDal, EfBannerDal>();
builder.Services.AddScoped<IBannerService, BannerManager>();

builder.Services.AddScoped<ISingerDal, EfSingerDal>();
builder.Services.AddScoped<ISingerService, SingerManager>();

builder.Services.AddScoped<ISongDal, EfSongDal>();
builder.Services.AddScoped<ISongService, SongManager>();

builder.Services.AddScoped<IContactDal, EfContactDal>();
builder.Services.AddScoped<IContactService, ContactManager>();

builder.Services.AddScoped<IEventDal, EfEventDal>();
builder.Services.AddScoped<IEventService, EventManager>();

builder.Services.AddScoped<IMessageDal, EfMessageDal>();
builder.Services.AddScoped<IMessageService, MessageManager>();

builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddValidatorsFromAssemblyContaining<SingerValidator>();


builder.Services.AddDbContext<OneMusicContext>();  //  DbContext�imiz de OneMusicContext olarak belirtiriz. Bunu vermezsek projeyi �al��t�ramay�z!!

builder.Services.AddControllersWithViews(option =>
{

    var authorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); //otantike olan bir kullan�c� gerekiyor authorize i�in


	option.Filters.Add(new AuthorizeFilter(authorizePolicy));  //Bu �ekilde proje seviyesinde b�t�n Controllerlar�m�za Authorize eklemi� olduk 
    // fakat sadece buras�yla b�rak�rsa Loginle kay�t olan t�m sayfalara eri�im kesilmi� oluyor. Bu sorunun giderilmesi i�in Login ve Register
    // controllerlar�na AllowAnonymous ekleriz ki onlara eri�im sa�layabilelim.

});

builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Login/Index";  // bu i�lemi mvc de web.config de yapard�k, net core da ise program.cs de yapar�z
    //options.AccessDeniedPath = "/ErrorPage/AccessDenied"; //yetkisi olmayan ki�i bu sayfatya eri�ir
    options.LogoutPath = "/Login/LogOut";

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404/", "?code{0}");  // olmayan bir sayfaya eri�meye �al���nca

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});


app.Run();
