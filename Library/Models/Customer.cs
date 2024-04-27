using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Firstname { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Lastname { get; set; } = null!;

        public string Fullname => $"{Firstname} {Lastname}";

        [Required]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; } = null!;


        public ICollection<Borrow>? Borrows { get; set; }

    }
}
