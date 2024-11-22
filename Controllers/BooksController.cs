using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bruj_Tudor_Lab2_EB.Data;
using Bruj_Tudor_Lab2_EB.Models;

namespace Bruj_Tudor_Lab2_EB.Controllers
{
    public class BooksController : Controller
    {
        private readonly Bruj_Tudor_Lab2_EBContext _context;

        public BooksController(Bruj_Tudor_Lab2_EBContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var booksContext = _context.Book.Include(b => b.Genre).Include(b => b.Author);
            return View(await booksContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            PopulateAuthorsDropdown();
            ViewData["GenreID"] = new SelectList(_context.Set<Genre>(), "ID", "Name");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Price,GenreID,AuthorID")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAuthorsDropdown(book.AuthorID);
            ViewData["GenreID"] = new SelectList(_context.Set<Genre>(), "ID", "Name", book.GenreID);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            PopulateAuthorsDropdown(book.AuthorID);
            ViewData["GenreID"] = new SelectList(_context.Set<Genre>(), "ID", "Name", book.GenreID);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Price,GenreID,AuthorID")] Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateAuthorsDropdown(book.AuthorID);
            ViewData["GenreID"] = new SelectList(_context.Set<Genre>(), "ID", "Name", book.GenreID);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'Bruj_Tudor_Lab2_EBContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return (_context.Book?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        // Helper method to populate authors dropdown
        private void PopulateAuthorsDropdown(object selectedAuthor = null)
        {
            var authorsQuery = from a in _context.Authors
                               orderby a.LastName, a.FirstName
                               select new
                               {
                                   ID = a.ID,
                                   FullName = a.FirstName + " " + a.LastName
                               };
            ViewData["AuthorID"] = new SelectList(authorsQuery.AsNoTracking(), "ID", "FullName", selectedAuthor);
        }
    }
}

