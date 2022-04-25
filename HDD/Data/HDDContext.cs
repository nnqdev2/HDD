
using Microsoft.EntityFrameworkCore;
using HDD.Models;
using HDD.Areas.Identity.Data;

namespace HDD.Data
{
    public partial class HDDContext : ApplicationDbContext
    {

        public HDDContext(DbContextOptions<HDDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmailCode> EmailCode { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<Dmvccddata> Dmvccddata { get; set; }
        public virtual DbSet<OwnersVin> OwnersVin { get; set; }
        public virtual DbSet<RetrofitApplication> RetrofitApplication { get; set; }
        public virtual DbSet<RetrofitCertification> RetrofitCertification { get; set; }
        public virtual DbSet<SecondaryOwnersAssignment> SecondaryOwnersAssignment { get; set; }
        public virtual DbSet<ValidCertifiedTypes> ValidCertifiedTypes { get; set; }
        public virtual DbSet<ValidRetrofitProviders> ValidRetrofitProviders { get; set; }
        public virtual DbSet<ValidRetrofitTypes> ValidRetrofitTypes { get; set; }
        public virtual DbSet<VehicleDocuments> VehicleDocuments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dmvccddata>(entity =>
            {
                entity.HasKey(e => e.Vin)
                    .HasName("PK_DMVData");

                entity.ToTable("DMVCCDData", "dbo");

                entity.Property(e => e.Vin)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VIN");

                entity.Property(e => e.ChangedOwnership)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.County)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EntryDateTime).HasColumnType("datetime");

                entity.Property(e => e.Gvw).HasColumnName("GVW");

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Plate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PubliclyOwned)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.RegistrationExpiration)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RenewalAgency)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RunDate).HasColumnType("datetime");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.WeightRange)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ZIP");
            });

            modelBuilder.Entity<OwnersVin>(entity =>
            {
                entity.HasKey(e => new { e.OwnerId, e.Vin });

                entity.ToTable("OwnersVIN", "dbo");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.Vin)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VIN");

                entity.Property(e => e.PrimaryOwner)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.VinNavigation)
                    .WithMany(p => p.OwnersVin)
                    .HasForeignKey(d => d.Vin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OwnersVIN_DMVCCDData");
            });

            modelBuilder.Entity<RetrofitApplication>(entity =>
            {
                entity.HasKey(e => e.Vin);

                entity.ToTable("RetrofitApplication", "dbo");

                entity.Property(e => e.Vin)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VIN");

                entity.Property(e => e.ApplicationDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ArtFamilyName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ART_FamilyName");

                entity.Property(e => e.Comments)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EngineDisplacement)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.EngineFamilyNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EngineManufacturer)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EntryDateTime).HasColumnType("datetime");

                entity.Property(e => e.RetrofitProvider)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.RetrofitType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.RunDate).HasColumnType("datetime");

                entity.HasOne(d => d.RetrofitProviderNavigation)
                    .WithMany(p => p.RetrofitApplication)
                    .HasForeignKey(d => d.RetrofitProvider)
                    .HasConstraintName("FK_RetrofitApplication_ValidRetrofitProviders");

                entity.HasOne(d => d.RetrofitTypeNavigation)
                    .WithMany(p => p.RetrofitApplication)
                    .HasForeignKey(d => d.RetrofitType)
                    .HasConstraintName("FK_RetrofitApplication_ValidRetrofitTypes");

                entity.HasOne(d => d.VinNavigation)
                    .WithOne(p => p.RetrofitApplication)
                    .HasForeignKey<RetrofitApplication>(d => d.Vin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RetrofitApplication_DMVCCDData");
            });

            modelBuilder.Entity<RetrofitCertification>(entity =>
            {
                entity.HasKey(e => e.Vin);

                entity.ToTable("RetrofitCertification", "dbo");

                entity.Property(e => e.Vin)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VIN");

                entity.Property(e => e.ActionDate).HasColumnType("datetime");

                entity.Property(e => e.ActionInspectorId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ActionInspectorID");

                entity.Property(e => e.AgencyChanged)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Certified)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Comments)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EntryDateTime).HasColumnType("datetime");

                entity.Property(e => e.EntryUserId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EntryUserID");

                entity.Property(e => e.Vinstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VINStatus")
                    .IsFixedLength(true);

                entity.HasOne(d => d.CertifiedNavigation)
                    .WithMany(p => p.RetrofitCertification)
                    .HasForeignKey(d => d.Certified)
                    .HasConstraintName("FK_RetrofitCertification_ValidCertifiedTypes");

                entity.HasOne(d => d.VinNavigation)
                    .WithOne(p => p.RetrofitCertification)
                    .HasForeignKey<RetrofitCertification>(d => d.Vin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RetrofitCertification_DMVCCDData");
            });

            modelBuilder.Entity<SecondaryOwnersAssignment>(entity =>
            {
                entity.HasKey(e => new { e.OwnerId, e.Vin, e.IncomingSecondaryOwnerEmail });

                entity.ToTable("SecondaryOwnersAssignment", "dbo");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.Vin)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VIN");

                entity.Property(e => e.IncomingSecondaryOwnerEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.AssignedDate).HasColumnType("datetime");

                entity.HasOne(d => d.VinNavigation)
                    .WithMany(p => p.SecondaryOwnersAssignment)
                    .HasForeignKey(d => d.Vin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SecondaryOwnersAssignment_DMVCCDData");
            });

            modelBuilder.Entity<ValidCertifiedTypes>(entity =>
            {
                entity.HasKey(e => e.CertifiedType);

                entity.ToTable("ValidCertifiedTypes", "dbo");

                entity.Property(e => e.CertifiedType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ValidRetrofitProviders>(entity =>
            {
                entity.HasKey(e => e.RetrofitProvoder);

                entity.ToTable("ValidRetrofitProviders", "dbo");

                entity.Property(e => e.RetrofitProvoder)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ValidRetrofitTypes>(entity =>
            {
                entity.HasKey(e => e.RetrofitType);

                entity.ToTable("ValidRetrofitTypes", "dbo");

                entity.Property(e => e.RetrofitType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VehicleDocuments>(entity =>
            {
                entity.HasKey(e => new { e.Vin, e.DocumentPath });

                entity.ToTable("VehicleDocuments", "dbo");

                entity.Property(e => e.Vin)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VIN");

                entity.Property(e => e.DocumentPath)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.VinNavigation)
                    .WithMany(p => p.VehicleDocuments)
                    .HasForeignKey(d => d.Vin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleDocuments_RetrofitApplication");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}