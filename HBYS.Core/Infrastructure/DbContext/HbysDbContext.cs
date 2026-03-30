using HBYS.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HBYS.Core.Infrastructure.DbContext;

public class HbysDbContext(DbContextOptions<HbysDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Visit> Visits => Set<Visit>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    public DbSet<OrderResult> OrderResults => Set<OrderResult>();
    public DbSet<Diagnosis> Diagnoses => Set<Diagnosis>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Patient
        modelBuilder.Entity<Patient>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.TcNo).IsRequired().HasMaxLength(11);
            e.HasIndex(x => x.TcNo).IsUnique();
            e.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            e.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            e.Property(x => x.Gender).HasConversion<string>();
            e.HasMany(x => x.Visits).WithOne(x => x.Patient).HasForeignKey(x => x.PatientId);
        });

        // Visit
        modelBuilder.Entity<Visit>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.ProtocolNo).IsRequired().HasMaxLength(50);
            e.HasIndex(x => x.ProtocolNo).IsUnique();
            e.Property(x => x.Status).HasConversion<string>();
            e.HasMany(x => x.Orders).WithOne(x => x.Visit).HasForeignKey(x => x.VisitId);
            e.HasMany(x => x.Diagnoses).WithOne(x => x.Visit).HasForeignKey(x => x.VisitId);
            e.HasOne(x => x.Invoice).WithOne(x => x.Visit).HasForeignKey<Invoice>(x => x.VisitId);
        });

        // Order
        modelBuilder.Entity<Order>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Status).HasConversion<string>();
            e.HasMany(x => x.OrderDetails).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
        });

        // OrderDetail
        modelBuilder.Entity<OrderDetail>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.TestCode).IsRequired().HasMaxLength(50);
            e.Property(x => x.TestName).IsRequired().HasMaxLength(200);
            e.Property(x => x.Status).HasConversion<string>();
            e.HasOne(x => x.Result).WithOne(x => x.OrderDetail).HasForeignKey<OrderResult>(x => x.OrderDetailId);
        });

        // Invoice
        modelBuilder.Entity<Invoice>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.TotalAmount).HasPrecision(18, 2);
            e.Property(x => x.SGKAmount).HasPrecision(18, 2);
            e.Property(x => x.PatientAmount).HasPrecision(18, 2);
            e.Property(x => x.Status).HasConversion<string>();
            e.HasMany(x => x.Details).WithOne(x => x.Invoice).HasForeignKey(x => x.InvoiceId);
        });

        // InvoiceDetail
        modelBuilder.Entity<InvoiceDetail>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.UnitPrice).HasPrecision(18, 2);
            e.Ignore(x => x.TotalPrice);
        });

        base.OnModelCreating(modelBuilder);
    }
}
