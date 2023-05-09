using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Moq;

namespace BookStore.Test
{
    public class BookServiceTest
    {
        private Mock<IBookRepository> _bookRepository;
        private Mock<IAuthorService> _authorService;

        private IList<Book> Books = new List<Book>()
        {
            new Book()
            {
                Id = new Guid("0c18f6ae-a16c-477e-b267-9184c4819742"),
                AuthorId = new Guid("dd9961d2-cab0-4bf5-9a9d-48862a64d63b"),
                Title = "Book1"
            },
            new Book()
            {
                Id = new Guid("0c18f6ae-a16c-477e-b267-9184c4819743"),
                AuthorId = new Guid("dd9961d2-cab0-4bf5-9a9d-48862a64d63b"),
                Title = "Book2"
            }
        };

        private IList<Author> Authors = new List<Author>()
        {
            new Author()
            {
                Id = new Guid("dd9961d2-cab0-4bf5-9a9d-48862a64d63b"),
                Name = "Author Name",
                Bio = "Author Bio"
            },
        };

        public BookServiceTest()
        {
            _bookRepository = new Mock<IBookRepository>();
            _authorService = new Mock<IAuthorService>();
        }

        [Fact]
        public async Task Book_GetAll_Count()
        {
            //setup
            var expectedCount = 2;

            _bookRepository.Setup(
                    x => x.GetAll())
                .Returns(async () =>
                    Books.AsEnumerable());
            //inject
            var service = new BookService(
                _bookRepository.Object, _authorService.Object);

            //Act
            var result = await service.GetAll();

            //Assert
            var books = result.ToList();
            Assert.NotNull(books);
            Assert.Equal(expectedCount, books.Count());
            Assert.Equal(Books, books);
        }


        [Fact]
        public async Task Book_GetById_Ok()
        {
            //setup
            var bookId = new Guid("0c18f6ae-a16c-477e-b267-9184c4819742");
            var expectedBook =
                Books.First(x => x.Id == bookId);
            var expectedTitle = $"!{expectedBook.Title}";

            _bookRepository.Setup(
                    x => x.GetById(bookId))
                .Returns(async () =>
                    Books.FirstOrDefault(x => x.Id == bookId));
            //inject
            var service = new BookService(_bookRepository.Object, _authorService.Object);

            //Act
            var result = await service.GetById(bookId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBook, result);
            Assert.Equal(expectedTitle, result?.Title);
        }

        [Fact]
        public async Task Book_GetById_Not_Found()
        {
            //setup
            var bookId = new Guid("1c18f6ae-a16c-477e-b267-9184c4819742");


            _bookRepository.Setup(
                    x => x.GetById(bookId))
                .Returns(async () =>
                    Books.FirstOrDefault(x => x.Id == bookId));
            //inject
            var service = new BookService(_bookRepository.Object, _authorService.Object);

            //Act
            var result = await service.GetById(bookId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Book_Add_Ok()
        {
            //setup
            var bookToAdd = new Book()
            {
                Id = new Guid("1c18f6ae-a16c-477e-b267-9184c4819742"),
                Title = "New Title",
                AuthorId = new Guid("dd9961d2-cab0-4bf5-9a9d-48862a64d63b"),
            };
            var expectedBookCount = 3;

            _authorService.Setup(a =>
                a.GetById(bookToAdd.AuthorId)).Returns(() =>Task.FromResult(Authors.FirstOrDefault()));

            _bookRepository.Setup(
                    x =>
                        x.GetAllByAuthorId(bookToAdd.AuthorId))
                    .Returns(() =>
                        Task.FromResult(Books.Where(x => x.AuthorId == bookToAdd.AuthorId)));
            
            _bookRepository.Setup(x =>
                x.Add(It.IsAny<Book>()))
                .Callback(() =>
            {
                Books.Add(bookToAdd);
            }).Returns(Task.CompletedTask);

            //inject
            var service = new BookService(_bookRepository.Object, _authorService.Object);
            
            //Act
            await service.Add(bookToAdd);

            //Assert
            Assert.Equal(expectedBookCount, Books.Count);
            Assert.Equal(bookToAdd, Books.LastOrDefault());
        }

        [Fact]
        public async Task Book_Add_Author_Not_Found()
        {
            //setup
            var bookToAdd = new Book()
            {
                Id = new Guid("1c18f6ae-a16c-477e-b267-9184c4819742"),
                Title = "New Title",
                AuthorId = new Guid("dd9961d2-cab0-4bf5-9a9d-48862a64d63a"),
            };
            var expectedBookCount = 2;

            _authorService.Setup(a =>
                a.GetById(bookToAdd.AuthorId)).Returns(() =>
                Task.FromResult(Authors.FirstOrDefault(x => x.Id == bookToAdd.AuthorId)));

            _bookRepository.Setup(
                    x =>
                        x.GetAllByAuthorId(bookToAdd.AuthorId))
                .Returns(() =>
                    Task.FromResult(Books.Where(x => x.AuthorId == bookToAdd.AuthorId)));

            _bookRepository.Setup(x =>
                x.Add(It.IsAny<Book>()));

            //inject
            var service = new BookService(_bookRepository.Object, _authorService.Object);

            //Act
            var result = await service.Add(bookToAdd);

            //Assert
            Assert.Equal(expectedBookCount, Books.Count);
            Assert.Null(result);
        }
    }
}