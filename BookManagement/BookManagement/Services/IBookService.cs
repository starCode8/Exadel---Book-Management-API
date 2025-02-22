using BookManagement.Models;
using BookManagement.Models.DTOs;

namespace BookManagement.Services
{
    public interface IBookService
    {
        void AddBook(CreateUpdateBookDto bookDto);
        void AddBooks(List<CreateUpdateBookDto> bookDtos);
        void UpdateBook(int id, Book updatedBook);
        void SoftDeleteBook(int id);
        void SoftDeleteBooks(List<int> ids);
        List<string> GetBooksByPopularity(int page, int pageSize);
        Book GetBookDetails(int id);
    }
}
