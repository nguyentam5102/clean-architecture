using System.ComponentModel.DataAnnotations;
using BookManagement.Domain.DTOs;
using BookManagement.Domain.Entities;
using BookManagement.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var allBooks = await _bookService.GetAllAsync();
            return Ok(allBooks);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(Guid id)
        {
            var book = await _bookService.GetBookAsycn(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(Guid id, string title, string author, double price)
        {
            var existingBook = await _bookService.GetBookAsycn(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            var dto = new BookDTO() { Id = existingBook.Id, Author = author, Title = title, Price = price };
            await _bookService.UpdateAsycn(dto);

            return StatusCode(201, dto);
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostBook([Required]string author, [Required]string title, [Range(1, double.MaxValue, ErrorMessage = "failed")]double price)
        {
            var bookDto = new BookDTO() { 
                Price = price, 
                Author = author, 
                Title = title 
            };
            var book = await _bookService.AddAsync(bookDto);
            return Created("", book.Id);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var res = await _bookService.DeleteAsycn(id);
            return res ? NoContent() : NotFound();
        }
    }
}
