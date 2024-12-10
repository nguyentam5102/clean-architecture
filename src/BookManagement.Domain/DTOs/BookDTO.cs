
namespace BookManagement.Domain.DTOs
{
    public class BookDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public double Price { get; set; }
    }
}
