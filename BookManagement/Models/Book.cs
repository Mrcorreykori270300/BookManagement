using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class Book
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Display(Name = "List Price")]
        [Required, Range(1, 100)]
        public double ListPrice { get; set; }
    }
}
