using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class AuthorInMemoryRepository : IAuthorRepository
    {
        public IEnumerable<Author> GetAll()
        {
            return InMemoryDb.Data.Authors;
        }

        public Author GetById(int id)
        {
            return InMemoryDb.Data
                .Authors.SingleOrDefault(x => x.Id == id);
        }

        public void Add(Author author)
        {
            InMemoryDb.Data.Authors.Add(author);
        }

        public void Delete(int id)
        {
            var author =
                InMemoryDb.Data.Authors
                    .FirstOrDefault(x => x.Id == id);

            if (author != null)
            {
                InMemoryDb.Data.Authors.Remove(author);
            }
        }

        public void Update(Author author)
        {
            var authorForUpdate =
                InMemoryDb.Data.Authors
                    .FirstOrDefault(x => x.Id == author.Id);

            if (authorForUpdate != null)
            {
                authorForUpdate.Name = author.Name;
            }
        }
    }
}
