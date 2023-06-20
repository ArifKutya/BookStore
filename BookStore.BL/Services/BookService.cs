using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    ppublic class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<Book> _bookValidator;

    public BookService(IBookRepository bookRepository, IValidator<Book> bookValidator)
    {
        _bookRepository = bookRepository;
        _bookValidator = bookValidator;
    }

    public async Task<Book> GetBookByIdAsync(Guid id)
    {
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task CreateBookAsync(Book book)
    {
        var validationResult = await _bookValidator.ValidateAsync(book);
        if (!validationResult.IsValid)
        {
            // Обработка на грешките валидацията
            throw new Exception("Invalid book data.");
        }

        await _bookRepository.CreateAsync(book);
    }

    public async Task UpdateBookAsync(Book book)
    {
        var validationResult = await _bookValidator.ValidateAsync(book);
        if (!validationResult.IsValid)
        {
            // Обработка на грешките валидацията
            throw new Exception("Invalid book data.");
        }

        await _bookRepository.UpdateAsync(book);
    }

    public async Task DeleteBookAsync(Guid id)
    {
        await _bookRepository.DeleteAsync(id);
    }
}
}
