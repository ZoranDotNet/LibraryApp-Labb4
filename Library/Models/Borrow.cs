using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Borrow
    {
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book? Book { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public DateTime Borrowed { get; set; } = DateTime.Now;
        public DateTime Returned { get; set; }

        public bool IsReturned { get; set; } = false;
        public Borrow()
        {
            Returned = Borrowed.AddDays(14);
        }
    }
}
