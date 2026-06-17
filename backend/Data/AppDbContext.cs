using Microsoft.EntityFrameworkCore;
using ProcessHub.Entities;

namespace ProcessHub.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Process> Processes => Set<Process>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<ProcessHistory> ProcessHistories => Set<ProcessHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureClient(modelBuilder);
            ConfigureProcess(modelBuilder);
            ConfigureUser(modelBuilder);
            ConfigureDocument(modelBuilder);
            ConfigureProcessHistory(modelBuilder);
        }

        private void ConfigureClient(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Clients");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(c => c.DocumentNumber)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany(c => c.Processes)
                      .WithOne(p => p.Client)
                      .HasForeignKey(p => p.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureProcess(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Process>(entity =>
            {
                entity.ToTable("Processes");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(p => p.Description)
                      .HasMaxLength(1000);

                entity.Property(p => p.Status)
                      .HasConversion<string>(); // Enum como string

                entity.HasOne(p => p.AssignedUser)
                      .WithMany(u => u.AssignedProcesses)
                      .HasForeignKey(p => p.AssignedUserId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(p => p.Documents)
                      .WithOne(d => d.Process)
                      .HasForeignKey(d => d.ProcessId);

                entity.HasMany(p => p.History)
                      .WithOne(h => h.Process)
                      .HasForeignKey(h => h.ProcessId);
            });
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasIndex(u => u.Email)
                      .IsUnique();

                entity.Property(u => u.Role)
                      .HasConversion<string>();
            });
        }

        private void ConfigureDocument(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Documents");

                entity.HasKey(d => d.Id);

                entity.Property(d => d.FileName)
                      .IsRequired()
                      .HasMaxLength(300);

                entity.Property(d => d.FilePath)
                      .IsRequired()
                      .HasMaxLength(500);
            });
        }

        private void ConfigureProcessHistory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessHistory>(entity =>
            {
                entity.ToTable("ProcessHistories");

                entity.HasKey(h => h.Id);

                entity.Property(h => h.OldStatus)
                      .HasConversion<string>();

                entity.Property(h => h.NewStatus)
                      .HasConversion<string>();

                entity.HasOne(h => h.ChangedByUser)
                      .WithMany(u => u.ChangesMade)
                      .HasForeignKey(h => h.ChangedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}