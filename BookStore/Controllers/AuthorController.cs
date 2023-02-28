using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorService _authorService;
        private readonly ILifeTimeService _lifeTimeService;

        public AuthorController(
            ILogger<AuthorController> logger,
            IAuthorService authorService, ILifeTimeService lifeTimeService)
        {
            _logger = logger;
            _authorService = authorService;
            _lifeTimeService = lifeTimeService;
        }

        [HttpGet("GetAllAuthors")]
        public IEnumerable<Author> GetAll()
        {
           return _authorService.GetAll();
        }

        [HttpGet("GetId")]
        public Author GetById(int id)
        {
            return _authorService.GetById(id);
        }

        [HttpPost("Add")]
        public void Add([FromBody]Author author)
        {
            _authorService.Add(author);
        }

        [HttpGet("GetGuid")]
        public string GetGuid()
        {
            return _lifeTimeService.GetId();
        }
    }
}