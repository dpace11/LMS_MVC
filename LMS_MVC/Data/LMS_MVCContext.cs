using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS_MVC.Models;

namespace LMS_MVC.Data
{
    public class LMS_MVCContext : DbContext
    {
        public LMS_MVCContext (DbContextOptions<LMS_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<LMS_MVC.Models.Student> Student { get; set; } = default!;

        public DbSet<LMS_MVC.Models.Book> Book { get; set; } = default!;

        public DbSet<LMS_MVC.Models.BookIssueReturn> BookIssue { get; set; } = default!;
    }
}
