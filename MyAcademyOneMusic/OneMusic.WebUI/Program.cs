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

// Programýn katmanlar arasýnda tanýmladýðýmýz metotlarý iþlemleri çalýþtýrabilmesi için her bir katman nerden biras almýþsa onu burda mutlaka tanýmlamamýz gerekir.
// Unutmayalýmki proje WebUI katmanýndan çalýþýyordu ve program.cs de bu katmanýn içerisinde. 
builder.Services.AddScoped<IAboutDal, EfAboutDal>(); //DataAccessLayer: IAboutDal içerisindeki metotlar EfAboutDal da yazýlmýþtýr. (registiration iþlemi)
builder.Services.AddScoped<IAboutService, AboutManager>(); // BusinessLayer: IAboutService içerisindeki metotlar AboutManager da yazýlmýþtýr. (registiration iþlemi)

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


builder.Services.AddDbContext<OneMusicContext>();  //  DbContext’imiz de OneMusicContext olarak belirtiriz. Bunu vermezsek projeyi çalýþtýramayýz!!

builder.Services.AddControllersWithViews(option =>
{

    var authorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); //otantike olan bir kullanýcý gerekiyor authorize için


	option.Filters.Add(new AuthorizeFilter(authorizePolicy));  //Bu þekilde proje seviyesinde bütün Controllerlarýmýza Authorize eklemiþ olduk 
    // fakat sadece burasýyla býrakýrsa Loginle kayýt olan tüm sayfalara eriþim kesilmiþ oluyor. Bu sorunun giderilmesi için Login ve Register
    // controllerlarýna AllowAnonymous ekleriz ki onlara eriþim saðlayabilelim.

});

builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Login/Index";  // bu iþlemi mvc de web.config de yapardýk, net core da ise program.cs de yaparýz
    //options.AccessDeniedPath = "/ErrorPage/AccessDenied"; //yetkisi olmayan kiþi bu sayfatya eriþir
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

//app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404/", "?code{0}");  // olmayan bir sayfaya eriþmeye çalýþýnca

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
