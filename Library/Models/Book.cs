using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Author { get; set; } = null!;
        public bool IsAvailable { get; set; } = true;
        public int BorrowedCount { get; set; }

        public ICollection<Borrow>? Borrows { get; set; }

    }
}
