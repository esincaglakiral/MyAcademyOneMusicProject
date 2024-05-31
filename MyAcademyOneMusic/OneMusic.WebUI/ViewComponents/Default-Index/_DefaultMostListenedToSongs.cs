using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultMostListenedToSongs : ViewComponent
    {
        private readonly ISongService _songService;

        public _DefaultMostListenedToSongs(ISongService songService)
        {
            _songService = songService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _songService.TGetSongWithAlbum().OrderByDescending(x => x.SongValue).Take(6).ToList();
            return View(values);
        }
    }
}
