using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Responses;

namespace BookStore.BL.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public LibraryService(IAuthorRepository authorRepository,
            IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }
        public async Task<GetAllBooksByAuthorResponse> GetAllBooksByAuthorId(Guid authorId)
        {
            var author = await _authorRepository.GetById(authorId);
            var books = Enumerable.Empty<Book>();

            if (author != null)
            {
                books = await _bookRepository.GetAllByAuthorId(authorId);
            }

            return new GetAllBooksByAuthorResponse()
            {
                Author = author,
                Books = books
            };
        }
    }
}
