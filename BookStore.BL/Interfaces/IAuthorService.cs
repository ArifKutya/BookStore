using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAll();

        Task<Author> GetById(Guid id);

        Task Add(AddAuthorRequest author);

        Task Delete(Guid id);

        Task Update(UpdateAuthorRequest author);
    }
}
