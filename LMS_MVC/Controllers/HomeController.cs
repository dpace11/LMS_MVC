using LMS_MVC.Data;
using LMS_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LMS_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LMS_MVCContext _context;

        public HomeController(ILogger<HomeController> logger, LMS_MVCContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var totalAuthors = _context.Author1.Count();
            var totalafffiliaction = _context.Student.Count();
            var totalPublication = _context.Publications.Count();
          
            var totalmembers = _context.Membership.Count(); ;
            var totalbooks = _context.Book.Count();
            var totalbookissued = _context.BookIssue.Count();


            int mostIssuedBookId = _context.BookIssue.GroupBy(issue => issue.BookID)
                .OrderByDescending(group => group.Count()) 
                .Select(group => group.Key) 
                .FirstOrDefault();

           var mostIssuedBookName=_context.Book.Where(c=>c.BookId== mostIssuedBookId).Select(c=>c.BookName).FirstOrDefault();

            var mostFamousAuthor = _context.Book.Where(b => b.BookId == mostIssuedBookId).Select(name => name.AuthorName).FirstOrDefault();

            ViewBag.totalAuthors = totalAuthors;
            ViewBag.totalafffiliaction = totalafffiliaction;
            ViewBag.totalPublication = totalPublication;
            ViewBag.totalmembers = totalmembers;
            ViewBag.totalbooks = totalbooks;
            ViewBag.totalbookissued = totalbookissued;
            ViewBag.mostreadbook = mostIssuedBookName;
            ViewBag.mostFamousAuthor = mostFamousAuthor;
          



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}