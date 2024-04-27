using Library.Data;
using Library.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly AppDbContext _context;

        public AdministrationController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult WichCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> WichCustomer(Customer model)
        {
            var customers = _context.Customers
               .Where(c => c.Firstname.Contains(model.Firstname) || c.Lastname.Contains(model.Lastname)).ToList();
            if (customers == null)
            {
                return NotFound();
            }

            return View("WichCustomerResult", customers);
        }


        public async Task<IActionResult> ReturnBook(int id)
        {
            var books = await (from b in _context.Borrows
                               join book in _context.Books on b.BookId equals book.Id
                               where b.CustomerId == id && !b.IsReturned
                               select new SelectListItem
                               {
                                   Value = book.Id.ToString(),
                                   Text = book.Title
                               }).ToListAsync();

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                return NotFound();
            }

            var model = new Borrow
            {
                CustomerId = customer.Id
            };
            ViewBag.Customer = customer;
            ViewBag.Books = new SelectList(books, "Value", "Text");

            return View(model);
        }
        [HttpPost]
        public IActionResult ReturnBook([Bind("BookId,CustomerId")] Borrow borrow)
        {
            var bookLoan = _context.Borrows.FirstOrDefault(b => b.BookId == borrow.BookId && b.CustomerId == borrow.CustomerId);
            if (bookLoan == null)
            {
                return NotFound();
            }
            bookLoan.IsReturned = true;
            bookLoan.Returned = DateTime.Now;
            var borrowedBook = _context.Books.Find(bookLoan.BookId);

            borrowedBook!.IsAvailable = true;

            _context.Update(borrowedBook);
            _context.SaveChanges();
            TempData["success"] = "The Book has been returned";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult TopList()
        {
            var books = _context.Books.OrderByDescending(b => b.BorrowedCount).Take(3).ToList();


            return View(books);
        }

        public IActionResult FindCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FindCustomer(Customer customer)
        {
            var customers = _context.Customers
                .Where(c => c.Firstname.Contains(customer.Firstname) || c.Lastname.Contains(customer.Lastname)).ToList();

            if (!customers.Any())
            {
                return NotFound();
            }


            return View("ResultList", customers);
        }

        //Get all borrows for 1 customer
        public async Task<IActionResult> Details(int id)
        {
            var customerName = _context.Customers.Where(c => c.Id == id).Select(c => c.Fullname).FirstOrDefault();
            ViewBag.customer = customerName;

            var books = await (from b in _context.Borrows
                               join bo in _context.Books on b.BookId equals bo.Id
                               where b.CustomerId == id
                               select new { Book = bo, Borrowed = b }).ToListAsync();

            var model = books.Select(x => new BorrowBooksVM
            {
                Book = x.Book,
                Borrow = x.Borrowed

            }).ToList();

            return View(model);
        }
    }
}
