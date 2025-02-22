using BookManagement.Models;
using BookManagement.Models.DTOs;
using BookManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateUpdateBookDto bookDto)
        {
            if (bookDto == null)
                return BadRequest("Book data is required.");

            _bookService.AddBook(bookDto);
            return Ok("Book added successfully.");
        }

        [HttpPost("bulk")]
        public IActionResult AddBooks([FromBody] List<CreateUpdateBookDto> bookDtos)
        {
            if (bookDtos == null || bookDtos.Count == 0)
                return BadRequest("Books list cannot be empty.");

            _bookService.AddBooks(bookDtos);
            return Ok(bookDtos);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null)
                return BadRequest("Updated book data is required.");

            var existingBook = _bookService.GetBookDetails(id);
            if (existingBook == null)
                return NotFound($"Book with ID {id} not found.");

            _bookService.UpdateBook(id, updatedBook);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult SoftDeleteBook(int id)
        {
            var existingBook = _bookService.GetBookDetails(id);
            if (existingBook == null)
                return NotFound($"Book with ID {id} not found.");

            _bookService.SoftDeleteBook(id);
            return NoContent();
        }

        [HttpDelete("bulk")]
        public IActionResult SoftDeleteBooks([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return BadRequest("List of IDs cannot be empty.");

            _bookService.SoftDeleteBooks(ids);
            return NoContent();
        }

        [HttpGet("popularity")]
        public IActionResult GetBooksByPopularity([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest("Page and pageSize must be positive numbers.");

            var books = _bookService.GetBooksByPopularity(page, pageSize);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookDetails(int id)
        {
            var book = _bookService.GetBookDetails(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
    }
}
