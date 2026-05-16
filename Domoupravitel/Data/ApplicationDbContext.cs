namespace Domoupravitel.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>(options)
    {
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<Budget> Budgets { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Transaction>().Property(p => p.Amount).HasPrecision(18, 2);
            builder.Entity<Transaction>().Property(p => p.Description).HasMaxLength(2000);
            builder.Entity<Budget>().Property(p => p.Amount).HasPrecision(18, 2);
            builder.Entity<Document>().Property(p => p.FileName).HasMaxLength(200);

            builder.Entity<Transaction>()
                .HasOne(t => t.Budget)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.BudgetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
