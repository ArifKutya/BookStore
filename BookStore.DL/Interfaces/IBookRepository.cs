using BookStore.Models.Base;
using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();

        Book GetById(int id);

        void Add(Book author);

        void Delete(int id);

        void Update(Book author);

        IEnumerable<Book> GetAllByAuthorId(int authorId);
    }
}
