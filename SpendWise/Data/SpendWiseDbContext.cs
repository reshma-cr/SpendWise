using Microsoft.EntityFrameworkCore;
using SpendWise.Models;

namespace SpendWise.Data
{
    public class SpendWiseDbContext : DbContext
    {
        public SpendWiseDbContext(DbContextOptions<SpendWiseDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
