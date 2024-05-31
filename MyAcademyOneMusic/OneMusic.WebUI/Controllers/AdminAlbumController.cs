using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminAlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISongService _songService;       
        private readonly ICategoryService _categoryService;

        public AdminAlbumController(IAlbumService albumService, UserManager<AppUser> userManager, ISongService songService, ICategoryService categoryService)
        {
            _albumService = albumService;
            _userManager = userManager;
            _songService = songService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var values = _albumService.TGetAlbumsWithArtist();
            return View(values);
        }

        public IActionResult DeleteAlbum(int id)
        {
            _albumService.TDelete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> CreateAlbum()
        {
            var categories = _categoryService.TGetList();

            var categoryList = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();

        
            ViewBag.CategoryList = categoryList;

            // Tüm sanatçıları aldım ve ViewBag'e ekledim
            var artists = await _userManager.GetUsersInRoleAsync("Artist");
            ViewBag.Singers = artists.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = $"{a.Name} {a.Surname}"
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult CreateAlbum(Album album)
        {
            // Albümü kaydettim
            _albumService.TCreate(album);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateAlbum(int id)
        {
            var categories = _categoryService.TGetList();

            var categoryList = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();


            ViewBag.CategoryList = categoryList;

            var values = _albumService.TGetById(id);

        // Tüm sanatçıları alıp ViewBag'e ekledim
           var artists = await _userManager.GetUsersInRoleAsync("Artist");
           ViewBag.SingerId = artists.Select(a => new SelectListItem 
          { 
              Value = a.Id.ToString(), 
              Text = $"{a.Name} {a.Surname}" 
          }).ToList();

         return View(values);
        }


        [HttpPost]
        public IActionResult UpdateAlbum(Album album)
        {
            _albumService.TUpdate(album);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> AlbumsByArtist(int artistId)
        {
            var artist = await _userManager.FindByIdAsync(artistId.ToString());
            var albums = _albumService.TGetAlbumsByArtist(artistId);

            var model = new AlbumsByArtistViewModel
            {
              Artist = artist,
              Albums = albums,
         
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> GetSongsByAlbumId(int albumId)
        {
            // Albüm ID'sine göre şarkıları getir

            var songs = _songService.TGetSongsByAlbumId(albumId);

            var model = new AlbumsByArtistViewModel
            {
                Songs = songs
            };
            return View(model);
        }



    }

    public class AlbumsByArtistViewModel
    {
        public AppUser Artist { get; set; }
        public Album Album { get; set; } // Albüm bilgisi eklendi

        public List<Album> Albums { get; set; }
        public List<Song> Songs { get; set; }
    }
}