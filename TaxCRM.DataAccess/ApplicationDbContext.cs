using Microsoft.EntityFrameworkCore;
using TaxCRM.Domain.Entrepreneurs;
using TaxCRM.Domain.Incomes;

namespace TaxCRM.DataAccess;

public class ApplicationDbContext : DbContext
{
	public DbSet<Entrepreneur> Entrepreneurs => Set<Entrepreneur>();
    public DbSet<EntrepreneurProfile> EntrepreneurProfiles => Set<EntrepreneurProfile>();
    public DbSet<Income> Incomes => Set<Income>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Entrepreneur>(a =>
        {
            a.HasKey(e => e.Id);
            a.Property(e => e.FirstName).IsRequired().UseCollation("SQL_Latin1_General_CP1_CS_AS").HasMaxLength(100);
            a.Property(e => e.LastName).IsRequired().UseCollation("SQL_Latin1_General_CP1_CS_AS").HasMaxLength(100);
        });

        builder.Entity<EntrepreneurProfile>(a =>
        {
            a.HasKey(e => e.Id);
            a.Property(e => e.TaxPayerNumber).IsRequired().HasMaxLength(30);
            a.Property(e => e.Country).IsRequired().HasMaxLength(5);

            a.HasOne<Entrepreneur>()
                .WithMany()
                .HasForeignKey(e => e.EntrepreneurId)
                .Metadata.DeleteBehavior = DeleteBehavior.Cascade;
        });

        builder.Entity<Income>(a =>
        {
            a.HasKey(e => e.Id);
            a.Property(e => e.Date).IsRequired();

            a.OwnsOne(e => e.Value)
                .Property(p => p.Amount).IsRequired().HasColumnType("decimal(12, 2)");
            a.OwnsOne(e => e.Value)
                .Property(p => p.Currency).IsRequired();

            a.HasOne<EntrepreneurProfile>()
                .WithMany()
                .HasForeignKey(e => e.EntrepreneurProfileId)
                .Metadata.DeleteBehavior = DeleteBehavior.Cascade;
        });
    }
}
