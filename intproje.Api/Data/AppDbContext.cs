using Microsoft.EntityFrameworkCore;
using intproje.Api.Models;

namespace intproje.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
    }
}
