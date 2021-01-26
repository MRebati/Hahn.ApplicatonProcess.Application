using System.Text;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.December2020.Data.Infrastructure
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext() : base(new DbContextOptions<ApplicationDbContext>())
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
    }
}
