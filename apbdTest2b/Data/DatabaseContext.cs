using apbdTest2b.Models;
using Microsoft.EntityFrameworkCore;

namespace apbdTest2b.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Concert> Concerts { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Purchase> Purchases { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Concert>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Date).IsRequired();
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Serial).IsRequired().HasMaxLength(50);
            entity.Property(e => e.SeatNumber).IsRequired();
            entity.HasOne(e => e.Concert)
                .WithMany(c => c.Tickets)
                .HasForeignKey(e => e.ConcertId);
        });
        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
            entity.HasOne(e => e.Customer)
                .WithMany(c => c.Purchases)
                .HasForeignKey(e => e.CustomerId);
            entity.HasOne(e => e.Ticket)
                .WithMany(t => t.Purchases)
                .HasForeignKey(e => e.TicketId);
        });
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, FirstName = "Jan", LastName = "Kowalski", PhoneNumber = "123456789" },
            new Customer { Id = 2, FirstName = "Janina", LastName = "Kowalska", PhoneNumber = "987654321" }
        );

        modelBuilder.Entity<Concert>().HasData(
            new Concert { Id = 1, Name = "Concert 1", Date = new DateTime(2025, 4, 8, 7, 0, 0) },
            new Concert { Id = 2, Name = "Concert 14", Date = new DateTime(2025, 5, 11, 6, 0, 0) }
        );

        modelBuilder.Entity<Purchase>().HasData(
            new Purchase { Id = 1, Date = new DateTime(2025, 6, 3, 9, 0, 0), Price = 33.4m, CustomerId = 1, TicketId = 1 },
            new Purchase { Id = 2, Date = new DateTime(2025, 6, 3, 9, 0, 0), Price = 48.4m, CustomerId = 1, TicketId = 2 }
        );
    }
}