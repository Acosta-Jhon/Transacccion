using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tranferir.Models
{
    public partial class CuentasContext : DbContext, ICuentasContext
    {
        public CuentasContext()
        {
        }

        public CuentasContext(DbContextOptions<CuentasContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Cuentas> Cuentas { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public void GuardarCambios()
        {
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DbTrasancciones;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuentas>(entity =>
            {
                entity.ToTable("CUENTAS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dinero).HasColumnName("dinero");

                entity.Property(e => e.Nombre).HasColumnName("nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
