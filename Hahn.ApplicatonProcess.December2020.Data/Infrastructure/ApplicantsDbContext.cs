using Hahn.ApplicatonProcess.December2020.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.December2020.Data.Infrastructure
{
    public class ApplicantsDbContext : DbContext
    {
        public ApplicantsDbContext(DbContextOptions<ApplicantsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
    }
}