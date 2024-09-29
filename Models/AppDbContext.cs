using Microsoft.EntityFrameworkCore;

namespace Splitwise_clone.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Participant> Participants { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Creator)
                .WithMany()
                .HasForeignKey(t => t.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Participant>()
                .HasKey(p => new { p.TransactionId, p.UserId });

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Transaction)
                .WithMany(t => t.Participants)
                .HasForeignKey(p => p.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.User)
                .WithMany(u => u.Participants)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
