using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models.DTOs
{
    public class CreateUpdateBookDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Year { get; set; }
    }
}
