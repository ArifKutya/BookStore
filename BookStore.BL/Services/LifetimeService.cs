using BookStore.BL.Interfaces;
using BookStore.Models.Base;

namespace BookStore.BL.Services
{
    public class LifetimeService : ILifeTimeService
    {
        private string _id;
        public LifetimeService()
        {
            _id = Guid.NewGuid().ToString();
        }
       
        public string GetId()
        {
            return _id;
        }
        public string GetIdNew()
        {
            return _id;
        }
    }

    public interface ILifeTimeService
    {
        string GetId();
    }
}
