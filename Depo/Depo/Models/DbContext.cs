using Depo.Models;
using Microsoft.EntityFrameworkCore;

public class WarehouseContext : DbContext
{
    public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options) { }

    public DbSet<Quality> Qualities { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Fabric> Fabrics { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<FabricExit> FabricExits { get; set; }
    public DbSet<Kullanici> Kullanicis { get; set; }  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fabric>()
            .HasOne(f => f.Quality)
            .WithMany()
            .HasForeignKey(f => f.QualityId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Fabric>()
            .HasOne(f => f.Color)
            .WithMany()
            .HasForeignKey(f => f.ColorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Fabric>()
            .HasOne(f => f.Location)
            .WithMany()
            .HasForeignKey(f => f.LocationId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Entry>()
            .HasOne(e => e.Fabric)
            .WithMany()
            .HasForeignKey(e => e.FabricId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Entry>()
            .HasOne(e => e.Location)
            .WithMany()
            .HasForeignKey(e => e.LocationId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<FabricExit>()
            .HasKey(fe => fe.ExitId);

        modelBuilder.Entity<FabricExit>()
            .HasOne(fe => fe.Fabric)
            .WithMany()
            .HasForeignKey(fe => fe.FabricId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<FabricExit>()
            .HasOne(fe => fe.Location)
            .WithMany()
            .HasForeignKey(fe => fe.LocationId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Kullanici>()
            .HasKey(k => k.UserId);

        modelBuilder.Entity<Logincs>()
            .HasKey(l => l.UserId);
        modelBuilder.Entity<Kullanici>().HasData(
              new Kullanici
              {
                  UserId = 1,
                  Mail = "user1@example.com",
                  Sifre = "password123",
                  LoggedStatus = true
              },
              new Kullanici
              {
                  UserId = 2,
                  Mail = "user2@example.com",
                  Sifre = "password456",
                  LoggedStatus = false
              }
                );

    }
}
