using BookManagement.Models;
using BookManagement.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BookManagement.Services
{
    public class BookService : IBookService
    {
        private readonly API_DB_Context context;

        public BookService(API_DB_Context dbContext)
        {
            context = dbContext;
        }

        public void AddBook(CreateUpdateBookDto bookDto)
        {
            if (context.Books.Any(b => b.Title == bookDto.Title))
                return;
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Year = bookDto.Year,
                ViewCount = 0,
                IsDeleted = false
            };

            context.Books.Add(book);
            context.SaveChanges();
        }

        public void AddBooks(List<CreateUpdateBookDto> bookDtos)
        {
            var existingBooks = context.Books.Select(b => b.Title).ToHashSet();
            var newBooks = bookDtos
            .Where(dto => !existingBooks.Contains(dto.Title))
            .Select(b => new Book
            {
                Title = b.Title,
                Author = b.Author,
                Year = b.Year,
                ViewCount = 0,
                IsDeleted = false
            });

            if (newBooks.Any()) 
            { 
                context.Books.AddRange(newBooks);
                context.SaveChanges();
            }
        }

        public void UpdateBook(int id, Book updatedBook)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.Year = updatedBook.Year;
                book.ViewCount = updatedBook.ViewCount;
            }
            context.SaveChanges();
        }

        public void SoftDeleteBook(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.IsDeleted = true;
            }
            context.SaveChanges();
        }

        public void SoftDeleteBooks(List<int> ids)
        {
            var booksToDelete = context.Books.Where(b => ids.Contains(b.Id)).ToList();
            booksToDelete.ForEach(b => b.IsDeleted = true);
            context.SaveChanges();
        }

        public List<string> GetBooksByPopularity(int page, int pageSize)
        {
            return context.Books
                .OrderByDescending(b => b.ViewCount)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => b.Title)
                .ToList();
        }

        public Book GetBookDetails(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            
            ++book.ViewCount;
            context.SaveChanges();

            return book;
        }
    }
}
