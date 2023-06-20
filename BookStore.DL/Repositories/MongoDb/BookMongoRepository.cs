using BookStore.DL.Interfaces;
using BookStore.Models.Configurations;
using BookStore.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookStore.DL.Repositories.MongoDb
{
   public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _bookCollection;

    public BookRepository(IMongoDatabase database)
    {
        _bookCollection = database.GetCollection<Book>("Books");
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

}
