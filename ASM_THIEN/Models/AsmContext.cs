using Microsoft.EntityFrameworkCore;

namespace ASM_THIEN.Models
{
    public class AsmContext : DbContext
    {
        public AsmContext(DbContextOptions<AsmContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
