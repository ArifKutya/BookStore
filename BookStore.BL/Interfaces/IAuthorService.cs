using BookStore.Models.Base;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAll();

        Author GetById(int id);

        void Add(Author author);

        void Delete(int id);

        void Update(Author author);
    }
}
