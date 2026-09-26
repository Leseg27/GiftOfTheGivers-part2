using GiftOfTheGivers.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Donation> Donations => Set<Donation>();
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<ReliefProject> ReliefProjects => Set<ReliefProject>();
    public DbSet<ReliefUpdate> ReliefUpdates => Set<ReliefUpdate>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Donations
        builder.Entity<Donation>()
            .HasIndex(d => d.CreatedAt);

        builder.Entity<Donation>()
            .HasIndex(d => d.TaxCertificateNumber)
            .IsUnique()
            .HasFilter("[TaxCertificateNumber] IS NOT NULL");

        builder.Entity<Donation>()
            .HasOne(d => d.User)
            .WithMany(u => u.Donations)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Volunteers
        builder.Entity<Volunteer>()
            .HasIndex(v => v.Email);

        // Relief updates → project
        builder.Entity<ReliefUpdate>()
            .HasOne(u => u.ReliefProject)
            .WithMany(p => p.Updates)
            .HasForeignKey(u => u.ReliefProjectId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relief updates → user
        builder.Entity<ReliefUpdate>()
            .HasOne(u => u.PostedBy)
            .WithMany(a => a.ReliefUpdates)
            .HasForeignKey(u => u.PostedByUserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}