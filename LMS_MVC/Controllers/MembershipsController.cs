using LMS_MVC.Data;
using LMS_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace LMS_MVC.Controllers
{
    public class MembershipsController : Controller
    {
        private readonly LMS_MVCContext _context;

        public MembershipsController(LMS_MVCContext context)
        {
            _context = context;
        }

        // GET: Memberships
        public async Task<IActionResult> Index()
        {
            return _context.Membership != null ?
                        View(await _context.Membership.ToListAsync()) :
                        Problem("Entity set 'LMS_MVCContext.Membership'  is null.");
        }

        // GET: Memberships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        // GET: Memberships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Memberships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentRollNo,FullName,MembershipIssueDate,MembershipEndDate")] Membership membership)
        {
            //string fullname = membership.FullName;
            bool roll = _context.Student.Any(c => c.StudentRollNo == membership.StudentRollNo);

            bool member_already_exist = _context.Membership.Any(m => m.StudentRollNo == membership.StudentRollNo);

            var roll_name_match = _context.Student.Where(s => s.StudentRollNo == membership.StudentRollNo && s.StudentName == membership.FullName).FirstOrDefault();

            if (member_already_exist != true)
            {
                if (roll)
                {
                    if (roll_name_match != null)
                    {
                        if (ModelState.IsValid)
                        {
                            membership.MembershipEndDate = membership.MembershipIssueDate.AddMonths(3);

                            _context.Add(membership);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        return View(membership);
                    }
                    else
                    {
                        string errormsg = "Rollno Name mis-match. Enter correct detail.";

                        ViewBag.error = true;
                        bool iserror = true;

                        ViewBag.ErrorMessage = errormsg;
                        return View();

                    }
                }
                else
                {
                    string errormsg = membership.StudentRollNo + " not in database. Enter into database first.";

                    ViewBag.error = true;
                    bool iserror = true;

                    ViewBag.ErrorMessage = errormsg;
                    return View();
                }
            }
            else
            {
                string errormsg = membership.StudentRollNo + " is already a member of library.";

                ViewBag.error = true;
                bool iserror = true;

                ViewBag.ErrorMessage = errormsg;
                return View();

            }
        }

        // GET: Memberships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership.FindAsync(id);
            if (membership == null)
            {
                return NotFound();
            }
            return View(membership);
        }

        // POST: Memberships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentRollNo,FullName,MembershipIssueDate,MembershipEndDate")] Membership membership)
        {
            if (id != membership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    membership.MembershipEndDate = membership.MembershipIssueDate.AddMonths(3);

                    _context.Update(membership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipExists(membership.Id))
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
            return View(membership);
        }

        // GET: Memberships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        // POST: Memberships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Membership == null)
            {
                return Problem("Entity set 'LMS_MVCContext.Membership'  is null.");
            }
            var membership = await _context.Membership.FindAsync(id);
            if (membership != null)
            {
                _context.Membership.Remove(membership);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipExists(int id)
        {
            return (_context.Membership?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Memberships/Edit/5
        public async Task<IActionResult> Renew(int? id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership.FindAsync(id);
            if (membership == null)
            {
                return NotFound();
            }
            return View(membership);
        }

        // POST: Memberships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Renew(int id, [Bind("Id,StudentRollNo,FullName,MembershipIssueDate,MembershipEndDate")] Membership membership)
        {
           

            if (id != membership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //membership.MembershipIssueDate = DateTime.Now;
                    membership.MembershipEndDate = membership.MembershipIssueDate.AddMonths(3);

                    _context.Update(membership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipExists(membership.Id))
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
            return View(membership);
        }
    }
}
