using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();

        Book GetById(int id);

        void Add(Book author);

        void Delete(int id);

        void Update(Book book);
    }
}
