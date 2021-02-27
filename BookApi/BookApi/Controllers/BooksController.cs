using BookLibrary.Models;
using BookLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        readonly ILogger<BooksController> _logger;
        IBookRepository _bookRepository;

        public BooksController(ILogger<BooksController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBooks(int afterId = 0)
        {
            return Ok(await _bookRepository.GetAllBooksAsync(afterId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            return Ok(await _bookRepository.GetBookByIdAsync(id));
        }

        [HttpGet("byName/{name}")]
        public async Task<IActionResult> GetBooksByName(string name, int afterId = 0)
        {
            return Ok(await _bookRepository.GetBooksByNameAsync(name, afterId));
        }

        [HttpGet("byTheme/{theme}")]
        public async Task<IActionResult> GetBooksByTheme(string theme, int afterId = 0)
        {
            return Ok(await _bookRepository.GetBooksByThemeAsync(theme, afterId));
        }

        [HttpGet("byDate")]
        public async Task<IActionResult> GetBooksByDate(DateTime date)
        {
            return Ok(await _bookRepository.GetBooksByDateAsync(date));
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] Book book)
        {
            _bookRepository.CreateBookAsync(book);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
                return BadRequest();
            _bookRepository.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            _bookRepository.DeleteBookAsync(await _bookRepository.GetBookByIdAsync(id));
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteAllBooks(int id)
        {
            _bookRepository.DeleteAllBooksAsync();
            return NoContent();
        }
    }
}