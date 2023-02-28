using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Base;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IEnumerable<Author> GetAll()
        {
            return _authorRepository.GetAll();
        }

        public Author GetById(int id)
        {
            return _authorRepository.GetById(id);
        }

        public void Add(Author author)
        {
            _authorRepository.Add(author);
        }
    }
}
