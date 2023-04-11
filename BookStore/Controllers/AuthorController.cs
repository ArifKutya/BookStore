using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("GetAllAuthors")]
        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _authorService.GetAll();
        }

        [HttpGet("GetById")]
        public async Task<Author> GetById(Guid id)
        {
            return await _authorService.GetById(id);
        }

        [HttpPost("Add")]
        public async Task Add([FromBody] AddAuthorRequest authorRequest)
        {
            await _authorService.Add(authorRequest);
        }

        [HttpPost("Update")]
        public async Task Update([FromBody] UpdateAuthorRequest author)
        {
            await _authorService.Update(author);
        }

        [HttpDelete("Delete")]
        public void Delete(Guid id)
        {
            _authorService.Delete(id);
        }
    }
}