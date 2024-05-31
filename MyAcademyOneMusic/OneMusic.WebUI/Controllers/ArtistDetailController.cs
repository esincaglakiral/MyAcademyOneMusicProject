using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    [AllowAnonymous]
    public class ArtistDetailController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISongService _songService;
        public ArtistDetailController(IAlbumService albumService, UserManager<AppUser> userManager, ISongService songService)
        {
            _albumService = albumService;
            _userManager = userManager;
            _songService = songService;
        }

      
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AlbumsByArtistDetail(int artistId)
        {
            var artist = await _userManager.FindByIdAsync(artistId.ToString());
            var albums = _albumService.TGetAlbumsByArtist(artistId);
            var songs = _songService.TGetSongsWithAlbumByUserId(artistId);

            var model = new AlbumsByArtistDetailViewModel
            {
                Artist = artist,
                Albums = albums,
                Songs = songs
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> GetSongsByAlbumDetail(int albumId)
        {
            // Albüm ID'sine göre şarkıları getir
            var artist = await _userManager.FindByIdAsync(albumId.ToString());
            var songs = _songService.TGetSongsByAlbumId(albumId);
            var albums = _albumService.TGetAlbumsByArtist(albumId);

            var model = new AlbumsByArtistDetailViewModel
            {
                Albums = albums,
                Artist = artist,
                Songs = songs
            };
           
            return View(model);
        }
    }

    public class AlbumsByArtistDetailViewModel
    {
        public AppUser Artist { get; set; }
        public Album Album { get; set; } // Albüm bilgisi eklendi

        public List<Album> Albums { get; set; }
        public List<Song> Songs { get; set; }
    }
}
