using BookStore.Models.Base;
using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book?> GetById(int id);
        Task Add(Book author);
        Task Delete(int id);
        Task Update(Book author);
        Task<IEnumerable<Book>> GetAllByAuthorId(Guid authorId);
    }
}
