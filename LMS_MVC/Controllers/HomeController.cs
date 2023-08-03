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

            /*var mostIssuedBook = _context.BookIssue
                                .GroupBy(x => x.Name)
                                .Select(x => x.Key)
                                .OrderByDescending(x => x.Count())
                                .FirstOrDefault();*/


           // var mostreadbook = _context.BookIssue.Select(x => x.Name).OrderDescending().FirstOrDefault();
            //  var mostFamousAuthor=_context.BookIssue.OrderByDescending(b=>b.Name).FirstOrDefault();

            var popularityQuery = from author in _context.Author1
                                  join book in _context.Book on author.AuthorName equals book.AuthorName into authorBooks
                                  //join publication in _context.Publications on book.PublicationName equals publication.PublicationName into authorPublications
                                  select new
                                  {
                                      AuthorName = author.AuthorName,
                                      //PublicationCount = authorPublications.Count(),
                                      BookCount = authorBooks.Count()
                                  };

            var mostFamousAuthor = popularityQuery.OrderByDescending(a => a.BookCount).FirstOrDefault();

            ViewBag.totalAuthors = totalAuthors;
            ViewBag.totalafffiliaction = totalafffiliaction;
            ViewBag.totalPulications = totalPublication;
            ViewBag.totalmembers = totalmembers;
            ViewBag.totalbooks = totalbooks;
            ViewBag.totalbookissued = totalbookissued;
           // ViewBag.mostreadbook = mostIssuedBook;
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