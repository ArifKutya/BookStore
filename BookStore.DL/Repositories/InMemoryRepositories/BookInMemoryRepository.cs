using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class BookInMemoryRepository : IBookRepository
    {
        public IEnumerable<Book> GetAll()
        {
            return InMemoryDb.Data.Books;
        }

        public Book GetById(int id)
        {
            return InMemoryDb.Data
                .Books.SingleOrDefault(x => x.Id == id);
        }

        public void Add(Book author)
        {
            InMemoryDb.Data.Books.Add(author);
        }

        public void Delete(int id)
        {
            var book =
                InMemoryDb.Data.Books
                    .FirstOrDefault(x => x.Id == id);

            if (book != null)
            {
                InMemoryDb.Data.Books.Remove(book);
            }
        }

        public void Update(Book author)
        {
            var forUpdate =
                InMemoryDb.Data.Books
                    .FirstOrDefault(x => x.Id == author.Id);

            if (forUpdate != null)
            {
                forUpdate.Title = author.Title;
            }
        }

        public IEnumerable<Book> GetAllByAuthorId(int authorId)
        {
            return InMemoryDb.Data.Books
                .Where(book => book.AuthorId == authorId);
        }
    }
}
