using MongoDB.Bson.Serialization.Attributes;

namespace BookStore.Models.Models
{
    public class Book
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid AuthorId { get; set; }
    }
}
