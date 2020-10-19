using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DotNetBankAPI_1.Models
{
    public partial class DotNetBankAPIContext : DbContext
    {
        public DotNetBankAPIContext()
        {
        }

        public DotNetBankAPIContext(DbContextOptions<DotNetBankAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<UserStore> UserStore { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=DotNetBankAPI;Integrated Security =True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerSsnId)
                    .HasName("PK__Customer__DCE85993759D7EA3");

                entity.Property(e => e.CustomerSsnId)
                    .HasColumnName("Customer SSN Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasColumnName("AddressLine 1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("addressLine 2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("Customer Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.AccountType })
                    .HasName("PK__tmp_ms_x__F918205FC873C492");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.AccountType)
                    .HasColumnName("Account Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SsnId).HasColumnName("SSN ID");

                entity.Property(e => e.Status1)
                    .HasColumnName("Status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Ssn)
                    .WithMany(p => p.Status)
                    .HasForeignKey(d => d.SsnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Status__SSN ID__3B75D760");
            });

            modelBuilder.Entity<UserStore>(entity =>
            {
                entity.HasKey(e => e.Login)
                    .HasName("PK__tmp_ms_x__5E55825AD7247573");

                entity.ToTable("userStore");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
