using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Concrete
{
    public class EventManager : IEventService
    {
        private readonly IEventDal _eventDal;

        public EventManager(IEventDal eventDal)
        {
            _eventDal = eventDal;
        }

        public void TCreate(Event entity)
        {
            _eventDal.Create(entity);
        }

        public void TDelete(int id)
        {
            _eventDal.Delete(id);
        }

        public Event TGetById(int id)
        {
            return _eventDal.GetById(id);
        }

        public List<Event> TGetList()
        {
            return _eventDal.GetList();
        }

        public void TUpdate(Event entity)
        {
            _eventDal.Update(entity);
        }
    }
}
