using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultAlbumComponent(IAlbumService albumService) : ViewComponent  //.Net 8 ile gelen primary constructor yapısı
    {
        public IViewComponentResult Invoke()
        {
            var values = albumService.TGetAlbumsWithArtist();
            return View(values);
        }
    }
}
