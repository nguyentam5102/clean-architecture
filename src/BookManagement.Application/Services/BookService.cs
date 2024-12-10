using AutoMapper;
using BookManagement.Domain.DTOs;
using BookManagement.Domain.Entities;
using BookManagement.Domain.Interface;
using BookManagement.Domain.Interface.Services;
using BookManagement.Infrastructure;

namespace BookManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly BookDbContext _dbContext;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _logger;

        public BookService(BookDbContext bookDbContext, IBookRepository bookRepository, IMapper mapper, ILogService logger) 
        {
            _dbContext = bookDbContext;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BookDTO> AddAsync(BookDTO dto)
        {
            dto.Id = Guid.NewGuid();
            var bookToAdd = _mapper.Map<Book>(dto);
            await _bookRepository.AddAsync(bookToAdd);
            _dbContext.SaveChanges();
            return dto;
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            _logger.LogInfo("Read all books");
            return books.Select(_mapper.Map<BookDTO>);
        }

        public async Task<BookDTO> GetBookAsycn(Guid id)
        {
            var book = await _bookRepository.GetBookAsycn(id);
            if (book == null)
            {
                _logger.LogWarning($"Book wtih ID {id.ToString()} not found");
                return null;
            }
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO> UpdateAsycn(BookDTO dto)
        {
            var book = await _bookRepository.GetBookAsycn(dto.Id);

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Price = dto.Price;

            await _bookRepository.UpdateAsycn(book);
            _dbContext.SaveChanges();
            return dto;
        }

        public async Task<bool> DeleteAsycn(Guid id)
        {
            var book = await GetBookAsycn(id);

            if (book == null)
            {
                _logger.LogError($"Book wtih ID {id.ToString()} not found");
                return false;
            }

            await _bookRepository.DeleteAsycn(id);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
