using BookManagement.Domain.Entities;

namespace BookManagement.Domain.Interface
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();

        Task<Book> AddAsync(Book book);

        Task<Book> GetBookAsycn(Guid id);

        Task<Book> UpdateAsycn(Book book);
        
        Task DeleteAsycn(Guid id);
    }
}
