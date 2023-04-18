using MongoDB.Bson.Serialization.Attributes;

namespace BookStore.Models.Base
{
    public class Person
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
