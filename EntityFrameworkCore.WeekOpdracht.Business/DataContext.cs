using EntityFrameworkCore.WeekOpdracht.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.WeekOpdracht.Business
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost,1433;Database=WeekOpdrachtTDL;User ID=sa;Password=Figlo@2016!#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .Property(b => b.DateTimeSend)
                .HasDefaultValueSql("getdate()");
        }
    }
}
