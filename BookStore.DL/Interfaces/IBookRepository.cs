using BookStore.Models.Base;
using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
 public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _bookCollection;

    public BookRepository(IMongoDatabase database)
    {
        _bookCollection = database.GetCollection<Book>("books");
    }

    public async Task<Book> GetByIdAsync(Guid id)
    {
        return await _bookCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _bookCollection.Find(_ => true).ToListAsync();
    }

    public async Task CreateAsync(Book book)
    {
        await _bookCollection.InsertOneAsync(book);
    }

    public async Task UpdateAsync(Book book)
    {
        await _bookCollection.ReplaceOneAsync(x => x.Id == book.Id, book);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _bookCollection.DeleteOneAsync(x => x.Id == id);
    }
}

public interface IBookService
{
    Task<Book> GetBookByIdAsync(Guid id);
    Task<List<Book>> GetAllBooksAsync();
    Task CreateBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(Guid id);
}

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<Book> _bookValidator;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IValidator<Book> bookValidator, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _bookValidator = bookValidator;
        _mapper = mapper;
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
            throw new Exception("Invalid book data.");
        }

        await _bookRepository.CreateAsync(book);
    }

    public async Task UpdateBookAsync(Book book)
    {
        var validationResult = await _bookValidator.ValidateAsync(book);
        if (!validationResult.IsValid)
        {
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
