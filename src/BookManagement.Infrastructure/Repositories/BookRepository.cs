using BookManagement.Domain.Entities;
using BookManagement.Domain.Interface;

namespace BookManagement.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _dbContext;

        public BookRepository(BookDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return _dbContext.Books;
        }

        public async Task<Book> AddAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            return book;
        }

        public async Task<Book> GetBookAsycn(Guid id)
        {
            return await _dbContext.Books.FindAsync(id);
        }

        public async Task<Book> UpdateAsycn(Book book)
        {
            _dbContext.Attach(book);
            var entry = _dbContext.Entry(book);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return book;
        }

        public async Task DeleteAsycn(Guid id)
        {
            var book = await GetBookAsycn(id);
            _dbContext.Books.Remove(book);
        }
    }
}
