﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS_MVC.Data;
using LMS_MVC.Models;
using Microsoft.Identity.Client;

namespace LMS_MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly LMS_MVCContext _context;

        public BooksController(LMS_MVCContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
              return _context.Book != null ? 
                          View(await _context.Book.OrderBy(b=>b.BookName).ToListAsync()) :
                          Problem("Entity set 'LMS_MVCContext.Book'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,BookName,AuthorName,PublicationName,Price,PurchaseDate,Quantity,BookLocation,RemainingQuantity")] Book book)
        {
            var is_author_present = _context.Author1.Any(b => b.AuthorName == book.AuthorName);

            var is_book_already_present = _context.Book.Any(b => b.BookName == book.BookName);

            var is_pub_present = _context.Publications.Any(p => p.PublicationName == book.PublicationName);

            if (!is_book_already_present)
            {

                if (is_author_present)
                {
                    if (is_pub_present)
                    {
                        if (ModelState.IsValid)
                        {
                            _context.Add(book);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                       
                        ViewBag.errorpub ="abc";

                        string errormsg = "Publication name doesnot exist in database. ";
                        ViewBag.ErrorMessagePub = errormsg;
                        return View();
                    }
                }
                else
                {
                    ViewBag.errorauth = "xyz";
                    string errormsg = "Author name doesnot exist in database. ";
                    ViewBag.ErrorMessageAuth = errormsg;
                    return View();
                }
            }
            else
            {
                string errormsg = "Book name conflict. Already exist in database ";
                ViewBag.ErrorMessage = errormsg;
                return View();
            }
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
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,BookName,AuthorName,PublicationName,Price,PurchaseDate,Quantity,BookLocation,RemainingQuantity")] Book book)
        {
            if (id != book.BookId)
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
                    if (!BookExists(book.BookId))
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
                .FirstOrDefaultAsync(m => m.BookId == id);
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
                return Problem("Entity set 'LMS_MVCContext.Book'  is null.");
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
          return (_context.Book?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
