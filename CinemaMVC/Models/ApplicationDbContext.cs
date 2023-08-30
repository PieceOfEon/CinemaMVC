using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CinemaMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Film> Films { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
