using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAll();

        Author GetById(int id);

        void Add(AddAuthorRequest author);

        void Delete(int id);

        void Update(UpdateAuthorRequest author);
    }
}
