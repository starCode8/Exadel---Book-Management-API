using BookManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement
{
    public class API_DB_Context : DbContext
    {
        public API_DB_Context(DbContextOptions<API_DB_Context> options) : base(options) {}
        public DbSet<Book> Books { get; set; }
    }    
}

