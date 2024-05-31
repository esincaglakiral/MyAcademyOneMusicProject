using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class AdminEventController : Controller
    {
        private readonly IEventService _eventService;

        public AdminEventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            var values = _eventService.TGetList();
            return View(values);
        }

        public IActionResult DeleteEvent(int id)
        {
            _eventService.TDelete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEvent(Event eventlist) 
        {
            _eventService.TCreate(eventlist);
            return RedirectToAction("Index");
        }


    [HttpGet]
    public IActionResult UpdateEvent(int id)
    {
        var values = _eventService.TGetById(id);
        return View(values);
    }

    [HttpPost]
    public IActionResult UpdateEvent(Event eventlist)
    {
        _eventService.TUpdate(eventlist);
        return RedirectToAction("Index");
    }


}
}
