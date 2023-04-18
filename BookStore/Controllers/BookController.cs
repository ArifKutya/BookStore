using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
           _bookService = bookService;
        }
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookService.GetAll();

            if (result != null && result.Any()) return Ok(result);

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == null) return BadRequest(id);

            var result = await _bookService.GetById(id);

            if (result != null) return Ok(result);

            return NotFound();
        }

        [HttpPost("Add")]
        public async Task Add([FromBody] Book book)
        {
            await _bookService.Add(book);
        }

        [HttpPost("Update")]
        public async Task Update([FromBody] Book book)
        {
            await _bookService.Update(book);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
           await _bookService.Delete(id);

           return Ok();
        }
    }
}
