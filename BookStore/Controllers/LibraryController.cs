using BookStore.BL.Interfaces;
using BookStore.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet("GetAllBooksByAuthor")]
        public GetAllBooksByAuthorResponse
            GetAllBooksByAuthor(int authorId)
        {
            return _libraryService.GetAllBooksByAuthorId(authorId);
        }
    }
}
