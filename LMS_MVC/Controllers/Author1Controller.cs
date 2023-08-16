using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS_MVC.Data;
using LMS_MVC.Models;
using LMS_MVC.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography;

namespace LMS_MVC.Controllers
{
    public class Author1Controller : Controller
    {
        private readonly LMS_MVCContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment environment;

        public Author1Controller(LMS_MVCContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }

        // GET: Author1
        public async Task<IActionResult> Index()
        {
              return _context.Author1 != null ? 
                          View(await _context.Author1.OrderBy(x=>x.AuthorName).ToListAsync()) :
                          Problem("Entity set 'LMS_MVCContext.Author1'  is null.");
        }

        // GET: Author1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Author1 == null)
            {
                return NotFound();
            }

            var author1 = await _context.Author1
                .FirstOrDefaultAsync(m => m.ID == id);
            if (author1 == null)
            {
                return NotFound();
            }

            return View(author1);
        }

        // GET: Author1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AuthorName,Address,PhoneNumber,Email,ImagePath")] ImageCreateModel author1)
        {


            /* // Calculate the hash of the uploaded image content
             string imageHash = ComputeImageHash(author1.ImagePath);
            */
            // Check if an image with the same hash exists in the database
            var is_already_present = _context.Author1.Any(a => a.AuthorName == author1.AuthorName);



            if (!is_already_present)
            {

                if (ModelState.IsValid)
                {
                    var path = environment.WebRootPath;
                    var filePath = "Content/Image/" + author1.ImagePath.FileName;
                    var fullPath = Path.Combine(path, filePath);
                    uploadFile(author1.ImagePath, fullPath);

                    var data = new Author1()
                    {
                        AuthorName = author1.AuthorName,
                        Address = author1.Address,
                        PhoneNumber = author1.PhoneNumber,
                        Email = author1.Email,
                        
                        ImagePath = fullPath
                    };

                    _context.Add(data);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
               
            }
            else
            {
                string errormsg = "Author name already exist";

                ViewBag.error = true;
                bool iserror = true;

                ViewBag.ErrorMessage = errormsg;
                return View();
            }
            return View(author1);
        }

        private string ComputeImageHash(IFormFile imagePath)
        {
            using (var stream = imagePath.OpenReadStream())
            {
                using (var md5 = MD5.Create())
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
        }

        public void uploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);

        }


        // GET: Author1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Author1 == null)
            {
                return NotFound();
            }

            var author1 = await _context.Author1.FindAsync(id);
            if (author1 == null)
            {
                return NotFound();
            }
            return View(author1);
        }

        // POST: Author1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AuthorName,Address,PhoneNumber,Email,ImagePath")] Author1 author1)
        {
            if (id != author1.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Author1Exists(author1.ID))
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
            return View(author1);
        }

        // GET: Author1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Author1 == null)
            {
                return NotFound();
            }

            var author1 = await _context.Author1
                .FirstOrDefaultAsync(m => m.ID == id);
            if (author1 == null)
            {
                return NotFound();
            }

            return View(author1);
        }

        // POST: Author1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Author1 == null)
            {
                return Problem("Entity set 'LMS_MVCContext.Author1'  is null.");
            }
            var author1 = await _context.Author1.FindAsync(id);
            if (author1 != null)
            {
                _context.Author1.Remove(author1);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Author1Exists(int id)
        {
          return (_context.Author1?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
