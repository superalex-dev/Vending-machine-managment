using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Vending_Machines_Managment.Models
{
    public partial class VendingMachineManagmentContext : DbContext
    {
        public VendingMachineManagmentContext()
        {
        }

        public VendingMachineManagmentContext(DbContextOptions<VendingMachineManagmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VendingMachine> VendingMachines { get; set; } = null!;
        public virtual DbSet<VendingMachineType> VendingMachineTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VendingMachineManagment;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendingMachine>(entity =>
            {
                entity.ToTable("VendingMachine");

                entity.Property(e => e.InstalledOn).HasColumnType("datetime");

                entity.Property(e => e.Location).HasColumnType("text");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.VendingMachines)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VendingMa__TypeI__3A81B327");
            });

            modelBuilder.Entity<VendingMachineType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeId).ValueGeneratedNever();

                entity.Property(e => e.TypeInfo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
