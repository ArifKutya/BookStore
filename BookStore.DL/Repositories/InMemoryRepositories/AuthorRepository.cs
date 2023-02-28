using BookStore.DL.Interfaces;
using BookStore.Models.Base;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class AuthorInMemoryRepository : IAuthorRepository
    {
        public IEnumerable<Author> GetAll()
        {
            return InMemoryDb.InMemoryDb.Authors;
        }

        public Author GetById(int id)
        {
            return InMemoryDb.InMemoryDb
                .Authors.SingleOrDefault(x => x.Id == id);
        }

        public void Add(Author author)
        {
            InMemoryDb.InMemoryDb.Authors.Add(author);
        }
    }
}
