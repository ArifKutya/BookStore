using BookStore.Models.Models;

namespace BookStore.DL.InMemoryDb
{
    public static class Data
    {
        public static List<Author> Authors = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "Pesho"
            },
            new Author()
            {
                Id = 2,
                Name = "Stamat"
            }
        };

        public static List<Book> Books = new List<Book>()
        {
            new Book()
            {
                Id = 1,
                Title = "Knigata na Pesho",
                AuthorId = 1
            },
            new Book()
            {
                Id = 2,
                Title = "Knigata na Stamat",
                AuthorId = 2
            }
        };
    }
}
