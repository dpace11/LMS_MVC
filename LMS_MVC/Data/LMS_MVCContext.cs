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

        public DbSet<LMS_MVC.Models.Membership > Membership { get; set; } = default!;

        public DbSet<LMS_MVC.Models.Author> Authors { get; set; } = default!;

        public DbSet<LMS_MVC.Models.Publication> Publications { get; set; } = default!;

        public DbSet<Author1> Author1 { get; set; } = default!;     



    }
}
