using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS_MVC.Data;
using LMS_MVC.Models;

namespace LMS_MVC.Controllers
{
    public class BookIssueReturnsController : Controller
    {
       
        private readonly LMS_MVCContext _context;

        public BookIssueReturnsController(LMS_MVCContext context)
        {
            _context = context;
            BookIssueReturn bookIssueReturn = new();
            Student student = new();
           
        }

        public async Task<IActionResult> Index()
        {
            var lMS_MVCContext = _context.BookIssue.Include(b => b.Book).Where(b=>b.BookReturnDate==null);
            return View(await lMS_MVCContext.ToListAsync());
        }

        // GET: BookIssueReturns
        public async Task<IActionResult> History()
        {
            var lMS_MVCContext = _context.BookIssue.Include(b => b.Book).Where(b => b.BookReturnDate != null);
            return View(await lMS_MVCContext.ToListAsync());
        }

        // GET: BookIssueReturns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Subject = _context.BookIssue.Where(b => b.Id == id).Select(b => b.Name).FirstOrDefault();
            if (id == null || _context.BookIssue == null)
            {
                return NotFound();
            }

            var bookIssueReturn = await _context.BookIssue
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookIssueReturn == null)
            {
                return NotFound();
            }

            return View(bookIssueReturn);
        }

        // GET: BookIssueReturns/Create
        public IActionResult Create()
        {
            ViewData["BookID"] = new SelectList(_context.Book, "BookId", "BookName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RollNo,Name,Departmet,BookIssueDate,BookID")] BookIssueReturn bookIssueReturn)
        {
            string errormsg;

            var duplicate_book = _context.BookIssue.Where(b => b.RollNo == bookIssueReturn.RollNo && b.BookID == bookIssueReturn.BookID && b.BookReturnDate == null).FirstOrDefault();
            

            int remaining_book_qty = _context.Book.Where(b => b.BookId == bookIssueReturn.BookID).Select(b => b.RemainingQuantity).FirstOrDefault();

            int student_book_count = _context.BookIssue.Where(b => b.RollNo == bookIssueReturn.RollNo && b.BookReturnDate==null).Count();
            

            var rollnoexist = _context.Student.Any(m => m.StudentRollNo == bookIssueReturn.RollNo);

            var roll_name_dept_match = _context
                                            .Student
                                            .Where(s => s.StudentRollNo == bookIssueReturn.RollNo && s.StudentName == bookIssueReturn.Name && s.Department == bookIssueReturn.Departmet).FirstOrDefault();

            var membership_exist = _context.Membership.Where(m => m.StudentRollNo == bookIssueReturn.RollNo).FirstOrDefault();

            if (rollnoexist)
            {
                if (membership_exist != null)
                {
                    if (roll_name_dept_match != null)
                    {
                        if (remaining_book_qty > 1)
                        {
                            if (student_book_count <= 4)
                            {

                                if (duplicate_book == null)
                                {
                                    remaining_book_qty = remaining_book_qty - 1;

                                    var update_book_Qty = _context.Book.Where(b => b.BookId == bookIssueReturn.BookID);
                                    foreach (var r in update_book_Qty)
                                    {
                                        r.RemainingQuantity = remaining_book_qty;
                                    }

                                    if (ModelState.IsValid)
                                    {
                                        bookIssueReturn.ActualReturnDate = bookIssueReturn.BookIssueDate.AddDays(14);

                                        _context.Add(bookIssueReturn);
                                        await _context.SaveChangesAsync();
                                        return RedirectToAction(nameof(Index));
                                    }
                                    ViewData["BookID"] = new SelectList(_context.Book, "BookId", "BookName", bookIssueReturn.BookID);
                                    return View(bookIssueReturn);
                                }
                                else
                                {
                                    errormsg = "this book is aleady issued to " + bookIssueReturn.RollNo;

                                    ViewBag.error = true;
                                    bool iserror = true;

                                    ViewBag.ErrorMessage = errormsg;
                                    return View();
                                }
                            }
                            else
                            {

                                errormsg = "Exceeded the limit of 5 books per student";

                                ViewBag.error = true;
                                bool iserror = true;

                                ViewBag.ErrorMessage = errormsg;
                                return View();
                            }
                        }
                        else
                        {
                            errormsg = "Book Out Of Stock.Only Availabe For Reading in Library";

                            ViewBag.error = true;
                            bool iserror = true;

                            ViewBag.ErrorMessage = errormsg;
                            return View();
                        }
                    }
                    else
                    {
                        errormsg = "Roll No, Name, Department Missmatch. Enter correct details";

                        ViewBag.error = true;
                        bool iserror = true;

                        ViewBag.ErrorMessage = errormsg;
                        return View();
                    }
                }
                else
                {
                    errormsg = bookIssueReturn.RollNo+" is not a member of library. Access membership first to issue book";

                    ViewBag.error = true;
                    bool iserror = true;

                    ViewBag.ErrorMessage = errormsg;
                    return View();
                }
            }
            else
            {
                errormsg = "Roll no doesnot exist . Register the student first";

                ViewBag.error = true;
                bool iserror = true;

                ViewBag.ErrorMessage = errormsg;
                return View();
            }
        }

        // GET: BookIssueReturns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookIssue == null)
            {
                return NotFound();
            }

            var bookIssueReturn = await _context.BookIssue.FindAsync(id);
            if (bookIssueReturn == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Book, "BookId", "BookName", bookIssueReturn.BookID);
            return View(bookIssueReturn);
        }

        // POST: BookIssueReturns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RollNo,Name,Departmet,BookIssueDate,BookReturnDate,BookID")] BookIssueReturn bookIssueReturn)
        {
            int remqty = _context.Book.Where(b => b.BookId == bookIssueReturn.BookID).Select(b => b.RemainingQuantity).FirstOrDefault();

         

            //int timeDifference=Convert.ToInt32( bookIssueReturn.ActualReturnDate-bookIssueReturn.BookReturnDate);

            if (id != bookIssueReturn.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               /* if (timeDifference>14)
                {
                    string errormsg = "Exceeded 14 days";

                    ViewBag.error = true;
                    bool iserror = true;

                    ViewBag.ErrorMessage = errormsg;
                    return View();
                }*/
                try
                {
                    bookIssueReturn.BookReturnDate = DateTime.Now;//////////+++++++++
                    remqty = remqty + 1;

                    var update_book_Qty = _context.Book.Where(b => b.BookId == bookIssueReturn.BookID);
                    foreach (var r in update_book_Qty)
                    {
                        r.RemainingQuantity = remqty;
                    }

                    _context.Update(bookIssueReturn);
                    await _context.SaveChangesAsync();


                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookIssueReturnExists(bookIssueReturn.Id))
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
            ViewData["BookID"] = new SelectList(_context.Book, "BookId", "AuthorName", bookIssueReturn.BookID);
            return View(bookIssueReturn);
        }

        // GET: BookIssueReturns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookIssue == null)
            {
                return NotFound();
            }

            var bookIssueReturn = await _context.BookIssue
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookIssueReturn == null)
            {
                return NotFound();
            }

            return View(bookIssueReturn);
        }

        // POST: BookIssueReturns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookIssue == null)
            {
                return Problem("Entity set 'LMS_MVCContext.BookIssue'  is null.");
            }
            var bookIssueReturn = await _context.BookIssue.FindAsync(id);
            if (bookIssueReturn != null)
            {
                _context.BookIssue.Remove(bookIssueReturn);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookIssueReturnExists(int id)
        {
          return (_context.BookIssue?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
