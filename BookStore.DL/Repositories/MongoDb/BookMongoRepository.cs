using BookStore.DL.Interfaces;
using BookStore.Models.Configurations;
using BookStore.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStore.DL.Repositories.MongoDb
{
    public class BookMongoRepository : IBookRepository
    {
        private readonly IMongoCollection<Book> _books;

        public BookMongoRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            var client = new MongoClient(
                mongoConfig.CurrentValue.ConnectionString);
            var database =
                client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _books = database
                .GetCollection<Book>($"{nameof(Book)}-BS");
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await
                _books.Find(author => true).ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
            return await _books
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Add(Book author)
        {
            await _books.InsertOneAsync(author);
        }

        public Task Delete(int id)
        {
            return _books.DeleteOneAsync(x => x.Id == id);
        }

        public async Task Update(Book book)
        {
            var filter =
                Builders<Book>.Filter.Eq(s => s.Id, book.Id);
            var update = Builders<Book>
                .Update.Set(s =>
                    s.Title, book.Title);

            await _books.UpdateOneAsync(filter, update);
        }

        public async Task<IEnumerable<Book>> GetAllByAuthorId(Guid authorId)
        {
            return await GetAll();
        }
    }
}
