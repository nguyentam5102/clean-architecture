using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagement.Domain.DTOs;
using BookManagement.Domain.Entities;

namespace BookManagement.Domain.Interface
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllAsync();

        Task<BookDTO> AddAsync(BookDTO dto);

        Task<BookDTO> GetBookAsycn(Guid id);

        Task<BookDTO> UpdateAsycn(BookDTO dto);

        Task<bool> DeleteAsycn(Guid id);
    }
}
