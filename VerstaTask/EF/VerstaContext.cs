namespace VerstaTask.EF
{
    using Microsoft.EntityFrameworkCore;
    using VerstaTask.Entities;
    using VerstaTask.Models;

    public class VerstaContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public VerstaContext(DbContextOptions<VerstaContext> options) : base(options)
        {

        }
    }
}
