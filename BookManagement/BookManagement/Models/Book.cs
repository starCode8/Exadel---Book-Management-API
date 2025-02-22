using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int ViewCount { get; set; }
        public bool IsDeleted { get; set; } = false;
       
    }
}
